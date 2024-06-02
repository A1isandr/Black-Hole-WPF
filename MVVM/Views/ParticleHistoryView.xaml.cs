using System;
using System.Collections.Generic;
using System.Linq;
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
using ReactiveUI;

namespace Black_Hole.MVVM.Views
{
    /// <summary>
    /// Interaction logic for ParticleHistoryView.xaml
    /// </summary>
    public partial class ParticleHistoryView : ReactiveUserControl<ParticleHistoryViewModel>
    {
        public ParticleHistoryView()
        {
            InitializeComponent();
        }
    }
}
