using System;
using System.Windows.Input;


namespace Unv.ConwayLifeGame
{
	public class RelayCommand
		: ICommand
	{
		#region Events
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		#endregion


		#region Attributes
		private readonly Action<object>		m_execute;
		private readonly Predicate<object>	m_canExecute;
		#endregion


		#region Constructors
		public RelayCommand(Action<object> execute)
			: this(execute, null) { }

		public RelayCommand(Action<object> execute, Predicate<object> canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("Missing execute action.");

			m_execute		= execute;
			m_canExecute	= canExecute;
		}
		#endregion


		#region Methods
		public bool CanExecute(object parameter)
		{
			if (m_canExecute == null)
				return true;

			return m_canExecute(parameter);
		}

		public void Execute(object parameter)
		{
			m_execute(parameter);
		}
		#endregion
	}
}
