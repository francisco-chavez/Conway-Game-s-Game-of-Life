
using Unv.ConwayLifeGame.ViewModels;


namespace Unv.ConwayLifeGame.Helpers
{
	/// <summary>
	/// This class will update the living state of all cells
	/// within a CellGridViewModel.
	/// </summary>
	public class ConwayCellProgressor
	{
		public virtual void StepCells(CellViewModel[] cells, int columns, int rows)
		{
			for (int i = 0; i < cells.Length; i++)
			{
				int livingNeighborCount = GetLivingNeighborCount(cells, columns, rows, i);

				switch (livingNeighborCount)
				{
				case 2:
					cells[i].WillKeepLiving = cells[i].IsLiving;
					break;

				case 3:
					cells[i].WillKeepLiving = true;
					break;

				default:
					cells[i].WillKeepLiving = false;
					break;
				}
			}

			for (int i = 0; i < cells.Length; i++)
				cells[i].IsLiving = cells[i].WillKeepLiving;
		}

		protected virtual int GetLivingNeighborCount(CellViewModel[] cells, int columns, int rows, int currentIndex)
		{
			int count = 0;

			// Find our current location in the grid
			int currentRow		= currentIndex / columns;
			int currentColumn	= currentIndex - (currentRow * columns);

			// Find out which directions from our current position
			// have neighbors
			bool hasNeighborsAbove		= currentRow != 0;
			bool hasNeighborsBelow		= currentRow != rows - 1;
			bool hasNeighborsOnLeft		= currentColumn != 0;
			bool hasNeighborsOnRight	= currentColumn != columns - 1;

			// Find the indeces of the neighbors
			int topIndex		= currentIndex - columns;
			int topLeftIndex	= topIndex - 1;
			int topRightIndex	= topIndex + 1;

			int leftIndex		= currentIndex - 1;
			int rightIndex		= currentIndex + 1;

			int bottomIndex		= currentIndex + columns;
			int bottomLeftIndex = bottomIndex - 1;
			int bottomRightIndex = bottomIndex + 1;

			// Check row above for living neighbors
			if (hasNeighborsAbove)
			{
				if (hasNeighborsOnLeft && cells[topLeftIndex].IsLiving)
					count++;

				if (cells[topIndex].IsLiving)
					count++;

				if (hasNeighborsOnRight && cells[topRightIndex].IsLiving)
					count++;
			}

			// Check current row for living neighbors
			if (hasNeighborsOnLeft && cells[leftIndex].IsLiving)
				count++;

			if (hasNeighborsOnRight && cells[rightIndex].IsLiving)
				count++;

			// Check row below for living neighbors
			if (hasNeighborsBelow)
			{
				if (hasNeighborsOnLeft && cells[bottomLeftIndex].IsLiving)
					count++;

				if (cells[bottomIndex].IsLiving)
					count++;

				if (hasNeighborsOnRight && cells[bottomRightIndex].IsLiving)
					count++;
			}

			// Return count of living neighbors
			return count;
		}
	}
}
