using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace Unv.ConwayLifeGame.Views
{
	public class CellView
		: Button
	{
		#region Attributes
		public static readonly DependencyProperty IsAliveProperty;
		#endregion


		#region Attributes
		public bool IsAlive
		{
			get { return (bool) GetValue(IsAliveProperty); }
			set { SetValue(IsAliveProperty, value); }
		}
		#endregion


		#region Constructors
		static CellView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(
				typeof(CellView),
				new FrameworkPropertyMetadata(typeof(CellView)));


			IsAliveProperty = DependencyProperty.Register(
				"IsAlive",
				typeof(bool),
				typeof(CellView),
				new FrameworkPropertyMetadata(false));
		}

		public CellView()
		{
			this.Click += Cell_Click;

			Binding b = new Binding();
			b.Path = new PropertyPath("IsLiving");
			b.Mode = BindingMode.TwoWay;
			b.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			this.SetBinding(CellView.IsAliveProperty, b);
		}
		#endregion


		#region Event Handlers
		void Cell_Click(object sender, RoutedEventArgs e)
		{
			this.IsAlive = !this.IsAlive;
		}
		#endregion
	}
}
