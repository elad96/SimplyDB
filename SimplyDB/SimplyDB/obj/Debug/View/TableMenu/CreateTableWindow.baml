   M S B A M L   `   `   `  ����C  ?SimplyDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:SimplyDB.View.TableMenuSimplyDB.View.TableMenu  5   	   *    $SimplyDB.TableMenu.CreateTableWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:SimplyDB.View.TableMenu     5   	     � Title$  CreateTableWindow��5   	   $	��450��6#   $	��800��60   .��5	      �  � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��3* �6   �� 5      $��* �6   .+�5   
   � $��0��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��2* �6#   .+�5      �� $��Choose Table Name:��6   $
��Bold"�62   $��0��6D   $��0��6T   $��Center=�6a   $��Center=�6|   �� 5      -   5         ��Name$ tableNameTextBox��5      $��Enter Table Name Here��60   $��0��6M   $��1��6Z   $��20,25q�6j   $��3q�6y   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ createTableDataGrid��5         CanUserAddRows .    CanUserDeleteRows . $��25q�6_   $��1��6k      AlternatingRowBackground
 �����   AutoGenerateColumns .  ��. $��Visible=�6  	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$

 Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   ��  �� Path$
 name�4  .System.Windows.Controls.DataGridComboBoxColumn  5      $
 	Data Type��6)   $ * 6<      SelectedValueBinding 6F   �� $
 type�   ElementStyle 5      �� 5        �� 
TargetType" ��6    .�5!      ��  �� Property � �� �� Value 6<   �� $ DataContext.ComboTypes� �� RelativeSource ��  �� AncestorType" �5"         EditingElementStyle 5$      �� 5%      " ��6    .�5&      ��  � �� 6<   �� $ DataContext.ComboTypes� �� " �5'      5)      4  .System.Windows.Controls.DataGridCheckBoxColumn  5+      $
 Primary Key��6)   $
 0.7* 6>    6K   �� $ pk�  5,      $
 Not NULL��6)   $
 0.7* 6;    6H   �� $ notnull�  5-      $
 Default Value��6%   $ * 6<    6F   �� $ 	dfltvalue�5/      � 51   
   $��2��6    52      �� 53      �� 54      .+�56      �� -   5         ��Name$ TableCreateConfirmButton��56   '   $��Confirm��6   $��0��6p   $��60,15q�6�   �� 57      -   5        $ TableCreateCancelButton��57   &   $��Cancel��6   $��1��6m   $��60,15q�6}   58      59      5;      