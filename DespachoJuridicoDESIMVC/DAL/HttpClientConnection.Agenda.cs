using DespachoJuridicoDESIMVC.Models.Agenda;
using DespachoJuridicoDESIMVC.Models.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DespachoJuridicoDESIMVC.DAL
{
    public partial class HttpClientConnection
    {
        /// <summary>
        /// Obtiene todas las agendas
        /// </summary>
        public async Task<AgendaMassagesResponse> GetAllAgenda()
        {
            var result = await RequestAsync<object>("api/Agenda/GetAll", HttpMethod.Post, null,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<AgendaMassagesResponse>(result.ToString());

            return response;
        }

        /// <summary>
        /// Obtiene agenda por ID
        /// </summary>
        public async Task<AgendaMassagesResponse> GetAgendaById(long id)
        {
            var request = new
            {
                Id = id
            };

            var result = await RequestAsync<object>("api/Agenda/GetById", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<AgendaMassagesResponse>(result.ToString());

            return response;
        }

        /// <summary>
        /// Elimina agenda
        /// </summary>
        public async Task<AgendaDeleteResponse> DeleteAgenda(long id)
        {
            var request = new
            {
                Id = id
            };

            var result = await RequestAsync<object>("api/Agenda/Delete", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<AgendaDeleteResponse>(result.ToString());

            return response;
        }

        /// <summary>
        /// Guarda o actualiza agenda
        /// </summary>
        public async Task<AgendaMassagesResponse> SaveOrUpdateAgenda(AgendaObj agenda)
        {
            MapAuditFields(agenda);

            var request = new SaveOrUpdateAgendaRequest()
            {
                Agenda = agenda
            };

            var result = await RequestAsync<object>("api/Agenda/SaveOrUpdate", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<AgendaMassagesResponse>(result.ToString());

            return response;
        }
    }

    #region Request Models

    public class SaveOrUpdateAgendaRequest
    {
        public AgendaObj Agenda { get; set; }
    }

    #endregion
}