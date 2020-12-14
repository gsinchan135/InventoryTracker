using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InventoryTracker.Models
{
    class Inventory
    {
        public Inventory()
        {
            GetItems = new List<Item>();
        }

        public List<Item> GetItems { get; }

        public void AddItem(Item item)
        {
            GetItems.Add(item);
        }

        public void RemoveItem(Item itemToRemove)
        {
            foreach (Item item in GetItems)
            {
                if (item == itemToRemove)
                {
                    GetItems.Remove(item);
                    break;
                }
            }
        }

        public void UpdateItem()
        {

        }

        public string GeneralReport()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine("~-~-~ General Report ~-~-~");
            for(int i = 0; i < GetItems.Count; i++)
            {
                report.AppendLine("Item Name: " + GetItems[i].ItemName);
                report.AppendLine("Available Quantity: " + GetItems[i].AvailableQuantity);
                report.AppendLine("Minimum Quantity: " + GetItems[i].MinimumQuantity + "\n");
            }

            return report.ToString();
        }

        public string ShoppingList()
        {
            StringBuilder shoppingList = new StringBuilder();
            shoppingList.AppendLine("~-~-~ Shopping List ~-~-~");
            for (int i = 0; i < GetItems.Count; i++)
            {
                if(GetItems[i].AvailableQuantity < GetItems[i].MinimumQuantity)
                {
                    shoppingList.AppendLine("Item Name: " + GetItems[i].ItemName);
                    shoppingList.AppendLine("Available Quantity: " + GetItems[i].AvailableQuantity);
                    shoppingList.AppendLine("Minimum Quantity: " + GetItems[i].MinimumQuantity + "\n");
                }
            }

            return shoppingList.ToString();
        }

        public Inventory LoadData(string fileName)
        {
            GetItems.Clear();
    
            string line;
            string[] values = null;

            string itemName;
            int itemQnty;
            int minQnty;
            string location;
            string supplier;
            string category;


            if (File.Exists(fileName))
            {
                 Inventory inventory = new Inventory();
                 Item anItem;
                 StreamReader sr = new StreamReader(fileName);
                 while((line = sr.ReadLine()) != null)
                 {
                    values = line.Split(',');
                    if (values.Length == Enum.GetValues(typeof(Item.itemData)).Length)
                    {
                        itemName = values[(int)Item.itemData.name];
                        if (!int.TryParse(values[(int)Item.itemData.availableQuantity], out itemQnty))
                            throw new InvalidDataException();

                        if (!int.TryParse(values[(int)Item.itemData.minimumQuantity], out minQnty))
                            throw new InvalidDataException();

                        location = values[(int)Item.itemData.location];
                        supplier = values[(int)Item.itemData.supplier];
                        category = values[(int)Item.itemData.category];

                        anItem = new Item(itemName, itemQnty, minQnty, location, supplier, category);
                        inventory.GetItems.Add(anItem);
                    }                    
                 }
                return inventory;
            }
            return null;
        }

        public void SaveData(string fileName)
        {
            //StreamWriter streamWriter = null;
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < GetItems.Count; i++)
            {
                builder.Append(GetItems[i].ItemName + ",");
                builder.Append(GetItems[i].AvailableQuantity + ",");
                builder.Append(GetItems[i].MinimumQuantity + ",");
                builder.Append(GetItems[i].Location + ",");
                builder.Append(GetItems[i].Supplier + ",");
                builder.AppendLine(GetItems[i].Category);
            }

            using StreamWriter sr = new StreamWriter(fileName, false);
            sr.WriteLine(builder.ToString());

            //streamWriter = new StreamWriter(fileName, false);
            //streamWriter.WriteLine(builder.ToString());
            //if(streamWriter != null)
            //    streamWriter.Close();
        }
    }
}
