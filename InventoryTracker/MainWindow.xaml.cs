using InventoryTracker.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
 
    public partial class MainWindow : Window
    {
        static Inventory inv = new Inventory();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            AddItemWindow addItem = new AddItemWindow();
            addItem.ShowDialog();

            try
            {
                Item anitem = new Item(addItem.newName, addItem.newAvailableQnty, addItem.newMinQnty, addItem.newLocation, addItem.newSupplier, addItem.newCategory);
                inv.AddItem(anitem);
                showInventory.Items.Add(anitem);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            inv.RemoveItem((Item)((((e.OriginalSource as Button).Parent as StackPanel).Parent as Grid).Children[0] as StackPanel).DataContext);
            showInventory.Items.Remove(((((e.OriginalSource as Button).Parent as StackPanel).Parent as Grid).Children[0] as StackPanel).DataContext);
        }


        private void UpdateItem(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            Item oldItem = (Item)((((e.OriginalSource as Button).Parent as StackPanel).Parent as Grid).Children[0] as StackPanel).DataContext;
            UpdateItem update = new UpdateItem(oldItem.ItemName, oldItem.AvailableQuantity, oldItem.MinimumQuantity, oldItem.Location, oldItem.Supplier, oldItem.Category);
            update.ShowDialog();

            try
            {
                Item updatedItem = new Item(update.updatedName, update.updatedQuantity, update.updatedMinQuantity, update.updatedLocation, update.updatedSupplier, update.updatedCategory);
                inv = inv.UpdateItem(oldItem, updatedItem);
                foreach (Item item in showInventory.Items)
                {
                    if (item == oldItem)
                        break;
                    counter++;
                }

                showInventory.Items.Remove(oldItem);
                showInventory.Items.Insert(counter, updatedItem);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void CreateGeneralReport(object sender, RoutedEventArgs e)
        {
            string report = inv.GeneralReport();

            GeneralReport reportWindow = new GeneralReport(report);

            reportWindow.Show();
     
        }
       
        private void CreateShoppingList(object sender, RoutedEventArgs e)
        {
            string sl = inv.ShoppingList();

            ShoppingList shoppingWindow = new ShoppingList(sl);

            shoppingWindow.Show();
        }

        private void LoadData(object sender, RoutedEventArgs e)
        {
            showInventory.Items.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV FILE|*.csv";
            try
            {
                if ((bool)openFileDialog.ShowDialog())
                {
                    inv = inv.LoadData(openFileDialog.FileName);
                    foreach (Item anItem in inv.GetItems)
                    {
                        showInventory.Items.Add(anItem);
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Invalid data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveData(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "CSV File|*.csv"
            };

            try
            {
                if ((bool)dialog.ShowDialog())
                {
                    inv.SaveData(dialog.FileName);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error Saving", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
