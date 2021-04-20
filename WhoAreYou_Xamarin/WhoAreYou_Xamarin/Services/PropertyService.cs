using Xamarin.Forms;

namespace WhoAreYou_Xamarin.Services
{
    class PropertyService
    {
        public object Read(string key)
        {
            
            if(App.Current.Properties.ContainsKey(key))
            {
                object value;

                App.Current.Properties.TryGetValue(key, out value);

                return value;
            }

            return null;            
        }

        public void Write(string key, object value)
        {
            if(App.Current.Properties.ContainsKey(key))
            {
                App.Current.Properties[key] = value;
            }

            else
            {
                App.Current.Properties.Add(key, value);
            }
        }

        public void Delete(string key)
        {
            if (App.Current.Properties.ContainsKey(key))
            {
                App.Current.Properties.Remove(key);
            }

        }
    }
}
