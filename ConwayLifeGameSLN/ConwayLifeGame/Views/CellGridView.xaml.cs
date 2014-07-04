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

using Unv.ConwayLifeGame.ViewModels;


namespace Unv.ConwayLifeGame.Views
{
	/// <summary>
	/// Interaction logic for CellGridView.xaml
	/// </summary>
	public partial class CellGridView 
		: UserControl
	{
		#region Attributes
		public static readonly DependencyProperty ViewModelProperty;

		private bool m_firstTimeLoaded = true;
		#endregion


		#region Properties
		public CellGridViewModel ViewModel
		{
			get { return (CellGridViewModel) GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}
		#endregion


		#region Constructors
		static CellGridView()
		{
			ViewModelProperty = DependencyProperty.Register(
				"ViewModel",
				typeof(CellGridViewModel),
				typeof(CellGridView),
				new PropertyMetadata(
					null, 
					ViewModel_Changed));
		}

		public CellGridView()
		{
			InitializeComponent();

			Binding b = new Binding();
			this.SetBinding(ViewModelProperty, b);

			this.Loaded += CellGridView_Loaded;
		}
		#endregion


		#region Dependency Property Event Handlers
		private static void ViewModel_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var me = (CellGridView) d;
			CellGridViewModel grid;
			
			grid = e.OldValue as CellGridViewModel;
			if (grid != null)
				grid.GridSizeUpdated -= me.UpdateGridSize;

			grid = e.NewValue as CellGridViewModel;
			if (grid != null)
				grid.GridSizeUpdated += me.UpdateGridSize;

			me.UpdateGridSize(null, null);
		}
		#endregion


		#region Event Handlers
		void CellGridView_Loaded(object sender, RoutedEventArgs e)
		{
			if (!m_firstTimeLoaded)
				return;

			ViewModel.SetNewGrid(10, 10);
		}

		private void UpdateGridSize(object source, EventArgs e)
		{
			PART_Grid.ColumnDefinitions.Clear();
			PART_Grid.RowDefinitions.Clear();

			if (ViewModel == null)
				return;

			for (int i = 0; i < ViewModel.ColumnCount; i++)
				PART_Grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
			for (int i = 0; i < ViewModel.RowCount; i++)
				PART_Grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Star) });
		}
		#endregion
	}
}
