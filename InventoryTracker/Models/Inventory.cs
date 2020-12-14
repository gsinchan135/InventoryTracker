using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace InventoryTracker.Models
{
    class Inventory
    {
        private List<Item> listOfItems;

        public Inventory()
        {
            listOfItems = new List<Item>();
        }
        public List<Item> GetItems
        {
            get { return listOfItems; }
        }

        public void AddItem(Item item)
        {
            listOfItems.Add(item);
        }
        public void RemoveItem()
        {
           
        }
        public void UpdateItem()
        {

        }
        public string GeneralReport()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine("~-~-~ General Report ~-~-~");
            for(int i = 0; i < listOfItems.Count; i++)
            {
                report.AppendLine("Item Name: " + listOfItems[i].ItemName);
                report.AppendLine("Available Quantity: " + listOfItems[i].AvailableQuantity);
                report.AppendLine("Minimum Quantity: " + listOfItems[i].MinimumQuantity + "\n");
            }

            return report.ToString();
        }
        public string ShoppingList()
        {
            StringBuilder shoppingList = new StringBuilder();
            shoppingList.AppendLine("~-~-~ Shopping List ~-~-~");
            for (int i = 0; i < listOfItems.Count; i++)
            {
                if(listOfItems[i].AvailableQuantity < listOfItems[i].MinimumQuantity)
                {
                    shoppingList.AppendLine("Item Name: " + listOfItems[i].ItemName);
                    shoppingList.AppendLine("Available Quantity: " + listOfItems[i].AvailableQuantity);
                    shoppingList.AppendLine("Minimum Quantity: " + listOfItems[i].MinimumQuantity + "\n");
                }
            }

            return shoppingList.ToString();
        }
        public Inventory LoadData(string fileName)
        {
            listOfItems.Clear();
    
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
                    if (values.Length == Enum.GetValues(typeof(itemData)).Length)
                    {
                        itemName = values[(int)itemData.name];
                        if (!int.TryParse(values[(int)itemData.availableQuantity], out itemQnty))
                            throw new InvalidDataException();

                        if (!int.TryParse(values[(int)itemData.minimumQuantity], out minQnty))
                            throw new InvalidDataException();

                        location = values[(int)itemData.location];
                        supplier = values[(int)itemData.supplier];
                        category = values[(int)itemData.category];

                        anItem = new Item(itemName, itemQnty, minQnty, location, supplier, category);
                        inventory.listOfItems.Add(anItem);
                    }                    
                }
                return inventory;
            }
            return null;
        }
        public void SaveData(string fileName)
        {
            StreamWriter streamWriter = null;
            StringBuilder builder = new StringBuilder();
            for(int i = 0; i < listOfItems.Count; i++)
            {
                builder.Append(listOfItems[i].ItemName + ",");
                builder.Append(listOfItems[i].AvailableQuantity + ",");
                builder.Append(listOfItems[i].MinimumQuantity + ",");
                builder.Append(listOfItems[i].Location + ",");
                builder.Append(listOfItems[i].Supplier + ",");
                builder.AppendLine(listOfItems[i].Category);
            }
            streamWriter = new StreamWriter(fileName, false);
            streamWriter.WriteLine(builder.ToString());
            if(streamWriter != null)
                streamWriter.Close();
        }
    }
}
