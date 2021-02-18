using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TelerikChartPOC
{
    public class PanOffsetInitEffect : RoutingEffect
    {
        //Allscripts.PlatformOfHealth.LongPressEffect
        public PanOffsetInitEffect() : base("Allscripts.POH.PanOffsetInitEffect")
        {

        }

        public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached("Command", typeof(ICommand), typeof(PanOffsetInitEffect), (object)null);
        public static ICommand GetCommand(BindableObject view)
        {
            return (ICommand)view.GetValue(CommandProperty);
        }

        public static void SetCommand(BindableObject view, ICommand value)
        {
            view.SetValue(CommandProperty, value);
        }


        public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("CommandParameter", typeof(object), typeof(PanOffsetInitEffect), (object)null);
        public static object GetCommandParameter(BindableObject view)
        {
            return view.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(BindableObject view, object value)
        {
            view.SetValue(CommandParameterProperty, value);
        }

        public double PanXOffset_Android { get; set; }

        public double NormalizedPan_iOS { get; set; }

        public double ZoomLevel { get; set; }

    }
}
