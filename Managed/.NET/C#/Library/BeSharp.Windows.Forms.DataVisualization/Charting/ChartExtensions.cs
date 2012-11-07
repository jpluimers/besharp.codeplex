using System;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace BeSharp.Windows.Forms.DataVisualization.Charting
{
    public static class ChartExtensions
    {
        public static Series SeriesByname(this Chart chart, string name)
        {
            Series result = chart.Series.Single(series => series.Name == name);
            return result;
        }

        public static void ClearAllSeries(this Chart chart)
        {
            foreach (Series series in chart.Series)
            {
                series.Points.Clear();
            }
        }
    }
}
