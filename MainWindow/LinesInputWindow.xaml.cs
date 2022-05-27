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
using System.Windows.Shapes;

namespace MWindow
{
    /// <summary>
    /// Логика взаимодействия для LinesInputWindow.xaml
    /// </summary>
    public partial class LinesInputWindow : Window
    {
        public int _lines;
        private const int maxLines = 10000;
        public LinesInputWindow()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            bool isOk = true;
            try
            {
                _lines = Convert.ToInt32(InputLinesBox.Text);
            }
            catch (Exception ex)
            {
                this.Hide();
                MessageBox.Show(ex.Message, "ERROR");
                this.ShowDialog();
                InputLinesBox.Text = "";
                isOk = false;
            }

            if (_lines > maxLines || _lines <= 0)
                isOk = false;

            if (isOk)
                this.DialogResult = true;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }    
    }
}
