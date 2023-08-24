using System;
using System.Numerics;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RaceIF
{
    public class Direcao : ICarroComponente
    {
        private float _angulo;
        private bool _power;
        public Vector2 VetorVisao { get; set; }
        public Acelerador Acelerador { get; }
        public Freio Freio { get; set; }
        public Cambio Cambio { get; set; }

        public Direcao()
        {
            _angulo = 0f;
            RodaFrontalDireita = new Roda("frontal", "direita");
            RodaFrontalEsquerda = new Roda("frontal", "esquerda");
            RodaTraseiraDireita = new Roda("traseira", "direita");
            RodaTraseiraEsquerda = new Roda("traseira", "esquerda");
            Cambio = new Cambio();
            Acelerador = new Acelerador();
            Freio = new Freio();
            VetorVisao = new Vector2(1, 0);
        }

        private Roda RodaFrontalDireita { get; set; }

        private Roda RodaFrontalEsquerda { get; set; }

        private Roda RodaTraseiraDireita { get; set; }

        private Roda RodaTraseiraEsquerda { get; set; }


        public void VirarADireita(float angulo)
        {
            Angulo += angulo;
        }

        public void VirarAEsquerda(float angulo)
        {
            Angulo -= angulo;
        }

        public void ResetarAngulo()
        {
            Angulo = 0;
        }

        public void Start()
        {
            _power = true;
        }

        public void Stop()
        {
            _power = false;
        }

        public void Acelerar()
        {
            if (!_power) return;
            Acelerador.Acelerar(1, _angulo);
            Cambio.TrocarMarcha(Acelerador.Velocidade);
        }

        public void Desacelerar()
        {
            if (!_power) return;
            if (Cambio.MarchaIndex > 1)
            {
                Frear(2);
                return;
            }
            // todo ré
        }

        public void Frear(int vel)
        {
            Freio.Acionado = true;
            if (Acelerador.Velocidade > 0)
            {
                Acelerador.Desacelerar(vel, _angulo);
                if (Acelerador.Velocidade < 0) Acelerador.Velocidade = 0;
            }
            else
            {
                Acelerador.Acelerar(vel, _angulo);
                if (Acelerador.Velocidade > 0) Acelerador.Velocidade = 0;
            }

            Freio.Acionado = false;
        }

        public void Inercia()
        {
            Frear(1);
        }

        public float Angulo
        {
            get => _angulo;
            private set
            {
                if (!(value <= 30f) || !(value >= -30f)) return;
                _angulo = value;
                RodaFrontalDireita.Angulo = value;
                RodaFrontalEsquerda.Angulo = value;
            }
        }
    }
}