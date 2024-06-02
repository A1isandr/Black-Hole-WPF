using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Black_Hole.MVVM.Models;
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
                _instance ??= new BlackHoleViewModel(new BlackHole());
                return _instance;
            }
        }

        #endregion

        #region Properties

        public BlackHole BlackHole { get; init; }

        #endregion

        #region Constructors

        private BlackHoleViewModel(BlackHole blackHole)
        {
            BlackHole = blackHole;
        }

        #endregion
    }
}
