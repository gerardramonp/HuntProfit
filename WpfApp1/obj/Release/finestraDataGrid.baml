   M S B A M L   `   `   `  ���� E  AHuntProfit, Version=1.4.1.0, Culture=neutral, PublicKeyToken=null'clr-namespace:HuntProfit
HuntProfit  !    HuntProfit.finestraDataGrid   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:HuntProfit     -     � Title$  HistorialDG��$	��555��$
��1340�� � Icon$% Resources/HuntProfit_TCicon.ico�� � WindowStyle$
 None=�
������� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=�.��� -    �Name$ gridHistorial�����	#FF363944 � RowDefinitions ��  � ColumnDefinitions �� .+�&   System.Windows.Controls.DataGrid  -     Name$ HistorialHunts��$
��Left=�$	��475��$��	20,47,0,0q�$	��Top=�$
��1300��$	   HorizontalScrollBarVisibility$	 Visible=�
   
IsReadOnly
 . ���	#FF4A4E5F   AlternatingRowBackground �	#FFA0D8E4 �� AlternationCount$ 2�����	#FF222431$��2q�
���   �   HeadersVisibility$ Column=� � 	Resources =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value*  $System.Windows.Controls.DataGridCell)  #System.Windows.Controls.DataGridRow �� Triggers P� Property P� Value��%[  '        '  v     '  �     �� "  .���  � �� �	#FF4EB8CE��  � ��$
 Bold"���  � ��$ 0,0,1,2q���  � ��
 �   ��� "  .���  � ��
 �   ���  � ��$ 14(���  � ��$ 0,0,0.4,0.4q���  � ��
 �   ��� "   P�  �@ AlternationIndex$ 0��. ���  � �� �	#FF84B5BF   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 26 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *HuntID+  $ Respawn��$	 152  �� *	Respawn+  $	 Day��$ 50  �� *Dia+  $
 Time��$ 55  �� *Time+  $	 Ppl��$ 25  �� *
Persones+  $
 W.EK��$ 70  �� *	WasteEK+  $
 W.ED��$ 70  �� *	WasteED+  $
 W.RP��$ 70  �� *	WasteRP+  $
 W.MS��$ 70  �� *	WasteMS+  $ T.Waste��$ 70  �� *
TotalWaste+  $
 Loot��$ 75  �� *Loot+  $ Balance��$ 75  �� *	Balance+  $ PROFIT/e��$ 80    
FontWeight$
 Bold"� �� *
ProfitEach+  $
 T.EK��$ 75  �� *
TransferEK+  $
 T.ED��$ 75  �� *
TransferED+  $
 T.RP��$ 75  �� *
TransferRP+  $
 T.MS��$ 75  �� *
TransferMS+  $
 Paid��$ 28  �� *Pagat+4	  .System.Windows.Controls.DataGridTemplateColumn	  $	 PAY��$ *  	  CellTemplate �� .R��� $��buttonPagar��-   $
��Pay!��$��2,2,2,2q� � Cursor$
 Hand���� $
��Left=�$��35��$	��Top=�$
��1340�����	#FF222431�� $
��Left=�$��2��$	��Top=�$
��1340��$��0,34,0,0q����	#FFD4D4D4�� -    ��Name$ lbClose��$��X��$
��Left=�$��1311,-5,0,0q�$	��Top=�$
��Bold"�$��20(����� " ��.���  � �� �	#FF4EB8CE P�  �@J�IsMouseOver . . ���  � �� �	#FFD4D4D4�� -   $ 	lbTitolDG��$��Hunt Record��$��SemiBold"����	#FFD4D4D4$
��Left=�$��	42,-3,0,0q�$	��Top=�$��20(��� -    ��Name$ imgIcono��$
��Left=�$��22��$��22��$��14,6,0,0q�$	��Top=�$!��Resources\HuntProfit_TC.jpg���� -   $ lbFaltaPagarEDtxt��$��ED missing:��$
��Left=�$��1141,522,0,0q�$	��Top=����	#FFD4D4D4$��16(�$��SemiBold"��� -	    ��Name$ tbFaltaPagarED��$
��Left=�$��25��$��80��$��1236,526,0,0q� �� TextWrapping$
 Wrap=�  �� 
IsReadOnly  . $	��Top=�! �� TextAlignment$! Center=����	#FF4EB8CE���	#FF222431���	#FFD4D4D4$��16(�$��SemiBold"�