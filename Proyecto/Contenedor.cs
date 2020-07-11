using ConversionCodigo;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Proyecto
{
    class Contenedor : System.Windows.Forms.Panel
    {
        private Panel panel;

        private Progreso progreso;
        private PanelGenerador panelGenerador;

        private Button btnAbortar, btnAyuda;

        public Contenedor(Panel panel)
        {
            this.panel = panel;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            //Propiedades del panel
            this.Size = new Size(785, 565);
            this.Location = new Point(0, 0);
            this.BackColor = Color.FromArgb(40, 40, 40);

            //Instancia del panel Progreso
            this.progreso = new Progreso();
            this.Controls.Add(this.progreso);

            //Instancia del panel del generador
            this.panelGenerador = new PanelGenerador(progreso, this);
            this.Controls.Add(panelGenerador);

            //Instnacia de los botones
            this.btnAbortar = new Button
            {
                Size = new Size(60, 60),
                Location = new Point(710, 20),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\abort.png")
            };
            btnAbortar.FlatAppearance.BorderSize = 0;
            this.btnAbortar.Click += Clic_Abortar;
            this.btnAbortar.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.btnAbortar.FlatAppearance.BorderSize = 0;
            this.btnAbortar.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.btnAbortar.FlatAppearance.BorderColor = Color.FromArgb(40, 40, 40);

            /*
            this.btnAyuda = new Button
            {
                Size = new Size(60, 60),
                Location = new Point(15, 20),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\help.png")
            };
            this.btnAyuda.Click += BtnAyuda;
            this.btnAyuda.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.btnAyuda.FlatAppearance.BorderSize = 0;
            this.btnAyuda.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.btnAyuda.FlatAppearance.BorderColor = Color.FromArgb(40, 40, 40);*/


            this.Controls.Add(this.btnAbortar);
           // this.Controls.Add(this.btnAyuda);
        }


        /// <summary>
        /// Evento al pulsar el botón de abortar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clic_Abortar(object sender, EventArgs e)
        {
           DialogResult dialog = MessageBox.Show("¿Está seguro de abortar la simulación?", "Aviso", MessageBoxButtons.YesNo);

            //En caso de que "No", se cancela la operación
            if (dialog.ToString().Equals("No"))
                return;

            //Destruimos todo
            this.panel.Controls.Clear();
            this.Dispose();

            GC.Collect();

            PanelInicio panelInicio = new PanelInicio(panel);
            this.panel.Controls.Add(panelInicio);
        }

        private void BtnAyuda(object sender, EventArgs e)
        {
            //this.panel.Controls.Clear();
            PanelAyuda panel = new PanelAyuda();

            this.panel.Controls.Add(panel);
            this.panel.Controls.SetChildIndex(panel, 0);
        }
    }
}
