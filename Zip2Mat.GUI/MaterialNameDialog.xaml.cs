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
    /// Interaction logic for MaterialNameDialog.xaml
    /// </summary>
    public partial class MaterialNameDialog : Window
	{
		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			txtMaterialName.SelectAll();
			txtMaterialName.Focus();
		}

		public string FileName
		{
			get { return txtMaterialName.Text; }
		}

		public MaterialNameDialog(string fileName)
		{
			InitializeComponent();
			txtMaterialName.Text = fileName;
		}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			this.DialogResult = false;
		}
    }
}
