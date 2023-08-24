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
        public Acelerador()
        {
            Velocidade = 0f;
        }

        public float Velocidade { get; set; }
        public Vector2 VetorVel = new(0, 0);
        public Vector2 VetorAceleracao = new(0, 0);

        public void UpdateVetorVel(float angulo)
        {
            this.VetorVel = VectorUtils.AddAngles(VetorVel, angulo);
            if (VectorUtils.CalculateMagnitude(this.VetorVel) < 10)
                this.VetorVel += this.VetorAceleracao;
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
            this.VetorAceleracao -= VectorUtils.CreateVectorFromScalarAndAngle(vel, direcao);
        }
    }
}