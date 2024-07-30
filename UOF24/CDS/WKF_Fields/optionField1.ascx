<%@ Control Language="C#" AutoEventWireup="true" CodeFile="optionField1.ascx.cs" Inherits="WKF_OptionalFields_optionField1" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>

<script>

    function ValidateFunction_<%=m_versionField.FieldID%>(source, arguments) {

        if ($('#<%=txtFieldValue.ClientID%>').val() == '123') {
            arguments.IsValid = false;
            return;
        }

        arguments.IsValid = true;
        return;
    }

</script>

<asp:TextBox ID="txtFieldValue" runat="server"></asp:TextBox>

<asp:Label ID="lblFieldValue" runat="server" Text="Label"></asp:Label>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="txtFieldValue" Display="Dynamic"
    ErrorMessage="欄位必填"></asp:RequiredFieldValidator>

<asp:CustomValidator ID="CustomValidator1" runat="server" 
    ClientValidationFunction="ValidateFunction"
    ErrorMessage="123">

</asp:CustomValidator>

<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False" meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>