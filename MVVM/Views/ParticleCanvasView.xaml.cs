using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
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
using Black_Hole.Services;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;

namespace Black_Hole.MVVM.Views
{
    /// <summary>
    /// Interaction logic for ParticleCanvasView.xaml
    /// </summary>
    public partial class ParticleCanvasView : ReactiveUserControl<ParticleCanvasViewModel>
    {
        public ParticleCanvasView()
        {
            InitializeComponent();

            ViewModel = new ParticleCanvasViewModel
            (
                (ParticlesService)App.ServiceProvider!.GetService(typeof(IParticlesService))!
            );

            // Костыль, но я так и не придумал, как забиндить положение частиц "по-реактивному".
            ParticlesCanvas.DataContext = ViewModel.Particles;

            this.WhenActivated(disposables =>
            {
                this.OneWayBind(ViewModel,
                        viewModel => viewModel.Particles,
                        view => view.ParticlesCanvas.ItemsSource)
                    .DisposeWith(disposables);
            });
        }
    }
}
