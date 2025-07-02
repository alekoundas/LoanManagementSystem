namespace Core.Entities
{
    public class LoanApplication
    {
        public int ApplicationID { get; set; }
        public int CustomerID { get; set; }
        public int LoanProductID { get; set; }
        public decimal RequestedAmount { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string ApplicationStatus { get; set; }
        public int? AssignedEmployeeID { get; set; }
        public DateTime? DecisionDate { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public string RejectionReason { get; set; }
    }
}
