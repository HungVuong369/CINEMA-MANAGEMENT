   M S B A M L   `   `   `  ����N  JHungVuong_WPF_C2_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HungVuong_WPF_C2_B1HungVuong_WPF_C2_B1  5      &     HungVuong_WPF_C2_B1.ucStatistics   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:HungVuong_WPF_C2_B1     5      
�������.��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   ��  �� Orientation$ Vertical=�6   .+�5      �� $��Center=�6   $��20(�5      $
��Bold"�5      .��6&   Thống kê62   �� 5      $��0��6   $ 
Horizontal=�6&   $��10q�6?   .+�5      �� $��Chọn loại rạp:��6   $��Center=�62   $��0 0 5 0q�6M   �� 5      -   5        $��cbCinema��5      $	��150��6+    �� SelectedIndex$ 0��67   5      �� 5      $��Chọn ngày:��6   $��Center=�6.   $��20 0 5 0q�6I   (  "System.Windows.Controls.DatePicker  5      -   5        $��
DatePicker��5      $	��150��6/   6;   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgOrder��5       $��1��6   $��10q�6/      AutoGenerateColumns .     SelectionMode$ Single=�5   /      SelectionUnit$ FullRow=�6F      
IsReadOnly .    Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Header$ Tên khách hàng��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 100 6^   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6=   �� *CustomerName+6^     5"      $ Số điện thoại��6%   $		 100 6^   
 6<   �� *CustomerPhone+6^     5#      $ Loại rạp��6%   $		 100 6V   
 67   �� *
CinemaType+6V     5$      $ Ngày��6%   $		 100 6L   
 63   �� *Date+6L     5%      $ Thời gian��6%   $		 100 6U   
 68   �� *
Showtime+6U     5&      $ Tổng số ghế��6%   $		 100 6X   
 6:   �� *	TotalSeat+6X     5'      $ Tổng số tiền��6%   $		 100 6Z   
 6;   �� *
TotalPrice+6Z   4  .System.Windows.Controls.DataGridTemplateColumn  5)      $	 * 6)      CellTemplate 5*      �� 5+      .R�5,      �� $ 
Horizontal=�6)   .+�5-   "   �� $��	btnDetail��6)   -   5        $��
Chi tiết��5-   :   $��5q�6M   $��60��5.   )   $��30��64   5/   )   50      51      53      55      56      57      