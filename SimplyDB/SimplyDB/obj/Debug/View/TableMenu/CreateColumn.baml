   M S B A M L   `   `   `  ����C  ?SimplyDB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:SimplyDB.View.TableMenuSimplyDB.View.TableMenu  5   	   *    $SimplyDB.View.TableMenu.CreateColumn   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:SimplyDB.View.TableMenu     5   	     � Title$  CreateColumn��5   	   $	��400��6   $	��350��6+   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��0��6!   $��Center=�61   $��Center=�6N   $
��Name��6i   $
��Bold"�6u   $��14(�6�   �� 5   
   -   5         ��Name$ 	NameInput��5   Y   $��0��60   $��1��6=   $��15q�6M   ��6   ��  �� Path$ ColName�6Y   �� 5   
   $��1��6   $��0��6!   $��Center=�61   $��Center=�6N   $��	Data Type��6i   $
��Bold"�6z   $��14(�6�   �� 5   
   -   5         ��Name$ 	TypeInput��5   <   $��1��6   $��1��6    $��15q�60   .@�5      �� $��INTEGER��6   �� 5      $
��REAL��6   �� 5      $
��TEXT��6   �� 5      $
��BLOB��6   5      �� 5!   
   $��2��6   $��0��6!   $��Center=�61   $��Center=�6N   $��Not Null��6i   $
��Bold"�6y   $��14(�6�   �� 5"   
   -   5         ��Name$ NotNullInput��5"   h   $��2��6   $��1��6    $��Center=�60   $��Center=�6M    d� 	IsChecked 6~   �� $ 
ColNotNull��� 5$   
   $��3��6   $��0��6!   $��Center=�61   $��Center=�6N   $��Default Value��6i   $
��Bold"�6~   $��14(�6�   �� 5%   
   -   5        $ DefaultValueInput��5%   ;   $��3��6   $��1��6   $��15q�6/   ��6V   �� $ ColDefaultValue��� 5'   
   -   5         ��Name$ ConfirmButton��5'   @   $��4��6   $��0��6   $��Confirm��6.   $��10q�6W   $
��Bold"�6c   6u   �� 5(   
   -   5        $ CancelButton��5(   ?   $��4��6   $��1��6   $��Cancel��6.   $��10q�6U   $
��Bold"�6a   6s   5)      5*      