

namespace Unv.ConwayLifeGame.ViewModels
{
	/// <summary>
	/// This is the base class for all ViewModels in the application.
	/// </summary>
	/// <remarks>
	/// I'm inheriting from INPC because it cuts down on the number of
	/// implementations of the INotifyPropertyChanged interface.Sometimes 
	/// I'll use the INPC as the base for my Model classes, and have a
	/// ViewModel base lets me insert necessary data/logic for all
	/// ViewModels while keeping that code out of the Models. For this
	/// application, this set-up is overkill, but I'm keeping just in 
	/// case I ended up needing it.
	/// -FCT
	/// </remarks>
	public abstract class ViewModel
		: INPC
	{
	}
}
