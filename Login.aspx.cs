using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace projetTest
{
    public partial class Login : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["USER"] = null;
        }

        //Méthode SHA1
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA1.Create();  // SHA1.Create()
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }
        //Fin Méthode SHA1

        protected void Button1_Click(object sender, EventArgs e)
        {
            
                //Test Connection avec Collection
                GSB.Cvisiteurs ovisiteurs = GSB.Cvisiteurs.getInstance();

                string userid = TextBox1.Text;

                string userpassword = GetHashString(TextBox2.Text);

                foreach (GSB.Cvisiteur oVisiteur in ovisiteurs.oCollvisiteur)
                {

                    if (oVisiteur.VIS_USER == userid && oVisiteur.VIS_MDP == userpassword)
                    {
                        Session["USER"] = true;
                        Response.Redirect("formulaireCommande.aspx");
                        
                    }

                    else
                    {
                    
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", "<script>alert('Identifiant ou Mot de passe incorrect');</script>");
                    }
                }
        }
        
    }
}