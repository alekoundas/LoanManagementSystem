namespace Core.DTOs
{
    public class LoanApplicationDto
    {
        public int ApplicationID { get; set; }
        public int CustomerID { get; set; }
        public int LoanProductID { get; set; }
        public decimal RequestedAmount { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string ApplicationStatus { get; set; }
        public decimal? ApprovedAmount { get; set; }
    }
}
