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
        Inventory inv = new Inventory();

        public MainWindow()
        {
            InitializeComponent();
            showInventory.ItemsSource = inv.ItemList;
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ItemWindow addItem = new ItemWindow(inv, null,true);
            showInventory.Items.Refresh();
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            inv.RemoveItem((Item)((((e.OriginalSource as Button).Parent as StackPanel).Parent as Grid).Children[0] as StackPanel).DataContext);
            showInventory.Items.Refresh();
        }


        private void UpdateItem(object sender, RoutedEventArgs e)
        {
            Item oldItem = (Item)((((e.OriginalSource as Button).Parent as StackPanel).Parent as Grid).Children[0] as StackPanel).DataContext;
            ItemWindow update = new ItemWindow(oldItem, inv, false);
            showInventory.Items.Refresh();
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
            if(inv.ItemList.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Loading a file will erase all current data. Would you like to save?", "Load data", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        SaveData(sender, e);
                        break;
                    case MessageBoxResult.No:
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV FILE|*.csv";
            try
            {
                if ((bool)openFileDialog.ShowDialog())
                {
                    inv.LoadData(openFileDialog.FileName);
                    showInventory.Items.Refresh();
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

        private void changeQuantity(object sender, RoutedEventArgs e)
        {
            Item item = (Item)(((((e.OriginalSource as Button).Parent as StackPanel).Parent as StackPanel).Parent as Grid).Children[0] as StackPanel).DataContext;

            try
            {
                if (((Button)sender).Name == "increaseItem")
                    item.AvailableQuantity += 1;
                else
                    item.AvailableQuantity -= 1;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Invalid data", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            showInventory.Items.Refresh();
        }
    }
}
