
using StoreApi.Filters;
using StoreApi.Middlewares;
using StoreApi.Sevcies;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


//builder.Services.AddControllers( options =>
//{
//    options.Filters.Add<StatsFilter>();
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Always register the TimeService, regardless of the environment
builder.Services.AddScoped<TimeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseMiddleware<StatsMiddleware>();

//app.Use((context, next) =>
//{
//    //handle the request ( before executing the controller)
//    DateTime requestTime = DateTime.Now;
//    var result =  next(context);
//    //handle the response (after the executing the controller)
//    DateTime responseTime = DateTime.Now;
//    TimeSpan processDuration = responseTime - requestTime;
//    Console.WriteLine("[Inline MiddleWare] Process Duration=" + processDuration.TotalMilliseconds + "ms");
//    return result;
//});



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
