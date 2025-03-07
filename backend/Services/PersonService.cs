using Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;

namespace Services
{
    public class PersonService : IPersonContract
    {
        private readonly ApplicationDbContext _context;

        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            return await _context.People.ToListAsync();
        }
    }
}