using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.ComponentModel;
using System.Numerics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Akademia_MS_Projekt
{
    public class FilterViewModel : INotifyPropertyChanged
    {
        private readonly BackgroundWorker worker = new BackgroundWorker();
        private double _U1;
        private double _R1;
        private double _R2;
        private int _Fmin;
        private int _Fmax;
        private double _L;
        private double _C;
        private Student _selectedStudent;
        private bool _canExecute;

        public SeriesCollection Dataseries1 { get; set; }
        public SeriesCollection Dataseries2 { get; set; }
        public SeriesCollection Dataseries3 { get; set; }

        public double U1
        {
            get { return _U1; }
            set
            {
                _U1 = value;
                NotifyPropertyChanged("U1");
            }
        }
        public double R1
        {
            get { return _R1; }
            set
            {
                _R1 = value;
                NotifyPropertyChanged("R1");
            }
        }
        public double R2
        {
            get { return _R2; }
            set
            {
                _R2 = value;
                NotifyPropertyChanged("R2");
            }
        }
        public int Fmin
        {
            get { return _Fmin; }
            set
            {
                _Fmin = value;
                NotifyPropertyChanged("Fmin");
            }
        }
        public int Fmax
        {
            get { return _Fmax; }
            set
            {
                _Fmax = value;
                NotifyPropertyChanged("Fmax");
            }
        }
        public double L
        {
            get { return _L; }
            set
            {
                _L = value;
                NotifyPropertyChanged("L");
            }
        }
        public double C
        {
            get { return _C; }
            set
            {
                _C = value;
                NotifyPropertyChanged("C");
            }
        }
        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                NotifyPropertyChanged("SelectedStudent");
                if(_selectedStudent != null)
                {
                    Student tempStudent = SelectedStudent;
                    R1 = tempStudent.R1;
                    R2 = tempStudent.R2;
                    L = tempStudent.L;
                    C = tempStudent.C;
                }
            }
        }

        
        public FilterViewModel()
        {
            _canExecute = true;

            U1 = 230;
            Fmin = 10;
            Fmax = 500;

            var dataConfig = Mappers.Xy<DataModelForChart>()
            .X(dataModel => dataModel.Value_x)
            .Y(dataModel => dataModel.Value_y);

            #region Inicjalizacja Dataseries

            Dataseries1 = new SeriesCollection(dataConfig)
            {
                new LineSeries
                {
                Title = "Widmo amplitudowe U2/U1 w funkcji częstotliwości",
                LineSmoothness = 1, //0: straight lines, 1: really smooth lines
                PointGeometry=null,
                Fill = Brushes.Transparent,

                Values = new ChartValues<DataModelForChart>
                {
                }
                }
            };
            Dataseries2 = new SeriesCollection(dataConfig)
            {
                 new LineSeries
                {
                Title = "Widmo fazowe U2/U1 w funkcji częstotliwości",
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometry=null,
                Fill = Brushes.Transparent,
                Values = new ChartValues<DataModelForChart>
                {
                }
                }
            };
            Dataseries3 = new SeriesCollection(dataConfig)
            {
                new LineSeries
                {
                Title = "Przebieg wartości prądu zasilającego w funkcji częstotliwości",
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometry=null,
                Fill = Brushes.Transparent,
                Values = new ChartValues<DataModelForChart>
                {
                }
                }
            };
            #endregion
        }

        #region Notyfikacje

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

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            worker.WorkerSupportsCancellation = true;
            
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            else
            {

                for (Data.Fakt = Data.F1; Data.Fakt <= Data.F2; Data.Fakt++)
                {
                    Complex Xl_temp = new Complex(0, (2.0 * 3.14159265 * Data.Fakt * L/1000));
                    Complex Xc_temp = new Complex(0, (-1 / (2.0 * 3.14159265 * Data.Fakt * C/1000000)));
                    Complex Z_z = new Complex(0, 0); Z_z = R1 + 1 / ((1 / Xc_temp) + (1 / R2) + (1 / Xl_temp));
                    Complex U1_z = new Complex(Data.U1, 0);
                    Complex I1_z = new Complex(0, 0); I1_z = U1_z / (Z_z);
                    Complex I3_z = new Complex(0, 0); I3_z = I1_z * ((Xc_temp * R2) / (Xc_temp + R2)) / (((Xc_temp * R2) / (Xc_temp + R2)) + Xl_temp);
                    Complex U2_z = new Complex(0, 0); U2_z = I3_z * Xl_temp;
                    Complex H_jw_z = new Complex(0, 0); H_jw_z = U2_z / (1.0 * U1_z);

                    Dataseries1[0].Values.Add(new DataModelForChart { Value_x = Data.Fakt, Value_y = Complex.Abs(H_jw_z) });
                    Dataseries2[0].Values.Add(new DataModelForChart { Value_x = Data.Fakt, Value_y = (H_jw_z.Phase * 180 / Math.PI) });
                    Dataseries3[0].Values.Add(new DataModelForChart { Value_x = Data.Fakt, Value_y = Complex.Abs(I1_z) });

                    if (Data.Fakt == Data.F2) worker.CancelAsync(); // problemy z ostatnim punktem wykresu, dlatego taki dziwny zabieg
                }
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
            Data.F1 = Fmin;
            Data.F2 = Fmax;

        }

        public void Drawit()
        {
            if (SelectedStudent != null)
            {
                Student tempStudent = SelectedStudent;
                R1 = tempStudent.R1;
                R2 = tempStudent.R2;
                L = tempStudent.L;
                C = tempStudent.C;
            }

            if (C >= 0 && L >= 0 && Fmin >= 0 && Fmax > 0 && R1 >= 0 && R2 >= 0 && U1 >= 0 && Fmax > Fmin)
            {
                if (!worker.IsBusy)
                {
                    Dataseries1[0].Values.Clear();
                    Dataseries2[0].Values.Clear();
                    Dataseries3[0].Values.Clear();
                    UpdateData();

                    worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
                    worker.RunWorkerAsync();
                    ChartsVisibility = Visibility.Visible;
                }
            }
        }
    }
}
