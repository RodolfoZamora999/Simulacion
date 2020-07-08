using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto
{
    class PanelSimulacion2 : Panel
    {
        private Progreso progreso;
        private Contenedor contenedor;
        private double[] probabilidades;
        private double[] numeros;

        private Label lblTitle, lblDescripcion;

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
                Size = new Size(400, 20),
                Location = new Point(15, 10),
                Text = "Simulación con " + numeros.Length + " números pseudoaleatorios.",
                Font = new Font("Calibri", 14, FontStyle.Bold)
            };

            this.lblDescripcion = new Label
            {
                Size = new Size(785, 40),
                Location = new Point(15, 40),
                Text = "A continuación se muestran los resultados de la simulación con los números pseudoaleatorios proporcionados.",
                Font = new Font("Calibri", 12, FontStyle.Regular)
            };

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblDescripcion);
        }
    }
}
