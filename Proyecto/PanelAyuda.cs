using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto
{
    class PanelAyuda : Panel
    {
        public PanelAyuda()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            //Propiedades del panel
            this.Size = new Size(785, 565);
            this.Location = new Point(0, 0);
            this.BackColor = Color.FromArgb(40, 40, 40);
        }
    }
}
