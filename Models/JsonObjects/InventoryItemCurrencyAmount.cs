// <author>Olga Brozhe.</author>
// <summary>Inventory item price object.</summary>

namespace TestWebUI.Models.JsonObjects
{
    public class InventoryItemCurrencyAmount : InventoryItem
    {
        public string Currency { get; set; }
        public float Amount { get; set; }
    }
}
