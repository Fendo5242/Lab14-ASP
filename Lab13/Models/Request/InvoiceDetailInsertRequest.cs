namespace Lab13.Models.Request
{
    public class DetailInsert
    {
        public int ProductID { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
    }

    public class InvoiceDetailInsertRequest
    {
        public int InvoiceId { get; set; }
        public List<DetailInsert> Details { get; set; }
    }
}
