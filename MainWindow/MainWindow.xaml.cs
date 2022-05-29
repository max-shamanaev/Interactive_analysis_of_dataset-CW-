using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Data;
using DataBindings;
using ColumnCharts;

using Forms = System.Windows.Forms;

namespace MWindow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private string dataTablesDir = "..\\..\\..\\DataTables";
        private int lines = 50;
        private List<Person> rawdata = new List<Person>();

        private BasicColumn columnChart = new BasicColumn();
        private Brush chartBrush = Brushes.LightBlue;

        private Brush defRowBackg = Brushes.WhiteSmoke;
        private Brush defRowAltBackg = Brushes.White;

        public bool isDSpecsGridVisible = true;
        public bool isButtonsVisible = true;
        public bool isChartVisible = true;

        public MainWindow()
        {
            InitializeComponent();

            ChangeDSpecsGridVisibility.IsChecked = isDSpecsGridVisible;
            ChangeButtonsVisibility.IsChecked = isButtonsVisible;
            ChangeChartVisibility.IsChecked = isChartVisible;
        }

        private void DrawChart(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            if (rawdata.Count != 0)
            {
                switch (button.Name)
                {
                    case "Draw_salary_to_location_chart":
                        {
                            columnChart = new BasicColumn("Salary, $", DataSpecs.AvgSalaryPerLocation(rawdata), chartBrush);
                            ColumnChart.DataContext = columnChart;
                            break;
                        }
                    case "Draw_salary_to_profession_chart":
                        {
                            columnChart = new BasicColumn("Salary, $", DataSpecs.AvgSalaryPerProfession(rawdata), chartBrush);
                            ColumnChart.DataContext = columnChart;
                            break;
                        }
                    case "Draw_salary_to_gender_chart":
                        {
                            columnChart = new BasicColumn("Salary, $", DataSpecs.AvgSalaryPerGender(rawdata), chartBrush);
                            ColumnChart.DataContext = columnChart;
                            break;
                        }
                    case "Draw_age_to_profession_chart":
                        {
                            columnChart = new BasicColumn("Age", DataSpecs.AvgAgePerProfession(rawdata), chartBrush);
                            ColumnChart.DataContext = columnChart;
                            break;
                        }
                    default:
                        {
                            ColumnChart.DataContext = new BasicColumn();
                            btnAux.Focus();
                            break;
                        }
                }       
            }
            else
            {
                btnAux.Focus();
            }
        }
        private void cmClearChart(object sender, RoutedEventArgs e)
        {
            ColumnChart.DataContext = new BasicColumn();
            btnAux.Focus();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "data";
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                RawData rd = new RawData();

                try
                {
                    rawdata = rd.CreateDataFromFile(dlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR");
                }

                DB_PersonsListRaw db = new DB_PersonsListRaw(rawdata);
                RawDataGrid.DataContext = db;

                List<DB_DataSpecs> dbspecs = new List<DB_DataSpecs>();
                dbspecs.Add(new DB_DataSpecs(new DataSpecs(rawdata)));
                DataSpecsGrid.DataContext = dbspecs;
            }
        }

        private void btnCreateFile_Click(object sender, RoutedEventArgs e)
        {
            var inputDlg = new LinesInputWindow();
            bool? res1 = inputDlg.ShowDialog();

            if (res1 == true)
            {
                lines = inputDlg._lines;

                var saveDlg = new Microsoft.Win32.SaveFileDialog();
                saveDlg.FileName = "data_" + lines + "lines";
                saveDlg.DefaultExt = ".txt";
                saveDlg.Filter = "Text documents (.txt)|*.txt";
                bool? res2 = saveDlg.ShowDialog();

                if (res2 == true)
                {
                    RawData rd = new RawData();

                    try
                    {
                        rd.CreateRandomData(saveDlg.FileName, lines);
                        rawdata = rd.CreateDataFromFile(saveDlg.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR");
                    }

                    DB_PersonsListRaw db = new DB_PersonsListRaw(rawdata);
                    RawDataGrid.DataContext = db;

                    List<DB_DataSpecs> dbspecs = new List<DB_DataSpecs>();
                    dbspecs.Add(new DB_DataSpecs(new DataSpecs(rawdata)));
                    DataSpecsGrid.DataContext = dbspecs;
                }
            }
        }

        private void Specs_Clear_Selected_Click(object sender, RoutedEventArgs e)
        {
            DataSpecsGrid.UnselectAllCells();
        }

        private void Input_Clear_Selected_Click(object sender, RoutedEventArgs e)
        {
            RawDataGrid.UnselectAllCells();
        }

        public static IEnumerable<T> FindVisualChilds<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChilds<T>(ithChild)) yield return childOfChild;
            }
        }
        private void Change_Chart_Fill_Click(object sender, RoutedEventArgs e)
        {
            var clrDlg = new Forms.ColorDialog();
            clrDlg.Color = System.Drawing.Color.LightBlue;
            clrDlg.FullOpen = true;

            if(clrDlg.ShowDialog() == Forms.DialogResult.OK)
            {
                System.Drawing.Color dColor = clrDlg.Color;
                System.Windows.Media.Color mColor = System.Windows.Media.Color.FromArgb(dColor.A, dColor.R, dColor.G, dColor.B);

                chartBrush = new SolidColorBrush(mColor);
                foreach (Button btn in FindVisualChilds<Button>(btnGrid))
                {
                    if (btn.IsFocused == true)
                    {
                        DrawChart(btn, null);
                        break;
                    }
                }
            }
        }

        private void ChangeDGridCellsForeg_Click(object sender, RoutedEventArgs e)
        {
            var clrDlg = new Forms.ColorDialog();
            clrDlg.Color = System.Drawing.Color.Black;
            clrDlg.FullOpen = true;

            string menuItemName = ((MenuItem)sender).Name;

            if (clrDlg.ShowDialog() == Forms.DialogResult.OK)
            {
                System.Drawing.Color dColor = clrDlg.Color;
                System.Windows.Media.Color mColor = System.Windows.Media.Color.FromArgb(dColor.A, dColor.R, dColor.G, dColor.B);

                switch (menuItemName)
                {
                    case "ChangeDGridCellsForeg":
                        {
                            RawDataGrid.Foreground = new SolidColorBrush(mColor);
                            DataSpecsGrid.Foreground = new SolidColorBrush(mColor);
                            break;
                        }
                    case "RawDataGridChangeForeg":
                        {
                            RawDataGrid.Foreground = new SolidColorBrush(mColor);
                            break;
                        }
                    case "DataSpecsChangeForeg":
                        {
                            DataSpecsGrid.Foreground = new SolidColorBrush(mColor);
                            break;
                        }
                }
            }
        }

        private void ChangeDGridCellsBackg_Click(object sender, RoutedEventArgs e)
        {
            var clrDlg = new Forms.ColorDialog();
            clrDlg.FullOpen = true;

            string menuItemName = ((MenuItem)sender).Name;

            if (clrDlg.ShowDialog() == Forms.DialogResult.OK)
            {
                System.Drawing.Color dColor = clrDlg.Color;
                System.Windows.Media.Color mColor = System.Windows.Media.Color.FromArgb(dColor.A, dColor.R, dColor.G, dColor.B);

                switch (menuItemName)
                {
                    case "ChangeDGridCellsBackg":
                        {
                            RawDataGrid.RowBackground = new SolidColorBrush(mColor);
                            RawDataGrid.AlternatingRowBackground = null;
                            RawDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.None;

                            DataSpecsGrid.RowBackground = new SolidColorBrush(mColor);
                            DataSpecsGrid.GridLinesVisibility = DataGridGridLinesVisibility.None;
                            break;
                        }
                    case "RawDataGridChangeBackg":
                        {
                            RawDataGrid.RowBackground = new SolidColorBrush(mColor);
                            RawDataGrid.AlternatingRowBackground = null;
                            RawDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.None;
                            break;
                        }
                    case "DataSpecsGridChangeBackg":
                        {
                            DataSpecsGrid.RowBackground = new SolidColorBrush(mColor);
                            DataSpecsGrid.GridLinesVisibility = DataGridGridLinesVisibility.None;
                            break;
                        }
                }
            }
        }

        private void ChangeDSpecsGridVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (isDSpecsGridVisible)
            {
                isDSpecsGridVisible = false;
                DataSpecsGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                isDSpecsGridVisible = true;
                DataSpecsGrid.Visibility = Visibility.Visible;
            }
        }

        private void ChangeButtonsVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (isButtonsVisible)
            {
                isButtonsVisible = false;
                btnsPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                isButtonsVisible = true;
                btnsPanel.Visibility = Visibility.Visible;
            }
        }

        private void ChangeChartVisibility_Click(object sender, RoutedEventArgs e)
        {
            if (isChartVisible)
            {
                isChartVisible = false;
                ColumnChart.Visibility = Visibility.Collapsed;
            }
            else
            {
                isChartVisible = true;
                ColumnChart.Visibility = Visibility.Visible;
            }
        }

        private void DefaultDGridCellsBackg_Click(object sender, RoutedEventArgs e)
        {
            RawDataGrid.RowBackground = defRowBackg;
            RawDataGrid.AlternatingRowBackground = defRowAltBackg;
            RawDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;

            DataSpecsGrid.RowBackground = defRowAltBackg;
            DataSpecsGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
        }

        private void DefaultDGridCellsForeg_Click(object sender, RoutedEventArgs e)
        {
            RawDataGrid.Foreground = Brushes.Black;
            DataSpecsGrid.Foreground = Brushes.Black;
        }

        private void SetDefaultColors_Click(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).Name == "RawDataGridDefaultColors")
            {
                RawDataGrid.RowBackground = defRowBackg;
                RawDataGrid.AlternatingRowBackground = defRowAltBackg;
                RawDataGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
                RawDataGrid.Foreground = Brushes.Black;
            }
            else if (((MenuItem)sender).Name == "DataSpecsGridDefaultColors")
            {
                DataSpecsGrid.RowBackground = defRowAltBackg;
                DataSpecsGrid.GridLinesVisibility = DataGridGridLinesVisibility.All;
                DataSpecsGrid.Foreground = Brushes.Black;
            }
            else
            {
                chartBrush = Brushes.LightBlue;
                foreach (Button btn in FindVisualChilds<Button>(btnGrid))
                {
                    if (btn.IsFocused == true)
                    {
                        DrawChart(btn, null);
                        break;
                    }
                }
            }
        }
    }
}
