using Microsoft.EntityFrameworkCore;
using pruebaApiC_.Modelos;

namespace pruebaApiC_.Context
{
    public class ClienteContext: DbContext
    {
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options) 
        {

        } 
        public DbSet<Cliente> Clientes { get; set; }
    }
}
