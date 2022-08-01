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
    public partial class AddSklad : Form
    {
        public AddSklad()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xkryn\source\repos\OOOVad\OOOVad\res\oooVad.mdf;Integrated Security=True;Connect Timeout=30";
        string[] data = new string[3];
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
        private void AddSklad_Load(object sender, EventArgs e)
        {
            if (this.getTypeQuery().Equals("add"))
                this.Text = "Добавление склада";
            else if (this.getTypeQuery().Equals("change"))
            {
                this.Text = "Редактирование";
                textBoxNum.Text = data[1];
                textBoxAddres.Text = data[2];
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

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.getTypeQuery().Equals("add"))
                add();
            else if (this.getTypeQuery().Equals("change"))
                update();
        }

        private void add()
        {
            try
            {
                if (!string.IsNullOrEmpty(textBoxNum.Text) && !string.IsNullOrWhiteSpace(textBoxNum.Text) &&
                    !string.IsNullOrEmpty(textBoxAddres.Text) && !string.IsNullOrWhiteSpace(textBoxAddres.Text))
                {
                    sqlConnection = new SqlConnection(@connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO Sklad ([Номер склада], [Адрес])" +
                        " VALUES (@Num, @Addres)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Num", Convert.ToInt64(textBoxNum.Text));
                    sqlCommand.Parameters.AddWithValue("Addres", textBoxAddres.Text);

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
                if (!string.IsNullOrEmpty(textBoxNum.Text) && !string.IsNullOrWhiteSpace(textBoxNum.Text) &&
                    !string.IsNullOrEmpty(textBoxAddres.Text) && !string.IsNullOrWhiteSpace(textBoxAddres.Text))
                {
                    sqlConnection = new SqlConnection(@connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("UPDATE Sklad SET " +
                        $"[Номер склада] = '{Convert.ToInt64(textBoxNum.Text)}'," +
                        $"[Адрес] = '{textBoxAddres.Text}' WHERE id = {data[0]}", sqlConnection);

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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
