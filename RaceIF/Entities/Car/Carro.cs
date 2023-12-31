﻿using RaceIF.Replay;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace RaceIF
{
    public class Carro : IRecordable, IPresentable
    {
        public static long Ticks = 0;
        public string Modelo;
        public string Fabricante;
        public int Ano;
        public bool Ligado;
        public Direcao Direcao { get; }
        public int PosX {get; set;}
        public int PosY { get; set; }

        public List<Image> Sprites = new()
        {
            Properties.Resources.TAXI_CLEAN_ALLD0000,
            Properties.Resources.TAXI_CLEAN_ALLD0001,
            Properties.Resources.TAXI_CLEAN_ALLD0002,
            Properties.Resources.TAXI_CLEAN_ALLD0003,
            Properties.Resources.TAXI_CLEAN_ALLD0004,
            Properties.Resources.TAXI_CLEAN_ALLD0005,
            Properties.Resources.TAXI_CLEAN_ALLD0006,
            Properties.Resources.TAXI_CLEAN_ALLD0007,
            Properties.Resources.TAXI_CLEAN_ALLD0008,
            Properties.Resources.TAXI_CLEAN_ALLD0009,
            Properties.Resources.TAXI_CLEAN_ALLD0010,
            Properties.Resources.TAXI_CLEAN_ALLD0011,
            Properties.Resources.TAXI_CLEAN_ALLD0012,
            Properties.Resources.TAXI_CLEAN_ALLD0013,
            Properties.Resources.TAXI_CLEAN_ALLD0014,
            Properties.Resources.TAXI_CLEAN_ALLD0015,
            Properties.Resources.TAXI_CLEAN_ALLD0016,
            Properties.Resources.TAXI_CLEAN_ALLD0017,
            Properties.Resources.TAXI_CLEAN_ALLD0018,
            Properties.Resources.TAXI_CLEAN_ALLD0019,
            Properties.Resources.TAXI_CLEAN_ALLD0020,
            Properties.Resources.TAXI_CLEAN_ALLD0021,
            Properties.Resources.TAXI_CLEAN_ALLD0022,
            Properties.Resources.TAXI_CLEAN_ALLD0023,
            Properties.Resources.TAXI_CLEAN_ALLD0024,
            Properties.Resources.TAXI_CLEAN_ALLD0025,
            Properties.Resources.TAXI_CLEAN_ALLD0026,
            Properties.Resources.TAXI_CLEAN_ALLD0027,
            Properties.Resources.TAXI_CLEAN_ALLD0028,
            Properties.Resources.TAXI_CLEAN_ALLD0029,
            Properties.Resources.TAXI_CLEAN_ALLD0030,
            Properties.Resources.TAXI_CLEAN_ALLD0031,
            Properties.Resources.TAXI_CLEAN_ALLD0032,
            Properties.Resources.TAXI_CLEAN_ALLD0033,
            Properties.Resources.TAXI_CLEAN_ALLD0034,
            Properties.Resources.TAXI_CLEAN_ALLD0035,
            Properties.Resources.TAXI_CLEAN_ALLD0036,
            Properties.Resources.TAXI_CLEAN_ALLD0037,
            Properties.Resources.TAXI_CLEAN_ALLD0038,
            Properties.Resources.TAXI_CLEAN_ALLD0039,
            Properties.Resources.TAXI_CLEAN_ALLD0040,
            Properties.Resources.TAXI_CLEAN_ALLD0041,
            Properties.Resources.TAXI_CLEAN_ALLD0042,
            Properties.Resources.TAXI_CLEAN_ALLD0043,
            Properties.Resources.TAXI_CLEAN_ALLD0044,
            Properties.Resources.TAXI_CLEAN_ALLD0045,
            Properties.Resources.TAXI_CLEAN_ALLD0046,
            Properties.Resources.TAXI_CLEAN_ALLD0047
        };

        public Carro(string fabricante, string modelo)
        {
            Fabricante = fabricante;
            Modelo = modelo;
            PosX = 200;
            PosY = 115;
            Direcao = new Direcao();
        }

        public Image Update()
        {
            PosX += (int)(Direcao.Acelerador.VetorVel.X);
            PosY += (int)(Direcao.Acelerador.VetorVel.Y);

            Direcao.Acelerador.UpdateVetorAceleracao(Direcao.Angulo);
            Direcao.Acelerador.UpdateVetorDesaceleracao(Direcao.Angulo);
            Direcao.Acelerador.UpdateVetorVel(Direcao.Angulo);

            Direcao.AtualizarVetorDirecao();

            Ticks++;
            return Sprites[GetCurrentSprite()];
        }

        private int GetCurrentSprite()
        {
            double ang = VectorUtils.AngleFromVector(Direcao.VetorVisao);
            int supostoSprite = (int)Math.Round(ang / 7.5);
            return supostoSprite > 47 ? 0 : supostoSprite;
        }

        public void Start()
        {
            if (Ligado) return;
            Ligado = true;
            Direcao.Start();
        }

        public void Stop()
        {
            if (!Ligado || Direcao.Acelerador.VetorVel.Length() != 0 || Direcao.Cambio.MarchaIndex != 0) return;
            Ligado = false;
            Direcao.Stop();
        }

        public IEntityState Record()
        {
            return new EntityView(PosX, PosY, GetCurrentSprite());
        }

        public Image Present(IEntityState state)
        {
            if (state is EntityView st)
            {
                PosX = st.PosX;
                PosY = st.PosY;
                return Sprites[st.SpriteIndex];
            }
            return null;
        }


        public void Reset()
        {
            Ticks = 0;
            PosX = 200;
            PosY = 115;
            Direcao.Reset();
        }
    }
}