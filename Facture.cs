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
    public partial class Facture : Form
    {
        public Facture()
        {
            InitializeComponent();
            remplirFact();
            remplirEmp();
            afficher1();
            afficher2();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Zeineb Smidi\Documents\Magasin.mdf"";Integrated Security=True" + ";Connect Timeout=30");
        private void remplirFact()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select ProdNum from ProduitTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("ProdNum", typeof(int));
            dt.Load(Rdr);
            NumProdCB.ValueMember = "ProdNum";
            NumProdCB.DataSource = dt;
            Con.Close();
        }
        private void remplirEmp()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select EmpNum from EmployeTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNum", typeof(int));
            dt.Load(Rdr);
            NumEmpCB.ValueMember = "EmpNum";
            NumEmpCB.DataSource = dt;
            Con.Close();
        }
        int x = 0;
        private void chercherQte()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from ProduitTbl where ProdNum ='" + NumProdCB.SelectedValue.ToString() + "'",Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                x = Convert.ToInt32(dr["ProdQte"].ToString());
                QteTb.Text = dr["ProdQte"].ToString();
                QteTb.Visible = true;
            }
            Con.Close();
        }
        private void chercherPrix()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from ProduitTbl where ProdNum ='" + NumProdCB.SelectedValue.ToString() + "'", Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                PrixTB.Text = dr["ProdPrix"].ToString();
                PrixTB.Visible = true;
            }
            Con.Close();
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Produit Prod = new Produit();
            Prod.Show();
            this.Hide();
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

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void reinitialiser()
        {

            QteTb.Text = "";
            QaTB.Text = "";
            PrixTB.Text = "";
            TotalTB.Text = "";
            Cle = 0;
        }
        private void afficher1()
        {
            Con.Open();
            String req = "select * from AchatTbl";
            SqlDataAdapter sda = new SqlDataAdapter(req, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            AchatDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void afficher2()
        {
            Con.Open();
            String req1 = "select * from FactureTbl";
            SqlDataAdapter sda1 = new SqlDataAdapter(req1, Con);
            SqlCommandBuilder builder1 = new SqlCommandBuilder(sda1);
            var ds1 = new DataSet();
            sda1.Fill(ds1);
            FactureDGV.DataSource = ds1.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int q = Int32.Parse(QaTB.Text);
            int p = Int32.Parse(PrixTB.Text);
            int total = p * q;
            TotalTB.Text = "" + total;
            try
            {
                Con.Open();

                String req = "insert into AchatTbl values('" + NumEmpCB.SelectedValue.ToString() + "'," + TotalTB.Text + ")";
                SqlCommand cmd = new SqlCommand(req, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Achat ajouté avec succès");
                Con.Close();
                afficher1();
                afficher2();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            Con.Open();
            for (int i = 0; i < AchatDGV.Rows.Count - 1; i++)
            {
                int qr = 0;
                Cle = Convert.ToInt32(AchatDGV.Rows[i].Cells[0].Value.ToString());
                qr = Int32.Parse(QteTb.Text) - Int32.Parse(QaTB.Text);
                String res = "" + qr;
                String req2 = "update ProduitTbl set ProdQte=" + res + "where ProdNum=" + Cle + "";
                SqlCommand cmd2 = new SqlCommand(req2, Con);
                cmd2.ExecuteNonQuery();
            }
            Con.Close();
            chercherQte();
            remplirFact();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void NumProdCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void QteTb_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void NumProdCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            chercherQte();
            chercherPrix();
        }
        int Cle = 0;
        private void button5_Click(object sender, EventArgs e)
        {
            int tot = 0;
            for(int i = 0;i<AchatDGV.Rows.Count-1;i++)
            {
                tot=tot+Int32.Parse(AchatDGV.Rows[i].Cells[2].Value.ToString());
            }
            totTb.Text = "" + tot;
            try
            {
                Con.Open();
                String req = "insert into FactureTbl values('" + NumEmpCB.SelectedValue.ToString() + "'," + totTb.Text + ")";
                SqlCommand cmd = new SqlCommand(req, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Facture ajoutée avec succès");
                Con.Close();
                afficher1();
                afficher2();
                reinitialiser();
                Con.Open();
                String req1 = "Delete  from AchatTbl";
                SqlCommand cmd1 = new SqlCommand(req1, Con);
                cmd1.ExecuteNonQuery();
                Con.Close();
                afficher1();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            reinitialiser(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
