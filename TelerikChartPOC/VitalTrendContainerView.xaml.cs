using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TelerikChartPOC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VitalTrendContainerView : ContentView
    {
        private VitalTrendContainerVM viewModel;

        public VitalTrendContainerView()
        {
            InitializeComponent();

            viewModel = new VitalTrendContainerVM(this.TempTrend, this.RespTrend);
            this.BindingContext = viewModel;
        }
    }
}