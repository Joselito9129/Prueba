<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Login.LoginPage" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml" xml:lang>

    <head runat="server">
        <title>LOGIN</title>
    </head>

    <body>
        <form id="loginForm" runat="server">
            <div>
                <h2>Iniciar sesión</h2>
                <div>
                    <label for="UserTxt">Usuario:</label>
                    <asp:TextBox ID="UserTxt" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Username" runat="server" ControlToValidate="UserTxt"
                        ErrorMessage="El usuario es obligatorio" ForeColor="Red" ValidationGroup="LoginValidation">
                    </asp:RequiredFieldValidator>
                </div>
                <div>
                    <label for="TxtPass">Contraseña:</label>
                    <asp:TextBox ID="TxtPass" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="Password" runat="server" ControlToValidate="TxtPass"
                        ErrorMessage="La Contraseña es obligatoria" ForeColor="Red" ValidationGroup="LoginValidation">
                    </asp:RequiredFieldValidator>
                </div>
                <div>
                    <asp:Button ID="btnLogin" runat="server" Text="Iniciar sesión" OnClick="btnLogin_Click"
                        ValidationGroup="LoginValidation" />
                </div>
            </div>
        </form>
    </body>

    </html>