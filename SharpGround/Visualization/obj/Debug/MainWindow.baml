   M S B A M L   `   `   `  ����H  DVisualization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:VisualizationVisualization  5   	       Visualization.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Visualization     5   	     � Title$  
MainWindow��5   	   $	��481��6   $	��808��6)    � 	Resources 5
       �� DataType)   #Visualization.MassParticleViewModel � ToolTip!   Visualization.EdgeViewModel	 �� X1	 �� Y1	 �� X2	 �� Y2��%�  (��       5      *, +)(�� �     5      *, +)�� 5   
   "  5      .R�5      A� ��5      �� *
Diameter+��5      �� *
Diameter+��5      �� *Color+��5      �� *Color+ 5      �� *	ToolTip+5      �� 5   
   "  5      .R�5      �� $	��0.5��5      ��5      �� *Color+ 5      �� *X1+ 5      �� *Y1+ 5       �� *X2+ 5!      �� *Y2+5#      5$       � DataContext 5&         Visualization.ViewModel  5+   
   .��5.      �� $	��450��6   $	��800��6   $��Stretch=�6&   $��Stretch=�6D   .+�5/   
   �� .+�50      �� -   5        $��start_button��50   %   $��Start��6   $
��Left=�6:   $��Bottom=�6V   $��75��6r   6}   �� 51      -   5        $��pause_button��51   %   $��Pause��6   $
��Left=�6;   $��Bottom=�6V   $��75��6q   $y�Hidden=�6�   52      �� 53   
   -   5        $��canvas��53      �� 54   
   ��55      �� *
Vertices+��57      �� 58      .R�59      �� 5:      ��5<      �� 5=      .�5>      �� 	 �� Property	 �@��Left
 �� Value
 5@      �� *X+�� 5B      	 �@��Top
 5D      �� *Y+5F      5H      �� 5I   
   ��5J      �� *Edges+��5L      �� 5M      .R�5N      �� 5O      5Q      5R      5S      