using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Data;
using LiveCharts;
using LiveCharts.Wpf;

namespace ColumnCharts
{
    public partial class BasicColumn : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public BasicColumn() { }
        public BasicColumn(string _title, Dictionary<string, double> dict)
        {
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = _title,
                    Values = new ChartValues<double>(dict.Values)
                }
            };

            Labels = new List<string>();
            foreach (var d in dict)
                Labels.Add(d.Key);
            Formatter = value => value.ToString("N");
        }
    }
}
