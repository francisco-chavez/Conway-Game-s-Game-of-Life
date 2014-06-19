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

		public int X
		{
			get { return mn_x; }
			set
			{
				if (mn_x != value)
				{
					mn_x = value;
					OnPropertyChanged("X");
				}
			}
		}
		private int mn_x;

		public int Y
		{
			get { return mn_y; }
			set
			{
				if (mn_y != value)
				{
					mn_y = value;
					OnPropertyChanged("Y");
				}
			}
		}
		private int mn_y;
		#endregion
	}
}
