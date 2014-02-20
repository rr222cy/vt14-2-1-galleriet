<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Gallery.Default" ViewStateMode="Disabled" %>

<!DOCTYPE html>
<html lang="sv">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="description" content="Galleri i ASP.NET Web Forms C#" />
    <meta name="author" content="Robert Roos"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Galleriet</title>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href='http://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,700,400italic' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" type="text/css" href="~/css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="Wrapper">    
            <header>
                <div id="HeaderMenu">
                    <a href="Default.aspx">Startsidan</a>
                </div>
            <h1>Galleriet</h1>
            </header>            

            <article class="white">
                <section>
                    <h2>Bildvisaren</h2>
                    <div>
                        <asp:Image ID="SelectedImage" Width="100%" runat="server" />
                    </div>
                    <div class="spacingTop">
                        <%-- Repeater that loops out all thumbnails in the gallery. --%>
                        <asp:Repeater ID="GalleryThumbnailsRepeater" runat="server" ItemType="Gallery.Models.GalleryClass" SelectMethod="GalleryThumbnailsRepeater_GetData">
                            <HeaderTemplate>
                                <div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="PictureHyperLink" runat="server" NavigateUrl='<%# "~/Default.aspx?Picture=" + Item.Name %>'>
                                    <img src='<%# "galleryImages/thumbnails/" + Item.Name %>' width="150" height="150" Class='<%# Item.Name %>' alt="" runat="server" />
                                </asp:HyperLink>
                            </ItemTemplate>
                            <FooterTemplate>
                                </div>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </section>
            </article>

            <%-- Highlights the thumbnail of the picture that is beeing shown. --%>
            <script>
                var querystring = window.location.search.substring(9);
                var pictureClass = document.getElementsByClassName(querystring)[0].setAttribute("class", "imageBorder");
            </script>

            <article class="grey">
                <section>
                    <h2>Ladda upp bild</h2>
                    <div>
                        <p>Välj bilden du vill ladda upp</p>
                        <p><asp:Label ID="UploadStatusLabel" runat="server" Text="" CssClass="smaller"></asp:Label></p>
                        
                        <%-- Upload control and validation that checks if a file is chosen, and filetype is allowed. --%>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="field-validation-error" />
                        <asp:FileUpload ID="PictureUpload" runat="server" CssClass="standardButton" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="En fil måste väljas!" Text="*" ControlToValidate="PictureUpload" Display="Dynamic" CssClass="field-validation-error"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Filen är inte av typen JPG/PNG/GIF" Text="*" ControlToValidate="PictureUpload" CssClass="field-validation-error" Display="Dynamic" ValidationExpression="([^\s]+(\.(?i)(jpg|png|gif))$)"></asp:RegularExpressionValidator>
                    </div>
                    <div>
                        <asp:Button ID="UploadButton" runat="server" Text="Ladda upp bild" CssClass="standardButton" OnClick="UploadButton_Click" />
                    </div>
                </section>
            </article>
        </div>
        
        <footer class="site-footer">
            <div style="padding-top: 13px; padding-left: 10px;">     
                <p><span class="smaller" style="color: #ffffff; padding-top: 5px;">© Copyright 2014 - Producerad av: <a href="http://www.robertroos.eu" target="_blank">RobertRoos.eu</a></span></p>
            </div>
        </footer>
    </form>
<script type="text/javascript" src="js/scripts.js"></script>
</body>
</html>