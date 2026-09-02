using OrderProcessing.Api.Services;
var builder=WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); builder.Services.AddEndpointsApiExplorer(); builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IOrderRepository,InMemoryOrderRepository>(); builder.Services.AddScoped<IOrderPricingService,OrderPricingService>();
builder.Services.AddCors(o=>o.AddPolicy("Client",p=>p.WithOrigins(builder.Configuration["ClientUrl"] ?? "https://localhost:7102").AllowAnyHeader().AllowAnyMethod()));
var app=builder.Build(); if(app.Environment.IsDevelopment()){app.UseSwagger();app.UseSwaggerUI();} app.UseHttpsRedirection(); app.UseCors("Client"); app.MapControllers(); app.Run();
public partial class Program { }
