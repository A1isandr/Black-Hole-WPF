using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;

namespace Black_Hole.MVVM.ViewModels
{
	public class BlackHoleViewModel : ReactiveObject
	{
        #region Singleton

        private static BlackHoleViewModel? _instance;

        public static BlackHoleViewModel Instance
        {
            get
            {
                _instance ??= new BlackHoleViewModel();
                return _instance;
            }
        }

        #endregion

        #region Properties

        public Vector2 Position { get; set; } = new((float)Application.Current.MainWindow!.ActualWidth, (float)Application.Current.MainWindow.ActualHeight);

        #endregion

        #region Constructors

        private BlackHoleViewModel()
        {
            Application.Current.MainWindow!.SizeChanged += (_, _) =>
            {
                Position = new Vector2((float)Application.Current.MainWindow!.ActualWidth, (float)Application.Current.MainWindow.ActualHeight);
            };
        }

        #endregion
    }
}
