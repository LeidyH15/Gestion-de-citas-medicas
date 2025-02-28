using MicroserviceRecetas.Application.DTOs;
using MicroserviceRecetas.Application.Interfaces;
using RestSharp;
using System.Configuration;
using System.Threading.Tasks;

namespace MicroserviceRecetas.Application.Services
{
    public class CitaService : ICitaService
    {
        private readonly RestClient _client;
        private readonly string _uri;

        public CitaService()
        {
            _client = new RestClient(ConfigurationManager.AppSettings["CitasApiUrl"]);
            _uri = "api/Citas";
        }
        public async Task<CitaDTO> GetById(int id)
        {
            var request = new RestRequest($"{_uri}/{id}", Method.Get);
            var response = await _client.ExecuteAsync<CitaDTO>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return null; // O puedes manejar errores dependiendo de la respuesta
            }
        }
    }
}