namespace Akademia_MS_Projekt
{

    public class MainWindowViewModel
    {

        public Panels[] Panels { get; }

        public MainWindowViewModel()
        {
            Panels = new[]
            {
                new Panels("Start", new Home()),
                new Panels("Filtr pasywny", new Filter()),
                new Panels("Wzmacniacz różnicowy", new Amplifier()),
                new Panels("Generator sprawdzianów", new Print()),
                new Panels("Materiały dodatkowe", new Sources()),
                new Panels("O aplikacji", new About())
            };
        }
    }
}


