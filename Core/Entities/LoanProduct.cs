namespace Core.Entities
{
    public class LoanProduct
    {
        public int LoanProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MaxLoanAmount { get; set; }
        public int MinCreditScore { get; set; }
        public int MinTermMonths { get; set; }
        public int MaxTermMonths { get; set; }
        public bool IsActive { get; set; }
    }
}
