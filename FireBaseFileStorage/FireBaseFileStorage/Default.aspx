<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <table style="width: 100%;">
        <tr>
            <td colspan="2">&nbsp;
                <h2>Upload Videos & Images to Firebase</h2>
            </td>
            <%-- <td>&nbsp;</td>
            <td>&nbsp;</td>--%>
        </tr>
        <tr>

            <td style="padding: 5px; width: 239px;" class="form-group">
                <%-- <asp:Label ID="Label1" runat="server" Text="Upload Video" Font-Bold="true"></asp:Label>--%>
                <asp:FileUpload ID="fileUploadControl" runat="server" CssClass="form-control" />
                <br />
                <asp:Button ID="fileUploadbtn" runat="server" Text="Upload" CssClass="btn btn-primary" />
            </td>
            <td>&nbsp;<asp:Label ID="fileUploadStatus" runat="server" Text="" Font-Bold="true"></asp:Label></td>
        </tr>
        <%-- <tr>
            <td style="padding: 5px; width: 239px;" class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Upload Image" Font-Bold="true"></asp:Label>
                <asp:FileUpload ID="imgUploadControl" runat="server" />
                <asp:Button ID="UploadImgbtn" runat="server" Text="Upload" CssClass="btn btn-success btn-sm" />
            </td>
            <td>&nbsp;<asp:Label ID="imgUploadStatus" runat="server" Text="" Font-Bold="true"></asp:Label></td>
        </tr>--%>
    </table>
    <div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="False" ShowFooter="false" BorderStyle="None">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Image") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
        <div class="col-md-6">
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" ShowHeader="False" ShowFooter="false" BorderStyle="None">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>

                            <video width="320" height="240" controls autoplay>
                                <source src="<%# Eval("Video") %>" type="video/ogg">
                                <source src="<%# Eval("Video") %>" type="video/mp4">
                                <object data="movie.mp4" width="320" height="240">
                                    <embed width="320" height="240" src='<%# Eval("Video") %> '></object></video>


                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
