namespace Core.DTOs
{
    public class LoanProductDto
    {
        public int LoanProductID { get; set; }
        public string ProductName { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MaxLoanAmount { get; set; }
        public int MinCreditScore { get; set; }
        public bool IsActive { get; set; }
    }
}
