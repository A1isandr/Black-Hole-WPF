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
	/// Interaction logic for MainView.xaml
	/// </summary>
	public partial class MainView : ReactiveUserControl<MainViewModel>
	{
		public MainView()
		{
			InitializeComponent();

			ViewModel = MainViewModel.Instance;

            this.WhenActivated(disposables =>
            {
                this.BindCommand(ViewModel,
                        viewModel => viewModel.ResetSimulation,
                        view => view.BlackHole,
                        nameof(BlackHole.MouseLeftButtonUp))
                    .DisposeWith(disposables);
            });
        }

        private async void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(Simulation.Run);
        }
    }
}
