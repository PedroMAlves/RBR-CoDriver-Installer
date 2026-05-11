
using System.Text.Json;

namespace RBR_CoDriver_Installer
{
    // A estrutura do teu co-piloto
    public class Codriver
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
        public string image_url { get; set; }
        public string codriver_name { get; set; }
        public string preview_url { get; set; }
        public string scale_name { get; set; }
        public string scale_image { get; set; }

    }

    public class CodriverService
    {
        private readonly string _jsonUrl = "https://raw.githubusercontent.com/PedroMAlves/RBR-CoDriver-Installer/refs/heads/main/codrivers.json";

        public async Task<List<Codriver>> GetAvailableCodriversAsync()
        {
            using (var client = new HttpClient())
            {
                // Importante: Adicionar um User-Agent para o GitHub não bloquear o request
                client.DefaultRequestHeaders.Add("User-Agent", "RBR-CoDriver-Installer-App");

                var response = await client.GetStringAsync(_jsonUrl);
                return JsonSerializer.Deserialize<List<Codriver>>(response);
            }
        }
    }
}
