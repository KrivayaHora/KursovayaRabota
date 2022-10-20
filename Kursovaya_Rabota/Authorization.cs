﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MySql;
using MySql.Data.MySqlClient;
using MetroFramework.Forms;

namespace Kursovaya_Rabota
{
    public partial class Autorization : MetroForm
    {
        static string sha256(string randomString)
        {
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        public void RoleUsers()
        {
            ConnectStaff = new MySqlConnection("server=chuc.caseum.ru;port=33333;username=st_2_20_24;password=54843478;database=is_2_20_st24_KURS");
            string UserLG = LoginBox.Text;
            string sql = "SELECT `Role` FROM Employee WHERE `Login` = @LG";
            ConnectStaff.Open();
            MySqlParameter Par = new MySqlParameter("LG", UserLG);

            MySqlCommand command = new MySqlCommand(sql, ConnectStaff);
            command.Parameters.Add(Par);

            string Role = command.ExecuteScalar().ToString();

            switch (Role)
            {
                case "Администратор":
                    ManagementForm form = new ManagementForm();
                    form.Show();
                    break;
                case "Сборщик":
                    PcBuild pcform = new PcBuild();
                    pcform.Show();
                    break;
                default:
                    MessageBox.Show("Пользователь неопознан");
                    this.Close();
                    break;
            }
        }
        MySqlConnection ConnectStaff;
        public Autorization()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConnectStaff = new MySqlConnection("server=chuc.caseum.ru;port=33333;username=st_2_20_24;password=54843478;database=is_2_20_st24_KURS");
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            ConnectStaff.Open();
            string sql = $"SELECT * FROM `Employee` WHERE `Login`= @LB AND `Password`= @PB";

            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(sql, ConnectStaff);
            cmd.Parameters.Add("@LB", MySqlDbType.VarChar).Value = LoginBox.Text;
            cmd.Parameters.Add("@PB", MySqlDbType.VarChar).Value = sha256(PasswordBox.Text);
           
            adapter.SelectCommand = cmd;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                RoleUsers();
            }
            else
                MessageBox.Show("Неверные данные");

            ConnectStaff.Close();
            this.Visible = false;
        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoginBox_TextChanged(object sender, EventArgs e)
        {

        }

       

        private void materialRaisedButton2_Click_1(object sender, EventArgs e)
        {
            RegistrationForm form = new RegistrationForm();
            form.Show();
        }
    }
}
