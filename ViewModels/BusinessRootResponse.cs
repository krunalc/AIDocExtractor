using Azure.AI.FormRecognizer.DocumentAnalysis;
using System;

namespace FileUploadReader.ViewModels
{
  public class BusinessRootResponse
  {
    public AnalyzeResultB AnalyzeResult { get; set; }
  }

  public class AnalyzeResultB
  {
    public string ApiVersion { get; set; }
    public string ModelId { get; set; }
    public string StringIndexType { get; set; }
    public string Content { get; set; }
    public List<DocumentB> Documents { get; set; }
    public string ContentFormat { get; set; }
  }

  public class DocumentB
  {
    public string DocType { get; set; }
    public List<BoundingRegion> BoundingRegions { get; set; }
    public Dictionary<string, FieldB> Fields { get; set; }
    public double Confidence { get; set; }
  }

  public class FieldB
  {
    public string Type { get; set; }
    public string Content { get; set; }
    public List<BoundingRegion> BoundingRegions { get; set; }
    public float? Confidence { get; set; }
    public string ValuePhoneNumber { get; set; }
    public ValueAddress ValueAddress { get; set; }
    public List<ItemB> ValueArray { get; set; }
    public Dictionary<string, FieldB> ValueObject { get; set; }

  }

  public class ItemB
  {
    public Dictionary<string, FieldB> ValueObject { get; set; }  // for ContactNames, valueObject like FirstName, LastName
    public string Content { get; set; }
    public float? Confidence { get; set; }
    public string Type { get; set; }
    public string ValueString { get; set; }
    public string ValuePhoneNumber { get; set; }
  }

}
