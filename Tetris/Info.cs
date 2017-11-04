using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Info : Form
    {
        public Info()
        {
            InitializeComponent();
        }

        private void Info_Load(Object sender, EventArgs e)
        {
            StringBuilder txtMethod = new StringBuilder();
            txtMethod.AppendLine("Method: ");
            txtMethod.AppendLine("");
            txtMethod.AppendLine("↑: rotate block");
            txtMethod.AppendLine("↓: move block down");
            txtMethod.AppendLine("←: move block left");
            txtMethod.AppendLine("→: move block right");
            txtMethod.AppendLine("Space: drop block");
            txtMethod.AppendLine("A: start");
            txtMethod.AppendLine("S: pause");
            txtMethod.AppendLine("D: stop");
            lblMethod.Text = txtMethod.ToString();

            StringBuilder txtDeveloper = new StringBuilder();
            txtDeveloper.AppendLine("Developer Information");
            txtDeveloper.AppendLine("Minseok Choi");
            txtDeveloper.AppendLine("pronovice2000@gmail.com");
            lblDeveloper.Text = txtDeveloper.ToString();
        }

        private void btnClose_Click(Object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
