// <author>Olga Brozhe.</author>
// <summary>Inventory item object.</summary>

using System.Collections.Generic;

namespace TestWebUI.Models.JsonObjects
{
    public class InventoryItem
    {
        public string InventoryName { get; set; }

        public string Description { get; set; }

        public Dictionary<string, InventoryItemCurrencyAmount>? InventoryItemPrice { get; set; }
    }
}
