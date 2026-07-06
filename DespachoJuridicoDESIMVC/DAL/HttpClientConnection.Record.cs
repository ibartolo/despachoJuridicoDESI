using DespachoJuridicoDESIMVC.Models.Common;
using DespachoJuridicoDESIMVC.Models.Messages;
using DespachoJuridicoDESIMVC.Models.Record;
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
        /// Obtiene todos los expedientes
        /// </summary>
        public async Task<RecordMassagesResponse> GetAllRecords()
        {
            try
            {
                var result = await RequestAsync<object>("api/Record/GetAll", HttpMethod.Post, null,
                    new Func<string, string>((responseString) =>
                    {
                        return responseString;
                    }), token.Token.access_token);

                if (result == null)
                {
                    return new RecordMassagesResponse
                    {
                        Result = new OperationResult
                        {
                            Successful = false,
                            SystemMessages = new List<SystemMessage>
                    {
                        new SystemMessage { Message = "La respuesta del servidor es nula." }
                    }
                        }
                    };
                }

                var response = JsonConvert.DeserializeObject<RecordMassagesResponse>(result.ToString());

                if (response == null)
                {
                    return new RecordMassagesResponse
                    {
                        Result = new OperationResult
                        {
                            Successful = false,
                            SystemMessages = new List<SystemMessage>
                    {
                        new SystemMessage { Message = "No se pudo deserializar la respuesta del servidor." }
                    }
                        }
                    };
                }

                return response;
            }
            catch (HttpRequestException ex)
            {
                // Error de red o de conexión
                return new RecordMassagesResponse
                {
                    Result = new OperationResult
                    {
                        Successful = false,
                        SystemMessages = new List<SystemMessage>
                {
                    new SystemMessage { Message = $"Error de conexión: {ex.Message}" }
                }
                    }
                };
            }
            catch (JsonException ex)
            {
                // Error al deserializar JSON
                return new RecordMassagesResponse
                {
                    Result = new OperationResult
                    {
                        Successful = false,
                        SystemMessages = new List<SystemMessage>
                {
                    new SystemMessage { Message = $"Error al procesar la respuesta del servidor: {ex.Message}" }
                }
                    }
                };
            }
            catch (Exception ex)
            {
                // Cualquier otro error
                return new RecordMassagesResponse
                {
                    Result = new OperationResult
                    {
                        Successful = false,
                        SystemMessages = new List<SystemMessage>
                {
                    new SystemMessage { Message = $"Error inesperado: {ex.Message}" }
                }
                    }
                };
            }
        }

        /// <summary>
        /// Obtiene expedientes por ID de caso
        /// </summary>
        public async Task<RecordMassagesResponse> GetRecordsByCaseId(long caseId)
        {
            var request = new
            {
                CaseId = caseId
            };

            var result = await RequestAsync<object>("api/Record/GetByCaseId", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<RecordMassagesResponse>(result.ToString());

            return response;
        }

        /// <summary>
        /// Obtiene expediente por ID
        /// </summary>
        public async Task<RecordMassagesResponse> GetRecordById(long id)
        {
            var request = new
            {
                Id = id
            };

            var result = await RequestAsync<object>("api/Record/Id", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<RecordMassagesResponse>(result.ToString());

            return response;
        }

        /// <summary>
        /// Elimina expediente (cambio de estatus)
        /// </summary>
        public async Task<RecordDeleteResponse> DeleteRecord(long id)
        {
            var request = new
            {
                Id = id
            };

            var result = await RequestAsync<object>("api/Record/Delete", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<RecordDeleteResponse>(result.ToString());

            return response;
        }

        /// <summary>
        /// Guarda o actualiza expediente
        /// </summary>
        public async Task<RecordMassagesResponse> SaveOrUpdateRecord(RecordObj record)
        {
            MapAuditFields(record);

            var request = new SaveOrUpdateRecordRequest()
            {
                Record = record
            };

            var result = await RequestAsync<object>("api/Record", HttpMethod.Post, request,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<RecordMassagesResponse>(result.ToString());

            return response;
        }
    }
}