using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Unv.ConwayLifeGame.ViewModels
{
	public class MainWindowViewModel
		: ViewModel
	{
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


		public MainWindowViewModel()
		{
			// I know that the "this." isn't needed, but CellGridViewModel = new CellGridViewModel()
			// can look a bit off when you read the property name without the IDE color coding certain
			// items for the developer.
			// -FCT
			this.CellGridViewModel = new CellGridViewModel();
		}
	}
}
