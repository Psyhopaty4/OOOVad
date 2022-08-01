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
    public partial class AddMaterial : Form
    {
        public AddMaterial()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xkryn\source\repos\OOOVad\OOOVad\res\oooVad.mdf;Integrated Security=True;Connect Timeout=30";
        string[] data = new string[7];
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
        private void AddMaterial_Load(object sender, EventArgs e)
        {
            loadComboBox(comboBoxType, "SELECT [Тип материала] FROM Type", "Тип материала");
            loadComboBox(comboBoxSklad, "SELECT [Номер склада] FROM Sklad", "Номер склада");
            loadComboBox(comboBoxUnit, "SELECT [Единица измерения] FROM Units", "Единица измерения");

            if (this.getTypeQuery().Equals("add"))
                this.Text = "Добавление материала";
            else if (this.getTypeQuery().Equals("change"))
            {
                this.Text = "Редактирование";
                textBoxName.Text = data[1];
                comboBoxType.Text = data[2];
                comboBoxUnit.Text = data[3];
                comboBoxSklad.Text = data[4];   
                textBoxCount.Text = data[5];
                textBoxPrice.Text = data[6];
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
                if (!string.IsNullOrEmpty(textBoxName.Text) && !string.IsNullOrWhiteSpace(textBoxName.Text) &&
                    !string.IsNullOrEmpty(textBoxCount.Text) && !string.IsNullOrWhiteSpace(textBoxCount.Text) &&
                    !string.IsNullOrEmpty(comboBoxSklad.Text) && !string.IsNullOrWhiteSpace(comboBoxSklad.Text) &&
                    !string.IsNullOrEmpty(comboBoxType.Text) && !string.IsNullOrWhiteSpace(comboBoxType.Text) &&
                    !string.IsNullOrEmpty(textBoxPrice.Text) && !string.IsNullOrWhiteSpace(textBoxPrice.Text) &&
                    !string.IsNullOrEmpty(comboBoxUnit.Text) && !string.IsNullOrWhiteSpace(comboBoxUnit.Text))
                {
                    sqlConnection = new SqlConnection(@connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO Material ([Наименование], [id_type], [id_unit], [id_sklad], [Количество], [Цена])" +
                        " VALUES (@Name, @Type, @Unit, @Sklad, @Count, @Price)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("Name", textBoxName.Text);
                    sqlCommand.Parameters.AddWithValue("Type", comboBoxType.SelectedIndex + 1);
                    sqlCommand.Parameters.AddWithValue("Unit", comboBoxUnit.SelectedIndex + 1);
                    sqlCommand.Parameters.AddWithValue("Sklad", comboBoxSklad.SelectedIndex + 1);
                    sqlCommand.Parameters.AddWithValue("Count", Convert.ToInt32(textBoxCount.Text));
                    sqlCommand.Parameters.AddWithValue("Price", Convert.ToInt64(textBoxPrice.Text));

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
                    !string.IsNullOrEmpty(textBoxCount.Text) && !string.IsNullOrWhiteSpace(textBoxCount.Text) &&
                    !string.IsNullOrEmpty(comboBoxSklad.Text) && !string.IsNullOrWhiteSpace(comboBoxSklad.Text) &&
                    !string.IsNullOrEmpty(comboBoxType.Text) && !string.IsNullOrWhiteSpace(comboBoxType.Text) &&
                    !string.IsNullOrEmpty(textBoxPrice.Text) && !string.IsNullOrWhiteSpace(textBoxPrice.Text) &&
                    !string.IsNullOrEmpty(comboBoxUnit.Text) && !string.IsNullOrWhiteSpace(comboBoxUnit.Text))
                {
                    sqlConnection = new SqlConnection(@connectionString);
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand("UPDATE Material SET " +
                        $"[Наименование] = '{textBoxName.Text}'," +
                        $"[id_type] = '{comboBoxType.SelectedIndex + 1}'," +
                        $"[id_unit] = '{comboBoxUnit.SelectedIndex + 1}'," +
                        $"[id_sklad] = '{comboBoxSklad.SelectedIndex + 1}'," +
                        $"[Количество] = '{Convert.ToInt32(textBoxCount.Text)}'," +
                        $"[Цена] = '{Convert.ToDouble(textBoxPrice.Text)}' WHERE id = {data[0]}", sqlConnection);

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
