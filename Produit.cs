using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace MPGestionDunMagasin
{
    public partial class Produit : Form
    {
        public Produit()
        {
            InitializeComponent();
            remplirProd();
            afficher();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Fournisseur Fourn = new Fournisseur();
            Fourn.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Facture Fact = new Facture();
            Fact.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Dashboard Dash = new Dashboard();
            Dash.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Zeineb Smidi\Documents\Magasin.mdf"";Integrated Security=True" + ";Connect Timeout=30");
        private void remplirProd()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select FournNum from FournisseurTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("FournNum", typeof(int));
            dt.Load(Rdr);
            FournCB.ValueMember = "FournNum";
            FournCB.DataSource = dt;
            Con.Close();
        }
        
        private void reinitialiser()
        {
            NomTb.Text = "";
            PrixTb.Text = "";
            QteTb.Text = "";
            Cle = 0;
        }
        private void afficher()
        {
            Con.Open();
            String req = "select * from ProduitTbl";
            SqlDataAdapter sda = new SqlDataAdapter(req, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProduitDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (NomTb.Text == "" || PrixTb.Text == "" || QteTb.Text == "" || FournCB.SelectedIndex==-1)
            {
                MessageBox.Show("Complétez les informations SVP");
            }
            else
            {
                try
                {
                    Con.Open();
                    String req = "insert into ProduitTbl values('" + NomTb.Text + "'," + PrixTb.Text + "," + QteTb.Text + "," + FournCB.SelectedValue.ToString() + ")";
                    SqlCommand cmd = new SqlCommand(req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produit ajouté avec succès");
                    Con.Close();
                    afficher();
                    reinitialiser();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reinitialiser();
        }
        int Cle = 0;
        private void ProduitDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NomTb.Text = ProduitDGV.SelectedRows[0].Cells[1].Value.ToString();
            PrixTb.Text = ProduitDGV.SelectedRows[0].Cells[2].Value.ToString();
            QteTb.Text = ProduitDGV.SelectedRows[0].Cells[3].Value.ToString();
            FournCB.SelectedValue = ProduitDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (NomTb.Text == "")
            {
                Cle = 0;
            }
            else
            {
                Cle = Convert.ToInt32(ProduitDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Cle == 0)
            {
                MessageBox.Show("Sélectionnez le produit à supprimer");
            }
            else
            {
                try
                {
                    Con.Open();
                    String req = "delete from ProduitTbl where ProdNum=" + Cle + "";
                    SqlCommand cmd = new SqlCommand(req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produit supprimé avec succès");
                    Con.Close();
                    afficher();
                    reinitialiser();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NomTb.Text == "" || PrixTb.Text == "" || QteTb.Text == "" || FournCB.SelectedIndex == -1)
            {
                MessageBox.Show("Information manquante");
            }
            else
            {
                try
                {
                    Con.Open();
                    String req = "update ProduitTbl set ProdNom='" + NomTb.Text + "', ProdPrix=" + PrixTb.Text + ", ProdQte=" + QteTb.Text + ", ProdFourn=" + FournCB.SelectedValue.ToString() + "where ProdNum=" + Cle + "";
                    SqlCommand cmd = new SqlCommand(req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Produit modifié avec succès");
                    Con.Close();
                    afficher();
                    reinitialiser();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Connexion C = new Connexion();
            C.Show();
            this.Hide();
        }

        private void FournCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
