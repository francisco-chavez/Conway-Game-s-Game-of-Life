using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Unv.ConwayLifeGame.ViewModels
{
	public class CellViewModel
		: ViewModel
	{
		#region Properties
		/// <summary>
		/// Gets or sets the the cell's state of life.
		/// </summary>
		public bool IsLiving
		{
			get { return mn_isLiving; }
			set
			{
				if (mn_isLiving != value)
				{
					mn_isLiving = value;
					OnPropertyChanged("IsLiving");
				}
			}
		}
		private bool mn_isLiving;

		/// <summary>
		/// Gets or sets if the cell will be dead or alive when we enter the next step.
		/// </summary>
		public bool WillKeepLiving
		{
			get { return mn_willKeepLiving; }
			set
			{
				if (mn_willKeepLiving != value)
				{
					mn_willKeepLiving = value;
					OnPropertyChanged("WillKeepLiving");
				}
			}
		}
		private bool mn_willKeepLiving;

		/// <summary>
		/// Gets or sets the cell's row position in the cell grid.
		/// </summary>
		public int Row
		{
			get { return mn_row; }
			set
			{
				if (mn_row != value)
				{
					mn_row = value;
					OnPropertyChanged("Row");
				}
			}
		}
		private int mn_row;

		/// <summary>
		/// Gets or sets the cell's column position in the cell grid.
		/// </summary>
		public int Column
		{
			get { return mn_column; }
			set
			{
				if (mn_column != value)
				{
					mn_column = value;
					OnPropertyChanged("Column");
				}
			}
		}
		private int mn_column;
		#endregion
	}
}
