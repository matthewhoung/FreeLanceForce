namespace Domain.DTOs
{
    public class SignatureDto
    {
        public int FormId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public string? Memo { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public bool? IsRejected { get; set; }
        public DateTime? RejectedAt { get; set; }
    }

}
