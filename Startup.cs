using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectManagementSystem.Data;
using ProjectManagementSystem.Features.Deliverables;
using ProjectManagementSystem.Features.Issues;
using ProjectManagementSystem.Features.Resources;
using ProjectManagementSystem.Features.Skills;
using ProjectManagementSystem.Features.Tasks;
using ProjectManagementSystem.Features.Requirements;
using ProjectManagementSystem.Features.ActionItems;
using ProjectManagementSystem.Features.Decisions;

namespace ProjectManagementSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {   
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
                        
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = false; // optional
            })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            
            services.AddTransient(typeof(ResourceService), typeof(ResourceService));
            services.AddTransient(typeof(TaskService), typeof(TaskService));
            services.AddTransient(typeof(IssueService), typeof(IssueService));
            services.AddTransient(typeof(DeliverableService), typeof(DeliverableService));
            services.AddTransient(typeof(SkillService), typeof(SkillService));
            services.AddTransient(typeof(ActionItemService), typeof(ActionItemService));
            services.AddTransient(typeof(DecisionService), typeof(DecisionService));

            services.AddTransient(typeof(RequirementService), typeof(RequirementService));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {   
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();                
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.ApplicationServices
             .UseBootstrapProviders()
             .UseFontAwesomeIcons();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
