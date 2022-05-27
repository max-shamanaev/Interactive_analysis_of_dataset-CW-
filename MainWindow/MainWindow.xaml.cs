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

        public MainWindow()
        {
            InitializeComponent();
       
        }

        private void DrawChart(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            switch (button.Name)
            {
                case "Draw_salary_to_location_chart":
                    {
                        ColumnChart.DataContext = new BasicColumn("Salary, $", DataSpecs.AvgSalaryPerLocation(rawdata));
                        break;
                    }
                case "Draw_salary_to_profession_chart":
                    {
                        ColumnChart.DataContext = new BasicColumn("Salary, $", DataSpecs.AvgSalaryPerProfession(rawdata));
                        break;
                    }
                case "Draw_salary_to_gender_chart":
                    {
                        ColumnChart.DataContext = new BasicColumn("Salary, $", DataSpecs.AvgSalaryPerGender(rawdata));
                        break;
                    }
                case "Draw_age_to_profession_chart":
                    {
                        ColumnChart.DataContext = new BasicColumn("Age", DataSpecs.AvgAgePerProfession(rawdata));
                        break;
                    }
                default:
                    {
                        ColumnChart.DataContext = new BasicColumn();
                        break;
                    }
            }
        }
        private void cmClearChart(object sender, RoutedEventArgs e)
        {
            ColumnChart.DataContext = new BasicColumn();
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

                if (res1 == true && res2 == true)
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

        private void Change_Font_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Clear_Selected_Click(object sender, RoutedEventArgs e)
        {
            RawDataGrid.UnselectAllCells();
        }

        private void DSpecs_Clear_Selected_Click(object sender, RoutedEventArgs e)
        {
            DataSpecsGrid.UnselectAllCells();
        }
    }
}
