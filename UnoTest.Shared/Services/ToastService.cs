using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace UnoTest.Shared.Services
{
    public class ToastService
    {
        public void Show(string message, bool IsLongToast)
        {
            Debug.WriteLine(message);
            var xml = string.Format(@"<toast>
                <visual>
                    <binding template=""ToastImageAndText04"">
                        <image id=""1"" src="""" alt=""unotest""/>
                        <text id=""1"">Smart Health</text>
                        <text id=""2"">{0}</text>
                        <text id=""3""></text>
                    </binding>
                </visual>
            </toast>", message);
            var toastXml = new XmlDocument();
            toastXml.LoadXml(xml);
            var toast = new ToastNotification(toastXml);
            toast.Tag = "123456";
            toast.Group = "SmartHealthNotifcation";
            toast.ExpirationTime = DateTime.Now.AddDays(1);
            toast.Failed += (s, e) => Debug.WriteLine(e.ErrorCode);
            toast.Activated += (s, e) => Debug.WriteLine(e.ToString());

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
