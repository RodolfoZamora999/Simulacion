using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto
{
    class PanelSimulacion : Panel
    {
        private Contenedor contenedor;
        private Progreso progreso;

        private readonly double[] numeros;

        private Label lblTitle, lblDescripcion, lblAgnos, lblMeses, lblDistribucion;
        private Label lblTotalAgno, lblTotalMes, lblTotalProbabilidad;

        private ListView listViewAgnos, listViewMeses, listViewDistribucion;

        private Button btnSimular;

        private double[] probabilidad;

        public PanelSimulacion(double[] numeros, Contenedor contenedor, Progreso progreso)
        {
            this.numeros = numeros;
            this.contenedor = contenedor;
            this.progreso = progreso;

            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            //Propiedades del panel
            this.Size = new Size(785, 465);
            this.Location = new Point(0, 100);
            this.BackColor = Color.White;

            //Componentes secundarios
            this.lblTitle = new Label
            {
                Size = new Size(300, 20),
                Location = new Point(15, 10),
                Text = "Simulación de Homicidios dolosos en México",
                Font = new Font("Calibri", 14, FontStyle.Bold)
            };

            this.lblDescripcion = new Label
            {
                Size = new Size(785, 40),
                Location = new Point(15, 40),
                Text = "En esta simulación lo que se plantea hacer es simular los posibles homicidios en el siguiente año distribuidos\r\n" +
                "en meses, todo esto en base a datos recopilados de 11 años de la página oficial del INEGI.",
                Font = new Font("Calibri", 12, FontStyle.Regular)
            };

            this.listViewAgnos = new ListView 
            {
                Size = new Size(210, 290),
                Location = new Point(10, 120),
                View = View.Details, 
                Font = new Font("Calibri", 10)
            };
            this.listViewAgnos.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader {Text = "Año", Width = 100 },
                new ColumnHeader {Text =  "Homicidios", Width = 100} });

            this.lblAgnos = new Label 
            {
                Size = new Size(210, 30),
                Location = new Point(10, 90),
                Text = "Tasa de homicidios por año",
                Font = new Font("Calibri", 12f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(0, 120, 208), 
                ForeColor = Color.White
            };
            this.lblTotalAgno = new Label()
            {
                Size = new Size(130, 30),
                Location = new Point(90, 410),
                Text = "Total: 500000",
                Font = new Font("Calibri", 12f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(0, 120, 208),
                ForeColor = Color.White
            };

            this.listViewMeses = new ListView
            {
                Size = new Size(210, 290),
                Location = new Point(230, 120),
                View = View.Details,
                Font = new Font("Calibri", 10)
            };
            this.listViewMeses.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader {Text = "Mes", Width = 90 },
                new ColumnHeader {Text =  "Homicidios", Width = 90} });

            this.lblMeses = new Label
            {
                Size = new Size(210, 30),
                Location = new Point(230, 90),
                Text = "Tasa de homicidios por mes",
                Font = new Font("Calibri", 12f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(0, 120, 208),
                ForeColor = Color.White
            };

            this.lblTotalMes = new Label()
            {
                Size = new Size(130, 30),
                Location = new Point(310, 410),
                Text = "Total: 500000",
                Font = new Font("Calibri", 12f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(0, 120, 208),
                ForeColor = Color.White
            };

            this.listViewDistribucion = new ListView
            {
                Size = new Size(210, 290),
                Location = new Point(450, 120),
                View = View.Details,
                Font = new Font("Calibri", 10)
            };
            this.listViewDistribucion.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader {Text = "Mes", Width = 90 },
                new ColumnHeader {Text =  "Probabilidad", Width = 100} });

            this.lblDistribucion = new Label
            {
                Size = new Size(210, 30),
                Location = new Point(450, 90),
                Text = "Probabilidad por mes",
                Font = new Font("Calibri", 12f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(0, 120, 208),
                ForeColor = Color.White
            };

            this.lblTotalProbabilidad = new Label()
            {
                Size = new Size(130, 30),
                Location = new Point(530, 410),
                Text = "Total: 1",
                Font = new Font("Calibri", 12f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.FromArgb(0, 120, 208),
                ForeColor = Color.White
            };

            //Cargamos datos
            CargarDatos();

            this.btnSimular = new Button 
            {
                Size = new Size(110, 40),
                Location = new Point(670, 420),
                Text = "Simular",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Calibri", 14f),
                BackColor = Color.Red,
                ForeColor = Color.White
            };
            this.btnSimular.Click += BtnEventSimular;


            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.listViewAgnos);
            this.Controls.Add(this.lblAgnos);
            this.Controls.Add(this.lblTotalAgno);
            this.Controls.Add(this.listViewMeses);
            this.Controls.Add(this.lblMeses);
            this.Controls.Add(this.lblTotalMes);
            this.Controls.Add(this.listViewDistribucion);
            this.Controls.Add(this.lblDistribucion);
            this.Controls.Add(this.lblTotalProbabilidad);
            this.Controls.Add(this.btnSimular);
        }


        private void CargarDatos()
        {
            //Creación de la lista
            List<Datos> list = new List<Datos>();

            //Cargamos la lista
            list.Add(new Datos("2008", 14006));
            list.Add(new Datos("2009", 19803));
            list.Add(new Datos("2010", 25757));
            list.Add(new Datos("2011", 27213));
            list.Add(new Datos("2012", 25967));
            list.Add(new Datos("2013", 23063));
            list.Add(new Datos("2014", 20010));
            list.Add(new Datos("2015", 20762));
            list.Add(new Datos("2016", 24559));
            list.Add(new Datos("2017", 32079));
            list.Add(new Datos("2018", 36685));

            foreach (Datos dato in list)
            {
                ListViewItem item = new ListViewItem(new String[] { dato.Agno.ToString(), dato.Homicidios.ToString()});
                listViewAgnos.Items.Add(item);
            }

            int sumaTotal = 0;
            int count = listViewAgnos.Items.Count;

            for (int i = 0; i < count; i++)
                sumaTotal += Int32.Parse(listViewAgnos.Items[i].SubItems[1].Text);

            this.lblTotalAgno.Text = "Total: " + sumaTotal;


            //Cargamos de nuevo
            list.Clear();
            list.Add(new Datos("Enero", 20233));
            list.Add(new Datos("Febrero", 19343));
            list.Add(new Datos("Marzo", 21576));
            list.Add(new Datos("Abril", 21543));
            list.Add(new Datos("Mayo", 23697));
            list.Add(new Datos("Junio", 23008));
            list.Add(new Datos("Julio", 22991));
            list.Add(new Datos("Agosto", 23778));
            list.Add(new Datos("Septiembre", 23244));
            list.Add(new Datos("Octubre", 23894));
            list.Add(new Datos("Noviembre", 22464));
            list.Add(new Datos("Diciembre", 24133));

            foreach (Datos dato in list)
            {
                ListViewItem item = new ListViewItem(new String[] { dato.Agno.ToString(), dato.Homicidios.ToString() });
                listViewMeses.Items.Add(item);
            }

            //Hacemos un poco de magina
            count = listViewMeses.Items.Count;
            sumaTotal = 0;

           for(int i = 0; i < count; i++)
                sumaTotal += Int32.Parse(listViewMeses.Items[i].SubItems[1].Text);

            this.lblTotalMes.Text = "Total: " + sumaTotal;


           //Calculamos la probabilidad
            probabilidad = new double[count];
            for (int i = 0; i < count; i++)
                probabilidad[i] = Double.Parse(listViewMeses.Items[i].SubItems[1].Text) / sumaTotal;

            for(int i = 0; i < probabilidad.Length; i++)
            {
                ListViewItem item = new ListViewItem(new String[] { list[i].Agno.ToString(),
                    Math.Round(probabilidad[i], 5).ToString()});

                listViewDistribucion.Items.Add(item);
            }

            double suma = 0;
            foreach (var value in probabilidad)
                suma += value;

            this.lblTotalProbabilidad.Text = "Total: " + suma;
        }

        private void BtnEventSimular(object sender, EventArgs e)
        {
            //Limpiamos la interfaz
            this.contenedor.Controls.Remove(this);
            this.Dispose();

            //Instancia del panel
            PanelSimulacion2 panel = new PanelSimulacion2(probabilidad, numeros, contenedor, progreso);
            contenedor.Controls.Add(panel);
            contenedor.Refresh();
        }
    }

    /// <summary>
    /// Clase de apoyo para encapsular valores.
    /// </summary>
    class Datos
    {
        private int homicidios;
        private string agno;

        public Datos(string agno, int homicidios)
        {
            this.Agno = agno;
            this.Homicidios = homicidios;
        }

        public int Homicidios { get => homicidios; set => homicidios = value; }
        public string Agno { get => agno; set => agno = value; }
    }
}
