using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Carros
{
    public class Ignicao : ICarroComponente
    {
        public Direcao Direcao { get; }
        public bool Power { get; set; }

        protected Ignicao(Direcao direcao)
        {
            Power = false;
            Direcao = direcao;
        }

        public void Start()
        {
            if (Power) return;
            Power = true;
            Direcao.Start();
        }

        public void Stop()
        {
            if (!Power || Direcao.Acelerador.Velocidade != 0 || Direcao.Cambio.MarchaIndex != 0) return;
            Power = false;
            Direcao.Stop();
        }
    }
}