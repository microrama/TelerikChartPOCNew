using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TelerikChartPOC.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Allscripts.POH")]
[assembly: ExportEffect(typeof(iOSPanEffect), nameof(TelerikChartPOC.PanEffect))]
namespace TelerikChartPOC.iOS
{
    public class iOSPanEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                // this.Control is the native component
                // TKChartAxis exposes two properties dedicated to control the pan level of the axis - "Pan" and "NormalizedPan".
                // See https://docs.telerik.com/devtools/xamarin/nativecontrols/ios/chart/zoom-and-pan for more info

                if (this.Control is TelerikUI.TKChart chart &&
                    Element.Effects.FirstOrDefault() is PanEffect effect)
                {

                    // Set this with the value passed form the RoutedEffect class
                    chart.XAxis.AllowPan = true;
                    chart.XAxis.NormalizedPan = 1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected override void OnDetached()
        {

        }
    }
}