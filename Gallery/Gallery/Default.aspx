<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Gallery.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%: Styles.Render("~/bundles/css") %>
    
</head>
<body>

    <div id="header">
        <h1>Bildgalleri</h1>
    </div>
    
    <div id="bigPic">
            <asp:PlaceHolder ID="bigImgDisplay" runat="server" Visible="True">
                <asp:Image ID="bigImg" runat="server" width="700" Height="450"/>
                </asp:PlaceHolder>    
    </div>

    <div id="thumbList">
    <asp:Repeater ID="PicList" runat="server">
                <HeaderTemplate>
                    <ul>
                </HeaderTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
                <ItemTemplate>
                    <li><asp:HyperLink ID="HyperLink" runat="server" NavigateUrl='<%# String.Format("Default.aspx?img={0}", Container.DataItem)%>'>
                    <asp:Image ID="thumbs" runat="server" ImageUrl='<%# String.Format("~/Pictures/thumb/{0}", Container.DataItem)%>'/>
                    </asp:HyperLink></li>
                </ItemTemplate>
            </asp:Repeater>
    </div>
   
    <form id="uploadForm" runat="server">

    <div id="uploadFunction">
        Välj en fil att ladda upp </br></br>
        <asp:FileUpload ID="PictureFileUpload" runat="server" Width="730" />
        <asp:Button ID="UploadButton" runat="server" Text="Ladda upp" OnClick="UploadButton_Click" />
    </div>

        <asp:PlaceHolder ID="SuccessMessagePlaceHolder" runat="server" Visible="False">
               <div id="success_message"><p>Filen laddades upp</p></div>
        </asp:PlaceHolder>
      
    </form>

    <script src="Content/JScript.js"></script>
</body>
</html>
