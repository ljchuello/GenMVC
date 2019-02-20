using System;
using System.Web.UI;

namespace Libreria
{
    public class Notificacion
    {
        public static void Success(Page page, string mensaje)
        {
            try
            {
                mensaje = mensaje.Replace("'", "&#39;");
                mensaje = mensaje.Replace("\r", "");
                mensaje = mensaje.Replace("\n", " ");
                ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), $"Alertame('{mensaje}');", true);
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}