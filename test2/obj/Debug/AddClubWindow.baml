   M S B A M L   `   `   `  ����  FootballManager#    FootballManager.AddClubWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$%  Добавление клуба��5   	   $	��300��6"   $	��500��6/   $	��300��5   	   $	��500��6   .��5      �  � ColumnDefinitions 5   
   �� 5	      $
��0.7* �6   6.   �� 5
      $
��0.3* �6   6.   .+�5   
   � $��1��6    � RowDefinitions 5      �� 5      $��0.785* �6    62   �� 5      $��0.215* �6    62   .+�5      �� -   5         ��Name$ LogoIm��5      $��	0,10,15,0q�6$   ��67   ��  �� ElementName$ LogoText�� �� Path$
 Text�6l   �� 5      -   5         ��Name$ AddClubButton��5      $��
0,15,15,15q�6,   $��1��6@   $��2��6M   .��6y   Добавить6�   5      � 5   
    5   
   �� 5      6   �� 5      6   �� 5      6"   �� 5      6"   �� 5      6"    5   
   �� 5      $��0.33* �6   6/   �� 5      $��0.67* �6   6/   .+�5    
   �� $��Center=�6   $��Center=�6/   Нзвание клуба6[   �� 5!      $��2��6   $��Center=�6%   $��Center=�6@   Главный тренер6m   �� 5"      $��3��6   $��Center=�6%   $��Center=�6@   
Лига6c   �� 5#      $��4��6   $��Center=�6%   $��Center=�6@   Игроки6e   � 5$      $��1��6   $��3��6#    5%      �� 5&      $
��0.5* �6'   66   �� 5'      $
��0.5* �6'   66   .+�5)      �� -   5         ��Name$ 	AddLeText��5)      $��Center=�6-   $��	0,15,0,15q�6O   ��6b   �� $ AddLeBox��$
 Text�6�   �� 5*      -   5         ��Name$ AddLeBox��5*      $��Center=�6-   $��
0,15,10,15q�6O   $��1��6c   .@�5+      �� 	 �� 
IsSelected	 . .��65   Вне лиги6?   5,      5-      �� 5.      -   5        $ PlayerCmBox��5.      $��Center=�6,   $��
0,15,10,15q�6N   $��4��6b   $��1��6o   .@�5/      �� 	 . {�.  .��6C   " Выбрать из списка6V   �� 50      .��6   Не выбирать6,   51      �� 52   
   -   5        $ NameText��52      $��0��6$   $��Center=�61   $��1��6S   $��
0,15,10,15q�6c   6y   �� 53   
   -   5        $ 	CoachText��53      $��2��6%   $��Center=�62   $��2��6T   $��
0,15,10,15q�6d   6z   �� 54   
   $��0��6   $��1��6$   $��Center=�61   $��Center=�6L   Эмблема6r   � 55      $��1��6   $��1��6     56      �� 57      $��0,44* �6'   67   �� 58      $��0,56* �6'   67   .+�5:      �� -   5        $ LogoText��5:      $��1��6,   $��Center=�6<   $��
0,15,10,15q�6^   6t   �� 5;      -	   5        $ AddLogoButton��5;      $��	0,15,0,15q�60   $��0��6C   .��6o   Выбрать эмблему6�   5<      5=      5>      5?      