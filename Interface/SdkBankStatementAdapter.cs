//using Azure.AI.FormRecognizer.DocumentAnalysis;

using Azure.AI.DocumentIntelligence;

using FileUploadReader.ViewModels;

namespace FileUploadReader.Interface
{
    public class SdkBankStatementAdapter : IInvoiceAdapter
    {
        private readonly AnalyzedDocument _document;

        public SdkBankStatementAdapter(AnalyzedDocument document)
        {
            _document = document;
        }

        public List<InvoiceField> ExtractFields()
        {
            var fields = new List<InvoiceField>();

            if (_document.Fields.TryGetValue("AccountHolderName", out var accountName) &&
                accountName != null)
            {
                fields.Add(new InvoiceField
                {
                    Key = "Account Holder Name",
                    Value = accountName.Content,
                    Confidence = accountName.Confidence
                });
            }

            if (_document.Fields.TryGetValue("AccountHolderAddress", out var accAdd) &&
               accAdd != null)
            {
                fields.Add(new InvoiceField
                {
                    Key = "Account Holder Address",
                    Value = accAdd.Content,
                    Confidence = accAdd.Confidence
                });
            }

            if (_document.Fields.TryGetValue("BankName", out var bankName) &&
                       bankName != null)
            {
                fields.Add(new InvoiceField
                {
                    Key = "Bank Name",
                    Value = bankName.Content,
                    Confidence = bankName.Confidence
                });
            }

            if (_document.Fields.TryGetValue("BankAddress", out var bankAdd) &&
                  bankAdd != null)
            {
                fields.Add(new InvoiceField
                {
                    Key = "Bank Address",
                    Value = bankAdd.Content,
                    Confidence = bankAdd.Confidence
                });
            }

            if (_document.Fields.TryGetValue("StatementStartDate", out var start) &&
                  start != null)
            {
                fields.Add(new InvoiceField
                {
                    Key = "Statement Start Date",
                    Value = start.Content,
                    Confidence = start.Confidence
                });
            }

            if (_document.Fields.TryGetValue("StatementEndDate", out var end) &&
                 end != null)
            {
                fields.Add(new InvoiceField
                {
                    Key = "Statement End Date",
                    Value = end.Content,
                    Confidence = end.Confidence
                });
            }

            if (_document.Fields.TryGetValue("Accounts", out var accountsField) &&
                accountsField != null && accountsField.ValueList.Count() > 0)
            {
                foreach (var accountField in accountsField.ValueList)
                {
                    var accountDict = accountField.ValueDictionary;

                    string accountNumber = accountDict.TryGetValue("AccountNumber", out var accNoField)
                        ? accNoField?.ValueString ?? "Unknown"
                        : "Unknown";

                    fields.Add(new InvoiceField
                    {
                        Key = "Account Number",
                        Value = accNoField?.ValueString,
                        Confidence = accNoField?.Confidence
                    });

                    double biginningBalance = accountDict.TryGetValue("BeginningBalance", out var beginningBalanceField)
                        ? beginningBalanceField?.ValueDouble ?? 0
                        : 0;

                    fields.Add(new InvoiceField
                    {
                        Key = "Beginning Balance",
                        Value = beginningBalanceField?.ValueDouble.ToString(),
                        Confidence = beginningBalanceField?.Confidence
                    });

                    double endingBalance = accountDict.TryGetValue("EndingBalance", out var endingBalanceField)
                      ? endingBalanceField?.ValueDouble ?? 0
                      : 0;

                    fields.Add(new InvoiceField
                    {
                        Key = "Ending Balance",
                        Value = endingBalanceField?.ValueDouble.ToString(),
                        Confidence = endingBalanceField?.Confidence
                    });
                }

            }



            return fields;
        }
    }
}
