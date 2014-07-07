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


namespace Unv.ConwayLifeGame.Dialogs
{
	/// <summary>
	/// Interaction logic for NewGridDialog.xaml
	/// </summary>
	public partial class NewGridDialog 
		: Window
	{
		#region Constructors
		public NewGridDialog()
		{
			InitializeComponent();
			this.DataContext = this;
		}
		#endregion


		#region Event Handlers
		private void Create_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}
		#endregion
	}
}
