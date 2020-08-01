using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenMVC
{
    public static class Controlador
    {
        public static string Generar(List<OCampos> listCampos, Acronimo acronimo, string tbl)
        {
            StringBuilder stringBuilder = new StringBuilder();

            try
            {
                // Empezamos
                stringBuilder.AppendLine($"// {acronimo.AcronimoControlador}{tbl}.cs");
                stringBuilder.AppendLine($"// Clase generada por");
                stringBuilder.AppendLine($"// Leonardo Chuello");
                stringBuilder.AppendLine($"// {DateTime.Now:yyyy-MM-dd}");
                stringBuilder.AppendLine($"using System;");
                stringBuilder.AppendLine($"using System.Data;");
                stringBuilder.AppendLine($"using System.Data.SqlClient;");
                stringBuilder.AppendLine($"using {acronimo.ProyectoModelo};");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"namespace {acronimo.ProyectoControlador}");
                stringBuilder.AppendLine($"{{");
                stringBuilder.AppendLine($"    public class {acronimo.AcronimoControlador}{tbl}");
                stringBuilder.AppendLine($"    {{");
                stringBuilder.AppendLine($"        private {acronimo.AcronimoModelo}{tbl} {acronimo.AcronimoModelo}{tbl} = new {acronimo.AcronimoModelo}{tbl}();");
                stringBuilder.AppendLine($"");

                #region Select´s

                foreach (var rowSelect in listCampos.Where(x => x.Where).ToList())
                {
                    stringBuilder.AppendLine($"        public {acronimo.AcronimoModelo}{tbl} Select_{rowSelect.Campo}(string {rowSelect.Campo})");
                    stringBuilder.AppendLine($"        {{");
                    stringBuilder.AppendLine($"            try");
                    stringBuilder.AppendLine($"            {{");
                    stringBuilder.AppendLine($"                SqlCommand sqlCommand = new SqlCommand();");
                    stringBuilder.AppendLine($"                sqlCommand.Connection = Conexion.Devolver_SoloLectura();");
                    stringBuilder.AppendLine($"                sqlCommand.CommandType = CommandType.Text;");

                    switch (rowSelect.TipoSql)
                    {
                        case "decimal":
                        case "int":
                            stringBuilder.AppendLine($"                sqlCommand.CommandText = $\"{GenerarSelect(listCampos, tbl, $"WHERE {rowSelect.Campo} = @{rowSelect.Campo}")}\";");
                            break;

                        case "bool":
                            stringBuilder.AppendLine($"                sqlCommand.CommandText = $\"{GenerarSelect(listCampos, tbl, $"WHERE {rowSelect.Campo} = @{rowSelect.Campo}")}\";");
                            break;

                        case "DateTime":
                            stringBuilder.AppendLine($"                sqlCommand.CommandText = $\"SET DATEFORMAT YMD; {GenerarSelect(listCampos, tbl, $"WHERE {rowSelect.Campo} = @{rowSelect.Campo}'")}\";");
                            break;

                        default:
                            stringBuilder.AppendLine($"                sqlCommand.CommandText = $\"{GenerarSelect(listCampos, tbl, $"WHERE {rowSelect.Campo} = @{rowSelect.Campo}")}\";");
                            break;
                    }

                    stringBuilder.AppendLine($"                sqlCommand.Parameters.AddWithValue(\"@{rowSelect.Campo}\", {rowSelect.Campo});");
                    stringBuilder.AppendLine($"                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())");
                    stringBuilder.AppendLine($"                {{");
                    stringBuilder.AppendLine($"                    while (sqlDataReader.Read())");
                    stringBuilder.AppendLine($"                    {{");
                    stringBuilder.AppendLine($"                        {acronimo.AcronimoModelo}{tbl} = Maker(sqlDataReader);");
                    stringBuilder.AppendLine($"                    }}");
                    stringBuilder.AppendLine($"                }}");
                    stringBuilder.AppendLine($"                return {acronimo.AcronimoModelo}{tbl};");
                    stringBuilder.AppendLine($"            }}");
                    stringBuilder.AppendLine($"            catch (Exception ex)");
                    stringBuilder.AppendLine($"            {{");
                    stringBuilder.AppendLine($"                Console.WriteLine(ex);");
                    stringBuilder.AppendLine($"                return new {acronimo.AcronimoModelo}{tbl}();");
                    stringBuilder.AppendLine($"            }}");
                    stringBuilder.AppendLine($"        }}");
                    stringBuilder.AppendLine($"");
                }

                #endregion

                #region maker

                stringBuilder.AppendLine($"        protected virtual {acronimo.AcronimoModelo}{tbl} Maker(SqlDataReader sqlDataReader)");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            {acronimo.AcronimoModelo}{tbl} = new {acronimo.AcronimoModelo}{tbl}();");

                // Recorremos
                foreach (var row in listCampos)
                {
                    switch (row.TipoDotNet)
                    {
                        case "decimal":
                            stringBuilder.AppendLine($"            {acronimo.AcronimoModelo}{tbl}.{row.Campo} = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal(\"{row.Campo}\")) ? 0 : sqlDataReader.GetDecimal(sqlDataReader.GetOrdinal(\"{row.Campo}\"));");
                            break;

                        case "int":
                            stringBuilder.AppendLine($"            {acronimo.AcronimoModelo}{tbl}.{row.Campo} = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal(\"{row.Campo}\")) ? 0 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal(\"{row.Campo}\"));");
                            break;

                        case "DateTime":
                            stringBuilder.AppendLine($"            {acronimo.AcronimoModelo}{tbl}.{row.Campo} = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal(\"{row.Campo}\")) ? new DateTime(1900, 01, 01) : sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal(\"{row.Campo}\"));");
                            break;

                        //case "bool":
                        //    stringBuilder.AppendLine($"            {acronimo.AcronimoModelo}{tbl}.{row.Campo} = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal(\"{row.Campo}\")) ? false : sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal(\"{row.Campo}\"));");
                        //    break;

                        case "bool":
                            stringBuilder.AppendLine($"            {acronimo.AcronimoModelo}{tbl}.{row.Campo} = !sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal(\"{row.Campo}\")) && sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal(\"{row.Campo}\"));");
                            break;

                        default:
                            stringBuilder.AppendLine($"            {acronimo.AcronimoModelo}{tbl}.{row.Campo} = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal(\"{row.Campo}\")) ? string.Empty : sqlDataReader.GetString(sqlDataReader.GetOrdinal(\"{row.Campo}\"));");
                            break;
                    }
                }

                stringBuilder.AppendLine($"            return {acronimo.AcronimoModelo}{tbl};");
                stringBuilder.AppendLine($"        }}");

                #endregion

                stringBuilder.AppendLine($"    }}");
                stringBuilder.AppendLine($"}}");

                // Libre de pecados
                return stringBuilder.ToString();
            }
            catch (Exception)
            {
                return stringBuilder.ToString();
            }
        }

        private static string GenerarSelect(List<OCampos> listCampos, string tbl, string terminacion)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("SELECT ");

            for (int i = 0; i < listCampos.Count; i++)
            {
                // Ult posicion
                if (i != listCampos.Count - 1)
                {
                    // No es diferente
                    stringBuilder.Append($"{listCampos[i].Campo}, ");
                }
                else
                {
                    // Si es difente
                    stringBuilder.Append($"{listCampos[i].Campo} ");
                }
            }

            stringBuilder.Append($"FROM {tbl} {terminacion}");

            return stringBuilder.ToString();
        }
    }
}