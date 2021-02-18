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
        PanOffsetInitEffect paneffect;

        protected override void OnAttached()
        {
            try
            {
                // Control is the native component
                if (this.Control is Com.Telerik.Widget.Chart.Visualization.CartesianChart.RadCartesianChartView chart &&
                    Element.Effects.FirstOrDefault() is PanOffsetInitEffect effect)
                {
                    //paneffect = effect;
                    //chart.Touch += Chart_Touch;
                    //chart.LongClick += Chart_LongClick;
                    
                    //// set the zoom
                    //chart.SetZoom(effect.ZoomLevel, 1);

                    //// Set the Pan offset
                    //chart.SetPanOffset(effect.PanXOffset_Android, 0);

                    //chart.SetPanOffset(-1000.0, 0);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void Chart_LongClick(object sender, Android.Views.View.LongClickEventArgs e)
        {
            Console.WriteLine($"longclick ");
            var command = PanOffsetInitEffect.GetCommand(Element);
            command?.Execute(true);

            e.Handled = false;
        }

        private void Chart_Touch(object sender, Android.Views.View.TouchEventArgs e)
        {
            Console.WriteLine($" {e.Event.Action}");
            var command = PanOffsetInitEffect.GetCommand(Element);
            command?.Execute(false);

            //if(e.Event.Action == MotionEventActions.Cancel || 
            //    e.Event.Action == MotionEventActions.Up)
            //{
            //    var command = PanOffsetInitEffect.GetCommand(Element);
            //    command?.Execute(true);
            //}
            //else
            //{
            //    var command = PanOffsetInitEffect.GetCommand(Element);
            //    command?.Execute(false);
            //}

            e.Handled = false;
        }

        protected override void OnDetached()
        {
            if (this.Control is Com.Telerik.Widget.Chart.Visualization.CartesianChart.RadCartesianChartView chart &&
                   Element.Effects.FirstOrDefault() is PanOffsetInitEffect effect)
            {
                //chart.Touch -= Chart_Touch;
                //chart.LongClick -= Chart_LongClick;
            }
        }
    }
}