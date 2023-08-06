using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carros
{
    public class Acelerador
    {
        public Acelerador()
        {
            Velocidade = 0f;
        }

        public float Velocidade { get; set; }
        public float Aceleracao { get; set; }

        public void Acelerar(float vel)
        {
            if(Velocidade + vel <= 10) Velocidade += vel;
        }

        public void Desacelerar(float vel)
        {
            if (Velocidade - vel >= -10) Velocidade -= vel;
        }
    }
}
