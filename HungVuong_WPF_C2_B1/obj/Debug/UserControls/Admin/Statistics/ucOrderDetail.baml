   M S B A M L   `   `   `  ����N  JHungVuong_WPF_C2_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HungVuong_WPF_C2_B1HungVuong_WPF_C2_B1  5      '    !HungVuong_WPF_C2_B1.ucOrderDetail   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:HungVuong_WPF_C2_B1     5      
�������.��5
      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   ��  �� Orientation$ Vertical=�6   .+�5      �� $��Center=�6   $��20(�5      $
��Bold"�5      .��6&   
Chi tiết60   �� 5      -   5        $��spSeats��5      5      5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgOrderDetail��5       $��1��6   $��10q�65      AutoGenerateColumns .     SelectionMode$ Single=�5   /      SelectionUnit$ FullRow=�6F      
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Số ghế��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6T   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 65   �� *
SeatNumber+6T     5      $ Loại khách��6%   $	 100 6R   	 69   �� *Type+6R     5      $ Số tiền��6%   $	 100 6P   	 66   �� *Price+6P   5       5!      5"      