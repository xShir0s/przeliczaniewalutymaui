using System.Data;
using System.Net;
using System.Text.Json;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace kantorwymianywalutzlodzieje
{

    public class Currency
        {
        public string?table{ get; set; }
        public string? currency { get; set; }
        public string? code { get; set; }
        public IList<Rate> rates { get; set; }
    }
    public class Rate
    {
        public string? no { get; set; }
        public string? effectiveDate { get; set; }
        public double? bid { get; set; }
        public double? ask { get; set; }
    }



    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
            
            


            DateOnly dateToday = DateOnly.FromDateTime(DateTime.Now);
            DateOnly dateYesterday = dateToday.AddDays(-1);
             

            
            string dzis = dateToday.ToString("yyyy-MM-dd");
            string wczoraj = dateYesterday.ToString("yyyy-MM-dd");
         


            string url = "https://api.nbp.pl/api/exchangerates/rates/c/" + "usd" + "/" + $"{dzis}" + "/?format=json";
            string url4 = "https://api.nbp.pl/api/exchangerates/rates/c/" + "usd" + "/" + $"{wczoraj}" + "/?format=json";

            string url2 = "https://api.nbp.pl/api/exchangerates/rates/c/" + "gbp" + "/" + $"{dzis}" + "/?format=json";
            string url5 = "https://api.nbp.pl/api/exchangerates/rates/c/" + "gbp" + "/" + $"{wczoraj}" + "/?format=json";

            string url3 = "https://api.nbp.pl/api/exchangerates/rates/c/" + "jpy" + "/" + $"{dzis}" + "/?format=json";
            string url6 = "https://api.nbp.pl/api/exchangerates/rates/c/" + "jpy" + "/" + $"{wczoraj}" + "/?format=json";

            string json;
            string json2;
            string json3;
            string json4;
            string json5;
            string json6;

            using (var webClient = new WebClient())
            {

                json = webClient.DownloadString(url);
                json2 = webClient.DownloadString(url2);
                json3 = webClient.DownloadString(url3);
                json4 = webClient.DownloadString(url4);
                json5= webClient.DownloadString(url5);
                json6 = webClient.DownloadString(url6);



            }

            Currency usd = JsonSerializer.Deserialize<Currency>(json);


            string s = $" {usd.code} ";
           


            s += $"Cena skupu: {usd.rates[0].bid} ";
            s += $"Cena sprzedaży: {usd.rates[0].ask} ";
            USD.Text = s;
            Currency usdwczoraj = JsonSerializer.Deserialize<Currency>(json4);
            var roznicaUSD = (usd.rates[0].ask) - (usdwczoraj.rates[0].ask);
            if(roznicaUSD >= 0)
            {
                USDPIC.Source = "up.jpg";
            }
            else
            {
                USDPIC.Source = "down.jpg";
            }

            Currency gbp = JsonSerializer.Deserialize<Currency>(json2);


            string a = $"{gbp.code} ";
            


            a += $"Cena skupu: {gbp.rates[0].bid} ";
            a += $"Cena sprzedaży: {gbp.rates[0].ask} ";
            GBP.Text = a;

            Currency gbpwczoraj = JsonSerializer.Deserialize<Currency>(json5);
            var roznicaGBP= (gbp.rates[0].ask) - (gbpwczoraj.rates[0].ask);
            if (roznicaGBP >= 0)
            {
                GBPPIC.Source = "up.jpg";
            }
            else
            {
                GBPPIC.Source = "down.jpg";
            }

            Currency jpy = JsonSerializer.Deserialize<Currency>(json3);


            string c = $"{jpy.code} ";


            c += $"Cena skupu: {jpy.rates[0].bid} ";
            c += $"Cena sprzedaży: {jpy.rates[0].ask} ";
            JPY.Text = c;
            Currency jpywczoraj = JsonSerializer.Deserialize<Currency>(json6);
            var roznicaJPY = (jpy.rates[0].ask) - (jpywczoraj.rates[0].ask);
            if (roznicaJPY >= 0)
            {
               JPYPIC.Source = "up.jpg";
            }
            else
            {
                JPYPIC.Source = "down.jpg";
            }







        }
    }

}
