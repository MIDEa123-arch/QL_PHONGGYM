using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_PHONGGYM.All_User_Control
{
    public partial class UC_Employee : UserControl
    {
        private OracleConnection conn;
        public UC_Employee(OracleConnection connection)
        {
            InitializeComponent();
            conn = connection;
        }
        public UC_Employee() : this(null)
        {
        }
    }
}
