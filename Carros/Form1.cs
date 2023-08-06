using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;


namespace Carros
{
    public partial class Form1 : Form
    {
        static Carro carro = new Carro("Fabricante", "Modelo");
        bool right, left, up, down;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

       
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void moveTimerEvent(object sender, EventArgs e)
        {
            if (up)
            {
                carro.Direcao.Acelerador.Acelerar(1);   
            }
            if (left)
            {
                carro.Direcao.VirarAEsquerda(0.1f);
            }
            if (right)
            {
                carro.Direcao.VirarADireita(0.1f);
            }
            if (down)
            {
                carro.Direcao.Acelerador.Desacelerar(1.5f);
            }
            carro.UpdateLocation();
            pictureBox1.Left = carro.PosX;
            pictureBox1.Top = carro.PosY;
        }

        private void keyIsUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                left = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                right = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                carro.Acelerando = false;
                up = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                down = false;
            }
        }

        private void keyIsDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
            {
                left = true;
            }
            if(e.KeyCode == Keys.Right)
            {
                right = true;
            }
            if(e.KeyCode == Keys.Up)
            {
                carro.Acelerando = true;
                up = true;
            }
            if(e.KeyCode == Keys.Down)
            {
                down = true;
            }
        }
    }
}
