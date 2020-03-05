using CoreApi.Interfaces;
using CoreApi.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// CORS with "AllowAll" policy so we can hit it from different domains
			services.AddCors(options =>
			{
				options.AddPolicy(
					"AllowAll",
					p => p.AllowAnyOrigin()
						.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowCredentials()
				);
			});
			services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));
			});

			// Add caching (in-memory and response)
			services.AddMemoryCache();
			services.AddResponseCaching();

			services.AddMvc(options =>
				{
					// Setup a 30 second cache profile
					options.CacheProfiles.Add("Cache30Seconds",
						new CacheProfile()
						{
							Duration = 30,
							Location = ResponseCacheLocation.Any
						}
					);
				}
			).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddScoped<IHackerNewsRequestManager, HackerNewsRequestManager>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseResponseCaching();
			app.UseHttpsRedirection();
			app.UseMvc();
			app.UseCors("AllowAll");
		}
	}
}
