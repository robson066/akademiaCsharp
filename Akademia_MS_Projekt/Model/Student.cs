using System;

namespace Akademia_MS_Projekt
{
    public class Student
    {

        private int _index;
        private string _firstName;
        private string _lastName;

        private int _L;
        private int _C;
        private int _R1;
        private int _R2;
        private int _R3;
        private int _R4;
        private int _R5;
        private int _R6;

        public int Index { get => _index; set => _index = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }

        public int L { get => _L; set => _L = value; }
        public int C { get => _C; set => _C = value; }
        public int R1 { get => _R1; set => _R1 = value; }
        public int R2 { get => _R2; set => _R2 = value; }
        public int R3 { get => _R3; set => _R3 = value; }
        public int R4 { get => _R4; set => _R4 = value; }
        public int R5 { get => _R5; set => _R5 = value; }
        public int R6 { get => _R6; set => _R6 = value; }

        System.Random x = new Random(System.DateTime.Now.Millisecond);

        public Student(int Index, string FirstName, string LastName)
        {
            this.Index = Index;
            this.FirstName = FirstName;
            this.LastName = LastName;
            L = x.Next(1, 100);
            C = x.Next(1, 100);
            R1 = x.Next(1, 100);
            R2 = x.Next(1, 100);
            R3 = x.Next(1, 100);
            R4 = x.Next(1, 100);
            R5 = x.Next(1, 100);
            R6 = x.Next(1, 100);
        }
    }
}
