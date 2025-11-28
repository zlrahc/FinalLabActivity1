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
    public partial class UserModule : Form
    {
        private string _userId;

        public UserModule(string userId)
        {
            InitializeComponent();
            _userId = userId;
            this.IsMdiContainer = true;
        }

        private void UserModule_Load(object sender, EventArgs e)
        {
            // Initialize MenuStrip or Buttons for modules
            MenuStrip menu = new MenuStrip();

            ToolStripMenuItem accountMenu = new ToolStripMenuItem("My Account");
            accountMenu.Click += myAccountToolStripMenuItem_Click;

            ToolStripMenuItem borrowMenu = new ToolStripMenuItem("Borrow Equipment");
            borrowMenu.Click += borrowEquipmentToolStripMenuItem_Click;

            ToolStripMenuItem returnMenu = new ToolStripMenuItem("Return Equipment");
            returnMenu.Click += returnEquipmentToolStripMenuItem_Click;

            menu.Items.Add(accountMenu);
            menu.Items.Add(borrowMenu);
            menu.Items.Add(returnMenu);
            this.MainMenuStrip = menu;
            this.Controls.Add(menu);
        }

        private void myAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserAccount accountForm = new UserAccount(_userId);
            accountForm.MdiParent = this;
            accountForm.Show();
        }

        private void borrowEquipmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BorrowForm borrowForm = new BorrowForm(_userId);
            borrowForm.MdiParent = this;
            borrowForm.Show();
        }

        private void returnEquipmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReturnForm returnForm = new ReturnForm(_userId);
            returnForm.MdiParent = this;
            returnForm.Show();
        }
    }
}
