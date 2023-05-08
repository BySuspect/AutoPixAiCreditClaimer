using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace AutoPixAiCreditClaimer
{
    public partial class Form1 : Form
    {
        List<UserItems> UserList = new List<UserItems>();
        public Form1()
        {
            InitializeComponent();

            /// Add accounts to list
            UserList.Add(new UserItems
            {
                email = "mail",
                pass = "pass",
            });
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            foreach (var user in UserList)
            {

                if (true)
                {
                    IWebDriver driver = new ChromeDriver();

                    /* Login */
                    driver.Navigate().GoToUrl("https://pixai.art/login");
                    IWebElement loginbtn;
                    IWebElement emailInput;
                    IWebElement passInput;
                    while (true)
                    {
                        try
                        {
                            emailInput = driver.FindElement(By.Id("email-input"));
                            passInput = driver.FindElement(By.Id("password-input"));
                            loginbtn = driver.FindElement(By.Id(":r0:"));
                            break;
                        }
                        catch { }
                        Thread.Sleep(49);//Minimum CPU usage        
                    }
                    emailInput.SendKeys(user.email);
                    passInput.SendKeys(user.pass);
                    loginbtn.Submit();

                    /* Claim daily */
                    while (true)
                    {
                        try
                        {
                            try
                            {
                                driver.FindElement(By.CssSelector("div.cursor-pointer.flex.items-center.flex-shrink-0 > img")).Click();
                            }
                            catch
                            {
                                driver.FindElement(By.CssSelector("div.cursor-pointer.flex.items-center.flex-shrink-0 > div")).Click();
                            }
                            driver.FindElement(By.CssSelector("li.sc-dkrFOg.eQVywz.MuiButtonBase-root.MuiMenuItem-root.MuiMenuItem-gutters.sc-fHSyak.iTNXVP.MuiMenuItem-root.MuiMenuItem-gutters")).Click();
                            driver.FindElement(By.CssSelector("div.sc-kDvujY.kBxslW.sc-fbYMXx.MuiPopover-root.sc-fXqpFg.gUdnEO.MuiMenu-root.MuiModal-root > div.sc-jrcTuL.jacrvR.MuiBackdrop-root.MuiBackdrop-invisible.sc-ipEyDJ.gqbUTC.MuiModal-backdrop")).Click();
                            driver.FindElement(By.CssSelector("a.flex.gap-2.items-center.font-bold.font-quicksand.text-theme-primary")).Click();
                            try
                            {
                                driver.FindElement(By.CssSelector("div.flex.flex-col.gap-6.w-fit > section > button")).Click();
                                break;
                            }
                            catch
                            {
                                var text = driver.FindElement(By.CssSelector("div.flex.flex-col.gap-6.w-fit > section > button > div > div > div")).Text;
                                Debug.WriteLine(text);
                                if (text != null) break;
                            }
                        }
                        catch {/* MessageBox.Show("Login error");*/ }
                        Thread.Sleep(49);//Minimum CPU usage        
                    }

                    //Thread.Sleep(-1);
                    driver.Quit();

                }
                Thread.Sleep(49);//Minimum CPU usage      
            }
            Application.Exit();
        }
        class UserItems
        {
            public string email { get; set; }
            public string pass { get; set; }
        }
    }
}
