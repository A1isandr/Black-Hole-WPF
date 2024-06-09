using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
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
using Black_Hole.MVVM.Models;
using Black_Hole.MVVM.ViewModels;
using Black_Hole.Resources.Keys;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace Black_Hole.MVVM.Views
{
	/// <summary>
	/// Interaction logic for SimulationView.xaml
	/// </summary>
	public partial class SimulationView : ReactiveUserControl<SimulationViewModel>
	{
		public SimulationView()
		{
			InitializeComponent();

			ViewModel = SimulationViewModel.Instance;



            this.WhenActivated(disposables =>
            {
                //this.BindCommand(ViewModel,
                //        viewModel => viewModel.ResetSimulationCommand,
                //        view => view.BlackHoleView,
                //        nameof(BlackHoleView.MouseLeftButtonUp))
                //    .DisposeWith(disposables);

                ClickRectangle
                    .Events()
                    .MouseLeftButtonUp
                    .Subscribe(e =>
                    {
                        if (OptionsViewModel.Instance.IsOpen)
                        {
                            OptionsViewModel.Instance.IsOpen = false;
                            OptionsView.OptionsFlyInAnimation.Begin();
                        }

                        var point = e.GetPosition(this);

                        // Небольшой трюк, чтобы курсор указывал на центр эллипса при появлении.
                        point.X -= 10;
                        point.Y -= 10;
                        ViewModel.SpawnParticleCommand.Execute(point).Subscribe();
                    });

                DashboardView.SettingsButton
                    .Events()
                    .Click
                    .Subscribe(e =>
                    {
                        if (OptionsViewModel.Instance.IsOpen)
                        {
                            OptionsViewModel.Instance.IsOpen = false;
                            OptionsView.OptionsFlyInAnimation.Begin();
                        }
                        else
                        {
                            OptionsViewModel.Instance.IsOpen = true;
                            OptionsView.OptionsFlyOutAnimation.Begin();
                        }
                    })
                    .DisposeWith(disposables);

                BlackHoleView.BlackHole
                    .Events()
                    .MouseLeftButtonUp
                    .Subscribe(e =>
                    {
                        if (OptionsViewModel.Instance.IsOpen)
                        {
                            OptionsViewModel.Instance.IsOpen = false;
                            OptionsView.OptionsFlyInAnimation.Begin();
                        }
                        else
                        {
                            OptionsViewModel.Instance.IsOpen = true;
                            OptionsView.OptionsFlyOutAnimation.Begin();
                        }
                    });
            });
        }

        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel!.StartStopSimulationCommand.Execute().Subscribe();
        }
    }
}
