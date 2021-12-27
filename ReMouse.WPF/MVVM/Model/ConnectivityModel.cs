using ReMouse.WPF.Core.DataBinding;

namespace ReMouse.WPF.MVVM.Model
{
    public class ConnectivityModel : ObservableObject
    {
        private string _localName;
        public string LocalName
        {
            get { return _localName; }
            set
            {
                _localName = value;
                OnPropertyChanged();
            }
        }

        private string _remoteName;
        public string RemoteName
        {
            get { return _remoteName; }
            set
            {
                _remoteName = value;
                OnPropertyChanged();
            }
        }
    }
}
