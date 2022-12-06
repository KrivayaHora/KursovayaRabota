﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursovaya_Rabota.FormsPC
{
    public partial class CoolerCPUView : Form
    {
        MySqlConnection ConnectStaff;
        MySqlDataAdapter MyDA = new MySqlDataAdapter();
        BindingSource BindingS = new BindingSource();
        DataTable DT = new DataTable();
        void LoadImage(string a)
        {
            var rec = WebRequest.Create(a);
            using (var res = rec.GetResponse())
            using (var stream = res.GetResponseStream())
                pictureBox1.Image = Bitmap.FromStream(stream);
        }
        public void GetCooler()
        {
            string sqlview = "SELECT Items.ID AS `код`, Manufacture.title AS `Производитель`, Items.Title AS `Название`, Type.title AS `Тип товара`, Items.Price AS `Цена` FROM Items JOIN Type ON Items.Type_id = Type.id JOIN Manufacture ON Items.Manufacture_id = Manufacture.id WHERE Type.id = 8";
            ConnectStaff.Open();

            MyDA.SelectCommand = new MySqlCommand(sqlview, ConnectStaff);
            MyDA.Fill(DT);

            BindingS.DataSource = DT;

            dataGridView1.DataSource = BindingS;
            ConnectStaff.Close();

            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[4].Visible = true;
            dataGridView1.Columns[5].Visible = true;
            dataGridView1.Columns[6].Visible = true;


            dataGridView1.Columns[0].FillWeight = 20;
            dataGridView1.Columns[1].FillWeight = 15;
            dataGridView1.Columns[2].FillWeight = 15;
            dataGridView1.Columns[3].FillWeight = 15;
            dataGridView1.Columns[4].FillWeight = 15;
            dataGridView1.Columns[5].FillWeight = 15;
            dataGridView1.Columns[6].FillWeight = 15;

            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.RowHeadersVisible = false;

            dataGridView1.ColumnHeadersVisible = true;
        }
        public CoolerCPUView()
        {
            InitializeComponent();
        }

        private void CoolerCPUView_Load(object sender, EventArgs e)
        {
            ConnectStaff = new MySqlConnection("server=chuc.caseum.ru;port=33333;username=st_2_20_24;password=54843478;database=is_2_20_st24_KURS");
            //ConnectStaff = new MySqlConnection("server=10.90.12.110;port=33333;username=st_2_20_24;password=54843478;database=is_2_20_st24_KURS");
            GetCooler();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string id = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string socket;
                string chipset;
                string typemem;
                string ssdm;
                string sata;
                string video;
                string Name;

                ConnectStaff.Open();
                string sqlSocket = "SELECT Properties_value.`Value` FROM Properties_value WHERE Properties_value.Property_ID = 80 AND Properties_value.Item_ID = " + id;
                MySqlCommand cmd1 = new MySqlCommand(sqlSocket, ConnectStaff);
                socket = cmd1.ExecuteScalar().ToString();
                label12.Text = socket;

                string sqlchipset = "SELECT Properties_value.`Value` FROM Properties_value WHERE Properties_value.Property_ID = 82 AND Properties_value.Item_ID = " + id;
                MySqlCommand cmdchipset = new MySqlCommand(sqlchipset, ConnectStaff);
                chipset = cmdchipset.ExecuteScalar().ToString();
                label13.Text = chipset;

                string sqltypemem = "SELECT Properties_value.`Value` FROM Properties_value WHERE Properties_value.Property_ID = 81 AND Properties_value.Item_ID = " + id;
                MySqlCommand cmdtypemem = new MySqlCommand(sqltypemem, ConnectStaff);
                typemem = cmdtypemem.ExecuteScalar().ToString();
                label14.Text = typemem;

                string sqlssdm = "SELECT Properties_value.`Value` FROM Properties_value WHERE Properties_value.Property_ID = 84 AND Properties_value.Item_ID = " + id;
                MySqlCommand cmdThread = new MySqlCommand(sqlssdm, ConnectStaff);
                ssdm = cmdThread.ExecuteScalar().ToString();
                label15.Text = ssdm;

                string sqlvideo = "SELECT Properties_value.`Value` FROM Properties_value WHERE Properties_value.Property_ID = 83 AND Properties_value.Item_ID = " + id;
                MySqlCommand cmdvideo = new MySqlCommand(sqlvideo, ConnectStaff);
                video = cmdvideo.ExecuteScalar().ToString();
                label16.Text = video;

                string sqlImage = "SELECT Items.URLphoto FROM Items where Items.ID = " + id;
                MySqlCommand cmdpic = new MySqlCommand(sqlImage, ConnectStaff);
                string pic = cmdpic.ExecuteScalar().ToString();
                LoadImage(pic);

                string sqlName = "SELECT Items.Title FROM Items where Items.ID = " + id;
                MySqlCommand cmdName = new MySqlCommand(sqlName, ConnectStaff);
                Name = cmdName.ExecuteScalar().ToString();
                label1.Text = Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ConnectStaff.Close();
            }
        }
    }
}
