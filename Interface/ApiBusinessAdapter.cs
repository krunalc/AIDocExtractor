using Azure.AI.FormRecognizer.DocumentAnalysis;

using FileUploadReader.ViewModels;

namespace FileUploadReader.Interface
{
  public class ApiBusinessAdapter : IInvoiceAdapter
  {
    private readonly ViewModels.DocumentB _document;

    public ApiBusinessAdapter(ViewModels.DocumentB document)
    {
      _document = document;
    }

    public List<InvoiceField> ExtractFields()
    {
      var fields = new List<InvoiceField>();

      if (_document.Fields.TryGetValue("ContactNames", out var vendor) && vendor != null)
      {
        foreach (var v in vendor.ValueArray)
        {
          if (v.ValueObject.TryGetValue("FirstName", out var firstNameField))
          {
            fields.Add(new InvoiceField
            {
              Key = "FirstName",
              Value = firstNameField.Content,
              Confidence = firstNameField.Confidence
            });
          }

          if (v.ValueObject.TryGetValue("LastName", out var lastNameField))
          {
            fields.Add(new InvoiceField
            {
              Key = "LastName",
              Value = lastNameField.Content,
              Confidence = lastNameField.Confidence
            });
          }
        }
      }

      if (_document.Fields.TryGetValue("Emails", out var e) && e != null)
      {
        foreach (var v in e.ValueArray)
        {
          fields.Add(new InvoiceField
          {
            Key = "Email",
            Value = v.Content,
            Confidence = v.Confidence
          });
        }

      }


      if (_document.Fields.TryGetValue("JobTitles", out var j) && j != null)
      {
        foreach (var v in j.ValueArray)
        {
          fields.Add(new InvoiceField
          {
            Key = "JobTitle",
            Value = v.Content,
            Confidence = v.Confidence
          });
        }
      }


      if (_document.Fields.TryGetValue("Websites", out var w) && w != null)
      {
        foreach (var v in w.ValueArray)
        {
          fields.Add(new InvoiceField
          {
            Key = "Website",
            Value = v.Content,
            Confidence = v.Confidence
          });
        }
      }


      if (_document.Fields.TryGetValue("MobilePhones", out var m) && m != null)
      {
        foreach (var v in m.ValueArray)
        {
          fields.Add(new InvoiceField
          {
            Key = "Mobile Phone",
            Value = v.Content,
            Confidence = v.Confidence
          });
        }
      }


      if (_document.Fields.TryGetValue("WorkPhones", out var wp) && wp != null)
      {
        foreach (var v in wp.ValueArray)
        {
          fields.Add(new InvoiceField
          {
            Key = "Work Phone",
            Value = v.Content,
            Confidence = v.Confidence
          });
        }
      }


      if (_document.Fields.TryGetValue("Faxes", out var f) && f != null)
      {
        foreach (var v in f.ValueArray)
        {
          fields.Add(new InvoiceField
          {
            Key = "Faxe",
            Value = v.Content,
            Confidence = v.Confidence
          });
        }
      }

      if (_document.Fields.TryGetValue("Addresses", out var add) && add != null)
      {
        foreach (var v in add.ValueArray)
        {
          fields.Add(new InvoiceField
          {
            Key = "Address",
            Value = v.Content,
            Confidence = v.Confidence
          });
        }
      }

      if (_document.Fields.TryGetValue("Departments", out var d) && d != null)
      {
        foreach (var v in d.ValueArray)
        {
          fields.Add(new InvoiceField
          {
            Key = "Department",
            Value = v.Content,
            Confidence = v.Confidence
          });
        }
      }

      return fields;
    }
  }
}
