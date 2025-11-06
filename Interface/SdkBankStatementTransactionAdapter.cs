using Azure.AI.DocumentIntelligence;
using FileUploadReader.ViewModels;

namespace FileUploadReader.Interface
{
    public class SdkBankStatementTransactionAdapter
    {
        private readonly AnalyzedDocument _document;

        public SdkBankStatementTransactionAdapter(AnalyzedDocument document)
        {
            _document = document;
        }

        public List<BankTransaction> ExtractTransactions()
        {
            var transactionsList = new List<BankTransaction>();

            if (_document.Fields.TryGetValue("Accounts", out var accountsField) &&
                accountsField.ValueList != null)
            {
                foreach (var accountField in accountsField.ValueList)
                {
                    var accountDict = accountField.ValueDictionary;

                    string accountNumber = accountDict.TryGetValue("AccountNumber", out var accNoField)
                        ? accNoField?.ValueString ?? "Unknown"
                        : "Unknown";

                    string biginningBalance = accountDict.TryGetValue("BeginningBalance", out var beginningBalanceField)
                        ? beginningBalanceField?.ValueString ?? "0.00"
                        : "0.00";

                    if (accountDict.TryGetValue("Transactions", out var transactionsField) &&
                        transactionsField.ValueList != null)
                    {
                        foreach (var txField in transactionsField.ValueList)
                        {
                            var txDict = txField.ValueDictionary;

                            txDict.TryGetValue("Description", out var descField);
                            txDict.TryGetValue("Date", out var dateField);
                            txDict.TryGetValue("DepositAmount", out var depositField);
                            txDict.TryGetValue("CheckNumber", out var checkField);
                            txDict.TryGetValue("WithdrawalAmount", out var withdrawalField);

                            transactionsList.Add(new BankTransaction
                            {
                                AccountNumber = accountNumber,
                                Date = dateField?.ValueDate,
                                Description = descField?.ValueString,
                                CheckNumber = checkField?.ValueString,
                                DepositAmount = depositField != null ? (decimal?)depositField.ValueDouble : null,
                                WithdrawalAmount = withdrawalField != null ? (decimal?)withdrawalField.ValueDouble : null,
                                Confidence = descField?.Confidence ?? 0
                            });
                        }
                    }
                }
            }

            return transactionsList;
        }
    }



}
