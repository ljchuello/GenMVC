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
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"using System;");
                stringBuilder.AppendLine($"using System.Data;");
                stringBuilder.AppendLine($"using System.Data.SqlClient;");
                stringBuilder.AppendLine($"using System.Text;");
                stringBuilder.AppendLine($"using {acronimo.ProyectoModelo};");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"namespace {acronimo.ProyectoControlador}");
                stringBuilder.AppendLine($"{{");
                stringBuilder.AppendLine($"    public class {acronimo.AcronimoControlador}{tbl}");
                stringBuilder.AppendLine($"    {{");
                stringBuilder.AppendLine($"        private {acronimo.AcronimoModelo}{tbl} {acronimo.AcronimoModelo}{tbl} = new {acronimo.AcronimoModelo}{tbl}();");
                stringBuilder.AppendLine($"");

                #region Granular

                //List<OCampos> conWhere = listCampos.Where(x => x.Where).ToList();
                //List<OCampos> sinWhere = listCampos.Where(x => x.Where == false).ToList();

                List<string> selectList = new List<string>();
                foreach (var row in listCampos)
                {
                    selectList.Add($"tbl.{row.Campo}");
                }

                //stringBuilder.AppendLine($"private const string select = \"SELECT {string.Join(", ", selectList)}\" FROM {tbl} tbl");
                stringBuilder.AppendLine($"private const string Select = \"SELECT {string.Join(", ", selectList)} FROM {tbl} tbl\";");
                stringBuilder.AppendLine($"");

                #region First Select

                stringBuilder.AppendLine($"        public {acronimo.AcronimoModelo}{tbl} Select_{listCampos[0].Campo}({listCampos[0].TipoDotNet} {listCampos[0].Campo})");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            {acronimo.AcronimoModelo}{tbl} {acronimo.AcronimoModelo}{tbl} = new {acronimo.AcronimoModelo}{tbl}();");
                stringBuilder.AppendLine($"            try");
                stringBuilder.AppendLine($"            {{");
                stringBuilder.AppendLine($"                SqlCommand sqlCommand = new SqlCommand();");
                stringBuilder.AppendLine($"                sqlCommand.Connection = Conexion.Devolver();");
                stringBuilder.AppendLine($"                sqlCommand.CommandType = CommandType.Text;");
                stringBuilder.AppendLine($"                sqlCommand.CommandText = \"@Select WHERE tbl.{listCampos[0].Campo} = @{listCampos[0].Campo};\";");
                stringBuilder.AppendLine($"                sqlCommand.Parameters.AddWithValue(\"@Select\", Select);");
                stringBuilder.AppendLine($"                sqlCommand.Parameters.AddWithValue(\"@{listCampos[0].Campo}\", {listCampos[0].Campo});");
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
                stringBuilder.AppendLine($"                return {acronimo.AcronimoModelo}{tbl};");
                stringBuilder.AppendLine($"            }}");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");

                #endregion

                #region Insert

                stringBuilder.AppendLine($"        public static string Insert({acronimo.AcronimoModelo}{tbl} {acronimo.AcronimoModelo}{tbl})");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            StringBuilder stringBuilder = new StringBuilder();");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"\");");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"--  Insert {tbl}\");");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"INSERT INTO {tbl}\");");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"(\");");

                // Recorremos los item 1 de 2
                for (int i = 0; i <= listCampos.Count - 1; i++)
                {
                    // Validamos si es el ultimo
                    if (i != listCampos.Count - 1)
                    {
                        // No es el ultimo
                        stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"{listCampos[i].Campo}, -- {listCampos[i].Campo} | {listCampos[i].TipoSql} | {listCampos[i].TipoDotNet}\");");
                    }
                    else
                    {
                        // Si es el ultimo
                        stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"{listCampos[i].Campo} -- {listCampos[i].Campo} | {listCampos[i].TipoSql} | {listCampos[i].TipoDotNet}\");");
                    }
                }

                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\") VALUES (\");");

                // Recorremos los item 2 de 2
                for (int i = 0; i <= listCampos.Count - 1; i++)
                {
                    // Validamos si es el ultimo
                    if (i != listCampos.Count - 1)
                    {
                        // No es el ultimo
                        switch (listCampos[i].TipoSql)
                        {
                            case "int":
                            case "decimal":
                            case "bit":
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[i].Campo})}}, -- {listCampos[i].Campo} | {listCampos[i].TipoSql} | {listCampos[i].TipoDotNet}\");");
                                break;

                            default:
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"N'{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[i].Campo})}}', -- {listCampos[i].Campo} | {listCampos[i].TipoSql} | {listCampos[i].TipoDotNet}\");");
                                break;
                        }
                    }
                    else
                    {
                        // Si es el ultimo
                        switch (listCampos[i].TipoSql)
                        {
                            case "int":
                            case "decimal":
                            case "bit":
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[i].Campo})}} -- {listCampos[i].Campo} | {listCampos[i].TipoSql} | {listCampos[i].TipoDotNet}\");");
                                break;

                            default:
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"N'{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[i].Campo})}}' -- {listCampos[i].Campo} | {listCampos[i].TipoSql} | {listCampos[i].TipoDotNet}\");");
                                break;
                        }
                    }
                }

                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\");\");");
                stringBuilder.AppendLine($"            return stringBuilder.ToString();");
                stringBuilder.AppendLine($"        }}");

                #endregion

                #region Update

                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public static string Update({acronimo.AcronimoModelo}{tbl} {acronimo.AcronimoModelo}{tbl})");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            StringBuilder stringBuilder = new StringBuilder();");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"\");");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"--  Update {tbl}\");");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"UPDATE {tbl} SET\");");

                // Recorremos 1 de 2
                for (int i = 0; i <= listCampos.Count - 1; i++)
                {
                    if (i != listCampos.Count - 1)
                    {
                        // No es el ultimo
                        switch (listCampos[i].TipoSql)
                        {
                            case "int":
                            case "decimal":
                            case "bit":
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{listCampos[i].Campo} = {{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[i].Campo})}}, -- {listCampos[i].Campo} | {listCampos[i].TipoSql} | {listCampos[i].TipoDotNet}\");");
                                break;

                            default:
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{listCampos[i].Campo} = N'{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[i].Campo})}}', -- {listCampos[i].Campo} | {listCampos[i].TipoSql} | {listCampos[i].TipoDotNet}\");");
                                break;
                        }
                    }
                    else
                    {
                        // Si es el ultimo
                        switch (listCampos[i].TipoSql)
                        {
                            case "int":
                            case "decimal":
                            case "bit":
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{listCampos[i].Campo} = {{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[i].Campo})}} -- {listCampos[i].Campo} | {listCampos[i].TipoSql} | {listCampos[i].TipoDotNet}\");");
                                break;

                            default:
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{listCampos[i].Campo} = N'{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[i].Campo})}}' -- {listCampos[i].Campo} | {listCampos[i].TipoSql} | {listCampos[i].TipoDotNet}\");");
                                break;
                        }
                    }
                }

                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"WHERE\");");

                // Recorremos 2 de 2
                switch (listCampos[0].TipoSql)
                {
                    case "int":
                    case "decimal":
                    case "bit":
                        stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{listCampos[0].Campo} = {{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[0].Campo})}}; -- {listCampos[0].Campo} | {listCampos[0].TipoSql} | {listCampos[0].TipoDotNet}\");");
                        break;

                    default:
                        stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{listCampos[0].Campo} = N'{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[0].Campo})}}'; -- {listCampos[0].Campo} | {listCampos[0].TipoSql} | {listCampos[0].TipoDotNet}\");");
                        break;
                }

                stringBuilder.AppendLine($"            return stringBuilder.ToString();");
                stringBuilder.AppendLine($"        }}");

                #endregion

                #region Delete

                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"        public static string Delete({acronimo.AcronimoModelo}{tbl} {acronimo.AcronimoModelo}{tbl})");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            StringBuilder stringBuilder = new StringBuilder();");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"\");");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"--  Delete {tbl}\");");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"DELETE {tbl} WHERE\");");
                switch (listCampos[0].TipoSql)
                {
                    case "int":
                    case "decimal":
                    case "bit":
                        stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{listCampos[0].Campo} = {{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[0].Campo})}}; -- {listCampos[0].Campo} | {listCampos[0].TipoSql} | {listCampos[0].TipoDotNet}\");");
                        break;

                    default:
                        stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{listCampos[0].Campo} = N'{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[0].Campo})}}'; -- {listCampos[0].Campo} | {listCampos[0].TipoSql} | {listCampos[0].TipoDotNet}\");");
                        break;
                }
                stringBuilder.AppendLine($"            return stringBuilder.ToString();");
                stringBuilder.AppendLine($"        }}");
                stringBuilder.AppendLine($"");

                #endregion

                #endregion

                //#region Select´s

                //foreach (var rowSelect in listCampos.Where(x => x.Where).ToList())
                //{
                //    stringBuilder.AppendLine($"        public {acronimo.AcronimoModelo}{tbl} Select_{rowSelect.Campo}(string {rowSelect.Campo})");
                //    stringBuilder.AppendLine($"        {{");
                //    stringBuilder.AppendLine($"            try");
                //    stringBuilder.AppendLine($"            {{");
                //    stringBuilder.AppendLine($"                SqlCommand sqlCommand = new SqlCommand();");
                //    stringBuilder.AppendLine($"                sqlCommand.Connection = Conexion.ReadConnection();");
                //    stringBuilder.AppendLine($"                sqlCommand.CommandType = CommandType.Text;");

                //    switch (rowSelect.TipoSql)
                //    {
                //        case "decimal":
                //        case "int":
                //            stringBuilder.AppendLine($"                sqlCommand.CommandText = $\"{GenerarSelect(listCampos, tbl, $"WHERE {rowSelect.Campo} = @{rowSelect.Campo}")}\";");
                //            break;

                //        case "bool":
                //            stringBuilder.AppendLine($"                sqlCommand.CommandText = $\"{GenerarSelect(listCampos, tbl, $"WHERE {rowSelect.Campo} = @{rowSelect.Campo}")}\";");
                //            break;

                //        case "DateTime":
                //            stringBuilder.AppendLine($"                sqlCommand.CommandText = $\"SET DATEFORMAT YMD; {GenerarSelect(listCampos, tbl, $"WHERE {rowSelect.Campo} = @{rowSelect.Campo}'")}\";");
                //            break;

                //        default:
                //            stringBuilder.AppendLine($"                sqlCommand.CommandText = $\"{GenerarSelect(listCampos, tbl, $"WHERE {rowSelect.Campo} = @{rowSelect.Campo}")}\";");
                //            break;
                //    }

                //    stringBuilder.AppendLine($"                sqlCommand.Parameters.AddWithValue(\"@{rowSelect.Campo}\", {rowSelect.Campo});");
                //    stringBuilder.AppendLine($"                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())");
                //    stringBuilder.AppendLine($"                {{");
                //    stringBuilder.AppendLine($"                    while (sqlDataReader.Read())");
                //    stringBuilder.AppendLine($"                    {{");
                //    stringBuilder.AppendLine($"                        {acronimo.AcronimoModelo}{tbl} = Maker(sqlDataReader);");
                //    stringBuilder.AppendLine($"                    }}");
                //    stringBuilder.AppendLine($"                }}");
                //    stringBuilder.AppendLine($"                return {acronimo.AcronimoModelo}{tbl};");
                //    stringBuilder.AppendLine($"            }}");
                //    stringBuilder.AppendLine($"            catch (Exception ex)");
                //    stringBuilder.AppendLine($"            {{");
                //    stringBuilder.AppendLine($"                Console.WriteLine(ex);");
                //    stringBuilder.AppendLine($"                return new {acronimo.AcronimoModelo}{tbl}();");
                //    stringBuilder.AppendLine($"            }}");
                //    stringBuilder.AppendLine($"        }}");
                //    stringBuilder.AppendLine($"");
                //}

                //#endregion

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