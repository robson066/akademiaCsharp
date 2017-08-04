using System.Collections.Generic;

namespace Akademia_MS_Projekt
{
    public class Data
    {
        private static double _u1;
        private static int _f1;
        private static int _f2;
        private static int _fakt;
        private static double _u2min;
        private static double _u2max;
        private static double _uout;
        private static double _uakt;
        private static string _contentFilterTask;
        private static string _contentAmplifierTask;

        public static double U1 { get { return _u1; } set { _u1 = value; } }
        public static double U2min { get { return _u2min; } set { _u2min = value; } }
        public static double U2max { get { return _u2max; } set { _u2max = value; } }
        public static double Uout { get { return _uout; } set { _uout = value; } }
        public static double Uakt { get { return _uakt; } set { _uakt = value; } }
        public static int F1 { get { return _f1; } set { _f1 = value; } }
        public static int F2 { get { return _f2; } set { _f2 = value; } }
        public static int Fakt { get { return _fakt; } set { _fakt = value; } }
        public static string ContentFilterTask { get => _contentFilterTask; set => _contentFilterTask = value; }
        public static string ContentAmplifierTask { get => _contentAmplifierTask; set => _contentAmplifierTask = value; }

        public static List<Student> Students { get => students; set => students = value; }

        private static List<Student> students = new List<Student>()
        {
            new Student(123456, "Adam", "Kowalski")
        };
    }
}
