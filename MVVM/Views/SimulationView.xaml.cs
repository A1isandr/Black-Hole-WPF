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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Black_Hole.MVVM.Models;
using Black_Hole.MVVM.ViewModels;
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
                this.BindCommand(ViewModel,
                        viewModel => viewModel.ResetSimulationCommand,
                        view => view.BlackHole,
                        nameof(BlackHole.MouseLeftButtonUp))
                    .DisposeWith(disposables);

                ClickRectangle
                    .Events()
                    .MouseLeftButtonUp
                    .Subscribe(e =>
                    {
                        var point = e.GetPosition(this);

                        // Небольшой трюк, чтобы курсор указывал на центр эллипса при появлении.
                        point.X -= 10;
                        point.Y -= 10;
                        ViewModel.SpawnParticleCommand.Execute(point).Subscribe();
                    });
            });
        }

        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel!.StartStopSimulationCommand.Execute().Subscribe();
        }
    }
}
