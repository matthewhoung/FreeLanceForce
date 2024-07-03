namespace Domain.Enums
{
    public class Stages : Enumeration<Stages>
    {
        public static readonly Stages OrderForm = new(1, "OrderForm");
        public static readonly Stages AcceptanceForm = new(2, "AcceptanceForm");
        public static readonly Stages PaymentForm = new(3, "PaymentForm");

        private Stages(int value, string name)
            : base(value, name)
        {
        }

    }
}
