using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryTracker.Models
{
    enum itemData
    {
        name,
        availableQuantity,
        minimumQuantity,
        location,
        supplier,
        category
    };
    class Item
    {
        public string ItemName { get; set; }
        private int _availableQuantity;
        private int _minimumQuantity;
        public string Location { get; set; }
        public string Supplier { get; set; }
        public string Category { get; set; }

        public Item(string itemName, int availableQuantity, int minimumQuantity, string location, string supplier, string category)
        {
            ItemName = itemName;
            AvailableQuantity = availableQuantity;
            MinimumQuantity = minimumQuantity;
            Location = location;
            Supplier = supplier;
            Category = category;
        }


        public int AvailableQuantity
        {
            get { return _availableQuantity; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Available Quantity", "Cannot have an available quantity that is negative");

                _availableQuantity = value;
            }
        }

        public int MinimumQuantity
        {
            get { return _minimumQuantity; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Minimuum Quantity", "Cannot have an minimum quantity that is negative");

                _minimumQuantity = value;
            }
        }


    }
}
