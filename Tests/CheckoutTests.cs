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
        public void Scan_SingleItem_ReturnsCorrectPrice()
        {
            _checkout.Scan("A99");

            decimal total = _checkout.CalculateTotal();

            Assert.AreEqual(0.50m, total);
        }
    }
}