using System;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace BeSharp.Windows.Forms.DataVisualization.Charting
{
    /// <summary>
    /// Adds fluent equivelents of AddXY and AddY methods just like "public DataPoint Add(params double[] y)"
    /// </summary>
    public static class DataPointCollectionExtensions
    {
        /// <summary>Adds a System.Windows.Forms.DataVisualization.Charting.DataPoint object to the end of the collection, with the specified X-value and Y-value.</summary>
        /// <param name="dataPointCollection">The System.Windows.Forms.DataVisualization.Charting.DataPointCollection instance to add a DataPoint to.</param>
        /// <param name="xValue">X-value of the data point.</param>
        /// <param name="yValue">Y-value of the data point.</param>
        /// <returns>The new System.Windows.Forms.DataVisualization.Charting.DataPoint object.</returns>
        public static DataPoint AddXYDataPoint(this DataPointCollection dataPointCollection, double xValue, double yValue)
        {
            int dataPointIndex = dataPointCollection.AddXY(xValue, yValue);
            DataPoint dataPoint = dataPointCollection[dataPointIndex];
            return dataPoint;
        }

        /// <summary>Adds a System.Windows.Forms.DataVisualization.Charting.DataPoint object to the end of the collection, with the specified X-value and Y-value(s).</summary>
        /// <param name="dataPointCollection">The System.Windows.Forms.DataVisualization.Charting.DataPointCollection instance to add a DataPoint to.</param>
        /// <param name="xValue">X-value of the data point.</param>
        /// <param name="yValue">One or more comma-separated values that represent the Y-value(s) of the data point.</param>
        /// <returns>The new System.Windows.Forms.DataVisualization.Charting.DataPoint object.</returns>
        public static DataPoint AddXYDataPoint(this DataPointCollection dataPointCollection, object xValue, params object[] yValue)
        {
            return dataPointCollection[dataPointCollection.AddXY(xValue, yValue)];
        }

        /// <summary>Adds a System.Windows.Forms.DataVisualization.Charting.DataPoint object to the end of the collection, with the specified Y-value.</summary>
        /// <param name="dataPointCollection">The System.Windows.Forms.DataVisualization.Charting.DataPointCollection instance to add a DataPoint to.</param>
        /// <param name="yValue">The Y-value of the data point.</param>
        /// <returns>The new System.Windows.Forms.DataVisualization.Charting.DataPoint object.</returns>
        public static DataPoint AddYDataPoint(this DataPointCollection dataPointCollection, double yValue)
        {
            return dataPointCollection[dataPointCollection.AddY(yValue)];
        }

        /// <summary>Adds a System.Windows.Forms.DataVisualization.Charting.DataPoint object to the end of the collection, with the specified Y-value(s).</summary>
        /// <param name="dataPointCollection">The System.Windows.Forms.DataVisualization.Charting.DataPointCollection instance to add a DataPoint to.</param>
        /// <param name="yValue">A comma-separated list of Y-value(s) of the System.Windows.Forms.DataVisualization.Charting.DataPoint object added to the collection.</param>
        /// <returns>The new System.Windows.Forms.DataVisualization.Charting.DataPoint object.</returns>
        public static DataPoint AddYDataPoint(this DataPointCollection dataPointCollection, params object[] yValue)
        {
            return dataPointCollection[dataPointCollection.AddY(yValue)];
        }

    }
}
