using DespachoJuridicoDESIMVC.Helpers;
using DespachoJuridicoDESIMVC.Models.Autentication;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace DespachoJuridicoDESIMVC.DAL
{
    public partial class HttpClientConnection : HttpClientBase
    {
        private TokenCookie token;
        public HttpClientConnection(string baseUrl = "") : base(baseUrl)
        {
            token = SessionHelper.GetSessionUser();
        }

        public async Task<Token> GetToken(string user, string pass)
        {
            return await TokenAsync<Token>("token",
                new []
                {
                    new KeyValuePair<string, string>("grant_type","password"),
                    new KeyValuePair<string, string>("UserName",user),
                    new KeyValuePair<string, string>("Password",pass)
                }, "application/x-www-url-formencoded");
        }

        // Existing methods omitted for brevity...

        /// <summary>
        /// Populate audit properties (Created* or Updated*) on the supplied entity.
        /// Works with objects that implement IAuditable or via reflection (property/field names).
        /// </summary>
        public T MapAuditFields<T>(T entity) where T : class
        {
            if (entity == null) return null;

            var session = SessionHelper.GetSessionUser();
            if (session == null) return entity; // no session available -> skip

            var userName = session.UserName ?? string.Empty;
            var now = SessionHelper.GetDateCenterMexico();

            // Try direct interface
            if (entity is DespachoJuridicoDESIMVC.Models.IAuditable auditable)
            {
                var idObj = GetIdValue(entity);
                var id = ConvertIdToLong(idObj);
                if (id == 0)
                {
                    auditable.CreatedBy = userName;
                    auditable.CreatedDt = now;
                }
                else
                {
                    auditable.UpdatedBy = userName;
                    auditable.UpdatedDt = now;
                }

                return entity;
            }

            // Reflection fallback: look for Id, CreatedBy, CreatedDt, UpdatedBy, UpdatedDt
            var type = entity.GetType();
            var idValue = GetIdValue(entity);
            var idLong = ConvertIdToLong(idValue);
            if (idLong == 0)
            {
                SetPropertyOrField(type, entity, "CreatedBy", userName);
                SetPropertyOrField(type, entity, "CreatedDt", now);
            }
            else
            {
                SetPropertyOrField(type, entity, "UpdatedBy", userName);
                SetPropertyOrField(type, entity, "UpdatedDt", now);
            }

            return entity;
        }

        private static object GetIdValue(object obj)
        {
            if (obj == null) return null;
            var t = obj.GetType();
            var prop = t.GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
            if (prop != null) return prop.GetValue(obj);
            var field = t.GetField("Id", BindingFlags.Public | BindingFlags.Instance);
            if (field != null) return field.GetValue(obj);
            return null;
        }

        private static long ConvertIdToLong(object idValue)
        {
            if (idValue == null) return 0;
            try
            {
                return Convert.ToInt64(idValue);
            }
            catch
            {
                return 0;
            }
        }

        private static void SetPropertyOrField(Type type, object target, string name, object value)
        {
            var prop = type.GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                try
                {
                    // attempt to convert value to property type
                    var converted = ConvertToType(value, prop.PropertyType);
                    prop.SetValue(target, converted);
                    return;
                }
                catch
                {
                    // ignore conversion and try to set directly below
                }
            }

            var field = type.GetField(name, BindingFlags.Public | BindingFlags.Instance);
            if (field != null)
            {
                try
                {
                    var converted = ConvertToType(value, field.FieldType);
                    field.SetValue(target, converted);
                }
                catch
                {
                    // swallow - best-effort
                }
            }
        }

        private static object ConvertToType(object value, Type targetType)
        {
            if (value == null) return null;
            if (targetType.IsAssignableFrom(value.GetType())) return value;

            if (Nullable.GetUnderlyingType(targetType) != null)
            {
                targetType = Nullable.GetUnderlyingType(targetType);
            }

            return Convert.ChangeType(value, targetType);
        }
    }
}