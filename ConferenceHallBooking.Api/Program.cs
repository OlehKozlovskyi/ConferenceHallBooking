using ConferenceHallBooking.Api.Extensions;
using ConferenceHallBooking.Api.Filters;
using ConferenceHallBooking.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddInfrastructure(configuration);

services.AddAutomapper();

services.AddValidators();

services.AddSwagger();

services.AddCustomServices();

services.AddControllers(options =>
{
    options.Filters.Add<AutoValidationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Conference Hall Booking API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();
