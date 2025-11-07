using FileUploadReader.ViewModels;

namespace FileUploadReader.Interface
{
  public class ApiIdentityAdapter : IInvoiceAdapter
  {
    private readonly ViewModels.DocumentI _document;

    public ApiIdentityAdapter(ViewModels.DocumentI document)
    {
      _document = document;
    }

    public List<InvoiceField> ExtractFields()
    {
      var fields = new List<InvoiceField>();

      if (_document.Fields.TryGetValue("FirstName", out var fn) && fn != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "FirstName",
          Value = fn.Content,
          Confidence = fn.Confidence
        });
      }

      if (_document.Fields.TryGetValue("LastName", out var ln) && ln != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "LastName",
          Value = ln.Content,
          Confidence = ln.Confidence
        });
      }

      if (_document.Fields.TryGetValue("fatherName", out var fan) && fan != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Father Name",
          Value = fan.Content,
          Confidence = fan.Confidence
        });
      }

      if (_document.Fields.TryGetValue("DateOfBirth", out var d) && d != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Date Of Birth",
          Value = d.Content,
          Confidence = d.Confidence
        });
      }

      if (_document.Fields.TryGetValue("Sex", out var s) && s != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Sex",
          Value = s.Content,
          Confidence = s.Confidence
        });
      }

      if (_document.Fields.TryGetValue("Address", out var e) && e != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Address",
          Value = e.Content,
          Confidence = e.Confidence
        });
      }
    
      if (_document.Fields.TryGetValue("CountryRegion", out var c) && c != null)
      {

        fields.Add(new InvoiceField
        {
          Key = "Country Region",
          Value = c.ValueCountryRegion,
          Confidence = c.Confidence
        });
      }

     

      if (_document.Fields.TryGetValue("DateOfExpiration", out var de) && de != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Date Of Expiration",
          Value = de.Content,
          Confidence = de.Confidence
        });
      }

      if (_document.Fields.TryGetValue("DateOfIssue", out var di) && di != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Date Of Issue",
          Value = di.Content,
          Confidence = di.Confidence
        });
      }

      if (_document.Fields.TryGetValue("DocumentNumber", out var dn) && dn != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Document Number",
          Value = dn.Content,
          Confidence = dn.Confidence
        });
      }

      //if (_document.Fields.TryGetValue("Region", out var r) && r != null)
      //{
      //  fields.Add(new InvoiceField
      //  {
      //    Key = "Region",
      //    Value = r.Content,
      //    Confidence = r.Confidence
      //  });
      //}

    

      return fields;
    }
  }
}
