using Microsoft.Extensions.Hosting;
using Scalar.Aspire;

var builder = DistributedApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    var postgres = builder
        .AddPostgres("postgres")
        .WithDataVolume()
        .WithPgAdmin();

    var postgresdb = postgres.AddDatabase("datingPostgresqlDb");

    var datingWebApi = builder.AddProject<Projects.WebApi>("webapi")
        .WaitFor(postgresdb)
        .WithReference(postgresdb);

    builder.AddScalarApiReference()
        .WithApiReference(datingWebApi);
}
else
{
    builder.AddProject<Projects.WebApi>("webapi");
}

builder.Build().Run();
