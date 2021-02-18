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
        private DateTime allChartsEnddDate = DateTime.Now;
        private DateTime allChartsStartDate = DateTime.MaxValue;
        private const int numChartDataPoints = 24;

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _ScrollEnabled = true;
        public bool ScrollEnabled
        {
            get => _ScrollEnabled;
            set
            {
                if (_ScrollEnabled != value)
                {
                    _ScrollEnabled = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScrollEnabled)));
                }
            }
        }

        private List<VitalTrendVM> trendVmList = new List<VitalTrendVM>();

        private IVitalTrendView TempTrendView { get; set; }
        public VitalTrendVM TempVitalTrendVM { get; set; }

        private IVitalTrendView PulseTrendView { get; set; }
        public VitalTrendVM PulseVitalTrendVM { get; set; }

        private IVitalTrendView BPTrendView { get; set; }
        public VitalTrendVM BPVitalTrendVM { get; set; }

        private IVitalTrendView PainTrendView { get; set; }
        public VitalTrendVM PainVitalTrendVM { get; set; }

        private IVitalTrendView RespTrendView { get; set; }
        public VitalTrendVM RespVitalTrendVM { get; set; }

        private IVitalTrendView O2TrendView { get; set; }
        public VitalTrendVM O2VitalTrendVM { get; set; }

        #endregion

        public VitalTrendContainerVM(IVitalTrendView tempTrendView, IVitalTrendView pulseTrendView,
            IVitalTrendView bpTrendView, IVitalTrendView painTrendView,
            IVitalTrendView respTrendView, IVitalTrendView o2TrendView)
        {
            TempTrendView = tempTrendView;
            PulseTrendView = pulseTrendView;
            BPTrendView = bpTrendView;
            PainTrendView = painTrendView;
            RespTrendView = respTrendView;
            O2TrendView = o2TrendView;

            // Get the Data from Network and prepare VMs 
            this.LoadData();
        }

        private void UpdatePanZoomForAllCharts(TrendType updatingTrendType, Point panOffset, Size zoom)
        {
            ScrollEnabled = false;

            trendVmList?.ForEach(trendVM => 
            { 
                if(trendVM.TrendType != updatingTrendType)
                {
                    trendVM.SetPanZoomValues(panOffset, zoom);
                    //trendVM.PanOffset = panOffset;
                    //trendVM.Zoom = zoom;
                }
            });

            ScrollEnabled = true;
        }

        private void EnableScrollView(bool enable)
        {
            ScrollEnabled = enable;
            //if(enable == true)
            //{
            //    Task.Run(() => {
            //        Task.Delay(1000);
            //        ScrollEnabled = enable;
            //    });
            //}
            //else
            //{
            //    ScrollEnabled = enable;
            //}
        }

        /// <summary>
        /// Get Data and assemble it
        /// Set the BindingContext of each VitalTrendView
        /// </summary>
        public void LoadData()
        {
            // Get Temp Trend Data, create a VitalTrendVM that Temp Vital ContentView will bind to 
            var tempTuple = GetDateTimeData(numChartDataPoints, TrendType.Temp);
            TempVitalTrendVM = new VitalTrendVM(TempTrendView, TrendType.Temp, tempTuple.Item1, tempTuple.Item2, tempTuple.Item3, tempTuple.Item4, tempTuple.Item5, tempTuple.Item6, 
                UpdatePanZoomForAllCharts, EnableScrollView);
            trendVmList.Add(TempVitalTrendVM);

            tempTuple = GetDateTimeData(numChartDataPoints, TrendType.Pulse);
            PulseVitalTrendVM = new VitalTrendVM(PulseTrendView, TrendType.Pulse, tempTuple.Item1, tempTuple.Item2, tempTuple.Item3, tempTuple.Item4, tempTuple.Item5, tempTuple.Item6, UpdatePanZoomForAllCharts, EnableScrollView);
            trendVmList.Add(PulseVitalTrendVM);

            tempTuple = GetDateTimeData(numChartDataPoints, TrendType.BP);
            BPVitalTrendVM = new VitalTrendVM(BPTrendView, TrendType.BP, tempTuple.Item1, tempTuple.Item2, tempTuple.Item3, tempTuple.Item4, tempTuple.Item5, tempTuple.Item6, UpdatePanZoomForAllCharts, EnableScrollView);
            trendVmList.Add(BPVitalTrendVM);

            tempTuple = GetDateTimeData(numChartDataPoints, TrendType.Pain);
            PainVitalTrendVM = new VitalTrendVM(PainTrendView, TrendType.Pain, tempTuple.Item1, tempTuple.Item2, tempTuple.Item3, tempTuple.Item4, tempTuple.Item5, tempTuple.Item6, UpdatePanZoomForAllCharts, EnableScrollView);
            trendVmList.Add(PainVitalTrendVM);

            tempTuple = GetDateTimeData(numChartDataPoints, TrendType.Resp);
            RespVitalTrendVM = new VitalTrendVM(RespTrendView, TrendType.Resp, tempTuple.Item1, tempTuple.Item2, tempTuple.Item3, tempTuple.Item4, tempTuple.Item5, tempTuple.Item6, UpdatePanZoomForAllCharts, EnableScrollView);
            trendVmList.Add(RespVitalTrendVM);

            tempTuple = GetDateTimeData(numChartDataPoints, TrendType.O2);
            O2VitalTrendVM = new VitalTrendVM(O2TrendView, TrendType.O2, tempTuple.Item1, tempTuple.Item2, tempTuple.Item3, tempTuple.Item4, tempTuple.Item5, tempTuple.Item6, UpdatePanZoomForAllCharts, EnableScrollView);
            trendVmList.Add(O2VitalTrendVM);

            TempVitalTrendVM.MinChartDate = new DateTime(allChartsStartDate.Year, allChartsStartDate.Month, allChartsStartDate.Day, allChartsStartDate.Hour, 0, 0);
            PulseVitalTrendVM.MinChartDate = new DateTime(allChartsStartDate.Year, allChartsStartDate.Month, allChartsStartDate.Day, allChartsStartDate.Hour, 0, 0);
            BPVitalTrendVM.MinChartDate = new DateTime(allChartsStartDate.Year, allChartsStartDate.Month, allChartsStartDate.Day, allChartsStartDate.Hour, 0, 0);
            PainVitalTrendVM.MinChartDate = new DateTime(allChartsStartDate.Year, allChartsStartDate.Month, allChartsStartDate.Day, allChartsStartDate.Hour, 0, 0);
            RespVitalTrendVM.MinChartDate = new DateTime(allChartsStartDate.Year, allChartsStartDate.Month, allChartsStartDate.Day, allChartsStartDate.Hour, 0, 0);
            O2VitalTrendVM.MinChartDate = new DateTime(allChartsStartDate.Year, allChartsStartDate.Month, allChartsStartDate.Day, allChartsStartDate.Hour, 0, 0);
        }

        /// <summary>
        /// Create Mock Chart Data for each Vital Item based on the trend type
        /// </summary>
        /// <param name="itemsCount"></param>
        /// <param name="trendType"></param>
        /// <returns></returns>
        private Tuple<ObservableCollection<ChartDataPoint>, ObservableCollection<ChartDataPoint>, DateTime, DateTime, double, double> GetDateTimeData(int itemsCount, TrendType trendType)
        {
           
            ObservableCollection<ChartDataPoint> chartData = new ObservableCollection<ChartDataPoint>();
            ObservableCollection<ChartDataPoint> secondChartData = null;
            var rand = new Random();

            // set min & max initial values
            double minYValue = double.MaxValue, maxYValue = double.MinValue;

            for (int i = 0; i < itemsCount; i++)
            {
                var data = new ChartDataPoint();
                ChartDataPoint secondData = null;
                data.Date = allChartsEnddDate.AddHours(-i).AddMinutes((i + rand.Next(1, 30)));

                switch (trendType)
                {
                    case TrendType.BP:
                        data.Value = rand.Next(96, 105);

                        secondData = new ChartDataPoint();
                        secondData.Value = data.Value - 10;
                        secondData.Date = data.Date;
                        break;

                    case TrendType.Resp:
                        data.Value = rand.Next(20, 50);
                        break;

                    default:
                        data.Value = rand.Next(40, 80);
                        break;
                }

                // calculate Min & Max Y value across all data points for this chart
                minYValue = data.Value < minYValue ? data.Value : minYValue;
                maxYValue = data.Value > maxYValue ? data.Value : maxYValue;

                // calculate the min Date for all the charts
                if(allChartsStartDate > data.Date)
                {
                    allChartsStartDate = data.Date;
                }

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
            var date = allChartsEnddDate.AddHours(4);
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
        Pulse,
        BP,
        Pain,
        Resp,
        O2
    }

}
