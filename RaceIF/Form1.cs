using System;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RaceIF
{
    public partial class Form1 : Form
    {
        static readonly Carro Carro = new("Fabricante", "Modelo");
        public bool IsRightPressed, IsLeftPressed, IsUpPressed, IsDownPressed;

        public Form1()
        {
            InitializeComponent();
            Carro.Start();
        }
        
        private void MoveTimerEvent(object sender, EventArgs e)
        {
            if (IsUpPressed)
            {
                Carro.Direcao.Acelerar();
            }
            if (IsLeftPressed)
            {
                if(!Carro.Direcao.isDandoRe)
                    Carro.Direcao.VirarAEsquerda(0.3f);
                else 
                    Carro.Direcao.VirarAEsquerda(-0.3f);

            }
            if (IsRightPressed)
            {
                if (!Carro.Direcao.isDandoRe)
                    Carro.Direcao.VirarADireita(0.3f);
                else
                    Carro.Direcao.VirarADireita(-0.3f);
            }
            if (IsDownPressed)
            {
                Carro.Direcao.Desacelerar();
            }
            carBox.Image = Carro.Update();
            carBox.Left = Carro.PosX;
            carBox.Top = Carro.PosY;
            label1.Text = VectorUtils.AngleFromVector(Carro.Direcao.VetorVisao).ToString();
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left or Keys.A:
                    IsLeftPressed = false;
                    Carro.Direcao.ResetarAngulo();
                    break;
                case Keys.Right or Keys.D:
                    IsRightPressed = false;
                    Carro.Direcao.ResetarAngulo();
                    break;
                case Keys.Up or Keys.W:
                    Carro.Direcao.Acelerador.Acelerando = false;
                    IsUpPressed = false;
                    break;
                case Keys.Down or Keys.S:
                    Carro.Direcao.Acelerador.Desacelerando = false;
                    IsDownPressed = false;
                    break;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left or Keys.A:
                    IsLeftPressed = true;
                    break;
                case Keys.Right or Keys.D:
                    IsRightPressed = true;
                    break;
                case Keys.Up or Keys.W:
                    Carro.Direcao.Acelerador.Acelerando = true;
                    IsUpPressed = true;
                    break;
                case Keys.Down or Keys.S:
                    Carro.Direcao.Acelerador.Desacelerando = true;
                    IsDownPressed = true;
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            carBox.SizeMode = PictureBoxSizeMode.AutoSize;
            carBox.Location = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
        }

    }
}