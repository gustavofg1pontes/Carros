using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carros
{
    public class Roda
    {
        private string _posLogintudinal;
        private string _posTransversal;
        private float _angulo;

        public Roda(string posLogintudinal, string posTransversal)
        {
            _posLogintudinal = posLogintudinal;
            _posTransversal = posTransversal;
            _angulo = 0.0f;
        }

        public string PosLogintudinal
        {
            get => _posLogintudinal;
            set
            {
                if (value.ToLower().Equals("frontal") || value.ToLower().Equals("traseira"))
                    _posLogintudinal = value;
            }
        }

        public string PosTransversal
        {
            get => _posTransversal;
            set
            {
                if (value.ToLower().Equals("esquerda") || value.ToLower().Equals("direita"))
                    _posTransversal = value;
            }
        }

        public float Angulo
        {
            get => _angulo;
            set
            {
                if (value <= 30f && value >= -30f)
                    _angulo = value;
            }
        }
    }
}