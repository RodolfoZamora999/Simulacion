using Proyecto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConversionCodigo
{
    /// <summary>
    /// Prueba de JiCuadrada, tambien conocida como prueba de frecuencias.
    /// Clase que se encarga de analizar un conjunto de números pseudoaleatorios y 
    /// comprueba si se encuentra distribuidos de manera uniforme.
    /// </summary>
    class JiCuadrada
    {
        private Distribucion distribucion;
        private double valorTabla;
        private int toleranciaError;
        private double intervalos;
        private PanelPrueba2 panel;

        StringBuilder stringb;

        public int ToleranciaError { get => toleranciaError; set => toleranciaError = value; }
        public double Intervalos { get => intervalos; set => intervalos = value; }

        public double ValorTabla { get => valorTabla; set => valorTabla = value; }

        /// <summary>
        /// Método constructor vacio, al no proporcionase párametros para los valroes de "Tolerancia de error" e "Intervalos"
        /// estos se eligeran de manera automatica,
        /// Tolerancia de error = %5   Intervalos = raizCuadrada(CantidadMuestra)
        /// </summary>
        public JiCuadrada()
        {
            this.ToleranciaError = Distribucion.T5;

            //Se crea una instancia de la tabla
            this.distribucion = new Distribucion();
        }


        /// <summary>
        /// Método constructor en el que se define la toleranciaError de error.
        /// </summary>
        /// <param name="toleranciaError">Tolenacia de error.</param>
        public JiCuadrada(int toleranciaError, PanelPrueba2 panel)
        {
            this.ToleranciaError = toleranciaError;
            this.panel = panel;

            //Se crea una instancia de la tabla
            this.distribucion = new Distribucion();
        }


        /// <summary>
        /// Método constructor en el que se define la toleranciaError de error y la cantidad de intervalos desde el inicio.
        /// </summary>
        /// <param name="toleranciaError">Tolenacia de error.</param>
        /// <param name="intervalos">Intervalos deseados.</param>
        public JiCuadrada(int toleranciaError, double intervalos, PanelPrueba2 panel)
        {
            this.Intervalos = intervalos;
            this.ToleranciaError = toleranciaError;
            this.panel = panel;

            //Se crea una instancia de la tabla
            this.distribucion = new Distribucion();
        }

        /// <summary>
        /// Método de apoyo que genera una cantidad de intervaos estomada apartir de la cantidad de la muestra. RaizCuadrada(MuestraCantidad).
        /// </summary>
        /// <param name="longitud">Valor entero que representan el tamaño de la muestra</param>
        /// <returns>Cantidad de intervalos estimada.</returns>
        private double GenerarIntervalos(double longitud)
        {
            double valor = Math.Round(Math.Sqrt(longitud));

            //Limite para las cosas sigan bien
            if (valor > 40)
                valor = 40;

            return valor;
        }

        /// <summary>
        /// Método que determina la frecuencia esperada para todos los intervalos.
        /// </summary>
        /// <param name="longitud">Tamaño de la muestra proporcionada.</param>
        /// <returns></returns>
        private double CalcularFrecuenciaEsperada(int longitud)
        {
            double valor = longitud / Intervalos;

            return valor;
        }

        /// <summary>
        /// Método que hace el conteo de número en frecuencias.
        /// </summary>
        /// <param name="muestra">Matriz de números que se desea analizar.</param>
        /// <returns>Una matriz de enteros con las frecunecias contadas.</returns>
        private int[] CalcularFrecuenciasObservadas(double[] muestra)
        {
            double frecuencia = 1 / Intervalos;

            double min = 0;
            double max = frecuencia;

            int contador;

            int[] contadas = new int[(int)Intervalos];

            for (int i = 0; i < contadas.Length; i++)
            {
                contador = 0;

                //Se comparan los valores en frecuencias
                for (int j = 0; j < muestra.Length; j++)
                    if (muestra[j] >= min && muestra[j] < max)
                        contador++;


                contadas[i] = contador;


                min += frecuencia;
                max += frecuencia;
            }

            return contadas;
        }

        /// <summary>
        /// Método que sirve para hacer la sumatoria del valor de chiCuadrada de todos los intervalos.
        /// </summary>
        /// <param name="frecuenciaEsperada">El valor de la frecuencia esperada para todas los intervalos.</param>
        /// <param name="frecuenciasObservadas">Matriz que contiene las frecuencias observadas para cada intervalo.</param>
        /// <returns></returns>
        private double CalcularJiCuadrada(double frecuenciaEsperada, int[] frecuenciasObservadas)
        {
            double value = 0;

            for (int i = 0; i < frecuenciasObservadas.Length; i++)
                value += (Math.Pow(frecuenciaEsperada - frecuenciasObservadas[i], 2)) / frecuenciaEsperada;

            return value;
        }

        /// <summary>
        /// Método de apoyo que sirve para buscar un valor determinado en la tabla de distribución y lo compara con el 
        /// valor obtenido en la prueba.
        /// </summary>
        /// <param name="jiCuadrada"></param>
        /// <returns></returns>
        private bool BuscarValor(double jiCuadrada)
        {

            this.valorTabla = this.distribucion.BuscarValorJi(((int)intervalos) - 1, ToleranciaError);

            stringb.Append("\r\nValor de JiCuadrada en la tabla: \r\n" + valorTabla);

            panel.InsertarTextBox(stringb.ToString());

            panel.InsertarLabels(Math.Round(jiCuadrada, 5).ToString(), valorTabla.ToString());

            return jiCuadrada < valorTabla;
        }


        /// <summary>
        /// Método que que analiza una muestra de números pseudoaleatorios y 
        /// determina si se encuentran distribuidos de manera uniforme.
        /// </summary>
        /// <param name="muestra">Una matriz con los números pseudoaleatorios ha analizar.</param>
        /// <returns>True si paso la prueba, false en caso contrario.</returns>
        public bool EsUniforme(double[] muestra)
        {
            //Objeto de prueba
            stringb = new StringBuilder();

            //Asignación de intervalos de manera automatica.
            if (Intervalos == 0)
                this.Intervalos = GenerarIntervalos(muestra.Length);

            //Calculo del valor esperado. 
            double valorEsperado = CalcularFrecuenciaEsperada(muestra.Length);

           stringb.Append("Intervalos: \r\n"+intervalos);
           stringb.Append("\r\n\r\nFrecuencia esperada:\r\n" + valorEsperado);

            int[] frecuenciasObservadas = CalcularFrecuenciasObservadas(muestra);

            stringb.Append("\r\n\r\nFrecuencias observadas:\r\n");
            foreach (var value in frecuenciasObservadas)
                stringb.Append(value.ToString() + " \r\n");

            double JiCuadrada = CalcularJiCuadrada(valorEsperado, frecuenciasObservadas);

            stringb.Append("\r\nValor de JiCuadrada calculado:\r\n" + JiCuadrada + "\r\n");

            return BuscarValor(JiCuadrada);
        }
    }

}
