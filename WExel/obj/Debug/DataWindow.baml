   M S B A M L   `   `   `  ����@  <WExel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WExelWExel  5   	       WExel.DataWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   %localclr-namespace:WExel     5   	   -   5          � Title$  
DataWindow��5   	   $	��350��6   $	��450��6)    � 
ResizeMode$ CanMinimize=�65   .��5	      �  � ColumnDefinitions 5
   
   �� 5      6!   �� 5      6!   �� 5      6!    � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� -   5        $��image\help.png��5      $��20��6(   $��20��63   $��3��6?   $��
100,0,0,70q�6O    � ToolTip$ Ayuda��6   �� 5   
   -   5        $
��Box1��5      $��	5,45,5,35q�6   $ 
Valor de X��61   �� 5   
   -   5        $
��Box2��5      $��1��6   $��	5,45,5,35q�6/   $ 
Valor de Y��6B   �� 5   
   -   5        $��Añadir��5      $��2��6#   $��	5,45,5,35q�63   5      �� 5   
   -   5        $��lista��5      $��20q�6    $��1��6,   $��2��69   $��3��6J    �� View 5      �� 5      .P�5      �� $��X��6%    �� Width$	 192��61    �� DisplayMemberBinding 6=   �� *x+�� 5      $��Y��6%   $	 192��60    6<   �� *y+5      5       o� 5"   
   -   5        $��ClickDerecho��5"      $��3��6#   $	��166��64   $	��100��6@   $
��Left=�6M   $	��Top=�6h   $y�	Collapsed=�6�   .@�5#      m� -   5        $��Añadir��5#   '   $��Añadir��6   $	��166��65    J� 	AllowDrop . .@�5$      m� -	   5        $��InsetarFinal��5$   5   $��Inserta al final��6   $	��166��6^   m� 5%      -
   5        $��InsertarAntes��5%   3   $��Inserta antes��6   $	��166��6e   m� 5&      -   5        $��InsertarDespues��5&   5   $��Inserta después��6   $	��166��6k   5'      m� 5(      -   5        $��	Eliminar1��5(   *   $��Eliminar��6   $	��166��6S   m� 5)      -   5        $��Editar1��5)   (   $��Editar��6   $	��166��6M   m� 5*      -   5        $��Ordenar��5*   )   $��Ordenar��6   $	��166��6N   �� 5+      $	��166��6   m� 5,      -   5        $��Cerrar1��5,   (   $��Cerrar��6   $	��166��6M   5-      5.      5/      