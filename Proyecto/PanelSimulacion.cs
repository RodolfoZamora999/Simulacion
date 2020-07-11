using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

namespace Proyecto
{
    class PanelSimulacion : Panel
    {
        private Contenedor contenedor;
        private Progreso progreso;

        private readonly double[] numeros;

        private Label lblTitle, lblDescripcion, lblAgnos, lblMeses, lblDistribucion;
        private Label lblTotalAgno, lblTotalMes, lblTotalProbabilidad, lblEditar;

        private ListView listViewAgnos, listViewMeses, listViewDistribucion;

        private Button btnSimular, btnEditar;

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

            this.btnEditar = new Button 
            {
                Size = new Size(50, 50),
                Location = new Point(700, 90),
                Text = "",
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Calibri", 14f),
                BackColor = Color.FromArgb(40, 140, 250),
                ForeColor = Color.White,
                Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\edit.png"),
                ImageAlign = ContentAlignment.MiddleCenter
            };
            this.btnEditar.Click += BtnEventEdit;

            this.lblEditar = new Label 
            {
                Size = new Size(130, 30),
                Location = new Point(670, 140),
                Text = "Editar parámetros",
                Font = new Font("Calibri", 10f, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft
            };


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
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.lblEditar);
            this.Controls.Add(this.btnSimular);
        }


        public void CargarDatos()
        {
            //Verificación de la existencia del archivo 
            string ruta = Directory.GetCurrentDirectory() + "\\data\\data_years.txt";

            bool status = File.Exists(ruta);

            if (!status)
                return;

            //Limpiamos las listas
            listViewAgnos.Items.Clear();
            listViewMeses.Items.Clear();
            listViewDistribucion.Items.Clear();


            //Creación de la lista de hocimicidios por año
            List<Datos> list = new List<Datos>();

            //Abrimos el archivo 
            StreamReader stream = new StreamReader(ruta);

            string agno;
            string prob;
            while((agno = stream.ReadLine()) != null)
            {
                prob = stream.ReadLine();

                list.Add(new Datos(agno, Int32.Parse(prob)));
            }

            //Cierre del flujo de datos
            stream.Close();
            

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


            //Abrimos el segundo archivo
            ruta = Directory.GetCurrentDirectory() + "\\data\\data_month.txt";

            status = File.Exists(ruta);

            if (!status)
                return;

            StreamReader stream2 = new StreamReader(ruta);

            //Limpieza de la lista
            list.Clear();

            while ((agno = stream2.ReadLine()) != null)
            {
                prob = stream2.ReadLine();

                list.Add(new Datos(agno, Int32.Parse(prob)));
            }

            //Cerramos el flujo del archivo
            stream2.Close();

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

        private void BtnEventEdit(object sender, EventArgs e)
        {
            PanelEdicion panel = new PanelEdicion(this);
            btnEditar.Enabled = false;
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

        public void EnableButton()
        {
            this.btnEditar.Enabled = true;
            this.Refresh();
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

    /// <summary>
    /// Clase de apoyo para la edición de los datos registrados.
    /// </summary>
    class PanelEdicion : System.Windows.Forms.Form
    {
        private PanelSimulacion panel;

        private Label lblTitle;
        private Panel contenedor;
        private Label lblAgno;
        private Label lblMes;

        private Button btnSave;
        private Button btnCancelar;

        TextBox[] textBoxes2;
        TextBox[] textBoxes;

        Label[] labels;
        Label[] labels2;

        public PanelEdicion(PanelSimulacion panel)
        {
            this.panel = panel;
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            //Propiedades de la forma
            Size = new Size(500, 650);
            StartPosition = FormStartPosition.CenterScreen;
            Visible = true;
            MaximizeBox = false;
            MinimizeBox = false;
            MaximumSize = Size;
            MinimumSize = Size;

            //Propiedades de los componentes secundarios 

            this.contenedor = new Panel
            {
                Size = new Size(485, 650),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(40, 40, 40)
            };
            this.contenedor.AutoScroll = true;

            this.lblTitle = new Label 
            {
                Size = new Size(485, 30),
                Location = new Point(0, 10),
                Text = "Edición de los parámetros",
                Font = new Font("Calibri", 14f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White
            };

            this.lblAgno = new Label 
            {
                Size = new Size(200, 30),
                Location = new Point(10, 50),
                Text = "Edición de años",
                Font = new Font("Calibri", 12f, FontStyle.Bold),
                TextAlign = ContentAlignment.BottomLeft,
                ForeColor = Color.White
            };

            this.lblMes = new Label
            {
                Size = new Size(200, 30),
                Location = new Point(250, 50),
                Text = "Edición de meses",
                Font = new Font("Calibri", 12f, FontStyle.Bold),
                TextAlign = ContentAlignment.BottomLeft,
                ForeColor = Color.White
            };

            this.contenedor.Controls.Add(lblTitle);
            this.contenedor.Controls.Add(lblAgno);
            this.contenedor.Controls.Add(lblMes);
            this.Controls.Add(this.contenedor);

            this.FormClosed += CloseForm;

            //Cargamos todos los datos
            CargarDatos();
        }

        /// <summary>
        /// Método de apoyo para cargar todos los valores a editar
        /// </summary>
        private void CargarDatos()
        {
            string ruta = Directory.GetCurrentDirectory() + "\\data\\data_years.txt";

            //Verificamos la existencia del archivo
            if (!File.Exists(ruta))
            {
                MessageBox.Show("Archivo no encontrado: " + ruta);
                return;
            }

            //Abrimos el archivos
            StreamReader stream = new StreamReader(ruta);

            //Carmos los datos en una lista
            List<Datos> list = new List<Datos>();
            string value;
            while ((value = stream.ReadLine()) != null)
                list.Add(new Datos(value, Int32.Parse(stream.ReadLine())));



            //Temporal
            labels = new Label[list.Count];
            textBoxes = new TextBox[list.Count];


            //Creación de los Labels y texboxs necesarios
            for (int i = 0; i < list.Count; i++)
            {
                labels[i] = new Label
                {
                    Size = new Size(60, 30),
                    Location = new Point(10, 60 + ((i + 1) * 35)),
                    Text = list[i].Agno,
                    ForeColor = Color.White,
                    Font = new Font("Calibri", 12f, FontStyle.Bold),
                    BackColor = Color.FromArgb(23, 143, 243),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                textBoxes[i] = new TextBox
                {
                    Size = new Size(130, 30),
                    Location = new Point(80, 60 + ((i + 1) * 35)),
                    Text = list[i].Homicidios.ToString(),
                    Font = new Font("Calibri", 12f, FontStyle.Regular),
                    TextAlign = HorizontalAlignment.Center,
                    Name = "txt" + list[i].Agno
                };
                textBoxes[i].TextChanged += ComprobarEntrada;
                textBoxes[i].LostFocus += FocusExit;

                contenedor.Controls.Add(labels[i]);
                contenedor.Controls.Add(textBoxes[i]);
            }

            //Cerramos el flujo de datos
            stream.Close();


            //Cargamos el segundo archivo
            ruta = Directory.GetCurrentDirectory() + "\\data\\data_month.txt";

            //Comprobación
            if (!File.Exists(ruta))
            {
                MessageBox.Show("Archivo no encontrado: " + ruta);
                return;
            }

            //Cargamos el archivo
            stream = new StreamReader(ruta);

            //Reutilizamos la lista
            list.Clear();

            while ((value = stream.ReadLine()) != null)
                list.Add(new Datos(value, Int32.Parse(stream.ReadLine())));


            //Temporal
            labels2 = new Label[list.Count];
            this.textBoxes2 = new TextBox[list.Count];


            //Creación de los Labels y texboxs necesarios
            for (int i = 0; i < list.Count; i++)
            {
                labels2[i] = new Label
                {
                    Size = new Size(70, 30),
                    Location = new Point(250, 60 + ((i + 1) * 35)),
                    Text = list[i].Agno,
                    ForeColor = Color.White,
                    Font = new Font("Calibri", 12f, FontStyle.Bold),
                    BackColor = Color.FromArgb(23, 143, 243),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                textBoxes2[i] = new TextBox
                {
                    Size = new Size(130, 30),
                    Location = new Point(330, 60 + ((i + 1) * 35)),
                    Text = list[i].Homicidios.ToString(),
                    Font = new Font("Calibri", 12f, FontStyle.Regular),
                    TextAlign = HorizontalAlignment.Center,
                    Name = "txt" + list[i].Agno
                };
                textBoxes2[i].TextChanged += ComprobarEntrada;
                textBoxes2[i].LostFocus += FocusExit;

                contenedor.Controls.Add(labels2[i]);
                contenedor.Controls.Add(textBoxes2[i]);
            }

            //Cerramos el flujo de archivos
            stream.Close();

            //Ponemos los botones de guardar
            this.btnSave = new Button
            {
                Size = new Size(150, 50),
                Location = new Point(240, textBoxes2[textBoxes2.Length - 1].Location.Y + 50),
                Text = "Guardar",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(43, 187, 36),
                Font = new Font("Calibri", 18f),
                FlatStyle = FlatStyle.Flat
            };
            this.btnSave.Click += BtnGuardarEvent;

            this.btnCancelar = new Button
            {
                Size = new Size(150, 50),
                Location = new Point(70, textBoxes2[textBoxes2.Length - 1].Location.Y + 50),
                Text = "Cancelar",
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(206, 37, 37),
                Font = new Font("Calibri", 18f),
                FlatStyle = FlatStyle.Flat
            };
            this.btnCancelar.Click += BtnCancelarEvent;

            this.contenedor.Controls.Add(btnSave);
            this.contenedor.Controls.Add(btnCancelar);
        }

        private void BtnGuardarEvent(object sender, EventArgs e)
        {
            //Tenemos que comprobar que los valores concuerden
            if (textBoxes == null || textBoxes2 == null)
                return;

            int totalAgnos = 0;
            int totalMeses = 0;

            foreach (var value in textBoxes)
                totalAgnos += Int32.Parse(value.Text);

            foreach (var value in textBoxes2)
                totalMeses += Int32.Parse(value.Text);

            //Hacemos la comparación
            if (!(totalAgnos == totalMeses))
            {
                MessageBox.Show("Los valores no concuerdan, verifique las cantidades....", "Advertencia");
                return;
            }

            //Aquí ponemos todo el código necesario para sobre-escribir el archivo

            //Primer archivo, años y estadisticas
            string ruta = Directory.GetCurrentDirectory() + "\\data\\data_years.txt"; 

            if(File.Exists(ruta))
                //Eliminamos el archivo
                File.Delete(ruta);
      
            //Lo volvemos a crear
            File.Create(ruta).Close();

            StreamWriter streamWriter = new StreamWriter(ruta);

            for(int i = 0; i < textBoxes.Length; i++)
            {
                streamWriter.WriteLine(labels[i].Text);
                streamWriter.WriteLine(textBoxes[i].Text);
            }

            //Cerramos el flujo
            streamWriter.Close();


            //Segundo archivo a editar
            string ruta2 = Directory.GetCurrentDirectory() + "\\data\\data_month.txt";

            if (File.Exists(ruta2))
                //Eliminamos el archivo
                File.Delete(ruta2);

            //Lo volvemos a crear
            File.Create(ruta2).Close();

            streamWriter = new StreamWriter(ruta2);

            for (int i = 0; i < textBoxes2.Length; i++)
            {
                streamWriter.WriteLine(labels2[i].Text);
                streamWriter.WriteLine(textBoxes2[i].Text);
            }

            //Cerramos el flujo
            streamWriter.Close();

            MessageBox.Show("¡Exito! Valores guardados correctamente.", "Aviso");

            //Cargamos los datos
            this.panel.CargarDatos();
            this.panel.EnableButton();

            //Cerramos el formulario
            this.Close();   
        }

        private void BtnCancelarEvent(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Está seguro de cancelar?", "Atención",
                MessageBoxButtons.YesNo);

            if (dialog.ToString().Equals("No"))
                return;

            //Cargamos los datos
            this.panel.CargarDatos();
            this.panel.EnableButton();

            //Cerramos el formulario
            this.Close();
        }


        /// <summary>
        /// Evento que controla las casillas. Marca un "0" de dejarlas vacias. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FocusExit(object sender, EventArgs e)
        {
            string value = ((TextBox)sender).Text;

            if (!(value.Length > 0))
                ((TextBox)sender).Text = "0";
        }

        /// <summary>
        /// Método de apoyo que verifica si una cadena de caracteres cumple con la condición.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComprobarEntrada(object sender, EventArgs e)
        {
            char[] valores = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string value = ((TextBox)sender).Text;

            char[] palabra = value.ToArray();

            bool status;

            for (int i = 0; i < palabra.Length; i++)
            {
                status = false;
                char letra = palabra[i];

                for (int j = 0; j < valores.Length; j++)
                {
                    if (letra == valores[j])
                    {
                        status = true;
                        break;
                    }
                }

                if (!status)
                {
                    MessageBox.Show("Inserte solo caracteres validos....", "Aviso");
                    ((TextBox)sender).Text = "0";
                }
            }
        }

        private void CloseForm(object sendwer, EventArgs e)
        {
            //Cargamos los datos
            this.panel.CargarDatos();
            this.panel.EnableButton();
        }
    }
}
