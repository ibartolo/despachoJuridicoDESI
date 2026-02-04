using DespachoJuridicoDESIMVC.Models.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace DespachoJuridicoDESIMVC.DAL
{
    public partial class HttpClientConnection
    {
        public async Task<UserMassages> GetUsuarioByCorreo(string email)
        {
            var request = new
            {
                email = email
            };

            var result = await RequestAsync<object>("api/Autentication/GetUsuarioByCorreo", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }));

            var response = JsonConvert.DeserializeObject<UserMassages>(result.ToString());

            return response;
        }
    }
}