using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace TelerikChartPOC
{
    public class ViewModelFirstCut
    {
        private Action<string, Point, Size> callParentAction; 
        private int _dataCount = 120;
        private int _dataCreateCounter = 2;
        public ObservableCollection<TemporalData> Data { get; set; }

        public Point ChartPanOffset { get; set; }

        public DateTime MaxChartDate { get; set; }

        public DateTime MinChartDate { get; set; }

        public ViewModelFirstCut(int dataCount = 120, int dataCreateCounter = 2, Action<string, Point, Size> returnAction = null)
        {
            callParentAction = returnAction;
            _dataCount = dataCount;
            _dataCreateCounter = dataCreateCounter;

            this.Data = GetDateTimeData(_dataCount);
        }

        public ViewModelFirstCut(Action<string, Point, Size> returnAction)
        {
            callParentAction = returnAction;

            this.Data = GetDateTimeData(_dataCount);
        }

        public void updateParentChartOffsetChange(string chartName, Point chartOffset, Size zoom)
        {
            callParentAction?.Invoke(chartName, chartOffset, zoom);
        }

        private ObservableCollection<TemporalData> GetDateTimeData(int itemsCount)
        {
            var startDate = DateTime.Now;

            ObservableCollection<TemporalData> items = new ObservableCollection<TemporalData>();
            for (int i = 0; i < itemsCount; i++)
            {
                TemporalData data = new TemporalData();
                data.Date = startDate.AddHours(-i).AddMinutes((i + _dataCreateCounter) * 2);
                data.Value = Math.Sin(i);

                items.Add(data);
            }

            var maxChartDate = startDate.AddHours(1);
            MaxChartDate = new DateTime(maxChartDate.Year, maxChartDate.Month, maxChartDate.Day, maxChartDate.Hour, 0, 0);
            var minChartDate = items[items.Count - 1].Date.AddHours(-1);
            MinChartDate = new DateTime(minChartDate.Year, minChartDate.Month, minChartDate.Day, minChartDate.Hour, 0, 0);

            return items;
        }
    }

    public class TemporalData
    {
        public DateTime Date { get; set; }

        public double Value { get; set; }
    }
}
