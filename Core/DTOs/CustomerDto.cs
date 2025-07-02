namespace Core.DTOs
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int? CreditScore { get; set; }
    }
}
