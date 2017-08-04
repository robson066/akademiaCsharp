using System;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace Akademia_MS_Projekt
{
    public class AmplifierViewModel : INotifyPropertyChanged
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private double _U1;
        private double _U2min;
        private double _U2max;
        private int _R3;
        private int _R4;
        private int _R5;
        private int _R6;
        private double Uout;
        private Student _selectedStudent;
        private bool _canExecute;

        public SeriesCollection Dataseries1 { get; set; }

        public double U1
        {
            get { return _U1; }
            set
            {
                _U1 = value;
                NotifyPropertyChanged("U1");
            }
        }
        public double U2min
        {
            get { return _U2min; }
            set
            {
                _U2min = value;
                NotifyPropertyChanged("U2min");
            }
        }
        public double U2max
        {
            get { return _U2max; }
            set
            {
                _U2max = value;
                NotifyPropertyChanged("U2max");
            }
        }
        public int R3
        {
            get { return _R3; }
            set
            {
                _R3 = value;
                NotifyPropertyChanged("R3");
            }
        }
        public int R4
        {
            get { return _R4; }
            set
            {
                _R4 = value;
                NotifyPropertyChanged("R4");
            }
        }
        public int R5
        {
            get { return _R5; }
            set
            {
                _R5 = value;
                NotifyPropertyChanged("R5");
            }
        }
        public int R6
        {
            get { return _R6; }
            set
            {
                _R6 = value;
                NotifyPropertyChanged("R6");
            }
        }
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                NotifyPropertyChanged("SelectedStudent");
                if (_selectedStudent != null)
                {
                    Student tempStudent = SelectedStudent;
                    R3 = tempStudent.R2;
                    R4 = tempStudent.R3;
                    R5 = tempStudent.R4;
                    R6 = tempStudent.R5;
                }
            }
        }

        #region Binding

        private ICommand _clickDrawit;

        public ICommand ClickDrawit
        {
            get
            {
                return _clickDrawit ?? (_clickDrawit = new CommandHandler(() => Drawit(), _canExecute));
            }
        }

        public void NotifyPropertyChanged(string _advancedFormat)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_advancedFormat));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private Action<PropertyChangedEventArgs> RaisePropertyChanged()
        {
            return args => PropertyChanged?.Invoke(this, args);
        }
        private Visibility _chartsVisibility = Visibility.Hidden;

        public Visibility ChartsVisibility
        {
            get { return _chartsVisibility; }
            set
            {
                _chartsVisibility = value;
                NotifyPropertyChanged("ChartsVisibility");
            }
        }

        #endregion

        public AmplifierViewModel()
        {
            _canExecute = true;
            
            U1 = 230;
            U2min = 10;
            U2max = 120;

            var dataConfig = Mappers.Xy<DataModelForChart>()
            .X(dataModel => dataModel.Value_x)
            .Y(dataModel => dataModel.Value_y);

            Dataseries1 = new SeriesCollection(dataConfig)
            {
                new LineSeries
                {
                Title = "Przebieg napięcia wejściowe względem napięcia wyjściowego",
                LineSmoothness = 1, //0: straight lines, 1: really smooth lines
                PointGeometry=null,
                Fill = Brushes.Transparent,

                Values = new ChartValues<DataModelForChart>
                {
                }
                }
            };


        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            worker.WorkerSupportsCancellation = true;
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            else

                for (Data.Uakt = Data.U2min; Data.Uakt <= Data.U2max; Data.Uakt++)
                {
                    double Rz1 = (R3 + R4) * R6;
                    double Rz2 = (R5 + R6) * R3;
                    double Rz = Rz1 / Rz2;
                    Uout = Rz * Data.Uakt - Data.U1 * (R4 / R3);

                    Dataseries1[0].Values.Add(new DataModelForChart { Value_x = Data.Uakt, Value_y = Uout });

                    if (Data.Uakt == Data.U2max) worker.CancelAsync();
                }
        }

        private void Cancelworker()
        {
            if (worker.WorkerSupportsCancellation)
            {
                worker.CancelAsync();
            }
        }

        public void UpdateData()
        {
            Data.U1 = U1;
            Data.U2min = U2min;
            Data.U2max = U2max;
        }

        public void Drawit()
        {
            if (R3 > 0 && R4 > 0 && R5 > 0 && R6 > 0 && U1 >= 0 && U2min >= 0 && U2max >= 0 && U2max > U2min)
            {
                if (!worker.IsBusy)
                {
                    Dataseries1[0].Values.Clear();
                    UpdateData();

                    worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
                    worker.RunWorkerAsync();
                    ChartsVisibility = Visibility.Visible;
                }
            }
        }
    }
}
