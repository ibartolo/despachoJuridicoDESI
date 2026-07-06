using DespachoJuridicoDESIMVC.Models.Case;
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
        public async Task<StatusCaseMessagesResponse> GetAllStatusCase()
        {
            var result = await RequestAsync<object>("api/Case/StatusCase/List", HttpMethod.Post, null,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<StatusCaseMessagesResponse>(result.ToString());

            return response;
        }

        public async Task<CaseMessagesResponse> GetAllCase()
        {
            var result = await RequestAsync<object>("api/Case/List", HttpMethod.Post, null,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<CaseMessagesResponse>(result.ToString());
            return response;
        }
        public async Task<CaseMessagesResponse> GetCaseById(long id)
        {
            var result = await RequestAsync<object>("api/Case/First", HttpMethod.Post, new { Id = id },
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);
            var response = JsonConvert.DeserializeObject<CaseMessagesResponse>(result.ToString());
            return response;
        }
        public async Task<CaseMessagesResponse> SaveOrUpdateCase(CaseObj caseObj)
        {
            var requetes = new {
                Case = caseObj
            };
            var result = await RequestAsync<object>("api/Case", HttpMethod.Post, requetes,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);
            var response = JsonConvert.DeserializeObject<CaseMessagesResponse>(result.ToString());
            return response;
        }
    }
}