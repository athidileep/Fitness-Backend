using Amazon;
using Amazon.SecretsManager;
using Domain;
using Persistance;
using Services;
using Services.Abstraction;
using Services.Abstraction.Lookup;
using Services.Helpers;
using Services.Lookup;

namespace ApplicationService;

public class Startup
{
    private readonly IWebHostEnvironment _env;
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        _env = env;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public async void ConfigureServices(IServiceCollection services)
    {
        var configSettings = await GetConfigSettings();
        services.AddPersistence(configSettings.DbConnectionString);

        services.AddTransient<IUser, UserService>();
        services.AddTransient<ICommonServices, CommonService>();
        services.AddTransient<IActivityService, ActivityService>();
        services.AddTransient<IGoalSettingServices, GoalSettingServices>();
        services.AddTransient<IGoalTypeServicescs, GoalTypeServices>();

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ApplicationService", Version = "v1" });
            c.ResolveConflictingActions(apiDescription => apiDescription.First());
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseCors(x => x
           .WithMethods("GET", "POST", "DELETE", "PUT", "OPTIONS")
           .AllowAnyOrigin()
           //.AllowAnyMethod()
           .AllowAnyHeader());

        app.UseRouting();

        app.UseAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("../swagger/v1/swagger.json", "Application MicroService");
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

        });
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    private async Task<ConfigSettings> GetConfigSettings()
    {
        return await GetSecretsAndAmendConfig();
    }
    private static Task<ConfigSettings> GetSecretsAndAmendConfig()
    {
        var secretId = Environment.GetEnvironmentVariable(Domain.Constants.ENVIRONMENT_VARIABLES_SECRET_KEY); 

        if (string.IsNullOrEmpty(secretId))
        {
            throw new ArgumentNullException(secretId);
        } 

        var awsSecretManager = new AwsSecretManager(new AmazonSecretsManagerClient(RegionEndpoint.USEast1)); 

        var secrets = awsSecretManager.GetSecrets(secretId).Result;  

        var postgresDatabaseConfig = string.Empty;

        if (secrets == null)
        {
            throw new ArgumentNullException();
        }
        else { postgresDatabaseConfig = $"User ID={Convert.ToString(secrets["Username"])};Password={Convert.ToString(secrets["Password"])};Host={Convert.ToString(secrets["Host"])};Port={Convert.ToString(secrets["Port"])};Database={Convert.ToString(secrets["DatabaseName"])};Pooling=true;"; }

        return Task.FromResult(new ConfigSettings()
        {
            DbConnectionString = postgresDatabaseConfig.ToString(), 
        });
    }
}
    public class ConfigSettings
    { 
        public string DbConnectionString { get; set; } 

    }