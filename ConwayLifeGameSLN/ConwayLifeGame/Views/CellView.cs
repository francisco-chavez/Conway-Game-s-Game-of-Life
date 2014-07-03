using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		public static readonly DependencyProperty AcceptsManualInputProperty;
		#endregion


		#region Attributes
		public bool IsAlive
		{
			get { return (bool) GetValue(IsAliveProperty); }
			set { SetValue(IsAliveProperty, value); }
		}

		public bool AcceptsManualInput
		{
			get { return GetAcceptsManualInput(this); }
			set { SetAcceptsManualInput(this, value); }
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

			AcceptsManualInputProperty = DependencyProperty.RegisterAttached(
				"AcceptsManualInput",
				typeof(bool),
				typeof(CellView),
				new FrameworkPropertyMetadata(
					true,
					FrameworkPropertyMetadataOptions.Inherits));
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
			if (AcceptsManualInput)
				this.IsAlive = !this.IsAlive;
		}
		#endregion


		#region Methods
		public static void SetAcceptsManualInput(UIElement element, bool value)
		{
			element.SetValue(AcceptsManualInputProperty, value);
		}

		public static bool GetAcceptsManualInput(UIElement element)
		{
			return (bool) element.GetValue(AcceptsManualInputProperty);
		}
		#endregion
	}
}
