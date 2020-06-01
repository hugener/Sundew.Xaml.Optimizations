using System.ComponentModel;
using System.Runtime.CompilerServices;
using Sundew.Xaml.Optimizations.ApiDesigner.Wpf.Annotations;

namespace Sundew.Xaml.Optimizations.ApiDesigner.Wpf
{
    public class VM : INotifyPropertyChanged
    {
        private string name;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => this.name;
            set
            {
                this.name = value;
                this.OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}