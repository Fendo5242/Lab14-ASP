namespace Lab13.Models.Request
{
    public class InvoiceRequestInsert
    {
        public int IdCustomer { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceNumber { get; set; }
        public float Total { get; set; }
    }
}
