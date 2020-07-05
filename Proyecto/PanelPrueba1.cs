using ConversionCodigo;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Proyecto
{
    class PanelPrueba1 : Panel
    {
        private readonly Progreso progreso;
        private readonly double[] matrizNumeros;
        private readonly Contenedor contenedor;
        private Label lblTitle, lblNumeros, lblSignificancia, lblStatus, lblStatusValue, lblInformacion, lblJiObtenido, lblJiTabla;
        private TextBox txtProcedimiento;
        private ComboBox comboBox;
        private Button btnIniciar, btnSiguiente;
        private Panel panelResultados;
        private PictureBox picture;


        public PanelPrueba1(double[] matrizNumeros, Progreso progreso, Contenedor contenedor)
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
                Text = "Prueba 1: Corridas arriba y abajo del promedio",
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
                Text = "Números propocionados:     " + matrizNumeros.Length,
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
                Location = new Point(600, 85),
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
                Size = new Size(390, 60),
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
            this.Controls.Add(this.txtProcedimiento);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.panelResultados);
            this.Controls.Add(btnSiguiente);
        }

        private void IniciarPrueba(object sender, EventArgs e)
        {
            //Instancia para la prueba de corridas arriba y abajo del promedio
            ArribaAbajoPromedio arribaAbajoPromedio =
                new ArribaAbajoPromedio(Int32.Parse(comboBox.SelectedIndex.ToString()), this);

            //Hacemos la prueba
            bool status = arribaAbajoPromedio.EsIndependiente(matrizNumeros);

            if (status)
            {
                lblStatusValue.ForeColor = Color.Green;
                lblStatusValue.Text = "Prueba exitosa";
                picture.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\aprobado.png");
                lblInformacion.Text = "Puedes proseguir con la siguiente prueba.";
                MessageBox.Show("Prueba exitosa");

                btnSiguiente.Enabled = true;
            }

            else
            {
                lblStatusValue.ForeColor = Color.Red;
                lblStatusValue.Text = "Prueba fallida";
                picture.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\desarprobado.png");
                lblInformacion.Text = "No puedes proseguir con la siguiente prueba." +
                    "\r\nIntenta con otra muestra de números diferente.";
                MessageBox.Show("Prueba fallida...");
            }        
        }

        public void InsertarTextBox(String texto)
        {
            this.txtProcedimiento.Text = texto;
        }

        public void InsertarLabels(string jiObtenido, string jiTabla)
        {
            this.lblJiObtenido.Text = "JiCuadrado obtenido: " + jiObtenido;
            this.lblJiTabla.Text = "JiCuadrado tabla:       " + jiTabla;
        }

        private void BtnSiguiente(Object sender, EventArgs e)
        {
            progreso.ProgresoValue += 1;
            progreso.UpdateTextColor();
            progreso.Refresh();

            contenedor.Controls.Remove(this);

            PanelPrueba2 panel = new PanelPrueba2(matrizNumeros, progreso, contenedor);
            contenedor.Controls.Add(panel);
            contenedor.Refresh();
        }

    }


}
