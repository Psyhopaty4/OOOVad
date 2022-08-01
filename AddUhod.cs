using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOOVad
{
    public partial class AddUhod : Form
    {
        public AddUhod()
        {
            InitializeComponent();
        }

        SqlConnection sqlConnection = new SqlConnection();

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xkryn\source\repos\OOOVad\OOOVad\res\oooVad.mdf;Integrated Security=True;Connect Timeout=30";
        double sum = 0;
        double sumAll = 0;
        int id = 0;
        double price = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                addInvoice();
                addMaterial();
                textBox1.Enabled = false;
                maskedTextBox1.Enabled = false;
                textBoxCount.Text = "";
                textBoxSum.Text = "";
                comboBoxSklad.Text = "";
                comboBoxMaterial.Text = "";
            }
            else if (id != 0)
            {
                addMaterial();
                textBoxCount.Text = "";
                textBoxSum.Text = "";
                comboBoxSklad.Text = "";
                comboBoxMaterial.Text = "";
            }
            sumAllAdd();
            this.Close();
        }

        private void addInvoice()
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox1.Text) && !string.IsNullOrWhiteSpace(maskedTextBox1.Text) &&
                    !string.IsNullOrEmpty(textBoxCount.Text) && !string.IsNullOrWhiteSpace(textBoxCount.Text) &&
                    !string.IsNullOrEmpty(textBoxSum.Text) && !string.IsNullOrWhiteSpace(textBoxSum.Text) &&
                    comboBoxMaterial.SelectedIndex > -1 && comboBoxSklad.SelectedIndex > -1)
                {
                    sqlConnection = new SqlConnection(@connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO Sale ([Номер отпуска], [Дата оформления], [Сумма])" +
                        " VALUES (@Num, @DataCreate, @Sum)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Num", textBox1.Text);
                    sqlCommand.Parameters.AddWithValue("DataCreate", maskedTextBox1.Text);
                    sqlCommand.Parameters.AddWithValue("Sum", sumAll);

                    sqlCommand.ExecuteNonQuery();

                    SqlCommand sqlCommandGetID = new SqlCommand($"SELECT id FROM Sale WHERE [Номер отпуска] LIKE {textBox1.Text}", sqlConnection);
                    id = Convert.ToInt32(sqlCommandGetID.ExecuteScalar());
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
        private void addMaterial()
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
                    !string.IsNullOrEmpty(maskedTextBox1.Text) && !string.IsNullOrWhiteSpace(maskedTextBox1.Text) &&
                    !string.IsNullOrEmpty(textBoxCount.Text) && !string.IsNullOrWhiteSpace(textBoxCount.Text) &&
                    !string.IsNullOrEmpty(textBoxSum.Text) && !string.IsNullOrWhiteSpace(textBoxSum.Text) &&
                    comboBoxMaterial.SelectedIndex > -1 && comboBoxSklad.SelectedIndex > -1)
                {
                    sqlConnection = new SqlConnection(@connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO SalesInvoice ([id_material], [id_sklad], [Количество], [Сумма], [id_invoice])" +
                        " VALUES (@Material, @Sklad, @Count, @Sum, @Invoice)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Material", Convert.ToInt32(comboBoxMaterial.SelectedIndex) + 1);
                    sqlCommand.Parameters.AddWithValue("Sklad", Convert.ToInt32(comboBoxSklad.SelectedIndex) + 1);
                    sqlCommand.Parameters.AddWithValue("Count", Convert.ToInt32(textBoxCount.Text));
                    sqlCommand.Parameters.AddWithValue("Sum", Convert.ToDouble(textBoxSum.Text));
                    sqlCommand.Parameters.AddWithValue("Invoice", id);

                    sqlCommand.ExecuteNonQuery();

                    /*SqlCommand sqlCommandGetCount = new SqlCommand($"SELECT [Количество] FROM Material WHERE [id_sklad] = {Convert.ToInt32(comboBoxSklad.SelectedIndex) + 1} ", sqlConnection);
                    SqlCommand sqlCommandAddCount = new SqlCommand($"UPDATE Material SET [Количество] = {Convert.ToInt32(textBoxCount.Text)} " +
                        $"WHERE [id_sklad] = {Convert.ToInt32(comboBoxSklad.SelectedIndex) + 1}", sqlConnection);*/

                    sumAll += Convert.ToDouble(textBoxSum.Text);
                    label7.Text = sumAll.ToString();
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
        private void sumAllAdd()
        {
            try
            {
                sqlConnection = new SqlConnection(@connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand($"UPDATE Sale SET [Сумма] = {sumAll} WHERE [id] = {id}", sqlConnection);
                sqlCommand.ExecuteNonQuery();
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
        private void comboBoxMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxCount.Text) && !string.IsNullOrWhiteSpace(textBoxCount.Text) &&
                !string.IsNullOrEmpty(comboBoxMaterial.Text) && !string.IsNullOrWhiteSpace(comboBoxMaterial.Text))
            {
                sqlConnection = new SqlConnection(@connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand($"SELECT [Цена] FROM Material WHERE [Наименование] LIKE '{comboBoxMaterial.Text}", sqlConnection);
                price = Convert.ToDouble(sqlCommand.ExecuteScalar());
                sqlConnection.Close();
                textBoxSum.Text = Convert.ToString(Convert.ToDouble(textBoxCount.Text) * price);
            }
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxCount.Text) && !string.IsNullOrWhiteSpace(textBoxCount.Text) &&
                !string.IsNullOrEmpty(comboBoxMaterial.Text) && !string.IsNullOrWhiteSpace(comboBoxMaterial.Text))
            {
                sqlConnection = new SqlConnection(@connectionString);
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand($"SELECT [Цена] FROM Material WHERE [Наименование] LIKE '{comboBoxMaterial.Text}'", sqlConnection);
                price = Convert.ToInt32(sqlCommand.ExecuteScalar());
                sqlConnection.Close();
                textBoxSum.Text = Convert.ToString(Convert.ToInt32(textBoxCount.Text) * price);
            }
        }

        void loadComboBox(ComboBox comboBox, string query, string name)
        {
            sqlConnection = new SqlConnection(@connectionString);
            DataTable table = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandText = query;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(table);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox.Items.Add(table.Rows[i][name]);
            }
        }

        private void AddUhod_Load(object sender, EventArgs e)
        {
            loadComboBox(comboBoxMaterial, "SELECT [Наименование] FROM Material", "Наименование");
            loadComboBox(comboBoxSklad, "SELECT [Номер склада] FROM Sklad", "Номер склада");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (id == 0)
            {
                addInvoice();
                addMaterial();
                textBox1.Enabled = false;
                maskedTextBox1.Enabled = false;
                textBoxCount.Text = "";
                textBoxSum.Text = "";
                comboBoxSklad.Text = "";
                comboBoxMaterial.Text = "";
            }
            else if (id != 0)
            {
                addMaterial();
                textBoxCount.Text = "";
                textBoxSum.Text = "";
                comboBoxSklad.Text = "";
                comboBoxMaterial.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
