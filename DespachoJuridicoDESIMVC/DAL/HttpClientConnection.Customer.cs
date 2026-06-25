using DespachoJuridicoDESIMVC.Models.Customer;
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
            var result = await RequestAsync<object>("api/Customer/List", HttpMethod.Post, null,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<ClientMassagesResponse>(result.ToString());

            return response;
        }
        public async Task<ClientMassagesResponse> GetClienteById(long id)
        {
            var request = new
            {
                Id = id
            };

            var result = await RequestAsync<object>("api/Customer/First", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<ClientMassagesResponse>(result.ToString());

            return response;
        }
        public async Task<ClientMassagesResponse> SaveOrUpdateCliente(ClienteObj  client)
        {
            MapAuditFields(client);

            var request = new SaveOrUpdateClienteRequest()
            { 
                Cliente  = client
            };

            var result = await RequestAsync<object>("api/Customer", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<ClientMassagesResponse>(result.ToString());

            return response;
        }
    }
}