using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace RBR_CoDriver_Installer
{
    public class Config
    {
        public string luppis_pacenotes_url { get; set; }
        public string luppis_site_url { get; set; }
    }
    public class ConfigServices
    {
        private readonly string _jsonUrl = "https://raw.githubusercontent.com/PedroMAlves/RBR-CoDriver-Installer/refs/heads/main/configs.json";

        public async Task<Config> GetAvailableCodriversAsync()
        {
            using (var client = new HttpClient())
            {
                // Importante: Adicionar um User-Agent para o GitHub não bloquear o request
                client.DefaultRequestHeaders.Add("User-Agent", "RBR-CoDriver-Installer-App");

                var response = await client.GetStringAsync(_jsonUrl);
                return JsonSerializer.Deserialize<Config>(response);
            }
        }
    }
}
