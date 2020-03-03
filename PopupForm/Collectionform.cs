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
    public partial class Collectionform : Form
    {
        public Collectionform(string width,string height , string pcbwidth, string pcbheight)
        {
            InitializeComponent();
            Pcbwidthtx.Text = pcbheight;
            Pcbheighttx.Text = pcbwidth;
            Widthtx.Text = height;
            Heighttx.Text = width;
            Typecom.SelectedIndex = 0;
        }

        private void Pcbwidthtx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void Pcbheighttx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void Okbt_Click(object sender, EventArgs e)
        {
            Collection collection = new Collection();
            collection.Type = Typecom.SelectedItem.ToString();
            collection.Width = Widthtx.Text;
            collection.Height = Heighttx.Text;
            collection.Pcbwidth = Pcbwidthtx.Text;
            collection.Pcbheight = Pcbheighttx.Text;
            this.Tag = collection;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            
        }

        private void Canelbt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Widthtx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }

        private void Heighttx_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是退格和数字，则屏蔽输入
            if (!(e.KeyChar == '\b' || (e.KeyChar >= '0' && e.KeyChar <= '9')))
            {
                e.Handled = true;
            }
        }
    }
}
