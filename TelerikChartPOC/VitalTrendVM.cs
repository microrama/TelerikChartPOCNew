using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace TelerikChartPOC
{

    public class VitalTrendVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Action<TrendType, Point, Size> _notifyParentCallback;
        private IVitalTrendView _view;

        public VitalTrendVM(IVitalTrendView view, TrendType trendType, ObservableCollection<ChartDataPoint> chartData,
            DateTime minChartDate, DateTime maxChartDate,
            Action<TrendType, Point, Size> returnAction = null)
        {
            _view = view;
            _notifyParentCallback = returnAction;
            this.TrendType = trendType;
            this.ChartData = chartData;
            this.MinChartDate = minChartDate;
            this.MaxChartDate = maxChartDate;
        }

        #region Properties 

        public TrendType TrendType { get; set; }

        public ObservableCollection<ChartDataPoint> ChartData { get; set; }

        public DateTime MaxChartDate { get; set; }

        public DateTime MinChartDate { get; set; }

        private Point _panOffset = new Point(0,0);
        public Point PanOffset
        {
            get => _panOffset;
            set
            {
                if(_panOffset != value)
                {
                    _panOffset = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PanOffset)));
                }
                
            }
        }

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
