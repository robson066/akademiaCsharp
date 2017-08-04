using System;
using System.Windows.Controls;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Events;
using LiveCharts.Wpf;

namespace Akademia_MS_Projekt
{
    /// <summary>
    /// Interaction logic for Filter.xaml
    /// </summary>
    public partial class Filter : UserControl
    {
        public Filter()
        {
            InitializeComponent();
            DataContext = new FilterViewModel();

        }

    }

}


