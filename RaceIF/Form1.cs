using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RaceIF
{
    public partial class Form1 : Form
    {
        static readonly Carro Carro = new("Fabricante", "Modelo");
        public PictureBox CarroPictureBox = new PictureBox();
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
                CarroPictureBox.Image = Carro.Update();
                if (Carro.PosX < 0) Carro.PosX = 0;
                else if(Carro.PosX + CarroPictureBox.Image.Width > this.Width) Carro.PosX = this.Width - CarroPictureBox.Image.Width;
                if (Carro.PosY < 0) Carro.PosY = 0;
                else if (Carro.PosY + CarroPictureBox.Image.Height > this.Height) Carro.PosY = this.Height - CarroPictureBox.Image.Height;
                CarroPictureBox.Left = Carro.PosX;
                CarroPictureBox.Top = Carro.PosY;
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
            Image backgroundImage = Properties.Resources.RACEBACKGROUND;

            // Redimensione a imagem para cobrir a tela
            backgroundImage = ResizeImage(backgroundImage, this.ClientSize.Width, this.ClientSize.Height);

            // Crie um controle PictureBox e configure a imagem de fundo
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = backgroundImage;
            pictureBox.Dock = DockStyle.Fill; // Para preencher todo o espaço do formulário

            this.Controls.Add(pictureBox);
            
            CarroPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            CarroPictureBox.Location = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
            
            Image carroImage = Properties.Resources.TAXI_CLEAN_ALLD0000;

            // Crie um controle PictureBox para o carro
            
            CarroPictureBox.Image = ResizeImage(carroImage, 97, 53);
            CarroPictureBox.BackColor = Color.Transparent; // Defina o fundo como transparente
            pictureBox.Controls.Add(CarroPictureBox); // Adicione o carro ao controle da pista
            CarroPictureBox.Location = new Point(100, 100); // Posição do carro na pista
        }
        
        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }
        
        static bool CorEhParecidaComBranco(Color cor, int tolerancia)
        {
            int diferencaR = Math.Abs(cor.R - Color.WhiteSmoke.R);
            int diferencaG = Math.Abs(cor.G - Color.WhiteSmoke.G);
            int diferencaB = Math.Abs(cor.B - Color.WhiteSmoke.B);
            
            return (diferencaR <= tolerancia && diferencaG <= tolerancia && diferencaB <= tolerancia);
        }

    }
}