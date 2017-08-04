using System.ComponentModel;
using System.Windows.Input;

namespace Akademia_MS_Projekt
{
    class HomeViewModel : INotifyPropertyChanged
    {
        private bool _canExecute;
        private int _indexFromTextbox;
        private string _firstNameFromTextbox;
        private string _lastNameFromTextbox;
        private ICommand _clickAddStudent;

        public int IndexFromTextbox
        {
            get { return _indexFromTextbox; }
            set
            {
                _indexFromTextbox = value;
                NotifyPropertyChanged("IndexFromTextbox");
            }
        }
        public string FirstNameFromTextbox
        {
            get { return _firstNameFromTextbox; }
            set
            {
                _firstNameFromTextbox = value;
                NotifyPropertyChanged("FirstNameFromTextbox");
            }
        }
        public string LastNameFromTextbox
        {
            get { return _lastNameFromTextbox; }
            set
            {
                _lastNameFromTextbox = value;
                NotifyPropertyChanged("LastNameFromTextbox");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ClickAddStudent
        {
            get
            {
                return _clickAddStudent ?? (_clickAddStudent = new CommandHandler(() => AddStudent(), _canExecute));
            }
        }

        public void NotifyPropertyChanged(string _advancedFormat)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_advancedFormat));
        }

        public HomeViewModel()
        {
            IndexFromTextbox = 142536;
            FirstNameFromTextbox = "Rychu";
            LastNameFromTextbox = "Rychowski";
            if (IndexFromTextbox != 0 && FirstNameFromTextbox != null && LastNameFromTextbox != null) _canExecute = true;
        }

        void AddStudent()
        {
            Student someNewStudent = new Student(IndexFromTextbox, FirstNameFromTextbox, LastNameFromTextbox);
            Data.Students.Add(someNewStudent);
        }
    }
}
