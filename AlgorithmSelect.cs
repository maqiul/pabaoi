using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi
{
    public partial class AlgorithmSelect : Form
    {
        string typename;
        public AlgorithmSelect()
        {
            InitializeComponent();
            getcombox();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control control in panel3.Controls) {

                if (control is RadioButton) {
                    RadioButton radioButton = (RadioButton)control;
                    if (radioButton.Checked) {
                        typename = radioButton.Text;                                       
                    }
                
                }
                            
            }
            Algorithmtype algorithmtype = new Algorithmtype();
            algorithmtype.Typename = typename;
            algorithmtype.Owmername = comboBox1.SelectedItem.ToString();
            this.Tag = algorithmtype;
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void getcombox() {
            string selectsql = "select zijiname from zijiban";
            DataTable dataTable = SQLiteHelper.GetDataTable(selectsql);
            //List<string> comboxstring = new List<string>();
            for (int i = 0; i < dataTable.Rows.Count; i++) {
                comboBox1.Items.Add(dataTable.Rows[i]["zijiname"].ToString());
            }

        
        }
    }
}
