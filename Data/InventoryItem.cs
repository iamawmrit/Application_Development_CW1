using System.ComponentModel.DataAnnotations;


namespace AD_CW1_20048632_Amrit_Adhikari_C2.Data
{
    internal class InventoryItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Please provide the item name.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Please provide the quantity.")]   
        public int Quantity { get; set; }

        public string Description { get; set; }
        [Required(ErrorMessage = "Please provide a description.")]  //  Added this line

        public string TakenBy { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
