namespace Domain.Forms
{
    public class OrderForm
    {
        public Form Form { get; private set; }
        public int ProcurementId { get; private set; }
        public int FormId { get; private set; }
        public FormStatus Status { get; private set; }
        public SerialNumber SerialNumber { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        private OrderForm()
        {
        }

        public OrderForm(int procurementId, int formId, FormStatus? status, SerialNumber serialNumber)
        {
            ProcurementId = procurementId;
            FormId = formId;
            Status = status ?? FormStatus.Pending;
            SerialNumber = serialNumber;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void UpdateStatus(FormStatus status)
        {
            Status = status;
        }
    }
}
