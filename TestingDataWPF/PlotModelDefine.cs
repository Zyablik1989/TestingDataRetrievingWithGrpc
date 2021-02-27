using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;


namespace TestingDataWPF
{
    public class PlotModelDefine
    {
        public static PlotModel GridLinesHorizontal(string data)
        {
            var plotModel = new PlotModel();

            var xAxis = new LinearAxis();
            xAxis.Position = AxisPosition.Left;
            xAxis.MajorGridlineStyle = LineStyle.Solid;
            xAxis.AxislineColor = OxyColors.Black;
            xAxis.TickStyle = TickStyle.Outside;
            xAxis.TicklineColor = OxyColors.Black;
            xAxis.TextColor = OxyColors.Black;

            var yAxis = new LinearAxis();
            yAxis.Position = AxisPosition.Bottom;
            yAxis.MajorGridlineStyle = LineStyle.Solid;
            yAxis.AxislineColor = OxyColors.Black;
            yAxis.TickStyle = TickStyle.Outside;
            yAxis.TicklineColor = OxyColors.Black;
            yAxis.TextColor = OxyColors.Black;

            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);


            FunctionSeries fs = new FunctionSeries();

            //String with unpredictable length of random char-numbers converting to numbers.
            var characters = data.ToCharArray();
            for (int i = 0; i < characters.Length; i++)
            {
                var point = Convert.ToInt32(characters[i].ToString());
                fs.Points.Add(new DataPoint( i + 1, point));
            }

            //Adding graph to plot
            plotModel.Series.Add(fs);

            return plotModel;
        }
    }
}
