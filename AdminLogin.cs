using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPGestionDunMagasin
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MdpTb.Text=="")
            {
                MessageBox.Show("Entrer le mot de passe administrateur");
            }
            else if(MdpTb.Text == "Admin")
            {
                Employe Emp = new Employe();
                Emp.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Contactez l'administrateur");
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Connexion C = new Connexion();
            C.Show();
            this.Hide();
        }
    }
}
