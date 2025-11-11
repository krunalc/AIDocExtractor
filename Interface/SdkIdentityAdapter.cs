using Azure.AI.FormRecognizer.DocumentAnalysis;

using FileUploadReader.ViewModels;

namespace FileUploadReader.Interface
{
  public class SdkIdentityAdapter : IInvoiceAdapter
  {
    private readonly AnalyzedDocument _document;

    public SdkIdentityAdapter(AnalyzedDocument document)
    {
      _document = document;
    }


    public List<InvoiceField> ExtractFields()
    {
      var fields = new List<InvoiceField>();

      if (_document.Fields.TryGetValue("FirstName", out var first) && first != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "First Name",
          Value = first.Content?.ToString(),
          Confidence = first.Confidence
        });
      }

      if (_document.Fields.TryGetValue("LastName", out var last) && last != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Last Name",
          Value = last.Content?.ToString(),
          Confidence = last.Confidence
        });
      }

      if (_document.Fields.TryGetValue("FatherName", out var father) && father != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Father Name",
          Value = father.Content?.ToString(),
          Confidence = father.Confidence
        });
      }

      if (_document.Fields.TryGetValue("DateOfBirth", out var d) && d != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Date Of Birth",
          Value = d.Content?.ToString(),
          Confidence = d.Confidence
        });
      }

      if (_document.Fields.TryGetValue("Sex", out var sex) && sex != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Sex",
          Value = sex.Content?.ToString(),
          Confidence = sex.Confidence
        });
      }


      if (_document.Fields.TryGetValue("Address", out var add) && add != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Address",
          Value = add.Content?.ToString(),
          Confidence = add.Confidence
        });
      }

      if (_document.Fields.TryGetValue("CountryRegion", out var cou) && cou != null)
      {
                var internalValueProp = cou.Value.GetType().GetProperty("InternalValue",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                var internalValue = internalValueProp?.GetValue(cou.Value);

                fields.Add(new InvoiceField
                {
                    Key = "Country Region",
                    Value = internalValue?.ToString(), // This will give you "USA"
                    Confidence = cou.Confidence
                });
                //fields.Add(new InvoiceField
                //{
                //  Key = "Country Region",
                //  Value = cou.Value.ToString(),
                //  Confidence = cou.Confidence
                //});
            }

      if (_document.Fields.TryGetValue("DateOfExpiration", out var date) && date != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Date Of Expiration",
          Value = date.Content?.ToString(),
          Confidence = date.Confidence
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


      if (_document.Fields.TryGetValue("DocumentNumber", out var doc) && doc != null)
      {
        fields.Add(new InvoiceField
        {
          Key = "Document Number",
          Value = doc.Content?.ToString(),
          Confidence = doc.Confidence
        });
      }

      

      //if (_document.Fields.TryGetValue("Region", out var reg) && reg != null)
      //{
      //  fields.Add(new InvoiceField
      //  {
      //    Key = "Region",
      //    Value = reg.Content?.ToString(),
      //    Confidence = reg.Confidence
      //  });
      //}

     
      return fields;
    }
  }
}
