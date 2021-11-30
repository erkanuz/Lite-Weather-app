using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LiteWeatherApp
{
    public class Helper
    {
        public static async Task<Responser> Get(string url, string token = null)
        {
            using (var client = new HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(token))
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", token);

                var request = await client.GetAsync(url);
                if (request.IsSuccessStatusCode)
                {
                    return new Responser { Response = await request.Content.ReadAsStringAsync() };
                }
                else
                    return new Responser { ErrorMessage = request.ReasonPhrase };
            }
        }
    }

    public class Responser
    {
        public bool Succesful => ErrorMessage == null;
        public string ErrorMessage { get; set; }
        public string Response { get; set; }
    }
}
