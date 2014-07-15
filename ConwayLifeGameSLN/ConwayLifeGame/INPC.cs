using System.ComponentModel;


namespace Unv.ConwayLifeGame
{
	/// <summary>
	/// This class servers as a base class for the implementation of
	/// the INotifyPropertyChanged interface.
	/// </summary>
	public abstract class INPC
		: INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// This method is used raise the PropertyChanged event.
		/// </summary>
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
