using System.Text.Json;

namespace AD_CW1_20048632_Amrit_Adhikari_C2.Data
{
    internal class InventoryService
    {
        
        private static void SaveAll(List<InventoryItem> inventory)  // saveall method to save the inventory
        {
            string appDataDirectoryPath = Utils.GetAppDirectoryPath();  // get the app directory path
            string inventoryFilePath = Utils.GetInventoryFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(inventory);
            File.WriteAllText(inventoryFilePath, json);
        }

        public static List<InventoryItem> GetAll()
        {
            string inventoryFilePath = Utils.GetInventoryFilePath();    // get the inventory file path
            if (!File.Exists(inventoryFilePath))
            {
                return new List<InventoryItem>();
            }

            var json = File.ReadAllText(inventoryFilePath);

            return JsonSerializer.Deserialize<List<InventoryItem>>(json);   // deserialize the json
        }

        public static List<InventoryItem> Create(Guid userId, string itemName, int quantity, string description)    // create method to create the inventory
        {

            List<InventoryItem> inventory = GetAll();   // get all the inventory
            inventory.Add(new InventoryItem
            {
                ItemName = itemName,
                Quantity = quantity,
                Description = description,
                CreatedBy = userId
            });
            SaveAll(inventory);
            return inventory;
        }

        public static bool CheckByName(string name)
        {
            List<InventoryItem> inventory = GetAll();
            InventoryItem inventoryItem = inventory.FirstOrDefault(x => x.ItemName == name);    // check if the item name is already present
            if (inventoryItem == null)
            {
                return false;
            }
            return true;
        }

        public static Guid GetByName(string name)
        {
            List<InventoryItem> inventory = GetAll();
            InventoryItem inventoryItem = inventory.FirstOrDefault(x => x.ItemName == name);    // get the item name
            if (inventoryItem == null)
            {
                throw new Exception("Inventory not found.");
            }
            return inventoryItem.Id;
        }

        public static List<InventoryItem> Delete(Guid id)
        {
            List<InventoryItem> inventory = GetAll();
            InventoryItem inventoryItem = inventory.FirstOrDefault(x => x.Id == id);    // delete the item

            if (inventoryItem == null)
            {
                throw new Exception("Inventory not found.");    // throw exception if the item is not found
            }

            inventory.Remove(inventoryItem);
            SaveAll(inventory);
            return inventory;
        }

        public static void DeleteByUserId(Guid userId)  
        {
            string inventoryFilePath = Utils.GetInventoryFilePath();
            if (File.Exists(inventoryFilePath))
            {
                File.Delete(inventoryFilePath);
            }
        }

        public static List<InventoryItem> AddToOld(Guid userId, Guid id, int quantity)  
        {
            List<InventoryItem> inventory = GetAll();
            InventoryItem inventoryToUpdate = inventory.FirstOrDefault(x => x.Id == id);    

            if (inventoryToUpdate == null)
            {
                throw new Exception("Inventory not found.");
            }

        // Use compound assignment
            inventoryToUpdate.Quantity += quantity;
        // Use compound assignment
            SaveAll(inventory);
            return inventory;
        }

        public static List<InventoryItem> Update(Guid userId, Guid id, string itemName, int quantity, string description)
        {
            List<InventoryItem> inventory = GetAll();
            InventoryItem inventoryToUpdate = inventory.FirstOrDefault(x => x.Id == id);
            

            if (inventoryToUpdate == null)
            {
                throw new Exception("Inventory not found.");
            }


            inventoryToUpdate.ItemName = itemName;
            inventoryToUpdate.Quantity = inventoryToUpdate.Quantity - quantity;
            inventoryToUpdate.Description = description;
            SaveAll(inventory);
            return inventory;
        }

        public static void Update(Guid userId, Guid id, int quantity)
        {
            List<InventoryItem> inventory = GetAll();
            InventoryItem inventoryToUpdate = inventory.FirstOrDefault(x => x.Id == id);

            if (inventoryToUpdate == null)
            {
                throw new Exception("Inventory not found to update.");
            }

            inventoryToUpdate.Quantity = inventoryToUpdate.Quantity - quantity;
            SaveAll(inventory);
        }
    }
}
