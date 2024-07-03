namespace Domain.Interfaces
{
    public interface ISerialNumberRepository
    {
        Task<string> GenerateSerialNumberAsync(int formId, string stage, bool? isAttach);
    }
}
