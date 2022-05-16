// <author>Olga Brozhe.</author>
//<summary>Tests adding to cart functionality.</summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestWebUI.Helpers;
using WebUITesting.Models;
using WebUITesting.Pages;

namespace TestWebUI.Tests
{
    [TestClass]
    public class AddToCartTests : TestBase
    {
        [TestInitialize]
        public void TestInit()
        {
            LoginToEnvironment();

            // To get test data from Excel.
           // inventoryDataDictionary = ExcelReader.GetExcelTestData(GetExcelInventoryTestDataFilepath(), excelSheet);

            // To get test data from json.
            inventoryDataDictionary = JsonReader.GetJsonTestData(GetJsonInventoryTestDataFilepath());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DriverDispose();
        }

        //private static string excelSheet = "inventory";
        //private IDictionary<int, List<string>> inventoryDataDictionary = new Dictionary<int, List<string>>() { };

        /// <summary>
        /// Tests end-to-end adding to cart.
        /// </summary>
        [TestMethod]
        public void Inventory_AddToCart()
        {
            // Test data from Excel.
            //var itemToAdd = inventoryDataDictionary[0][0];
            //var itemCurrency = inventoryDataDictionary[0][2];
            //var itemAmount = inventoryDataDictionary[0][3];

            // Count the items in the cart.
            PrimaryHeader primaryHeaderBefore = new PrimaryHeader();
            var countBefore = primaryHeaderBefore.GetShoppingCartCounter();
            Log.Info($"There are {countBefore} items in the cart.");

            // From the inventory/ home page add an item to the cart.
            InventoryPage = new AllInventoryPage(Driver);
            Assert.AreEqual("Add To Cart", InventoryPage.WhichBtnDisplayedAddOrRemove(itemToAdd));

            var itemAmountInventory = CommonHelper.GetAmountFromPrice(InventoryPage.GetItemPrice(itemToAdd));
            var itemCurrencyInventory = CommonHelper.GetCurrencyFromPrice(InventoryPage.GetItemPrice(itemToAdd));
            Assert.AreEqual(itemCurrency, itemCurrencyInventory);
            Assert.AreEqual(float.Parse(itemAmount), itemAmountInventory);

            InventoryPage.ClickItemBtn(itemToAdd);
            Assert.AreEqual("Remove", InventoryPage.WhichBtnDisplayedAddOrRemove(itemToAdd));
            Log.Info($"Inventory item '{itemToAdd}' priced {itemCurrencyInventory}{itemAmountInventory} was sent to the cart.");

            // Count the items in the cart again.
            PrimaryHeader primaryHeaderAfter = new PrimaryHeader();
            var countAfter = primaryHeaderAfter.GetShoppingCartCounter();
            Log.Info($"There are {countAfter} items in the cart.");

            Assert.AreEqual(countBefore + 1, countAfter);

            // Check the item in the cart.
            ShoppingCartPage = primaryHeaderAfter.ClickShoppingCart();
            Assert.IsTrue(ShoppingCartPage.IsItemInShoppingCart(itemToAdd));

            var itemAmountCart = CommonHelper.GetAmountFromPrice(ShoppingCartPage.GetItemPrice(itemToAdd));
            var itemCurrencyCart = CommonHelper.GetCurrencyFromPrice(ShoppingCartPage.GetItemPrice(itemToAdd));

            Assert.AreEqual(itemCurrencyInventory, itemCurrencyCart);
            Assert.AreEqual(itemAmountInventory, itemAmountCart);
            Log.Info($"The item '{itemToAdd}' is in the cart, priced {itemCurrencyInventory}{itemAmountInventory}, as expected.");

            // Cleanup.
            ShoppingCartPage.ClickRemoveBtn(itemToAdd);

            Log.Info("The test passed.");
        }
    }
}
