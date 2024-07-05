namespace Domain.Entities
{
    public class OrderFormSignature : Signature
    {
        private OrderFormSignature() { }

        public OrderFormSignature(int formId, int userId, string role, string memo)
            : base(formId, userId, role, memo)
        {
        }
    }

    public class AcceptanceFormSignature : Signature
    {
        private AcceptanceFormSignature() { }

        public AcceptanceFormSignature(int formId, int userId, string role, string memo)
            : base(formId, userId, role, memo)
        {
        }
    }

    public class PaymentFormSignature : Signature
    {
        private PaymentFormSignature() { }

        public PaymentFormSignature(int formId, int userId, string role, string memo)
            : base(formId, userId, role, memo)
        {
        }
    }
}
