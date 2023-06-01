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
                IWebDriver driver = new ChromeDriver();


                /* Login */
                driver.Navigate().GoToUrl("https://pixai.art/login");
                while (true)
                {
                    try
                    {
                        try
                        {
                            driver.FindElement(By.CssSelector("div[id='root'] > div > button")).Click();
                        }
                        catch { }
                        driver.FindElement(By.Id("email-input")).SendKeys(user.email);
                        driver.FindElement(By.Id("password-input")).SendKeys(user.pass);
                        driver.FindElement(By.CssSelector("button[type='submit']")).Submit();
                        break;
                    }
                    catch { }
                    Thread.Sleep(49);//Minimum CPU usage        
                }

                /* Claim daily */
                try
                {
                    while (true)
                    {
                        try
                        {
                            //if profile have image
                            driver.FindElement(By.CssSelector("div.cursor-pointer.flex.items-center.flex-shrink-0 > img")).Click();
                            break;
                        }
                        catch
                        {
                            try
                            {
                                //if profile not have image
                                driver.FindElement(By.CssSelector("div.cursor-pointer.flex.items-center.flex-shrink-0 > div")).Click();
                                break;
                            }
                            catch
                            {
                                try
                                {
                                    //check is wrong password
                                    driver.FindElement(By.CssSelector("svg[data-testid='ReportProblemOutlinedIcon']"));
                                    MessageBox.Show("invalid account");
                                    goto endprogress;
                                }
                                catch
                                { }
                            }
                        }
                        Thread.Sleep(49);//Minimum CPU usage        
                    }

                drowdownmenu:
                    try
                    {
                        //profile button
                        driver.FindElement(By.CssSelector("li[role='menuitem'][tabindex='0']")).Click();
                    }
                    catch
                    {
                        Thread.Sleep(49);//Minimum CPU usage
                        goto drowdownmenu;
                    }

                //exitmenu:
                //    try
                //    {
                //        //exit menu
                //        driver.FindElement(By.CssSelector("div[class='sc-jSUZER icZvms sc-fbYMXx MuiPopover-root sc-fXqpFg gUdnEO MuiMenu-root MuiModal-root'] > div")).Click();
                //    }
                //    catch (Exception)
                //    {
                //        Thread.Sleep(49);//Minimum CPU usage
                //        goto exitmenu;
                //    }

                opencreditpage:
                    try
                    {
                        //open credit page
                        driver.FindElement(By.CssSelector("div.flex.gap-4.items-center > a")).Click();
                    }
                    catch
                    {
                        Thread.Sleep(49);//Minimum CPU usage
                        goto opencreditpage;
                    }

                claimcredit:
                    try
                    {
                        //claim credit
                        driver.FindElement(By.CssSelector("div.flex.flex-col.gap-6.w-fit > section > button")).Click();
                        goto endprogress;
                    }
                    catch
                    {
                        try
                        {
                            //check credit is claimed?
                            var text = driver.FindElement(By.CssSelector("div.flex.flex-col.gap-6.w-fit > section > button > div > div > div")).Text;
                            Debug.WriteLine(text);
                            if (text != null) goto endprogress;
                        }
                        catch
                        {
                            goto claimcredit;
                        }
                    }
                }
                catch { }

            endprogress:
                driver.Quit();
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
