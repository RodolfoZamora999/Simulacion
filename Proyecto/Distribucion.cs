using System;
using System.IO;

namespace ConversionCodigo
{
    /// <summary>
    /// Clase que se encarga de cargar y buscar un valor determinado de la tabla de distribución JiCuadrada.
    /// </summary>
    class Distribucion
    {
        private readonly double[,] valores;

        //Constantes para los porcentajes de toleranciaError.
        public readonly static int T1 = 0;
        public readonly static int T2_5 = 1;
        public readonly static int T5 = 2;
        public readonly static int T10 = 3;
        public readonly static int T15 = 4;
        public readonly static int T20 = 5;
        public readonly static int T25 = 6;
        public readonly static int T30 = 7;
        public readonly static int T35 = 8;
        public readonly static int T40 = 9;
        public readonly static int T45 = 10;
        public readonly static int T50 = 11;

        public Distribucion()
        {

            //Valores fijos. Determinados por mí.
            valores = new double[40, 12];

            CargarValores();
        }

        /// <summary>
        /// Método que carga un rango de valores de la tabla ChiCuadrada a un array.
        /// </summary>
        private void CargarValores()
        {
            //Nota: Cuidado donde almacenas el archivo de texto con los valores.

            try
            {
                string ruta = Directory.GetCurrentDirectory() + "\\data\\values.txt";


                if (!File.Exists(ruta))
                    return;

                //El archivo de texto tiene que estar en la misma carpeta que el programa 
                StreamReader stream = new StreamReader(ruta);

                string linea = "";

                int contadorFila = 0;
                int contadorColumna = 0;

                while ((linea = stream.ReadLine()) != null)
                {

                    char[] caracteres = linea.ToCharArray();

                    string palabra = "";


                    for (int i = 0; i < caracteres.Length; i++)
                    {

                        if (Char.IsWhiteSpace(caracteres[i]) || i == (caracteres.Length - 1))
                        {
                            valores[contadorFila, contadorColumna] = Double.Parse(palabra);

                            palabra = "";
                            contadorColumna++;
                        }
                        else
                            palabra += caracteres[i];

                    }

                    contadorFila++;
                    contadorColumna = 0;
                }


            }

            catch(Exception)
            {
                Console.WriteLine("Archivo con los valores de JiCuadrada no encontrado...");
                Console.WriteLine("Pongase en contacto con el administrador de sistemas para solucionar el problema. 404");
            }

        }

        /// <summary>
        /// Método que retorna el valor de la tabla de JiCuadrado apartir del grado de liberdad y porcentaje de toleranciaError 
        /// </summary>
        /// <param name="gradoLiberdad">Grados de libertad deseados.  Minimo: 1  Máximo: 40 </param>
        /// <param name="tolerancia">Tolerancia de error deseado.   
        /// Tolerancias Admitidas: 1%, 2.5%, 5%, 10%, 20%, 25%, 30%, 35%, 40%, 45%, 50% </param>
        /// <returns>El valor correspondiente apartir de los parámetros proporcionados. Retorna -1 en caso de no ser valido.</returns>
        public double BuscarValorJi(int gradosLibertad, int tolerancia)
        {
            //Verifica si la matriz no está inicializada.
            if (valores == null)
                return -1;

            //Verifica que se cumpla la condición.
            if (gradosLibertad < 1 || gradosLibertad > 40)
                return -1;

            if (tolerancia < 0 || tolerancia > 11)
                return -1;



            return valores[gradosLibertad - 1, tolerancia];
        }
    }
}
