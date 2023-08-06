using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Carros
{
    public class Carro : Ignicao
    {
        public string Fabricante;
        public string Modelo;
        public bool Acelerando = false;
        public int PosX;
        public int PosY;

        public Carro(string fabricante, string modelo) : base(new Direcao())
        {
            Fabricante = fabricante;
            Modelo = modelo;
            PosX = 50;
            PosY = 60;
        }

        public void UpdateLocation()
        {
            // todo ticks system
            // por natureza o carro vai tender a ficar parado!
            if(!Acelerando) Direcao.Inercia();
            PosX += (int)(Direcao.Acelerador.Velocidade * Math.Sin(Direcao.Angulo));
            PosY += (int)(Direcao.Acelerador.Velocidade * Math.Cos(Direcao.Angulo));
        }
    }
}
