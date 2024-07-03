namespace Domain.ValueObjects
{
    public class SerialNumber
    {
        public string Generate(int formId, string stage, int count)
        {
            var datePart = DateTime.UtcNow.ToString("MMddyyyy");
            var partialOfStage = stage.Substring(0, 1);
            var currentSerialCount = count + 1;

            return $"{partialOfStage}{datePart}-{currentSerialCount}";
        }

        public string GenerateForAttachment(string serialNumber, bool isAttach)
        {
            return isAttach ? $"{serialNumber}-A" : $"{serialNumber}-B";
        }

        public string TransformForStage(string serialNumber, string fromStage, string toStage)
        {
            return serialNumber.Replace(fromStage.Substring(0, 1), toStage.Substring(0, 1));
        }
    }
}
