using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.DataAccess;
using Microsoft.OpenApi.Models;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddHttpClient("CreditScoreService", client =>
{
    client.BaseAddress = new Uri("https://mock-credit-score-api.com");
});
builder.Services.AddScoped<ICustomerRepository>(sp => new CustomerRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ILoanRepository>(sp => new LoanRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ILoanApplicationRepository>(sp => new LoanApplicationRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPaymentRepository>(sp => new PaymentRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ILoanProductRepository>(sp => new LoanProductRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ILoanApplicationService, LoanApplicationService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ILoanProductService, LoanProductService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Loan Portfolio API", Version = "v1" });
});


var app = builder.Build();


app.UseCors("AllowAll");
// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();