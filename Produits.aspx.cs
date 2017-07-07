using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace projetTest
{
    public partial class Produits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GSB.Cmedicaments omedicaments = GSB.Cmedicaments.getInstance();

            foreach (GSB.Cmedicament oMedicament in omedicaments.oCollmedicament)
            {
                ddMedicament.Items.Add(oMedicament.MED_NOMCOMMERCIAL);
            }
        }

        protected void ddMedicament_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nom = ddMedicament.SelectedValue.ToString();

            GSB.Cmedicaments omedicaments = GSB.Cmedicaments.getInstance();

            foreach (GSB.Cmedicament oMedicament in omedicaments.oCollmedicament)
            {
                if (oMedicament.MED_NOMCOMMERCIAL == nom)
                {
                    tbDptL.Text = oMedicament.MED_DEPOTLEGAL;
                    tbCompo.Text = oMedicament.MED_COMPOSITION;
                    tbEffet.Text = oMedicament.MED_EFFETS;
                    tbContreInd.Text = oMedicament.MED_CONTREINDIC;
                }
            }
        }

        protected void btAjouter_Click(object sender, EventArgs e)
        {
            MySqlConnection sqlConn = new MySqlConnection("server=localhost;database=projetgsb;uid=root;pwd=;");

            MySqlCommand sqlCmd = new MySqlCommand("INSERT INTO famille (FAM_CODE, FAM_LIBELLE) VALUES (" + "'" + tbID.Text + "'" + "," + "'" + tbFamille.Text + "'" + ")");

            sqlCmd.Connection = sqlConn;
            sqlConn.Open();
            sqlCmd.ExecuteNonQuery();
        }


    }
}