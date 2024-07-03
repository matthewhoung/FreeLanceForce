namespace Domain.Enums
{
    public class Roles : Enumeration<Roles>
    {
        public static readonly Roles Creator = new(1, "Creator");
        public static readonly Roles Acceptor = new(2, "Acceptor");
        public static readonly Roles Associate = new(3, "Associate");
        public static readonly Roles Substitute = new(4, "Substitute");
        public static readonly Roles Accountant = new(5, "Accountant");
        public static readonly Roles Auditor = new(6, "Auditor");
        public static readonly Roles Manager = new(7, "Manager");
        public static readonly Roles Director = new(8, "Director");

        private Roles(int value, string name)
            : base(value, name)
        {
        }
    }
}
