using Domain.Entities;

namespace Services.Contracts
{
    public interface IPersonContract
    {
        Task<IEnumerable<Person>> GetPeopleAsync();
    }
}