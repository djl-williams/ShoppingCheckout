namespace ShoppingCheckout.Core
{
    public class Checkout
    {
        private readonly List<Item> _items;
        private readonly List<SpecialOffer> _offers;
        private readonly Dictionary<string, int> _scannedItems;

        public Checkout(List<Item> items, List<SpecialOffer> offers)
        {
            _items = items;
            _offers = offers;
            _scannedItems = new Dictionary<string, int>();
        }

        public void Scan(string sku)
        {
            if (_scannedItems.ContainsKey(sku))
                _scannedItems[sku]++;
            else
                _scannedItems[sku] = 1;
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;

            foreach (var scannedItem in _scannedItems)
            {
                string sku = scannedItem.Key;
                int quantity = scannedItem.Value;

                // Find the item
                var item = _items.FirstOrDefault(i => i.SKU == sku);
                if (item == null) continue;

                // Find if there's an offer for this item
                var offer = _offers.FirstOrDefault(o => o.SKU == sku);

                if (offer != null)
                {
                    // Calculate how many complete offers can be applied
                    int offerCount = quantity / offer.Quantity;

                    // Calculate remaining items not covered by the offer
                    int remainingItems = quantity % offer.Quantity;

                    // Apply offer price for complete offers
                    total += offerCount * offer.OfferPrice;

                    // Apply regular price for remaining items
                    total += remainingItems * item.UnitPrice;
                }
                else
                {
                    // No offer, apply regular price
                    total += quantity * item.UnitPrice;
                }
            }

            return total;
        }
    }
}