namespace Domain.ValueObjects
{
    public class LineItemDetail
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public decimal Total => Price * Quantity;
        public bool IsApproved { get; private set; }
        public DateTime? ApprovedAt { get; private set; }
        public bool IsRejected { get; private set; }
        public DateTime? RejectedAt { get; private set; }

        public LineItemDetail(
            string title,
            string description,
            decimal price,
            int quantity)
        {
            if (string.IsNullOrWhiteSpace(title)) 
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            if (price < 0) 
                throw new ArgumentException("Price cannot be negative.", nameof(price));
            if (quantity < 0) 
                throw new ArgumentException("Quantity cannot be negative.", nameof(quantity));

            Title = title;
            Description = description;
            Price = price;
            Quantity = quantity;
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
    }
}
