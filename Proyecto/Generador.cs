using System;

namespace ConversionCodigo
{
    class Generador
    {
        private readonly double semillaFinal, multiplicativo, aditivo, mod;
        private double semilla;

        /// <summary>
        /// Unico método constructor de la clase.
        /// </summary>
        /// <param name="multiplicativo">Valor de la constante multiplicativa</param>
        /// <param name="aditivo">Valor de la constante aditiva</param>
        /// <param name="semilla">El valor inicial  para empezar con la generación números. (La semilla)</param>
        /// <param name="mod">Valor del modulo, está tiene que ser mayor que cualquiera de los valores antes proporcionados</param>
        public Generador(double multiplicativo, double aditivo, double semilla, double mod)
        {
            this.multiplicativo = multiplicativo;
            this.aditivo = aditivo;
            this.semilla = semilla;
            this.mod = mod;
            this.semillaFinal = semilla;
        }

        /// <summary>
        /// Método que genera un número pseudoaleatorio apartir del método congruencial mixto.
        /// </summary>
        /// <returns>Un número pseudoaleatorio generador entre 0 y 1</returns>
        private double GenerarNumeroAleatorio()
        {
            double numero = ((multiplicativo * semilla) + aditivo);

            double resultado = numero % mod;

            semilla = resultado;

            resultado = resultado / mod;

            resultado = Math.Round(resultado, 5);

            return resultado;
        }

        /// <summary>
        /// Método que genera un conjunto de números pseudoaleatorios.
        /// </summary>
        /// <param name="cantidad">Especifica la cantidad de números a generar.</param>
        /// <returns>Un conjunto de números pseudoaleatorios en forma de una matriz.</returns>
        public double[] GenerarNumerosAleatorios(int cantidad)
        {
            if (cantidad == -1)
                return new double[0];

            semilla = semillaFinal;

            double[] numeros = new double[cantidad];

            for (int i = 0; i < cantidad; i++)
                numeros[i] = GenerarNumeroAleatorio();

            return numeros;
        }

        /// <summary>
        /// Método que cuenta el periódo de una secuencia de números.
        /// Se dice que el tamaño del período es menor/igual al Módulo introduccido.
        /// En caso de ser del tamaño del módulo se dice que tiene un período completo.
        /// </summary>
        /// <returns>El número de periódo de la secuencia de números En caso de no encontrarlo retorna -1.</returns>
        public int GetPeriodo()
        {
            //Se "rebobina" la semilla para hacer el conteo
            this.semilla = semillaFinal;

            double inicio = GenerarNumeroAleatorio();

            for (int i = 1; i < (mod + 1); i++)
            {
                if (inicio == GenerarNumeroAleatorio())
                    return i;
            }

            return -1;
        }
    }
}
