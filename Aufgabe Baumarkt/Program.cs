namespace Aufgabe_Baumarkt
{
    internal class Program
    {
        static void TextInWörterbuchParsen(string text)
        {
            Dictionary<string, List<string>> Baumarkt = KundennummerWöterbuch(text);
            foreach (KeyValuePair<string, List<string>> datensatz in Baumarkt)
            {
                Console.WriteLine("Kundennummer: " + datensatz.Key);
                foreach (string artikel in datensatz.Value)
                {
                    Console.WriteLine(" - " + artikel);
                }
            }
        }
        static Dictionary<string, List<string>> KundennummerWöterbuch(string text)
        {
            text = text.Replace(" ", "");
            Dictionary<string, List<string>> Baumarkt = new Dictionary<string, List<string>>();
            string[] zeile = text.Split('\n');
            foreach (string datensatz in zeile)
            {
                string[] kundeN = datensatz.Split(';');
                string[] artikel = kundeN[1].Split(',');
                Baumarkt.Add(kundeN[0], new List<string>(artikel));
            }
            return Baumarkt;
        }
        static void WöterbuchNeuAufbauen(string text)
        {
            Dictionary<string, List<string>> Baumarkt = KundennummerWöterbuch(text);
            Dictionary<string, List<string>> Artikel = new Dictionary<string, List<string>>();
            string nummer;

            foreach (KeyValuePair<string, List<string>> datensatz in Baumarkt)
            {
                nummer = datensatz.Key;
                foreach (string item in datensatz.Value)
                {
                    if (!Artikel.Keys.Contains(item))
                    {
                        Artikel.Add(item, new List<string>());
                    }
                    Artikel[item].Add(nummer);
                }
            }
            foreach (KeyValuePair<string, List<string>> datensatz in Artikel)
            {
                Console.WriteLine("Artikel: " + datensatz.Key);
                foreach (string kunde in datensatz.Value)
                {
                    Console.WriteLine(" - " + kunde);
                }
            }
        }
        static void Main(string[] args)
        {
            string liste = "0123; Hammer, Dübel, Nägel\n"
             + "4711; Kantholz, Säge, Nägel, Leim\n"
             + "8698; Schrauben, Dübel, Hänge-WC\n"
             + "9876; Fischfutter, Hammer, Säge\n"
             + "4862; Kantholz, Säge\n"
             + "3179; Schrauben, Schraubenzieher, Dübel\n"
             + "7410; Leim, Fischfutter\n"
             + "8520; Hänge-WC, Nägel, Säge";
            Console.WriteLine("Welche Sortierung möchten Sie wählen?");
            Console.WriteLine(" 1 - nach Kundennummer");
            Console.WriteLine(" 2 - nach Artikel");
            Console.Write("Ihre Auswahl: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    TextInWörterbuchParsen(liste);
                    break;
                case "2":
                    WöterbuchNeuAufbauen(liste);
                    break;
            }
        }
    }
}
