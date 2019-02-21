using System;
using System.Data;
using System.Web.UI;
using Libreria;

namespace GenMVC
{
    public partial class Default : Page
    {
        private Sql _sql = new Sql();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Cargamos la sesión anterior
                _sql = Fichero.Leer();

                txtDbServidor.Text = _sql.Servidor;
                txtDbusuario.Text = _sql.Usuario;
                txtContrasenia.Text = _sql.Contrasenia;
                txtDbBaseDatos.Text = _sql.BaseDatos;

                // Evitamso el doble click
                UControl.EvitarDobleEnvioButton(this, btnConectarse);
                UControl.EvitarDobleEnvioButton(this, btnBdGenerar);
            }
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

                // Llenamos
                _sql.Servidor = txtDbServidor.Text;
                _sql.Usuario = txtDbusuario.Text;
                _sql.Contrasenia = txtContrasenia.Text;
                _sql.BaseDatos = txtDbBaseDatos.Text;

                // Validamos si funciona
                if (!_sql.ProbarConexion(_sql))
                {
                    Notificacion.Success(this, "No se ha podido establecer una conexión con la BD =(");
                    return;
                }

                // Guardamos los cambios
                Fichero.Escribir(_sql);

                // Obtenemos las tablas
                DataTable dataTable = _sql.Select_Tables(_sql);
                ddlBdTablas.DataSource = dataTable;
                ddlBdTablas.DataTextField = "TABLE_NAME";
                ddlBdTablas.DataValueField = "TABLE_NAME";
                ddlBdTablas.DataBind();

                // Libre de pecados
                Notificacion.Success(this, "Se ha establecido la conexión con éxito");
            }
            catch (Exception ex)
            {
                Notificacion.Success(this, $"Ha ocurrido un error; {ex.Message}");
            }
        }

        protected void btnBdGenerar_OnClick(object sender, EventArgs e)
        {
            try
            {
                // Validamos que este buena la conexión
                _sql = Fichero.Leer();

                // Validamos si conecta
                if (!_sql.ProbarConexion(_sql))
                {
                    Notificacion.Success(this, $"No se ha podido establecer una conexión con la BD");
                    return;
                }

                // Existe la tabla mencionada?
                if (!_sql.ExisteTabla(_sql, ddlBdTablas.SelectedValue))
                {
                    Notificacion.Success(this, $"No exista la tabla {ddlBdTablas.SelectedValue}");
                    return;
                }

                // Generamos la consulta
                DataTable dataTable = _sql.Select_Campos(_sql, ddlBdTablas.SelectedValue);

                // Le mostramos eso al grid
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Notificacion.Success(this, $"Ha ocurrido un error; {ex.Message}");
            }
        }

        #endregion
    }
}