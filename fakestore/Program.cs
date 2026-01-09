using System.Text.Json;

namespace fakestore
{
    public class Rating
    {
        public float rate { get; set; }
        public int count { get; set; }

    }
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; } = "";
        public decimal price { get; set; }
        public string description { get; set; } = "";
        public string category { get; set; } = "";
        public string image { get; set; } = "";

        public Rating rating { get; set; }

    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url = "https://fakestoreapi.com/products";
            using HttpClient client = new();
            List<Product> products = new List<Product>();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string jsonData = await response.Content.ReadAsStringAsync();

                products = JsonSerializer.Deserialize<List<Product>>(jsonData);

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            while (true)
            {

                Console.Clear();
                Console.WriteLine("=== Hauptmenü ===");
                Console.WriteLine("1. Title");
                Console.WriteLine("2. Produkt kategorie");
                Console.WriteLine("3. Rating Sortiere");
                Console.WriteLine("4. Min Preis \n   Max Preis");
                Console.WriteLine("0. Beenden");
                Console.Write("Bitte wählen Sie eine Option: ");

                string format = Console.ReadLine();
                Console.Clear();
                switch (format)
                {
                    case "1":
                        products.Sort((a, b) => a.title.CompareTo(b.title));
                        foreach (Product singleProduct in products)
                        {
                            System.Console.WriteLine($"{new string(singleProduct.title.Take(60).ToArray()).PadRight(60)} - {singleProduct.price,10}");
                        }
                        await Task.Delay(10000);
                        break;
                    case "2":
                        products.Sort((a, b) => a.category.CompareTo(b.category));
                        string cat = "";
                        foreach (Product singleProduct in products)
                        {
                            if (singleProduct.category != cat)
                            {

                                cat = singleProduct.category;
                                Console.WriteLine("==" + cat + "==");
                            }

                            System.Console.WriteLine($"   {new string(singleProduct.title.Take(60).ToArray()).PadRight(60)} - {singleProduct.price,10}");
                        }
                        await Task.Delay(10000);
                        break;
                    case "3":
                        products.Sort((a, b) => a.rating.rate.CompareTo(b.rating.rate));

                        foreach (Product singleProduct in products)
                        {
                            System.Console.WriteLine($"{singleProduct.rating.rate,6}:  {new string(singleProduct.title.Take(60).ToArray()).PadRight(60)} - {singleProduct.price,10}");
                        }
                        await Task.Delay(10000);
                        break;
                    case "4":
                        var min = products.MinBy((a => a.price));
                        var max = products.MaxBy((a => a.price));
                        System.Console.WriteLine($"{new string(min.title.Take(60).ToArray()).PadRight(60)} - {min.price,10}");
                        System.Console.WriteLine($"{new string(max.title.Take(60).ToArray()).PadRight(60)} - {max.price,10}");
                        await Task.Delay(10000);
                        break;
                    default:
                        Console.WriteLine("Programm wird beendet...");
                        return;
                }
            }
        }
    }
}
