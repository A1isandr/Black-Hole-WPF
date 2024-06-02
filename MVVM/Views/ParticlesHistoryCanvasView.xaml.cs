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
using Black_Hole.MVVM.ViewModels;
using Black_Hole.Services;
using ReactiveUI;

namespace Black_Hole.MVVM.Views
{
    /// <summary>
    /// Interaction logic for ParticlesHistoryCanvasView.xaml
    /// </summary>
    public partial class ParticlesHistoryCanvasView : ReactiveUserControl<ParticlesHistoryCanvasViewModel>
    {
        public ParticlesHistoryCanvasView()
        {
            InitializeComponent();

            ViewModel = new ParticlesHistoryCanvasViewModel
                (
                    (ParticlesHistoryService)App.ServiceProvider!.GetService(typeof(IParticlesHistoryService))!
                );
            ParticlesHistoryCanvas.DataContext = ViewModel.ParticlesHistory;

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel,
                        viewModel => viewModel.ParticlesHistory,
                        view => view.ParticlesHistoryCanvas.ItemsSource)
                    .DisposeWith(disposables);
            });
        }
    }
}
