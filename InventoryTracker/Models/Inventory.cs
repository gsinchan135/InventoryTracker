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
            ItemList = new List<Item>();
        }

        public List<Item> ItemList { get; }

        public void AddItem(Item item)
        {
            ItemList.Add(item);
        }

        public void RemoveItem(Item itemToRemove)
        {
            ItemList.Remove(itemToRemove);
        }

        public void UpdateItem(Item oldItem, Item updatedItem)
        {
            oldItem.ItemName = updatedItem.ItemName;
            oldItem.AvailableQuantity = updatedItem.AvailableQuantity;
            oldItem.MinimumQuantity = updatedItem.MinimumQuantity;
            oldItem.Location = updatedItem.Location;
            oldItem.Supplier = updatedItem.Supplier;
            oldItem.Category = updatedItem.Category;
        }

        public string GeneralReport()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine("~-~-~ General Report ~-~-~");
            for(int i = 0; i < ItemList.Count; i++)
            {
                report.AppendLine("Item Name: " + ItemList[i].ItemName);
                report.AppendLine("Available Quantity: " + ItemList[i].AvailableQuantity);
                report.AppendLine("Minimum Quantity: " + ItemList[i].MinimumQuantity + "\n");
            }

            return report.ToString();
        }

        public string ShoppingList()
        {
            StringBuilder shoppingList = new StringBuilder();
            shoppingList.AppendLine("~-~-~ Shopping List ~-~-~");
            for (int i = 0; i < ItemList.Count; i++)
            {
                if(ItemList[i].AvailableQuantity < ItemList[i].MinimumQuantity)
                {
                    shoppingList.AppendLine("Item Name: " + ItemList[i].ItemName);
                    shoppingList.AppendLine("Available Quantity: " + ItemList[i].AvailableQuantity);
                    shoppingList.AppendLine("Minimum Quantity: " + ItemList[i].MinimumQuantity + "\n");
                }
            }

            return shoppingList.ToString();
        }

        public void LoadData(string fileName)
        {
            ItemList.Clear();
    
            string line;
            string[] values = null;

            string itemName;
            int itemQnty;
            int minQnty;
            string location;
            string supplier;
            string category;


            //File data must come in rows of 6 values, each seperated by a comma(,)
            //specifically: string, int, int, string/int , string, string, string
            if (File.Exists(fileName))
            {
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
                        ItemList.Add(anItem);
                    }                    
                 }
            }
        }

        public void SaveData(string fileName)
        {
            //StreamWriter streamWriter = null;
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < ItemList.Count; i++)
            {
                builder.Append(ItemList[i].ItemName + ",");
                builder.Append(ItemList[i].AvailableQuantity + ",");
                builder.Append(ItemList[i].MinimumQuantity + ",");
                builder.Append(ItemList[i].Location + ",");
                builder.Append(ItemList[i].Supplier + ",");
                builder.AppendLine(ItemList[i].Category);
            }

            using StreamWriter sr = new StreamWriter(fileName, false);
            sr.WriteLine(builder.ToString());
        }
    }
}
