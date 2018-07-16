<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ReportError.aspx.cs" Inherits="ReportError" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">

.ModalPopupBG
{
    background-color: #808080;
    filter: alpha(opacity=50);
    opacity: 0.7;
}

.feedback
{
    min-width:200px;
    min-height:150px;
    background:white;
}
</style>
    <br /><br />
    <asp:LinkButton ID="ErrorPO" runat="server">Report Error</asp:LinkButton>
    <br /><br />

     <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
         OkControlID="err_btnOkay"
        TargetControlID="ErrorPO" PopupControlID="ErrorPanel"
        PopupDragHandleControlID="err_PopupHeader" Drag="true"
        BackgroundCssClass="ModalPopupBG" DropShadow="True">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="ErrorPanel" Style="display: block" runat="server" Width="500px" BorderColor="#0099CC" BorderStyle="Solid" HorizontalAlign="Left" BorderWidth="3px">
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="feedback">
             <div class="CloseModalPO" id="err_Close" style="margin: auto; padding: 4px; vertical-align: middle; text-align: right; font-size: small;">
                <asp:LinkButton ID="err_btnClose" runat="server" OnClick="ERRbtnCancel_Click" causesvalidation="false" ViewStateMode="Inherit">X</asp:LinkButton>
            </div>
            <div class="PopupHeader" id="err_PopupHeader" style="margin: auto; padding: 10px; vertical-align: middle; text-align: center; font-size: x-large;">Report Error</div>
            <div class="PopupBody" style=" vertical-align: middle; padding-top: 10px; padding-bottom: 10px; padding-left: 10%;">
                <table id="Table2" border="0" style="width: 100%; vertical-align: middle">
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Name:"></asp:Label>
                            <br />
                            <asp:TextBox ID="err_txtName" runat="server" Width="270px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Email:"></asp:Label>
                            <br />
                            <asp:TextBox ID="err_txtEmail" runat="server" Width="270px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Description:"></asp:Label>
                            <br />
                            <asp:TextBox ID="err_txtMessage" runat="server" Height="100px" Width="400px" Wrap="True" TextMode="MultiLine"></asp:TextBox>
                        <br /><br />
                            <botdetect:WebFormsCaptcha ID="err_captchaBox" runat="server"></botdetect:WebFormsCaptcha>
                            <br />
                            <asp:Label ID="err_Captchalbl" runat="server" Text="Enter Captcha:"></asp:Label>
                            <br />
                <asp:textbox ID="err_txtCaptcha" runat="server"></asp:textbox>
                             
                        </td>
                    </tr>
                </table>
            </div>
            <div class="Controls" style="margin: auto; padding: 15px; vertical-align: middle; text-align: center">
                 <asp:Label ID="err_lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                <br />
                <asp:Button ID="err_btnOkay" runat="server" Text="Send" UseSubmitBehavior="false" OnClick="btnReport_Click" Font-Size="Large" Height="40" Width="120" />
                 <asp:LinkButton ID="Close_btn" runat="server" OnClick="ERRbtnCancel_Click" causesvalidation="false" ViewStateMode="Inherit" Font-Size="X-Small">Close</asp:LinkButton>
            </div>
        </div>
                   </ContentTemplate>
             <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="err_btnOkay" />
                 <asp:AsyncPostBackTrigger ControlID="Close_btn" />
                 <asp:AsyncPostBackTrigger ControlID="err_btnClose" />
                </Triggers>
    </asp:UpdatePanel>
    </asp:Panel>

     
</asp:Content>

