using System;
using System.Collections.Generic;
using System.Globalization;
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
using Unv.ConwayLifeGame.Controls;
using Unv.ConwayLifeGame.Model;
using Unv.ConwayLifeGame.ViewModels;


namespace Unv.ConwayLifeGame.Views
{
	/// <summary>
	/// Interaction logic for CellGridView.xaml
	/// </summary>
	/// <remarks>
	/// Yes, I know that I coupled the CellGridView to the CellGridViewModel, but
	/// please keep in mind that the CellGridViewModel is NOT coupled to the
	/// CellGridView. Most views in MVVM are sort of coupled to their view-models
	/// because the bindings tend to be hard coded. 
	/// -FCT
	/// </remarks>
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


			Binding b;

			//b = new Binding("CellGridState");
			//b.Mode = BindingMode.OneWay;
			//b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			//this.SetBinding(CellGridStateProperty, b);

			b = new Binding();
			b.Path = new PropertyPath("CellGridState");
			b.Mode = BindingMode.OneWay;
			b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			b.Converter = new GridStateToMouseInterationConverter();
			this.SetBinding(IsHitTestVisibleProperty, b);

			b = new Binding();
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
			if (ViewModel == null)
				return;

			var cellGridPanel = FindCellGrid(this.PART_ItemsControl) as UIElement;

			if (cellGridPanel == null)
				return;

			CellGridPanel.SetColumnCount(cellGridPanel, ViewModel.ColumnCount);
			CellGridPanel.SetRowCount(cellGridPanel, ViewModel.RowCount);
		}

		private DependencyObject FindCellGrid(DependencyObject parent)
		{
			int childCount = VisualTreeHelper.GetChildrenCount(parent);
			DependencyObject result = null;
			for(int i = 0; i < childCount; i++)
			{
				result = VisualTreeHelper.GetChild(parent, i) as DependencyObject;

				if (result is CellGridPanel)
					break;
					
				result = FindCellGrid(result);

				if (result != null)
					break;
			}

			return result;
		}
		#endregion
	}

	public class GridStateToMouseInterationConverter
		: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var state = value as CellGridState?;

			return state.HasValue ? state.Value == CellGridState.SettingInitialGeneration : false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
