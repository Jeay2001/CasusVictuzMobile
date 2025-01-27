using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CasusVictuzMobile.MVVM.ViewModel
{
    public partial class MapViewModel :ObservableObject
    {
        [ObservableProperty]
        private Models.Location location;
        public MapViewModel(int locationId)
        {
                     
        }
    }
}
