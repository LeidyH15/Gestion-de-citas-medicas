using MicroserviceCitas.Application.DTOs;
using MicroserviceCitas.Application.Interfaces;
using RestSharp;
using System.Configuration;
using System.Threading.Tasks;
using System.Web;

namespace MicroserviceCitas.Application.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly RestClient _client;
        private readonly string _uri;


        public PersonaService()
        {
            _client = new RestClient(ConfigurationManager.AppSettings["PersonasApiUrl"]);
            _uri = "api/Personas/buscar";
        }
        public async Task<PersonaDTO> GetByIdentificacion(int tipoPersona, string identificacion)
        {
            
            if (tipoPersona <= 0 || (tipoPersona != 1 && tipoPersona != 2))
            {
                throw new System.ArgumentException("El tipo de persona debe ser 1 (Médico) o 2 (Paciente).");
            }

            if (string.IsNullOrEmpty(identificacion))
            {
                throw new System.ArgumentException("La Identificacion de la persona no puede ser nula o vacia.");
            }
                        
            var request = new RestRequest(_uri, Method.Get);
            request.AddParameter("TipoPersona", tipoPersona);
            request.AddParameter("Identificacion", identificacion);

            // Token JWT MicroservicePersonas
            string token = HttpContext.Current?.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(token))
            {
                request.AddHeader("Authorization", token);
            }

            var response = await _client.ExecuteAsync<PersonaDTO>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
                        
            return null; 
        }

    }
}
