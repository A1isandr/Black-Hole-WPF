using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Black_Hole.Services;
using ReactiveUI;

namespace Black_Hole.MVVM.ViewModels
{
    public class OptionsViewModel : ReactiveObject
    {
        #region Singleton

        private static OptionsViewModel? _instance;

        public static OptionsViewModel Instance
        {
            get
            {
                _instance ??= new OptionsViewModel();
               
                return _instance;
            }
        }

        #endregion

        #region Properties

        public bool IsOpen { get; set; }

        #endregion
    }
}
