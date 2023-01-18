// Comando para usar no terminal do gerenciador de pacotes nuget pelo console de gerenciamento de pacotes
// comando : dotnet tool install --global dotnet-ef
// comando : dir - listar todos os nossos arquivos para verificar se esta tudo correto 

// Caso for usar em outra maquina para gerar o database !
//-------------------------------------------------------------------------------
// comando : remove-migration
// * se nao fuioncionar o remove migration , colocar update-database -migration:0
// comando : remove-migration
//-------------------------------------------------------------------------------

// comando : Add-Migration MyCommand1 - Ajuda a criar nossa Base Buildar
// comando : Update-Database - Atualiza��o da base de dados

using ProjetoMVCDB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//adicionar o servi�o que "aciona" a string de conex�o configurada
// em appsetting.json
builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

// adicionar o conjunto "regras" devidamente indicado para  - postereiormente se possivel a total manipula��o da base
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

// Aqui � necess�rio indicar o m�todo de autentica��o de Acesso
// para �reas restritas do web App
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
