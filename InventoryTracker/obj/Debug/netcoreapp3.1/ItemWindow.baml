   M S B A M L   `   `   `  ����K  GInventoryTracker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:InventoryTrackerInventoryTracker  5   	   !    InventoryTracker.ItemWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:InventoryTracker     5   	     � Title$  
ItemWindow��5	   	   $	��450��6   $	��400��6)    � Icon$ Images/jac_logo.jpg��5
   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��	Item Name��6   $��10q�6&   .��5      �� -   5         ��Name$
 item��5   "   $��20��6   62   5      �� 5   
   $��Category��6   $��1��6%   $��10q�62   .��5      �� -   5         ��Name$ cmbCategories��5      $��25��6.   5      �� 5   
   $��Location��6   $��2��6%   $��10q�62   .��5      �� -   5        $ location��5   "   $��20��6   66   5       �� 5!   
   $��Supplier��6   $��3��6%   $��10q�62   .��5"      �� -   5        $ supplier��5"   "   $��20��6   66   5#      � 5$   
   $��4��6   $��10q�6    � ColumnDefinitions 5%      �� 5&      $
��Auto �6#   �� 5'      $
��Auto �6#   .+�5)      �� $
��10 0q�6   $	��110��6&   Available Quantity:6G   �� 5*      -   5        $ availableQnty��5*   2   $	��100��6   $��1��6"   6l   5+      � 5,   
   $��5��6   $��10q�6    5-      �� 5.      $
��Auto �6#   �� 5/      $
��Auto �6#   .+�51      �� $
��10 0q�6   $	��110��6&   Minimum Quantity:6E   �� 52      -   5        $ minimumQnty��52   2   $	��100��6   $��1��6"   6G   53      �� 55   
   -   5         ��Name$ saveBtn��55   V   $��6��6   $
��10 0q�6   $
��10 0q�6,   $
��Left=�6;   .��6}   Save6�   56      57      