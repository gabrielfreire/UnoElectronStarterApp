using ElectronNET.API;
using ElectronNET.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnoTest.Electron.Controllers
{
    public class MenusController
    {
        public static void SetMenu()
        {
            if (HybridSupport.IsElectronActive)
            {
                var menu = new MenuItem[]
                {
                    new MenuItem { Label = "File", Submenu = new MenuItem[]
                        {
                            new MenuItem { Label = "Quit", Accelerator = "CmdOrCtrl+Q", Role = MenuRole.close }
                        }
                    },
                    new MenuItem { Label = "View", Submenu = new MenuItem[]
                        {
                            new MenuItem { Label = "Reload", Accelerator = "CmdOrCtrl+R", Click = () =>
                                {
                                    var mainWindowId = ElectronNET.API.Electron.WindowManager.BrowserWindows.ToList().First().Id;
                                    ElectronNET.API.Electron.WindowManager.BrowserWindows.ToList().ForEach(browserWindow =>
                                    {
                                        if (browserWindow.Id != mainWindowId)
                                        {
                                            browserWindow.Close();
                                        }
                                        else
                                        {
                                            browserWindow.Reload();
                                        }
                                    });
                                }
                            },
                            new MenuItem { Type = MenuType.separator },
                            new MenuItem { Label = "Open Developer Tools", Accelerator = "CmdOrCtrl+I", 
                                Click = () => ElectronNET.API.Electron.WindowManager.BrowserWindows.First().WebContents.OpenDevTools() }
                        }
                    }
                };

                ElectronNET.API.Electron.Menu.SetApplicationMenu(menu);
            }
        }
    }
}
