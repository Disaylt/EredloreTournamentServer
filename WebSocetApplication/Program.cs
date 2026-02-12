var builder = WebApplication.CreateBuilder(args);

//builder.AddGameConfigs();
builder.AddServiceDefaults();
//builder.AddConfigurations();

//var databaseConfig = builder.Configuration.GetDatabaseConfig();
//var authConfig = builder.Configuration.GetAuthConfig();

builder.Services.AddControllers();
//builder.Services.AddOpenApi(opt =>
//{
//    opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
//});

//builder.Services
//    .AddOpenApi("v1")
//    .AddOpenApi("v2")
//    .AddProblemDetails()
//    .AddExceptionHandler<CoreRequestExceptionHandler>()
//    .AddEndpointsApiExplorer()
//    .AddAppVersioning()
//    .AddApplicationLayer()
//    .AddDatabase(databaseConfig)
//    .AddWebApiAuth(authConfig)
//    .AddAuthorization()
//    .AddInfrastructureServices()
//    .AddHttpContextAccessor();

var app = builder.Build();

//await app.RunMigration();

//app.UseExceptionHandler();
app.MapDefaultEndpoints();

//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.MapScalarApiReference(opt =>
//    {
//        opt.AddPreferredSecuritySchemes("Bearer");
//    });
//}

app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
