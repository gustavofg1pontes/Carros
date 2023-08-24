using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceIF
{
    public class Freio
    {
        private bool _acionado;

        public Freio()
        {
            _acionado = false;
        }

        public bool Acionado
        {
            get => _acionado;
            set => _acionado = value;
        }

    }
}
