using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Proyecto
{
    class PanelInicio : Panel
    {
        private Panel panel;
        private Button btnInicio;

        public PanelInicio()
        {
            this.InitializeComponent();
        }

        public PanelInicio(Panel panel)
        {
            this.panel = panel;
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            //Propiedades del panel
            this.Size = new Size(785, 565);
            this.Location = new Point(0, 0);
            //this.BackColor = Color.FromArgb(40, 40, 40);
            this.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\fondo.jpg");

            this.btnInicio = new Button
            {
                Size = new Size(250, 100),
                Location = new Point((785 - 250) / 2, 200),
                Text = "Iniciar",
                Font = new Font("Arial", 24f, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White
            };
            this.btnInicio.FlatAppearance.MouseDownBackColor = Color.FromArgb(30, 255, 0, 0);
            this.btnInicio.FlatAppearance.MouseOverBackColor = Color.FromArgb(30, 0, 0, 255);
            this.btnInicio.Click += BtnEventInicio;
           
            this.Controls.Add(this.btnInicio);
        }

        private void BtnEventInicio(object sender, EventArgs e)
        {
            this.panel.Controls.Clear();
            this.Dispose();
            this.panel.Controls.Add(new Contenedor(panel));
        }
    }
}
