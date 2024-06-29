namespace Lab13.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }

        public bool IsCustomer {  get; set; }
    }
}
