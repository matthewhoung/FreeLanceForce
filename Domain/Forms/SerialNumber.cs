namespace Domain.Forms
{
    public class SerialNumber
    {
        //追加:add,追減:sub
        public string SerialNumberGenerator(int formId, string stage, int count, string? isAttachForm)
        {
            var datePart = DateTime.UtcNow.ToString("MMddyyyy");
            var partialOfStage = stage.Substring(0, 1);
            var currentSerialCount = count + 1;
            string? attachTypePart = null;

            if (!string.IsNullOrEmpty(isAttachForm) && 
                isAttachForm != "A" && 
                isAttachForm != "B")
            {
                throw new ArgumentException("Invalid value for isAttachForm. Allowed values are 'A', 'B', or null.");
            }

            if (isAttachForm != null)
            {
                attachTypePart = isAttachForm;
            }

            switch (isAttachForm)
            {
                case null:
                    return $"{datePart}-{partialOfStage}-{currentSerialCount}";
                default:
                    return $"{datePart}-{partialOfStage}-{currentSerialCount}-{attachTypePart}";
            }
        }
    }
}
