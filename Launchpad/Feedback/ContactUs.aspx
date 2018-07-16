<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <style type="text/css">
        .ModalPopupBG {
            background-color: #808080;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .feedback {
            min-width: 200px;
            min-height: 150px;
            background: white;
        }
    </style>
    <br />
    <br />
    <asp:LinkButton ID="ContactPO" runat="server"> Contact Us </asp:LinkButton>
    <br />
    <br />

    <ajaxToolkit:ModalPopupExtender id="ModalPopupExtender1" runat="server"
        okcontrolid="cu_btnOkay"
        targetcontrolid="ContactPO" popupcontrolid="ContactPanel"
        popupdraghandlecontrolid="cu_PopupHeader" drag="true"
        backgroundcssclass="ModalPopupBG" dropshadow="True">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="ContactPanel" Style="display: block" runat="server" Width="400px" BorderColor="#36CDC8" BorderStyle="Solid" HorizontalAlign="Left" BorderWidth="4px">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="feedback">
            <div class="CloseModalPO" id="cu_Close" style="margin: auto; padding: 4px; vertical-align: middle; text-align: right; font-size: medium;">
                <asp:LinkButton ID="cu_btnClose" runat="server" OnClick="CUbtnCancel_Click" CausesValidation="false" ViewStateMode="Inherit" Class="contact_close_button"> X </asp:LinkButton>
            </div>

            <div class="PopupHeader" id="cu_PopupHeader" style="margin: auto; padding: 5px; vertical-align: middle; text-align: center; font-size: x-large;">Contact Us!</div>
            <div class="PopupBody" style="vertical-align: middle; padding-top: 10px; padding-bottom: 10px; padding-left: 10%;">
                <table id="Table1" border="0" style="width: 100%; vertical-align: middle">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>
                            <br />
                            <asp:TextBox ID="cu_txtName" runat="server" Width="270px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Email:"></asp:Label>
                            <br />
                            <asp:TextBox ID="cu_txtEmail" runat="server" Width="270px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Message:"></asp:Label>
                            <br />
                            <asp:TextBox ID="cu_txtMessage" runat="server" Height="100px" Width="400px" Wrap="True" TextMode="MultiLine"></asp:TextBox>
                            <br />
                            <br />
                            <BotDetect:WebFormsCaptcha ID="cu_captchaBox" runat="server" Visible="True"></BotDetect:WebFormsCaptcha>
                            <br />
                            <asp:Label ID="cu_Captchalbl" runat="server" Text="Enter Captcha:" Visible="True"></asp:Label>
                            <br />
                            <asp:TextBox ID="cu_txtCaptcha" runat="server" Visible="True"></asp:TextBox>

                        </td>
                    </tr>
                </table>


            </div>
            <br />
            <div class="Controls" style="margin:auto; padding-bottom: 50px; vertical-align: middle; text-align: center">
                <asp:Label ID="cu_lblError" runat="server" Text="" ForeColor="Red"></asp:Label>

                <br />
                <asp:Button ID="cu_btnOkay" runat="server" Text="Submit" UseSubmitBehavior="false" OnClick="btnContact_Click" Font-Size="Large" Height="40" Width="120"  />
                 <asp:LinkButton ID="Close_btn" runat="server" OnClick="CUbtnCancel_Click" causesvalidation="false" ViewStateMode="Inherit" Font-Size="medium">Close</asp:LinkButton>

            </div>
        </div>
                   </ContentTemplate>
             <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cu_btnOkay" />
                 <asp:AsyncPostBackTrigger ControlID="Close_btn" />
                 <asp:AsyncPostBackTrigger ControlID="cu_btnClose" />
                </Triggers>
    </asp:UpdatePanel>
    </asp:Panel>


</asp:Content>

