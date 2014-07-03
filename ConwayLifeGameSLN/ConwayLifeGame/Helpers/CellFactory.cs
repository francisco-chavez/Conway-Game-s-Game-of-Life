using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

using Unv.ConwayLifeGame.ViewModels;


namespace Unv.ConwayLifeGame.Helpers
{
	public class CellFactory
	{
		#region Events
		public event EventHandler CellCreationFinished;
		#endregion


		#region Attributes
		private DispatcherTimer		m_timer;
		private CellGridViewModel	m_cellGridViewModel;
		private List<CellViewModel> m_cells;
		private Random				m_randomGen;
		#endregion


		#region Constructors
		public CellFactory(CellGridViewModel cellGridViewModel)
		{
			if (cellGridViewModel == null)
				throw new ArgumentNullException("Cell Grid is missing");


			m_cellGridViewModel = cellGridViewModel;

			m_cells		= new List<CellViewModel>(100);
			m_randomGen = new Random();

			m_timer		= new DispatcherTimer(DispatcherPriority.Input);
			m_timer.Interval = TimeSpan.FromMilliseconds(25);
			m_timer.Tick += Timer_Tick;
		}
		#endregion


		#region Event Handlers
		void Timer_Tick(object sender, EventArgs e)
		{
			int index = m_cells.Count - 1;

			for (int i = 0; i < 25 && index >= 0; i++, index--)
			{
				var cell = m_cells[index];
				m_cells.RemoveAt(index);
				m_cellGridViewModel.Cells.Add(cell);
			}

			if (index < 0)
			{
				m_timer.Stop();
				if (CellCreationFinished != null)
					CellCreationFinished(this, null);
			}
		}
		#endregion


		#region Methods
		public void CreateCells()
		{
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
