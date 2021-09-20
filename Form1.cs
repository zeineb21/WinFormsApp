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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
        int depart = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            depart += 1;
            progressBar2.Value = depart;
            if (progressBar1.Value == 100)
            {
                progressBar2.Value = 0;
                timer2.Stop();
                Connexion Conn = new Connexion();
                Conn.Show();
                this.Hide();
            }
        }
    }
}
