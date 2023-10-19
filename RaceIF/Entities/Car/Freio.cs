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

        public void Reset()
        {
            _acionado = false;
        }
    }
}
