using System;
using System.ComponentModel;

namespace Akademia_MS_Projekt
{
    public class AmplifierTaskDialogViewModel : INotifyPropertyChanged
    {
        private string _contentOfTask;

        public string ContentOfTask
        {
            get { return Data.ContentAmplifierTask; }
            set
            {
                this.MutateVerbose(ref _contentOfTask, value, RaisePropertyChanged());
                Data.ContentAmplifierTask = _contentOfTask;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
    }
}
