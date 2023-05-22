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
                    while (true)
                    {
                        try
                        {
                            try
                            {
                                driver.FindElement(By.CssSelector("button.sc-dIfARi.gbCagB.MuiButtonBase-root.MuiButton-root.MuiButton-text.MuiButton-textPrimary.MuiButton-sizeMedium.MuiButton-textSizeMedium.sc-hHTYSt.dnTtlO.MuiButton-root.MuiButton-text.MuiButton-textPrimary.MuiButton-sizeMedium.MuiButton-textSizeMedium.pt-2.text-primary-light.underline.opacity-80")).Click();
                            }
                            catch { }
                            driver.FindElement(By.Id("email-input")).SendKeys(user.email);
                            driver.FindElement(By.Id("password-input")).SendKeys(user.pass);
                            driver.FindElement(By.CssSelector("form.flex.flex-col.gap-4 > button")).Submit();
                            break;
                        }
                        catch { }
                        Thread.Sleep(49);//Minimum CPU usage        
                    }

                    /* Claim daily */
                    while (true)
                    {
                        try
                        {
                            try
                            {
                                //if profile have image
                                driver.FindElement(By.CssSelector("div.cursor-pointer.flex.items-center.flex-shrink-0 > img")).Click();
                            }
                            catch
                            {
                                //if profile not have image
                                driver.FindElement(By.CssSelector("div.cursor-pointer.flex.items-center.flex-shrink-0 > div")).Click();
                            }
                            //profile button class
                            driver.FindElement(By.CssSelector("li.sc-dIfARi.gbCagB.MuiButtonBase-root.MuiMenuItem-root.MuiMenuItem-gutters.sc-brePNt.bOBcly.MuiMenuItem-root.MuiMenuItem-gutters[tabindex='0']")).Click();
                            //exit menu
                            driver.FindElement(By.CssSelector("div.sc-jSUZER.icZvms.sc-fbYMXx.MuiPopover-root.sc-fXqpFg.gUdnEO.MuiMenu-root.MuiModal-root > div.sc-eDvSVe.leIUKU.MuiBackdrop-root.MuiBackdrop-invisible.sc-gKPRtg.ioznrs.MuiModal-backdrop")).Click();
                            //open credit page
                            driver.FindElement(By.CssSelector("a.flex.gap-2.items-center.font-bold.font-quicksand.text-theme-primary")).Click();
                            try
                            {
                                //claim credit
                                driver.FindElement(By.CssSelector("div.flex.flex-col.gap-6.w-fit > section > button")).Click();
                                break;
                            }
                            catch
                            {
                                //check credit is claimed?
                                var text = driver.FindElement(By.CssSelector("div.flex.flex-col.gap-6.w-fit > section > button > div > div > div")).Text;
                                Debug.WriteLine(text);
                                if (text != null) break;
                            }
                        }
                        catch { }
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
