using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RaceIF
{
    public class Acelerador
    {
        //todo carro NÃO alterar direcao ao dar ré (quando dá ré, ele passa a acelerar para aquela direção, ficando "ao contrário")
        public bool Acelerando = false;
        public bool Desacelerando = false;

        public Acelerador()
        {

        }

        public Vector2 VetorVel = new(0, 0);
        public Vector2 VetorAceleracao = new(0, 0);
        public Vector2 VetorDesaceleracao = new(0, 0);

        public void UpdateVetorVel(float angulo)
        {
            this.VetorVel = VectorUtils.AddAngles(VetorVel, angulo);
            if (!Desacelerando && !Acelerando)
            {
                this.VetorAceleracao.X = 0;
                this.VetorAceleracao.Y = 0;
                this.VetorDesaceleracao.X = 0;
                this.VetorDesaceleracao.Y = 0;
            }
            this.VetorVel += this.VetorAceleracao - this.VetorDesaceleracao;
        }


        public void UpdateVetorAceleracao(float angulo)
        {
            this.VetorAceleracao = VectorUtils.AddAngles(VetorAceleracao, angulo);
        }

        public void Acelerar(float vel, float direcao)
        {
            this.VetorAceleracao += VectorUtils.CreateVectorFromScalarAndAngle(vel, direcao);
        }

        public void Desacelerar(float vel, float direcao)
        {
            this.VetorDesaceleracao += VectorUtils.CreateVectorFromScalarAndAngle(vel, direcao);
        }
    }
}