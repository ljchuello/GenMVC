using System;
using System.Collections.Generic;
using System.Text;

namespace GenMVC
{
    public static class Modelo
    {
        public static string Generar(List<OCampos> listCampos, Acronimo acronimo, string tbl)
        {
            StringBuilder stringBuilder = new StringBuilder();

            try
            {
                // Empezamos
                stringBuilder.AppendLine($"// {acronimo.AcronimoModelo}{tbl}.cs");
                stringBuilder.AppendLine($"// Clase generada por");
                stringBuilder.AppendLine($"// Leonardo Chuello");
                stringBuilder.AppendLine($"// {DateTime.Now:yyyy-MM-dd}");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"namespace {acronimo.ProyectoModelo}");
                stringBuilder.AppendLine($"{{");
                stringBuilder.AppendLine($"    public class {acronimo.AcronimoModelo}{tbl}");
                stringBuilder.AppendLine($"    {{");

                // Recorremos los campos
                foreach (var row in listCampos)
                {
                    switch (row.TipoDotNet)
                    {
                        case "decimal":
                            stringBuilder.AppendLine($"        public decimal {row.Campo} {{ set; get; }} = 0;");
                            break;

                        case "int":
                            stringBuilder.AppendLine($"        public int {row.Campo} {{ set; get; }} = 0;");
                            break;

                        case "DateTime":
                            stringBuilder.AppendLine($"        public DateTime {row.Campo} {{ set; get; }} = new DateTime(1900, 01,01);");
                            break;

                        case "bool":
                            stringBuilder.AppendLine($"        public bool {row.Campo} {{ set; get; }} = false;");
                            break;

                        default:
                            stringBuilder.AppendLine($"        public string {row.Campo} {{ set; get; }} = string.Empty;");
                            break;
                    }
                }

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