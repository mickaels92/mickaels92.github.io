<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="projetTest.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="row">
		<div class="col-md-4 col-md-offset-4">
    		<div class="panel panel-default">
			  	<div class="panel-heading">
			    	<h3 class="panel-title">Veuillez-vous connecter</h3>
			 	</div>
			  	<div class="panel-body">
			    	<form accept-charset="UTF-8" role="form">
                    <fieldset>
			    	  	<div class="form-group">
                              <label class="control-label"  for="username">Identifiant</label>
			    		    <asp:TextBox class="form-control" ID="TextBox1" runat="server"></asp:TextBox>&nbsp;
                              <asp:requiredfieldvalidator controltovalidate="TextBox1" runat="server" id="RequiredFieldValidator1" enableclientscript="true" errormessage="Identifiant obligatoire">
        </asp:requiredfieldvalidator><br />
			    		</div>
			    		<div class="form-group">
                            <label class="control-label" for="password">Mot de passe</label>
			    			<asp:TextBox class="form-control" ID="TextBox2" TextMode="Password" runat="server"></asp:TextBox>&nbsp;
                            <asp:requiredfieldvalidator controltovalidate="TextBox2" runat="server" id="RequiredFieldValidator2" enableclientscript="true" errormessage="Mot de passe obligatoire">
        </asp:requiredfieldvalidator><br />
			    		</div>
                        <asp:Button class="btn btn-lg btn-success btn-block" ID="Button1" runat="server" Text="Se connecter" OnClick="Button1_Click" />
			    	</fieldset>
			      	</form>
			    </div>
			</div>
		</div>
	</div>
</div>
</asp:Content>
