using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BolilleroCaro
{
    public class Simulacion
    {
        
        private Bolillero bolillero;
      //  private IRandomNumberGenerator azar;

        private readonly IRandomNumberGenerator azar;
        public Simulacion(Bolillero bolillero, IRandomNumberGenerator azar) // toma un bolillero y un generador de números aleatorios
        {
            this.bolillero = bolillero;
            this.azar = azar;
        }
        public bool Jugar(List<int> jugadas) => bolillero.sacarJugada(jugadas);


        public int JugarNVeces(List<int> jugadas, int cantVeces, Bolillero bolillero)
        {
            int cantGanados = 0;
            for (int i = 0; i < cantVeces; i++)
            {
                bolillero.regresarBolillas();
                if (this.Jugar(jugadas) == true)
                {
                    cantGanados++;
                }


            }
            return cantGanados;

        }
        public long SimularSinHilos(Bolillero bolillero, List<int> jugada, int cantSimulaciones)
        {
            long cantGanados = 0;
            for (int i = 0; i < cantSimulaciones; i++)
            {
                Bolillero bolilleroClone = (Bolillero)bolillero.Clone();
                bolilleroClone.regresarBolillas();
                if (bolilleroClone.sacarJugada(jugada))
                {
                    cantGanados++;
                }
            }
            return cantGanados;
            
        }
        public int SimularConHilos(Bolillero bolillero, List<int> jugada, int jugarNVeces, int cantHilos)
        {
            var vectorTarea = new Task<int>[cantHilos];
            for (int i = 0; i < cantHilos; i++)
            {
                Bolillero clon = (Bolillero)bolillero.Clone();
                vectorTarea[i] = Task.Run(() =>
                {
                    int ganadosEnHilo = 0;
                    for (int j = 0; j < jugarNVeces / cantHilos; j++)
                    {
                        lock (clon) // Acceso seguro al bolillero
                        {
                            clon.regresarBolillas();
                            if (clon.sacarJugada(jugada))
                            {
                                ganadosEnHilo++;
                            }
                        }
                    }
                    return ganadosEnHilo;
                });
            }
            Task.WaitAll(vectorTarea);
            
            foreach (var tarea in vectorTarea)
            {
                if (tarea.Exception != null)
                {
                    Console.WriteLine("Ocurrió un error durante la simulación: " + tarea.Exception.Message);
                    return 0; 
                }
            }

            int totalGanados = vectorTarea.Sum(t => t.Result);
            return totalGanados;
        }
        public async Task<long> SimularConHilosAsync(Bolillero bolillero, List<int> jugada, int cantSimulaciones, int cantHilos)
        {
            var tasks = new List<Task<long>>();

            for (int i = 0; i < cantHilos; i++)
            {
                Bolillero bolilleroClone = (Bolillero)bolillero.Clone();
                tasks.Add(SimularHiloAsync(bolilleroClone, jugada, cantSimulaciones / cantHilos));
            }

            long totalGanados = 0;
            foreach (var task in tasks)
            {
                totalGanados += await task.ConfigureAwait(false);
            }

            return totalGanados;
        }

        private async Task<long> SimularHiloAsync(Bolillero bolillero, List<int> jugada, int cantSimulaciones)
        {
            long cantGanados = 0;
            for (int i = 0; i < cantSimulaciones; i++)
            {
                bolillero.regresarBolillas(); // Restablece el estado del bolillero clonado si es necesario
                if (bolillero.sacarJugada(jugada))
                {
                    cantGanados++;
                }
            }
            return cantGanados;
        }

       /* public int SimularConHilos(Bolillero bolillero, List<int> jugada, int jugarNVeces, int cantHilos)
        {
            throw new NotImplementedException();
        }*/
    }
}
