namespace Application.Forms.DTOs
{
    public class SignatureMemberDTO
    {
        public int FormId { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
        public string? Memo { get; set; }
    }
}
