using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unv.ConwayLifeGame.Helpers;


namespace Unv.ConwayLifeGame.ViewModels
{
	public class CellGridViewModel
		: ViewModel
	{
		#region Attributes
		private CellFactory m_cellFactory;
		#endregion


		#region Properties
		public virtual int RowCount
		{
			get { return mn_rowCount; }
			protected set
			{
				if (mn_rowCount != value)
				{
					mn_rowCount = value;
					OnPropertyChanged("RowCount");
				}
			}
		}
		private int mn_rowCount;

		public virtual int ColumnCount
		{
			get { return mn_columnCount; }
			protected set
			{
				if (mn_columnCount != value)
				{
					mn_columnCount = value;
					OnPropertyChanged("ColumnCount");
				}
			}
		}
		private int mn_columnCount;

		public virtual ObservableCollection<CellViewModel> Cells
		{
			get { return mn_cells; }
			protected set
			{
				if (mn_cells != value)
				{
					mn_cells = value;
					OnPropertyChanged("Cells");
				}
			}
		}
		private ObservableCollection<CellViewModel> mn_cells;
		#endregion


		#region Constructors
		public CellGridViewModel()
		{
			m_cellFactory = new CellFactory(this);

			Cells = new ObservableCollection<CellViewModel>();
		}
		#endregion


		#region Methods
		public void SetNewGrid(int columnCount, int rowCount)
		{
			ColumnCount = columnCount;
			RowCount = rowCount;
		}
		#endregion
	}
}
