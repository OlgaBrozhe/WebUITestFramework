// <author>Olga Brozhe.</author>
// <summary>Inventory item object.</summary>

namespace TestWebUI.Models.JsonObjects
{
    public class InventoryItemFromJson
    {
        public string InventoryName { get; set; }

        public string Description { get; set; }

        public string PriceCurrency { get; set; }

        public float PriceAmount { get; set; }
    }
}
