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
    public partial class AddProvider : Form
    {
        public AddProvider()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xkryn\source\repos\OOOVad\OOOVad\res\oooVad.mdf;Integrated Security=True;Connect Timeout=30";
        string[] data = new string[6];
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

        private void AddProvider_Load(object sender, EventArgs e)
        {
            if (this.getTypeQuery().Equals("add"))
                this.Text = "Добавление поставщика";
            else if (this.getTypeQuery().Equals("change"))
            {
                this.Text = "Редактирование";
                textBoxName.Text = data[1];
                textBoxINN.Text = data[2];
                textBoxOGRN.Text = data[3];
                textBoxAdress.Text = data[4];
                textBoxRate.Text = data[5];
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
                if (!string.IsNullOrEmpty(textBoxName.Text) && !string.IsNullOrWhiteSpace(textBoxName.Text) &&
                    !string.IsNullOrEmpty(textBoxINN.Text) && !string.IsNullOrWhiteSpace(textBoxINN.Text) &&
                    !string.IsNullOrEmpty(textBoxOGRN.Text) && !string.IsNullOrWhiteSpace(textBoxOGRN.Text) &&
                    !string.IsNullOrEmpty(textBoxAdress.Text) && !string.IsNullOrWhiteSpace(textBoxAdress.Text) &&
                    !string.IsNullOrEmpty(textBoxRate.Text) && !string.IsNullOrWhiteSpace(textBoxRate.Text))
                {
                    sqlConnection = new SqlConnection(@connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO Provider ([Название организации], [ИНН], [ОГРН], [Адрес], [Рейтинг])" +
                        " VALUES (@Name, @INN, @OGRN, @Adress, @Rate)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Name", textBoxName.Text);
                    sqlCommand.Parameters.AddWithValue("INN", textBoxINN.Text);
                    sqlCommand.Parameters.AddWithValue("OGRN", textBoxOGRN.Text);
                    sqlCommand.Parameters.AddWithValue("Adress", textBoxAdress.Text);
                    sqlCommand.Parameters.AddWithValue("Rate", Convert.ToDouble(textBoxRate.Text));

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
                if (!string.IsNullOrEmpty(textBoxName.Text) && !string.IsNullOrWhiteSpace(textBoxName.Text) &&
                    !string.IsNullOrEmpty(textBoxINN.Text) && !string.IsNullOrWhiteSpace(textBoxINN.Text) &&
                    !string.IsNullOrEmpty(textBoxOGRN.Text) && !string.IsNullOrWhiteSpace(textBoxOGRN.Text) &&
                    !string.IsNullOrEmpty(textBoxAdress.Text) && !string.IsNullOrWhiteSpace(textBoxAdress.Text) &&
                    !string.IsNullOrEmpty(textBoxRate.Text) && !string.IsNullOrWhiteSpace(textBoxRate.Text))
                {
                    sqlConnection = new SqlConnection(@connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("UPDATE Provider SET " +
                        $"[[Название организации] = '{textBoxName.Text}'," +
                        $"[ИНН] = '{textBoxINN.Text}'," +
                        $"[ОГРН] = '{textBoxOGRN.Text}'," +
                        $"[Адрес] = '{textBoxAdress.Text}'," +
                        $"[Рейтинг] = '{Convert.ToDouble(textBoxRate.Text)}' WHERE id = {data[0]}", sqlConnection);

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
