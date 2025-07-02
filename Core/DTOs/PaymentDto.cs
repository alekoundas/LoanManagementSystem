namespace Core.DTOs
{
    public class PaymentDto
    {
        public int PaymentID { get; set; }
        public int LoanID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentType { get; set; }
    }
}
