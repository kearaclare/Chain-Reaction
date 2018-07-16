<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Subscribe.aspx.cs" Inherits="Subscribe" %>

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

    <br />
   

    <br />
    <br />

    <asp:LinkButton ID="SubscribePO" runat="server">Subscribe</asp:LinkButton>
    <br />  <br />
    
    
       <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" 
         OkControlID="sub_btnOkay"
        TargetControlID="SubscribePO" PopupControlID="SubscribePanel"
        PopupDragHandleControlID="sub_PopupHeader" Drag="true"
        BackgroundCssClass="ModalPopupBG" DropShadow="True">
    </ajaxToolkit:ModalPopupExtender>

    



    


    <asp:Panel ID="SubscribePanel" Style="display: block" runat="server" Width="500px" BorderColor="#0099CC" BorderStyle="Solid" HorizontalAlign="Left" BorderWidth="3px">
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="feedback">
            <div class="CloseModalPO" id="sub_Close" style="margin: auto; padding: 4px; vertical-align: middle; text-align: right; font-size: small;">
                <asp:LinkButton ID="sub_btnClose" runat="server" OnClick="SUBbtnCancel_Click" causesvalidation="false" ViewStateMode="Inherit">X  </asp:LinkButton>
            </div>
            <div class="PopupHeader" id="sub_PopupHeader" style="margin: auto; padding: 10px; vertical-align: middle; text-align: center; font-size: x-large;">Subscribe</div>
            <div class="PopupBody" style=" vertical-align: middle; padding-top: 10px; padding-bottom: 10px; padding-left: 10%;">
                <table id="Table3" border="0" style="width: 100%; vertical-align: middle">
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Text="Name:"></asp:Label>
                            <br />
                            <asp:TextBox ID="sub_txtName" runat="server" Width="270px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Email:"></asp:Label>
                            <br />
                            <asp:TextBox ID="sub_txtEmail" runat="server" Width="270px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Phone Number:"></asp:Label>
                            <br />
                            <asp:TextBox ID="sub_txtPhone" runat="server" Width="200px"></asp:TextBox>
                            <br /><br />
                            <botdetect:WebFormsCaptcha ID="sub_captchaBox" runat="server"></botdetect:WebFormsCaptcha>
                            <br />
                            <asp:Label ID="sub_Captchalbl" runat="server" Text="Enter Captcha:"></asp:Label>
                            <br />
                <asp:textbox ID="sub_txtCaptcha" runat="server"></asp:textbox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="Controls" style="margin: auto; padding: 15px; vertical-align: middle; text-align: center">
                 <asp:Label ID="sub_lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
                <br />
               <asp:Button ID="sub_btnOkay" runat="server" Text="Send" UseSubmitBehavior="false" OnClick="btnSubscribe_Click" Font-Size="Large" Height="40" Width="120" Font-Underline="False" />
               <asp:LinkButton ID="Close_btn" runat="server" OnClick="SUBbtnCancel_Click" causesvalidation="false" ViewStateMode="Inherit" Font-Size="X-Small">Close</asp:LinkButton>
            </div>
        </div>
                </ContentTemplate>
             <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="sub_btnOkay" />
                 <asp:AsyncPostBackTrigger ControlID="Close_btn" />
                 <asp:AsyncPostBackTrigger ControlID="sub_btnClose" />
                </Triggers>
    </asp:UpdatePanel>
    </asp:Panel>
 

  

</asp:Content>

