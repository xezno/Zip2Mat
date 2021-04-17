using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Zip2Mat.GUI
{
    /// <summary>
    /// Interaction logic for FileName.xaml
    /// </summary>
    public partial class FileNameDialog : Window
	{
		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			txtFileName.SelectAll();
			txtFileName.Focus();
		}

		public string FileName
		{
			get { return txtFileName.Text; }
		}

		public FileNameDialog(string fileName)
		{
			InitializeComponent();
			txtFileName.Text = fileName;
		}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.DialogResult = true;
		}
    }
}
