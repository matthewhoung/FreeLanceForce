namespace Domain.Forms.Enums
{
    public class Roles : Enumeration<Roles>
    {
        public static readonly Roles Admin = new(1, "Admin");
        public static readonly Roles Creator = new(2, "Creator");
        public static readonly Roles Acceptor = new(3, "Acceptor");
        public static readonly Roles Associate = new(4, "Associate");
        public static readonly Roles Substitute = new(5, "Substitute");
        public static readonly Roles Accountant = new(6, "Accountant");
        public static readonly Roles Auditor = new(7, "Auditor");
        public static readonly Roles Manager = new(8, "Manager");
        public static readonly Roles Director = new(9, "Director");

        private Roles(int value, string name)
            : base(value, name)
        {
        }
    }
}
