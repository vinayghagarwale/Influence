   M S B A M L   `   `   `  ����U  QMahApps.Metro, Version=0.14.0.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      T PNextGenImpactAnalysisTool, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:NextGenImpactAnalysisTool.ViewsNextGenImpactAnalysisTool.Views 5      Q-clr-namespace:NextGenImpactAnalysisTool.ModelNextGenImpactAnalysisTool.Model 5      7   1NextGenImpactAnalysisTool.Views.DependencyDiagram   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      Ncontrols;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:NextGenImpactAnalysisTool.Views     5      ?model-clr-namespace:NextGenImpactAnalysisTool.Model     5      $	��800��5
   '     � WindowStartupLocation$  CenterScreen=�5      (   "MahApps.Metro.Controls.MetroWindow   ShowTitleBar .  � Title$ Impact Diagram��5   "      ShowCloseButton .    ShowMaxRestoreButton .    ShowMinButton .     AccentColorBrush   	GlowBrush# �   5   $    � 	Resources 5      *  $System.Windows.Controls.DataGridCell �� 
TargetType  
ColumnGone	 �� Property
 �� Value  DisabledColumn,  &System.Windows.Controls.DataGridColumn��%E  &	       &	 J     7  �� 5   
   "  6   .�5      �� 	 � ��

 �   �6+   5      �� 5   
   .�5      �� 	 �@ CanUserResize
 .  5   )   �� 5      	 �@ CanUserSort
 .  5   )   �� 5      	 �@ CanUserReorder
 .  5   )   �� 5      	 �@ 	CellStyle8
   5   )   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5       $	��150 �6   �� 5!      .+�5#   
   �� -   5        $��	showcoord��5#      �� 5$   
   $��1��6   $��6q�6!   
�������._�5%      �� $��1��6   $��Visible=�6,   .��5&      �� -   5        $��mycanvas��5&      $
��4000��6)   $
��4000��67   
�������5(      5)      5*      5+      5,      