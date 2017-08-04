using System.Diagnostics;
using System.Windows.Input;

namespace Akademia_MS_Projekt
{
    class SourcesViewModel
    {
        private bool _canExecute;
        public SourcesViewModel()
        {
            _canExecute = true;

        }

        private ICommand _clickSource1;
        private ICommand _clickSource2;
        private ICommand _clickSource3;
        private ICommand _clickSource4;
        private ICommand _clickSource5;

        private void Source1()
        {
            Process.Start("http://www.dydaktyka.ib.pwr.wroc.pl/materialy/ETP1006L%20Elektronika%20i%20elektrotechnika/filtry_p.pdf");
        }
        private void Source2()
        {
            Process.Start("http://etacar.put.poznan.pl/mariusz.naumowicz/EiE/filtry.pdf");
        }
        private void Source3()
        {
            Process.Start("http://home.agh.edu.pl/~maziarz/LabPE/wzmacniacz.html");
        }
        private void Source4()
        {
            Process.Start("http://www.ue.pwr.wroc.pl/wyklad_w10/W9A_Wzm_operacyjne.pdf");
        }
        private void Source5()
        {            
            Process.Start("http://www.mif.pg.gda.pl/homepages/jasiu/stud/ECS/wykl-14-US-opamp.pdf");
        }

        public ICommand ClickSource1
        {
            get
            {
                return _clickSource1 ?? (_clickSource1 = new CommandHandler(() => Source1(), _canExecute));
            }
        }
        public ICommand ClickSource2
        {
            get
            {
                return _clickSource2 ?? (_clickSource2 = new CommandHandler(() => Source2(), _canExecute));
            }
        }
        public ICommand ClickSource3
        {
            get
            {
                return _clickSource3 ?? (_clickSource3 = new CommandHandler(() => Source3(), _canExecute));
            }
        }
        public ICommand ClickSource4
        {
            get
            {
                return _clickSource4 ?? (_clickSource4 = new CommandHandler(() => Source4(), _canExecute));
            }
        }
        public ICommand ClickSource5
        {
            get
            {
                return _clickSource5 ?? (_clickSource5 = new CommandHandler(() => Source5(), _canExecute));
            }
        }
    }
    
}
