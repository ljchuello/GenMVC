using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;

namespace GenMVC
{
    public class Sql
    {
        public string Servidor { set; get; } = string.Empty;
        public string Usuario { set; get; } = string.Empty;
        public string Contrasenia { set; get; } = string.Empty;
        public string BaseDatos { set; get; } = string.Empty;

        public bool ProbarConexion(Sql sql)
        {
            try
            {
                // Cadena de conexión
                using (SqlConnection sqlConnection = new SqlConnection($"data source={sql.Servidor}; initial catalog={sql.BaseDatos}; persist security info=True; user id={sql.Usuario}; password={sql.Contrasenia}; MultipleActiveResultSets=True;Connection Timeout=15;"))
                {
                    // Abrimos
                    sqlConnection.Open();

                    // Comando
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT GETDATE();";
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.ExecuteNonQuery();
                }

                // Libre de pecados
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ExisteTabla(Sql sql, string tbl)
        {
            bool exito = false;

            try
            {
                // Cadena de conexión
                using (SqlConnection sqlConnection = new SqlConnection($"data source={sql.Servidor}; initial catalog={sql.BaseDatos}; persist security info=True; user id={sql.Usuario}; password={sql.Contrasenia}; MultipleActiveResultSets=True;Connection Timeout=15;"))
                {
                    // Abrimos
                    sqlConnection.Open();

                    // Comando
                    SqlCommand sqlCommand = new SqlCommand
                    {
                        Connection = sqlConnection,
                        CommandText = $"SELECT 1 AS 'Existe' FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_NAME = '{tbl}';",
                        CommandType = CommandType.Text
                    };
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        exito = Convert.ToBoolean(reader[0]);
                    }
                }

                // Libre de pecados
                return exito;
            }
            catch (Exception ex)
            {
                return exito;
            }
        }

        public DataTable Select_Tables(Sql sql)
        {
            DataTable dataTable = new DataTable();
            try
            {
                // Cadena de conexión
                using (SqlConnection sqlConnection = new SqlConnection($"data source={sql.Servidor}; initial catalog={sql.BaseDatos}; persist security info=True; user id={sql.Usuario}; password={sql.Contrasenia}; MultipleActiveResultSets=True;Connection Timeout=15;"))
                {
                    // Abrimos
                    sqlConnection.Open();

                    // Comando
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE IN ('BASE TABLE', 'VIEW') ORDER BY TABLE_NAME;";
                    sqlCommand.CommandType = CommandType.Text;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(dataTable);
                }

                // Libre de pecados
                return dataTable;
            }
            catch (Exception)
            {
                return dataTable;
            }
        }

        public DataTable Select_Campos(Sql sql, string tbl)
        {
            DataTable dataTable = new DataTable();
            try
            {
                // Cadena de conexión
                using (SqlConnection sqlConnection = new SqlConnection($"data source={sql.Servidor}; initial catalog={sql.BaseDatos}; persist security info=True; user id={sql.Usuario}; password={sql.Contrasenia}; MultipleActiveResultSets=True;Connection Timeout=15;"))
                {
                    // Abrimos
                    sqlConnection.Open();

                    // Comando
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandText = $"SELECT  c.name AS 'Nombre', t.name AS 'TipoSql', CASE WHEN t.name = 'nvarchar' THEN 'string' WHEN t.name = 'int' THEN 'int' WHEN t.name = 'bit' THEN 'bool' WHEN t.name = 'datetime' THEN 'DateTime' WHEN t.name = 'decimal' THEN 'decimal' ELSE 'string' END AS 'TipoNET', c.max_length AS 'Largo', c.precision AS 'Precision', c.scale AS 'Escala', CASE WHEN i.index_id > 0 THEN '1' ELSE '0' END AS 'Where' FROM sys.columns c INNER JOIN  sys.types t ON c.user_type_id = t.user_type_id LEFT OUTER JOIN sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id LEFT OUTER JOIN  sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id WHERE c.object_id = OBJECT_ID('{tbl}');";
                    sqlCommand.CommandType = CommandType.Text;
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    sqlDataAdapter.Fill(dataTable);
                }

                // Libre de pecados
                return dataTable;
            }
            catch (Exception)
            {
                return dataTable;
            }
        }

        private static string directorio = @"C:\GenMVC\";
        private static readonly string archivo = $"{directorio}\\sql.config";

        public static void Escribir(Sql sql)
        {
            try
            {
                // Validamos que exista u ndirectorio
                if (!Directory.Exists(directorio))
                {
                    // Creamos el directorio
                    Directory.CreateDirectory(directorio);
                }

                // Validamos que exista el archivo
                if (!File.Exists(archivo))
                {
                    // Creamos el archivos
                    File.WriteAllText(archivo, string.Empty);
                }

                // Escribimos en el archivoo
                File.WriteAllText(archivo, Fichero.ObjectToJson(sql));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static Sql Leer()
        {
            Sql sql = new Sql();

            try
            {
                // Validamos que exista un directorio
                if (!Directory.Exists(directorio))
                {
                    // Creamos el directorio
                    Directory.CreateDirectory(directorio);
                }

                // Validamos que exista el archivo
                if (!File.Exists(archivo))
                {
                    // Creamos el archivos
                    File.WriteAllText(archivo, string.Empty);
                }

                // Escribimos en el archivoo
                string temp = File.ReadAllText(archivo);

                // Convertimos
                sql = JsonConvert.DeserializeObject<Sql>(temp) ?? new Sql();

                // Libre de pecados
                return sql;
            }
            catch (Exception)
            {
                return sql;
            }
        }
    }
}