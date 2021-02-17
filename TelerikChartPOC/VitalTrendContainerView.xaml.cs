﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            viewModel = new VitalTrendContainerVM(this.TempTrend, this.PulseTrend, this.BPTrend, this.PainTrend, this.RespTrend, this.O2Trend);
            this.BindingContext = viewModel;
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            Debug.WriteLine($"scrolling.. {e.ScrollX} {e.ScrollY}");
        }

        private void TempTrend_Unfocused(object sender, FocusEventArgs e)
        {
            Debug.WriteLine($"unfocused");
        }

        private void TempTrend_Focused(object sender, FocusEventArgs e)
        {
            Debug.WriteLine($"focused");
        }
    }
}