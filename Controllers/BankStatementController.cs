using Azure;
using Azure.AI.DocumentIntelligence;

using FileUploadReader.Interface;
using FileUploadReader.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace FileUploadReader.Controllers
{
    public class BankStatementController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private readonly AzureCognitiveSettings _azureSettings;

        public BankStatementController(IWebHostEnvironment environment, AzureCognitiveSettings azureSettings)
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
            if (TempData["ExtractedTransactions"] is string extractedTransactionJson)
            {
                var extractedItems = System.Text.Json.JsonSerializer.Deserialize<List<BankTransaction>>(extractedTransactionJson);
                ViewBag.ExtractedTransactions = extractedItems;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadBankStatement(IFormFile file)
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

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AnalyzeBankStatement()
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

            var client = new DocumentIntelligenceClient(new Uri(endpoint), new AzureKeyCredential(key));

            using var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            var binaryData = BinaryData.FromStream(stream);

            // ✅ Use BinaryData directly
            var operation = await client.AnalyzeDocumentAsync(
                WaitUntil.Completed,
                //"prebuilt-bankStatement.us",
                "prebuilt-read",
                binaryData
            );

            Azure.AI.DocumentIntelligence.AnalyzeResult result = operation.Value;

            var d = result.Documents.FirstOrDefault();
            IInvoiceAdapter adapter = new SdkBankStatementAdapter(d);
            List<InvoiceField> fields;
            fields = adapter.ExtractFields();

            var transactions = new SdkBankStatementTransactionAdapter(d);
            var list = transactions.ExtractTransactions();

            TempData["UploadedFilePath"] = relativePath;
            TempData["ExtractedFields"] = System.Text.Json.JsonSerializer.Serialize(fields);
            TempData["ExtractedTransactions"] = System.Text.Json.JsonSerializer.Serialize(list);
            return RedirectToAction("Index");
        }

    }
}
