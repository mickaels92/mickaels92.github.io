<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="formulaireCommande.aspx.cs" Inherits="projetTest.formulaireCommande" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <ul class="nav navbar-nav navbar-right">
      <li><a href="Login.aspx"><span class="glyphicon glyphicon-log-in"></span> Se déconnecté</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <!--<div class="col-md-4 col-md-offset-4">-->
    <div class="panel-group">
    <!--Entête Bon de commande-->
    <div class="panel panel-default">
        <div class="panel-heading">
            <label for="cmd">Commande</label>
        </div>
        <div class="panel-body">
            <label for="numCmd">N° commande</label><br />
            <asp:TextBox ID="tNumComm" class="form-control" runat="server"></asp:TextBox>
        </div>
    <!--Calendrier Jquery-->
        <div class="panel-body">
        <label for="datCmd">Date</label><br />
    <input type="text" id="datepicker" class="datepicker" runat="server">
        <script>
        $(document).on('ready',function() { 
            $(".datepicker").datepicker({
                altField: "#datepicker",
                closeText: 'Fermer',
                prevText: 'Précédent',
                nextText: 'Suivant',
                currentText: 'Aujourd\'hui',
                monthNames: ['Janvier', 'Février', 'Mars', 'Avril', 'Mai', 'Juin', 'Juillet', 'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre'],
                monthNamesShort: ['Janv.', 'Févr.', 'Mars', 'Avril', 'Mai', 'Juin', 'Juil.', 'Août', 'Sept.', 'Oct.', 'Nov.', 'Déc.'],
                dayNames: ['Dimanche', 'Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi'],
                dayNamesShort: ['Dim.', 'Lun.', 'Mar.', 'Mer.', 'Jeu.', 'Ven.', 'Sam.'],
                dayNamesMin: ['D', 'L', 'M', 'M', 'J', 'V', 'S'],
                weekHeader: 'Sem.',
                dateFormat: 'dd-mm-yy'
            });
        });
        var d = $('input').datepicker('getDate');
        </script><br /><br />
            </div>
        
        </div>
    <!--Calendrier Jquery-->
    <!--Entête Bon de commande-->
    
    
    <!--Coordonnées de livraison--> 
        <div class="panel panel-default">
            <div class="panel-heading">
                <label for="dest">Destinataire</label>
            </div>
            <div class="panel-body">
                <label for="praticien">Praticien</label>
                <asp:DropDownList ID="ddPraticien" class="form-control" runat="server" OnSelectedIndexChanged="ddPraticien_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            </div>
            <div class="panel-body">
                <label for="Numpraticien">Numéro praticien</label>
                <asp:TextBox ID="tbNumPraticien" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="panel-body">
                <label for="adrss">Adresse</label>
                <asp:TextBox ID="tbAdresse" class="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="panel-body">
                <label for="ville">Ville</label>
                <asp:TextBox ID="tbVille" class="form-control" runat="server"></asp:TextBox>
            </div>
             <div class="panel-body">
                <label for="codPos">Code postal</label>
                <asp:TextBox ID="tbCP" class="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
    <!--Coordonnées de livraison-->
    
    <!--Choix de la commande-->
    <div class="panel panel-default">
        <div class="panel-heading">
                <label for="prdts">Produits</label>
        </div>
        <div class="panel-body">
            <label for="médic">Médicament</label>
            <asp:DropDownList class="form-control" ID="ddMedicament" runat="server" OnSelectedIndexChanged="ddMedicament_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
        </div>
        <!--Modification-->
        <div class="panel-body">
            <label for="famcode">Code Famille</label>
            <asp:Textbox class="form-control" ID="tbCF" runat="server"></asp:Textbox>
        </div>
        <!--Modif-->
        <div class="panel-body">
            <label for="qtite">Quantité</label>
            <asp:TextBox ID="tQtite" class="form-control" runat="server"></asp:TextBox>
            <!--<asp:RequiredFieldValidator runat="server" id="reqTQtite" controltovalidate="tQtite" errormessage="Saisir la quantité souhaité" />-->
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="tQtite" runat="server" ErrorMessage="Saisir des nombres uniquement" ValidationExpression="\d+"></asp:RegularExpressionValidator>
        </div>
        <div class="panel-body">
            <asp:Button ID="bAjouter" class="btn btn-success" runat="server" Text="Ajouter" OnClick="bAjouter_Click" />
        </div>
        <div class="panel-body">
            <asp:Button ID="bSupprimer" class="btn btn-default" runat="server" Text="Supprimer" OnClick="bSupprimer_Click"/>
        </div>
        <div class="panel-body">
    <label for="votreCmd">Votre panier</label>
        <asp:GridView ViewState=True  ID="GridView1" runat="server" CssClass=" table table-bordered table-hover" >
                <AlternatingRowStyle CssClass="table table-striped" />
                <Columns>
                        <asp:TemplateField HeaderText="Numéro">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
        </asp:GridView>
    <br /><br />
            </div>
        </div>
    <!--Choix de la commande-->
  
    
    <!--Finalisation de la commande-->
    <div class="panel panel-default">
        <div class="panel-heading">
            <label for="finCmd">Finalisez votre commande</label>
        </div>
        <div class="panel-body">
            <label for="trspt">Transporteur</label>
            <asp:DropDownList class="form-control" ID="ddTransporteur" runat="server"></asp:DropDownList>
        </div>
          <div class="panel-body">
            <label for="emailFacturation">Email de facturation</label>
            <asp:Textbox class="form-control" ID="tEmailF" runat="server"></asp:Textbox>
        </div>
        <div class="panel-body">
            <asp:Button ID="bValider" class="btn btn-success" runat="server" Text="Valider la commande" OnClick="bValider_Click" />
        </div>
    </div>
    <!--Finalisation de la commande-->
  </div> 
</div>
</asp:Content>
