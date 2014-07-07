using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unv.ConwayLifeGame.Helpers;
using Unv.ConwayLifeGame.Model;


namespace Unv.ConwayLifeGame.ViewModels
{
	public class CellGridViewModel
		: ViewModel
	{
		#region Events
		public event EventHandler GridSizeUpdated;
		#endregion


		#region Attributes
		protected CellFactory m_cellFactory;
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

		public virtual bool IsBusy
		{
			get { return mn_isBusy; }
			protected set
			{
				if (mn_isBusy != value)
				{
					mn_isBusy = value;
					OnPropertyChanged("IsBusy");
				}
			}
		}
		private bool mn_isBusy = false;

		public virtual bool FormationLocked
		{
			get { return mn_formationLocked; }
			protected set
			{
				if (mn_formationLocked != value)
				{
					mn_formationLocked = value;
					OnPropertyChanged("FormationLocked");
				}
			}
		}
		private bool mn_formationLocked;

		public virtual CellGridState CellGridState
		{
			get { return mn_cellGridState; }
			protected set
			{
				if (mn_cellGridState != value)
				{
					mn_cellGridState = value;
					OnPropertyChanged("CellGridState");
				}
			}
		}
		private CellGridState mn_cellGridState = CellGridState.ManualProgression;
		#endregion


		#region Constructors
		public CellGridViewModel()
		{
			Cells = new ObservableCollection<CellViewModel>();

			m_cellFactory = new CellFactory(this);
			m_cellFactory.CellCreationFinished += CellFactory_CellCreationFinished;
		}
		#endregion


		#region Event Handlers
		void CellFactory_CellCreationFinished(object sender, EventArgs e)
		{
			this.IsBusy				= false;
			this.FormationLocked	= false;
			this.CellGridState		= CellGridState.SettingInitialGeneration;
		}
		#endregion


		#region Methods
		public virtual void SetNewGrid(int columnCount, int rowCount)
		{
			IsBusy			= true;
			FormationLocked = true;
			ColumnCount		= columnCount;
			RowCount		= rowCount;

			this.Cells.Clear();

			if (GridSizeUpdated != null)
				GridSizeUpdated(this, null);

			m_cellFactory.CreateCells();
		}
		#endregion
	}
}
