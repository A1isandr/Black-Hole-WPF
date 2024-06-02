using System.Reactive.Disposables;
using System.Text;
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
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using RegExp2DFA.Helpers;

namespace Black_Hole
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
	{
		public MainWindow()
		{
			InitializeComponent();

			ViewModel = new MainWindowViewModel();

			this.WhenActivated(disposables =>
			{
                Header
                    .Events()
                    .MouseDown
                    .Subscribe(e =>
                    {
                        if (e.ChangedButton != MouseButton.Left) return;

                        if (WindowState == WindowState.Maximized)
                        {
                            var mouseWindowRelativeCoords = Mouse.GetPosition(this);

                            Left = mouseWindowRelativeCoords.X;
                            Top = mouseWindowRelativeCoords.Y - 5;

                            WindowState = WindowState.Normal;
                        }

                        DragMove();
                    })
                    .DisposeWith(disposables);

                CloseWindowButton
                    .Events()
                    .Click
                    .Subscribe(e => Close())
                    .DisposeWith(disposables);

                MaxWindowButton
                    .Events()
                    .Click
                    .Subscribe(e =>
                    {
                        switch (WindowState)
                        {
                            case WindowState.Maximized:
                            {
                                WindowState = WindowState.Normal;

                                var resource =
                                    Application.Current.FindResource(IconResourcesKeys.WindowMaximizeIconKey);

                                if (resource is not null)
                                {
                                    MaxWindowButton.Content = ((GeometryDrawing)resource).Geometry;
                                }

                                break;
                            }
                            case WindowState.Normal:
                            {
                                WindowState = WindowState.Maximized;

                                var resource = Application.Current.FindResource(IconResourcesKeys.WindowRestoreIconKey);

                                if (resource is not null)
                                {
                                    MaxWindowButton.Content = ((GeometryDrawing)resource).Geometry;
                                }

                                break;
                            }
                            case WindowState.Minimized:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException(nameof(e.Source));
                        }
                    })
                    .DisposeWith(disposables);

                MinWindowButton
                    .Events()
                    .Click
                    .Subscribe(e => WindowState = WindowState.Minimized)
                    .DisposeWith(disposables);
            });
		}
	}
}