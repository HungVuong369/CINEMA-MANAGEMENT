   M S B A M L   `   `   `  ����N  JHungVuong_WPF_C2_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HungVuong_WPF_C2_B1HungVuong_WPF_C2_B1  5      *    $HungVuong_WPF_C2_B1.ucUserManagement   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:HungVuong_WPF_C2_B1     5      ���#F5F5F5.��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��Danh sách tài khoản��6   $��24(�6;   $
��Bold"�6I   �� 5   
   -   5        $��btnAdd��5      $��0��6   $��Thêm��6,   $��5q�6;   $��80��6F   $��40��6Q   $��Right=�6]   $��Center=�6y   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgUserManagement��5      $��1��6+      AutoGenerateColumns .  $��5q�6T      CanUserAddRows .     
IsReadOnly . $��Stretch=�5      $��Stretch=�6)      SelectionMode$ Single=�5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Số thứ tự��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *Index+6R     5      $ Tên tài khoản��6%   $	 150 6Y    6<   �� *
Username+6Y     5      $ Mật khẩu��6%   $	 100 6T    67   �� *
Password+6T     5      $ Vai trò��6%   $	 100 6S    66   �� *
Priority+6S   4  .System.Windows.Controls.DataGridTemplateColumn  5      $ * 6)   	   CellTemplate	 5       �� 5!      .R�5"      �� 
 �� Orientation$
 
Horizontal=�6)   .+�5#   "   �� $��	btnDelete��6)   -   5        $
��Xóa��5#   :   $��5q�6H   $��60��5$   )   $��30��64   5%   )   �� 5&   "   $��	btnUpdate��6)   -   5        $��Sửa��5&   :   $��5q�6H   $��60��5'   )   $��30��64   5(   )   5)      5*      5,      5.      5/      50      