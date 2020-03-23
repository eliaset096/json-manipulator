using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace gui
{
    public partial class Interface : Form
    {
        private string url;
        private string url_final;

        private List<Medition> meditions;
        private string result;
        private Boolean boxChecked = false;

        private DataGridView totalData;
        private int totalRows;
        private int currentPage;
        private int maxPages;
        private int rowsPerPage;

        public Interface()
        {
            InitializeComponent();
        }

        // Agre los componentes de los combobox a sus respectivos.
        public void AddDataEntitiesComboBox() {

            cbFields.Items.Add("Seleccione un campo");
            cbFields.SelectedIndex = 0;

           
            /**
            cbFields.Items.Add("fecha");
            cbFields.Items.Add("autoridad_ambiental");
            cbFields.Items.Add("nombre_de_la_estaci_n");
            cbFields.Items.Add("tecnolog_a");
            cbFields.Items.Add("latitud");
            cbFields.Items.Add("longitud");
            cbFields.Items.Add("c_digo_del_departamento");
            cbFields.Items.Add("departamento");
            cbFields.Items.Add("c_digo_del_municipio");
            cbFields.Items.Add("nombre_del_municipio");
            cbFields.Items.Add("tipo_de_estaci_n");
            cbFields.Items.Add("tiempo_de_exposici_n");
            cbFields.Items.Add("variable");
            cbFields.Items.Add("unidades");
            cbFields.Items.Add("concentraci_n");
            **/
           
            cbClauses.Items.Add("Seleccione un clausula");
            cbClauses.SelectedIndex = 0;
            cbClauses.Items.Add("select");
            cbClauses.Items.Add("where");
            cbClauses.Items.Add("order");

        }

        private async void Interface_Load(object sender, EventArgs e)
        {
            totalData = new DataGridView();
            totalRows = 0;
            currentPage = 0;
            maxPages = 0;
            rowsPerPage = 0;


            AddDataEntitiesComboBox();
            btShowFields.Enabled = false;
        }

        // Obtiene los datos de la url 
        public async Task<string> getHttps(String url_f)
        {
            WebRequest request = WebRequest.Create(url_f);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            return await sr.ReadToEndAsync();
        }
        
        // Marca todos los campos
        private void btMarkAll_Click(object sender, EventArgs e)
        {
            foreach (Control c in tlpFields.Controls)
            {
                CheckBox cb = (CheckBox)c;
                cb.Checked = true;
            }
            btShowFields.Enabled = true;
        }

        // Desmarca todos los campos
        private void btDesmarkAll_Click(object sender, EventArgs e)
        {
            foreach (Control c in tlpFields.Controls)
            {
                CheckBox cb = (CheckBox)c;
                cb.Checked = false;
            }
            btShowFields.Enabled = false;
        }

        // Muestra los campos seleccionados 
        private async void btShowFields_Click(object sender, EventArgs e)
        {
            // Toma la url del dataset
            url = tbURLDataset.Text+"?";
            // Verifica los campos seleccionados
            url_final = url;
            url_final += "$select=";
            foreach (Control c in tlpFields.Controls)
            {
                CheckBox cb = (CheckBox)c;
                if (cb.Checked)
                {
                    url_final += cb.Text + ",";
                    boxChecked = true;
                }
            }
            url_final = url_final.TrimEnd(',');

            result = await getHttps(url_final);
            if (result == null || result.Equals(""))
            {
                MessageBox.Show("El Dataset buscado no fue encontrado");
            }

            meditions = JsonConvert.DeserializeObject<List<Medition>>(result);
            if (boxChecked == true)
            {
                dataGridView1.DataSource = meditions;  
            }
            else
            {
                MessageBox.Show("Debe marcar algún campo");
            }


            foreach (Control c in tlpFields.Controls)
            {
                CheckBox cb = (CheckBox)c;
                if (cb.Checked) {
                    cbFields.Items.Add(cb.Text);
                }
            }
  
        }

        // Muestra los filtros
        private async void btFilter_Click_1(object sender, EventArgs e)
        {
            url_final = url;

            // Agrega la clausula indicada a la url
            if (!cbClauses.SelectedItem.Equals("Seleccione un clausula"))
            {
                if (cbClauses.SelectedItem.Equals("select"))
                {
                    url_final += "$" + cbClauses.SelectedItem + "=";

                    if (!cbFields.SelectedItem.Equals("Seleccione un campo"))
                    {
                        url_final += cbFields.SelectedItem;
                    }
                    if (!cbSeparater.SelectedItem.Equals("Seleccione un separador"))
                    {
                        url_final += "  " + cbSeparater.SelectedItem;
                    }
                    if (!tbValue.Text.Equals(" "))
                    {
                        url_final += tbValue.Text;
                    }

                }
                else if (cbClauses.SelectedItem.Equals("where"))
                {
                    url_final += "$" + cbClauses.SelectedItem+"=";

                    if (!cbFields.SelectedItem.Equals("Seleccione un campo"))
                    {
                        url_final += cbFields.SelectedItem;
                    }
                    if (!cbSeparater.SelectedItem.Equals("Seleccione un separador"))
                    {
                        url_final += "  " + cbSeparater.SelectedItem+ "  ";
                    }
                    if (!tbValue.Text.Equals(" "))
                    {
                        url_final += tbValue.Text;
                    }

                }
                else if (cbClauses.SelectedItem.Equals("order"))
                {
                    url_final += "$" + cbClauses.SelectedItem + "=";

                    if (!cbFields.SelectedItem.Equals("Seleccione un campo"))
                    {
                        url_final += cbFields.SelectedItem;
                    }
                    if (!cbSeparater.SelectedItem.Equals("Seleccione un separador"))
                    {
                        url_final += "  "+cbSeparater.SelectedItem;
                    }
                    if (!tbValue.Text.Equals(" "))
                    {
                        url_final += tbValue.Text;
                    }
                   
                } 
            }


            // Agrega el límite del número de filas
             if (tbNumberRows.Text.Trim()!="")
             {
                url_final += "&$limit=" + tbNumberRows.Text;
             }
             
            Console.WriteLine(url_final);

            result = await getHttps(url_final);
            url_final = "";

            Console.WriteLine(url_final);

            if (result == null || result.Equals(""))
            {
                MessageBox.Show("El Dataset buscado no fue encontrado");
            }

            meditions = JsonConvert.DeserializeObject<List<Medition>>(result);
            if (boxChecked == true)
            {
                dataGridView1.DataSource = meditions;
              
            }
            else
            {
                MessageBox.Show("Debe marcar algún campo");
            }

        }

        private void cbClauses_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbSeparater.Items.Clear();

            if (cbClauses.SelectedItem.Equals("select"))
            {
                cbSeparater.Items.Add("Seleccione un separador");
                cbSeparater.SelectedIndex = 0;
                cbSeparater.Items.Add("=");
            }
            else if (cbClauses.SelectedItem.Equals("where"))
            {
                cbSeparater.Items.Add("Seleccione un separador");
                cbSeparater.SelectedIndex = 0;
                cbSeparater.Items.Add("=");
                cbSeparater.Items.Add(">");
                cbSeparater.Items.Add("<");
            }
            else if (cbClauses.SelectedItem.Equals("order"))
            {
                cbSeparater.Items.Add("Seleccione un separador");
                cbSeparater.SelectedIndex = 0;
                cbSeparater.Items.Add("DESC");
                cbSeparater.Items.Add("ASC");
            }

            if (cbClauses.SelectedItem.Equals("select"))
            {
                cbSeparater.Enabled = false;
                tbValue.Enabled = false;
            }
            else if (cbClauses.SelectedItem.Equals("where"))
            {
                cbSeparater.Enabled = true;
                tbValue.Enabled = true;
            }
            else if (cbClauses.SelectedItem.Equals("order"))
            {
                cbSeparater.Enabled = true;
                tbValue.Enabled = false;
            }

        }

        private void tlpFields_Paint(object sender, PaintEventArgs e)
        {
            foreach (Control c in tlpFields.Controls)
            {
                CheckBox cb = (CheckBox)c;
                if (cb.Checked==true)
                {
                    btShowFields.Enabled = true;
                }
            }
        }

        private void cbFields_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbClauses.SelectedIndex == 0) {
                cbSeparater.Enabled = false;
            }
        }
    }
}
