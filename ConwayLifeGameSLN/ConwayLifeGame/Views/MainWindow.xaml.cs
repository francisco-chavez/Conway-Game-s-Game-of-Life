using System.Windows;

using Unv.ConwayLifeGame.ViewModels;


namespace Unv.ConwayLifeGame.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow 
		: Window
	{
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = new MainWindowViewModel();
		}
	}
}
