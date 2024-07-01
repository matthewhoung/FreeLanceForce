namespace Domain.Forms.Enums
{
    public class Status : Enumeration<Status>
    {
        public static readonly Status Pending = new(1, "Pending");
        public static readonly Status Inprogress = new(2, "Inprogress");
        public static readonly Status Finished = new(3, "Finished");
        public static readonly Status Archived = new(4, "Archived");

        private Status(int value, string name)
            : base(value, name)
        {
        }
    }
}
