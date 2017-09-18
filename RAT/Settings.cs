using System;

using System.Windows.Forms;
using System.Data.SqlServerCe;
using System.IO;


namespace RAT
{
    public partial class Settings : Form
    {

        private SqlCeCommand cmd;
        private SqlCeConnection con;
        private string c = "Persist Security Info = False; Data Source = 'SalesData.sdf';" +
    "Password = 'myrod'; File Mode = 'shared read'; " +
    "Max Database Size = 256; Max Buffer Size = 1024";


        public Settings()
        {
            

            string query = "create table EXP(Email Varchar(100), password Varchar(100))";

            con = new SqlCeConnection(c);
            con.Open();
            SqlCeCommand cmd = new SqlCeCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }



        private void ribbonButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Details Saved");
        }

        private void ribbonButton2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void ribbonTab2_ActiveChanged(object sender, EventArgs e)
        {

        }

        private void ribbonButton3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void CreateUser()
        {
            string query = "create table EXP(Email Varchar(100), password Varchar(100))";

            con = new SqlCeConnection(c);
            con.Open();
            SqlCeCommand cmd = new SqlCeCommand(query, con);                       
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            

            con = new SqlCeConnection(c);
            con.Open();
            var comm = new SqlCeCommand("Insert into EXP(ID,source) Values(1,2)", con);
            comm.ExecuteNonQuery();
            
            con.Close();
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            con = new SqlCeConnection(c);
            con.Open();
            var comm = new SqlCeCommand("SELECT ID FROM EXP", con);
            SqlCeDataReader reader = comm.ExecuteReader();

            while (reader.Read())
                MessageBox.Show(reader.GetInt32(0).ToString());
            con.Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }
    }


}
