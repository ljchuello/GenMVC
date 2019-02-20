using System;
using System.Web.UI;
using Libreria;

namespace GenMVC
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UControl.EvitarDobleEnvioButton(this, btnConectarse);
            }
        }

        protected void btnPrueba_OnClick(object sender, EventArgs e)
        {
            Notificacion.Success(this, "abc-123");
        }

        #region Base de datos

        protected void btnConectarse_OnClick(object sender, EventArgs e)
        {
            try
            {
                // Validmoas que haya ingresado un servidor
                if (Cadena.Vacia(txtDbServidor.Text))
                {
                    Notificacion.Success(this, "Debe de ingresar un servidor SQL al cual conectarse");
                    return;
                }

                // Validmoas que haya ingresado un usario
                if (Cadena.Vacia(txtDbusuario.Text))
                {
                    Notificacion.Success(this, "Debe de ingresar un usuario SQL con el cual se conectará");
                    return;
                }

                // Validmoas que haya ingresado una contraseña
                if (Cadena.Vacia(txtContrasenia.Text))
                {
                    Notificacion.Success(this, "Debe de ingresar una contraseña de SQL");
                    return;
                }

                // Validmoas que haya ingresado una BD
                if (Cadena.Vacia(txtDbBaseDatos.Text))
                {
                    Notificacion.Success(this, "Debe ingresar la base de datos a la cual desea conectarse");
                    return;
                }
            }
            catch (Exception ex)
            {
                Notificacion.Success(this, $"Ha ocurrido un error; {ex.Message}");
            }
        }

        #endregion
    }
}