using Domain.Enums;

namespace Application.Forms.DTOs
{
    public class FormDetailDto
    {
        public int FormId { get; set; }
        public int ProductId { get; set; }
        public int ProcurementId { get; set; }
        public string SerialNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Stages Stage { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
