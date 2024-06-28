namespace Domain.Forms
{
    public class SerialNumber
    {
        //追加:add,追減:sub
        public string SerialNumberGenerator(int formId, string stage, int? count, string? attachType)
        {
            var datePart = DateTime.UtcNow.ToString("MMddyyyy");
            var partialOfStage = stage.Substring(0, 1);
            var currentSerialCount = count.HasValue ? count.Value + 1 : (int?)null;
            string? attachTypePart = null;

            if (attachType == "add" || attachType == "sub")
            {
                attachTypePart = attachType == "add" ? "A" : "B";
            }

            switch (attachType, count)
            {
                case (null, null):
                    return $"{datePart}-{partialOfStage}";
                case (null, not null):
                    return $"{datePart}-{partialOfStage}-{currentSerialCount}";
                case (not null, null):
                    return $"{datePart}-{partialOfStage}-{attachTypePart}";
                case (not null, not null):
                    return $"{datePart}-{partialOfStage}-{attachTypePart}-{currentSerialCount}";
                default:
                    throw new ArgumentException("Invalid combination of attachType and count");
            }
        }
    }
}
