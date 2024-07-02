namespace Domain.Interfaces
{
    public interface ISerialNumberRepository
    {
        Task<string> SerialNumberAsync(int formId, string stage, bool? isAttach);
    }
}
