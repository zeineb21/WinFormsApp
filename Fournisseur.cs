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
    public partial class Fournisseur : Form
    {
        public Fournisseur()
        {
            InitializeComponent();
            afficher();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Zeineb Smidi\Documents\Magasin.mdf"";Integrated Security=True" + ";Connect Timeout=30");
        private void reinitialiser()
        {
            NomTb.Text = "";
            AdresseTb.Text = "";
            DescriptionTb.Text = "";
            TelTb.Text = "";
            Cle = 0;
            

        }
        private void afficher()
        {
            Con.Open();
            String req = "select * from FournisseurTbl";
            SqlDataAdapter sda = new SqlDataAdapter(req, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            FournisseurDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(NomTb.Text=="" || AdresseTb.Text== "" || DescriptionTb.Text=="" || TelTb.Text == "")
            {
                MessageBox.Show("Complétez les informations SVP");
            }
            else
            {
                try
                {
                    Con.Open();
                    String req = "insert into FournisseurTbl values('" + NomTb.Text + "','" + AdresseTb.Text + "','" + DescriptionTb.Text + "','" + TelTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fournisseur ajouté avec succès");
                    Con.Close();
                    afficher();
                    reinitialiser();
                }
                catch(Exception Ex)
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
        private void FournisseurDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NomTb.Text = FournisseurDGV.SelectedRows[0].Cells[1].Value.ToString();
            AdresseTb.Text = FournisseurDGV.SelectedRows[0].Cells[2].Value.ToString();
            DescriptionTb.Text = FournisseurDGV.SelectedRows[0].Cells[3].Value.ToString();
            TelTb.Text = FournisseurDGV.SelectedRows[0].Cells[4].Value.ToString();
            if(NomTb.Text=="")
            {
                Cle = 0;
            }
            else
            {
                Cle = Convert.ToInt32(FournisseurDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Cle == 0)
            {
                MessageBox.Show("Sélectionnez le fournisseur à supprimer");
            }
            else
            {
                try
                {
                    Con.Open();
                    String req = "delete from FournisseurTbl where FournNum="+Cle+"";
                    SqlCommand cmd = new SqlCommand(req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fournisseur supprimé avec succès");
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
            if (NomTb.Text == "" || AdresseTb.Text == "" || DescriptionTb.Text == "" || TelTb.Text == "")
            {
                MessageBox.Show("Information manquante");
            }
            else
            {
                try
                {
                    Con.Open();
                    String req = "update FournisseurTbl set FournNom='"+NomTb.Text+"', FournAd='"+AdresseTb.Text+"',FournDesc='"+DescriptionTb.Text+"', FournTel='"+TelTb.Text+"'where FournNum="+Cle+"";
                    SqlCommand cmd = new SqlCommand(req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Fournisseur modifié avec succès");
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

        private void label1_Click(object sender, EventArgs e)
        {
            Produit Prod = new Produit();
            Prod.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Dashboard Dash = new Dashboard();
            Dash.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Connexion C = new Connexion();
            C.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Facture Fact = new Facture();
            Fact.Show();
            this.Hide();
        }
    }
}
