namespace Domain.Forms
{
    public class OrderForm
    {
        public Form Form { get; private set; }
        public int ProcurementId { get; private set; }
        public int FormId { get; private set; }
        public string SerialNumber { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public FormStatus Status { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        private OrderForm()
        {
        }

        public OrderForm(int procurementId, int formId, FormStatus? status, string serialNumber, string? title, string? description)
        {
            ProcurementId = procurementId;
            FormId = formId;
            Status = status ?? FormStatus.Pending;
            SerialNumber = serialNumber;
            Title = title ?? string.Empty;
            Description = description ?? string.Empty;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void UpdateStatus(FormStatus status)
        {
            Status = status;
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

    }
}
