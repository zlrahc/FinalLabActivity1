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
    public partial class ReturnForm : Form
    {
        private string _userId;
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\codes\\Visual Studio\\FinalLabActivity1\\MainDatabase.mdf\";Integrated Security=True";

        public ReturnForm(string userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void ReturnForm_Load(object sender, EventArgs e)
        {
            LoadBorrowedEquipment();
        }

        private void LoadBorrowedEquipment()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"SELECT r.RecordID, e.Name, r.DateBorrowed
                                     FROM records r
                                     INNER JOIN equipments e ON r.EquipmentID = e.EquipmentID
                                     WHERE r.UserID = @userId AND r.Status='Borrowed'";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddWithValue("@userId", _userId);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvBorrowed.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvBorrowed.CurrentRow != null)
            {
                string recordId = dgvBorrowed.CurrentRow.Cells["RecordID"].Value.ToString();
                DateTime returnDate = DateTime.Now;

                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        string updateQuery = @"UPDATE records 
                                               SET DateReturned=@dateReturned, Status='Returned'
                                               WHERE RecordID=@recordId";

                        SqlCommand cmd = new SqlCommand(updateQuery, con);
                        cmd.Parameters.AddWithValue("@dateReturned", returnDate);
                        cmd.Parameters.AddWithValue("@recordId", recordId);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Equipment returned successfully!");
                        LoadBorrowedEquipment();
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
