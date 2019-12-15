using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnoTest.Electron.Extensions
{
    public static class AppWasmExtensions
    {
        public static void UseUnoWasm(this IApplicationBuilder app)
        {
            var provider = new FileExtensionContentTypeProvider();
            // Add new mappings
            provider.Mappings.Remove(".dll");
            provider.Mappings.Remove(".wasm");
            provider.Mappings.Remove(".woff");
            provider.Mappings.Remove(".woff2");
            provider.Mappings[".wasm"] = "application/wasm";
            provider.Mappings[".clr"] = "application/octet-stream";
            provider.Mappings[".pdb"] = "application/octet-stream";
            provider.Mappings[".json"] = "application/octet-stream";
            provider.Mappings[".woff"] = "application/font-woff";
            provider.Mappings[".woff2"] = "application/font-woff";
            provider.Mappings[".image"] = "image/png";

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");

            app.UseDefaultFiles(options);

            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
        }
    }
}
