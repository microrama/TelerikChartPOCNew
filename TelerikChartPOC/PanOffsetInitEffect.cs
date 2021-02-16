using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TelerikChartPOC
{
    public class PanOffsetInitEffect : RoutingEffect
    {
        //Allscripts.PlatformOfHealth.LongPressEffect
        public PanOffsetInitEffect() : base("Allscripts.POH.PanOffsetInitEffect")
        {

        }

        public double PanXOffset_Android { get; set; }

        public double NormalizedPan_iOS { get; set; }

        public double ZoomLevel { get; set; }

    }
}
