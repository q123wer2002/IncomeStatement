using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IncomeStatement
{
	public class Startup
	{
		public Startup( IConfiguration configuration )
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration
		{
			get;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices( IServiceCollection services )
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure( IApplicationBuilder app, IHostingEnvironment env )
		{

			app.Use(async ( context, next ) => {
				await next();

				if(
					context.Response.StatusCode == 404 &&
					!System.IO.Path.HasExtension(context.Request.Path.Value) &&
					!context.Request.Path.Value.StartsWith("/api")
				) {
					context.Request.Path = "/index.html";
					context.Response.StatusCode = 200;
					await next();
				}
			});

			if( env.IsDevelopment() ) {
				app.UseDeveloperExceptionPage();
			}
			else {
				app.UseHsts();
			}

			// load vue
			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
