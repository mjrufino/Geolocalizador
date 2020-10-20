using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task Main()
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

            HttpResponseMessage response = await client.GetAsync("https://nominatim.openstreetmap.org/search?street=parana%202662&city=villa%20ballester&county=buenos%20aires&country=argentina&postalcode=1653&format=jsonv2");
            Console.WriteLine("done");
            response.EnsureSuccessStatusCode();
            Console.WriteLine("done");
            string responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseBody);

            var a = JsonConvert.DeserializeObject<List<aResponse>>(responseBody);

            Console.Read();
        }
    }
    public class aResponse
    {
        public string lat { get; set; }

        public string lon { get; set; }
    }
}
