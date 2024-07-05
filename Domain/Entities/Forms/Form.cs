using Domain.Enums;

namespace Domain.Entities.Forms
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
        public ICollection<AcceptanceForm> AcceptanceForms { get; private set; } = new List<AcceptanceForm>();
        public ICollection<PaymentForm> PaymentForms { get; private set; } = new List<PaymentForm>();

        // EF Core uses this constructor
        protected Form()
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
