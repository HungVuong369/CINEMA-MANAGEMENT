   M S B A M L   `   `   `  ����N  JHungVuong_WPF_C2_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HungVuong_WPF_C2_B1HungVuong_WPF_C2_B1  5      )    #HungVuong_WPF_C2_B1.ucDetailBooking   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:HungVuong_WPF_C2_B1     5      
�������.��5
      � $��20q�6     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0 0 0 20q�6.   .+�5      �� $��Loại rạp:��6   $
��Bold"�6)   $��0 0 5 0q�6;   �� 5      -   5        $��tbCinemaType��5      $��0 0 30 0q�6,   �� 5      $��Ngày:��6   $
��Bold"�6%   $��0 0 5 0q�67   �� 5      -   5        $��tbDate��5      $��0 0 30 0q�6&   �� 5      $��Giờ:��6   $
��Bold"�6$   $��0 0 5 0q�66   �� 5      -   5        $��
tbShowtime��5      $��0 0 30 0q�6*   �� 5      $��Tên khách hàng:��6   $
��Bold"�6/   $��0 0 5 0q�6A   �� 5      -   5        $��tbCustomerName��5      $��0 0 30 0q�6.   �� 5      $��Số điện thoại:��6   $
��Bold"�6.   $��0 0 5 0q�6@   �� 5      -   5        $��tbCustomerPhone��5      $��0 0 0 0q�6/   5      &   System.Windows.Controls.DataGrid  5   
   -   5        $��dgListTicket��5      $��1��6'      AutoGenerateColumns .    
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn   Header$ 
Số ghế��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 300 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *
SeatNumber+6T     5!      $ Số tiền��6%   $	 300 6P    66   �� *Price+6P     5"      $ 
Loại vé��6%    66   �� *Type+5$      �� 5&   
   $ 
Horizontal=�6   $��0 15 0 0q�6.   $��2��6@   $��Center=�6M   .+�5'      �� $��Tổng số tiền: ��6   $
��Bold"�6.   $��20(�6@   $��0 0 5 0q�6N   �� 5(      -   5        $��tbTotalPrice��5(      $��20(�6,   $��0 0 50 0q�6:   �� 5)      $��Tổng số ghế: ��6   $
��Bold"�6-   $��20(�6?   $��0 0 5 0q�6M   �� 5*      -   5        $��tbTotalSeat��5*      $��20(�6+   $��0 0 50 0q�69   �� 5+      -	   5        $��
btnConfirm��5+      $��Xác nhận đặt vé��6'   $��0 0 48 0q�6A   $��19(�6S   $	��200��6a   6m   �� 5,      -
   5        $��	btnCancel��5,      $��Hủy đặt vé��6&   $��19(�6;   $	��200��6I   6U   5-      5.      5/      