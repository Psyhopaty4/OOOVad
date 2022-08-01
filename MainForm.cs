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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        BindingSource bind1 = new BindingSource();
        BindingSource bind2 = new BindingSource();
        BindingSource bind3 = new BindingSource();
        BindingSource bind4 = new BindingSource();
        BindingSource bind5 = new BindingSource();
        BindingSource bind6 = new BindingSource();
        BindingSource bind7 = new BindingSource();
        BindingSource bind8 = new BindingSource();
        BindingSource bind9 = new BindingSource();

        DataSet dataSet = new DataSet();
        SqlDataAdapter sqlData = new SqlDataAdapter();

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xkryn\source\repos\OOOVad\OOOVad\res\oooVad.mdf;Integrated Security=True;Connect Timeout=30";
        string admin = "";

        public string getAdmin()
        {
            return admin;
        }

        public void setAdmin(string admin)
        {
            this.admin = admin; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (this.getAdmin().Equals("admin"))
            {
                toolStrip7.Enabled = true;
                toolStrip6.Enabled = true;
            } else
            {
                toolStrip7.Enabled = false;
                toolStrip6.Enabled = false;
            }

            loadData("SELECT Material.id, Material.[Наименование], Type.[Тип материала], " +
                "Units.[Единица измерения], Sklad.[Номер склада], Material.[Количество], Material.[Цена]" +
                "FROM Material, Type, Units, Sklad WHERE Type.id = Material.id_type AND Units.id = Material.id_unit AND " +
                "Sklad.id = Material.id_sklad", dataGridView1, bind1);
            loadData("SELECT * FROM Sklad", dataGridView2, bind2);
            loadData("SELECT * FROM Provider", dataGridView3, bind3);
            loadData("SELECT Purchase.[id], Purchase.[Номер поставки], Purchase.[Дата оформления], " +
                "Purchase.[Дата поставки], Provider.[Название организации] AS [Поставщик], Purchase.[Сумма] " +
                "FROM Purchase, Provider WHERE Provider.[id] = Purchase.[id_provider]", dataGridView4, bind4);
            loadData("SELECT * FROM Sale", dataGridView9, bind5);
            loadData("SELECT * FROM Type", dataGridView7, bind6);
            loadData("SELECT * FROM Units", dataGridView6, bind7);
        }

        public void loadData(string query, DataGridView dataGridView, BindingSource bindingSource)
        {
            SqlConnection sqlConnection = new SqlConnection(@connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText = query;
            sqlData = new SqlDataAdapter(sqlCommand);
            dataSet.Tables.Clear();
            sqlData.Fill(dataSet);
            bindingSource.DataSource = dataSet.Tables[0];
            dataGridView.DataSource = bindingSource;
            sqlConnection.Close();
        }

        private void toolStripButtonAdd_Click(object sender, EventArgs e)
        {
            AddMaterial addMaterial = new AddMaterial();
            addMaterial.setTypeQuery("add");
            addMaterial.Show();
        }

        private void toolStripButtonDelete_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            AddMaterial addMaterial = new AddMaterial();
            addMaterial.setTypeQuery("change");
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
                addMaterial.Data[i] = dataGridView1.Rows[selectedrowindex].Cells[i].Value.ToString();
            addMaterial.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            AddProvider addProvider = new AddProvider();
            addProvider.setTypeQuery("add");
            addProvider.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView3.SelectedCells[0].RowIndex;
            AddProvider addProvider = new AddProvider();
            addProvider.setTypeQuery("change");
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
                addProvider.Data[i] = dataGridView3.Rows[selectedrowindex].Cells[i].Value.ToString();
            addProvider.Show();
        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            AddUnit addUnit = new AddUnit();
            addUnit.setTypeQuery("add");
            addUnit.Show();
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView6.SelectedCells[0].RowIndex;
            AddUnit addUnit = new AddUnit();
            addUnit.setTypeQuery("change");
            for (int i = 0; i < dataGridView6.Columns.Count; i++)
                addUnit.Data[i] = dataGridView6.Rows[selectedrowindex].Cells[i].Value.ToString();
            addUnit.Show();
        }

        private void toolStripButton30_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView7.SelectedCells[0].RowIndex;
            AddType addType = new AddType();
            addType.setTypeQuery("change");
            for (int i = 0; i < dataGridView7.Columns.Count; i++)
                addType.Data[i] = dataGridView7.Rows[selectedrowindex].Cells[i].Value.ToString();
            addType.Show();
        }

        private void toolStripButton28_Click(object sender, EventArgs e)
        {
            AddType addType = new AddType();
            addType.setTypeQuery("add");
            addType.Show();
        }

        private void toolStripButton34_Click(object sender, EventArgs e)
        {
            AddPrihod addPrihod = new AddPrihod();
            addPrihod.Show();
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            AddUhod addUhod = new AddUhod();
            addUhod.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            AddPrihod addPrihod = new AddPrihod();
            addPrihod.Show();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            AddUhod addUhod = new AddUhod();
            addUhod.Show();
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectedIndex = e.RowIndex;
                int id = Convert.ToInt32(dataGridView4.Rows[selectedIndex].Cells["id"].Value);
                loadData($"SELECT Material.[Наименование] AS [Материал], Sklad.[Номер склада], " +
                    $"PurchaseInvoice.[Количество], PurchaseInvoice.[Сумма] " +
                    $"FROM PurchaseInvoice, Material, Sklad WHERE Material.[id] = PurchaseInvoice.[id_material] AND " +
                    $"Sklad.[id] = PurchaseInvoice.[id_sklad] AND [id_invoice] = {id}", dataGridView8, bind8);
            } catch (Exception ex)
            {
                MessageBox.Show("Не выбрана накладная!");
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            AddSklad addSklad = new AddSklad();
            addSklad.setTypeQuery("add");
            addSklad.Show();
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
            AddSklad addSklad = new AddSklad();
            addSklad.setTypeQuery("change");
            for (int i = 0; i < dataGridView2.Columns.Count; i++)
                addSklad.Data[i] = dataGridView2.Rows[selectedrowindex].Cells[i].Value.ToString();
            addSklad.Show();
        }

        private void dataGridView9_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int selectedIndex = e.RowIndex;
                int id = Convert.ToInt32(dataGridView9.Rows[selectedIndex].Cells["id"].Value);
                loadData($"SELECT Material.[Наименование] AS [Материал], Sklad.[Номер склада], " +
                    $"SalesInvoice.[Количество], SalesInvoice.[Сумма] " +
                    $"FROM SalesInvoice, Material, Sklad WHERE Material.[id] = SalesInvoice.[id_material] AND " +
                    $"Sklad.[id] = SalesInvoice.[id_sklad] AND SalesInvoice.[id_invoice] = {id}", dataGridView5, bind9);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не выбрана накладная!");
            }
        }

        private void toolStripButtonUpdate_Click(object sender, EventArgs e)
        {
            loadData("SELECT Material.id, Material.[Наименование], Type.[Тип материала], " +
                "Units.[Единица измерения], Sklad.[Номер склада], Material.[Количество], Material.[Цена]" +
                "FROM Material, Type, Units, Sklad WHERE Type.id = Material.id_type AND Units.id = Material.id_unit AND " +
                "Sklad.id = Material.id_sklad", dataGridView1, bind1);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            loadData("SELECT * FROM Sklad", dataGridView2, bind2);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            loadData("SELECT * FROM Provider", dataGridView3, bind3);
        }

        private void toolStripButton31_Click(object sender, EventArgs e)
        {
            loadData("SELECT Purchase.[id], Purchase.[Номер поставки], Purchase.[Дата оформления], " +
                "Purchase.[Дата поставки], Provider.[Название организации] AS [Поставщик], Purchase.[Сумма] " +
                "FROM Purchase, Provider WHERE Provider.[id] = Purchase.[id_provider]", dataGridView4, bind4);
        }

        private void toolStripButton33_Click(object sender, EventArgs e)
        {
            loadData("SELECT * FROM Sale", dataGridView9, bind5);
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            loadData("SELECT * FROM Units", dataGridView6, bind7);
        }

        private void toolStripButton26_Click(object sender, EventArgs e)
        {
            loadData("SELECT * FROM Type", dataGridView7, bind6);
        }

        private void toolStripTextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(toolStripTextBoxSearch.Text) && !string.IsNullOrWhiteSpace(toolStripTextBoxSearch.Text))
                bind1.Filter = $"[Наименование] LIKE '{toolStripTextBoxSearch.Text}%'";
            else
                bind1.RemoveFilter();
        }
    }
}
