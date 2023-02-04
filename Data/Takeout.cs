using System.ComponentModel.DataAnnotations;


namespace AD_CW1_20048632_Amrit_Adhikari_C2.Data
{
    internal class Takeout
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "Please provide the item name.")]  //  This is the line that is causing the error
        public string Itemname { get; set; }
        [Required(ErrorMessage = "Please provide the quantity.")]   //  This is the line that is causing the error
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Please provide the name of the requester.")]  // Added this line

        public Guid ApprovedBy { get; set; }
        public DateTime TakeoutTime { get; set; }
        public string TakenBy { get; set; } 
    }
}
