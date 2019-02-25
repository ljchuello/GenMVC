<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GenMVC.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>Generador MVC</title>

    <link href="/bootstrap/v3.3.7/css/bootstrap.css" rel="stylesheet" />
    <link href="/base/css/stylesheet.css" rel="stylesheet" />

    <script src="/jquery/v3.3.1/jquery-3.3.1.js"></script>
    <script src="/bootstrap/v3.3.7/js/bootstrap.js"></script>

    <script>
        function Alertame(msg) {
            alert(msg);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager runat="server"></asp:ScriptManager>

        <div class="row" style="padding: 10px;">
            <!-- Tab Principal -->
            <ul class="nav nav-tabs" role="tablist">
                <li role="presentation" class="active"><a href="#Principal" aria-controls="Principal" role="tab" data-toggle="tab">Principal</a></li>
                <li role="presentation"><a href="#Modelo" aria-controls="Modelo" role="tab" data-toggle="tab">Modelo</a></li>
                <li role="presentation"><a href="#Controlador" aria-controls="Controlador" role="tab" data-toggle="tab">Controlador</a></li>
                <li role="presentation"><a href="#QueryUsp" aria-controls="QueryUsp" role="tab" data-toggle="tab">Query & USP</a></li>
                <li role="presentation"><a href="#Granular" aria-controls="Granular" role="tab" data-toggle="tab">Granular</a></li>
            </ul>

            <!-- Tab Principal8 -->
            <div class="tab-content">

                <div role="tabpanel" class="tab-pane active" id="Principal" style="padding: 10px;">

                    <div>
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs" role="tablist">
                            <li role="presentation" class="active"><a href="#tabInicio" aria-controls="tabInicio" role="tab" data-toggle="tab">Inico</a></li>
                            <li role="presentation"><a href="#TabBd" aria-controls="TabBd" role="tab" data-toggle="tab">Base de datos</a></li>
                            <li role="presentation"><a href="#TabAcronimo" aria-controls="TabAcronimo" role="tab" data-toggle="tab">Acrónimo</a></li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content">

                            <div role="tabpanel" class="tab-pane active" id="tabInicio" style="padding: 10px;">

                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>

                                        <div class="row">

                                            <div class="col-xs-12">

                                                <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                                                    <AlternatingRowStyle BackColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                                </asp:GridView>

                                            </div>

                                        </div>

                                        <div class="row">

                                            <asp:Button ID="btngenerarClases" runat="server" CssClass="btn btn-success" Text="Generar clases" OnClick="btngenerarClases_OnClick" />

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                            <div role="tabpanel" class="tab-pane" id="TabBd">

                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>

                                        <div class="row">

                                            <div class="col-xs-6">

                                                <div class="col-xs-12">
                                                    <h3>Conectarse al servidor</h3>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-xs-12">
                                                        <label for="<%=txtDbServidor.ClientID%>">Servidor</label>
                                                        <asp:TextBox ID="txtDbServidor" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-xs-12">
                                                        <label for="<%=txtDbusuario.ClientID%>">Usuario</label>
                                                        <asp:TextBox ID="txtDbusuario" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-xs-12">
                                                        <label for="<%=txtContrasenia.ClientID%>">Contraseña</label>
                                                        <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-xs-12">
                                                        <label for="<%=txtDbBaseDatos.ClientID%>">Base de datos</label>
                                                        <asp:TextBox ID="txtDbBaseDatos" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <asp:Button ID="btnConectarse" runat="server" CssClass="btn btn-primary" Text="Conectarse" OnClick="btnConectarse_OnClick" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-xs-6">

                                                <div class="col-xs-12">
                                                    <h3>Conectarse al servidor</h3>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-xs-12">
                                                        <label for="<%=ddlBdTablas.ClientID%>">Servidor</label>
                                                        <asp:DropDownList ID="ddlBdTablas" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="-">-</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <asp:Button ID="btnBdGenerar" runat="server" CssClass="btn btn-primary" Text="Generar" OnClick="btnBdGenerar_OnClick" />
                                                    </div>
                                                </div>

                                            </div>

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                            <div role="tabpanel" class="tab-pane" id="TabAcronimo">

                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>

                                        <div class="row">

                                            <div class="col-xs-6">

                                                <div class="col-xs-12">
                                                    <h3>Acrónimosr</h3>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-xs-12">
                                                        <label for="<%=txtProyectoModelo.ClientID%>">Proyecto modelo</label>
                                                        <asp:TextBox ID="txtProyectoModelo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-xs-12">
                                                        <label for="<%=txtAcronimoModelo.ClientID%>">Acrónimo modelo</label>
                                                        <asp:TextBox ID="txtAcronimoModelo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-xs-12">
                                                        <label for="<%=txtProyectoControlador.ClientID%>">Proyecto controlador</label>
                                                        <asp:TextBox ID="txtProyectoControlador" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="form-group col-xs-12">
                                                        <label for="<%=txtAcronimoControlador.ClientID%>">Acrónimo controlador</label>
                                                        <asp:TextBox ID="txtAcronimoControlador" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <asp:Button ID="btnGuardarAcronimo" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardarAcronimo_OnClick" />
                                                    </div>
                                                </div>

                                            </div>

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                        </div>

                    </div>

                </div>

                <div role="tabpanel" class="tab-pane" id="Modelo" style="padding: 10px;">

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <div class="row">
                                <textarea id="txtModelo" runat="server" class="form-control" rows="300"></textarea>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

                <div role="tabpanel" class="tab-pane" id="Controlador" style="padding: 10px;">

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <div class="row">
                                <textarea id="txtControlador" runat="server" class="form-control" rows="300"></textarea>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

                <div role="tabpanel" class="tab-pane" id="QueryUsp" style="padding: 10px;">
                    QueryUsp
                </div>

                <div role="tabpanel" class="tab-pane" id="Granular" style="padding: 10px;">

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <div class="row">
                                <textarea id="txtGranular" runat="server" class="form-control" rows="300"></textarea>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

            </div>

        </div>

    </form>
</body>
</html>
