using InventoryTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventoryTracker
{
    /// <summary>
    /// Interaction logic for ItemWindow.xaml
    /// </summary>
    public partial class ItemWindow : Window
    {
        public string theCategory
        {
            get; set;
        }
        public ItemWindow(object obj, object temp,  bool windowType)
        {
            //windowType = true = adding a new item to the inventory
            //windowType = false = updating an item already in the inventory
            InitializeComponent();
            cmbCategories.ItemsSource = Enum.GetValues(typeof(Item.Categories));

            if (windowType)
            {
                ShowDialog();
                Inventory inventory = obj as Inventory;
                
                try
                {
                    Item anitem = new Item(newName, newAvailableQnty, newMinQnty, newLocation, newSupplier, newCategory);

                    inventory.AddItem(anitem);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                Item item = obj as Item;
                Inventory inventory = temp as Inventory;

                newName = item.ItemName;
                newAvailableQnty = item.AvailableQuantity;
                newMinQnty = item.MinimumQuantity;
                newLocation = item.Location;
                newSupplier = item.Supplier;

                object category;
                _ = Enum.TryParse(typeof(Item.Categories), item.Category, out category);

                cmbCategories.SelectedIndex = (int)category;

                ShowDialog();
                Item newItem = new Item(newName, newAvailableQnty, newMinQnty, newLocation, newSupplier, newCategory);

                try
                {
                    inventory.UpdateItem(item, newItem);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private string newName
        {
            get{ return item.Text; }
            set { item.Text = value; }
        }
        
        private string newCategory
        {
            get { return cmbCategories.Text; }
            set { cmbCategories.Text = value; }
        }
        
        private string newLocation
        {
            get { return location.Text; }
            set { location.Text = value; }
        }

        private string newSupplier
        {
            get { return supplier.Text; }
            set { supplier.Text = value; }
        }

        private int newAvailableQnty
        {
            get
            {
                int num;
                bool pass;
                pass = int.TryParse(availableQnty.Text, out num);

                if (pass)
                    return num;
                else
                    return -1;
            }
            set { availableQnty.Text = value.ToString(); }
        }

        private int newMinQnty
        {
            get
            {
                int num;
                bool pass;
                pass = int.TryParse(minimumQnty.Text, out num);

                if (pass)
                    return num;
                else
                    return -1;
            }
            set { minimumQnty.Text = value.ToString(); }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            (((e.OriginalSource as Button).Parent as Grid).Parent as Window).Close();
        }

        private void NumValidation(object sender, TextCompositionEventArgs e)
        {
            Regex reg = new Regex("[^0-9]+");
            e.Handled = reg.IsMatch(e.Text);
        }
    }
}
