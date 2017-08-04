using System;
using System.ComponentModel;
using System.Windows.Input;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using MaterialDesignThemes.Wpf;
using PdfSharp.Drawing.Layout;

namespace Akademia_MS_Projekt
{
    class PrintViewModel
    {
        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                NotifyPropertyChanged("SelectedStudent");
            }
        }

        public PrintViewModel()
        {
            _canExecute = true;
        }

        public void PrintFilterTask()
        {
            Student tempStudent = SelectedStudent;
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Filtr pasywny";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XPen pen = new XPen(XColors.Black, 5);
            XTextFormatter tf = new XTextFormatter(gfx);
            XFont fontBold = new XFont("Verdana", 12, XFontStyle.Bold);
            XFont fontRegular = new XFont("Verdana", 10, XFontStyle.Regular);

            XImage image = XImage.FromFile("RLC.jpg");
            gfx.DrawImage(image, 15, 100, 260, 110);

            gfx.DrawString(tempStudent.FirstName.ToString(), fontBold, XBrushes.Black,
              new XRect(20, 20, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString(tempStudent.LastName.ToString(), fontBold, XBrushes.Black,
              new XRect(20, 35, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString(tempStudent.Index.ToString(), fontBold, XBrushes.Black,
             new XRect(20, 50, page.Width, page.Height),
             XStringFormats.TopLeft);

            gfx.DrawString("R1=" + tempStudent.R1.ToString() + " Om", fontRegular, XBrushes.Black,
             new XRect(300, 115, page.Width, page.Height),
             XStringFormats.TopLeft);

            gfx.DrawString("R2=" + tempStudent.R2.ToString() + " Om", fontRegular, XBrushes.Black,
             new XRect(300, 130, page.Width, page.Height),
             XStringFormats.TopLeft);

            gfx.DrawString("L=" + tempStudent.L.ToString() + " mH", fontRegular, XBrushes.Black,
             new XRect(300, 145, page.Width, page.Height),
             XStringFormats.TopLeft);

            gfx.DrawString("C=" + tempStudent.C.ToString() + " µF", fontRegular, XBrushes.Black,
             new XRect(300, 160, page.Width, page.Height),
             XStringFormats.TopLeft);

            XRect rect = new XRect(300, 20, 250, 80);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.DrawString(Data.ContentFilterTask.ToString(), fontRegular, XBrushes.Black, rect, XStringFormats.TopLeft);

            string filename = "Filtr_" + tempStudent.Index.ToString() + ".pdf";
            document.Save(filename);

            Process.Start(filename);
        }

        public void PrintAmplifierTask()
        {
            Student tempStudent = SelectedStudent;
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Wzmacniacz różnicowy";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XPen pen = new XPen(XColors.Black, 5);
            XTextFormatter tf = new XTextFormatter(gfx);
            XFont fontBold = new XFont("Verdana", 12, XFontStyle.Bold);
            XFont fontRegular = new XFont("Verdana", 10, XFontStyle.Regular);

            XImage image = XImage.FromFile("Wzmacniacz.jpg");
            gfx.DrawImage(image, 15, 100, 260, 110);

            gfx.DrawString(tempStudent.FirstName.ToString(), fontBold, XBrushes.Black,
              new XRect(20, 20, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString(tempStudent.LastName.ToString(), fontBold, XBrushes.Black,
              new XRect(20, 35, page.Width, page.Height),
              XStringFormats.TopLeft);

            gfx.DrawString(tempStudent.Index.ToString(), fontBold, XBrushes.Black,
             new XRect(20, 50, page.Width, page.Height),
             XStringFormats.TopLeft);

            gfx.DrawString("R1=" + tempStudent.R3.ToString() + " Om", fontRegular, XBrushes.Black,
             new XRect(300, 115, page.Width, page.Height),
             XStringFormats.TopLeft);

            gfx.DrawString("R2=" + tempStudent.R4.ToString() + " Om", fontRegular, XBrushes.Black,
             new XRect(300, 130, page.Width, page.Height),
             XStringFormats.TopLeft);

            gfx.DrawString("R3=" + tempStudent.R5.ToString() + " Om", fontRegular, XBrushes.Black,
             new XRect(300, 145, page.Width, page.Height),
             XStringFormats.TopLeft);

            gfx.DrawString("R3=" + tempStudent.R6.ToString() + " Om", fontRegular, XBrushes.Black,
             new XRect(300, 160, page.Width, page.Height),
             XStringFormats.TopLeft);

            XRect rect = new XRect(300, 20, 250, 80);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.DrawString(Data.ContentAmplifierTask.ToString(), fontRegular, XBrushes.Black, rect, XStringFormats.TopLeft);

            string filename = "Wzmacniacz_" + tempStudent.Index.ToString() + ".pdf";
            document.Save(filename);
            Process.Start(filename);
        }

        #region Binding

        private bool _canExecute;
        private ICommand _printFilter;
        private ICommand _printAmplifier;

        public ICommand PrintFilter
        {
            get
            {
                return _printFilter ?? (_printFilter = new CommandHandler(() => PrintFilterTask(), _canExecute));
            }
        }
        public ICommand PrintAmplifier
        {
            get
            {
                return _printAmplifier ?? (_printAmplifier = new CommandHandler(() => PrintAmplifierTask(), _canExecute));
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

        public ICommand TaskDialogFiltr => new CommandImplementation(OpenDialogForFilter);
        public ICommand TaskDialogAmplifier => new CommandImplementation(OpenDialogForAmplifier);

        #endregion

        private async void OpenDialogForFilter(object o)
        {
            var view = new FilterTaskDialog
            {
                DataContext = new FilterTaskDialogViewModel()
            };

            var result = await DialogHost.Show(view, "RootDialog");
        }

        private async void OpenDialogForAmplifier(object o)
        {
            var view = new AmplifierTaskDialog
            {
                DataContext = new AmplifierTaskDialogViewModel()
            };

            var result = await DialogHost.Show(view, "RootDialog");
        }
    }
}
