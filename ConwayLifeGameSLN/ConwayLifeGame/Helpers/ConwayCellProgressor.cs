using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Unv.ConwayLifeGame.ViewModels;


namespace Unv.ConwayLifeGame.Helpers
{
	public class ConwayCellProgressor
	{
		public virtual void StepCells(CellViewModel[] cells, int columns, int rows)
		{
			for (int i = 0; i < cells.Length; i++)
			{
			}

			for (int i = 0; i < cells.Length; i++)
				cells[i].IsLiving = cells[i].WillKeepLiving;
		}

		protected virtual int GetLivingNeighborCount(CellViewModel[] cells, int columns, int rows, int currentIndex)
		{
			int count = 0;

			return count;
		}
	}
}
