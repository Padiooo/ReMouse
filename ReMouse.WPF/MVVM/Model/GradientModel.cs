using ReMouse.WPF.Core.DataBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ReMouse.WPF.MVVM.Model
{
    public class GradientModel : ObservableObject
    {
        private Color _color1;
        public Color Color1
        {
            get => _color1;
            set
            {
                _color1 = value;
                OnPropertyChanged();
            }
        }

        private Color _color2;
        public Color Color2
        {
            get => _color2;
            set
            {
                _color2 = value;
                OnPropertyChanged();
            }
        }
    }
}
