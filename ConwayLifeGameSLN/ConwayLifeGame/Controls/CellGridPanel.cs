using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Unv.ConwayLifeGame.Controls
{
	public class CellGridPanel
		: Grid
	{
		#region Attributes
		public static readonly DependencyProperty ColumnCountProperty;
		public static readonly DependencyProperty RowCountProperty;
		#endregion


		#region Properties
		protected int ColumnCount
		{
			get { return (int) GetValue(ColumnCountProperty); }
		}
		protected int RowCount
		{
			get { return (int) GetValue(RowCountProperty); }
		}
		#endregion


		#region Constructors
		static CellGridPanel()
		{
			ColumnCountProperty = DependencyProperty.Register(
				"ColumnCount",
				typeof(int),
				typeof(CellGridPanel),
				new FrameworkPropertyMetadata(
					3,
					FrameworkPropertyMetadataOptions.Inherits,
					ColumnCount_Changed));

			RowCountProperty = DependencyProperty.Register(
				"RowCount",
				typeof(int),
				typeof(CellGridPanel),
				new FrameworkPropertyMetadata(
					3,
					FrameworkPropertyMetadataOptions.Inherits,
					RowCount_Changed));
		}
		#endregion


		#region Depdendency Property Event Handlers
		private static void ColumnCount_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (!(d is CellGridPanel))
				return;

			CellGridPanel obj = (CellGridPanel) d;
			int columnCount = (int) e.NewValue;

			obj.ColumnDefinitions.Clear();

			for (int i = 0; i < columnCount; i++)
				obj.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1.0, GridUnitType.Star) });
		}

		private static void RowCount_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (!(d is CellGridPanel))
				return;

			CellGridPanel obj = (CellGridPanel) d;
			int rowCount = (int) e.NewValue;

			obj.RowDefinitions.Clear();

			for (int i = 0; i < rowCount; i++)
				obj.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1.0, GridUnitType.Star) });
		}
		#endregion


		#region Methods
		public static void SetColumnCount(UIElement element, int value)
		{
			element.SetValue(ColumnCountProperty, value);
		}

		public static int GetColumnCount(UIElement element)
		{
			return (int) element.GetValue(ColumnCountProperty);
		}

		public static void SetRowCount(UIElement element, int value)
		{
			element.SetValue(RowCountProperty, value);
		}

		public static int GetRowCount(UIElement element)
		{
			return (int) element.GetValue(RowCountProperty);
		}
		#endregion
	}
}
