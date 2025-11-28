using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalLabActivity1
{
    public partial class AdminModule : Form
    {
        public AdminModule()
        {
            InitializeComponent();
        }

        private void AdminModule_Load(object sender, EventArgs e)
        {

        }
        private string _userId;

        public AdminModule(string userId) 
        {
            InitializeComponent();
            _userId = userId; 
        }

    }
}
