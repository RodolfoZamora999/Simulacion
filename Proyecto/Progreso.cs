using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto
{
    class Progreso : Panel
    {
        private int progreso;

        private Label lblGenerador, lblPrueba1, lblPrueba2, lblSimulacion;

        private SolidBrush pen;

        private Color azul;
        private Color naranja;

        public int ProgresoValue { get => progreso; set => progreso = value; }

        public Progreso()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            //Propiedades del panel
            Size = new Size(500, 90);
            Location = new Point( (785 - 500) / 2, 5);


            this.progreso = 0;

            this.lblGenerador = new Label
            {
                Size = new Size(120, 20),
                Location = new Point(5, 65),
                Text = "1. Generador",
                Font = new Font("Calibri", 12f),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            this.Controls.Add(this.lblGenerador);

            this.lblPrueba1 = new Label
            {
                Size = new Size(120, 20),
                Location = new Point(140, 65),
                Text = "2. Prueba 1",
                Font = new Font("Calibri", 12f),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            this.Controls.Add(this.lblPrueba1);

            this.lblPrueba2 = new Label
            {
                Size = new Size(120, 20),
                Location = new Point(275, 65),
                Text = "3. Prueba 2",
                Font = new Font("Calibri", 12f),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            this.Controls.Add(this.lblPrueba2);

            this.lblSimulacion = new Label
            {
                Size = new Size(120, 20),
                Location = new Point(410, 65),
                Text = "4. Aplicación",
                Font = new Font("Calibri", 12f),
                ForeColor = Color.White,
                BackColor = Color.Transparent
            };
            this.Controls.Add(this.lblSimulacion);

            //Creación de colores
            this.azul = Color.FromArgb(23, 143, 243);
            this.naranja = Color.FromArgb(243, 143, 23);

            //Creación del pincel
            this.pen = new SolidBrush(azul);
        }

        public void UpdateTextColor()
        {
            switch(ProgresoValue)
            {
                case 0:
                    lblGenerador.ForeColor = Color.FromArgb(33, 243, 23);
                    break;

                case 1:
                    lblGenerador.ForeColor = Color.FromArgb(33, 243, 23);
                    lblPrueba1.ForeColor = Color.FromArgb(33, 243, 23);
                    break;

                case 2:
                    lblGenerador.ForeColor = Color.FromArgb(33, 243, 23);
                    lblPrueba1.ForeColor = Color.FromArgb(33, 243, 23);
                    lblPrueba2.ForeColor = Color.FromArgb(33, 243, 23);
                    break;

                case 3:
                    lblGenerador.ForeColor = Color.FromArgb(33, 243, 23);
                    lblPrueba1.ForeColor = Color.FromArgb(33, 243, 23);
                    lblPrueba2.ForeColor = Color.FromArgb(33, 243, 23);
                    lblSimulacion.ForeColor = Color.FromArgb(33, 243, 23);
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // get the graphics object to use to draw
            Graphics g = pe.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            this.pen.Color = azul;

            int d = 20;
            for(int i = 0; i < 4; i++)
            {
                if(i < 3)
                {
                    if (progreso > 0 && i < progreso)
                        pen.Color = naranja;
                        
                    g.FillRectangle(pen, d + 40, 28, 100, 15);
                }
                    
                    
                if (i <= progreso)
                    pen.Color = naranja;

                g.FillEllipse(pen, d, 0, 60, 60);

                d += 130;
                pen.Color = azul;
            }
        }
    }
}
