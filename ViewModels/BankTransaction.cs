namespace FileUploadReader.ViewModels
{
    public class BankTransaction
    {
        public string AccountNumber { get; set; }
        public DateTimeOffset? Date { get; set; }
        public string Description { get; set; }
        public decimal? DepositAmount { get; set; }
        public decimal? WithdrawalAmount { get; set; }
        public float? Confidence { get; set; }
        public string CheckNumber { get; set; }
    }
}
