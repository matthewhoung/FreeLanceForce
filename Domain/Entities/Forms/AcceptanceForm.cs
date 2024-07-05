using Domain.Enums;

namespace Domain.Entities.Forms
{

    public class AcceptanceForm
    {
        public Form Form { get; private set; }
        public int ProcurementId { get; private set; }
        public int FormId { get; private set; }
        public string SerialNumber { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public Status Status { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        private AcceptanceForm()
        {
        }

        public AcceptanceForm(int formId, Status status, string serialNumber, string title, string description)
        {
            FormId = formId;
            Status = status;
            SerialNumber = serialNumber;
            Title = title;
            Description = description;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void UpdateStatus(Status status)
        {
            Status = status;
            UpdateAt = DateTime.UtcNow;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
