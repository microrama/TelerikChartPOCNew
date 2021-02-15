using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TelerikChartPOC
{
    public partial class MainPage : ContentPage
    {

        private void SetPanOffsetForAllCharts(string chartControlDriverName, Point panOffset, Size zoom)
        {
            if(chartControlDriverName == this.chartContainer2.Id.ToString())
            {
                this.chartContainer1.SetPanOffset(panOffset, zoom);
            }
            else
            {
                this.chartContainer2.SetPanOffset(panOffset, zoom);
            }
        }

        public MainPage()
        {
            InitializeComponent();

            //setPanOffsetForAllChartsAction = SetPanOffsetForAllCharts;

            this.chartContainer1.BindingContext = new ViewModelFirstCut(120, 1, SetPanOffsetForAllCharts);
            this.chartContainer2.BindingContext = new ViewModelFirstCut(120, 1, SetPanOffsetForAllCharts);
        }
    }
}
