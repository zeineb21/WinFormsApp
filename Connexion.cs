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
    public partial class Connexion : Form
    {
        public Connexion()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Zeineb Smidi\Documents\Magasin.mdf"";Integrated Security=True" + ";Connect Timeout=30");
        private void button1_Click(object sender, EventArgs e)
        {
            if(NomTb.Text == ""|| MdpTb.Text == "")
            {
                MessageBox.Show("Entrer le nom d'utilisateur et le mot de passe");
            }
            else
            {
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select count(*) from EmployeTbl where EmpNom='" + NomTb.Text + "' and EmpMDP='" + MdpTb.Text + "'", Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    Dashboard Dash = new Dashboard();
                    Dash.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Mot de passe incorrect");
                }
                Con.Close();
            }
            
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin Log = new AdminLogin();
            Log.Show();
            this.Hide();
        }
    }
}
