using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

using Unv.ConwayLifeGame.ViewModels;


namespace Unv.ConwayLifeGame.Helpers
{
	/// <summary>
	/// This class is used to create and set up the CellViewModels and then
	/// populate the CellGridViewModel with said cells.
	/// </summary>
	public class CellFactory
	{
		#region Events
		public event EventHandler CellCreationFinished;
		#endregion


		#region Attributes
		private DispatcherTimer		m_timer;
		private CellGridViewModel	m_cellGridViewModel;
		private List<CellViewModel> m_cells;
		#endregion


		#region Constructors
		public CellFactory(CellGridViewModel cellGridViewModel)
		{
			if (cellGridViewModel == null)
				throw new ArgumentNullException("Cell Grid is missing");


			m_cellGridViewModel = cellGridViewModel;

			// I'm pre-sizing the list to 100, so that we'll spend
			// a bit less time resizing it when we start creating
			// new Cell View Models.
			m_cells		= new List<CellViewModel>(100); 

			// We'll be using a DispatcherTimer to insert the 
			// CellViewModels into the CellGridViewModel without
			// freezing the thread. It's very important not to 
			// freeze it because we'll be working in the UI 
			// thread, and freezing that MAY lead to the user 
			// feeling like the program is broken.
			// -FCT
			m_timer		= new DispatcherTimer(DispatcherPriority.Input);
			m_timer.Interval = TimeSpan.FromMilliseconds(20);
			m_timer.Tick += Timer_Tick;
		}
		#endregion


		#region Event Handlers
		/// <summary>
		/// Insert a few cells into the Cell Grid View Model.
		/// </summary>
		private void Timer_Tick(object sender, EventArgs e)
		{
			int index = m_cells.Count - 1;

			// Move a few cells over from the factory to the
			// Cell Grid View Model.
			for (int i = 0; i < 25 && index >= 0; i++, index--)
			{
				var cell = m_cells[index];
				m_cells.RemoveAt(index);
				m_cellGridViewModel.Cells.Add(cell);
			}

			// If there are no more cells to move over,
			// stop the timer and call anyone that's 
			// listening for the finished signal/event
			if (index < 0)
			{
				m_timer.Stop();
				if (CellCreationFinished != null)
					CellCreationFinished(this, null);
			}
		}
		#endregion


		#region Methods
		/// <summary>
		/// If the factory isn't currently working on creating a new set of cells for 
		/// the Cell Grid View Model, it will start a set of cells for it. These cells
		/// will be for the number of columns and rows at the time of the call. Once 
		/// Cell View Models have been created, they will be inserted into the Cell
		/// Grid View Model asynchronously by only inserting a few at a time.
		/// </summary>
		public void CreateCellsAsync()
		{
			if (m_timer.IsEnabled)
				return;

			int width	= m_cellGridViewModel.ColumnCount;
			int heigth	= m_cellGridViewModel.RowCount;

			m_cells.AddRange(
				from row in Enumerable.Range(0, heigth)
				from column in Enumerable.Range(0, width)
				select new CellViewModel() { Row = row, Column = column, IsLiving = false, WillKeepLiving = false });

			m_cells.Reverse();

			m_timer.Start();
		}
		#endregion
	}
}
