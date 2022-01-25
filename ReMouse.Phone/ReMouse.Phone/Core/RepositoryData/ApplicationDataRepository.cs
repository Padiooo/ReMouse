
using Xamarin.Forms;

namespace ReMouse.Phone.Core.RepositoryData
{
    public class ApplicationDataRepository : RepositoryBase<ApplicationData>
    {
        public const string AppDataRepository = "appdata";

        public ApplicationDataRepository()
            : base(AppDataRepository)
        {
            //First time application is ran, set default values
            var items = GetAll();
            if (items.Count == 0)
                Reset();
        }

        public override void Reset()
        {
            var items = GetAll();
            items.Clear();
            ApplicationData data = new ApplicationData()
            {
                MouseSpeed = 15,
                Theme = ApplicationTheme.Light,
                IPAddress = "192.168.0.0",
                Custom_Background = Color.Black.ToHex(),
                Custom_Foreground = Color.White.ToHex()
            };
            items.Add(data);
            Save();
        }
    }
}
