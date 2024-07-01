namespace Domain.Forms
{
    public class SerialNumber
    {
        //追加:add,追減:sub
        public string SerialNumberGenerator(int formId, string stage, int count, bool? isAttachForm)
        {
            var datePart = DateTime.UtcNow.ToString("MMddyyyy");
            var partialOfStage = stage.Substring(0, 1);
            var currentSerialCount = count + 1;
            string? attachTypePart = null;

            if (isAttachForm.HasValue)
            {
                attachTypePart = isAttachForm.Value ? "A" : "B";
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
