namespace Core.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int LoanID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentType { get; set; }
        public int? RecordedByEmployeeID { get; set; }
    }
}
