namespace Application.Forms.DTOs
{
    public class CreateFormDTO
    {
        public int ProjectId { get; set; }
        public string? Stage { get; set; } = null;
        public string? Status { get; set; } = null;
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool? IsAttatchForm { get; set; } = null;
    }
}
