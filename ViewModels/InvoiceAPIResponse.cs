namespace FileUploadReader.ViewModels
{
  public class InvoiceAPIResponse
  {
    public string Status { get; set; }
    public string CreatedDateTime { get; set; }
    public string LastUpdatedDateTime { get; set; }
    public AnalyzeResult AnalyzeResult { get; set; }
  }

  public class AnalyzeResult
  {
    public string ApiVersion { get; set; }
    public string ModelId { get; set; }
    public string StringIndexType { get; set; }
    public List<Document> Documents { get; set; }
    public string Content { get; set; }
  }

  public class Document
  {
    public string DocType { get; set; }
    public Dictionary<string, Field> Fields { get; set; }
    public float Confidence { get; set; }
  }

  public class Field
  {
    public string Type { get; set; }
    public string Content { get; set; }

    public ValueCurrency ValueCurrency { get; set; }
    public string ValueString { get; set; }
    public double? ValueNumber { get; set; }
    public string ValuePhoneNumber { get; set; }
    public string ValueCountryRegion { get; set; }
    public string ValueDate { get; set; }
    public ValueAddress ValueAddress { get; set; }
    public float Confidence { get; set; }
    public List<Item> ValueArray { get; set; }  // For Items[]
  }

  public class ValueCurrency
  {
    public string CurrencySymbol { get; set; }
    public double Amount { get; set; }
    public string CurrencyCode { get; set; }
  }

  public class ValueAddress
  {
    public string HouseNumber { get; set; }
    public string Road { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string CountryRegion { get; set; }
    public string StreetAddress { get; set; }
  }

  public class Item
  {
    public Dictionary<string, Field> ValueObject { get; set; }
    public string Content { get; set; }
  }
}
