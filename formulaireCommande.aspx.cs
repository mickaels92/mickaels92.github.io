using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Net.Mail;

namespace projetTest
{
    public partial class formulaireCommande : System.Web.UI.Page
    {
        //déclaration de la datable
        DataTable dt;

        protected void Page_Init(object sender, EventArgs e)
        {
            GSB.Cpraticiens opraticiens = GSB.Cpraticiens.getInstance();

            foreach (GSB.Cpraticien oPraticien in opraticiens.oCollpraticien)
            {
                //peuple la ComboBox
                ddPraticien.Items.Add(oPraticien.PRA_NOM);
            }

            GSB.Cmedicaments omedicaments = GSB.Cmedicaments.getInstance();

            foreach (GSB.Cmedicament oMedicament in omedicaments.oCollmedicament)
            {
                ddMedicament.Items.Add(oMedicament.MED_NOMCOMMERCIAL);
            }

            GSB.Ctransporteurs otransporteurs = GSB.Ctransporteurs.getInstance();

            foreach (GSB.Ctransporteur oTransporteur in otransporteurs.oColltransporteur)
            {
                ddTransporteur.Items.Add(oTransporteur.TRANS_NOM);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["USER"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!Page.IsPostBack)
            {
                dt = new DataTable();
                MakeDataTable();
            }
            else
            {
                dt = (DataTable)ViewState["DataTable"];
            }
            ViewState["DataTable"] = dt;
        }

        protected MemoryStream CreatePDF()
        {
            // Create a Document object
            Document document = new Document(PageSize.A4, 70, 70, 70, 70);

            //MemoryStream
            MemoryStream PDFData = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, PDFData);

            // First, create our fonts
            var titleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
            var boldTableFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 10, Font.NORMAL);
            Rectangle pageSize = writer.PageSize;

            // Open the Document for writing
            document.Open();
            //Add elements to the document here

            #region Top table
            // Create the header table 
            PdfPTable headertable = new PdfPTable(3);
            headertable.HorizontalAlignment = 0;
            headertable.WidthPercentage = 100;
            headertable.SetWidths(new float[] { 4, 2, 4 });  // then set the column's __relative__ widths
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            //headertable.DefaultCell.Border = Rectangle.NO_BORDER; //à modifier
            headertable.SpacingAfter = 30;
            PdfPTable nested = new PdfPTable(1);
            //nested.DefaultCell.Border = Rectangle.NO_BORDER;//à modifier
            PdfPCell nextPostCell1 = new PdfPCell(new Phrase("GalaxySwissBourdin", bodyFont));
            nextPostCell1.Border = Rectangle.NO_BORDER | Rectangle.RIGHT_BORDER;
            nested.AddCell(nextPostCell1);
            PdfPCell nextPostCell2 = new PdfPCell(new Phrase("2, rue Albert Jacquard", bodyFont));
            nextPostCell2.Border = Rectangle.NO_BORDER | Rectangle.RIGHT_BORDER;
            nested.AddCell(nextPostCell2);
            PdfPCell nextPostCell3 = new PdfPCell(new Phrase("Vénissieux 69200", bodyFont));
            nextPostCell3.Border = Rectangle.NO_BORDER | Rectangle.RIGHT_BORDER;
            nested.AddCell(nextPostCell3);
            PdfPCell nesthousing = new PdfPCell(nested);
            nesthousing.Rowspan = 4;
            nesthousing.Padding = 0f;
            headertable.AddCell(nesthousing);

            headertable.AddCell("");
            PdfPCell invoiceCell = new PdfPCell(new Phrase("GalaxySwissBourdin Labs", titleFont));
            invoiceCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;//à modifier
            invoiceCell.Border = Rectangle.NO_BORDER;
            headertable.AddCell(invoiceCell);
            PdfPCell noCell = new PdfPCell(new Phrase("N° Commande : ", bodyFont));
            noCell.HorizontalAlignment = 2;
            noCell.Border = Rectangle.NO_BORDER;
            headertable.AddCell(noCell);
            headertable.AddCell(new Phrase(tNumComm.Text, bodyFont));
            PdfPCell dateCell = new PdfPCell(new Phrase("Date : ", bodyFont));
            dateCell.HorizontalAlignment = 2;
            dateCell.Border = Rectangle.NO_BORDER;
            headertable.AddCell(dateCell);
            headertable.AddCell(new Phrase(datepicker.Value, bodyFont));
            PdfPCell billCell = new PdfPCell(new Phrase("Destinataire : ", bodyFont));
            billCell.HorizontalAlignment = 2;
            billCell.Border = Rectangle.NO_BORDER;
            headertable.AddCell(billCell);
            headertable.AddCell(new Phrase(ddPraticien.SelectedValue + "\n" + tbAdresse.Text + "\n" + tbCP.Text + "\n" + tbVille.Text + "\n" + ddTransporteur.Text, bodyFont));
            document.Add(headertable);
            #endregion

            #region Items Table
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            HTMLWorker htmlparser = new HTMLWorker(document);
            htmlparser.Parse(sr);
            Response.Write(document);
            GridView1.AllowPaging = true;
            GridView1.DataBind();
            #endregion

            writer.CloseStream = false; //set the closestream property
                                            // Close the Document without closing the underlying stream
                document.Close();
                PDFData.Position = 0;
                return PDFData;
            }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }


        protected void bValider_Click(object sender, EventArgs e)
        {
            using (MailMessage mm = new MailMessage("micklsp@outlook.com", tEmailF.Text))
            {
                mm.Subject = "Facturation : Votre commande";
                mm.Body = "Bonjour, voici un exemplaire au format PDF de votre facture";
                
                mm.Attachments.Add(new Attachment(CreatePDF(), "VotreCommande.pdf"));
               
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient("smtp.live.com");
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential("micklsp@outlook.com", "mmdp08011992");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Message envoyé !');", true);
            }

        //ajouter à la bdd dans la table commande et modifier la table stock_medicament
        //rajouter un bouton annuler et supprimer une commande rétablir les stocks de la table stock_medicament
            string date_commande = datepicker.Value;

            GSB.Ccommandes ocommandes = GSB.Ccommandes.getInstance();
            GSB.Ccommande ocommandeNew = new GSB.Ccommande();
            ocommandeNew.COM_NUM = Convert.ToString(tNumComm.Text);
            ocommandeNew.COM_EMAIL = Convert.ToString(tEmailF.Text);
            ocommandeNew.TRANS_NOM = Convert.ToString(ddTransporteur.Text);
            ocommandeNew.COM_DATE = Convert.ToString(date_commande);
            ocommandeNew.PRA_NUM = Convert.ToString(tbNumPraticien.Text);

            Boolean OK = ocommandeNew.enregistrerCommande();

            if (OK)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Enregistré !');", true);
            }

            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Non Enregistré');", true);
            }
        }

        protected void ddPraticien_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nom = ddPraticien.SelectedValue.ToString();

            GSB.Cpraticiens opraticiens = GSB.Cpraticiens.getInstance();

            foreach (GSB.Cpraticien oPraticien in opraticiens.oCollpraticien)
            {
                if (oPraticien.PRA_NOM==nom)
                {
                    tbNumPraticien.Text = oPraticien.PRA_NUM;
                    tbAdresse.Text = oPraticien.PRA_ADRESSE;
                    tbVille.Text = oPraticien.PRA_VILLE;
                    tbCP.Text = oPraticien.PRA_CP;
                }
            }
        }

        protected void ddMedicament_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = ddMedicament.SelectedValue.ToString();

            GSB.Cmedicaments omedicaments = GSB.Cmedicaments.getInstance();

            foreach (GSB.Cmedicament oMedicament in omedicaments.oCollmedicament)
            {
                if (oMedicament.MED_NOMCOMMERCIAL == name)
                {
                    tbCF.Text = oMedicament.FAM_CODE;
                }
            }
        }

        protected void bAjouter_Click(object sender, EventArgs e)
        {
            string id = tNumComm.Text;

            AddToDataTable();
            BindGrid();
            enregistrementComMed();
        }

        private void MakeDataTable()
        {
            dt.Columns.Add("Médicament");
            dt.Columns.Add("Quantité");
        }

        //ajout des médicaments à la datatable
        private void AddToDataTable()
        {
            string medic = ddMedicament.SelectedValue;
            DataRow dr = dt.NewRow();
            dr["Médicament"] = medic;
            dr["Quantité"] = tQtite.Text;
            dt.Rows.Add(dr);
        }

        //Remplis le gridview avec la datable
        private void BindGrid()
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        //Bouton de déconnexion
        protected void disconnect_Click(object sender, EventArgs e)
        {
            Session["USER"] = null;
        }

        //enregistrement des médicaments correspondants à la commande
        public void enregistrementComMed()
        {
            MySqlConnection sqlConn = new MySqlConnection("server=localhost;database=projetgsb;uid=root;pwd=;");

            MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO commande_medicament (COM_NUM, PRA_NUM, QTITE_COM_MED, FAM_CODE,  MED_NOMCOMMERCIAL) VALUES (" +
                                      tNumComm.Text + "," + "'" + tbNumPraticien.Text + "'" + "," + "'" + tQtite.Text + "'" + "," + "'" + tbCF.Text + "'" + "," + "'" + ddMedicament.Text + "'" + ")");

                sqlCmd.Connection = sqlConn;
                sqlConn.Open();
                sqlCmd.ExecuteNonQuery();
        }

        protected void bSupprimer_Click(object sender, EventArgs e)
        {
            MySqlConnection sqlConn = new MySqlConnection("server=localhost;database=projetgsb;uid=root;pwd=;");

            MySqlCommand sqlCmd = new MySqlCommand("DELETE FROM commande_medicament WHERE COM_NUM = "+tNumComm.Text+" ");

            sqlCmd.Connection = sqlConn;
            sqlConn.Open();
            sqlCmd.ExecuteNonQuery();
            sqlConn.Close();
        }

    }
}