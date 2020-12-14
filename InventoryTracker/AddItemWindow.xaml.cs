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
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        private bool _saved = false;
        public AddItemWindow()
        {
            InitializeComponent();
        }
        public string newName
        {
            get { return item.Text; }
        }
        public string newCategory
        {
            get { return category.Text; }
        }
        public string newLocation
        {
            get { return location.Text; }
        }
        public string newSupplier
        {
            get { return supplier.Text; }
        }
        public int newAvailableQnty
        {
            get 
            {
                bool pass;
                int num;
                pass = int.TryParse(availableQnty.Text, out num);
                if (pass && num >= 0)
                    return num;

                throw new ArgumentOutOfRangeException("Error. Available Quantity is not a valid number");
            }
        }
        public int newMinQnty
        {
            get 
            {
                bool pass;
                int num;
                pass = int.TryParse(minimumQnty.Text, out num);
                if (pass && num >= 0)
                    return num;

                throw new ArgumentOutOfRangeException("Error. Minimum Quantity is not a valid number");
            }
        }
        public bool saved
        {
            get { return _saved; }
        }
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            _saved = true;
            Application.Current.Windows[1].Close();
        }

        private void numValidation(object sender, TextCompositionEventArgs e)
        {
            Regex reg = new Regex("[^0-9]+");
            e.Handled = reg.IsMatch(e.Text);
        }
    }
}
