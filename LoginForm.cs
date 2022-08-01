using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOOVad
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                if (textBox1.Text.Equals("user") && textBox2.Text.Equals("user"))
                {
                    MainForm main = new MainForm();
                    main.Show();
                    this.Hide();
                } 
                else if (textBox1.Text.Equals("admin") && textBox2.Text.Equals("admin"))
                {
                    MainForm main = new MainForm();
                    main.setAdmin(textBox1.Text);
                    main.Show();
                    this.Hide();
                } 
                else
                {
                    MessageBox.Show("Неправильное имя пользователя и/или пароль!");
                }
            } else
            {
                MessageBox.Show("Поля не заполнены!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
