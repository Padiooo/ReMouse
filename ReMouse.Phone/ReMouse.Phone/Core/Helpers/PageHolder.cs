using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ReMouse.Phone.Core.Helpers
{
    public class PageHolder
    {
        public const string ResourceKey = "PageHolder";

        private readonly Dictionary<Type, Lazy<Page>> _pages = new Dictionary<Type, Lazy<Page>>();

        public PageHolder() { }

        public void RegisterPage<T>() where T : Page, new()
        {
            Type key = typeof(T);
            if (!_pages.ContainsKey(key))
            {
                Lazy<Page> page = new Lazy<Page>(() => new T());
                _pages.Add(key, page);
            }
        }

        public T GetPage<T>() where T : Page, new()
        {
            Type key = typeof(T);
            if (!_pages.TryGetValue(key, out Lazy<Page> page))
            {
                page = new Lazy<Page>(() => new T());
                _pages.Add(key, page);
            }

            return (T)page.Value;
        }
    }
}
