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
using Black_Hole.MVVM.Models;
using Black_Hole.MVVM.ViewModels;
using Black_Hole.Resources.Keys;
using Black_Hole.Resources.Uri;
using ReactiveMarbles.ObservableEvents;
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

            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel,
                        viewModel => viewModel.C,
                        view => view.SpeedOfLightTextBox.Text)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        viewModel => viewModel.G,
                        view => view.GravitationalConstTextBox.Text)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        viewModel => viewModel.DeltaTime,
                        view => view.DeltaTimeTextBox.Text)
                    .DisposeWith(disposables);

                this.Bind(ViewModel,
                        viewModel => viewModel.BlackHoleMass,
                        view => view.BlackHoleMassTextBox.Text)
                    .DisposeWith(disposables);

                ThemesCombobox
                    .Events()
                    .SelectionChanged
                    .Subscribe(e =>
                    {
                        var app = (App)Application.Current;

                        var blackHoleThemeTextKey = (string)TryFindResource(TextResourcesKeys.BlackHoleThemeTextKey);
                        var whiteHoleThemeTextKey = (string)TryFindResource(TextResourcesKeys.WhiteHoleThemeTextKey);

                        if (((TextBlock)ThemesCombobox.SelectedItem).Text == blackHoleThemeTextKey)
                        {
                            app.ChangeTheme(ThemeResourceDictionariesUri.BlackHoleThemeUri);
                        }
                        else if (((TextBlock)ThemesCombobox.SelectedItem).Text == whiteHoleThemeTextKey)
                        {
                            app.ChangeTheme(ThemeResourceDictionariesUri.WhiteHoleThemeUri);
                        }
                    })
                    .DisposeWith(disposables);

                LanguagesCombobox
                    .Events()
                    .SelectionChanged
                    .Subscribe(e =>
                    {
                        var app = (App)Application.Current;

                        switch (((TextBlock)LanguagesCombobox.SelectedItem).Text)
                        {
                            case "English":
                                app.ChangeLanguage(LanguageResourceDictionariesUri.EnglishUsLanguageUri);
                                break;
                            case "Русский":
                                app.ChangeLanguage(LanguageResourceDictionariesUri.RussianLanguageUri);
                                break;
                        }
                    })
                    .DisposeWith(disposables);
            });
        }
    }
}
