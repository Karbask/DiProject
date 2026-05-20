var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Aquí registramos los servicios con distintos periodos de vida. 
//Puedes cambiar entre AddSingleton, AddScoped y AddTransient para ver cómo afecta la creación de instancias del servicio Myservice.

builder.Services.AddSingleton<IMyService, Myservice>();
//builder.Services.AddScoped<IMyService, Myservice>();
//builder.Services.AddTransient<IMyService, Myservice>();

var app = builder.Build();

app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();//Obtenemos una instancia del servicio Myservice en el middleware.
    myService.logCreation("Middleware accessed. First middleware.");
    await next.Invoke();
});

app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>(); //Obtenemos otra instancia del servicio Myservice en el segundo middleware.
    myService.logCreation("Middleware accessed. Second middleware.");
    await next.Invoke();
});

app.MapGet("/", (IMyService myService) =>
{
    myService.logCreation("Root endpoint accessed");//Obtenemos una instancia del servicio Myservice directamente en el endpoint.
    return Results.Ok("Check the console for the service creation log."); 
} );

app.Run();


