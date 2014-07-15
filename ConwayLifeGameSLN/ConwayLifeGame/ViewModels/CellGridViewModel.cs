using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

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
		protected CellFactory			m_cellFactory;
		protected CellViewModel[]		m_cells;
		protected ConwayCellProgressor	m_cellStepProgressor = new ConwayCellProgressor();
		#endregion


		#region Properties
		public ICommand StartStopCommand
		{
			get
			{
				if (m_startStopCommand == null)
					m_startStopCommand = new RelayCommand(StartStopGameProgress, StartStopGameProgressCanExecute);

				return m_startStopCommand;
			}
		}
		private RelayCommand m_startStopCommand;

		public ICommand StepCommand
		{
			get
			{
				if (m_stepCommand == null)
					m_stepCommand = new RelayCommand(GameProgressStep, GameProgressStepCanExecute);

				return m_stepCommand;
			}
		}
		private RelayCommand m_stepCommand;

		/// <summary>
		/// Gets the number of rows in the cell grid.
		/// </summary>
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

		/// <summary>
		/// Gets the number of columns in the cell grid.
		/// </summary>
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

		/// <summary>
		/// Gets the collection containing the cells in the cell grid.
		/// </summary>
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

		/// <summary>
		/// When the Cell Grid View Model is currently busy, then it's not
		/// a good time to make changes to the cell grid view model (unless
		/// you're the reason it's busy).
		/// </summary>
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

		/// <summary>
		/// Gets the current Cell Grid State of the Cell Grid View Model.
		/// </summary>
		public virtual CellGridState CellGridState
		{
			get { return mn_cellGridState; }
			protected set
			{
				if (mn_cellGridState != value)
				{
					mn_cellGridState = value;
					OnPropertyChanged("CellGridState");
					OnPropertyChanged("IsOnAuto");
				}
			}
		}
		private CellGridState mn_cellGridState = CellGridState.ManualProgression;

		/// <summary>
		/// Gets the current step/generation of the game.
		/// </summary>
		public virtual int CellGeneration
		{
			get { return mn_cellGeneration; }
			protected set
			{
				if (mn_cellGeneration != value)
				{
					mn_cellGeneration = value;
					OnPropertyChanged("CellGeneration");
				}
			}
		}
		private int mn_cellGeneration;

		public virtual bool IsOnAuto
		{
			get { return this.CellGridState == CellGridState.AutoProgression; }
		}
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
			m_cells = this.Cells.ToArray();

			this.IsBusy				= false;
			this.CellGridState		= CellGridState.SettingInitialGeneration;
		}
		#endregion


		#region Methods
		/// <summary>
		/// This method is used flush out the CellGridViewModel and change the grid size.
		/// </summary>
		public virtual void SetNewGrid(int columnCount, int rowCount)
		{
			if (IsBusy)
				throw new InvalidOperationException("The Cell Grid View Model is too busy to changes its settings right now.");
			
			this.IsBusy				= true;
			this.CellGridState		= CellGridState.LoadingCells;
			this.ColumnCount		= columnCount;
			this.RowCount			= rowCount;
			this.CellGeneration		= 0;

			this.Cells.Clear();

			if (GridSizeUpdated != null)
				GridSizeUpdated(this, null);

			m_cellFactory.CreateCellsAsync();
		}

		private void StartStopGameProgress(object parameters)
		{
			throw new NotImplementedException();
		}

		private bool StartStopGameProgressCanExecute(object parameters)
		{
			switch (this.CellGridState)
			{
			case CellGridState.ManualProgression:
			case CellGridState.SettingInitialGeneration:
			case CellGridState.AutoProgression:
				return true;

			case CellGridState.LoadingCells:
				return false;

			default:
				return false;
			}
		}

		private void GameProgressStep(object parameters)
		{
			this.CellGridState = CellGridState.ManualProgression;
			m_cellStepProgressor.StepCells(m_cells, this.ColumnCount, this.RowCount);
			this.CellGeneration++;
		}

		private bool GameProgressStepCanExecute(object parameters)
		{
			switch (this.CellGridState)
			{
			case CellGridState.ManualProgression:
			case CellGridState.SettingInitialGeneration:
				return true;

			case CellGridState.AutoProgression:
			case CellGridState.LoadingCells:
				return false;

			default:
				return false;
			}
		}
		#endregion
	}

	/// <summary>
	/// This converter is really more like a ToString() method for the CellGridState enum.
	/// </summary>
	public class CellGridStateTextConverter
		: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var state = (CellGridState) value;
			switch (state)
			{
			case CellGridState.AutoProgression:
				return "Auto Run";
			case CellGridState.LoadingCells:
				return "Loading";
			case CellGridState.ManualProgression:
				return "Manual";
			case CellGridState.SettingInitialGeneration:
				return "Seed State";
			default:
				return string.Empty;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
