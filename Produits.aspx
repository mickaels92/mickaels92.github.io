<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Produits.aspx.cs" Inherits="projetTest.Produits" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="panel panel-default">
            <div class="panel-heading">
                <label for="dest">Nos Produits</label>
            </div>
            <div class="panel-body">
                <label for="medoc">Médicament</label>
                <asp:DropDownList ID="ddMedicament" class="form-control" runat="server" OnSelectedIndexChanged="ddMedicament_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            </div>
            <div class="panel-body">
                <label for="dptl">Dépôt légal</label>
                <asp:TextBox ID="tbDptL" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="panel-body">
                <label for="compo">Composition</label>
                <asp:TextBox ID="tbCompo" class="form-control" runat="server"></asp:TextBox>
            </div>
             <div class="panel-body">
                <label for="effet">Effets</label>
                <asp:TextBox ID="tbEffet" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="panel-body">
                <label for="contreindic">Contre Indication</label>
                <asp:TextBox ID="tbContreInd" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    <!--Modification-->
    <div class="panel panel-default">
        <div class="panel-heading">
            <label for="famille">Famille de médicament</label>
        </div>
        <div class="panel-body">
            <label for="id">Identifiant</label>
            <asp:TextBox ID="tbID" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
        <div class="panel-body">
            <label for="libelle">Famille médicament</label>
            <asp:Textbox ID="tbFamille" CssClass="form-control" runat="server"></asp:Textbox>
        </div>
         <div class="panel-body">
            <asp:Button ID="btAjouter" class="btn btn-default" runat="server" Text="Ajouter" OnClick="btAjouter_Click"/>
        </div>
    </div>
</asp:Content>
