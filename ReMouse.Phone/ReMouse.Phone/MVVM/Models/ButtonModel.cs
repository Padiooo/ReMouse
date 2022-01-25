using ReMouse.Phone.Core.RepositoryData;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ReMouse.Phone.MVVM.Models
{
    /// <summary>
    /// Used by ButtonsPageViewModel.
    /// </summary>
    public class ButtonModel : ObservableObject
    {
        public ButtonData Data { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        public void Update()
        {
            OnPropertyChanged(nameof(Data));
        }
    }
}
