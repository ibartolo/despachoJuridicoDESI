using DespachoJuridicoDESIMVC.Models.EventType;
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
        /// Obtiene todos los tipos de evento
        /// </summary>
        public async Task<EventTypeMassagesResponse> GetAllEventTypes()
        {
            var result = await RequestAsync<object>("api/EventType/GetAll", HttpMethod.Post, null,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<EventTypeMassagesResponse>(result.ToString());

            return response;
        }

        /// <summary>
        /// Obtiene tipo de evento por ID
        /// </summary>
        public async Task<EventTypeMassagesResponse> GetEventTypeById(long id)
        {
            var request = new
            {
                Id = id
            };

            var result = await RequestAsync<object>("api/EventType/GetById", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<EventTypeMassagesResponse>(result.ToString());

            return response;
        }

        /// <summary>
        /// Elimina tipo de evento
        /// </summary>
        public async Task<EventTypeDeleteResponse> DeleteEventType(long id)
        {
            var request = new
            {
                Id = id
            };

            var result = await RequestAsync<object>("api/EventType/Delete", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<EventTypeDeleteResponse>(result.ToString());

            return response;
        }

        /// <summary>
        /// Guarda o actualiza tipo de evento
        /// </summary>
        public async Task<EventTypeMassagesResponse> SaveOrUpdateEventType(EventTypeObj eventType)
        {
            MapAuditFields(eventType);

            var request = new SaveOrUpdateEventTypeRequest()
            {
                EventType = eventType
            };

            var result = await RequestAsync<object>("api/EventType/SaveOrUpdate", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<EventTypeMassagesResponse>(result.ToString());

            return response;
        }
    }
}