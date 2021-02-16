using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TelerikChartPOC.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Allscripts.POH")]
[assembly: ExportEffect(typeof(AndroidPanOffsetInitEffect), "PanOffsetInitEffect")]
namespace TelerikChartPOC.Droid
{
    public class AndroidPanOffsetInitEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                // Control is the native component
                if (this.Control is Com.Telerik.Widget.Chart.Visualization.CartesianChart.RadCartesianChartView chart &&
                    Element.Effects.FirstOrDefault() is PanOffsetInitEffect effect)
                {
                    //// set the zoom
                    //chart.SetZoom(effect.ZoomLevel, 1);

                    //// Set the Pan offset
                    //chart.SetPanOffset(effect.PanXOffset_Android, 0);

                    chart.SetPanOffset(-1000.0, 0);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        protected override void OnDetached()
        {
            
        }
    }
}