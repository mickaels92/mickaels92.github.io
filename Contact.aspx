<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="projetTest.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
    <div class="row">
		<div class="col-md-4 col-md-offset-4">
    		<div class="panel panel-default">
			  	<div class="panel-heading">
			    	<h3 class="panel-title">Contactez-nous</h3>
			 	</div>
			  	<div class="panel-body">
			    	<form accept-charset="UTF-8" role="form">
                    <fieldset>
			    	  	<div class="form-group">
                              <label class="control-label"  for="nom">Nom</label>
			    		    <asp:TextBox class="form-control" ID="NomContact" placeholder="Nom Prénom" runat="server"></asp:TextBox>&nbsp;<br />
			    		</div>
			    		<div class="form-group">
                            <label class="control-label" for="email">Email</label>
			    			<asp:TextBox class="form-control" ID="EmailContact" placeholder="Adresse Email"  runat="server"></asp:TextBox>&nbsp;<br />
			    		</div>
                        <div class="form-group">
                            <label class="control-label" for="message">Message</label>
				            <textarea rows="10" cols="100" class="form-control" placeholder="Message" id="message" required data-validation-required-message="Please enter your message" data-validation-minlength-message="Min 5 characters" maxlength="999" style="resize:none"></textarea>
		                </div> 	
                        <asp:Button class="btn btn-lg btn-success btn-block" ID="Envoie" runat="server" Text="Envoyer" />
			    	</fieldset>
			      	</form>
			    </div>
			</div>
		</div>
	</div>
</div>

  
</asp:Content>
