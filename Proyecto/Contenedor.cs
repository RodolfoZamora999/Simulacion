﻿using ConversionCodigo;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Proyecto
{
    class Contenedor : System.Windows.Forms.Panel
    {
        private Progreso progreso;
        private PanelGenerador panelGenerador;

        private Button btnAbortar;

        public Contenedor()
        {
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
            this.Controls.Add(this.btnAbortar);
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
            this.Controls.Clear();

            //Tenemos que invocarel panel principal, esto quedara pendiente...
        }
    }
}
