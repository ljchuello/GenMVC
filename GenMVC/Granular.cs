using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenMVC
{
    public class Granular
    {
        public static string Generar(List<OCampos> listCampos, Acronimo acronimo, string tbl)
        {
            StringBuilder stringBuilder = new StringBuilder();

            try
            {
                // Empezamos
                stringBuilder.AppendLine($"// O{tbl}.cs");
                stringBuilder.AppendLine($"// Clase generada por");
                stringBuilder.AppendLine($"// Sermatick Cia Ltda");
                stringBuilder.AppendLine($"using System.Text;");
                stringBuilder.AppendLine($"using {acronimo.ProyectoModelo};");
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"namespace Libreria.SqlServer");
                stringBuilder.AppendLine($"{{");
                stringBuilder.AppendLine($"    // ReSharper disable once InconsistentNaming");
                stringBuilder.AppendLine($"    public class O{tbl}");
                stringBuilder.AppendLine($"    {{");

                #region Insert

                stringBuilder.AppendLine($"        // ReSharper disable once InconsistentNaming");
                stringBuilder.AppendLine($"        public string Insert({acronimo.AcronimoModelo}{tbl} {acronimo.AcronimoModelo}{tbl})");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            StringBuilder stringBuilder = new StringBuilder();");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"\");");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"--  Insert {tbl}\");");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"INSERT INTO {tbl}\");");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"(\");");

                // Recorremos los item 1 de 2
                for (int i = 0; i <= listCampos.Count-1; i++)
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
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"'{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[i].Campo})}}', -- {listCampos[i].Campo} | {listCampos[i].TipoSql} | {listCampos[i].TipoDotNet}\");");
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
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"'{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{listCampos[i].Campo})}}' -- {listCampos[i].Campo} | {listCampos[i].TipoSql} | {listCampos[i].TipoDotNet}\");");
                                break;
                        }
                    }
                }

                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\");\");");
                stringBuilder.AppendLine($"            return stringBuilder.ToString();");
                stringBuilder.AppendLine($"        }}");

                #endregion

                #region Update

                stringBuilder.AppendLine($"        // ReSharper disable once InconsistentNaming");
                stringBuilder.AppendLine($"        public string Update({acronimo.AcronimoModelo}{tbl} {acronimo.AcronimoModelo}{tbl})");
                stringBuilder.AppendLine($"        {{");
                stringBuilder.AppendLine($"            StringBuilder stringBuilder = new StringBuilder();");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"\");");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"--  Update {tbl}\");");
                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"UPDATE {tbl} SET\");");

                List<OCampos> conWhere = listCampos.Where(x => x.Where).ToList();
                List<OCampos> sinWhere = listCampos.Where(x => x.Where == false).ToList();

                // Recorremos 1 de 2
                for (int i = 0; i <= sinWhere.Count-1; i++)
                {
                    if (i != sinWhere.Count - 1)
                    {
                        // No es el ultimo
                        switch (sinWhere[i].TipoSql)
                        {
                            case "int":
                            case "decimal":
                            case "bit":
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{sinWhere[i].Campo} = {{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{sinWhere[i].Campo})}}, -- {sinWhere[i].Campo} | {sinWhere[i].TipoSql} | {sinWhere[i].TipoDotNet}\");");
                                break;

                            default:
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{sinWhere[i].Campo} = '{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{sinWhere[i].Campo})}}', -- {sinWhere[i].Campo} | {sinWhere[i].TipoSql} | {sinWhere[i].TipoDotNet}\");");
                                break;
                        }
                    }
                    else
                    {
                        // Si es el ultimo
                        switch (sinWhere[i].TipoSql)
                        {
                            case "int":
                            case "decimal":
                            case "bit":
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{sinWhere[i].Campo} = {{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{sinWhere[i].Campo})}} -- {sinWhere[i].Campo} | {sinWhere[i].TipoSql} | {sinWhere[i].TipoDotNet}\");");
                                break;

                            default:
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{sinWhere[i].Campo} = '{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{sinWhere[i].Campo})}}' -- {sinWhere[i].Campo} | {sinWhere[i].TipoSql} | {sinWhere[i].TipoDotNet}\");");
                                break;
                        }
                    }
                }

                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\"WHERE\");");

                // Recorremos 2 de 2
                for (int i = 0; i <= conWhere.Count - 1; i++)
                {
                    if (i != conWhere.Count - 1)
                    {
                        // No es el ultimo
                        switch (conWhere[i].TipoSql)
                        {
                            case "int":
                            case "decimal":
                            case "bit":
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{conWhere[i].Campo} = {{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{conWhere[i].Campo})}}, -- {conWhere[i].Campo} | {conWhere[i].TipoSql} | {conWhere[i].TipoDotNet}\");");
                                break;

                            default:
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{conWhere[i].Campo} = '{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{conWhere[i].Campo})}}', -- {conWhere[i].Campo} | {conWhere[i].TipoSql} | {conWhere[i].TipoDotNet}\");");
                                break;
                        }
                    }
                    else
                    {
                        // Si es el ultimo
                        switch (conWhere[i].TipoSql)
                        {
                            case "int":
                            case "decimal":
                            case "bit":
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{conWhere[i].Campo} = {{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{conWhere[i].Campo})}} -- {conWhere[i].Campo} | {conWhere[i].TipoSql} | {conWhere[i].TipoDotNet}\");");
                                break;

                            default:
                                stringBuilder.AppendLine($"            stringBuilder.AppendLine($\"{conWhere[i].Campo} = '{{Tools.Remplazar({acronimo.AcronimoModelo}{tbl}.{conWhere[i].Campo})}}' -- {conWhere[i].Campo} | {conWhere[i].TipoSql} | {conWhere[i].TipoDotNet}\");");
                                break;
                        }
                    }
                }

                stringBuilder.AppendLine($"            stringBuilder.AppendLine(\");\");");
                stringBuilder.AppendLine($"            return stringBuilder.ToString();");
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