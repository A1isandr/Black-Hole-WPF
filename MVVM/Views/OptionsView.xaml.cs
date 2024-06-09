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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Black_Hole.MVVM.ViewModels;
using Black_Hole.Resources.Keys;
using ReactiveUI;

namespace Black_Hole.MVVM.Views
{
    /// <summary>
    /// Interaction logic for OptionsView.xaml
    /// </summary>
    public partial class OptionsView : ReactiveUserControl<OptionsViewModel>
    {
        public readonly Storyboard OptionsFlyOutAnimation;
        public readonly Storyboard OptionsFlyInAnimation;

        public OptionsView()
        {
            InitializeComponent();

            ViewModel = OptionsViewModel.Instance;

            OptionsFlyOutAnimation = (Storyboard)TryFindResource(AnimationResourcesKeys.OptionsFlyOutAnimationKey);
            OptionsFlyInAnimation = (Storyboard)TryFindResource(AnimationResourcesKeys.OptionsFlyInAnimationKey);
        }
    }
}
