   M S B A M L   `   `   `  ����N  JHungVuong_WPF_C2_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HungVuong_WPF_C2_B1HungVuong_WPF_C2_B1  5      .    (HungVuong_WPF_C2_B1.ucScheduleManagement   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:HungVuong_WPF_C2_B1     5      
�������.��5
      � $��10q�6     � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ Vertical=�6   .+�5      �� $��Lịch trình��6   $��30(�6)   $
��Bold"�67   $��Center=�6I   6i   5      �� 5   
   -   5        $��spCinema��5      $ 
Horizontal=�6%   $��0��6>   .+�5      �� $��Chọn rạp��6   $��20��6'   $��1q�63   $��0 0 5 0q�6?   6R   �� 5      -   5        $��cbCinema��5      $	��150��6'   $��20��63   5      5      �� 5   
   -   5        $��btnAdd��5      $��0��6   $��Thêm��6,   $��5q�6;   $��80��6F   $��40��6Q   $��Right=�6]   $��Center=�6y   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgScheduleManagement��5      $��1��6/      AutoGenerateColumns .  $��5q�6X      CanUserAddRows .     
IsReadOnly . $��Stretch=�5      $��Stretch=�6)      SelectionMode$ Single=�5          Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn   Header$ Mã phim��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6M   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 66   �� *Id+6M     5$      $ 	Tên phim��6%   $	 150 6P   	 67   �� *Name+6P     5%      $ Thời lượng (phút)��6%   $	 100 6\   	 6@   �� *	Runtime+6\   4  .System.Windows.Controls.DataGridTemplateColumn  5&      $ * 6)   
   CellTemplate
 5'      �� 5(      .R�5)      �� $ 
Horizontal=�6)   .+�5*   "   �� $��btnDeleteSchedule��6)   -   5        $
��Xóa��5*   B   $��5q�6P   $��80��5+   )   $��30��64   5,   )   �� 5-   "   $��	btnDetail��6)   -   5        $��Chi tiết ngày��5-   :   $��5q�6R   $��80��5.   )   $��30��64   5/   )   51      52      54      56      57      58      