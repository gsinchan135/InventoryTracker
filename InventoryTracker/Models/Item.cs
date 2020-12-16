using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryTracker.Models
{
    class Item
    {
        private int _availableQuantity;
        private int _minimumQuantity;
        private string _categoriesLiteral;
        private string _supplier;
        private string _location;
        private string _itemName;

        public Item(string itemName, int availableQuantity, int minimumQuantity, string location, string supplier, string category)
        {
            ItemName = itemName;
            AvailableQuantity = availableQuantity;
            MinimumQuantity = minimumQuantity;
            Location = location;
            Supplier = supplier;
            Category = category;
        }

        //used for enumerating load
        public enum itemData
        {
            name,
            availableQuantity,
            minimumQuantity,
            location,
            supplier,
            category
        };
        
        public enum Categories
        {
            Writing,
            Math,
            Science,
            Sports,
            Textbook,
            Notebook,
            Electronics
        };
        
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (value == "")
                    throw new ArgumentException("Field cannot be empty.", "Item Name.");

                _itemName = value;
            }
        }
        public string Location
        {
            get { return _location; }
            set
            {
                if (value == "")
                    throw new ArgumentException("Field cannot be empty.", "Location.");

                _location = value;
            }
        }
        public string Supplier
        {
            get { return _supplier; }
            set
            {
                if (value == "")
                    throw new ArgumentException("Field cannot be empty.", "Supplier.");

                _supplier = value;
            }
        }
        public string Category
        {
            get { return _categoriesLiteral; }
            set
            {
                if (value == "")
                    throw new ArgumentException("Field cannot be empty.", "Category.");

                _categoriesLiteral = value;
            }
        }

        public int AvailableQuantity
        {
            get { return _availableQuantity; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Field cannot be empty.", "Available Quantity.");

                _availableQuantity = value;
            }
        }

        public int MinimumQuantity
        {
            get { return _minimumQuantity; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Field cannot be empty.", "Minimum Quantity.");

                _minimumQuantity = value;
            }
        }
    }
}
