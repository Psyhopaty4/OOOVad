using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OOOVad
{
    public partial class AddType : Form
    {
        public AddType()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xkryn\source\repos\OOOVad\OOOVad\res\oooVad.mdf;Integrated Security=True;Connect Timeout=30";
        string[] data = new string[2];
        string typeQuery = "";

        SqlConnection sqlConnection = new SqlConnection();

        public string[] Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.getTypeQuery().Equals("add"))
                add();
            else if (this.getTypeQuery().Equals("change"))
                update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddType_Load(object sender, EventArgs e)
        {
            if (this.getTypeQuery().Equals("add"))
                this.Text = "Добавление материала";
            else if (this.getTypeQuery().Equals("change"))
            {
                this.Text = "Редактирование";
                textBoxUnit.Text = data[1];
            }
        }

        public void setTypeQuery(string typeQuery)
        {
            this.typeQuery = typeQuery;
        }

        public string getTypeQuery()
        {
            return typeQuery;
        }

        private void add()
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxUnit.Text) && !string.IsNullOrWhiteSpace(textBoxUnit.Text))
                {
                    sqlConnection = new SqlConnection(@connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO Type ([Тип материала])" +
                        " VALUES (@Type)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Type", textBoxUnit.Text);


                    sqlCommand.ExecuteNonQuery();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Все поля обязательны к заполнению!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void update()
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxUnit.Text) && !string.IsNullOrWhiteSpace(textBoxUnit.Text))
                {
                    sqlConnection = new SqlConnection(@connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("UPDATE Type SET " +
                        $"[Тип материала] = '{textBoxUnit.Text}' WHERE id = {data[0]}", sqlConnection);

                    sqlCommand.ExecuteNonQuery();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Все поля обязательны к заполнению!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

    }
}
