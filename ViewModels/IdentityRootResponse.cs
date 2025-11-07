using Azure.AI.FormRecognizer.DocumentAnalysis;
using System;

namespace FileUploadReader.ViewModels
{
  public class IdentityRootResponse
  {
    public AnalyzeResultI AnalyzeResult { get; set; }
  }

  public class AnalyzeResultI
  {
    public string ApiVersion { get; set; }
    public string ModelId { get; set; }
    public string StringIndexType { get; set; }
    public string Content { get; set; }
    public List<DocumentI> Documents { get; set; }
  }

  public class DocumentI
  {
    public string DocType { get; set; }
    public List<BoundingRegion> BoundingRegions { get; set; }
    public Dictionary<string, FieldI> Fields { get; set; }
    public double Confidence { get; set; }
  }

  public class FieldI
  {
    public string Type { get; set; }
    public string Content { get; set; }
    public float Confidence { get; set; }

    public ValueAddress ValueAddress { get; set; }               // For address
    public List<FieldItemI> ValueArray { get; set; }              // For arrays like ContactNames, WorkPhones
    public Dictionary<string, FieldI> ValueObject { get; set; }   // For objects like FirstName/LastName
    public string ValueString { get; set; }                      // For string values
    public string ValuePhoneNumber { get; set; }                 // For phone numbers
    public string ValueCountryRegion { get; set; }                 // For phone numbers
  }

  public class FieldItemI
  {
    public string Type { get; set; }
    public string Content { get; set; }
    public List<BoundingRegion> BoundingRegions { get; set; }
    public float Confidence { get; set; }
    public ValueAddress ValueAddress { get; set; }
    public Dictionary<string, Field> ValueObject { get; set; }
    public string ValueString { get; set; }
    public string ValuePhoneNumber { get; set; }
  }
}
