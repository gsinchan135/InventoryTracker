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

        //An Item object is passed in to be added to the list of Items inside the inventory class
        public void AddItem(Item item)
        {
            ItemList.Add(item);
        }
        
        //An Item object is passed to have it removed from the ItemList
        public void RemoveItem(Item itemToRemove)
        {
            ItemList.Remove(itemToRemove);
        }

        //Two Item objects are passed in and the first argument has it's properties replaced with the second argument
        public void UpdateItem(Item oldItem, Item updatedItem)
        {
            oldItem.ItemName = updatedItem.ItemName;
            oldItem.AvailableQuantity = updatedItem.AvailableQuantity;
            oldItem.MinimumQuantity = updatedItem.MinimumQuantity;
            oldItem.Location = updatedItem.Location;
            oldItem.Supplier = updatedItem.Supplier;
            oldItem.Category = updatedItem.Category;
        }
        //Loops through the items in the ItemList in order to create a string of all items, their quantity and minimum quantity
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

        //Loops through the items in the ItemList but only adds item to the string builder if the available quantity is below the minimum quantity
        //So that the user knows specifically, which items they need to buy more of.
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

        //If a user does not want to spend time manually adding data each time, they can load the contents of a csv file
        //and if the data is correct, it will be added to the list
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
            //specifically: string, int, int, string, string, string, string
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

        //Once the user has added all the data that they want to, they can save the data to a csv file in order to be loaded again later.
        public void SaveData(string fileName)
        {
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
