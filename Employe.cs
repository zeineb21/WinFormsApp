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
    public partial class Employe : Form
    {
        public Employe()
        {
            InitializeComponent();
            afficher();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Zeineb Smidi\Documents\Magasin.mdf"";Integrated Security=True" + ";Connect Timeout=30");
        private void button1_Click(object sender, EventArgs e)
        {
            if (NomTb.Text == "" || GenreCB.SelectedIndex == -1 || MdpTb.Text == "" || TelTb.Text=="")
            {
                MessageBox.Show("Complétez les informations SVP");
            }
            else
            {
                try
                {
                    Con.Open();
                    String req = "insert into EmployeTbl values('" + NomTb.Text+"','" + TelTb.Text+"','"+GenreCB.SelectedItem.ToString() + "','" + MdpTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employé ajouté avec succès");
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
        private void reinitialiser()
        {
            NomTb.Text = "";
            TelTb.Text = "";
            MdpTb.Text = "";
            GenreCB.SelectedIndex = -1;
            Cle = 0;
        }
        private void afficher()
        {
            Con.Open();
            String req = "select * from EmployeTbl";
            SqlDataAdapter sda = new SqlDataAdapter(req, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            EmployeDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        int Cle = 0;
        private void EmployeDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NomTb.Text = EmployeDGV.SelectedRows[0].Cells[1].Value.ToString();
            TelTb.Text = EmployeDGV.SelectedRows[0].Cells[2].Value.ToString();
            GenreCB.SelectedValue = EmployeDGV.SelectedRows[0].Cells[3].Value.ToString();
            MdpTb.Text = EmployeDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (NomTb.Text == "")
            {
                Cle = 0;
            }
            else
            {
                Cle = Convert.ToInt32(EmployeDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reinitialiser();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (NomTb.Text == "" || GenreCB.SelectedIndex == -1 || MdpTb.Text == "" || TelTb.Text == "")
            {
                MessageBox.Show("Information manquante");
            }
            else
            {
                try
                {
                    Con.Open();
                    String req = "update EmployeTbl set EmpNom='" + NomTb.Text + "', EmpTel='" + TelTb.Text + "', EmpGenre='" + GenreCB.SelectedItem.ToString() + "', EmpMDP='" + MdpTb.Text + "'where EmpNum=" + Cle + "";
                    SqlCommand cmd = new SqlCommand(req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employé modifié avec succès");
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (Cle == 0)
            {
                MessageBox.Show("Sélectionnez l'employé à supprimer");
            }
            else
            {
                try
                {
                    Con.Open();
                    String req = "delete from EmployeTbl where EmpNum=" + Cle + "";
                    SqlCommand cmd = new SqlCommand(req, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employé supprimé avec succès");
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Connexion C = new Connexion();
            C.Show();
            this.Hide();
        }
    }
}
