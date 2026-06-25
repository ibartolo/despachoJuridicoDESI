using DespachoJuridicoDESIMVC.DAL;
using DespachoJuridicoDESIMVC.Helpers;
using DespachoJuridicoDESIMVC.Models.Autentication;
using DespachoJuridicoDESIMVC.Models.Customer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static DespachoJuridicoDESIMVC.Helpers.FiltersHelper;

namespace DespachoJuridicoDESIMVC.Controllers
{
    [Autenticated]
    public class CustomerController : BaseController
    {
        #region Views
        public async Task<ActionResult> Create(long id = 0)
        {
            var response = await httpClient.GetClienteById(id);
            if (response?.Result?.Successful == true && response.ClientObjs != null && response.ClientObjs.Count == 1)
            {
                return View(response.ClientObjs.FirstOrDefault());
            }

            return View(new ClienteObj ());
        }
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region Data Access
        public async Task<string> GetAllClientes()
        {
            var response = await httpClient.GetAllClientes();

            return JsonConvert.SerializeObject(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveOrUpdateCliente(ClienteObj client)
        {
            if (client == null)
            {
                ModelState.AddModelError(string.Empty, "Datos de cliente inválidos.");
                return View("Create");
            }

            try
            {
                var response = await httpClient.SaveOrUpdateCliente(client);

                if (response?.Result?.Successful == true)
                {
                    // Si es un cliente nuevo (Id = 0), enviar notificación
                    if (client.Id == 0 && IsValidEmail(client.Correo))
                    {
                        EnviarNotificacionAltaCliente(client);
                    }

                    return RedirectToAction("Index");
                }

                if (response?.Result?.SystemMessages != null && response.Result.SystemMessages.Any())
                {
                    foreach (var msg in response.Result.SystemMessages)
                    {
                        ModelState.AddModelError(string.Empty, msg?.Message ?? "Error desconocido.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar el cliente.");
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar el cliente. Intente nuevamente más tarde.");
            }

            return View("Create", client);
        }

        /// <summary>
        /// Envía notificación de alta de nuevo cliente
        /// </summary>
        private void EnviarNotificacionAltaCliente(ClienteObj client)
        {
            try
            {
                // Verificar que el cliente tenga un correo electrónico
                if (string.IsNullOrEmpty(client.Correo))
                {
                    return;
                }

                // Leer el template del correo
                var templatePath = HttpContext.Server.MapPath("~/Templates/Template_AltaUsuario.html");
                var mensaje = System.IO.File.ReadAllText(templatePath);

                // Reemplazar variables
                mensaje = mensaje.Replace("{{NombreCliente}}", client.Nombre);
                mensaje = mensaje.Replace("{{CorreoCliente}}", client.Correo);
                mensaje = mensaje.Replace("{{TelefonoCliente}}", client.Telefono);

                // Preparar destinatarios
                var para = new List<string> { client.Correo };

                // Enviar correo
                EmailHelper.EnvioEmaiil(para, "Bienvenido al Despacho Jurídico Salas y Asociados", mensaje);
            }
            catch (Exception ex)
            {
                // Registrar error pero no interrumpir el flujo
                System.Diagnostics.Debug.WriteLine($"Error al enviar correo de alta de cliente: {ex.Message}");
            }
        }
        #endregion
    }
}