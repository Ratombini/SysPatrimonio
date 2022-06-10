using Microsoft.EntityFrameworkCore;

namespace SysPatrimonio.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> opcoes) : base(opcoes) { }

        public DbSet<DbCategoria> categorias { get; set; }
        public DbSet<DbDepartamento> departamentos { get; set; }
        public DbSet<DbFornecedor> fornecedores { get; set; }
        public DbSet<DbLocal> locais { get; set; }
        public DbSet<DbPatrimonio> patrimonios { get; set; }
        public DbSet<DbUsuario> usuarios { get; set; }

        
    }
}
