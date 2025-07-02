namespace Core.Entities
{
    public class Loan
    {
        public int LoanID { get; set; }
        public int ApplicationID { get; set; }
        public int CustomerID { get; set; }
        public int LoanProductID { get; set; }
        public decimal ApprovedAmount { get; set; }
        public DateTime DisbursementDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal InterestRate { get; set; }
        public decimal CurrentBalance { get; set; }
        public int OriginalTermMonths { get; set; }
        public string LoanStatus { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public DateTime? NextPaymentDueDate { get; set; }
    }
}
