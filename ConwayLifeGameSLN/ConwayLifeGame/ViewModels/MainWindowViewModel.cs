using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Unv.ConwayLifeGame.Dialogs;


namespace Unv.ConwayLifeGame.ViewModels
{
	public class MainWindowViewModel
		: ViewModel
	{
		#region Properties
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
		#endregion


		#region Properties
		private RelayCommand m_createNewCellGridCommand;
		public ICommand CreateNewCellGridCommand
		{
			get
			{
				if (m_createNewCellGridCommand == null)
					m_createNewCellGridCommand = new RelayCommand(CreateNewCellGridExecute, CreateNewCellGridCanExecute);

				return m_createNewCellGridCommand;
			}
		}
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
			var dlg = new NewGridDialog();
			dlg.ColumnCount = this.CellGridViewModel.ColumnCount;
			dlg.RowCount = this.CellGridViewModel.RowCount;
			dlg.Owner = App.Current.MainWindow;

			bool keepGoing = dlg.ShowDialog() == true;

			if (!keepGoing)
				return;

			this.CellGridViewModel.SetNewGrid(dlg.ColumnCount, dlg.RowCount);
		}

		private bool CreateNewCellGridCanExecute(object parameters)
		{
			if (this.CellGridViewModel.IsBusy)
				return false;

			return true;
		}
		#endregion
	}
}
