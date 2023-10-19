using System.Collections.Generic;
using System.Linq;

namespace RaceIF
{
    public class Cambio
    {
        private int _marchaIndex;
        public readonly List<Marcha> Marchas;

        public Cambio()
        {
            _marchaIndex = 0;
            Marchas = new List<Marcha>
            {
                new Marcha(-1, -20, 0),
                new Marcha(0, 0, 0),
                new Marcha(1, 0, 20),
                new Marcha(2, 20, 40),
                new Marcha(3, 40, 50),
                new Marcha(4, 50, 60),
                new Marcha(5, 60, 122)
            };
        }

        public void TrocarMarcha(float velocidade)
        {
            if (velocidade == 0)
            {
                _marchaIndex = 0;
                return;
            }
            foreach (var marcha in Marchas.Where(marcha => !(velocidade < marcha.MarchaIndex) && !(velocidade > marcha.Max)))
            {
                _marchaIndex = marcha.MarchaIndex;
                return;
            }
        }

        public int MarchaIndex
        {
            get => _marchaIndex;
            set
            {
                if (value <= 6 && value >= -1)
                    _marchaIndex = value;
            }
        }

        public void Reset()
        {
            _marchaIndex = 0;
        }
    }
}
