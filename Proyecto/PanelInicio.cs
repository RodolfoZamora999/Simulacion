using System;
using System.Drawing;
using System.Windows.Forms;

namespace Proyecto
{
    class PanelInicio : Panel
    {
        private Panel panel;

        private Label lblTitle;
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
            this.BackColor = Color.FromArgb(40, 40, 40);

            this.lblTitle = new Label 
            {
                Size = new Size(500, 70),
                Location = new Point(142, 43),
                Text = "Simulador 2020", 
                Font = new Font("Arial", 48), 
                ForeColor = Color.White
            };

            this.btnInicio = new Button
            {
                Size = new Size(200, 80),
                Location = new Point(304, 467),
                Text = "Iniciar",
                Font = new Font("Arial", 18f, FontStyle.Bold),
                FlatStyle = FlatStyle.System
            };
            this.btnInicio.Click += BtnEventInicio;

            this.Controls.Add(this.lblTitle);
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
