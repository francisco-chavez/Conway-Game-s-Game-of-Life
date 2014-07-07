using System;
using System.ComponentModel;
using System.Windows;


namespace Unv.ConwayLifeGame.Dialogs
{
	/// <summary>
	/// Interaction logic for NewGridDialog.xaml
	/// </summary>
	public sealed partial class NewGridDialog 
		: Window, INotifyPropertyChanged
	{
		#region Events
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion


		#region Properties
		/// <summary>
		/// Get or set the number of columns the new Cell Grid will have.
		/// </summary>
		public int ColumnCount
		{
			get { return mn_columnCount; }
			set
			{
				if (mn_columnCount != value)
				{
					mn_columnCount = Clamp(5, value, 50);
					OnPropertyChanged("ColumnCount");
				}
			}
		}
		private int mn_columnCount;

		/// <summary>
		/// Get or set the number of rows the new Cell Grid will have.
		/// </summary>
		public int RowCount
		{
			get { return mn_rowCount; }
			set
			{
				if (mn_rowCount != value)
				{
					mn_rowCount = Clamp(5, value, 50);
					OnPropertyChanged("RowCount");
				}
			}
		}
		private int mn_rowCount;
		#endregion


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
			this.DialogResult	= true;

			this.ColumnCount	= 10;
			this.RowCount		= 10;
		}
		#endregion


		#region Methods
		/// <summary>
		/// Will bring the given value within the given min and max 
		/// values and return the result.
		/// </summary>
		private int Clamp(int min, int value, int max)
		{
			return Math.Min(max, Math.Max(min, value));
		}

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
