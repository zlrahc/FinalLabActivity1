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

namespace FinalLabActivity1
{
    public partial class BorrowForm : Form
    {
        private string _userId;
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\codes\\Visual Studio\\FinalLabActivity1\\MainDatabase.mdf\";Integrated Security=True";

        public BorrowForm(string userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void BorrowForm_Load(object sender, EventArgs e)
        {
            LoadAvailableEquipment();
        }

        private void LoadAvailableEquipment()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT EquipmentID, Name, Quantity, Description FROM equipments";
                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvEquipment.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (dgvEquipment.CurrentRow != null)
            {
                string equipmentId = dgvEquipment.CurrentRow.Cells["EquipmentID"].Value.ToString();
                DateTime borrowDate = DateTime.Now;

                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string insertQuery = @"INSERT INTO records (RecordID, UserID, EquipmentID, DateBorrowed, Status)
                                               VALUES (@recordId, @userId, @equipmentId, @dateBorrowed, 'Borrowed')";

                        SqlCommand cmd = new SqlCommand(insertQuery, con);
                        cmd.Parameters.AddWithValue("@recordId", Guid.NewGuid().ToString());
                        cmd.Parameters.AddWithValue("@userId", _userId);
                        cmd.Parameters.AddWithValue("@equipmentId", equipmentId);
                        cmd.Parameters.AddWithValue("@dateBorrowed", borrowDate);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Equipment borrowed successfully!");
                        LoadAvailableEquipment();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
