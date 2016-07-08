using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;

namespace CreateDummyFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateSizeLabel();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "Dummy Files|*.dum";
            saveFileDialog.Title = "Create a dummy file";
            saveFileDialog.FileName = "derp.dum";

            if (saveFileDialog.ShowDialog() == true)
            {
                // Get the file size from the slider
                long newFileSize = DoubleToLongInt(slider.Value) * 1024;

                MessageBox.Show("New file size: (bytes) " + newFileSize.ToString());

                if (newFileSize >= 0)
                {
                    try
                    {
                        FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
                        fs.Seek(newFileSize, SeekOrigin.Begin);
                        fs.WriteByte(0);
                        fs.Close();

                        MessageBox.Show("File saved!", "File saved!", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

        }

        private string KBToGB(double kb)
        {
            return (kb/1024/1024).ToString("N2");
        }
        
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateSizeLabel();
        }

        private long DoubleToLongInt(double dbl)
        {
            return (long) dbl;
        }

        private void UpdateSizeLabel()
        {
            long newValue = DoubleToLongInt(slider.Value);
            lblSize.Content = newValue + " KB (" + KBToGB(newValue) + " GB)";
        }

    }
}
