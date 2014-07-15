

namespace Unv.ConwayLifeGame.Model
{
	/// <summary>
	/// This holds various exclusive states that the
	/// cell grid can be in at any point of time.
	/// </summary>
	public enum CellGridState
	{
		SettingInitialGeneration	= 0,
		ManualProgression			= 1,
		AutoProgression				= 2,
		LoadingCells				= 3
	}
}
