using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pcbaoi.Tools
{
    public class DataGridviewUtils
    {
        public static int Getrowid(DataGridView dataGridView,int Columnid,string findvalue) {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView.Rows) 
            {
                if (row.Cells[Columnid].Value.ToString().Equals(findvalue)) {
                    return count;                
                }
                count++;
            
            }
            return -99;
        
        
        
        }
    }
}
