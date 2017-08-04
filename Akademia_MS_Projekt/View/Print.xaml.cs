using System.Windows.Controls;

namespace Akademia_MS_Projekt
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Print : UserControl
    {
        public Print()
        {
            InitializeComponent();
            DataContext = new PrintViewModel();
        }
    }
}
