   M S B A M L   `   `   `  ����N  JHungVuong_WPF_C2_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HungVuong_WPF_C2_B1HungVuong_WPF_C2_B1  5   	   .    (HungVuong_WPF_C2_B1.TicketingStaffWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:HungVuong_WPF_C2_B1     5   	   -   5          � Title$  TicketingStaffWindow��5	   	   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   
���   � �� Orientation$ 
Horizontal=�65   .+�5      �� -   5        $��tbHello��5      
�������$
��Bold"�6:   $��20(�6L   $��Center=�6Z   5      �� 5   
   -   5        $��	btnLogout��5      $��Đăng xuất��6"   $��Right=�66   $��Center=�6R   
���   �
�������$	��200��5      6   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5      $��* �6#   .+�5      �� .+�5      �� -   5        $��btnTicketPriceView��5      $��Xem giá vé��63   $
��auto��5      $��80��6&   $��0 0 0 10q�62   �� 5       -   5        $��btnBookingView��5       $��
Đặt vé��6/   $��0 0 0 10q�6@   $
��auto��5!      $��80��6&   5"      �� 5$      $��1��6   
�������$��1q�68   ._�5%      � -   5        $��
grdFeature��5%      5&      5'      5(      5)      5*      