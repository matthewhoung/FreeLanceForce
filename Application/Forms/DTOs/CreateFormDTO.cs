using Domain.Entities;

namespace Application.Forms.DTOs
{
    public class CreateFormDTO
    {
        public int? FormId { get; set; }
        public int ProjectId { get; set; }
        public string? Stage { get; set; }
        public string? Status { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool? IsAttach { get; set; }
        public IEnumerable<CreateSignatureMemberDTO> SignatureMembers { get; set; }
        public IEnumerable<LineItem> LineItems { get; set; }
    }
}
