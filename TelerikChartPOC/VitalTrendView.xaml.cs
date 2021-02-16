﻿using System;
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
    public interface IVitalTrendView
    {
        void SetPanZoomValues(Point panOffset, Size zoom);
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VitalTrendView : ContentView, IVitalTrendView
    {
        public VitalTrendView()
        {
            InitializeComponent();
        }

        private async void chart_NativeControlLoaded(object sender, EventArgs e)
        {
            chart.PropertyChanged += Chart_PropertyChanged;

            // Ideally, setting the PanOffset in XAML should have worked but that is a bug per Telerik Support
            // We have to wait a bit for the rendering to complete before setting it would take effect.
            // In future, we can set it in XAML when this bug is fixed
            // https://www.telerik.com/account/support-tickets/view-ticket/1507202
            await Task.Delay(100);
            if (Device.RuntimePlatform == Device.Android)
            {
                chart.PanOffset = new Point(-(chart.Width * chart.Zoom.Width), 0);
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                chart.PanOffset = new Point(340000, 0);
            }   
        }

        private void chart_NativeControlUnloaded(object sender, EventArgs e)
        {
            chart.PropertyChanged -= Chart_PropertyChanged;
        }

        private void Chart_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Debug.WriteLine($"{e.PropertyName}");
            var trendVM = this.BindingContext as VitalTrendVM;

            if (e.PropertyName == "Zoom")
            {
                if (sender?.GetType() == typeof(RadCartesianChart))
                {
                    // user is zooming
                    // notify parent VM of these changes on tyhis trend view
                    var chart = sender as RadCartesianChart;
                    Debug.WriteLine($"{chart.Zoom}  {trendVM.TrendType}");

                    trendVM.NotifyPanZoomChangeToParent(chart.PanOffset, chart.Zoom);
                }
            }

            if (e.PropertyName == "PanOffset")
            {
                // user is scrolling panning
                // notify parent VM of these changes on this trend view
                if (sender?.GetType() == typeof(RadCartesianChart))
                {
                    var chart = sender as RadCartesianChart;
                    Debug.WriteLine($"{chart.PanOffset}  {trendVM.TrendType}");

                    trendVM.NotifyPanZoomChangeToParent(chart.PanOffset, chart.Zoom);
                }

            }
        }

        #region Implement IVitalTrendView

        public void SetPanZoomValues(Point panOffset, Size zoom)
        {
            chart.PropertyChanged -= Chart_PropertyChanged;

            this.chart.Zoom = zoom;
            this.chart.PanOffset = panOffset;

            chart.PropertyChanged += Chart_PropertyChanged;
        }

        #endregion

    }
}