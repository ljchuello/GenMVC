using System;
using System.Collections.Generic;
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
                stringBuilder.AppendLine($"// Sermatick Cia Ltda");
                stringBuilder.AppendLine($"using System;");
                stringBuilder.AppendLine($"using System.Data.SqlClient;");
                stringBuilder.AppendLine($"using {acronimo.ProyectoModelo};");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"namespace {acronimo.ProyectoControlador}");
                stringBuilder.AppendLine($"{{");
                stringBuilder.AppendLine($"    public class {acronimo.AcronimoControlador}{tbl}");
                stringBuilder.AppendLine($"    {{");
                stringBuilder.AppendLine($"        private {acronimo.AcronimoModelo}{tbl} {acronimo.AcronimoModelo}{tbl} = new {acronimo.AcronimoModelo}{tbl}();");
                stringBuilder.AppendLine($"");

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

                        case "bool":
                            stringBuilder.AppendLine($"            {acronimo.AcronimoModelo}{tbl}.{row.Campo} = sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal(\"{row.Campo}\")) ? false : sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal(\"{row.Campo}\"));");
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
    }
}