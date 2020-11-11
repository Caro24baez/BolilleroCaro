using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BolilleroCaro
{
    public class Bolillero
    {
        public List<int> bolillasAdentro { get; set; }

        public List<int> bolillasAfuera{ get; set; }

        public int cantBolillas { get; set; }

        Random numRand = new Random();

        public Bolillero(int inico, int fin)
        {
            bolillasAdentro = new List<int>();
            bolillasAfuera = new List<int>();
        }

        public Bolillero()
        {
        }

        public void llenarBolillero(int inicio, int fin)
        {
            for (int i = 0; i < cantBolillas; i++)
            {
                bolillasAdentro.Add(i);
            }
        }

        public void regresarBolillas()
        {
            bolillasAdentro.AddRange(bolillasAfuera);
            bolillasAfuera.Clear();
        }


        public int sacarBolilla()
        {
            int bolilla = bolillasAdentro[numRand.Next(bolillasAdentro.Count)];
            bolillasAfuera.Add(bolilla);
            bolillasAdentro.Remove(bolilla);
            return bolilla;
        }

       
        }
    }

