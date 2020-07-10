using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto
{
    class PanelSimulacion2 : Panel
    {
        private Progreso progreso;
        private Contenedor contenedor;
        private double[] probabilidades;
        private double[] numeros;
        private Label lblTitle, lblDescripcion, lblMeses, lblTotalMes, lblConclusion;
        private Grafica grafica;
        private ListView listViewMeses;

        public PanelSimulacion2(double[] probabilidades, double[] numeros, Contenedor contenedor, Progreso progreso)
        {
            this.probabilidades = probabilidades;
            this.numeros = numeros;
            this.contenedor = contenedor;
            this.progreso = progreso;

            this.InicializeComponets();
        }

        private void InicializeComponets()
        {
            //Propiedades del panel
            this.Size = new Size(785, 465);
            this.Location = new Point(0, 100);
            this.BackColor = Color.White;

            //Componentes secundarios
            this.lblTitle = new Label
            {
                Size = new Size(500, 20),
                Location = new Point(15, 10),
                Text = "Simulación con " + numeros.Length + " números pseudoaleatorios finalizada.",
                Font = new Font("Calibri", 14, FontStyle.Bold)
            };

            this.lblDescripcion = new Label
            {
                Size = new Size(785, 35),
                Location = new Point(15, 30),
                Text = "A continuación se muestran los resultados de la simulación con los números pseudoaleatorios proporcionados.",
                Font = new Font("Calibri", 12, FontStyle.Regular)
            };

            this.listViewMeses = new ListView
            {
                Size = new Size(210, 270),
                Location = new Point(530, 90),
                View = View.Details,
                Font = new Font("Calibri", 10)
            };
            this.listViewMeses.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader {Text = "Mes", Width = 90 },
                new ColumnHeader {Text =  "Homicidios", Width = 90} });

            this.lblMeses = new Label
            {
                Size = new Size(210, 30),
                Location = new Point(530, 60),
                Text = "Tasa de homicidios simulados",
                Font = new Font("Calibri", 12f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(0, 120, 208),
                ForeColor = Color.White
            };

            this.lblTotalMes = new Label()
            {
                Size = new Size(130, 30),
                Location = new Point(610, 360),
                Text = "Total: 1500",
                Font = new Font("Calibri", 12f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(0, 120, 208),
                ForeColor = Color.White
            };

            this.lblConclusion = new Label
            {
                Size = new Size(510, 130),
                Location = new Point(10, 345),
                Text = "",
                Font = new Font("Calibri", 11f, FontStyle.Regular)
            };

           CargarDatos();

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.listViewMeses);
            this.Controls.Add(this.lblMeses);
            this.Controls.Add(this.lblTotalMes);
            this.Controls.Add(this.lblConclusion);
        }

        /// <summary>
        /// Método que se encarga de hacer toda la mágina de la simulación. En teoría este método es dinamico, lo
        /// cual significa que se puede adaptar a cualquier escenario
        /// </summary>
        /// <returns></returns>
        private int[] Simular()
        {
            if (numeros == null || probabilidades == null)
                return  null;

            int[] suma = new int[probabilidades.Length];


            int num;
            double numero;
            double limInf;
            double limSup;

            //En teoría, tienen que ser 12 probabilidades distintas (Porque son 12 meses)
            for(int i = 0; i < numeros.Length; i++)
            {
                numero = numeros[i];

                // 0 - 0.07
                num = 0;
                limInf = 0;
                limSup = probabilidades[num];
                if (numero >= limInf && numero < limSup)
                    suma[num] += 1;

                //0.07 - 0.14
                num++;
                limInf = limSup;
                limSup = limSup + probabilidades[num];     
                if (numero >= limInf && numero < limSup)
                    suma[num] += 1;

                //0.14 - 0.22
                num++;
                limInf = limSup;
                limSup = limSup + probabilidades[num];
                if (numero >= limInf && numero < limSup)
                    suma[num] += 1;

                //0.22 - 0.30
                num++;
                limInf = limSup;
                limSup = limSup + probabilidades[num];
                if (numero >= limInf && numero < limSup)
                    suma[num] += 1;

                //0.30 - 0.39
                num++;
                limInf = limSup;
                limSup = limSup + probabilidades[num];
                if (numero >= limInf && numero < limSup)
                    suma[num] += 1;

                //0.39 - 0.48
                num++;
                limInf = limSup;
                limSup = limSup + probabilidades[num];
                if (numero >= limInf && numero < limSup)
                    suma[num] += 1;

                //0.48 - 0.57
                num++;
                limInf = limSup;
                limSup = limSup + probabilidades[num];
                if (numero >= limInf && numero < limSup)
                    suma[num] += 1;

                //0.57 - 0.66
                num++;
                limInf = limSup;
                limSup = limSup + probabilidades[num];
                if (numero >= limInf && numero < limSup)
                    suma[num] += 1;

                //0.66 - 0.75
                num++;
                limInf = limSup;
                limSup = limSup + probabilidades[num];
                if (numero >= limInf && numero < limSup)
                    suma[num] += 1;

                //0.75 - 0.84
                num++;
                limInf = limSup;
                limSup = limSup + probabilidades[num];
                if (numero >= limInf && numero < limSup)
                    suma[num] += 1;

                //0.84 - 0.92
                num++;
                limInf = limSup;
                limSup = limSup + probabilidades[num];
                if (numero >= limInf && numero < limSup)
                    suma[num] += 1;

                //0.92 - 0.1
                num++;
                limInf = limSup;
                limSup = limSup + probabilidades[num];
                if (numero >= limInf && numero < limSup)
                    suma[num] += 1;

            }

            return suma;
        }

        private string[] MesMayor(int[] suma)
        {
            int max = suma.Max();
            int max2 = SegundoMayor(max, suma);

            int mes = Array.IndexOf(suma, max);
            int mes2 = Array.IndexOf(suma, max2);

            string[] nomMes = new string[] {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio",
                "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };


            int sumTotal = 0;
            foreach (int value in suma)
                sumTotal += value;

            double porcentaje = Math.Round((double)max / sumTotal, 4) * 100;
            double porcentaje2 = Math.Round((double)max2 / sumTotal, 4) * 100;


            return new string[] { nomMes[mes], porcentaje+"%", nomMes[mes2], porcentaje2+"%"};
        }

        /// <summary>
        ///Método de apoyo que busca el segundo número más grande 
        /// </summary>
        /// <param name="max"></param>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private int SegundoMayor(int max, int[] numbers)
        {
            int mayor = 0;
            foreach(int value in numbers)
            {
                if (value == max)
                    continue;

                if (mayor < value)
                    mayor = value;  
            }

            return mayor;
        }

        private void CargarDatos()
        {
            //Se obtiene la lista de simulacion de números
            int[] simulacion = Simular();

            //Creación de una lista 
            List<Datos> list = new List<Datos>();
            list.Add(new Datos("Enero", simulacion[0]));
            list.Add(new Datos("Febrero", simulacion[1]));
            list.Add(new Datos("Marzo", simulacion[2]));
            list.Add(new Datos("Abril", simulacion[3]));
            list.Add(new Datos("Mayo", simulacion[4]));
            list.Add(new Datos("Junio", simulacion[5]));
            list.Add(new Datos("Julio", simulacion[6]));
            list.Add(new Datos("Agosto", simulacion[7]));
            list.Add(new Datos("Septiembre", simulacion[8]));
            list.Add(new Datos("Octubre", simulacion[9]));
            list.Add(new Datos("Noviembre", simulacion[10]));
            list.Add(new Datos("Diciembre", simulacion[11]));

            //Cargamos los datos obtenidos al lisView
            foreach (Datos dato in list)
            {
                ListViewItem item = new ListViewItem(new String[] { dato.Agno.ToString(), dato.Homicidios.ToString() });
                listViewMeses.Items.Add(item);
            }

            int sum = 0;
            foreach(int value in simulacion)
                sum += value;

            this.lblTotalMes.Text = "Total: " + sum;


            this.grafica = new Grafica(simulacion);
            this.Controls.Add(grafica);

            //
            string[] mes = MesMayor(simulacion);

            this.lblConclusion.Text = "En base a los datos obtenidos, el mes más violento registrado ha sido " + mes[0] + ", " +
                "con un total del "+mes[1]+" de los homicidios totales en la simulación, seguido del mes de "+mes[2]+" " +
                "con una tasa de "+mes[3]+". Esto nos podría dar una pista de la razón por la que en estos periodos" +
                " del año la violenta aumenta en comparación de otros meses. Por el momento se recomienda poner " +
                "más atención a dichos meses y tomar mejores estrategias para combatir la violencia.";

        }

    }
    
    /// <summary>
    /// 
    /// </summary>
    class Grafica : Panel
    {
        private int[] simulacion;
        private double tamano;

        private Pen pen;
        private SolidBrush brush;

        private string[] meses;

        public Grafica(int[] simulacion)
        {
            this.simulacion = simulacion;

            this.InicializeComponets();
        }

        private void InicializeComponets()
        {
            Size = new Size(515, 280);
            Location = new Point(5, 60);
          //  BorderStyle = BorderStyle.FixedSingle;

            this.pen = new Pen(Color.Black);
            this.pen.Width = 4;

            this.brush = new SolidBrush(Color.Red);

            tamano = 0;
            foreach (int value in simulacion)
                tamano += value;

            tamano /= 5;

            this.meses = new string[]
            { 
                "Ene.", "Feb.", "Mar.", "Abri.", "May.", "Jun.",
                "Jul.", "Ago.", "Sep.", "Oct.", "Nov.", "Dic."};
        }

        /// <summary>
        /// Sobre-Escribimos el método para hacer de las nuestras
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            // get the graphics object to use to draw
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //Variables de apoyo
            int ancho = Width;
            int alto = Height - 25;
            int x = 35;
            int y = 10;

            //Dibujo de las lineas
            g.DrawLine(pen, x, y, x, alto);
            g.DrawLine(pen, x, alto, ancho, alto);

            //Variables temporales
            float  value; //Numeros contados del arreglo[i]

            //Dibujamos las graficas
           
            for(int i = 0; i < simulacion.Length; i++)
            {
                brush.Color = Color.Red;
                value = (float)simulacion[i];
                g.FillRectangle(brush, 40 * (i + 1),  (alto - ((value / (float)tamano) * (alto - y) ) ) , 
                    30, (value / (float)tamano ) * (alto - y - 4));

                brush.Color = Color.Black;
                g.DrawString(meses[i], new Font("Calibri", 13f, FontStyle.Bold), brush, new Point(40 * (i + 1),   alto - y + 13  ));
            }

            //Dibujo de los valores
            brush.Color = Color.Black;
            g.DrawString("0", new Font("Calibri", 14f, FontStyle.Bold), brush, new Point(0, alto - y - 10));

            g.DrawString((tamano / 2).ToString(), new Font("Calibri", 14f, FontStyle.Bold), brush, new Point(0, (alto - y - 16) / 2));

            g.DrawString(tamano.ToString(), new Font("Calibri", 14f, FontStyle.Bold), brush, new Point(0, y - 6));

            //Dibujo de lineas de comparación
            pen.Color = Color.FromArgb(43, 143, 243);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            pen.Width = 2;
            g.DrawLine(pen, x, y, Width, y);
            g.DrawLine(pen, new Point(x, (alto - y + 19) / 2), new Point(Width, (alto - y + 19) / 2));
        }

    }
}
