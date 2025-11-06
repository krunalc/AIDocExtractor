using System.Text.Json;
using System.Text;
using System.Net;
using FileUploadReader.ViewModels;

namespace FileUploadReader.DataHelper
{
  public static class DataHelper
  {
    private static AzureCognitiveSettings _settings;

    public static void Initialize(AzureCognitiveSettings settings)
    {
      _settings = settings;
    }

    public static async Task<Response<T>> AnalyzeDocumentAsync<T>(string fullPath, string modelName)
    {
      var result = new Response<T>();

      if (string.IsNullOrEmpty(fullPath) || !File.Exists(fullPath))
      {
        result.Result = false;
        result.Message = "File not found or path is invalid.";
        return result;
      }

      try
      {
        byte[] fileBytes = await File.ReadAllBytesAsync(fullPath);
        string base64Image = Convert.ToBase64String(fileBytes);

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _settings.Key);

        //string analyzeUrl = $"{_settings.BaseURL}/{modelName}:analyze?api-version=2023-07-31";
        string analyzeUrl = $"{_settings.BaseURL}/{modelName}:analyze?api-version=2024-07-31-preview";
        var analyzeRequest = new
        {
          base64Source = base64Image
        };

        var content = new StringContent(JsonSerializer.Serialize(analyzeRequest), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(analyzeUrl, content);

        await Task.Delay(5000);
        if (response.StatusCode != HttpStatusCode.Accepted)
        {
          var m = response.Content.ReadAsStringAsync();
          result.Result = false;
          result.Message = "Analyze request failed.";
          return result;
        }

        if (!response.Headers.TryGetValues("Operation-Location", out var values))
        {
          result.Result = false;
          result.Message = "Operation-Location header missing.";
          return result;
        }

        string operationUrl = values.First();
        await Task.Delay(3000); // Optional: Replace with polling loop for production
        if (string.IsNullOrEmpty(operationUrl))
        {
          result.Result = false;
          result.Message = "Operation-Location header not found.";
          return result;
        }

        var pollResponse = await httpClient.GetAsync(operationUrl);

        if (!pollResponse.IsSuccessStatusCode)
        {
          result.Result = false;
          result.Message = "Polling failed.";
          return result;
        }

        string resultJson = await pollResponse.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var root = JsonSerializer.Deserialize<T>(resultJson, options);

        result.Data = root;
        result.Result = true;
        result.Message = "Success";
        return result;
      }
      catch (Exception)
      {
        result.Result = false;
        result.Message = "Error Occured.";
        return result;
      }
    }
}
}
