using Domain.Forms.enums;

namespace Domain.Forms
{
    public class Form
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public Stages Stage { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        // Navigation property
        public ICollection<OrderForm> OrderForms { get; private set; } = new List<OrderForm>();

        // EF Core uses this constructor
        private Form()
        {
        }

        // Public constructor for creating new forms
        public Form(int productId, string? stage)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("ProductId cannot be less than or equal to zero.");
            }

            ProductId = productId;
            Stage = string.IsNullOrWhiteSpace(stage) ? 
                Stages.OrderForm : Stages.FromName(stage);
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void UpdateStage(string stage)
        {
            Stage = string.IsNullOrWhiteSpace(stage) ? 
                Stages.OrderForm : Stages.FromName(stage);
            UpdateAt = DateTime.UtcNow;
        }
    }
}
