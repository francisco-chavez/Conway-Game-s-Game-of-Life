﻿using System.Windows.Input;

using Unv.ConwayLifeGame.Dialogs;


namespace Unv.ConwayLifeGame.ViewModels
{
	/// <summary>
	/// This class is the ViewModel for the MainWindow
	/// </summary>
	public class MainWindowViewModel
		: ViewModel
	{
		#region Properties
		/// <summary>
		/// Gets the ViewModel of the Cell Grid.
		/// </summary>
		public virtual CellGridViewModel CellGridViewModel
		{
			get { return mn_cellGridViewModel; }
			protected set
			{
				if (mn_cellGridViewModel != value)
				{
					mn_cellGridViewModel = value;
					OnPropertyChanged("CellGridViewModel");
				}
			}
		}
		private CellGridViewModel mn_cellGridViewModel;

		/// <summary>
		/// Gets the command that both resets and resizes the
		/// cell grid.
		/// </summary>
		public ICommand CreateNewCellGridCommand
		{
			get
			{
				if (m_createNewCellGridCommand == null)
					m_createNewCellGridCommand = new RelayCommand(CreateNewCellGridExecute, CreateNewCellGridCanExecute);

				return m_createNewCellGridCommand;
			}
		}
		private RelayCommand m_createNewCellGridCommand;
		#endregion


		#region Constructors
		public MainWindowViewModel()
		{
			// I know that the "this." isn't needed, but CellGridViewModel = new CellGridViewModel()
			// can look a bit off when you read the property name without the IDE color coding certain
			// items for the developer.
			// -FCT
			this.CellGridViewModel = new CellGridViewModel();
		}
		#endregion


		#region Methods
		private void CreateNewCellGridExecute(object parameters)
		{
			// Prep the dialog that's used to get User input.
			var dlg = new NewGridDialog();
			dlg.ColumnCount = this.CellGridViewModel.ColumnCount;
			dlg.RowCount	= this.CellGridViewModel.RowCount;
			dlg.Owner		= App.Current.MainWindow;

			// Launch the dialog and find out if the User
			// really wants to do what they are about to
			// do.
			bool keepGoing = dlg.ShowDialog() == true;

			if (!keepGoing)
				return;

			// Use the User's input to start the creating of a new
			// Cell Grid.
			this.CellGridViewModel.SetNewGrid(dlg.ColumnCount, dlg.RowCount);
		}

		private bool CreateNewCellGridCanExecute(object parameters)
		{
			return !this.CellGridViewModel.IsBusy;
		}
		#endregion
	}
}
