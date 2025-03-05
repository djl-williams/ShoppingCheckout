using System;
using System.Collections.Generic;
using ShoppingCheckout.Core;

namespace CheckoutTests
{
    [TestClass]
    public class CheckoutTests
    {
        private List<Item>? _items;
        private List<SpecialOffer>? _offers;
        private Checkout? _checkout;

        [TestInitialize]
        public void Setup()
        {
            _items = new List<Item>
            {
                new Item { SKU = "A99", UnitPrice = 0.50m },
                new Item { SKU = "B15", UnitPrice = 0.30m },
                new Item { SKU = "C40", UnitPrice = 0.60m }
            };

            _offers = new List<SpecialOffer>
            {
                new SpecialOffer { SKU = "A99", Quantity = 3, OfferPrice = 1.30m },
                new SpecialOffer { SKU = "B15", Quantity = 2, OfferPrice = 0.45m }
            };

            
            _checkout = new Checkout(_items, _offers);
        }

        [TestMethod]
        public void SingleItemReturnsCorrectPrice()
        {
            _checkout.Scan("A99");

            decimal total = _checkout.CalculateTotal();

            Assert.AreEqual(0.50m, total);
        }

        [TestMethod]
        public void MultipleItemsReturnCorrectPrice()
        {
            _checkout.Scan("A99");
            _checkout.Scan("B15");
            _checkout.Scan("C40");

            decimal total = _checkout.CalculateTotal();

            // 0.50 + 0.30 + 0.60 = 1.40
            Assert.AreEqual(1.40m, total);
        }

        [TestMethod]
        public void ItemsWithOfferReturnsDiscountedPrice()
        {
            _checkout.Scan("A99");
            _checkout.Scan("A99");
            _checkout.Scan("A99");

            decimal total = _checkout.CalculateTotal();

            Assert.AreEqual(1.30m, total);
        }

        [TestMethod]
        public void BiscuitAppleBiscuitReturnsCorrectPrice()
        {
            _checkout.Scan("B15");  // First pack of Biscuits
            _checkout.Scan("A99");  // Apple
            _checkout.Scan("B15");  // Second pack of Biscuits

            decimal total = _checkout.CalculateTotal();

            Assert.AreEqual(0.95m, total);
        }

    }
}