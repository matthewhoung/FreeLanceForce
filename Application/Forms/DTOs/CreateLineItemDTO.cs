namespace Application.Forms.DTOs
{
    public class CreateLineItemDTO
    {
        public int FormId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
