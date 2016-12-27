using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuzzy_Logic
{
    class FuzzyL // класс нечеткая логика
    {
        double LowMPC;//нижняя черта множества
        double HighMPC;//верхняя черта множества
        bool PossibleTrue;
        bool PossibleFalce;
        string ChanceGrade;// описание вероятности
        double AvgMPC;// средний елемент множества
        double MPCrange=0;//размах можества

        //дефолтный конструктор
        public FuzzyL() { LowMPC = 0; HighMPC = 1; PossibleTrue = false; PossibleFalce = false; ChanceGrade = ""; }
        //инициализация с помощью нижней и верхней чёрт 
        public FuzzyL(double a, double b) { LowMPC = a; HighMPC = b; PossibleTrue = false; PossibleFalce = false; ChanceGrade = ""; }
        // инициализация с помощью середины и размаха
        public FuzzyL(double a, double b, int c) { LowMPC = a - b; HighMPC = a + b; PossibleTrue = false; PossibleFalce = false; ChanceGrade = "";
        if (LowMPC < 0) LowMPC = 0; if (HighMPC > 1) HighMPC = 1;}

        static public FuzzyL NotTrueIsFalse(FuzzyL obj)// что не истина то ложь
        {
            if (obj.HighMPC < 1.0) obj.PossibleFalce = true;
            return obj;

        }

        static public FuzzyL NotFalseIsTrue(FuzzyL obj)//что не ложь то истина
        {
            if (obj.LowMPC > 0.0) obj.PossibleTrue = true;
            return obj;
        }

        static public FuzzyL operator ! (FuzzyL obj)//перегуженый оператор НЕ
        {
            double a = obj.HighMPC;
            obj.HighMPC = 1.0 - obj.LowMPC;
            obj.LowMPC = 1.0 - a;
            return obj;
        }

        static public FuzzyL operator  | (FuzzyL obj1, FuzzyL obj2)//перегруженый оператор ИЛИ
        {
            obj1.LowMPC = Math.Max(obj1.LowMPC, obj2.LowMPC);
            obj1.HighMPC = Math.Max(obj1.HighMPC, obj2.HighMPC);
            return obj1;
        }

        static public FuzzyL operator & (FuzzyL obj1, FuzzyL obj2)// перегруженый оператор И
        {
            obj1.LowMPC = Math.Min(obj1.LowMPC, obj2.LowMPC);
            obj1.HighMPC = Math.Min(obj1.HighMPC, obj2.HighMPC);
            return obj1;
        }

        static public FuzzyL GetAvgMPC(FuzzyL obj)//найти среднее значение множества
        {
            obj.AvgMPC = (obj.HighMPC + obj.LowMPC) / 2;
            return obj;
        }

        static public FuzzyL GetCG(FuzzyL obj)//записать описание вероятности
        {
            if (obj.AvgMPC >= 0 && obj.AvgMPC <= 0.2) obj.ChanceGrade = "Very unlikely";
            else if (obj.AvgMPC > 0.2 && obj.AvgMPC <= 0.4) obj.ChanceGrade = "Unlikely";
            else if (obj.AvgMPC > 0.4 && obj.AvgMPC <= 0.6) obj.ChanceGrade = "Feasibly";
            else if (obj.AvgMPC > 0.6 && obj.AvgMPC <= 0.8) obj.ChanceGrade = "Likely";
            else if(obj.AvgMPC > 0.8 && obj.AvgMPC <= 1) obj.ChanceGrade = "Very likely";
            return obj;
        }

        static public FuzzyL GetRange(FuzzyL obj)//найти размах
        {
            obj.MPCrange = obj.HighMPC - obj.AvgMPC;
            return obj;
        }

        static public void PrintAll(FuzzyL obj)//вівести все значения
        {
            Console.WriteLine("LowMPC = " +obj.LowMPC);
            Console.WriteLine("HighMPC = " +obj.HighMPC);
            Console.WriteLine("PossibleTrue = " +obj.PossibleTrue);
            Console.WriteLine("PossibleFalce = " +obj.PossibleFalce);
            Console.WriteLine("ChanceGrade: " +obj.ChanceGrade);
            Console.WriteLine("AvgMPC = " +obj.AvgMPC);
            Console.WriteLine("MPCrange = " +obj.MPCrange);
        }

        static void Main(string[] args)
        {
            double l = Convert.ToDouble(Console.ReadLine());
            double h = Convert.ToDouble(Console.ReadLine());
            FuzzyL MPC = new FuzzyL(l, h);// инициализация с помощью минимума и максимума

            MPC = !MPC;//логическая операция НЕ

            FuzzyL.NotFalseIsTrue(MPC);
            FuzzyL.NotTrueIsFalse(MPC);
            FuzzyL.GetAvgMPC(MPC);
            FuzzyL.GetCG(MPC);
            FuzzyL.GetRange(MPC);
            FuzzyL.PrintAll(MPC);

            double a = Convert.ToDouble(Console.ReadLine());
            double b = Convert.ToDouble(Console.ReadLine());

            FuzzyL NewMPC = new FuzzyL(a, b, 1);// инициализация с помощью среднего значения и размаха

            MPC = MPC & NewMPC;//логическая операция И

            FuzzyL.NotFalseIsTrue(MPC);
            FuzzyL.NotTrueIsFalse(MPC);
            FuzzyL.GetAvgMPC(MPC);
            FuzzyL.GetCG(MPC);
            FuzzyL.GetRange(MPC);

            FuzzyL.PrintAll(MPC);
            Console.ReadKey();

 
 
        }
    }
}
