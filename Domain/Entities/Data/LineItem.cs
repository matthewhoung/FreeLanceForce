namespace Domain.Entities
{
    public class LineItem
    {
        public int LineItemId { get; private set; }
        public int FormId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public decimal Total { get; private set; }
        public bool IsApproved { get; private set; }
        public DateTime? ApprovedAt { get; private set; }
        public bool IsRejected { get; private set; }
        public DateTime? RejectedAt { get; private set; }

        private LineItem() { }

        public LineItem(int formId, string title, string description, decimal price, int quantity)
        {
            FormId = formId;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description;
            Price = price;
            Quantity = quantity;
            Total = price * quantity;
        }

        public void Approve(DateTime approvedAt)
        {
            IsApproved = true;
            ApprovedAt = approvedAt;
            IsRejected = false;
            RejectedAt = null;
        }

        public void Reject(DateTime rejectedAt)
        {
            IsApproved = false;
            IsRejected = true;
            RejectedAt = rejectedAt;
        }

        public void UpdateDetails(string title, string description, decimal price, int quantity)
        {
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description;
            Price = price;
            Quantity = quantity;
        }
    }
}
