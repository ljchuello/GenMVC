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
            </ul>

            <!-- Tab Principal8 -->
            <div class="tab-content">
                <div role="tabpanel" class="tab-pane active" id="Principal" style="padding: 10px;">

                    <div>
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs" role="tablist">
                            <li role="presentation" class="active"><a href="#tabInicio" aria-controls="tabInicio" role="tab" data-toggle="tab">tabInicio</a></li>
                            <li role="presentation"><a href="#TabBd" aria-controls="TabBd" role="tab" data-toggle="tab">Base de datos</a></li>
                        </ul>
                        <!-- Tab panes -->
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane active" id="tabInicio">
                            </div>
                            <div role="tabpanel" class="tab-pane" id="TabBd">

                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>

                                        <div class="row">
                                            <div class="form-group col-xs-12 col-sm-6">
                                                <label for="<%=txtDbServidor.ClientID%>">Servidor</label>
                                                <asp:TextBox ID="txtDbServidor" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="form-group col-xs-12 col-sm-6">
                                                <label for="<%=txtDbusuario.ClientID%>">Usuario</label>
                                                <asp:TextBox ID="txtDbusuario" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="form-group col-xs-12 col-sm-6">
                                                <label for="<%=txtContrasenia.ClientID%>">Contraseña</label>
                                                <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="form-group col-xs-12 col-sm-6">
                                                <label for="<%=txtDbBaseDatos.ClientID%>">Base de datos</label>
                                                <asp:TextBox ID="txtDbBaseDatos" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        
                                        <div class="row">
                                            <div class="col-xs-12 col-sm-6">
                                                <asp:Button ID="btnConectarse" runat="server" CssClass="btn btn-primary" Text="Conectarse" OnClick="btnConectarse_OnClick" />
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>

                    </div>

                </div>
                <div role="tabpanel" class="tab-pane" id="Modelo" style="padding: 10px;">
                    Modelo
                </div>
                <div role="tabpanel" class="tab-pane" id="Controlador" style="padding: 10px;">
                    Controlador
                </div>
                <div role="tabpanel" class="tab-pane" id="QueryUsp" style="padding: 10px;">
                    QueryUsp
                </div>
            </div>

        </div>

    </form>
</body>
</html>
