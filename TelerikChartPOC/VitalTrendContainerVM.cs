using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TelerikChartPOC
{
    public class VitalTrendContainerVM : INotifyPropertyChanged
    {
        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        private List<VitalTrendVM> trendVmList = new List<VitalTrendVM>();

        private IVitalTrendView TempTrendView { get; set; }
        public VitalTrendVM TempVitalTrendVM { get; set; }

        private IVitalTrendView RespTrendView { get; set; }
        public VitalTrendVM RespVitalTrendVM { get; set; }

        #endregion

        public VitalTrendContainerVM(IVitalTrendView tempTrendView, IVitalTrendView respTrendView)
        {
            TempTrendView = tempTrendView;
            RespTrendView = respTrendView;

            // Get the Data from Network and prepare VMs 
            this.LoadData();
        }

        private void UpdatePanZoomForAllCharts(TrendType updatingTrendType, Point panOffset, Size zoom)
        {
            trendVmList?.ForEach(trendVM => 
            { 
                if(trendVM.TrendType != updatingTrendType)
                {
                    trendVM.SetPanZoomValues(panOffset, zoom);
                    //trendVM.PanOffset = panOffset;
                    //trendVM.Zoom = zoom;
                }
            });
        }

        /// <summary>
        /// Get Data and assemble it
        /// Set the BindingContext of each VitalTrendView
        /// </summary>
        public void LoadData()
        {
            // Get Temp Trend Data, create a VitalTrendVM that Temp Vital ContentView will bind to 
            var tempTuple = GetDateTimeData(100, TrendType.Temp);
            TempVitalTrendVM = new VitalTrendVM(TempTrendView, TrendType.Temp, tempTuple.Item1, tempTuple.Item2, tempTuple.Item3, tempTuple.Item4, tempTuple.Item5, tempTuple.Item6, UpdatePanZoomForAllCharts);
            trendVmList.Add(TempVitalTrendVM);

            // Get Temp Trend Data, create a VitalTrendVM that Temp Vital ContentView will bind to 
            tempTuple = GetDateTimeData(100, TrendType.Resp);
            RespVitalTrendVM = new VitalTrendVM(RespTrendView, TrendType.Resp, tempTuple.Item1, tempTuple.Item2, tempTuple.Item3, tempTuple.Item4, tempTuple.Item5, tempTuple.Item6, UpdatePanZoomForAllCharts);
            trendVmList.Add(RespVitalTrendVM);
        }

        /// <summary>
        /// Create Mock Chart Data for each Vital Item based on the trend type
        /// </summary>
        /// <param name="itemsCount"></param>
        /// <param name="trendType"></param>
        /// <returns></returns>
        private Tuple<ObservableCollection<ChartDataPoint>, ObservableCollection<ChartDataPoint>, DateTime, DateTime, double, double> GetDateTimeData(int itemsCount, TrendType trendType)
        {
            var startDate = DateTime.Now;

            ObservableCollection<ChartDataPoint> chartData = new ObservableCollection<ChartDataPoint>();
            ObservableCollection<ChartDataPoint> secondChartData = null;
            var rand = new Random();

            // set min & max initial values
            double minYValue = double.MaxValue, maxYValue = double.MinValue;

            for (int i = 0; i < itemsCount; i++)
            {
                var data = new ChartDataPoint();
                ChartDataPoint secondData = null;
                data.Date = startDate.AddHours(-i).AddMinutes((i + rand.Next(1, 30)));

                switch (trendType)
                {
                    case TrendType.Temp:
                        data.Value = rand.Next(96, 105);

                        secondData = new ChartDataPoint();
                        secondData.Value = data.Value - 10;
                        secondData.Date = data.Date;
                        break;

                    case TrendType.Resp:
                        data.Value = rand.Next(10, 40);
                        break;
                }

                // calculate Min & Max Y value across all data points
                minYValue = data.Value < minYValue ? data.Value : minYValue;
                maxYValue = data.Value > maxYValue ? data.Value : maxYValue;

                chartData.Add(data);

                if(secondData != null)
                {
                    if(secondChartData == null)
                    {
                        secondChartData = new ObservableCollection<ChartDataPoint>();
                    }
                    secondChartData.Add(secondData);
                }
                    
            }

            // Create a Max chart point Date + some hours (to the hour) from the data for the X-Axis
            var date = startDate.AddHours(4);
            var maxChartDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);

            // Create a Min Chart Point Date - some hours (to the hour) from the data for the X-Axis
            date = chartData[chartData.Count - 1].Date.AddHours(-4); 
            var minChartDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, 0, 0);

            // return mock data
            return new Tuple<ObservableCollection<ChartDataPoint>, ObservableCollection<ChartDataPoint>, DateTime, DateTime, double, double>(chartData, secondChartData, minChartDate, maxChartDate, minYValue - 20, maxYValue + 20);
        }

    }

    public class ChartDataPoint
    {
        public DateTime Date { get; set; }

        public double Value { get; set; }
    }

    public enum TrendType
    {
        Temp = 1,
        Resp = 2
    }

}
