using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.IO;
using System.Data.SqlServerCe;

namespace RAT
{

    public partial class Home : Form
    {
        String uname;
        String pass;
        String rec;
        String mailfilepath = "C:/Rat/incidents.html";
        String msbifilepath = "C:/Rat/msbi.txt";
        int interval;
        String recpath = "C:/Rat/rec.config";
        String settingspath = "C:/Rat/settings.config";
        IWebDriver driver;
        mailsender ms = new mailsender();
        private SqlCeCommand cmd;
        private SqlCeConnection con;
        private string c = "Persist Security Info = False; Data Source = 'SalesData.sdf';" +
  "Password = 'myrod'; File Mode = 'shared read'; " +
  "Max Database Size = 256; Max Buffer Size = 1024";

        public Home()
        {

            InitializeComponent();
            if (!File.Exists("/SalesData.sdf/"))
            {
                string connString = "Data Source='SalesData.sdf'; LCID=1033;   Password='myrod'; Encrypt = TRUE;";
                SqlCeEngine engine = new SqlCeEngine(connString);
                engine.CreateDatabase();
                string query = "create table User(Email Varchar(100), password Varchar(100))";

                con = new SqlCeConnection(c);
                con.Open();
                SqlCeCommand cmd = new SqlCeCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }



            if (File.Exists(recpath) == false)
                 File.Create(recpath).Close();
                
                
            if (File.Exists(mailfilepath) == false)
                File.Create(mailfilepath).Close();
            if (File.Exists(settingspath) == false)
                File.Create(settingspath).Close();

            InitializeComponent();
            

            pictureBox1.ImageLocation = @"C:\Users\MukeshStyles\Pictures\rat\noanimation.gif";
        }


        

     
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

           new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                pictureBox1.ImageLocation = @"C:\Users\MukeshStyles\Pictures\rat\animation.gif";

                System.Threading.Thread.Sleep(4800);
                pictureBox1.ImageLocation = @"C:\Users\MukeshStyles\Pictures\rat\processing.gif";

                uname = "097757";
                pass = "Today1234";
                driver= new ChromeDriver("C:/Rat/");
                driver.Url = "https://brisbanecc.onbmc.com";
                driver.Manage().Window.Maximize();
                driver.FindElement(By.Id("userNameInput")).SendKeys(uname);
                driver.FindElement(By.Id("passwordInput")).SendKeys(pass);
                driver.FindElement(By.ClassName("submit")).Click();



                try
                {
                    System.Threading.Thread.Sleep(5000);
                  
                    if (driver.PageSource.Contains("Logout") == false)
                    {
                        MessageBox.Show("Wrong Username or Password");

                        return;
                    }
                }
                catch (Exception exp)
                {
                    //Alert alert = driver.switchTo().alert();


                }

                driver.FindElement(By.XPath("//*[@id=\"reg_img_304316340\"]")).Click();
                System.Threading.Thread.Sleep(2000);
                var element = driver.FindElement(By.XPath("//*[text()='Incident Management']"));
                Actions actions = new Actions(driver);
                actions.MoveToElement(element);
                actions.Perform();

                System.Threading.Thread.Sleep(2000);
                var element1 = driver.FindElement(By.XPath("//*[text()='Incident Management Console']"));
                Actions actions1 = new Actions(driver);
                actions1.MoveToElement(element1).Click();
                actions1.Perform();

                System.Threading.Thread.Sleep(5000);
                var element2 = driver.FindElement(By.XPath("//INPUT[@id='arid_WIN_3_303174300']"));
                Actions actions2 = new Actions(driver);
                actions2.MoveToElement(element2).Click();
                actions2.Perform();

                System.Threading.Thread.Sleep(2000);
                var element3 = driver.FindElement(By.XPath("//*[text()='Assigned To My Selected Groups']"));
                Actions actions3 = new Actions(driver);
                actions3.MoveToElement(element3).Click();
                actions3.Perform();

                System.Threading.Thread.Sleep(5000);
                driver.SwitchTo().Window(driver.WindowHandles.Last());
                driver.Manage().Window.Maximize();
                driver.FindElement(By.XPath("//*[text()='OK']")).Click();
                driver.SwitchTo().Window(driver.WindowHandles.First());

                //  System.Threading.Thread.Sleep(5000);
                // driver.FindElement(By.XPath("//*[@id='WIN_3_304017000']/div/div")).Click();

                System.Threading.Thread.Sleep(5000);
                driver.FindElement(By.XPath("//*[@id='WIN_3_304017000']/div/div")).Click();

                while (true)
                {

                    System.Threading.Thread.Sleep(5000);
                    driver.FindElement(By.XPath("//*[@id='reg_img_304352241']")).Click();


                    System.Threading.Thread.Sleep(5000);
                    String incidents = driver.FindElement(By.XPath("//*[@id='WIN_3_302087200']/div[2]/div/div[2]")).Text;



                    if (File.ReadAllText(mailfilepath).Contains(incidents.Replace("INC", "</td></tr><tr><td>INC")) == false)
                    {
                        if (File.Exists(mailfilepath) == false)
                        {
                            var localmailfile = File.Create(mailfilepath);
                            localmailfile.Close();
                        }
                        incidents = incidents.Replace("INC", "</td></tr><tr><td>INC");
                        StreamWriter mailfile = new StreamWriter(mailfilepath, append: false);
                        mailfile.WriteLine(incidents);
                        mailfile.Close();
                        string[] inc = new string[6];
                        string line;

                        int counter = 0;
                        // System.IO.StreamReader file =
                        // new System.IO.StreamReader(mailfilepath);
                         System.IO.StreamReader filemsbi = new System.IO.StreamReader(msbifilepath);

                        string readtext= File.ReadAllText(mailfilepath, Encoding.UTF8);

                        while((line=filemsbi.ReadLine())!=null)
                        {
                           // MessageBox.Show(line);
                           // MessageBox.Show(readtext);
                            if (readtext.Contains(line))
                            {
                                MessageBox.Show(line);
                                ms.sendmail();
                                
                            }


           }
                        /**
                        while ((line = file.ReadLine()) != null)
                        {
                            MessageBox.Show("one");
                            inc[counter] = line;
                            // MessageBox.Show(counter.ToString());
                            if (counter % 5 == 0 && counter != 0)
                            {
                                MessageBox.Show("two");
                                MessageBox.Show(inc[2]);
                                //Check for MSBI Track
                                while(file.ReadLine()!=null)
                                {
                                    MessageBox.Show("three");
                                    if (inc[2].Contains(file.ReadLine())!=null)
                                    {
                                        MessageBox.Show(file.ReadLine());
                                        MessageBox.Show(inc[counter]);
                                        MessageBox.Show("End");
                                    }
                                }
                            }


                            counter++;

                         //   ms.sendmail();
                        } **/


                        //  driver.FindElement(By.XPath("//*[@id='WIN_3_302087200']/div[2]/div/div[2]")).Click();
                    }
                }
            }).Start();

          

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                // Ratsettings rs = new Ratsettings();
                //rs.Show();

                Settings s = new Settings();
                    s.Show();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }
    }
}
