using NUnit.Framework;
using SeleniumCSharp.Framework;
using Utils.Core;
using ReportAddin;
using System.Collections.Generic;
using SeleniumCSharp.Pages;
using System;
using OpenQA.Selenium;

namespace SeleniumCSharp.Tests

{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    
    public class DemoTest
    {
        public string NoOfProductsAddedInCart { get; set; }
        public List<String> CCBAddrDetails { get; set; }

        [SetUp]
        public void Setup()
        {
            Logger.LogWriter = TestContext.Out;
            Logger.Name = TestContext.CurrentContext.Test.FullName;
            Logger.mode = LogMode.INFO;
            Report.StartTest(TestContext.CurrentContext.Test.FullName);
            Logger.Info("Setup completed");
        }

        [TearDown]
        public void CleanUp()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            Report.EndTest(testStatus, TestContext.CurrentContext.Test.FullName);
            DriverUtils.QuitDrivers();
        }

        [OneTimeTearDown]
        public void OneTimeStop()
        {
            Report.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void DemoTest1()
        {
            var driver = DriverUtils.GetDriver();

            try
            {
                Data dataObj = new Data("TestCaseData_Authentication_1");
                Logger.Info("Starting Test");
                LoginPage loginPage = new LoginPage(driver);
                // Entering UserName and Password details of login user
                loginPage.Login(dataObj.GetValue("UserName"), dataObj.GetValue("Password"));
                Report.LogPass("Successfully Logged into the Application");

                Header header = new Header(driver);
                //  Search required product from Product Catalog 
                header.SearchProduct(dataObj.GetValue("SearchProduct"));
                Assert.AreEqual(driver.Title, "Results");
                
                Products products = new Products(driver);
                // Select required product from search results based on product type
                string productName = products.SelectProductFromSearchResults(dataObj.GetValue("ProductType"));
                Report.LogPass("Successfully selected product"+productName+" from search results");

                Assert.AreEqual(driver.Title, productName);
                // Enter product quantity
                products.EnterProductQuantity(dataObj.GetValue("ProdQty"));
                Report.LogPass("Entered product quantity in Product page");
                products.ClickAddToCartButton();
                Report.LogPass("Successfully clicked on Add To Cart button in Product page");
                // Verifying that correct no of products added to the cart
                NoOfProductsAddedInCart = header.GetNoOfProductsAddedInCart();
                Assert.AreEqual(dataObj.GetValue("ProdQty"), NoOfProductsAddedInCart, "Incorrect no of products added to the cart");
                header.ClickCartIcon();
                Report.LogPass("Clicked cart icon  available in the header");
                Assert.AreEqual(driver.Title, "My Cart");
                Cart cart = new Cart(driver);
                cart.UpdateProductQuantity(dataObj.GetValue("UpdatedProdQty"));
                Report.LogPass("Updated product quantity in My Cart page");
                cart.ClickUpdateTotalsButton();
                Report.LogPass("Clicked Updated Totals button in My Cart page");
                NoOfProductsAddedInCart = header.GetNoOfProductsAddedInCart();
                Assert.AreEqual(dataObj.GetValue("UpdatedProdQty"), NoOfProductsAddedInCart, "Incorrect no of products added to the cart");
                Report.LogPass("Verified that no of products added in the cart is same as updated quantity of product");
                cart.ClickCheckOutButton();
                Report.LogPass("Clicked Checkout button in My Cart page");
                Assert.AreEqual(driver.Title, "Checkout");
                Agreements agreements = new Agreements(driver);
                agreements.SelectAgreeButtonInAgreementsPage();
                Report.LogPass("Clicked agree check box in Agreements page");
                Assert.IsTrue(agreements.IsPrintsthisPageLinkDsiaplyed());
                Report.LogPass("Verified Print this page link is available in Agreements page");
                agreements.ClickContinueButtonInAgreementsPage();
                Report.LogPass("Clicked continue button in Agreements page");
                Shipping shipping = new Shipping(driver);
                List<String> receipientDetails = shipping.GetRecepientFieldsValuesInShippingPage();
                Assert.AreEqual(receipientDetails[0].ToString(), dataObj.GetValue("Fname"));
                Assert.AreEqual(receipientDetails[1].ToString(), dataObj.GetValue("Lname"));
                Report.LogPass("Verified Receipient fields has current user's first name and last name values in Shipping page");
                Assert.IsTrue(shipping.IsShippingSpeedCheckBoxSelected());
                Report.LogPass("Verified shipping check box is checked in Shipping page");
                List<String> shippingAddrsDetails = shipping.GetShippingAddressValues();
                Report.LogPass("Collected all the shipping address values from shipping page");
                string orgEmailValueInShippingPage = shipping.GetOrgEmailAddressValue();
                shipping.ClickChangeEmailButton();
                Report.LogPass("Clicked Change Email button in Shipping page");
                OrgDetails orgDetails = new OrgDetails(driver);
                List<String> orgEmail = orgDetails.getOrgDetailsInOrgDetailsPage();
                Report.LogPass("Collected all the organization details from Organization Information page");
                Assert.AreEqual(orgEmailValueInShippingPage, orgEmail[0]);
                Assert.AreEqual(shippingAddrsDetails[0].ToString(), orgEmail[1]);
                Assert.AreEqual(shippingAddrsDetails[1].ToString(), orgEmail[2]);
                Assert.AreEqual(shippingAddrsDetails[2].ToString(), "US");
                Assert.AreEqual(shippingAddrsDetails[3].ToString(), "IN");
                Assert.AreEqual(shippingAddrsDetails[4].ToString().Trim(), orgEmail[3]);
                Assert.AreEqual(shippingAddrsDetails[5].ToString().Trim(), orgEmail[4]);
                Report.LogPass("verified that orgnization address details are matched with shipping address details in shipping page");
                orgDetails.ClickCancelButtonInOrgDetailsPage();
                Report.LogPass("Clicked cancel buton in organization information page");
                Report.LogPass("Clicked agree check box in Agreements page");
                cart.ClickCheckOutButton();
                agreements.SelectAgreeButtonInAgreementsPage();
                agreements.ClickContinueButtonInAgreementsPage();
                shipping.ClickContinueButtonInShippingPage();
                Report.LogPass("Navigated to the Billing page");
                Billing billing = new Billing(driver);

                List<String> calcTotalAmntOfeachProduct = billing.CalculateTotalAmountOfEachProductInBillingPage();
                List<String> getTotalAmntOfeachProd = billing.GetTotalAmountOfEachProductInBillingPage();
                for (int i = 0; i < getTotalAmntOfeachProd.Count; i++)
                {
                    Assert.AreEqual(calcTotalAmntOfeachProduct[i], getTotalAmntOfeachProd[i]);
                }
                Report.LogPass("Verified total amount of each product is correct in Billing page");
                String actualSubTotal = billing.CalculateSubTotalAmountInBillingPage(getTotalAmntOfeachProd);
                String expectedSubTotal = billing.GetSubTotalAmountInBillingPage();
                Assert.AreEqual(expectedSubTotal, actualSubTotal);
                Report.LogPass("Verified sub total amount is correct in Billing page");
                String actualTotal = billing.CalculateTotalDueAmountInBillingPage();
                String expectedTotal = billing.GetTotalDueAmountInBillingPage();
                Assert.AreEqual(expectedTotal, actualTotal);
                Report.LogPass("Verified totalDue amount is correct in Billing page");
                billing.SelectCreditCardPaymentMethod();
                Report.LogPass("Selected credit card payment method in Billing page");
                List<String> CCBAddrEditMode = billing.IsCreditCardBillingAddressDetailsDisabledInBillingPage();
                for (int i = 0; i < CCBAddrEditMode.Count; i++)
                {
                    Assert.IsNull(CCBAddrEditMode[i]);
                }

                Report.LogPass("Verified Credit card billing address details are enabled in Billing page when Credit card payment method is selected");

                List<String> CCBDetailsEditMode = billing.IsCreditCardDetailsDisabledInBillingPage();
                for (int i = 0; i < CCBDetailsEditMode.Count; i++)
                {
                    Assert.IsNull(CCBDetailsEditMode[i]);
                }

                Report.LogPass("Verified Credit card billing details are enabled in Billing page when Credit card payment method is selected");

                billing.SelectCheckPaymentMethod();
                Report.LogPass("Selected check payment method in Billing page");
                List<String> CCBAddrDisabledMode = billing.IsCreditCardBillingAddressDetailsDisabledInBillingPage();
                for (int i = 0; i < CCBAddrDisabledMode.Count; i++)
                {
                    Assert.IsNotNull(CCBAddrDisabledMode[i]);
                }

                Report.LogPass("Verified Credit card billing address details are disabled in Billing page when check payment method is selected");

                List<String> CCBDetailsDisabledMode = billing.IsCreditCardDetailsDisabledInBillingPage();
                for (int i = 0; i < CCBDetailsDisabledMode.Count; i++)
                {
                    Assert.IsNotNull(CCBDetailsDisabledMode[i]);
                }
                Report.LogPass("Verified Credit card billing details are disabled in Billing page when check payment method is selected");

                IList<IWebElement> CCTypeOptions = billing.GetAllCardTypeOptionsInBillingPage();
                Assert.AreEqual("MasterCard", CCTypeOptions[0].Text);
                Assert.AreEqual("Visa", CCTypeOptions[1].Text);
                Assert.AreEqual("Amex", CCTypeOptions[2].Text);
                Report.LogPass("Verified Credit card type options has only 'MasterCard', 'Visa', 'Amex' values in Billing page");

                billing.SelectCreditCardPaymentMethod();
                Report.LogPass("Selected credit card payment method is selected");
                string actualValidationMsg = billing.ValidationMsgOnInvalidCCNumberInBillingPage(dataObj.GetValue("InValidCCNumber"), dataObj.GetValue("CCType"));
                Report.LogPass("Entered invalid credit card number");
                Assert.AreEqual("Please enter a valid credit card number.", actualValidationMsg);
                Report.LogPass("Verified validation message is getting displayed when invalid credit card number is entered");

                billing.EnterCardNumberInBillingPage(dataObj.GetValue("ValidCCNumber"));
                billing.SelectCCExpirationMonth(dataObj.GetValue("CCExpMonth"));
                Report.LogPass("Enter valid credit card number and expired month details in Billing page");
                IList<IWebElement> actualCCExpiryDetails = billing.GetCCExpirationYearDetails();
                for (int i = 1; i < actualCCExpiryDetails.Count; i++)
                {

                    Assert.Greater((Int32.Parse(actualCCExpiryDetails[i].Text)), DateTime.Now.Year);
                }

                Report.LogPass("Verified all the values available in credit card expired year field are greater than current year");
                billing.SelectSameAsShippingAddress();
                Report.LogPass("Selected  SameAsShipping address check box in Billing page");
                CCBAddrDetails = billing.GetCCBAddressValuesInBillingPage();
                Assert.AreEqual("Test", CCBAddrDetails[0]);
                Assert.AreEqual("User", CCBAddrDetails[1]);
                Assert.AreEqual("dddd", CCBAddrDetails[2]);
                Assert.AreEqual("ffff", CCBAddrDetails[3]);
                Assert.AreEqual("US", CCBAddrDetails[4]);
                Assert.AreEqual("IN", CCBAddrDetails[5]);
                Assert.AreEqual("dddd", CCBAddrDetails[6].Trim());
                Assert.AreEqual("45123", CCBAddrDetails[7].Trim());
                Report.LogPass("Verified all the credit card billing address values are populated automatically when SameAsShipping Address check box is selected");

                billing.SelectSameAsShippingAddress();
                billing.EnterCCBAddrDetailsInBillingPage(CCBAddrDetails);
                billing.clickPlaceRequestButton();
                Report.LogPass("Clicked on Place Request button in Billing page");

                OrderConfirmation ordConfirm = new OrderConfirmation(driver);
                ordConfirm.clickProdDonationFAQLinkInOrderConfirmationPage();
                Report.LogPass("Clicked on Product Donation History Link");
                Assert.AreEqual("Product Donation FAQ", driver.Title);
                driver.Navigate().Back();
                ordConfirm.clickDonationHistoryLinkInOrderConfirmationPage();
                Report.LogPass("Clicked on Donation Request History Link"); 
                Assert.AreEqual("Donation Request History", driver.Title);
                driver.Navigate().Back();
                List<String> emails = ordConfirm.getConfirmationEmailValueInOrderConfirmationPage();
                Assert.AreEqual(dataObj.GetValue("Memail"), emails[0].ToString());
                Assert.AreEqual(dataObj.GetValue("Oemail"), emails[1].ToString());
                Report.LogPass("Verified confirmation and delivery instruction email fields has values");
                Assert.IsNotEmpty(ordConfirm.getRequestNumberInOrderConfirmationPage());
                Report.LogPass("Verified Request Number field has value");
                Assert.AreEqual(actualTotal, ordConfirm.getTotalDueAmountInOrderConfirmationPage());
                Report.LogPass("verified total due amount in Order Confirmation page is same as Billing page");
                List<String> reqDetails = ordConfirm.getRequestDetailsInOrderConfirmationPage();
                for (int i = 0; i < reqDetails.Count; i++)
                {
                    Assert.IsNotEmpty(reqDetails[i]);
                }

                Report.LogPass("Verified request details fields has values in Order Confirmation page");
            }
            catch (Exception e)
            {
                Report.LogException(e);
                driver.SaveScreenshotAndPageSource();
                throw e;
            }

        }

        [Test]
        public void DemoTest2()
        {
            var driver = DriverUtils.GetDriver();
            try
            {

                Data dataObj = new Data("TestCaseData_Authentication_1");
                Logger.Info("Starting Test");
                LoginPage loginPage = new LoginPage(driver);
                loginPage.Login(dataObj.GetValue("UserName"), dataObj.GetValue("Password"));
                Header header = new Header(driver);
                header.SearchProduct(dataObj.GetValue("SearchProduct"));
                Assert.AreEqual(driver.Title, "Results");
                Products products = new Products(driver);
                string productName = products.SelectProductFromSearchResults("2");
                Assert.AreEqual(driver.Title, productName);
                products.EnterProductQuantity(dataObj.GetValue("ProdQty"));
                products.ClickAddToCartButton();
                NoOfProductsAddedInCart = header.GetNoOfProductsAddedInCart();
                Assert.AreEqual(dataObj.GetValue("ProdQty"), NoOfProductsAddedInCart, "Incorrect no of products added to the cart");
                header.ClickCartIcon();
                Assert.AreEqual(driver.Title, "My Cart");
                Cart cart = new Cart(driver);
                cart.UpdateProductQuantity(dataObj.GetValue("UpdatedProdQty"));
                cart.ClickUpdateTotalsButton();
                String validationMsg = cart.ValidateProductLimitExceedMessage();
                Assert.AreEqual("* You can order a maximum of 4 Adobe items in a fiscal year. You have already ordered 2 and can only order 2 more.", validationMsg);
                Assert.IsTrue(cart.IsCheckOutButtonDisabled());
                cart.ClickProductRemoveLink();
                Assert.AreEqual("You do not have any items in your cart. You must add products to your cart before completing checkout.", cart.GetValidationMsgWhenNoProdIsAdded());
                header.SearchProduct(dataObj.GetValue("SearchProduct"));
                Assert.AreEqual(driver.Title, "Results");
                products.SelectProductFromSearchResults("2");
                Assert.AreEqual(driver.Title, productName);
                products.EnterProductQuantity(dataObj.GetValue("ProdQty"));
                products.ClickAddToCartButton();
                NoOfProductsAddedInCart = header.GetNoOfProductsAddedInCart();
                Assert.AreEqual(dataObj.GetValue("ProdQty"), NoOfProductsAddedInCart, "Incorrect no of products added to the cart");
                header.ClickCartIcon();
                Assert.AreEqual(driver.Title, "My Cart");
                cart.ClickClearCartButton();
                Assert.AreEqual("You do not have any items in your cart. You must add products to your cart before completing checkout.", cart.GetValidationMsgWhenNoProdIsAdded());
            }

            catch (Exception e)
            {
                driver.SaveScreenshotAndPageSource();
                throw e;
            }
        }

    }
}
