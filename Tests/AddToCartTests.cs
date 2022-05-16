﻿// <author>Olga Brozhe.</author>
//<summary>Tests adding to cart functionality.</summary>

using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DriverDispose();
        }

        /// <summary>
        /// Tests end-to-end adding to cart.
        /// </summary>
        [TestMethod]
        public void Inventory_AddToCart()
        {
            var itemToAdd = "Sauce Labs Backpack";

            // Count the items in the cart.
            PrimaryHeader primaryHeaderBefore = new PrimaryHeader();
            var countBefore = primaryHeaderBefore.GetShoppingCartCounter();
            Log.Info($"There are {countBefore} items in the cart.");

            // From the inventory/ home page add an item to the cart.
            InventoryPage = new InventoryPage(Driver);
            Assert.AreEqual("Add To Cart", InventoryPage.WhichBtnDisplayedAddOrRemove(itemToAdd));

            var itemAmountInventory = CommonHelper.GetAmountFromPrice(InventoryPage.GetItemPrice(itemToAdd));
            var itemCurrencyInventory = CommonHelper.GetCurrencyFromPrice(InventoryPage.GetItemPrice(itemToAdd));

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
