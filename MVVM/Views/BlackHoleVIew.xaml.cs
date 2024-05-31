using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Black_Hole.MVVM.ViewModels;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace Black_Hole.MVVM.Views
{
	/// <summary>
	/// Interaction logic for BlackHoleView.xaml
	/// </summary>
	public partial class BlackHoleView : ReactiveUserControl<BlackHoleViewModel>
	{
		private readonly Storyboard _sizeAnimation;
		private readonly Storyboard _mouseDownAnimation;
		private readonly Storyboard _mouseUpAnimation;

		public BlackHoleView()
		{
			InitializeComponent();

			_sizeAnimation = (Storyboard)TryFindResource("SizeAnimation");
			_mouseDownAnimation = (Storyboard)TryFindResource("MouseDownAnimation");
			_mouseUpAnimation = (Storyboard)TryFindResource("MouseUpAnimation");

			_mouseUpAnimation.Completed += (_, _) => _sizeAnimation.Begin();

			this.WhenActivated(disposables =>
			{
				this
					.Events()
					.PreviewMouseDown
					.Subscribe(e =>
                    {
                        CaptureMouse();
						_mouseDownAnimation.Begin();
					})
					.DisposeWith(disposables);

				this
					.Events()
					.PreviewMouseUp
					.Subscribe(e =>
					{
						ReleaseMouseCapture();
						_mouseUpAnimation.Begin();
					})
					.DisposeWith(disposables);
			});
		}

		private void BlackHoleView_OnLoaded(object sender, RoutedEventArgs e)
		{
			_sizeAnimation.Begin();
		}
	}
}
