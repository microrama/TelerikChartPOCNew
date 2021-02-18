using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TelerikChartPOC
{

    public class VitalTrendVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Action<TrendType, Point, Size> _notifyParentCallback;
        private Action<bool> _parentScrollAction;
        private IVitalTrendView _view;

        public VitalTrendVM(IVitalTrendView view, TrendType trendType, 
            ObservableCollection<ChartDataPoint> chartData, ObservableCollection<ChartDataPoint> secondChartData,
            DateTime minChartDate, DateTime maxChartDate, double minYValue, double maxYValue,
            Action<TrendType, Point, Size> notifyParentCallback = null, 
            Action<bool> parentScrollAction = null)
        {
            _view = view;
            _notifyParentCallback = notifyParentCallback;
            _parentScrollAction = parentScrollAction;
            this.TrendType = trendType;
            this.ChartData = chartData;
            this.SecondChartData = secondChartData;
            this.MinChartDate = minChartDate;
            this.MaxChartDate = maxChartDate;
            this.MinChartYValue = minYValue;
            this.MaxChartYValue = maxYValue;

            ChartTouch = new Command(ChartTouchHandler);

            switch(this.TrendType)
            {
                case TrendType.Temp:
                    this.TrendName = "Temperature";
                    break;

                case TrendType.Pulse:
                    this.TrendName = "Pulse";
                    break;

                case TrendType.BP:
                    this.TrendName = "Blood Pressure";
                    break;

                case TrendType.Pain:
                    this.TrendName = "Blood Pressure";
                    break;

                case TrendType.Resp:
                    this.TrendName = "Respiratory Rate";
                    break;

                case TrendType.O2:
                    this.TrendName = "O2 Saturation";
                    break;
            }
        }

        private void ChartTouchHandler(object obj)
        {
            Debug.WriteLine($"ChartTouchHandler = {obj}");
            _parentScrollAction((bool)obj);
        }

        #region Properties 

        public ICommand ChartTouch { get; set; }

        public TrendType TrendType { get; set; }

        public string TrendName { get; set; }

        public ObservableCollection<ChartDataPoint> ChartData { get; set; }

        public ObservableCollection<ChartDataPoint> SecondChartData { get; set; }

        public DateTime MaxChartDate { get; set; }

        public DateTime MinChartDate { get; set; }

        public double MaxChartYValue { get; set; }

        public double MinChartYValue { get; set; }

        //private Point _panOffset = new Point(0,0);
        //public Point PanOffset
        //{
        //    get => _panOffset;
        //    set
        //    {
        //        if(_panOffset != value)
        //        {
        //            _panOffset = value;
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PanOffset)));
        //        }

        //    }
        //}

        private Size _zoom = new Size(2,1);
        public Size Zoom
        {
            get => _zoom;
            set
            {
                if(_zoom != value)
                {
                    _zoom = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Zoom)));
                }
            }
        }

        #endregion

        #region Notify and React to Pan/Zoom user changes on a trend view

        public void NotifyPanZoomChangeToParent(Point panOffset, Size zoom)
        {
            _notifyParentCallback?.Invoke(this.TrendType, panOffset, zoom);
        }

        public void SetPanZoomValues(Point panOffset, Size zoom)
        {
            // Let the View update the valueson the Chart control
            _view.SetPanZoomValues(panOffset, zoom);
        }

        #endregion

    }
}
