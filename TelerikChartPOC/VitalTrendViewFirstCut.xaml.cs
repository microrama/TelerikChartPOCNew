using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.Chart;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TelerikChartPOC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VitalTrendViewFirstCut : ContentView
    {
        public VitalTrendViewFirstCut()
        {
            InitializeComponent();
        }

        private void chart1_NativeControlLoaded(object sender, EventArgs e)
        {
            chart1.PropertyChanged += Chart1_PropertyChanged;
        }

        private void chart1_NativeControlUnloaded(object sender, EventArgs e)
        {
            chart1.PropertyChanged -= Chart1_PropertyChanged;
        }

        private void Chart1_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Debug.WriteLine($"{e.PropertyName}");

            if (e.PropertyName == "Zoom")
            {
                if (sender?.GetType() == typeof(RadCartesianChart))
                {
                    var chart = sender as RadCartesianChart;
                    Debug.WriteLine($"{chart.Zoom}  {this.Id}");

                    // call VM to update the parent and send this ContentView's Id and it's current PanOffset
                    var vm = this.BindingContext as ViewModelFirstCut;
                    vm.updateParentChartOffsetChange(this.Id.ToString(), chart.PanOffset, chart.Zoom);
                }
            }

            if (e.PropertyName == "PanOffset")
            {
                // user is scrolling horizontally or panning
                // capture the current offset
                if(sender?.GetType() == typeof(RadCartesianChart))
                {
                    var chart = sender as RadCartesianChart;
                    Debug.WriteLine($"{chart.PanOffset}  {this.Id}");

                    // call VM to update the parent and send this ContentView's Id and it's current PanOffset
                    var vm = this.BindingContext as ViewModelFirstCut;
                    vm.updateParentChartOffsetChange(this.Id.ToString(), chart.PanOffset, chart.Zoom);
                }
                
            }
        }

        public void SetPanOffset(Point offset, Size zoom)
        {
            this.chart1.PanOffset = offset;
            this.chart1.Zoom = zoom;
        }
    }
}