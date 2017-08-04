using System.Windows.Controls;

namespace Akademia_MS_Projekt
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class Sources : UserControl
    {
        public Sources()
        {
            InitializeComponent();
            DataContext = new SourcesViewModel();
            
        }
    }
}
