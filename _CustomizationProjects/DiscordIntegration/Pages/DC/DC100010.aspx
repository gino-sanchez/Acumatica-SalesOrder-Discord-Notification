<%@ Page Language="C#" MasterPageFile="~/MasterPages/FormView.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="DC100010.aspx.cs" Inherits="Page_DC100010" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/FormView.master" %>

<asp:Content ID="cont1" ContentPlaceHolderID="phDS" Runat="Server">
	<px:PXDataSource ID="ds" runat="server" Visible="True" Width="100%"
        TypeName="DiscordIntegration.DiscordSetupMaint"
        PrimaryView="Setup"
        >
		<CallbackCommands>

		</CallbackCommands>
	</px:PXDataSource>
</asp:Content>
<asp:Content ID="cont2" ContentPlaceHolderID="phF" Runat="Server">
	<px:PXFormView ID="form" runat="server" DataSourceID="ds" DataMember="Setup" Width="100%" AllowAutoHide="false">
		<Template>
			<px:PXLayoutRule runat="server" ID="PXLayoutRule1" StartRow="True" ></px:PXLayoutRule>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule1" StartColumn="True" ></px:PXLayoutRule>
			<px:PXLayoutRule runat="server" ID="CstPXLayoutRule2" StartGroup="True" GroupCaption="Settings" ></px:PXLayoutRule>
			<px:PXCheckBox AlignLeft="True" TextAlign="Right" runat="server" ID="CstPXCheckBox6" DataField="EnableNotification" CommitChanges="True" ></px:PXCheckBox>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit3" DataField="Url" ></px:PXTextEdit>
			<px:PXTextEdit runat="server" ID="CstPXTextEdit4" DataField="WebhookID" ></px:PXTextEdit>
			<px:PXTextEdit TextMode="Password" runat="server" ID="CstPXTextEdit5" DataField="WebhookToken" ></px:PXTextEdit></Template>
		<AutoSize Container="Window" Enabled="True" MinHeight="200" ></AutoSize>
	</px:PXFormView>
</asp:Content>

