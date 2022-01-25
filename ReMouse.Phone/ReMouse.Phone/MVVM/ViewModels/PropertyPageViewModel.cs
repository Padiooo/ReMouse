using ReMouse.Phone.Core.RepositoryData;
using ReMouse.Phone.MVVM.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ReMouse.Phone.MVVM.ViewModels
{
    public class PropertyPageViewModel
    {
        private readonly List<ButtonData> buttonDatas;
        public ButtonDataModel Model { get; }

        public IAsyncCommand SaveCmd { get; }

        public string Title { get; }

        public PropertyPageViewModel(List<ButtonData> buttonDatas)
        {
            SaveCmd = new AsyncCommand(SaveAsync);

            this.buttonDatas = buttonDatas;

            if (buttonDatas.Count == 1)
            {
                Title = buttonDatas[0].FriendlyName;
                ButtonData data = new ButtonData();
                data.Copy(buttonDatas[0]);
                Model = new ButtonDataModel() { Data = data };
            }
            else
            {
                Title = "Buttons";

                ButtonData data = new ButtonData
                {
                    IsEnabled = true,
                    IsInBox = true,
                    Progress = buttonDatas[0].Progress
                };

                foreach (var bd in buttonDatas)
                {
                    data.IsEnabled &= bd.IsEnabled;
                    data.IsInBox &= bd.IsInBox;
                    data.Progress = bd.Progress == data.Progress ? data.Progress : 0.5f;
                }
                ButtonData previous = buttonDatas[0];
                if (buttonDatas.Any(bd =>
                {
                    bool res = bd.ColorMode != previous.ColorMode;
                    previous = bd;
                    return res;
                }))
                    data.ColorMode = ButtonData.ButtonColorMode.AppTheme;
                else
                    data.ColorMode = previous.ColorMode;

                if (!buttonDatas.Any(bd =>
                {
                    bool res = bd.Color != previous.Color;
                    previous = bd;
                    return res;
                }))
                    data.Color = previous.Color;

                Model = new ButtonDataModel() { Data = data };
            }
        }

        private async Task SaveAsync()
        {
            ButtonRepository buttonRepository = DependencyService.Get<ButtonRepository>();
            List<ButtonData> datas = buttonRepository.GetAll();

            foreach (var item in buttonDatas)
                datas.FirstOrDefault(d => d.Type == item.Type).Copy(Model.Data);

            buttonRepository.Save();

            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
