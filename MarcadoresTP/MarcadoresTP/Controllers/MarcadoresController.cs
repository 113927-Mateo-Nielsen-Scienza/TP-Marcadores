using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MarcadoresTP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcadoresController : ControllerBase
    {
        private static Dictionary<string, LoginResponse> cache = new Dictionary<string, LoginResponse>();

        [HttpGet]
        [Route("/obtenerMarcadores")]
        public async Task<MarcadoresResponse> GetMarcadores()
        {
            string url = "https://prog3.nhorenstein.com/api/marcador/GetMarcadores";
            using (var client = new HttpClient())
            {
                LoginResponse lR = cache["lR"];

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", lR.token);

                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MarcadoresResponse>(content);

                try
                {
                    MarcadoresResponse mR = result;
                    return mR;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<LoginResponse> Login(LoginUsuario l)
        {
            string url = "https://prog3.nhorenstein.com/api/usuario/LoginUsuarioWeb";
            using (var client = new HttpClient())
            {

                var json = JsonConvert.SerializeObject(new LoginUsuario()
                {
                    nombreUsuario = l.nombreUsuario,
                    password = l.password
                });
                var response = await client.PostAsync(url, new
                StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginResponse>(responseBody);

                    try
                    {
                        LoginResponse lR = result;

                        cache["lR"] = lR;

                        return lR;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw new Exception($"La solicitud de inicio de sesión falló. Código de estado: {response.StatusCode}");
                }
            }
        }
    }


    
}
