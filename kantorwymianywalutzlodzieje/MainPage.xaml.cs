using System.Data;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace kantorwymianywalutzlodzieje
{

    public class Currency
    {
        public string? table { get; set; }
        public string? currency { get; set; }
        public string? code { get; set; }
        public IList<Rate> rates { get; set; }
    }


    public class Rate
    {
        public string? no { get; set; }
        public string? effectiveDate { get; set; }
        public double? bid { get; set; }
        public double ask { get; set; }
    }

    

    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();

        }



           
            public void OnButtonClicked(object sender, EventArgs e)
            {
            DateOnly dateToday = DateOnly.FromDateTime(DateTime.Now);
            DateOnly dateYesterday = dateToday.AddDays(-1);



            string dzis = dateToday.ToString("yyyy-MM-dd");
            string wczoraj = dateYesterday.ToString("yyyy-MM-dd");



            string url = "https://api.nbp.pl/api/exchangerates/rates/c/" + currency1.Text + "/" + $"{dzis}" + "/?format=json";


            string url2 = "https://api.nbp.pl/api/exchangerates/rates/c/" + currency2.Text + "/" + $"{dzis}" + "/?format=json";





            string json;
            string json2;
            string json3;




            using (var webClient = new WebClient())
            {

                json = webClient.DownloadString(url);
                json2 = webClient.DownloadString(url2);








            }

                Currency curr1 = JsonSerializer.Deserialize<Currency>(json);

            string c1 = $"{curr1.code}"; 
                c1 += $" cena skupu {curr1.rates[0].bid} ";

                c1 += $"Cena sprzedaży: {curr1.rates[0].ask} ";

                Curr1.Text = c1;


                Currency curr2 = JsonSerializer.Deserialize<Currency>(json2);


                string c2 = $"{curr2.code} ";



                c2 += $"Cena skupu: {curr2.rates[0].bid} ";
                c2 += $"Cena sprzedaży: {curr2.rates[0].ask} ";
                Curr2.Text = c2;

            double przeliczenie = 0;
            double number = double.Parse(ilosc.Text);
            przeliczenie = ((curr1.rates[0].ask) * (number)) / curr2.rates[0].ask;
            
            Przeliczenie.Text = przeliczenie.ToString();





            }


    }

}
