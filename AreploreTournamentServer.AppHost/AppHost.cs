using Microsoft.Extensions.Hosting;
using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    var postgres = builder
        .AddPostgres("postgres")
        .WithDataVolume()
        .WithPgAdmin();

    var redis = builder
        .AddRedis("redis")
        .WithDataVolume();

    var postgresdb = postgres.AddDatabase("gamePostgresqlDb");

    var configPathSettings = builder.AddParameter("ApplicationConfigsPath", secret: true);

    var webApi = builder
        .AddProject<Projects.WebApi>("webapi")
        .WaitFor(postgresdb)
        .WithReference(postgresdb)
        .WithReference(redis)
        .WithEnvironment("ApplicationConfigsPath", configPathSettings);

    builder.AddScalarApiReference()
        .WithApiReference(webApi);
}
else
{
    builder.AddProject<Projects.WebApi>("webapi");
}

builder.Build().Run();
