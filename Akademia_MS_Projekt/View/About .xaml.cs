using System.Windows.Controls;

namespace Akademia_MS_Projekt
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
            DataContext = new AboutViewModel();
        }
    }
}
