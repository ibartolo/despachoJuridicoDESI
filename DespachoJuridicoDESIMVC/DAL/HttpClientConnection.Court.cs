using DespachoJuridicoDESIMVC.Models.Court;
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
        /// <summary>
        /// Obtiene todos los juzgados
        /// </summary>
        public async Task<CourtMassagesResponse> GetAllCourts()
        {
            var result = await RequestAsync<object>("api/Court/GetAll", HttpMethod.Post, null,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<CourtMassagesResponse>(result.ToString());

            return response;
        }

        /// <summary>
        /// Obtiene juzgado por ID
        /// </summary>
        public async Task<CourtMassagesResponse> GetCourtById(long id)
        {
            var request = new
            {
                Id = id
            };

            var result = await RequestAsync<object>("api/Court/GetById", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<CourtMassagesResponse>(result.ToString());

            return response;
        }

        /// <summary>
        /// Elimina juzgado
        /// </summary>
        public async Task<CourtDeleteResponse> DeleteCourt(long id)
        {
            var request = new
            {
                Id = id
            };

            var result = await RequestAsync<object>("api/Court/Delete", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<CourtDeleteResponse>(result.ToString());

            return response;
        }

        /// <summary>
        /// Guarda o actualiza juzgado
        /// </summary>
        public async Task<CourtMassagesResponse> SaveOrUpdateCourt(CourtObj court)
        {
            MapAuditFields(court);

            var request = new SaveOrUpdateCourtRequest()
            {
                Court = court
            };

            var result = await RequestAsync<object>("api/Court/SaveOrUpdate", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<CourtMassagesResponse>(result.ToString());

            return response;
        }
    }
}