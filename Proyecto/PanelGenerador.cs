using ConversionCodigo;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Proyecto
{
    class PanelGenerador : Panel
    {
        private Contenedor contenedor;

        private double[] numeros;

        private Label lblGenerador, lblMulti, lblAditivo, lblSemilla, lblModulo, lblCantidad, lblPerido, lblCGenerada;
        private TextBox txtMulti, txtAditivo, txtSemilla, txtModulo, txtCantidad;

        private Progreso progreso;
        private Button btnSiguiente, btnGenerar, btnSave, btnClean;
        private ListView listView;

        private Panel panelGenerar, panelCargar;

        private CheckBox checkBox;

        public PanelGenerador(Progreso progreso, Contenedor contenedor)
        {
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

            //Valor del progreso
            this.progreso.ProgresoValue = 0;
            this.progreso.UpdateTextColor();

            //Creación del listview
            this.listView = new ListView
            {
                Size = new Size(350, 400),
                Location = new Point(10, 10),
                View = View.Details
            };
            listView.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader {Text = "Index", Width = 140 },
                new ColumnHeader {Text =  "Número", Width = 140} });
            this.Controls.Add(this.listView);

            //Creación de Labels
            this.lblCGenerada = new Label
            {
                Text = "Números generados: 0",
                Size = new Size(230, 30),
                Location = new Point(10, 425),
                Font = new Font("Calibri", 14f)
            };
            this.Controls.Add(this.lblCGenerada);

            this.lblPerido = new Label
            {
                Text = "Período: 0",
                Size = new Size(200, 30),
                Location = new Point(250, 425),
                Font = new Font("Calibri", 14f)
            };
            this.Controls.Add(this.lblPerido);

            //Creación de la sección del generador
            this.panelGenerar = new Panel
            {
                Size = new Size(410, 200),
                Location = new Point(365, 10),
                BorderStyle = BorderStyle.FixedSingle
            };

            //Creación del checkBox
            this.checkBox = new CheckBox
            {
                Size = new Size(150, 25),
                Location = new Point(260, 5),
                Text = "Automático",
                BackColor = Color.Transparent,
                Font = new Font("Calibri", 15f)
            };
            this.checkBox.CheckedChanged += CheckUpdate;
            this.panelGenerar.Controls.Add(this.checkBox);

            this.lblGenerador = new Label
            {
                Text = "Generar números",
                Size = new Size(200, 30),
                Location = new Point(10, 2),
                Font = new Font("Calibri", 18f)
            };

            this.txtMulti = new TextBox
            {
                Size = new Size(60, 60),
                Location = new Point(10, 50),
                Font = new Font("Calibri", 14f),
                TextAlign = HorizontalAlignment.Center, 
                Text = "0"
            };
            this.txtMulti.TextChanged += ComprobarEntrada;
            this.txtMulti.Enter += EnterTextBox;
            this.txtMulti.LostFocus += FocusExit;

            this.lblMulti = new Label 
            {
                Text = "A",
                Size = new Size(60, 30),
                Location = new Point(25, 90),
                Font = new Font("Calibri", 22f, FontStyle.Bold)
            };


            this.txtAditivo = new TextBox
            {
                Size = new Size(60, 60),
                Location = new Point(90, 50),
                Font = new Font("Calibri", 14),
                TextAlign = HorizontalAlignment.Center,
                Text = "0"
            };
            this.txtAditivo.TextChanged += ComprobarEntrada;
            this.txtAditivo.Enter += EnterTextBox;
            this.txtAditivo.LostFocus += FocusExit;

            this.lblAditivo = new Label
            {
                Text = "C",
                Size = new Size(60, 30),
                Location = new Point(105, 90),
                Font = new Font("Calibri", 22f, FontStyle.Bold)
            };

            this.txtSemilla = new TextBox
            {
                Size = new Size(60, 60),
                Location = new Point(170, 50),
                Font = new Font("Calibri", 14f),
                TextAlign = HorizontalAlignment.Center,
                Text = "0"
            };
            this.txtSemilla.TextChanged += ComprobarEntrada;
            this.txtSemilla.Enter += EnterTextBox;
            this.txtSemilla.LostFocus += FocusExit;

            this.lblSemilla = new Label
            {
                Text = "X0",
                Size = new Size(60, 30),
                Location = new Point(175, 90),
                Font = new Font("Calibri", 22f, FontStyle.Bold)
            };

            this.txtModulo = new TextBox
            {
                Size = new Size(60, 60),
                Location = new Point(250, 50),
                Font = new Font("Calibri", 14f),
                TextAlign = HorizontalAlignment.Center,
                Text = "0"
            };
            this.txtModulo.TextChanged += ComprobarEntrada;
            this.txtModulo.Enter += EnterTextBox;
            this.txtModulo.LostFocus += FocusExit;

            this.lblModulo = new Label
            {
                Text = "M",
                Size = new Size(60, 30),
                Location = new Point(265, 90),
                Font = new Font("Calibri", 22f, FontStyle.Bold)
            };

            this.txtCantidad = new TextBox
            {
                Size = new Size(60, 60),
                Location = new Point(330, 50),
                Font = new Font("Calibri", 14f),
                TextAlign = HorizontalAlignment.Center,
                Text = "0"
            };
            this.txtCantidad.TextChanged += ComprobarEntrada;
            this.txtCantidad.Enter += EnterTextBox;
            this.txtCantidad.LostFocus += FocusExit;

            this.lblCantidad = new Label
            {
                Text = "Cantidad",
                Size = new Size(70, 30),
                Location = new Point(335, 90),
                Font = new Font("Calibri", 10f, FontStyle.Bold)
            };

            this.btnGenerar = new Button
            {
                Text = "Generar",
                Size = new Size(110, 50),
                Location = new Point(10, 140),
                Font = new Font("Calibri", 18f),
                BackColor = Color.FromArgb(41, 183, 18),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White
            };
            this.btnGenerar.Click += GenerarNumeros;

            this.btnSave = new Button
            {
                Size = new Size(30, 30),
                Location = new Point(370, 160),
                Font = new Font("Calibri", 18f),
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = false,
                Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\save2.png"),
                FlatStyle = FlatStyle.Flat    
            };
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Click += GuardarNumeros;

            this.panelGenerar.Controls.Add(this.btnSave);
            this.panelGenerar.Controls.Add(this.lblGenerador);
            this.panelGenerar.Controls.Add(this.txtMulti);
            this.panelGenerar.Controls.Add(this.lblMulti);
            this.panelGenerar.Controls.Add(this.txtAditivo);
            this.panelGenerar.Controls.Add(this.lblAditivo);
            this.panelGenerar.Controls.Add(this.txtSemilla);
            this.panelGenerar.Controls.Add(this.lblSemilla);
            this.panelGenerar.Controls.Add(this.txtModulo);
            this.panelGenerar.Controls.Add(this.lblModulo);
            this.panelGenerar.Controls.Add(this.txtCantidad);
            this.panelGenerar.Controls.Add(this.lblCantidad);
            this.panelGenerar.Controls.Add(this.btnGenerar);
            this.Controls.Add(panelGenerar);


            //Panel de guardado
            this.panelCargar = new Panel
            {
                Size = new Size(410, 190),
                Location = new Point(365, 220),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.CargarAchivos(panelCargar);
            this.Controls.Add(this.panelCargar);

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
            this.btnSiguiente.Click += BtnSiguiente_Click;
            this.Controls.Add(this.btnSiguiente);
        }

        private void CheckUpdate(object sender, EventArgs e)
        {
            if(((CheckBox)sender).Checked)
            {
                this.txtMulti.Enabled = false;
                this.txtMulti.Text = "0";
                this.txtAditivo.Enabled = false;
                this.txtAditivo.Text = "0";
                this.txtSemilla.Enabled = false;
                this.txtSemilla.Text = "0";
                this.txtModulo.Enabled = false;
                this.txtModulo.Text = "0";
            }
            else
            {
                this.txtMulti.Enabled = true;
                this.txtAditivo.Enabled = true;
                this.txtSemilla.Enabled = true;
                this.txtModulo.Enabled = true;
            }
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
            char[] valores = {'0','1','2','3','4','5','6','7','8','9'};
            string value = ((TextBox)sender).Text;

            char[] palabra = value.ToArray();

            bool status;

            for(int i = 0; i < palabra.Length; i++)
            {
                status = false;
                char letra = palabra[i];

                for(int j = 0; j < valores.Length; j++)
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

        /// <summary>
        /// Método para generar los números pseudoaleatorios
        /// </summary>
        private void GenerarNumeros(object sender, EventArgs e)
        {
            //Comprueba que no haya casillas vacias
            TextBox[] textBoxes = {txtMulti, txtAditivo, txtSemilla, txtModulo, txtCantidad};

            String value;

            for(int i = 0; i <  textBoxes.Length; i++)
            {
                value = textBoxes[i].Text;

                if (!(value.Length > 0))
                {
                    MessageBox.Show("Llene todas las casillas...", "Atención");
                    return;
                }
            }

            //En caso de modo automatico
            if(checkBox.Checked)
            {
                if(Int32.Parse(txtCantidad.Text) <= 400)
                {
                    textBoxes[0].Text = "101";
                    textBoxes[1].Text = "221";
                    textBoxes[2].Text = "17";
                    textBoxes[3].Text = "17001";
                }
                else
                {
                    textBoxes[0].Text = "37";
                    textBoxes[1].Text = "15";
                    textBoxes[2].Text = "10";
                    textBoxes[3].Text = "1048576";
                }
            }    

            //Obtención de los valores
            double a = Double.Parse(txtMulti.Text);
            double c = Double.Parse(txtAditivo.Text);
            double xo = Double.Parse(txtSemilla.Text);
            double modulo = Double.Parse(txtModulo.Text);
            int cantidad = Int32.Parse(txtCantidad.Text);

            //Instancia del generador
            Generador generador = new Generador(a, c, xo, modulo);

            //Obtención de la muestra de números
            this.numeros = generador.GenerarNumerosAleatorios(cantidad);

            //Impresión de los números
            listView.Items.Clear();

            ListViewItem listViewItem;
            for (int i = 0; i < numeros.Length; i++)
            {
                listViewItem = new ListViewItem(new String[] { (i + 1).ToString(), numeros[i].ToString()});
                this.listView.Items.Add(listViewItem);
            }

            //Impresión datos
            this.lblPerido.Text = "Período: " + generador.GetPeriodo().ToString();
            this.lblCGenerada.Text = "Números generados: " + this.numeros.Length.ToString();

            //Habilitamos el botón para guardar.
            this.btnSave.Enabled = true;

            //Habilitamos el botón para siguiente
            this.btnSiguiente.Enabled = true;

        }

        /// <summary>
        /// Método que carga archivos con números guardados previamente.
        /// </summary>
        /// <param name="panel">Panel donde se representaran</param>
        private void CargarAchivos(Panel panel)
        {
            //Limpiamos el panel
            panel.Controls.Clear();


            Label lblCargar = new Label
            {
                Text = "Cargar números guardados",
                Size = new Size(panelCargar.Width - 50, 30),
                Location = new Point(0, 0),
                Font = new Font("Calibri", 14f),
                TextAlign = ContentAlignment.MiddleCenter
            };
            panel.Controls.Add(lblCargar);

            this.btnClean = new Button 
            {
                Size = new Size(40, 40),
                Location = new Point(368, 0),
                BackColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Enabled = false, 
                FlatStyle = FlatStyle.Flat,
                Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\images\\basura.png"),
                ImageAlign = ContentAlignment.MiddleCenter
            };
            btnClean.FlatAppearance.BorderSize = 0;
            this.btnClean.Click += BtnCleanEvento;
            panel.Controls.Add(btnClean);

            string[] files;

            //Comprobación de la exitencia del directorio
            if (Directory.Exists(Directory.GetCurrentDirectory() + "\\save\\"))
                files = Directory.GetFileSystemEntries(Directory.GetCurrentDirectory() + "\\save\\");

            else
                files = new string[0];


            if (files.Length > 0)
            {
                ListView list = new ListView
                {
                    Size = new Size(panel.Width, panel.Height - 40),
                    Location = new Point(0, 40),
                    View = View.Details
                };

                list.ItemActivate += AbrirCargarArchivo;

                list.Columns.AddRange(new ColumnHeader[] {
                new ColumnHeader {Text = "Archivo", Width = 180 },
                new ColumnHeader {Text = "Hora de creación", Width = 180 }});
                this.Controls.Add(list);

                panel.Controls.Add(list);

                list.Items.Clear();

                //Cargamos los números
                for (int i = 0; i < files.Length; i++)
                {
                    ListViewItem item = new ListViewItem(new String[] { Path.GetFileName(files[i]), 
                        File.GetCreationTime(files[i]).ToString() });
                    list.Items.Add(item);
                }

                btnClean.Enabled = true;
            }

            else 
            {
                Label label = new Label
                {
                    Text = "No hay números guardados",
                    Size = new Size(panel.Width, panel.Height - 40),
                    Location = new Point(0, 30),
                    Font = new Font("Calibri", 16f, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                this.btnClean.Enabled = false;

                panel.Controls.Add(label);
            }
        }

        /// <summary>
        /// Evento cuando se hace doble clic en algún item de la colección.
        /// Cuándo se hace doble clic se carga los números almacenados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbrirCargarArchivo(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Desea cargar el archivo? ", "Aviso",
                MessageBoxButtons.YesNo);

            //En caso de marcar "No" se cancela la operación
            if (dialog.ToString().Equals("No"))
                return;

            string name = ((ListView)sender).SelectedItems[0].Text;
            
            string ruta = Directory.GetCurrentDirectory() + "\\save" + "\\" + name;
            
            //Vamos a leer los ficheros
            StreamReader streamReader = new StreamReader(ruta);

            txtMulti.Text = streamReader.ReadLine();
            txtAditivo.Text = streamReader.ReadLine();
            txtSemilla.Text = streamReader.ReadLine();
            txtModulo.Text = streamReader.ReadLine();
            txtCantidad.Text = streamReader.ReadLine();
            lblCGenerada.Text = streamReader.ReadLine();
            lblPerido.Text = streamReader.ReadLine();

            listView.Items.Clear();

            ListViewItem listViewItem;
            string value = "";
            int i = 1;
            while((value = streamReader.ReadLine()) != null)
            {
                listViewItem = new ListViewItem(new String[] { (i).ToString(), value});
                this.listView.Items.Add(listViewItem);
                i++;
            }

            //Cerramos el flujo
            streamReader.Close();

            //Cargamos los valores a la matriz
            this.numeros = null;
            this.numeros = new double[listView.Items.Count];

            for (int k = 0; k < numeros.Length; k++)
                numeros[k] = double.Parse(listView.Items[k].SubItems[1].Text);

            //Habilitamos el botón de siguiente
            this.btnSiguiente.Enabled = true;

            MessageBox.Show("Archivo cargado con exito", "Exito");
        }

        /// <summary>
        /// Método que guarda los números generados en un archivo de texto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GuardarNumeros(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Desea guardar el archivo? ", "Aviso",
               MessageBoxButtons.YesNo);

            //En caso de marcar "No" se cancela la operación
            if (dialog.ToString().Equals("No"))
                return;

            //Comprobamos que la ruta exista, de no ser así lo creamos
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\save"))
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\save");

            //Comprobamos si existen archivos
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\save");

            string name = "save.txt";

            for(int i = 0; i < files.Length; i++)
            {
                String path = Path.GetFileName(files[i]);

                if (name == path)
                    name = "save_" + i + ".txt";
            }

            //Almacenamos el archivo
            FileStream stream = File.Create(Directory.GetCurrentDirectory() + "\\save" + "\\" + name);
            stream.Close();

            string ruta = Directory.GetCurrentDirectory() + "\\save" + "\\" + name;

            //Comprobamos si realmente se creo el archivo
            if (!File.Exists(ruta))
            {
                MessageBox.Show("Error al guardar...", "Fallo con exito");
                return;
            }

            //Lo sobreescribimos.
            StreamWriter streamWriter = new StreamWriter(ruta);

            streamWriter.WriteLine(txtMulti.Text);
            streamWriter.WriteLine(txtAditivo.Text);
            streamWriter.WriteLine(txtSemilla.Text);
            streamWriter.WriteLine(txtModulo.Text);
            streamWriter.WriteLine(txtCantidad.Text);
            streamWriter.WriteLine(lblCGenerada.Text);
            streamWriter.WriteLine(lblPerido.Text);


            for (int i = 0; i < numeros.Length; i++)
                streamWriter.WriteLine(numeros[i].ToString());

            streamWriter.Close();

            MessageBox.Show("Arhivo guardado con exito \""+name+"\" ", "Exito");

            //Cargamos los archivos
            CargarAchivos(this.panelCargar);
        }

        /// <summary>
        /// Evento para el botón de borrado. Borra todos los archivos almacenados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCleanEvento(object sender, EventArgs e)
        {
            DialogResult dialog  = MessageBox.Show("¿Está seguro de eliminar todos los archivos?", "Atención",
                MessageBoxButtons.YesNo);

            if (dialog.ToString().Equals("No"))
                return;

            string[] archivos = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\save\\");

            foreach (var value in archivos)
                File.Delete(value);

            MessageBox.Show("Archivos eliminados");

            CargarAchivos(panelCargar);
        }

        /// <summary>
        /// Evento para el botón de siguiente.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            progreso.ProgresoValue += 1;
            progreso.UpdateTextColor();
            progreso.Refresh();

            contenedor.Controls.Remove(this);
            this.Dispose();

            PanelPrueba1 panel = new PanelPrueba1(numeros, progreso, contenedor);
            contenedor.Controls.Add(panel);
            contenedor.Refresh();
        }
    }
   
}
