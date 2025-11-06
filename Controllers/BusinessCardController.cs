using Azure;
using Microsoft.AspNetCore.Mvc;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using System.Net;
using FileUploadReader.Interface;
using FileUploadReader.ViewModels;

namespace FileUploadReader.Controllers
{
  public class BusinessCardController : Controller
  {
    private readonly IWebHostEnvironment _environment;
    private readonly AzureCognitiveSettings _azureSettings;

    public BusinessCardController(IWebHostEnvironment environment, AzureCognitiveSettings azureSettings)
    {
      _environment = environment;
      _azureSettings = azureSettings;
    }

    public IActionResult Index()
    {
      if (TempData["UploadedFilePath"] is string filePath)
      {
        ViewBag.UploadedFilePath = filePath;
      }
      if (TempData["ExtractedFields"] is string extractedFieldJson)
      {
        var extractedItems = System.Text.Json.JsonSerializer.Deserialize<List<InvoiceField>>(extractedFieldJson);
        ViewBag.ExtractedFields = extractedItems;
      }
      return View();
    }

        [HttpPost]
        public async Task<IActionResult> UploadBusinessCard(IFormFile file, string sampleFilePath)
        {
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                Directory.CreateDirectory(uploads);

                var filePath = Path.Combine(uploads, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                TempData["UploadedFilePath"] = $"/uploads/{file.FileName}";
                ViewBag.UploadedFilePath = $"/uploads/{file.FileName}";
                HttpContext.Session.SetString("UploadedFilePath", $"/uploads/{file.FileName}");
            }
            
            else if (!string.IsNullOrEmpty(sampleFilePath))
            {
                var normalizedPath = sampleFilePath
                    .Replace("~", string.Empty)
                    .TrimStart('/', '\\')
                    .Replace('/', Path.DirectorySeparatorChar)
                    .Replace('\\', Path.DirectorySeparatorChar);

                var sourcePath = Path.Combine(_environment.WebRootPath, normalizedPath);

                if (System.IO.File.Exists(sourcePath))
                {
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploads);

                    var fileName = Path.GetFileName(sourcePath);
                    var destinationPath = Path.Combine(uploads, fileName);

                    if (System.IO.File.Exists(destinationPath))
                    {
                        try
                        {
                            System.IO.File.Delete(destinationPath);
                        }
                        catch (IOException)
                        {
                            var nameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
                            var ext = Path.GetExtension(fileName);
                            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                            destinationPath = Path.Combine(uploads, $"{nameWithoutExt}_{timestamp}{ext}");
                        }
                    }

                    using (var sourceStream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (var destStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await sourceStream.CopyToAsync(destStream);
                    }

                    var fileNameOnly = Path.GetFileName(destinationPath);
                    TempData["UploadedFilePath"] = $"/uploads/{fileNameOnly}";
                    ViewBag.UploadedFilePath = $"/uploads/{fileNameOnly}";
                    HttpContext.Session.SetString("UploadedFilePath", $"/uploads/{fileNameOnly}");
                }
                else
                {
                    TempData["Error"] = $"Sample business card file not found: {sourcePath}";
                }
            }
            else
            {
                TempData["Error"] = "Please upload or select a sample business card.";
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
    public async Task<IActionResult> AnalyzeBusinessCard()
    {
      var relativePath = HttpContext.Session.GetString("UploadedFilePath");
      if (string.IsNullOrEmpty(relativePath))
      {
        TempData["Error"] = "No file uploaded.";
        return RedirectToAction("Index");
      }

      var fullPath = Path.Combine(_environment.WebRootPath, relativePath.TrimStart('/'));

      var endpoint = _azureSettings.Endpoint;
      var key = _azureSettings.Key;

      var credential = new AzureKeyCredential(key);
      var client = new DocumentAnalysisClient(new Uri(endpoint), credential);

      using var stream = new FileStream(fullPath, FileMode.Open);

      AnalyzeDocumentOperation operation = await client.AnalyzeDocumentAsync(
          WaitUntil.Completed,
          "prebuilt-businessCard",
          stream);

      Azure.AI.FormRecognizer.DocumentAnalysis.AnalyzeResult result = operation.Value;

      AnalyzedDocument doc = result.Documents.First();

      IInvoiceAdapter adapter = new SdkBusinessAdapter(doc);

      List<InvoiceField> fields;
      fields = adapter.ExtractFields();

      TempData["UploadedFilePath"] = relativePath;
      TempData["ExtractedFields"] = System.Text.Json.JsonSerializer.Serialize(fields);
      return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AnalyzeBusinessCardAPI()
    {
      var relativePath = HttpContext.Session.GetString("UploadedFilePath");
      var fullPath = Path.Combine(_environment.WebRootPath, relativePath.TrimStart('/'));

      if (string.IsNullOrEmpty(fullPath))
      {
        TempData["Error"] = "No file uploaded.";
        return RedirectToAction("Index");
      }

      var dataHelperResponse = await DataHelper.DataHelper.AnalyzeDocumentAsync<BusinessRootResponse>(fullPath, "prebuilt-businessCard");

      if (dataHelperResponse.Data == null || !dataHelperResponse.Result)
      {
        TempData["Error"] = dataHelperResponse.Message;
        return RedirectToAction("Index");
      }

      var root = dataHelperResponse.Data;
      DocumentB doc = root?.AnalyzeResult.Documents.First();
      IInvoiceAdapter adapter = new ApiBusinessAdapter(doc);

      List<InvoiceField> fields;
      fields = adapter.ExtractFields();

      TempData["UploadedFilePath"] = relativePath;
      TempData["ExtractedFields"] = System.Text.Json.JsonSerializer.Serialize(fields);
      return RedirectToAction("Index");
    }
  }
}
