   M S B A M L   `   `   `  ����C  ?SimplyDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SimplyDBSimplyDB  5   	       SimplyDB.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:SimplyDB     5   	     � Title$  SimplyDB��5   	   $	��800��6   $
��1600��6'   .��5	      ��  �� Padding$ 5q�6   ._�5
   
   �  � RowDefinitions 5      �� 5      $
��auto �6    �� 5      .+�5      � $��1��6    � ColumnDefinitions 5      �� 5      $
��0.6* �6'   �� 5      $��* �6'   �� 5      $��5* �6'   .+�5      �� $��1q�6   $��5q�6-   
���   �$��0��6L   ._�5      �� .+�5      �� -   5         ��Name$ LockingButton��5   3   $��Lock Db��6!   ���#F8CBAD$��2q�6_   6j   �� 5      -   5        $ RefreshButton��5   8   $��Refresh (F5)��6!   ���#F8CBAD$��2,2,2,15q�6d   6v   �� 5      -   5        $ CreateDbButton��5   5   $��	Create DB��6!   ���#C6E0B4$��2q�6b   6m   �� 5      -   5        $ LoadDbButton��5   3   $��Load DB��6!   ���#C6E0B4$��2q�6^   6i   �� 5      -   5        $ BackupDbButton��5   5   $��	Backup DB��6!   ���#C6E0B4$��2q�6b   6m   �� 5       -   5        $ DeleteDbButton��5    5   $��	Delete DB��6!   ���#C6E0B4$��2,2,2,15q�6b   6t   5!      5"      �� 5%      $��1q�6   $��5q�6-   
���   �$��1��6L   ._�5&      �� .+�5(      �� $��DB Name:��6$   $
��Bold"�64   $��Center=�6F   �� 5)      $��1q�6!   $��5q�65   
���   �._�5*      �� -   5         ��Name$ SelectedDbNameTextBlock��5*   (   $��5q�6�   ��6I   ��  �� Path$ MainDbVM.DbName� �� UpdateSourceTrigger$ PropertyChanged=�6�   5+      �� 5-      $��DB Location:��6$   $
��Bold"�68   $��Center=�6J   �� 5.      $��1q�6!   $��5q�65   
���   �._�5/      �� -   5        $! SelectedDbLocationTextBlock��5/   (   $��5q�6�   ��6M   �� $ MainDbVM.DbPath�$ PropertyChanged=�6�   50      �� 52      $��1q�6$   
���   �68   �� 54      $��Tables:��6$   $
��Bold"�63   $��Center=�6E   �� 55      -	   5         ��Name$ tableListBox��55   "   $��Stretch=�68   $��Center=�6V   ��6�   �� $ MainDbVM.TableNames�$ PropertyChanged=��� 56      -
   5        $ CreateTableButton��56   8   $��Create Table��6!   ���#FFF2CC$��2,5,2,2q�6h   6y   �� 57      -   5        $ EditModeButton��57   >   $��Activate Edit Mode��6!   ���#FFF2CC$��2q�6k   6v   �� 58      -   5        $ DeleteTableButton��58   8   $��Delete Table��6!   ���#FFF2CC$��2,2,2,5q�6h   6y   �� 59      $��1q�6$   
���   �68   �� 5;      $��Views:��6$   $
��Bold"�62   $��Center=�6D   �� 5<      -   5        $ viewListBox��5<   "   $��Stretch=�67   $��Center=�6U   ��6�   �� $ MainDbVM.ViewNames�$ PropertyChanged=��� 5=      -   5        $ CreateViewButton��5=   7   $��Create View��6!   ���#FFE699$��2,5,2,5q�6f   6w   �� 5>      -   5        $ DeleteViewButton��5>   7   $��Delete View��6!   ���#FFE699$��2,2,2,5q�6f   6w   5@      5A      �� 5C      $��1q�6   $��5q�6-   
���   �$��2��6L   ._�5D      �  5E      �� 5F      $��* �6,   �� 5G      $��* �6,   .+�5J      �� $��0��6%   .+�5K      �� 	 �� Orientation$	 
Horizontal=�6)   $��5q�6B   .+�5L   "   �� $��Table Name: ��6,   $��5q�6@   $
��Bold"�6K   �� 5M   "   $��1q�6)   
���   �$	��300��6Q   ._�5N   &   �� -   5        
 ��Name$ 
 SelectedTableNameTextBlock��5N   .   $��5q�6R   5O   #   �� 5P   "   -   5        $ tableNameSetButton��5P   T   $	��Set��6)   $��50��67   $
��Bold"�6B   6p   �� 5Q   "   -   5        $ InsertColumn��5Q   _   $��Create Column��6)   $��3q�6A   $
��Bold"�6M   $
��40 0q�6u   6�   5R      � 5V      $	��350��6#    � 	Resources 5W   "      ActionButtons��%%  &	        �� 5X   &   .R�5Y   *   �  5Z   .   �� 5[   2   �� 5\   2   .+�5^   .   �� $ DeleteRowButton��65   -   5        $��Delete��5^   N   $��0��6_   6o   �� 5_   .   $ RenameRowButton��65   -   5        $��Rename��5_   N   $��1��6_   6o   5`   +   5a   '   5b   #   .+�5e   "   &   System.Windows.Controls.DataGrid  -   5          Name$ tableInfoDataGrid��5e   +      SelectionUnit$ FullRow=�6F      SelectionMode$ Single=�6^      CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly . $��20q�6�      AlternatingRowBackground
 �����   AutoGenerateColumns .  ��. $��Visible=�6c  $��2��6�  ��6�   ��    Columns 5f   &   0  *System.Windows.Controls.DataGridTextColumn  5g   *   ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6=      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6K   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6U   �� $
 name�  5h   *   $ 	Data Type��6=   $ * 6P    6Z   �� $
 type�4  .System.Windows.Controls.DataGridCheckBoxColumn  5i   *   $ Primary Key��6A   $
 0.7* 6V    6c   �� $ pk�  5j   *   $ Not NULL��6A   $
 0.7* 6S    6`   �� $ notnull�  5k   *   $ Default Value��6=   $ * 6T    6^   �� $ 	dfltvalue�4  .System.Windows.Controls.DataGridTemplateColumn  5l   *   $ Actions��6A   $
 0.6* 6R      CellTemplate# [  6_   5n   #   5o      5p      �� 5s      $��1��6%   .+�5t      �� $��1q�6%   $��5q�69   
���   �6D   � 5v      $
��10 5q�6#    5w   "   �� 5x   &   $	��100 �67   �� 5y   &   $��* �67   �� 5z   &   $	��120 �67   �� 5{   &   $	��120 �67   .+�5~   "   �� $��Custom Query: ��6,   $��5q�6B   $
��Bold"�6M   $��0��6_   �� 5   "   -   5        $
 customQueryTextBox��5   *   $��Custom Query ��6F   $��2q�6[   $��3q�6f   $��1��6r   �� 5�   "   -   5        $ CustomQuerySubmitButton��5�   :   $��Submit��6)   ���#FFE699$	��8 2q�6p   $��2��6}   $
��Bold"�6�   �� 5�   "   -   5        $ ContentSaveButton��5�   A   $��Apply Changes��6)   ���#EECCCC$	��8 2q�6q   $��3��6~   $
��Bold"�6�   5�      � 5�      $	��300��6#   .+�5�   "     -   5        $ tableContentDataGrid��5�   +    . 
 �����$��25q�6�    . ��. $��Visible=�6�   5�      5�      5�      5�      5�      5�      5�      5�      