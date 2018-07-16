<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="fullFeedbackView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <br /><br />

    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="ContactUs.aspx"> Contact Us </asp:LinkButton>
    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="ReportError.aspx">Report Error</asp:LinkButton>
    <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="Subscribe.aspx">Subscribe</asp:LinkButton>

    <br /><br />
    <asp:RadioButtonList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="True" RepeatLayout="Flow">
      </asp:RadioButtonList>
     <asp:GridView ID="GV1" runat="server" DataKeyNames="ID" AutoGenerateColumns="False" Width="1200px"  OnRowDeleting="GV1_RowDeleting" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                    <Columns>
             
                         <asp:TemplateField ShowHeader="False" ItemStyle-Width="300" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# ReadFeedbackType(Eval("feedbackType")) %>' Font-Size="Medium"></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="500" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("fullName") %>' Font-Size="X-Large"></asp:Label>
                                <br /><br />
                                 <asp:Label runat="server" Text='<%# Eval("email") %>' Font-Size="Medium"></asp:Label>
                                <br /><br />
                                 <asp:Label runat="server" Text='<%# Eval("phone") %>' Font-Size="Medium"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField ShowHeader="False" ItemStyle-Width="500" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("feedbackMsg") %>' Font-Size="Medium"></asp:Label>
                                <br /><br />
                                <asp:Label runat="server" Text='<%# Eval("page") %>' Font-Size="Medium"></asp:Label>
                                 
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField ShowHeader="False" ItemStyle-Width="25" HeaderStyle-BackColor="#006699" ItemStyle-VerticalAlign="Top">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbSelectToDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="Delete" />
                                <ajaxToolkit:ConfirmButtonExtender ID="cbe" runat="server" TargetControlID="lbSelectToDelete"
                                    ConfirmText="Delete will be permanent, are you sure?" />
                                 <br /><br />
                              
                                
                            </ItemTemplate>

                            <HeaderStyle BackColor="#006699"></HeaderStyle>

                            <ItemStyle Width="25px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="White" ForeColor="#000066" />
                    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                    <RowStyle ForeColor="#000066" />
                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
</asp:Content>

