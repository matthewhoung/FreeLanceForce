namespace Domain.Forms
{
    public class SerialNumber
    {
        //追加:add,追減:sub
        public string SerialNumberGenerator(int formId, string stage, int count)
        {
            var datePart = DateTime.UtcNow.ToString("MMddyyyy");
            var partialOfStage = stage.Substring(0, 1);
            var currentSerialCount = count + 1;
            
            return $"{partialOfStage}{datePart}-{currentSerialCount}";
        }
    }
}
