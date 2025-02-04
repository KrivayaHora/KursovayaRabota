﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Kursovaya_Rabota.FormsPC;
using MySql.Data.MySqlClient;

namespace Kursovaya_Rabota.Project.Forms
{
    public partial class Managementform : Form
    {
        MySqlConnection ConnectStaff;
        Form activeform;
       void ChildForm(Form Child)
       {
           if(activeform != null)
           {
               activeform.Close();
           }
           activeform = Child;
           Child.TopLevel = false;
           this.IsMdiContainer = true;
           Child.FormBorderStyle = FormBorderStyle.None;
           Child.Dock = DockStyle.Fill;
           panel4.Controls.Add(Child);
           panel4.Tag = Child;
           Child.BringToFront();
           Child.Show();
       }
        public Managementform()
        {
            InitializeComponent();
        }
        private void Management_Load(object sender, EventArgs e)
        {
            ConnectStaff = new MySqlConnection("server=chuc.caseum.ru;port=33333;username=st_2_20_24;password=54843478;database=is_2_20_st24_KURS");
            //ConnectStaff = new MySqlConnection("server=10.90.12.110;port=33333;username=st_2_20_24;password=54843478;database=is_2_20_st24_KURS");
            
        }
        private void ClientsButton_Click(object sender, EventArgs e)
        {
            ChildForm(new ClientsV());
            label2.Text = "Вкладка клиенты";
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            ChildForm(new PcBuild());
            label2.Text = "Вкладка конфигуратор";
        }

        private void ComponentButton_Click(object sender, EventArgs e)
        {
            ChildForm(new NewComponent());
            label2.Text = "Вкладка учёт комплектующих";
        }

        private void StaffButton_Click(object sender, EventArgs e)
        {
            ChildForm(new Staff());
            label2.Text = "Вкладка сотрудники";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChildForm(new Config());
            label2.Text = "Вкладка сборки";
        }
    }
}
