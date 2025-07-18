using Azure.AI.FormRecognizer.DocumentAnalysis;

using FileUploadReader.ViewModels;

namespace FileUploadReader.Interface
{
    public class SdkBusinessAdapter : IInvoiceAdapter
    {
        private readonly AnalyzedDocument _document;

        public SdkBusinessAdapter(AnalyzedDocument document)
        {
            _document = document;
        }

        public List<InvoiceField> ExtractFields()
        {
            var fields = new List<InvoiceField>();

            if (_document.Fields.TryGetValue("ContactNames", out var contName) &&
              contName != null)
            {
                foreach (var contact in contName.Value.AsList())
                {
                    if (contact.FieldType == DocumentFieldType.Dictionary)
                    {
                        var dict = contact.Value.AsDictionary();
                        if (dict.TryGetValue("FirstName", out var firstNameField))
                        {
                            fields.Add(new InvoiceField
                            {
                                Key = "FirstName",
                                Value = firstNameField.Content,
                                Confidence = firstNameField.Confidence
                            });
                        }

                        if (dict.TryGetValue("LastName", out var lastNameField))
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
            }
            if (_document.Fields.TryGetValue("Emails", out var email) &&
             email != null)
            {
                foreach (var e in email.Value.AsList())
                {
                    fields.Add(new InvoiceField
                    {
                        Key = "Email",
                        Value = e.Content,
                        Confidence = e.Confidence
                    });
                }
            }

            



            if (_document.Fields.TryGetValue("JobTitles", out var job) &&
            job != null)
            {
                if (job.FieldType == DocumentFieldType.List)
                {
                    foreach (DocumentField jobTitleField in job.Value.AsList())
                    {
                        if (jobTitleField.FieldType == DocumentFieldType.String)
                        {
                            string jobTitle = jobTitleField.Value.AsString();

                            fields.Add(new InvoiceField
                            {
                                Key = "Job Title",
                                Value = jobTitleField.Content,
                                Confidence = jobTitleField.Confidence
                            });

                        }
                    }
                }
            }

            if (_document.Fields.TryGetValue("Websites", out var web) &&
            web != null)
            {
                if (web.FieldType == DocumentFieldType.List)
                {
                    foreach (DocumentField w in web.Value.AsList())
                    {
                        if (w.FieldType == DocumentFieldType.String)
                        {
                            string jobTitle = w.Value.AsString();

                            fields.Add(new InvoiceField
                            {
                                Key = "Website",
                                Value = w.Content,
                                Confidence = w.Confidence
                            });

                        }
                    }
                }
            }

            if (_document.Fields.TryGetValue("MobilePhones", out var mob) &&
            mob != null)
            {
                if (mob.FieldType == DocumentFieldType.List)
                {
                    foreach (DocumentField m in mob.Value.AsList())
                    {
                        if (m.FieldType == DocumentFieldType.String)
                        {
                            string jobTitle = m.Value.AsString();

                            fields.Add(new InvoiceField
                            {
                                Key = "Mobile Phone",
                                Value = m.Content,
                                Confidence = m.Confidence
                            });

                        }
                    }
                }
            }

            if (_document.Fields.TryGetValue("WorkPhones", out var work) &&
            work != null)
            {
                if (work.FieldType == DocumentFieldType.List)
                {
                    foreach (DocumentField w in work.Value.AsList())
                    {
                        if (w.FieldType == DocumentFieldType.PhoneNumber)
                        {
                            string jobTitle = w.Content;

                            fields.Add(new InvoiceField
                            {
                                Key = "Work Phone",
                                Value = w.Content,
                                Confidence = w.Confidence
                            });
                        }

                        if (w.FieldType == DocumentFieldType.Unknown)
                        {
                            if (!string.IsNullOrEmpty(w.Content))
                            {
                                fields.Add(new InvoiceField
                                {
                                    Key = "Work Phone",
                                    Value = w.Content,
                                    Confidence = w.Confidence
                                });
                            }
                        }
                    }
                }
            }

            if (_document.Fields.TryGetValue("Faxes", out var fax) &&
            fax != null)
            {
                foreach (DocumentField f in fax.Value.AsList())
                {
                    if (f.FieldType == DocumentFieldType.String)
                    {
                        string jobTitle = f.Value.AsString();

                        fields.Add(new InvoiceField
                        {
                            Key = "Fax",
                            Value = f.Content,
                            Confidence = f.Confidence
                        });
                    }
                }
            }

            if (_document.Fields.TryGetValue("Addresses", out var add) &&
            add != null)
            {
                foreach (DocumentField a in add.Value.AsList())
                {
                    if (a.FieldType == DocumentFieldType.Address)
                    {
                        string jobTitle = a.Content;

                        fields.Add(new InvoiceField
                        {
                            Key = "Address",
                            Value = a.Content,
                            Confidence = a.Confidence
                        });
                    }
                }
            }

            if (_document.Fields.TryGetValue("Departments", out var dept) &&
            dept != null)
            {
                foreach (DocumentField d in dept.Value.AsList())
                {
                    if (d.FieldType == DocumentFieldType.String)
                    {
                        string jobTitle = d.Value.AsString();

                        fields.Add(new InvoiceField
                        {
                            Key = "Department",
                            Value = d.Content,
                            Confidence = d.Confidence
                        });
                    }
                }
            }

            if (_document.Fields.TryGetValue("CompanyNames", out var comp) && comp != null)
            {
                foreach (var c in comp.Value.AsList())
                {
                    fields.Add(new InvoiceField
                    {
                        Key = "Company Name",
                        Value = c.Content,
                        Confidence = c.Confidence
                    });
                }
            }
            return fields;
        }
    }
}
