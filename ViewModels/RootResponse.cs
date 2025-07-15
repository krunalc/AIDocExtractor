
using System.Reflection.Metadata;

namespace FileUploadReader.ViewModels
{
  public class RootResponse
  {
    public AnalyzeResult analyzeResult { get; set; }
  }

  public class AnalyzeResult1
  {
    public List<Document> documents { get; set; }
  }

  public class Document1
  {
    public Dictionary<string, Field> fields { get; set; }
  }

  public class Field1
  {
    public string type { get; set; }
    public string content { get; set; }
  }

}
