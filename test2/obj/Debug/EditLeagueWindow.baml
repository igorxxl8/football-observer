   M S B A M L   `   `   `  ����  FootballManager&     FootballManager.EditLeagueWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Просмотр лиги��5   	   $	��420��6   $	��600��6,   $	��400��5   	   $	��500��6   .��5      � -   5         �Name$ Ed��5       � ColumnDefinitions 5   
   �� 5	      $��0.45* �6   6/   �� 5
      $��0.55* �6   6/   .+�5   
   �  � RowDefinitions 5      �� 5      $��0.145* �6    62   �� 5      $��0.855* �6    62   .+�5      �� -   5         ��Name$ 
LogoChange��5      $y�Hidden=�6D   ��6*   �� *
LogoPath+6[   �� 5      -   5         ��Name$ LogoBut��5      {�.  $��
10,20,10,0q�68   .��6b   Изменить фото6q   �� 5      -   5         ��Name$
 Logo��5      $��1��6"   $��10q�6d   ��6/   ��  �� ElementName$ 
LogoChange�� �� Path$
 Text�6r   5      � 5   
   $��1��6    5      �� 5      $
��0.1* �6    60   �� 5      $
��0.1* �6    60   �� 5      $
��0.1* �6    60   �� 5      $
��0.1* �6    60   �� 5      $
��0.1* �6    60   �� 5      $
��0.3* �6    60   �� 5      $
��0.1* �6    60   �� 5      $
��0.3* �6    60   �� 5      $��0.15* �6    61   .+�5!      �  5"      �� 5#      $
��0.3* �6'   66   �� 5$      $
��0.7* �6'   66   .+�5&      �� $��Bottom=�6   Название лиги6F   �� 5'      -   5        	 ��Name$	 OnEdit��5'      $��1��6+   $��Bottom=�6;   .��6k   /-Включить редактирование6�   5(      �� 5)      $��2��6   $��Center=�6%   Страна6H   �� 5*      
 �� Orientation$
 
Horizontal=�6   $��4��62   $��Bottom=�6?   .+�5+      �� $��Center=�6   
Клубы6>   �� 5,      -   5        $ RemBut��5,      {�.  $��15,0,0,0q�6;   .��6   Убрать из лиги6�   5-      �� 5.      -   5        $ NameText��5.      $��Center=�6(   $��1��6�   {�.  $��0,0,20,0q�6�   ��6J   �� *Name+ �� Mode$ TwoWay=� �� ValidatesOnDataErrors . 6�   �� 5/      -   5        $ CountryText��5/      $��Center=�6+   $��3��6�   {�.  $��0,0,20,0q�6�   ��6M   �� *	Country+$ TwoWay=� . 6�   �� 50      -	   5         ��Name$ listclub��50   $   $��5��6    �� SelectedIndex$ 0��66   $��0,0,20,0q�6H   ��6Z   �� *Clubs+ �� View 51      �� 52      .P�53      ��  �� DisplayMemberBinding 6)   �� $
 Name�.��6T   Название6^   �� 54       6)   �� $ Coach�.��6U   Тренер6]   55      57      �� 58      $
 
Horizontal=�6   $��6��62   $��Bottom=�6?   .+�59      �� $��Center=�6   0.Клубы не состоящие в лиге6R   �� 5:      -
   5        $ AddBut��5:      {�.  $��15,0,0,0q�6;   .��6   Принять в лигу6�   5;      �� 5<      -   5        $ listaddclub��5<   $   $��7��6   $ 0��69   {�.  $��0,0,20,0q�6]   ��6o   �� *Clubs+ 5=      �� 5>      .P�5?      ��  6)   �� $
 Name�.��6T   Название6^   �� 5@       6)   �� $ Coach�.��6U   Тренер6]   5A      5C      � 5D      $��8��6    5E      �� 5F      $��0.48* �6'   67   �� 5G      $��0.52* �6'   67   .+�5I      �� -   5        $ OKBut��5I      $��
0,10,10,10q�6(   .��6P   Ок6T   �� 5J      -   5        $ EditBut��5J      $��1��6*   {�.  $��10,10,20,10q�6M   .��6x   Редактировать6�   5K      5L      5M      5N      