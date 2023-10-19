using System.Numerics;

namespace RaceIF
{
    public class Direcao : ICarroComponente
    {
        //todo VetorDirecao não mudar de direção ao dar ré
        private float _angulo;
        private bool _power;


        public Vector2 VetorVisao;
        public bool ReAtivada { get; set; }
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
            VetorVisao = VectorUtils.CreateVectorFromScalarAndAngle(1, 0);
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

        public void Reset()
        {
            Angulo = 0;
            Acelerador.Reset();
            Freio.Reset();
            Cambio.Reset();
        }

        public void Start()
        {
            _power = true;
        }

        public void Stop()
        {
            _power = false;
        }

        public void AtualizarVetorDirecao()
        {
            if (VectorUtils.HasChangedDirection(Acelerador.VetorVel, this.VetorVisao)){
                this.VetorVisao = VectorUtils.CreateVectorFromScalarAndAngle(1, VectorUtils.GetOppositeAngle(this.Acelerador.VetorVel));
                ReAtivada = true;
            }
            else
            {
                this.VetorVisao = VectorUtils.CreateVectorFromScalarAndAngle(1, VectorUtils.AngleFromVector(this.Acelerador.VetorVel));
                ReAtivada = false;
            }
        }

        public void Acelerar()
        {
            if (!_power) return;
            if (Acelerador.VetorAceleracao.Length() < 3)
            {
                Acelerador.Acelerar(0.5f, VectorUtils.AngleFromVector(VetorVisao));
            }
            Cambio.TrocarMarcha(Acelerador.VetorVel.Length());
        }

        public void Desacelerar()
        {
            if (!_power) return;
            if (Cambio.MarchaIndex > 0)
            {
                Frear(.5f);
                return;
            }
        }

        public void Frear(float vel)
        {
            Freio.Acionado = true;
            Acelerador.Desacelerar(vel, VectorUtils.AngleFromVector(VetorVisao));
            Freio.Acionado = false;
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