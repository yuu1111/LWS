using LWS.Properties;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace LWS
{
    class ResourceManager : INotifyPropertyChanged
    {
        #region Properties

        public static ResourceManager Current { get; } = new ResourceManager();

        public Resources Resources { get; } = new Resources();
        
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public void ChangeCulture(string name)
        {
            Resources.Culture = CultureInfo.GetCultureInfo(name);
            RaisePropertyChanged("Resources");
        }
    }
}
