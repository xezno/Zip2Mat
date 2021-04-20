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

namespace Zip2Mat.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StackPanel_Drop(object sender, DragEventArgs eventArgs)
        {
            if (eventArgs.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])eventArgs.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    var matName = System.IO.Path.GetFileNameWithoutExtension(file);
                    var fileNameDialog = new MaterialNameDialog(matName) { Owner = this };

                    if ((bool)fileNameDialog.ShowDialog())
                    {
                        matName = fileNameDialog.FileName;
                        MatGen.Generate(file, matName, (bool)chkTga.IsChecked, (bool)chkNormalize.IsChecked);
                        MessageBox.Show($"Generated material {matName}");
                    }
                }
            }
        }
    }
}
