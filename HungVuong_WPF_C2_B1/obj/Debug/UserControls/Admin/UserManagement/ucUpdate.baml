   M S B A M L   `   `   `  ����N  JHungVuong_WPF_C2_B1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:HungVuong_WPF_C2_B1HungVuong_WPF_C2_B1  5      "    HungVuong_WPF_C2_B1.ucUpdate   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:HungVuong_WPF_C2_B1     5      
�������.��5
      � ���#F5F7FA  � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� -   5        $��lbMain��5      $��0��6   $��2��6+   $��24(�6�   $
��Bold"�6�   $��80 10 10 10q�5      $��Center=�5      ��5   ?   ��  �� Path$ lbMainContent� �� RelativeSource ��  �� AncestorType"   HungVuong_WPF_C2_B1.ucUpdate"  5      �� 5   
   $��1��6   $��0��6   $��Tài khoản:��6-   $��	10,10,5,5q�6B   �� 5   
   -   5        $��txtUsername��5      $��1��6%   $��1��62   $��	5,10,10,5q�6B   $	��200��6U   ���#CDD4D9$��1q�5   (   $��5q�6<   �� 5   
   $��2��6   $��0��6   $��Mật khẩu:��6-   $��	10,5,5,10q�6A   �� 5    
   -   5        $��txtPassword��5       $��2��6%   $��1��62   $��	5,5,10,10q�6B   $	��200��6U   ���#CDD4D9$��1q�5!   (   $��5q�6<   �� 5#   
   $��3��6   $��1��6"    �� Orientation$ 
Horizontal=�62   $��Center=�6K   $��	0,10,6,10q�6h   .+�5%      �� -   5        $��	btnCancel��5%      $��HỦY��6&   $��
0,10,20,10q�64   $��90��6H   $
��Bold"�6S   ���#EBEDF0
������  � Cursor$
 Hand��5'      ��5(      �� 5)       �� 
TargetType" ��6   .�5*      ��  �� Property � ��	 �� Value	 �#007ACC67   
 �� Triggers
 5+      P� 5,       P� Property �@J�IsMouseOver P� Value . . �5-   "   ��  � ��
	 �����6?   �� 5.   "    � ��	 �#007ACC6?   5/      51      53      �� 55      -   5        $��btnSave��55      $��	0,10,0,10q�6�   $��90��6�   $
��Bold"�6�   ���#007ACC
�������
������ $
 Hand��57      ��55   $   �� $ btnActionContent� �� "  ��58      �� 59      " ��6   
 5:      P� 5;       �@J�IsMouseOver . . �5<   "   ��  � ��	 �#005A8C6?   5=      5?      5A      5B      5C      5D      