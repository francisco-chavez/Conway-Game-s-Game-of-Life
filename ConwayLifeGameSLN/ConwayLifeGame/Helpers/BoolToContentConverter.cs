using System;
using System.Globalization;
using System.Windows.Data;


namespace Unv.ConwayLifeGame.Helpers
{
	/// <summary>
	/// This converter class lets you convert a Nullable&lt;bool&gt;'s values
	/// into any object within a binding.
	/// </summary>
	public class BoolToContentConverter
		: IValueConverter
	{
		#region Properties
		/// <summary>
		/// Gets or sets the content object to return when
		/// the given value to convert is True.
		/// </summary>
		public object TrueContent	{ get; set; }

		/// <summary>
		/// Gets or sets the content object to return when
		/// the given value to convert is False.
		/// </summary>
		public object FalseContent	{ get; set; }

		/// <summary>
		/// Gets or sets the content object to return when
		/// the given value to convert is either Null or
		/// unable to be converted to a boolean value.
		/// </summary>
		public object NullContent	{ get; set; }
		#endregion


		#region Constructors
		public BoolToContentConverter()
		{
			this.TrueContent	= "True";
			this.FalseContent	= "False";
			this.NullContent	= "Null";
		}
		#endregion


		#region Methods
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var boolean = value as bool?;

			return 
				boolean.HasValue 
				? (boolean.Value ? TrueContent : FalseContent) 
				: NullContent;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
