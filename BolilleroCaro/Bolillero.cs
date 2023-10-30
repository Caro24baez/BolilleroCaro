using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;

namespace BolilleroCaro
{
    public class Bolillero : ICloneable
    {
        public List<int> bolillasAdentro { get; private set; }
        public List<int> bolillasAfuera { get; private set; }
        public int cantBolillas { get; private set; }
        public int lengJugadas { get; private set; }
        private readonly IRandomNumberGenerator azar;

        public Bolillero(int cantBolilla, IRandomNumberGenerator azar, int lengJugada)
        {
            this.azar = azar;
            cantBolillas = cantBolilla;
            lengJugadas = lengJugada;
            bolillasAdentro = new List<int>();
            bolillasAfuera = new List<int>();
            llenarBolillero(cantBolilla);
        }

        public Bolillero()
         {
             bolillasAdentro = new List<int>();
             bolillasAfuera = new List<int>();
             this.azar = azar;
         }

        private Bolillero(Bolillero original)
        {
            bolillasAdentro = new List<int>(original.bolillasAdentro);
            bolillasAfuera = new List<int>(original.bolillasAfuera);
            this.azar = original.azar;
        }
        private void llenarBolillero(int cantBolilla)
        {
            Console.WriteLine("Llenando bolillero con " + cantBolilla + " bolillas...");
            for (int i = 0; i < cantBolilla; i++)
            {
                bolillasAdentro.Add(i);
            }
        }

        public void regresarBolillas()
        {
            bolillasAdentro.AddRange(bolillasAfuera);
            bolillasAfuera.Clear();
        }

        public bool verificarJugada(List<int> jugada, List<int> jugada2) => jugada.SequenceEqual(jugada2);

        public bool sacarJugada(List<int> jugada)
        {
            for (int i = 0; i < jugada.Count; i++)
            {
                var bolilla = sacarBolilla();
                Console.WriteLine("Bolilla sacada " + bolilla);
                // Console.WriteLine($"Bolilla esperada: {jugada[i]}");
                if (bolilla == jugada[i])
                {
                    return true;
                }
                if (bolilla != jugada[i])
                {
                    return false; //La manera que la jugada sea exitosa, en nuestro caso, tiene que salir el 1 y el 8 en ese orden. (1 - 8).
                }
                    
            }
            return true;
        }

        public int sacarBolilla()
        {
            if (bolillasAdentro != null && bolillasAdentro.Count > 0)
            {
                int bolillaIndex = azar.Next(0, bolillasAdentro.Count);
                int bolilla = bolillasAdentro[bolillaIndex];
                bolillasAfuera.Add(bolilla);
                bolillasAdentro.RemoveAt(bolillaIndex);
                return bolilla;
            }
            else
            {
                Console.WriteLine("No hay bolillas adentro en el bolillero.");
                return -1;
            }
        }

        public object Clone()
        {
            Bolillero bolilleroClone = new Bolillero(this);
            return bolilleroClone;
        }
    }
} 

