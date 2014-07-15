using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
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
	/// because the bindings tend to be hard coded in XAML. 
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
		/// <summary>
		/// Gets or sets the ViewModel for this View.
		/// </summary>
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

			// This binding will make it so that the User can't click
			// on the CellViews when the CellGridState is not in a
			// SeedingState. Could I have done this binding in XAML,
			// yes I could have.
			// -FCT
			b = new Binding();
			b.Path				= new PropertyPath("CellGridState");
			b.Mode				= BindingMode.OneWay;
			b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			b.Converter			= new GridStateToMouseInterationConverter();
			this.SetBinding(IsHitTestVisibleProperty, b);

			// This binding will make sure I can reference the item
			// in the DataContext from the ViewModel property. This
			// way, I don't have to write the code that will caste
			// the ViewModel into a ViewModel each time I reference
			// the DataContext. It will still get caste eachtime
			// I reference it, I just don't have to write the
			// each time.
			// It also lets me do things like registering to the
			// events I want to listen to each time the DataContex
			// is changed.
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

			var cellGridPanel = Find_CellGridPanel(this.PART_ItemsControl) as UIElement;

			if (cellGridPanel == null)
				return;

			CellGridPanel.SetColumnCount(cellGridPanel, ViewModel.ColumnCount);
			CellGridPanel.SetRowCount(cellGridPanel, ViewModel.RowCount);
		}

		/// <summary>
		/// This recursive method will look for a CellGridPanel within
		/// the VisualTree of the DependencyObject that is passed in. If
		/// no CellGridPanel is found, it will return null. This is a
		/// depth-search-first method.
		/// </summary>
		private DependencyObject Find_CellGridPanel(DependencyObject parent)
		{
			int childCount = VisualTreeHelper.GetChildrenCount(parent);
			DependencyObject result = null;
			for(int i = 0; i < childCount; i++)
			{
				// See if ith child is a CellGridPanel
				result = VisualTreeHelper.GetChild(parent, i) as DependencyObject;

				if (result is CellGridPanel)
					break;
				
				// Since the ith child wan't a CellGridPanel, see if the
				// ith child contains a CellGridPanel by making a call to
				// Find_CellGridPanle on the ith child.
				result = Find_CellGridPanel(result);

				// If the ith child returned something other than null,
				// then it returned a CellGridPanel, and there's no point
				// in checking the rest of the children for a CellGridPanel.
				if (result != null)
					break;
			}

			return result;
		}
		#endregion
	}

	/// <summary>
	/// This method converts a CellGridState into permission to allow mouse clicks.
	/// </summary>
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
