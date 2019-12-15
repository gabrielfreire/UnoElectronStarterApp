using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using UnoTest.Electron.Controllers;
using UnoTest.Electron.Extensions;

namespace UnoTest.Electron
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
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            // use Wasm
            app.UseUnoWasm();

            // Open the Electron-Window here
            if (HybridSupport.IsElectronActive)
            {
                _ = ElectronBootstrap();
            }

        }
        public async Task ElectronBootstrap()
        {
            var browserWindow = await ElectronNET.API.Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                Width = 1152,
                Height = 940,
                Show = false,
                // can not use jquery requiresjs because of imcompatibility with UNO Wasm bootstraper
                // https://github.com/unoplatform/uno/issues/624
                WebPreferences = new WebPreferences { NodeIntegration = false }
            });

            await browserWindow.WebContents.Session.ClearCacheAsync();
            browserWindow.SetTitle(Configuration["DemoTitleInSettings"]);
            browserWindow.Show();

            // set custom menu
            MenusController.SetMenu();
        }
    }
}
