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
    public partial class UserAccount : Form
    {
        private string _userId;
        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\codes\\Visual Studio\\FinalLabActivity1\\MainDatabase.mdf\";Integrated Security=True";

        public UserAccount(string userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void UserAccount_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"SELECT s.StudentID, s.FirstName, s.LastName, s.Gender, s.Course
                                     FROM students s
                                     INNER JOIN users u ON u.StudentID = s.StudentID
                                     WHERE u.UserID = @userId";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@userId", _userId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblStudentID.Text = reader["StudentID"].ToString();
                        lblFirstName.Text = reader["FirstName"].ToString();
                        lblLastName.Text = reader["LastName"].ToString();
                        lblGender.Text = reader["Gender"].ToString();
                        lblCourse.Text = reader["Course"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}