   M S B A M L   `   `   `  ����E  AHuntProfit, Version=1.4.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:HuntProfit
HuntProfit  5   	   !    HuntProfit.finestraDataGrid   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:HuntProfit     5   	   -   5          � Title$  HistorialDG��5   	   $	��555��6   $
��1340��6*    � Icon$% Resources/HuntProfit_TCicon.ico��67    � WindowStyle$
 None=�6^   
������� � 
ResizeMode$ NoResize=�6�    � WindowStartupLocation$ CenterScreen=�5	   	   .��5
      � -   5         �Name$ gridHistorial��5
      ���	#FF363944 � RowDefinitions 5   
   �� 5       � ColumnDefinitions 5   
   �� 5      .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ HistorialHunts��5      $
��Left=�6+   $	��475��6F   $��	20,47,0,0q�6S   $	��Top=�6f   $
��1300��6~   $	   HorizontalScrollBarVisibility$	 Visible=�6�   
   
IsReadOnly
 . ���	#FF4A4E5F   AlternatingRowBackground �	#FFA0D8E4 �� AlternationCount$ 2��6  ���	#FF222431$��2q�6.  
���   �   HeadersVisibility$ Column=�6U   � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value*  $System.Windows.Controls.DataGridCell)  #System.Windows.Controls.DataGridRow �� Triggers P� Property P� Value��%>  '        '  �     '  �    �� 5      "  6   .�5      ��  � �� �	#FF4EB8CE63   �� 5       � ��$
 Bold"�63   �� 5       � ��$ 0,0,1,2q�68   �� 5       � ��
 �   �64   5      �� 5      "  6   .�5      ��  � ��
 �   �63   �� 5       � ��$ 14(�61   �� 5       � ��$ 0,0,0.4,0.4q�68   �� 5       � ��
 �   �64   5       �� 5!      "  6    5"      P� 5#       �@ AlternationIndex$ 0��6>   . �5$      ��  � �� �	#FF84B5BF6;   5%      5'      5(         Columns 5*      0  *System.Windows.Controls.DataGridTextColumn  5+      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 26 61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *HuntID+  5,      $ Respawn��6%   $	 152 66    6B   �� *	Respawn+  5-      $	 Day��6%   $ 50 62    6=   �� *Dia+  5.      $
 Time��6%   $ 55 63    6>   �� *Time+  5/      $	 Ppl��6%   $ 25 62    6=   �� *
Persones+  50      $
 W.EK��6%   $ 70 63    6>   �� *	WasteEK+  51      $
 W.ED��6%   $ 70 63    6>   �� *	WasteED+  52      $
 W.RP��6%   $ 70 63    6>   �� *	WasteRP+  53      $
 W.MS��6%   $ 70 63    6>   �� *	WasteMS+  54      $ T.Waste��6%   $ 70 66    6A   �� *
TotalWaste+  55      $
 Loot��6%   $ 75 63    6>   �� *Loot+  56      $ Balance��6%   $ 75 66    6A   �� *	Balance+  57      $ PROFIT/e��6%   $ 80 67      
FontWeight$
 Bold"�6a    6B   �� *
ProfitEach+6a     58      $
 T.EK��6%   $ 75 63    6>   �� *
TransferEK+  59      $
 T.ED��6%   $ 75 63    6>   �� *
TransferED+  5:      $
 T.RP��6%   $ 75 63    6>   �� *
TransferRP+  5;      $
 T.MS��6%   $ 75 63    6>   �� *
TransferMS+  5<      $
 Paid��6%   $ 28 63    6>   �� *Pagat+4	  .System.Windows.Controls.DataGridTemplateColumn	  5=      $	 PAY��6)   $ * 66    	  CellTemplate 5>      �� 5?      .R�5@      �� $��buttonPagar��6%   -   5        $
��Pay!��5@   R   $��2,2,2,2q�6a    � Cursor$
 Hand��6r   5A      5C      5E      �� 5G   
   $
��Left=�6   $��35��6,   $	��Top=�68   $
��1340��6P   ���	#FF2224316]   �� 5H   
   $
��Left=�6   $��2��6,   $	��Top=�67   $
��1340��6O   $��0,34,0,0q�6\   ���	#FFD4D4D46n   �� 5I   
   -   5         ��Name$ lbClose��5I      $��X��6!   $
��Left=�6-   $��1311,-5,0,0q�6H   $	��Top=�6]   $
��Bold"�6u   $��20(�6�   ��5J      �� 5K      " ��6   .�5L      ��  � �� �	#FF4EB8CE63    5M      P� 5N       �@J�IsMouseOver . . �5O      ��  � �� �	#FFD4D4D46;   5P      5R      5T      �� 5V   
   -   5        $ 	lbTitolDG��5V      $��Hunt Record��6#   $��SemiBold"�69   ���	#FFD4D4D4$
��Left=�6f   $��	42,-3,0,0q�6�   $	��Top=�6�   $��20(�6�   �� 5W   
   -   5         ��Name$ imgIcono��5W      $
��Left=�6"   $��22��6=   $��22��6I   $��14,6,0,0q�6T   $	��Top=�6f   $!��Resources\HuntProfit_TC.jpg��6   �� 5X   
   -   5        $ lbFaltaPagarEDtxt��5X      $��ED missing:��6+   $
��Left=�6A   $��1141,522,0,0q�6\   $	��Top=�6r   ���	#FFD4D4D4$��16(�6�   $��SemiBold"�6�   �� 5Y   
   -	   5         ��Name$ tbFaltaPagarED��5Y      $
��Left=�6*   $��25��6E   $��80��6R   $��1236,526,0,0q�6]    �� TextWrapping$
 Wrap=�6s     �� 
IsReadOnly  . $	��Top=�6�   ! �� TextAlignment$! Center=�6�   ���	#FF4EB8CE���	#FF222431���	#FFD4D4D4$��16(�6  $��SemiBold"�6  5[      5\      