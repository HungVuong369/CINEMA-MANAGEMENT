   M S B A M L   `   `   `  ����N  JHungVuong_WPF_C2_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HungVuong_WPF_C2_B1HungVuong_WPF_C2_B1  5      $    HungVuong_WPF_C2_B1.ucShowtime   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:HungVuong_WPF_C2_B1     5      
�������.��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��Danh sách giờ��6   $��24(�65   $
��Bold"�6C   �� 5   
   -   5        $��btnAdd��5      $��0��6   $��Thêm��6,   $��5q�6;   $��80��6F   $��40��6Q   $��Right=�6]   $��Center=�6y   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��
dgShowtime��5      $��1��6%      AutoGenerateColumns .  $��5q�6N      CanUserAddRows .     
IsReadOnly . $��Stretch=�5      $��Stretch=�6)      SelectionMode$ Single=�5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Tên phim��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *	MovieName+6U     5      $ Thời lượng��6%   $	 100 6U    69   �� *	Runtime+6U     5      $ Loại rạp��6%   $	 100 6V    67   �� *
CinemaType+6V     5      $ Ngày��6%   $	 100 6L    63   �� *Date+6L     5      $ Thời gian��6%   $	 150 6U    68   �� *
Showtime+6U   4  .System.Windows.Controls.DataGridTemplateColumn  5       $ * 6)   	   CellTemplate	 5!      �� 5"      .R�5#      �� 
 �� Orientation$
 
Horizontal=�6)   .+�5$   "   �� $��btnDeleteDate��6)   -   5        $
��Xóa��5$   >   $��5q�6L   $��70��5%   )   $��30��64   5&   )   �� 5'   "   $��	btnUpdate��6)   -   5        $��Sửa��5'   :   $��5q�6H   $��70��5(   )   $��30��64   5)   )   5*      5+      5-      5/      50      51      