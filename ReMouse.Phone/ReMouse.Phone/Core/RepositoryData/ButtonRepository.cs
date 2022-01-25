using ReMouse.Shared.Packets;
using System;

namespace ReMouse.Phone.Core.RepositoryData
{
    public class ButtonRepository : RepositoryBase<ButtonData>
    {
        public const string Name = "buttons";

        public ButtonRepository() : base(Name)
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
            foreach (var type in (ButtonType[])Enum.GetValues(typeof(ButtonType)))
            {
                ButtonData data = new ButtonData
                {
                    Type = type
                };

                items.Add(data);
            }
            Save();
        }
    }
}
