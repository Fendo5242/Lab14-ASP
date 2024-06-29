namespace Lab13.Models.Request
{
    public class InvoiceInsertRequest
    {
        public int CustomerId { get; set; }
        public List<InvoiceDetail> Invoices { get; set; }
    }

    public class InvoiceDetail
    {
        public DateTime Date { get; set; }
        public string InvoiceNumber { get; set; }
        public float Total { get; set; }
    }
}
