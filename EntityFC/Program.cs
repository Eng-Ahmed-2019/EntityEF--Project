using Dapper;
using System.Data;
using EntityFC.Data;
using EntityFC.Entities;
using EntityFC.Services;
using EntityFC.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var connectionString =
                    context.Configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException(
                        "DefaultConnection was not found in appsettings.json");
                }

                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                    /*
                    options.UseSqlServer(connectionString, o =>
                    {
                        o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
                    */
                });

                services.AddScoped<IEmployee, EmployeeServices>();
            })
            .Build();

        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        string connection =
                "Server=(localdb)\\MSSQLLocalDB;Database=CompanyDb;Trusted_Connection=True;" +
                "TrustServerCertificate=True;MultipleActiveResultSets=True;";
        using var conn = new SqlConnection(connection);

        /*
        using var scopeA = host.Services.CreateScope();
        using var scopeB = host.Services.CreateScope();

        var contextA = scopeA.ServiceProvider.GetRequiredService<AppDbContext>();
        var contextB = scopeB.ServiceProvider.GetRequiredService<AppDbContext>();

        var pA = contextA.Products.Find(1);
        if (pA is null) return;

        var pB = contextB.Products.Find(1);
        if (pB is null) return;

        pB.Quantity += 5;
        await contextB.SaveChangesAsync();
        Console.WriteLine("User B Updated Successfully");

        pA.Quantity += 2;
        */

        try
        {
            var context = services.GetRequiredService<AppDbContext>();


        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static async Task TransferMoney(AppDbContext context)
    {
        using var trans = await context.Database.BeginTransactionAsync();

        try
        {
            var ahmed = await context.Users.FindAsync(1);
            var mohamed = await context.Users.FindAsync(2);

            if (ahmed is null || mohamed is null)
                throw new Exception("User not found");

            decimal amount = 100m;

            if (amount <= 0 || ahmed.Balance < amount)
                throw new Exception("Invalid amount or insufficient balance");

            ahmed.Balance -= amount;
            await context.SaveChangesAsync();

            //throw new Exception("Something went wrong!");

            mohamed.Balance += amount;
            await context.SaveChangesAsync();

            await trans.CommitAsync();
        }
        catch
        {
            await trans.RollbackAsync();
            Console.WriteLine("Transaction Failed! All Changes Rolled Back.");
            throw;
        }
    }
}