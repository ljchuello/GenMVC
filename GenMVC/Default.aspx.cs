using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Libreria;

namespace GenMVC
{
    public partial class Default : Page
    {
        private Sql _sql = new Sql();
        private Acronimo _acronimo = new Acronimo();
        private OCampos _oCampos = new OCampos();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                // Cargamos la sesión anterior
                _sql = Sql.Leer();
                _acronimo = Acronimo.Leer();

                txtDbServidor.Text = _sql.Servidor;
                txtDbusuario.Text = _sql.Usuario;
                txtContrasenia.Text = _sql.Contrasenia;
                txtDbBaseDatos.Text = _sql.BaseDatos;

                txtProyectoModelo.Text = _acronimo.ProyectoModelo;
                txtAcronimoModelo.Text = _acronimo.AcronimoModelo;
                txtProyectoControlador.Text = _acronimo.ProyectoControlador;
                txtAcronimoControlador.Text = _acronimo.AcronimoControlador;
                txtProyectoContenedor.Text = _acronimo.ProyectoContenedor;

                // Evitamso el doble click
                UControl.EvitarDobleEnvioButton(this, btnConectarse);
                UControl.EvitarDobleEnvioButton(this, btnBdGenerar);
                UControl.EvitarDobleEnvioButton(this, btnGuardarAcronimo);
            }
        }

        #region Base de datos

        protected void btnConectarse_OnClick(object sender, EventArgs e)
        {
            try
            {
                // Validamos la BD
                if (!ValidarBd())
                {
                    return;
                }

                // Guardamos los cambios
                Sql.Escribir(_sql);

                // Obtenemos las tablas
                DataTable dataTable = _sql.Select_Tables(_sql);
                ddlBdTablas.DataSource = dataTable;
                ddlBdTablas.DataTextField = "TABLE_NAME";
                ddlBdTablas.DataValueField = "TABLE_NAME";
                ddlBdTablas.DataBind();

                btnBdGenerar_OnClick(null, null);

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
                _sql = Sql.Leer();

                // Validamos si conecta
                if (!_sql.ProbarConexion(_sql))
                {
                    Notificacion.Success(this, $"No se ha podido establecer una conexión con la BD");
                    return;
                }

                // Existe la tabla mencionada?
                //if (!_sql.ExisteTabla(_sql, ddlBdTablas.SelectedValue))
                //{
                //    Notificacion.Success(this, $"No exista la tabla {ddlBdTablas.SelectedValue}");
                //    return;
                //}

                // Generamos la consulta
                DataTable dataTable = _sql.Select_Campos(_sql, ddlBdTablas.SelectedValue);

                // Le mostramos eso al grid
                GridView1.DataSource = dataTable;
                GridView1.DataBind();

                btngenerarClases_OnClick(null, null);
            }
            catch (Exception ex)
            {
                Notificacion.Success(this, $"Ha ocurrido un error; {ex.Message}");
            }
        }

        bool ValidarBd()
        {
            try
            {
                // Validmoas que haya ingresado un servidor
                if (Cadena.Vacia(txtDbServidor.Text))
                {
                    Notificacion.Success(this, "Debe de ingresar un servidor SQL al cual conectarse");
                    return false;
                }

                // Validmoas que haya ingresado un usario
                if (Cadena.Vacia(txtDbusuario.Text))
                {
                    Notificacion.Success(this, "Debe de ingresar un usuario SQL con el cual se conectará");
                    return false;
                }

                // Validmoas que haya ingresado una contraseña
                if (Cadena.Vacia(txtContrasenia.Text))
                {
                    Notificacion.Success(this, "Debe de ingresar una contraseña de SQL");
                    return false;
                }

                // Validmoas que haya ingresado una BD
                if (Cadena.Vacia(txtDbBaseDatos.Text))
                {
                    Notificacion.Success(this, "Debe ingresar la base de datos a la cual desea conectarse");
                    return false;
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
                    return false;
                }

                // Libre de pecados
                return true;
            }
            catch (Exception ex)
            {
                Notificacion.Success(this, $"Ha ocurrido un error; {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Acrónimos

        protected void btnGuardarAcronimo_OnClick(object sender, EventArgs e)
        {
            try
            {
                // Validamos
                if (!ValidarAcronimos())
                {
                    return;
                }

                // Seteamos
                _acronimo.ProyectoModelo = txtProyectoModelo.Text;
                _acronimo.AcronimoModelo = txtAcronimoModelo.Text;
                _acronimo.ProyectoControlador = txtProyectoControlador.Text;
                _acronimo.AcronimoControlador = txtAcronimoControlador.Text;
                _acronimo.ProyectoContenedor = txtProyectoContenedor.Text;

                // Guardamos
                Acronimo.Escribir(_acronimo);

                // Libre de pecados
                Notificacion.Success(this, $"Se ha guardado el acrónimo");
            }
            catch (Exception ex)
            {
                Notificacion.Success(this, $"Ha ocurrido un error; {ex.Message}");
            }
        }

        bool ValidarAcronimos()
        {
            try
            {
                // Validamos que haya ingresado un modelo
                if (Cadena.Vacia(txtProyectoModelo.Text))
                {
                    Notificacion.Success(this, "Debe ingresar un nombre de proyecto del modelo");
                    return false;
                }

                // Validamos que haya ingresado un acronimo paramodelo
                if (Cadena.Vacia(txtAcronimoModelo.Text))
                {
                    Notificacion.Success(this, "Debe ingresar un acronimo para el modelo");
                    return false;
                }

                // Validamos que haya ingresado un controlador
                if (Cadena.Vacia(txtProyectoControlador.Text))
                {
                    Notificacion.Success(this, "Debe ingresar el nombre de proyecto del modelo");
                    return false;
                }


                // Validamos que haya ingresado un controlador
                if (Cadena.Vacia(txtAcronimoControlador.Text))
                {
                    Notificacion.Success(this, "Debe ingresar el nombre del acronimo del controlador");
                    return false;
                }
                // Validamos que haya ingresado un contenedor
                if (Cadena.Vacia(txtProyectoContenedor.Text))
                {
                    Notificacion.Success(this, "Debe ingresar el nombre de proyecto contenedor");
                    return false;
                }

                // Libre de pecados
                return true;
            }
            catch (Exception ex)
            {
                Notificacion.Success(this, $"Ha ocurrido un error; {ex.Message}");
                return false;
            }
        }

        #endregion

        protected void btngenerarClases_OnClick(object sender, EventArgs e)
        {
            List<OCampos> listOCampos = new List<OCampos>();
            try
            {
                // Llenamos
                foreach (GridViewRow row in GridView1.Rows)
                {
                    // Seteamos el objeto
                    _oCampos = new OCampos();
                    _oCampos.Campo = row.Cells[0].Text;
                    _oCampos.TipoSql = row.Cells[1].Text;
                    _oCampos.TipoDotNet = row.Cells[2].Text;
                    _oCampos.Largo = Convert.ToInt32(row.Cells[3].Text);
                    _oCampos.Precision = Convert.ToInt32(row.Cells[4].Text);
                    _oCampos.Escala = Convert.ToInt32(row.Cells[5].Text);
                    _oCampos.Where = row.Cells[6].Text == "1";

                    // Agregamos
                    listOCampos.Add(_oCampos);
                }

                // Leer acronimo
                _acronimo = Acronimo.Leer();

                // Generamos el modelo
                txtModelo.InnerText = Modelo.Generar(listOCampos, _acronimo, ddlBdTablas.SelectedValue);

                // Generamos el controlador
                txtControlador.InnerText = Controlador.Generar(listOCampos, _acronimo, ddlBdTablas.SelectedValue);

                // Granular
                txtGranular.InnerText = Granular.Generar(listOCampos, _acronimo, ddlBdTablas.SelectedValue);

            }
            catch (Exception ex)
            {
                Notificacion.Success(this, $"Ha ocurrido un error; {ex.Message}");
            }
        }
    }
}