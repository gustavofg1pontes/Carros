using System;
using System.Drawing;
using System.Windows.Forms;
using RaceIF.Replay;

namespace RaceIF
{
    public partial class MainForm : Form
    {
        static readonly Carro Carro = new("Fabricante", "Modelo");
        public PictureBox CarroPictureBox = new PictureBox();
        public bool IsRightPressed, IsLeftPressed, IsUpPressed, IsDownPressed;

        private Label notificationLabel;

        public MainForm()
        {
            InitializeComponent();
            InitializeNotificationLabel();
            Carro.Start();
            
        }

        private void MoveTimerEvent(object sender, EventArgs e)
        {

            if (ReplayManager.IsReplaying)
            {
                var tickRecords = ReplayManager.Replay();
                if (tickRecords != null)
                {
                    foreach (var record in tickRecords.Records)
                    {
                        if (record.Type == EntityType.CAR)
                        {
                            PresentCar(Carro.Present(record.State));
                        }
                    }
                    return;
                }
                else
                {
                    Carro.Reset();
                }
            }

            if (IsUpPressed)
            {
                Carro.Direcao.Acelerar();
            }
            if (IsLeftPressed)
            {
                if (!Carro.Direcao.ReAtivada)
                    Carro.Direcao.VirarAEsquerda(0.6f);
                else
                    Carro.Direcao.VirarAEsquerda(-0.6f);

            }
            if (IsRightPressed)
            {
                if (!Carro.Direcao.ReAtivada)
                    Carro.Direcao.VirarADireita(0.6f);
                else
                    Carro.Direcao.VirarADireita(-0.6f);
            }

            if (IsDownPressed)
            {
                Carro.Direcao.Desacelerar();
            }

            Image carImage = Carro.Update();
            if (Carro.PosX < 0) Carro.PosX = 0;
            else if (Carro.PosX + CarroPictureBox.Image.Width > this.Width) Carro.PosX = this.Width - CarroPictureBox.Image.Width;
            if (Carro.PosY < 0) Carro.PosY = 0;
            else if (Carro.PosY + CarroPictureBox.Image.Height > this.Height) Carro.PosY = this.Height - CarroPictureBox.Image.Height;
            PresentCar(carImage);

            if (ReplayManager.IsRecording) ReplayManager.Record(Carro.Ticks);
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.G:
                    if (ReplayManager.IsRecording)
                    {
                        ReplayManager.IsRecording = false;
                        ShowNotification("Gravação finalizada");
                    }
                    else
                    {
                        ReplayManager.StartWatching(new System.Collections.Generic.List<IRecordable> { Carro });
                        ShowNotification("Gravação iniciada");
                        
                    }
                    break;
                case Keys.R:
                    if (ReplayManager.HasRecords && !ReplayManager.IsRecording)
                    {
                        ReplayManager.IsReplaying = true;
                        ShowNotification("Reprodução iniciada");
                    }
                    break;
            }

            if (ReplayManager.IsReplaying) return;
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
            if (ReplayManager.IsReplaying) return;
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

            backgroundImage = ResizeImage(backgroundImage, this.ClientSize.Width, this.ClientSize.Height);

            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = backgroundImage;
            pictureBox.Dock = DockStyle.Fill;

            this.Controls.Add(pictureBox);

            CarroPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;

            Image carroImage = Properties.Resources.TAXI_CLEAN_ALLD0000;

            CarroPictureBox.Image = ResizeImage(carroImage, 97, 53);
            CarroPictureBox.BackColor = Color.Transparent;
            CarroPictureBox.Left = Carro.PosX;
            CarroPictureBox.Top = Carro.PosY;
            CarroPictureBox.Dock = DockStyle.None;
            pictureBox.Controls.Add(CarroPictureBox);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
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

        private void PresentCar(Image img)
        {
            CarroPictureBox.Image = img;
            CarroPictureBox.Left = Carro.PosX;
            CarroPictureBox.Top = Carro.PosY;
        }

        static bool CorEhParecidaComBranco(Color cor, int tolerancia)
        {
            int diferencaR = Math.Abs(cor.R - Color.WhiteSmoke.R);
            int diferencaG = Math.Abs(cor.G - Color.WhiteSmoke.G);
            int diferencaB = Math.Abs(cor.B - Color.WhiteSmoke.B);

            return (diferencaR <= tolerancia && diferencaG <= tolerancia && diferencaB <= tolerancia);
        }

        private void InitializeNotificationLabel()
        {
            notificationLabel = new Label
            {
                AutoSize = true,
                BackColor = Color.Yellow,
                Padding = new Padding(10),
                FlatStyle = FlatStyle.Flat,
                BorderStyle = BorderStyle.FixedSingle,
                Visible = false,
                Font = new Font("Arial", 16),
            };

            Controls.Add(notificationLabel);
        }

        private void ShowNotification(string message)
        {
            notificationLabel.Text = message;
            notificationLabel.Location = new Point(
                Screen.PrimaryScreen.WorkingArea.Width - notificationLabel.Width,
                0
            );

            notificationLabel.Visible = true;

            Timer timer = new Timer();
            timer.Interval = 5000; // Tempo de exibição da notificação (5 segundos)
            timer.Tick += (sender, e) =>
            {
                notificationLabel.Visible = false;
                timer.Dispose();
            };
            timer.Start();
        }

    }
}