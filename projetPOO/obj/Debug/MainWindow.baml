   M S B A M L   `   `   `  ����D  @projetPOO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:projetPOO	projetPOO  5   	       projetPOO.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:projetPOO     5   	     � Title$  
MainWindow��5   	   $	��350��6   $
��1100��6)   .��5	      � .+�5
   
   � $��0��6   $��0��6   .+�5      �� $��0��6   $��0��6!   $��
Hauteur : ��61   �� 5      $��1��6   $��0��6!   $��
Largeur : ��61   �� 5      -   5        $��
MazeHeight��5      $��0��6(   $��1��65   $��50��6E   �� 5      -   5        $��	MazeWidth��5      $��1��6'   $��1��64   $��50��6D   	� 5      -   5        $��GenerateMod1��5   +    d� 	IsChecked$
 trueY�6    	� 	GroupName$ GenerateMod��6?   $��0��6W   $��2��6d   $��Mode de génération n°1��6t   $
��Left=�6�   $	��Top=�6�   	� 5      -   5        $��GenerateMod2��5      $ GenerateMod��6.   $��1��6F   $��2��6S   $��Mode de génération n°2��6c   $
��Left=�6�   $	��Top=�6�   �� 5      -   5        $��generateMaze��5   2   $��0��6   $��4��6"   $��Générer le labyrinthe��6F   $
��Auto��6f   6s   �� 5      $��0��6   $��3��6!   $��Temps (en ms) entre ��61   �� 5      $��1��6   $��3��6!   $!��chaque affichage de case : ��61   �� 5      -   5        $��MazeDisplayTime��5      $��1��6-   $��4��6:   $��5��6J   �� 5      -   5        $��generateAnimateMaze��5   2   $��1��6   $��5��6"   $(��"Générer le labyrinthe(Animation)��6M   $
��Auto��6x   6�   �� 5      -   5        $��generatePath��5   2   $��0��6   $��5��6"   $#��Générer le chemin de sortie��6F   $
��Auto��6l   {�.  6�    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5       �� 5!      $��* �6#   �� 5"      $��* �6#   �� 5#      $��* �6#   �� 5$      $��* �6#   �� 5%      $��* �6#   �� 5&      $��* �6#   5(      � 5)   
   -	   5        $��MainGrid��5)   =   $��1��6   $��0��6   $��5,5,5,5q�6,   5,      �� 5-   
   -
   5        $��isAllConnectedLabel��5-   -   $��2��6   $��0��6   $
��Left=�6H   $	��Top=�6c    5/   
   �� 50      $
��Auto �6   �� 51      $��2* �6   �� 52      $
��Auto �6    54   
   �� 55      $��* �6   57      58      