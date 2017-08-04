using System;
using System.ComponentModel;

namespace Akademia_MS_Projekt
{
    public class FilterTaskDialogViewModel: INotifyPropertyChanged
    {
        private string _contentOfTask;

        public string ContentOfTask
        {
            get { return Data.ContentFilterTask; }
            set
            {
                this.MutateVerbose(ref _contentOfTask, value, RaisePropertyChanged());
                Data.ContentFilterTask = _contentOfTask;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }

    }
}
