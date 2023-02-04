
using System.Text.Json;

namespace AD_CW1_20048632_Amrit_Adhikari_C2.Data
{
    internal class TakeoutService   //this is a class that is used to store the data of the takeout
    {
       
        private static void SaveAll(List<Takeout> takeouts) 
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath(); //   GetAppDirectoryPath() method is in Utils.cs
            string appTakeoutsFilePath = Utils.GetTakeoutsFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(takeouts);  //  Serialize() method is in System.Text.Json
            File.WriteAllText(appTakeoutsFilePath, json);
        }

        public static List<Takeout> GetAll()
        {
            string appTakeoutsFilePath = Utils.GetTakeoutsFilePath();   //  GetTakeoutsFilePath() method is in Utils.cs
            if (!File.Exists(appTakeoutsFilePath))
            {
                return new List<Takeout>();
            }

            var json = File.ReadAllText(appTakeoutsFilePath);

            return JsonSerializer.Deserialize<List<Takeout>>(json);
        }

        public static List<Takeout> Create(Guid userId, string itemname, int quantity, string takenBy)      
        
        {
            List<Takeout> takeout = GetAll();   //  GetAll() method is in TakeoutService.cs

            takeout.Add(
                new Takeout
                {
                    Itemname = itemname,
                    Quantity = quantity,
                    ApprovedBy = userId,
                    TakenBy = takenBy,
                    TakeoutTime = DateTime.Now
                }
            );
            SaveAll(takeout);
            return takeout;
            
            
        }

        public static List<Takeout> Update(Guid userId, Guid id)
        {

            List<Takeout> takeout = GetAll();
            foreach (Takeout item in takeout)
            {
                if (item.Id == id)
                {
                    item.ApprovedBy = userId;
                    item.TakeoutTime = DateTime.Now;
                }
            }
            SaveAll(takeout);
            return takeout;
        }

        public static List<Takeout> Delete(Guid id)

        {
            List<Takeout> takeout = GetAll();
            Takeout takeoutIem = takeout.FirstOrDefault(x => x.Id == id);   //  GetTakeoutsFilePath() method is in Utils.cs

            if (takeoutIem == null)
            {
                throw new Exception("Takeout not found.");
            }

            takeout.Remove(takeoutIem);
            SaveAll(takeout);
            return takeout;
        }
        public static List<Takeout> getBarData()
        {
            List<Takeout> items = GetAll();

            List<Takeout> filteredItems = new List<Takeout>();

            foreach (Takeout inventoryItem in items)
            {
                Takeout itemToUpdate = filteredItems.FirstOrDefault(x => x.Itemname == inventoryItem.Itemname);

                if (itemToUpdate == null)
                {
                    filteredItems.Add(itemToUpdate);
                }
                else
                {
                    itemToUpdate.Quantity++;
                }

            }

            return filteredItems;
        }

    }
}
