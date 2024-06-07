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
using Black_Hole.Resources.Keys;
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

			ViewModel = BlackHoleViewModel.Instance;

			_sizeAnimation = (Storyboard)TryFindResource(AnimationResourcesKeys.BlackHoleSizeAnimationKey);
			_mouseDownAnimation = (Storyboard)TryFindResource(AnimationResourcesKeys.BlackHoleMouseDownAnimationKey);
			_mouseUpAnimation = (Storyboard)TryFindResource(AnimationResourcesKeys.BlackHoleMouseUpAnimationKey);

			_mouseUpAnimation.Completed += (_, _) => _sizeAnimation.Begin();

			this.WhenActivated(disposables =>
            {
                //this.OneWayBind(ViewModel,
                //        viewModel => viewModel.AccretionDiskRadius,
                //        view => view.AccretionDisk.Width)
                //    .DisposeWith(disposables);

                //this.OneWayBind(ViewModel,
                //        viewModel => viewModel.AccretionDiskRadius,
                //        view => view.AccretionDisk.Height)
                //    .DisposeWith(disposables);

                //this.OneWayBind(ViewModel,
                //        viewModel => viewModel.ParticleOrbit,
                //        view => view.PhotonOrbit.Width)
                //    .DisposeWith(disposables);

                //this.OneWayBind(ViewModel,
                //        viewModel => viewModel.ParticleOrbit,
                //        view => view.PhotonOrbit.Height)
                //    .DisposeWith(disposables);

                //this.OneWayBind(ViewModel,
                //        viewModel => viewModel.BlackHole.Rs,
                //        view => view.BlackHole.Height)
                //    .DisposeWith(disposables);

                //this.OneWayBind(ViewModel,
                //        viewModel => viewModel.BlackHole.Rs,
                //        view => view.BlackHole.Width)
                //    .DisposeWith(disposables);

      //          this.OneWayBind(ViewModel,
						//viewModel => viewModel.Start,
						//view => view.BottomLine.Y1)
						//.DisposeWith(disposables);

      //          this.OneWayBind(ViewModel,
      //                  viewModel => viewModel.Start,
      //                  view => view.BottomLine.Y2)
      //              .DisposeWith(disposables);

      //          this.OneWayBind(ViewModel,
      //                  viewModel => viewModel.End,
      //                  view => view.TopLine.Y1)
      //              .DisposeWith(disposables);

      //          this.OneWayBind(ViewModel,
      //                  viewModel => viewModel.End,
      //                  view => view.TopLine.Y2)
      //              .DisposeWith(disposables);

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
