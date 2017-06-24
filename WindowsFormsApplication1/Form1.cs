using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronPython;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        int a, b, nPart;
        ScriptEngine m_pyEngine = null;
        ScriptScope m_pyScope = null;
        ScriptSource m_Source = null;
        public Form1()
        {
            InitializeComponent();
            m_pyEngine = Python.CreateEngine();
            m_pyScope = m_pyEngine.CreateScope();
            textBox4.Text = "0";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength != 0)
            {
                a = Convert.ToInt32(textBox1.Text);
                if (a > 223)
                {
                    MessageBox.Show("Please enter a number in range [0,223]");
                }
                else if (a < 128)
                {
                    textBox5.Text = "8";
                    nPart = 8;
                }
                else if (a >= 128 && a < 192)
                {
                    textBox5.Text = "16";
                    nPart = 16;

                }
                else if (a >= 192 && a < 224)
                {
                    textBox5.Text = "24";
                    nPart = 24;
                }
                if (a < 128)
                {
                    textBox3.Text = "0";
                    textBox3.Enabled = false;
                    textBox2.Text = "0";
                    textBox2.Enabled = false;
                }
                if (a >= 128)
                {
                    textBox2.Enabled = true;

                }
                if (a >= 192)
                    textBox3.Enabled = true;
            }

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength != 0)
            {
                b = Convert.ToInt32(textBox2.Text);
                if (b > 255)
                {
                    MessageBox.Show("Please enter a number in range [0,255]");
                }
            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.TextLength != 0)
            {
                b = Convert.ToInt32(textBox3.Text);
                if (b > 255)
                {
                    MessageBox.Show("Please enter a number in range [0,255]");
                }
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.TextLength != 0)
            {
                b = Convert.ToInt32(textBox4.Text);
                if (b > 255)
                {
                    MessageBox.Show("Please enter a number in range [0,255]");
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text.Length != 0 && textBox2.Text.Length != 0 && textBox3.Text.Length != 0))
            {
                fillsubnetcombos(nPart);
            }


        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.ShowDialog();
                m_Source = m_pyEngine.CreateScriptSourceFromFile(openFileDialog1.FileName);
                m_Source.Execute(m_pyScope);
                dynamic open = m_pyScope.GetVariable("get");
                open(this);
            }
            else MessageBox.Show("You haven't selected any number of subnets");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x, y;
            x = Convert.ToInt32((comboBox1.Text).ToString());
            x = Convert.ToInt32(Math.Ceiling(Math.Log(x, 2)));
            y = Convert.ToInt32((textBox5.Text).ToString());
            textBox6.Text = (x + y).ToString();
            tbMaskA.Enabled = true;
            tbMaskB.Enabled = true;
            tbMaskC.Enabled = true;
            tbMaskD.Enabled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mA, mB, mC, mD;
            int netNum, subnet;
            netNum = Convert.ToInt32(textBox5.Text.ToString());

            if (tbMaskA.TextLength != 0 && tbMaskB.TextLength != 0 && tbMaskC.TextLength != 0 && tbMaskD.TextLength != 0 && textBox6.TextLength != 0)
            {
                netNum = Convert.ToInt32(textBox5.Text.ToString());
                subnet = Convert.ToInt32(comboBox1.Text.ToString());
                subnet = Convert.ToInt32(Math.Ceiling(Math.Log(subnet, 2)));
                mA = tbMaskA.Text.ToString();
                mB = tbMaskB.Text.ToString();
                mC = tbMaskC.Text.ToString();
                mD = tbMaskD.Text.ToString();
                Mask mask = new Mask(mA, mB, mC, mD);
                richTextBox2.Text = mask.getMask(mask, netNum, subnet);

            }
        }
        private void tbMaskA_TextChanged(object sender, EventArgs e)
        {
            if (tbMaskA.TextLength != 0)
            {
                int maskA;
                maskA = Convert.ToInt32(tbMaskA.Text);
                if (maskA > 224)
                {
                    MessageBox.Show("Please enter a number in range [0,224]");
                }
            }
        }

        private void tbMaskA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
           (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }

        private void tbMaskB_TextChanged(object sender, EventArgs e)
        {
            if (tbMaskB.TextLength != 0)
            {
                int maskB;
                maskB = Convert.ToInt32(tbMaskB.Text);
                if (maskB > 224)
                {
                    MessageBox.Show("Please enter a number in range [0,255]");
                }
            }

        }

        private void tbMaskB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void tbMaskC_TextChanged(object sender, EventArgs e)
        {
            if (tbMaskC.TextLength != 0)
            {
                int maskC;
                maskC = Convert.ToInt32(tbMaskC.Text);
                if (maskC > 224)
                {
                    MessageBox.Show("Please enter a number in range [0,255]");
                }
            }
        }

        private void tbMaskC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void tbMaskD_TextChanged(object sender, EventArgs e)
        {
            if (tbMaskD.TextLength != 0)
            {
                int maskD;
                maskD = Convert.ToInt32(tbMaskD.Text);
                if (maskD > 224)
                {
                    MessageBox.Show("Please enter a number in range [0,255]");
                }
            }
        }

        private void tbMaskD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        public void fillsubnetcombos(int c)
        {
            if (c == 8)
            {
                c = 16;
                MessageBox.Show("Iz razloga sto racunalu treba previse vremena dok ucita sve elemente  u broj mogućih podmreza (combox) ogranicio sam broj na 2^16 elemenata, dok bi za a klasu mreža trebao biti 2 ^24 sto je prevelik broj. Potrebno je određeno vrijeme da se učitaju 2^16 elemeneta u comboboxove.");

            }
            comboBox1.Items.Clear();
            int d, brmr;
            d = 32 - c;
            brmr = Convert.ToInt32(Math.Pow(2, d));
            comboBox1.BeginUpdate();
            for (int i = 0; i < brmr; i++)
            {
                comboBox1.Items.Add(i + 1);
            }
            comboBox1.EndUpdate();
        }
    }
}
