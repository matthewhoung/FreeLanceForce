namespace Domain.Forms
{
    public class SerialNumber
    {
        //追加:add,追減:sub
        public string SerialNumberGenerator(int formId, string stage, int count, string? attachType)
        {
            var datePart = DateTime.UtcNow.ToString("MMddyyyy");
            var partialOfStage = stage.Substring(0, 1);
            var currentSerialCount = count + 1;
            string? attachTypePart = null;

            if (attachType == "add" || attachType == "sub")
            {
                attachTypePart = attachType == "add" ? "A" : "B";
            }

            switch (attachType)
            {
                case null:
                    return $"{datePart}-{partialOfStage}-{currentSerialCount}";
                case not null:
                    return $"{datePart}-{partialOfStage}-{currentSerialCount}-{attachTypePart}";
                default:
                    throw new ArgumentException("Invalid combination of attachType and count");
            }
        }
    }
}
