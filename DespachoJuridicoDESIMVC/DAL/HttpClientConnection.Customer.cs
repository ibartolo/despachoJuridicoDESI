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
        public async Task<ClientMassagesResponse> GetAllClientes()
        {
            var result = await RequestAsync<object>("api/Customer/GetAllClientes", HttpMethod.Post, null,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<ClientMassagesResponse>(result.ToString());

            return response;
        }
    }
}