using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjetoMVCDB.Models
{
    // praticar o mecanismo de herança com a superclasse
    // identityDbContext dessa classe é a dependencia
    // Microsoft.AspNetCore.Identity.EntityFrameworkCore

    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        // é necessário que etá tenha de forma explicita seu construtor
        // devidamente referenciado - porque, quando essa classe for
        // chamada a execução é preciso "priorizar" A referencia do
        // contexto do banco de dados que será criado/manipulado
        // pela aplicação
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }
    }
}
