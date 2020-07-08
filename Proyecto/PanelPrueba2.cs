using ConversionCodigo;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto
{
    class PanelPrueba2 : Panel
    {
        private readonly Progreso progreso;
        private readonly double[] matrizNumeros;
        private readonly Contenedor contenedor;
        private Label lblTitle, lblNumeros, lblSignificancia, lblStatus, lblStatusValue, 
            lblInformacion, lblJiObtenido, lblJiTabla, lblIntervalos;
        private TextBox txtProcedimiento, txtIntervalos;
        private ComboBox comboBox;
        private Button btnIniciar, btnSiguiente;
        private Panel panelResultados;
        private PictureBox picture;
        private CheckBox checkBox;


        public PanelPrueba2(double[] matrizNumeros, Progreso progreso, Contenedor contenedor)
        {
            this.matrizNumeros = matrizNumeros;
            this.progreso = progreso;
            this.contenedor = contenedor;

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            //Propiedades del panel
            this.Size = new Size(785, 465);
            this.Location = new Point(0, 100);
            this.BackColor = Color.White;

            //Creación de los componentes secundarios
            this.lblTitle = new Label
            {
                Text = "Prueba 2: Prueba de ChiCuadrada",
                Size = new Size(785, 30),
                Location = new Point(0, 0),
                Font = new Font("Calibri", 18f, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter

            };
            this.Controls.Add(this.lblTitle);

            this.txtProcedimiento = new TextBox
            {
                Size = new Size(350, 360),
                Location = new Point(20, 45),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new Font("Calibri", 12f)
            };

            this.lblNumeros = new Label()
            {
                Text = "Números propocionados:          " + matrizNumeros.Length,
                Size = new Size(320, 25),
                Location = new Point(382, 50),
                Font = new Font("Calibri", 14f)
            };

            this.lblSignificancia = new Label()
            {
                Text = "Nivel de significancia: ",
                Size = new Size(210, 25),
                Location = new Point(382, 90),
                Font = new Font("Calibri", 14f)
            };

            this.comboBox = new ComboBox
            {
                Size = new Size(60, 40),
                Location = new Point(620, 85),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Calibri", 14f)

            };
            this.comboBox.Items.Add("0.01");
            this.comboBox.Items.Add("0.025");
            this.comboBox.Items.Add("0.05");
            this.comboBox.Items.Add("0.10");
            this.comboBox.Items.Add("0.15");
            this.comboBox.Items.Add("0.20");
            this.comboBox.Items.Add("0.25");
            this.comboBox.Items.Add("0.30");
            this.comboBox.Items.Add("0.35");
            this.comboBox.Items.Add("0.40");
            this.comboBox.Items.Add("0.45");
            this.comboBox.Items.Add("0.50");
            this.comboBox.SelectedIndex = 2;

            this.lblIntervalos = new Label()
            {
                Text = "Cant. Intervalos (Opcional): ",
                Size = new Size(230, 25),
                Location = new Point(382, 125),
                Font = new Font("Calibri", 14f)
            };

            this.txtIntervalos = new TextBox 
            {
                Size = new Size(60, 60),
                Location = new Point(620, 125),
                Font = new Font("Calibri", 14f),
                TextAlign = HorizontalAlignment.Center,
                Text = "0",
                Enabled = false
            };
            this.txtIntervalos.TextChanged += ComprobarEntrada;
            this.txtIntervalos.Enter += EnterTextBox;
            this.txtIntervalos.LostFocus += FocusExit;

            //Creación del checkBox
            this.checkBox = new CheckBox
            {
                Size = new Size(150, 25),
                Location = new Point(690, 135),
                Text = "Automático",
                BackColor = Color.Transparent,
                Font = new Font("Calibri", 10f),
                Checked = true
            };

            this.checkBox.CheckedChanged += EventCheck;


            this.btnIniciar = new Button
            {
                Text = "Iniciar prueba",
                Size = new Size(130, 40),
                Location = new Point(382, 160),
                Font = new Font("Calibri", 16f),
                BackColor = Color.FromArgb(41, 183, 18),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White
            };
            this.btnIniciar.Click += IniciarPrueba;

            this.panelResultados = new Panel
            {
                Size = new Size(390, 200),
                Location = new Point(382, 207),
                BorderStyle = BorderStyle.FixedSingle
            };

            this.lblStatus = new Label()
            {
                Text = "Estatus de la prueba: ",
                Size = new Size(175, 30),
                Location = new Point(20, 10),
                Font = new Font("Calibri", 14f, FontStyle.Bold)
            };

            this.lblStatusValue = new Label()
            {
                Text = "Prueba no iniciada",
                Size = new Size(190, 30),
                Location = new Point(195, 10),
                Font = new Font("Calibri", 14f, FontStyle.Bold),
                ForeColor = Color.Orange
            };

            this.picture = new PictureBox
            {
                Size = new Size(94, 94),
                Location = new Point(20, 50)
            };

            this.lblInformacion = new Label
            {
                Text = "",
                Size = new Size(320, 60),
                Location = new Point(20, 155),
                Font = new Font("Calibri", 12f)
            };

            this.lblJiObtenido = new Label
            {
                Text = "",
                Size = new Size(300, 30),
                Location = new Point(120, 70),
                Font = new Font("Calibri", 12f)
            };

            this.lblJiTabla = new Label
            {
                Text = "",
                Size = new Size(300, 30),
                Location = new Point(120, 100),
                Font = new Font("Calibri", 12f)
            };

            this.panelResultados.Controls.Add(this.lblStatus);
            this.panelResultados.Controls.Add(this.lblStatusValue);
            this.panelResultados.Controls.Add(this.picture);
            this.panelResultados.Controls.Add(this.lblInformacion);
            this.panelResultados.Controls.Add(this.lblJiObtenido);
            this.panelResultados.Controls.Add(this.lblJiTabla);

            this.btnSiguiente = new Button
            {
                Text = "Siguente",
                Location = new Point(650, 415),
                Size = new Size(120, 45),
                BackColor = Color.FromArgb(23, 143, 243),
                Font = new Font("Calibri", 14f),
                ForeColor = Color.White,
                Enabled = false
            };
            this.btnSiguiente.Click += BtnSiguiente;

            this.Controls.Add(lblNumeros);
            this.Controls.Add(lblSignificancia);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.lblIntervalos);
            this.Controls.Add(this.txtIntervalos);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.txtProcedimiento);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.panelResultados);
            this.Controls.Add(btnSiguiente);
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
        /// Evento que limpia una casilla para mejor comoidad de ingresar texto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterTextBox(object sender, EventArgs e)
        {
            ((TextBox)sender).Text = "";
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

        private void EventCheck(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                txtIntervalos.Text = "0";
                txtIntervalos.Enabled = false;
            }
            else
                txtIntervalos.Enabled = true;
        }

        private void IniciarPrueba(object sender, EventArgs e)
        {
            //Instancia para la prueba de corridas arriba y abajo del promedio
            JiCuadrada jiCuadrada;

            if (!checkBox.Checked)
                jiCuadrada = new JiCuadrada(comboBox.SelectedIndex, double.Parse(txtIntervalos.Text), this);
            else
                jiCuadrada = new JiCuadrada(comboBox.SelectedIndex, this);

            //Hacemos la prueba
            bool status = jiCuadrada.EsUniforme(matrizNumeros);

            if (status)
            {
                lblStatusValue.ForeColor = Color.Green;
                lblStatusValue.Text = "Prueba exitosa";
                picture.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\aprobado.png");
                lblInformacion.Text = "Puedes proseguir con la simulación.";
                MessageBox.Show("Prueba exitosa");

                btnSiguiente.Enabled = true;
            }

            else
            {
                lblStatusValue.ForeColor = Color.Red;
                lblStatusValue.Text = "Prueba fallida";
                picture.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\desarprobado.png");
                lblInformacion.Text = "No puedes proseguir con la simulación." +
                    "\r\nIntenta con otra muestra de números diferente.";

                Button button = new Button
                {
                    Size = new Size(45, 45),
                    Location = new Point(340, 150),
                    BackColor = Color.White,
                    TextAlign = ContentAlignment.MiddleCenter,
                    FlatStyle = FlatStyle.Flat,
                    Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\reload.png"),
                    ImageAlign = ContentAlignment.MiddleCenter
                };
                button.FlatAppearance.BorderSize = 0;
                button.Click += BtnReiniciar;
                panelResultados.Controls.Add(button);

                MessageBox.Show("Prueba fallida...");
            }
        }

        private void BtnSiguiente(Object sender, EventArgs e)
        {
            //Limpiamos el panel
            this.contenedor.Controls.Remove(this);
            this.Dispose();

            //Incrementamos 
            this.progreso.ProgresoValue += 1;
            this.progreso.UpdateTextColor();
            this.progreso.Refresh();

            //Creamos la instancia del siguinete panel
            this.contenedor.Controls.Add(new PanelSimulacion(matrizNumeros, contenedor, progreso));
        }

        public void InsertarTextBox(String texto)
        {
            this.txtProcedimiento.Text = texto;
        }

        private void BtnReiniciar(object sender, EventArgs e)
        {
            //Limpiamos la interfaz
            this.contenedor.Controls.Remove(this);
            this.Dispose();


            progreso.ProgresoValue = 0;
            progreso.UpdateTextColor();

            //Instancia del panel
            PanelGenerador panel = new PanelGenerador(progreso, contenedor);
            contenedor.Refresh();
            contenedor.Controls.Add(panel);
        }

        public void InsertarLabels(string jiObtenido, string jiTabla)
        {
            this.lblJiObtenido.Text = "JiCuadrado obtenido: " + jiObtenido;
            this.lblJiTabla.Text = "JiCuadrado tabla:       " + jiTabla;
        }

    }
}
