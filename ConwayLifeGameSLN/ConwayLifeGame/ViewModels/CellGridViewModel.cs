﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
			this.CellGridState		= CellGridState.SettingInitialGeneration;
		}
		#endregion


		#region Methods
		public virtual void SetNewGrid(int columnCount, int rowCount)
		{
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
