namespace Domain.Forms
{
    public class Form
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public FormStage Stage { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        private Form() 
        {
        }

        public Form(int productId, string title, string description,FormStage? stage)
        {
            ProductId = productId;
            Title = title;
            Description = description;
            Stage = stage?? FormStage.OrderForm;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void UpdateTitle(string title)
        {
            Title = title;
            UpdateAt = DateTime.UtcNow;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
            UpdateAt = DateTime.UtcNow;
        }

        public void UpdateStage(FormStage stage) 
        {
            Stage = stage;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
