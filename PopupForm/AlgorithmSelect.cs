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
        //算法框类型
        string typename;
        public AlgorithmSelect()
        {
            InitializeComponent();
            GetSubSubstratecm();
            SubSubstratecm.SelectedIndex = 0;
        }

        private void Okbt_Click(object sender, EventArgs e)
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
            algorithmtype.Owmername = SubSubstratecm.SelectedItem.ToString();
            this.Tag = algorithmtype;
            this.DialogResult = DialogResult.OK;
        }

        private void Canelbt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //获取子基板信息，填充到combox中
        public void GetSubSubstratecm() {
            string selectsql = "select zijiname from zijiban";
            DataTable dataTable = SQLiteHelper.GetDataTable(selectsql);
            //List<string> comboxstring = new List<string>();
            for (int i = 0; i < dataTable.Rows.Count; i++) {
                SubSubstratecm.Items.Add(dataTable.Rows[i]["zijiname"].ToString());
            }

        
        }
    }
}
