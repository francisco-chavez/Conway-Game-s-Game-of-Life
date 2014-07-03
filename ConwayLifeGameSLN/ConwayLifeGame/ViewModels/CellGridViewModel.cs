using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unv.ConwayLifeGame.ViewModels
{
	public class CellGridViewModel
		: ViewModel
	{
		#region Properties
		public int GridWidth
		{
			get { return mn_gridWidth; }
			private set
			{
				if (mn_gridWidth != value)
				{
					mn_gridWidth = value;
					OnPropertyChanged("GridWidth");
				}
			}
		}
		private int mn_gridWidth;

		public int GridHeight
		{
			get { return mn_gridHeight; }
			private set
			{
				if (mn_gridHeight != value)
				{
					mn_gridHeight = value;
					OnPropertyChanged("GridHeight");
				}
			}
		}
		private int mn_gridHeight;
		#endregion


		#region Methods
		public void SetNewGrid(int gridWidth, int gridHeight)
		{
			GridWidth = gridWidth;
			GridHeight = gridHeight;
		}
		#endregion
	}
}
