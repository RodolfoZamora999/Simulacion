using Proyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConversionCodigo
{
    /// <summary>
    /// Corridas arriba y abajo del promedio. Prueba de independencia.
    /// Clase que analiza si una muestra de números pseudoaleatorios son independientes entre si.
    /// </summary>
    class ArribaAbajoPromedio
    {
        StringBuilder stringb;

        private PanelPrueba1 panelPrueba1;

        private Distribucion distribucion;
        private double valorTabla;
        private int toleranciaError;

        public ArribaAbajoPromedio(PanelPrueba1 panelPrueba1)
        {
            this.ToleranciaError = Distribucion.T5;

            this.panelPrueba1 = panelPrueba1;

            //Se crea una instancia de la tabla
            this.distribucion = new Distribucion();
        }

        public ArribaAbajoPromedio(int tolerancia, PanelPrueba1 panelPrueba1)
        {
            this.ToleranciaError = tolerancia;

            this.panelPrueba1 = panelPrueba1;

            //Se crea una instancia de la tabla
            this.distribucion = new Distribucion();
        }

        public int ToleranciaError { get => toleranciaError; set => toleranciaError = value; }

        /// <summary>
        /// Método encargado de hacer el cálculo de la JiCuadrada para cada longitud.
        /// </summary>
        /// <param name="frecuenciaEsperada">Una matriz con los valores de la frecuencia esperada.</param>
        /// <param name="frecuenciaObservada">Una matriz con los valores de la frecuencia observada.</param>
        /// <returns></returns>
        private double CalcularJiCuadrada(double[] frecuenciaEsperada, double[] frecuenciaObservada)
        {
            if (frecuenciaEsperada.Length != frecuenciaObservada.Length)
                return -1;

            double jiCuadrada = 0;

            for (int i = 0; i < frecuenciaEsperada.Length; i++)
                jiCuadrada += Math.Pow((frecuenciaEsperada[i] - frecuenciaObservada[i]), 2) / frecuenciaEsperada[i];

            return jiCuadrada;
        }

        /// <summary>
        /// Método que se encarga de contar todas las secuencias dentro de una muestra.
        /// Cuenta cuantas veces se repite una secuencia.
        /// </summary>
        /// <param name="secuencia">Matriz con la secuencia que se desea analizar.</param>
        /// <param name="longitudes">Una matriz que contiene las cantidades diferentes de longitudes dentro de la muestra.</param>
        /// <returns>Una matriz con la cantidad de secuencias contadas en orden (1, 2, 3, etc.)</returns>
        private double[] FrecuenciaObservada(int[] secuencia, Int32[] longitudes)
        {
            List<int> cont = new List<int>();

            int contador = 1;

            int apoyo = 0;

            //Cuenta todas las longitudes
            for (int i = 0; i < secuencia.Length; i++)
            {
                if (i != 0)
                {
                    if (apoyo == secuencia[i])
                        contador++;

                    else
                    {
                        cont.Add(contador);
                        apoyo = secuencia[i];
                        contador = 1;
                    }

                    if (i == (secuencia.Length - 1))
                        cont.Add(contador);
                }
                else
                    apoyo = secuencia[i];
            }

            //Reagrupa la cantidades de secuencias diferentes...
            double[] matriz = new double[longitudes.Length];

            for (int i = 0; i < longitudes.Length; i++)
            {
                apoyo = longitudes[i];

                contador = 0;

                for (int j = 0; j < cont.Count(); j++)
                {
                    if (apoyo == cont[j])
                        contador++;
                }

                matriz[i] = contador;
            }

            return matriz;
        }

        /// <summary>
        /// Método que se encarga de calcular la frecuencia esperada para cada longitud.
        /// </summary>
        /// <param name="longitudes">Una matriz con las longitudes contadas. Está matriz tiene que estar ordenada.</param>
        /// <param name="cantNumeros">La cantidad de números que vienen en la muestra original.</param>
        /// <returns></returns>
        private double[] FrecuenciaEsperada(int[] longitudes, int cantNumeros)
        {
            //Ordenamos la matriz
            Array.Sort(longitudes);

            double[] frecuenciaEsperada = new double[longitudes.Length];

            for (int i = 1; i <= longitudes.Length; i++)
            {
                frecuenciaEsperada[i - 1] = (cantNumeros - longitudes[i - 1] + 3) / (Math.Pow(2, longitudes[i - 1] + 1));
            }

            return frecuenciaEsperada;
        }

        /// <summary>
        /// Método encargado de contar las secuencias dentro de una muestra.
        /// Si una secuencia se repite más de una vez no la va a considerar de nuevo.
        /// </summary>
        /// <param name="secuencia">Una matriz con la secuencia que se desee analizar</param>
        /// <returns>Una matriz con las longitudes contadas.</returns>
        private int[] ContarLongitudes(int[] secuencia)
        {
            //Estructura de datos que agrega datos si es que no están disponibles
            HashSet<int> hashSet = new HashSet<int>();

            int contador = 1;

            int apoyo = 0;

            for (int i = 0; i < secuencia.Length; i++)
            {
                if (i != 0)
                {
                    if (apoyo == secuencia[i])
                        contador++;

                    else
                    {
                        hashSet.Add(contador);
                        contador = 1;
                        apoyo = secuencia[i];
                    }

                    if (i == secuencia.Length - 1)
                        hashSet.Add(contador);
                }
                else
                    apoyo = secuencia[i];
            }

            //Conversión de hashset a array
            int[] array = hashSet.ToArray();

            //Ser ordena el array
            Array.Sort(array);

            return array;
        }

        /// <summary>
        /// Método que se encarga de generar una secunecia de 0s y 1s apartir de una muestra de números pseudoaleatorio.
        /// Estos números tienen que estar entre 0 y 1.
        /// </summary>
        /// <param name="muestra">Muestra de números ha analizar.</param>
        /// <returns>Una matriz con la secuencia de 0s y 1s correspondiente</returns>
        private int[] GenenerarSecuencia(double[] muestra)
        {
            int[] secuencia = new int[muestra.Length];


            for (int i = 0; i < muestra.Length; i++)
            {
                if (muestra[i] > 0.5)
                    secuencia[i] = 1;

                else if (muestra[i] <= 0.5)
                    secuencia[i] = 0;
            }

            return secuencia;
        }

        /// <summary>
        /// Método de apoyo que sirve para buscar un valor determinado en la tabla de distribución y lo compara con el 
        /// valor obtenido en la prueba.
        /// </summary>
        /// <param name="jiCuadrada">Valor calculado</param>
        /// <param name="longitudes">Cantidad de longitudes diferentes</param>
        /// <returns></returns>
        private bool BuscarValor(double jiCuadrada, int longitudes)
        {
            this.valorTabla = this.distribucion.BuscarValorJi(longitudes - 1, ToleranciaError);

            stringb.Append("\r\nValor de JiCuadrada en la tabla: \r\n" + valorTabla);
           
            panelPrueba1.InsertarTextBox(stringb.ToString());

            panelPrueba1.InsertarLabels( Math.Round(jiCuadrada, 5).ToString(), valorTabla.ToString());

            return jiCuadrada < valorTabla;
        }




        /// <summary>
        /// Método que se encarga de analizar si una muestra de números pseudoaleatorios 
        /// esta realmente uniformemente distribuida.
        /// </summary>
        /// <param name="muestra">Una muestra de números pseudoaleatorios que se analizaran.</param>
        /// <returns>True si la muestra pasa la prueba, false en caso contrario.</returns>
        public bool EsIndependiente(double[] muestra)
        {
            //Variable de apoyo
            stringb = new StringBuilder();

            //Primer paso: Generar la secuencia.
            int[] secuencia = GenenerarSecuencia(muestra);
            stringb.Append("Secuencias generadas: \r\n");
            for(int i = 0; i<secuencia.Length; i++)
            {
                stringb.Append(secuencia[i] + " ");

                if ((i + 1) % 25 == 0)
                    stringb.Append("\r\n");
            }

            //Segundo paso: Contar las secuencias disponibles.
            int[] longitudes = ContarLongitudes(secuencia);
            stringb.Append("\r\nLongitudes diferentes contadas: " + longitudes.Length+"\r\n");
            foreach (var value in longitudes)
                stringb.Append(value.ToString() + "\r\n");

            //Tercer paso: Calcular las frecuencias esperadas.
            double[] frecEsperada = FrecuenciaEsperada(longitudes, muestra.Length);
            stringb.Append("\r\nFrecuencias esperadas: \r\n");
            foreach (var value in frecEsperada)
                stringb.Append(value + "\r\n");

            //Cuarto paso: Calcular las frecuencias observadas.
            double[] frecObservada = FrecuenciaObservada(secuencia, longitudes);
            stringb.Append("\r\nFrecuencias observadas: \r\n");
            foreach (var value in frecObservada)
                stringb.Append(value.ToString() + " \r\n");

            //Quinto paso: Calcular la JiCuadrada
            double jiCuadrada = CalcularJiCuadrada(frecEsperada, frecObservada);
            stringb.Append("\r\nValor de JiCuadrada calculado: " + jiCuadrada + "\r\n");

            return BuscarValor(jiCuadrada, longitudes.Length);
        }

    }
}
