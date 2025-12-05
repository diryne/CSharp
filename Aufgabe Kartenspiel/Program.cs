using System.Collections.Generic;

namespace Aufgabe_Kartenspiel
{
    public enum Kartenfarbe
    {
        Kreuz,
        Pik,
        Herz,
        Karo
    }
    public enum Kartenwert
    {
        Sieben = 7,
        Acht,
        Neun,
        Zehn,
        Bube,
        Dame,
        König,
        Ass
    }
    public class Karte
    {
        private Kartenfarbe _farbe;
        private Kartenwert _wert;

        public Karte(Kartenfarbe kartenfarbe, Kartenwert kartenwert)
        {
            _farbe = kartenfarbe;
            _wert = kartenwert;
        }

        public static List<Karte> Mischen(List<Karte> sortiert)
        {
            Random randy = new Random();
            for (int i = 0; i < 100; i++)
            {
                int a = randy.Next(sortiert.Count);
                int b = randy.Next(sortiert.Count);
                (sortiert[a], sortiert[b]) = (sortiert[b], sortiert[a]);
            }
            return sortiert;
        }
        public static Stack<Karte> StapelErstellen(Kartenfarbe kartenfarbe)
        {
            Stack<Karte> kartenStapel = new Stack<Karte>();
            var allewerte = Enum.GetValues(typeof(Kartenwert));
            foreach (Kartenwert w in allewerte)
                kartenStapel.Push(new Karte(kartenfarbe, w));
            return kartenStapel;
        }
        public static Stack<Karte> StapelErstellen()
        {

            Stack<Karte> kartenStapel = new Stack<Karte>();
            var allewerte = Enum.GetValues(typeof(Kartenwert));
            var alleFarben = Enum.GetValues(typeof(Kartenfarbe));
            foreach (Kartenfarbe k in alleFarben)
                foreach (Kartenwert w in allewerte)
                    kartenStapel.Push(new Karte(k, w));
            return kartenStapel;
        }
        public static void Info(Stack<Karte> kartenStapel)
        {
            foreach (var item in kartenStapel)
                Console.WriteLine(item._farbe + " " + item._wert);
        }
        public static List<Stack<Karte>> TeilStapel(Stack<Karte> Stapel, int anzahl)
        {
            List<Stack<Karte>> result = new List<Stack<Karte>>();
            int anzahlTeil = Stapel.Count / anzahl;
            for (int i = 0; i < anzahl; i++)
            {
                Stack<Karte> StapelNew = new Stack<Karte>();
                for (int j = 0; j < anzahlTeil; j++)
                {
                    StapelNew.Push(Stapel.Pop());
                }
                result.Add(StapelNew);

            }

            return result;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Kartenfarbe kP = Kartenfarbe.Pik;
            Kartenfarbe kH = Kartenfarbe.Herz;
            Stack<Karte> Stapel1 = Karte.StapelErstellen(kP);
            Stack<Karte> Stapel2 = Karte.StapelErstellen(kH);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Alle Karten der Pik-Reihe");
            Console.ResetColor();
            Karte.Info(Stapel1);
            Console.WriteLine("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Alle Karten der Herz-Reihe");
            Console.ResetColor();
            Karte.Info(Stapel2);
            Console.WriteLine("----------------------------------------------------");
            Stack<Karte> Stapel12 = new Stack<Karte>();
            var allewerte = Enum.GetValues(typeof(Kartenwert));
            while (Stapel1.Count > 0)
            {
                Stapel12.Push(Stapel1.Pop());
                Stapel12.Push(Stapel2.Pop());
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Pik-Reihe und Herz-Reihe abwechselnd");
            Console.ResetColor();
            Karte.Info(Stapel12);
            Console.WriteLine("----------------------------------------------------");
            List<Stack<Karte>> StapelList = Karte.TeilStapel(Stapel12, 4);
            for (int i = 0; i < 4; i++)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Der " + (i + 1) + ". Teil des Stapels");
                Console.ResetColor();
                Karte.Info(StapelList[i]);
                Console.WriteLine("----------------------------------------------------");

            }
            Stack<Karte> Stapel13 = new Stack<Karte>(StapelList[2].Reverse().Concat(StapelList[0].Reverse()));
            Stack<Karte> Stapel24 = new Stack<Karte>(StapelList[3].Reverse().Concat(StapelList[1].Reverse()));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Der erste Teil des Stapels auf den dritten Teil des Stapels");
            Console.ResetColor();
            Karte.Info(Stapel13);
            Console.WriteLine("----------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Der zweite Teil des Stapels auf den vierten Teil des Stapels");
            Console.ResetColor();
            Karte.Info(Stapel24);
            Console.WriteLine("----------------------------------------------------");

            Stack<Karte> Stapel1324 = new Stack<Karte>(Stapel24.Reverse().Concat(Stapel13.Reverse()));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Stapel1 => Stapel3 => Stapel2 => Stapel4");
            Console.ResetColor();
            Karte.Info(Stapel1324);
            Console.WriteLine("----------------------------------------------------");


        }
    }
}
