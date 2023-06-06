using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using TerminalApi.Models;

namespace TerminalApi
{
    public class PersonDbContext: DbContext
    {
        public DbSet<Person> People { get; set; }
        public PersonDbContext(DbContextOptions<PersonDbContext> options): base(options)
        {
            if (Database.GetService<IDatabaseCreator>() is not RelationalDatabaseCreator dataBase) return;
            
            if (!dataBase.CanConnect()) dataBase.Create();

            if (!dataBase.HasTables()) dataBase.CreateTables();
        }
    }
}
