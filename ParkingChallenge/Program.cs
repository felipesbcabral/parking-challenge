using MongoDB.Driver;
using ParkingChallenge.Core.Domain.Interfaces.Repositories;
using ParkingChallenge.Core.Domain.Interfaces.Requests;
using ParkingChallenge.Core.Domain.UseCases;
using ParkingChallenge.Core.Domain.UseCases.CreateParking;
using ParkingChallenge.Core.Domain.UseCases.GetParking;
using ParkingChallenge.Core.Domain.UseCases.UpdateParking;
using ParkingChallenge.Core.Infra;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

//builder.Services.Configure<ParkingDatabaseConfiguration>(
//    builder.Configuration.GetSection("ConnectionStrings"));

var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:ConnectionString");
var databaseName = builder.Configuration.GetValue<string>("ConnectionStrings:DatabaseName");
var mongoClient = new MongoClient(connectionString);
var mongoDatabase = mongoClient.GetDatabase(databaseName);
builder.Services.AddScoped<IParkingRepository, ParkingRepository>(x => new ParkingRepository(mongoDatabase, "ParkingLot"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<ParkingContext>();
builder.Services.AddControllers();
builder.Services.AddScoped<IRequestHandler<GetParkingInput, ResponseUseCase>, GetParkingUseCase>();
builder.Services.AddScoped<IRequestHandler<CreateParkingInput, ResponseUseCase>, CreateParkingUseCase>();
builder.Services.AddScoped<IRequestHandler<UpdateParkingInput, ResponseUseCase>, UpdateParkingUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
