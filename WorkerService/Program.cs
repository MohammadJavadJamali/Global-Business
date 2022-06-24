using Application;
using WorkerService;
using Application.Repository;
using Microsoft.EntityFrameworkCore;

IHost builder = Host.CreateDefaultBuilder(args)
    .UseWindowsService()
    .ConfigureServices((hostContext, services)=>
    {
        services.AddQuatzServices(hostContext.Configuration);

        services.AddDbContext<DataContext>(options => options
                .UseSqlServer(hostContext.Configuration.GetConnectionString("DefualtConnection")));

        services.AddScoped<ISave, Save>();
        services.AddScoped<INode, NodeService>();
        services.AddScoped<IUser, UserService>();
        services.AddScoped<IProfit, ProfitService>();
        services.AddScoped<ITransaction, TransactionService>();
        services.AddScoped<IUserFinancial, UserFinancialService>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IFinancialPackage, FinancialPackageService>();
    })
    .Build();

await builder.RunAsync();
