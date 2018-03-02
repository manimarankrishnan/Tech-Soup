using System;
using System.Threading;
using System.Collections.Generic;
using SeleniumCSharp.Framework;
using SeleniumCSharp.Selenium;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace SeleniumCSharp.Pages
{
    public class Billing : Page
    {
        public List<String> totalAmntOfEachProduct { get; set; }
        public List<String> actualTotalAmntOfEachProduct { get; set; }

        public Billing(DriverWrapper Driver) : base(Driver)
        {

        }

        public List<String> CalculateTotalAmountOfEachProductInBillingPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            Thread.Sleep(4000);
            Actions actions = new Actions(Driver);
            actions.MoveToElement(Driver.FindElement(By.CssSelector
                (".table.table-xs-headers.table-cart>tbody>tr")));
            actions.Perform();
            int totalProducts = Driver.FindElements(By.CssSelector(".table.table-xs-headers.table-cart>tbody>tr")).Count;
            totalAmntOfEachProduct = new List<String>();
            for (int i = 1; i <= totalProducts; i++)
            {
                String qty = Driver.FindElement(By.CssSelector(".table.table-xs-headers.table-cart>tbody>tr:nth-child(1)>td[headers='Quantity']")).Text;
               // String qty1 = Driver.FindElement(By.CssSelector(".table.table-xs-headers.table-cart>tbody>tr:nth-child(+i)>td[headers='Quantity']")).Text;
                string adFee = Driver.FindElement(By.CssSelector(".table.table-xs-headers.table-cart>tbody>tr:nth-child(1)>td[headers='Admin Fee']")).Text.Trim('$');
                Double quantity;
                Double.TryParse(qty.ToString(), out quantity);
                double adminFee;
               Double.TryParse(adFee, out adminFee);
                Double tot=  quantity * adminFee;
                totalAmntOfEachProduct.Add(tot.ToString());

            }
            return totalAmntOfEachProduct;
        }

        public List<String> GetTotalAmountOfEachProductInBillingPage()
        {
            Actions actions = new Actions(Driver);
            actions.MoveToElement(Driver.FindElement(By.CssSelector
                (".table.table-xs-headers.table-cart>tbody>tr")));
            actions.Perform();

            int totalProducts = Driver.FindElements(By.CssSelector(".table.table-xs-headers.table-cart>tbody>tr")).Count;
            actualTotalAmntOfEachProduct = new List<String>();
            Double tot;
            for (int i = 1; i <= totalProducts; i++)
            {
               Double.TryParse(Driver.FindElement(By.CssSelector(".table.table-xs-headers.table-cart>tbody>tr:nth-child(1)>td[headers='Total']")).Text.Trim('$'), out tot);
                actualTotalAmntOfEachProduct.Add(tot.ToString());
            }
            return actualTotalAmntOfEachProduct;
        }

        public String CalculateSubTotalAmountInBillingPage(List<String> actualTotalAmntOfEachProduct)
        {
            Double subTotalAmnt = 0;
            Double prodSubTotalAmnt;
            for (int i = 0; i < actualTotalAmntOfEachProduct.Count; i++)
            {
                Double.TryParse(actualTotalAmntOfEachProduct[i].ToString(), out prodSubTotalAmnt);
                subTotalAmnt += prodSubTotalAmnt;
            }
            return subTotalAmnt.ToString();
        }

        public String GetSubTotalAmountInBillingPage()
        {
            Double subTot;

            Actions actions = new Actions(Driver);
            actions.MoveToElement(Driver.FindElement(By.CssSelector
                ("#SubTotalTopValue")));
            actions.Perform();

            Double.TryParse(Driver.FindElement(By.CssSelector("#SubTotalTopValue")).Text.Trim('$'), out subTot);
            return subTot.ToString();
        }

        public String CalculateTotalDueAmountInBillingPage()
        {
            Double subTotAmnt;
            Double salesTax;
            Double shippAmnt;
            Actions actions = new Actions(Driver);
            actions.MoveToElement(Driver.FindElement(By.CssSelector
                ("#SubTotalTopValue")));
            actions.MoveToElement(Driver.FindElement(By.CssSelector
                ("#TaxTotalAmount")));
            actions.MoveToElement(Driver.FindElement(By.CssSelector
                ("#ShippingGrandTotalAmount")));
            actions.Perform();
            String subtotalAmnt = Driver.FindElement(By.CssSelector("#SubTotalTopValue")).Text.Trim('$');
            String salesTaxTotalAmnt = Driver.FindElement(By.CssSelector("#TaxTotalAmount")).Text.Trim('$');
            String shippingAmnt = Driver.FindElement(By.CssSelector("#ShippingGrandTotalAmount")).Text.Trim('$');
            Double.TryParse(subtotalAmnt.ToString(), out subTotAmnt);
            Double.TryParse(salesTaxTotalAmnt.ToString(), out salesTax);
            Double.TryParse(shippingAmnt.ToString(), out shippAmnt);
            Double tot = subTotAmnt + salesTax + shippAmnt;
            return tot.ToString();
        }

        public String GetTotalDueAmountInBillingPage()
        {
            Double tot;
            Actions actions = new Actions(Driver);
            actions.MoveToElement(Driver.FindElement(By.CssSelector
                ("#GrandTotalAmount")));
            actions.Perform();
            Double.TryParse(Driver.FindElement(By.CssSelector("#GrandTotalAmount")).Text.Trim('$'), out tot);
            return tot.ToString();
        }

        public List<String> IsCreditCardDetailsDisabledInBillingPage()
        {
            List<String> ccOptions = new List<String>();
            Actions actions = new Actions(Driver);
            actions.MoveToElement(Driver.FindElement(By.CssSelector
                ("select[id*='_creditCardExpirationYear']")));
            actions.Perform();
            ccOptions.Add(Driver.FindElement(By.CssSelector("select[id*='_CreditCardType']")).GetAttribute("disabled"));
            ccOptions.Add(Driver.FindElement(By.CssSelector("input[id*='_txtCCNumber']")).GetAttribute("disabled"));
            ccOptions.Add(Driver.FindElement(By.CssSelector("select[id*='_creditCardExpirationMonth']")).GetAttribute("disabled"));
            ccOptions.Add(Driver.FindElement(By.CssSelector("select[id*='_creditCardExpirationYear']")).GetAttribute("disabled"));
            return ccOptions;
        }

        public List<String> IsCreditCardBillingAddressDetailsDisabledInBillingPage()
        {
            List<String> ccBillingAddrOptions = new List<String>();
            ccBillingAddrOptions.Add(Driver.FindElement(By.CssSelector("input[id*='FirstName'][name='txtBillFirstName']")).GetAttribute("disabled"));
            ccBillingAddrOptions.Add(Driver.FindElement(By.CssSelector("input[id*='LastName'][name='txtBillLastName']")).GetAttribute("disabled"));
            ccBillingAddrOptions.Add(Driver.FindElement(By.CssSelector("input[id='BilltxtLine1']")).GetAttribute("disabled"));
            ccBillingAddrOptions.Add(Driver.FindElement(By.CssSelector("input[id='BilltxtLine2']")).GetAttribute("disabled"));
            ccBillingAddrOptions.Add(Driver.FindElement(By.CssSelector("select[id='BillddCountry']")).GetAttribute("disabled"));
            ccBillingAddrOptions.Add(Driver.FindElement(By.CssSelector("select[id='BillddState']")).GetAttribute("disabled"));
            ccBillingAddrOptions.Add(Driver.FindElement(By.CssSelector("#txtBillCity")).GetAttribute("disabled"));
            ccBillingAddrOptions.Add(Driver.FindElement(By.CssSelector("#txtBillZip")).GetAttribute("disabled"));
            return ccBillingAddrOptions;
        }


        public List<String> GetCCBAddressValuesInBillingPage()
        {
            List<String> ccBillingAddrOptions = new List<string>();
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

            ccBillingAddrOptions.Add(js.ExecuteScript("return $(\"input#FirstName\").val();").ToString());
            ccBillingAddrOptions.Add(js.ExecuteScript("return $(\"input[id*='LastName'][name='txtBillLastName']\").val();").ToString());
            ccBillingAddrOptions.Add(js.ExecuteScript("return $(\"input[id='BilltxtLine1']\").val();").ToString());
            ccBillingAddrOptions.Add(js.ExecuteScript("return $(\"input[id='BilltxtLine2']\").val();").ToString());
            ccBillingAddrOptions.Add(js.ExecuteScript("return $(\"select[id='BillddCountry']\").val();").ToString());
            ccBillingAddrOptions.Add(js.ExecuteScript("return $(\"select[id='BillddState']\").val();").ToString());
            ccBillingAddrOptions.Add(js.ExecuteScript("return $(\"#txtBillCity\").val();").ToString());
            ccBillingAddrOptions.Add(js.ExecuteScript("return $(\"#txtBillZip\").val();").ToString());
            return ccBillingAddrOptions;
        }

        public void EnterCCBAddrDetailsInBillingPage(List<String> addrDetails)
        {
            Actions actions = new Actions(Driver);
            actions.MoveToElement(Driver.FindElement(By.CssSelector
                ("#txtBillZip")));
            actions.Perform();
            Driver.FindElement(By.CssSelector("input[id*='FirstName'][name='txtBillFirstName']")).SendKeys(addrDetails[0]);
            Driver.FindElement(By.CssSelector("input[id*='LastName'][name='txtBillLastName']")).SendKeys(addrDetails[1]);
            Driver.FindElement(By.CssSelector("input[id='BilltxtLine1']")).SendKeys(addrDetails[2]);
            Driver.FindElement(By.CssSelector("input[id='BilltxtLine2']")).SendKeys(addrDetails[3]);

            SelectElement billCountry = new SelectElement(Driver.FindElement(By.CssSelector("select[id='BillddCountry']")));
            billCountry.SelectByValue(addrDetails[4]);
            SelectElement billState = new SelectElement(Driver.FindElement(By.CssSelector("select[id='BillddState']")));
            billState.SelectByValue(addrDetails[5]);
          
            Driver.FindElement(By.CssSelector("#txtBillCity")).SendKeys(addrDetails[6].Trim());
            Driver.FindElement(By.CssSelector("#txtBillZip")).SendKeys(addrDetails[7].Trim());
        }
        public IList<IWebElement> GetAllCardTypeOptionsInBillingPage()
        {
            SelectElement cartType = new SelectElement(Driver.FindElement(By.CssSelector("select[id*='_CreditCardType']")));
            return cartType.Options;
        }
        public void EnterCardNumberInBillingPage(String ccNumber)
        {
            Driver.FindElement(By.CssSelector("input[id*='_txtCCNumber']")).Clear();
            Driver.FindElement(By.CssSelector("input[id*='_txtCCNumber']")).SendKeys(ccNumber);
        }

        public String ValidationMsgOnInvalidCCNumberInBillingPage(String ccNumber, String ccType)
        {
            Actions actions = new Actions(Driver);
            actions.MoveToElement(Driver.FindElement(By.CssSelector
                ("span[id*='_creditCardNumberValidator']")));
            actions.Perform();
            Driver.FindElement(By.CssSelector("select[id*='_CreditCardType']")).SendKeys(ccType);
            Driver.FindElement(By.CssSelector("input[id*='_txtCCNumber']")).SendKeys(ccNumber);
            Driver.FindElement(By.CssSelector("input[id*='FirstName'][name='txtBillFirstName']")).Click();
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("span[id*='_creditCardNumberValidator']")));
            return Driver.FindElement(By.CssSelector("span[id*='_creditCardNumberValidator']")).Text;
        }

        public IList<IWebElement> GetCCExpirationYearDetails()
        {
            SelectElement expYear = new SelectElement(Driver.FindElement(By.CssSelector("select[id*='_creditCardExpirationYear']")));
            return expYear.Options;
        }


        public void SelectSameAsShippingAddress()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                (".checkbox>label>span")));
            Driver.FindElement(By.CssSelector(".checkbox>label>span")).Click();
        }

        public void SelectCheckPaymentMethod()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("#radio_cheque")));
            Driver.FindElement(By.CssSelector("#radio_cheque")).Click();
        }

        public void SelectCreditCardPaymentMethod()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("#radio_creditcard")));
            Driver.FindElement(By.CssSelector("#radio_creditcard")).Click();
        }

        public void SelectCCExpirationMonth(string expMonth)
        {
            SelectElement exp = new SelectElement(Driver.FindElement(By.CssSelector("select[id*='_creditCardExpirationMonth']")));
            exp.SelectByText(expMonth);
        }

        public void clickPlaceRequestButton()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[class='btn btn-primary btn-xs-full-width']")));
            Driver.FindElement(By.CssSelector("input[class='btn btn-primary btn-xs-full-width']")).Click();
        }

        public void clickgoBackButtonInBillingPage()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(TestConfiguration.PageLoadTimeout));
            wait.Until(ExpectedConditions.ElementExists(By.CssSelector
                ("input[id*='_butPrevious']")));
            Driver.FindElement(By.CssSelector("input[id*='_butPrevious']")).Click();
        }

    }

}

