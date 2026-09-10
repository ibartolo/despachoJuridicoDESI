using DespachoJuridicoDESIMVC.Models.Dashboard;
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
        /// Obtiene los datos del dashboard
        /// </summary>
        public async Task<DashboardMassagesResponse> GetDashboardData()
        {
            var result = await RequestAsync<object>("api/Dashboard/GetDashboardData", HttpMethod.Post, null,
                new Func<string, string>((responseString) =>
                {
                    return responseString;
                }), token.Token.access_token);

            var response = JsonConvert.DeserializeObject<DashboardMassagesResponse>(result.ToString());

            return response;
        }
    }
}