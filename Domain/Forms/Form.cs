namespace Domain.Forms
{
    public class Form
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public FormStage Stage { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        // Navigation property
        public ICollection<OrderForm> OrderForms { get; private set; } = new List<OrderForm>();

        // EF Core uses this constructor
        private Form()
        {
        }

        // Public constructor for creating new forms
        public Form(int? productId, FormStage? stage)
        {
            if (!productId.HasValue || productId.Value <= 0)
            {
                throw new ArgumentException("ProductId cannot be less than or equal to zero.");
            }

            ProductId = productId.Value;
            Stage = stage ?? FormStage.OrderForm;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void UpdateStage(FormStage stage)
        {
            Stage = stage;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
