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

        private string url = "https://www.datos.gov.co/resource/ysq6-ri4e.json";
        //private string url = "https://www.datos.gov.co/resource/ysq6-ri4e.json?autoridad_ambiental=AMVA";
        // private string url = "https://www.datos.gov.co/resource/ysq6-ri4e.json?autoridad_ambiental=AMVA"; 
        public Interface()
        {
            InitializeComponent();
            AddDataEntitiesComboBox();

        }

        public void AddDataEntitiesComboBox() {
            comboBox1.Items.Add("Seleccione una entidad");
            comboBox1.SelectedIndex = 0;
            comboBox1.Items.Add("AMVA");
            comboBox1.Items.Add("CAM");
            comboBox1.Items.Add("CAR");
            comboBox1.Items.Add("CARDER");
            comboBox1.Items.Add("CDMB");
            comboBox1.Items.Add("CODECHOCÓ");
            comboBox1.Items.Add("CORANTIOQUIA");
            comboBox1.Items.Add("CORMACARENA");
            comboBox1.Items.Add("CORNARE");
            comboBox1.Items.Add("CORPAMAG");
            comboBox1.Items.Add("CORPOBOYACA");
            comboBox1.Items.Add("CORPOCALDAS");
            comboBox1.Items.Add("CORPOCESAR");
            comboBox1.Items.Add("CORPOGUAJIRA");
            comboBox1.Items.Add("CORPONARIÑO");
            comboBox1.Items.Add("CORPONOR");
            comboBox1.Items.Add("CORPORINOQUIA");
            comboBox1.Items.Add("CORTOLIMA");
            comboBox1.Items.Add("CRA");
            comboBox1.Items.Add("CRC");
            comboBox1.Items.Add("CRQ");
            comboBox1.Items.Add("CVC");
            comboBox1.Items.Add("CVS");
            comboBox1.Items.Add("DAGMA");
            comboBox1.Items.Add("EPA Barranquilla Verde");
            comboBox1.Items.Add("EPA Cartagena");
            comboBox1.Items.Add("SDA");
        }

        public void FilterForEntities(string entity) 
        {
            url = "https://www.datos.gov.co/resource/ysq6-ri4e.json" + "?autoridad_ambiental=" + entity;
        }

        private async void Interface_Load(object sender, EventArgs e)
        {
            
        }


        public async Task<string> getHttps()
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            return await sr.ReadToEndAsync();
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterForEntities(""+comboBox1.SelectedItem);
            string respuesta = await getHttps();
            List<Medition> meditions = JsonConvert.DeserializeObject<List<Medition>>(respuesta);
            dataGridView1.DataSource = meditions;
        }
    }
}
