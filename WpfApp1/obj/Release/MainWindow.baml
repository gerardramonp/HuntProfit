   M S B A M L   `   `   `  ���� E  AHuntProfit, Version=1.4.1.0, Culture=neutral, PublicKeyToken=null'clr-namespace:HuntProfit
HuntProfit  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem     HuntProfit.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     *localclr-namespace:HuntProfit     9System&clr-namespace:System;assembly=mscorlib     :xctk)http://schemas.xceed.com/wpf/xaml/toolkit     
customNamewpftool     -     �Name$  windowHuntProfit�� � Title$ Hunt Profit��$��600     ��$��385.5�� � Icon$% Resources/HuntProfit_TCicon.ico�� � 
ResizeMode$ NoResize=����	#FF222431���	#FFD4D4D4 � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 	Resources  �� 
TargetType �� 	Resources	 �� Property
 �� Value   canviColorLBTancar �� Triggers P� Property P� Value��%  '��       '�� L     &	  �     �� " �� ���� '��       " ��.��� 	 �@��CornerRadius$
 5���� " �� ���� '��       " ��.��� 	 �@��CornerRadius$
 5���� " �� P�  �@J�IsMouseOver . . ��� 	 � ��
 �	#FFD4D4D4.��� $��0,0,0,0q����	#FF363944 � ColumnDefinitions �� $	��390 � � RowDefinitions .+��� -    ��Name$ canvasHeader��$
��Left=�$��45��$	��Top=�$	��386�����	#FF222431�� $
��Left=�$	��114��$��	0,486,0,0q�$	��Top=�$	��386�����	#FF222431�� -    ��Name$ 	lbRespawn��$��Respawn:��$
��Left=�$��7,58,0,0q�$	��Top=�$��SemiBold"����	#FFD4D4D4$��20(��� -    ��Name$ 	cbRespawn�� �� Text$ Select a respawn...��$
��Left=�$��
107,60,0,0q�$	��Top=�$	��209�� �� 
IsEditable .  �� StaysOpenOnEdit . ���	#FF222431���	#FF1CC5E8$��18(�.@��� $��	Hero Cave���� $��Spike���� $��Grimvale (Upper floors)���� $��Grimvale (Deep)���� $��Medussa Tower���� $��	Banuta -1���� $��Deeper Banuta���� $��Walls���� $��Ice Elfs���� $��Asuras���� $��	Prison -1���� $��	Prison -2���� $��	Prison -3���� $��Nightmare Isle���� $��Gazer Spectres���� $��
Carnivoras���� $��Mirror���� $��Elite Skeletons���� $��F.Ascention(Entrance)���� $��Falcon���� -   $ lbWastes��$��Waste��$
��Left=�$��126,100,0,0q�$	��Top=�$��SemiBold"����	#FFD4D4D4$��20(��� -   $ 
lbTransfer��$��Transfer��$
��Left=�$��255,100,0,0q�$	��Top=�$��SemiBold"����	#FFD4D4D4$��20(�$��93���� -   $
 lbEK��$	��EK:��$
��Left=�$��
73,136,0,0q�$	��Top=����	#FFD4D4D4$��20(��� -   $
 lbED��$	��ED:��$
��Left=�$��
71,181,0,0q�$	��Top=����	#FFD4D4D4$��20(��� -	   $
 lbRP��$	��RP:��$
��Left=�$��
71,226,0,0q�$	��Top=����	#FFD4D4D4$��20(��� -
   $
 lbMS��$	��MS:��$
��Left=�$��
66,271,0,0q�$	��Top=����	#FFD4D4D4$��20(��� -   $ lbLoot��$��Loot:��$
��Left=�$��
53,376,0,0q�$	��Top=�$��SemiBold"����	#FFD4D4D4$��20(��� -   $ lbTime��$��Time:��$
��Left=�$��
48,420,0,0q�$	��Top=�$��SemiBold"����	#FFD4D4D4$��20(��� -   $ lbTimeValue��$
��0:00��$
��Left=�$��306,424,0,0q�$	��Top=�$��SemiBold"����	#FFD4D4D4$��18(��� -    ��Name$ tbWEK��$
��Left=�$��30��$��111,140,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��100�� �� TextAlignment$ Right=����	#FFF07D0A���	#FF222431���	#FFD4D4D4$��20(��� -   $ tbWED��$
��Left=�$��30��$��111,185,0,0q�$
 Wrap=�$	��Top=�$	��100��$ Right=����	#FF00FF64���	#FF222431���	#FFD4D4D4$��20(��� -   $ tbWRP��$
��Left=�$��30��$��111,230,0,0q�$
 Wrap=�$	��Top=�$	��100��$ Right=����	#FFFFDC00���	#FF222431���	#FFD4D4D4$��20(��� -   $ tbWMS��$
��Left=�$��30��$��111,275,0,0q�$
 Wrap=�$	��Top=�$	��100��$ Right=����	#FFBE00FF���	#FF222431���	#FFD4D4D4$��20(��� -   $ tbLoot��$
��Left=�$��30��$��111,380,0,0q�$
 Wrap=�$	��Top=�$	��100��$ Right=�$��SemiBold"����	#FF4EB8CE���	#FF222431���	#FFD4D4D4$��20(��� -    ��Name$ 
sliderTime��$
��Left=�$��111,433,0,0q�$	��Top=�$	��195�� � Maximum$	 180Q� �� TickPlacement$ BottomRight=� �� Ticks$ 0, 30, 60, 90, 120, 150 R����	#FFD4D4D4 �� IsMoveToPointEnabled . �� -   $
 lb1h��$��1h��$
��Left=�$��167,452,0,0q�$	��Top=�$��10(����� " ��.��� 	 � ��
 �	#FFD4D4D4 P�  �@J�IsMouseOver . . ��� 	 � ��
 �	#FF4EB8CE�� -   $
 lb2h��$��2h��$
��Left=�$��229,452,0,0q�$	��Top=�$��10(����� " ��.��� 	 � ��
 �	#FFD4D4D4 P�  �@J�IsMouseOver . . ��� 	 � ��
 �	#FF4EB8CE�� -   $ tbTEK��$
��Left=�$��30��$��246,140,0,0q�$
 Wrap=�$	��Top=�$	��100��$ Right=� �� 
IsReadOnly . ���	#FFF07D0A���	#FF333951���	#FFD4D4D4$��20(��� -   $ tbTED��$
��Left=�$��30��$��246,185,0,0q�$
 Wrap=�$	��Top=�$	��100��$ Right=� . ���	#FF00FF64���	#FF333951���	#FFD4D4D4$��20(��� -   $ tbTRP��$
��Left=�$��30��$��246,230,0,0q�$
 Wrap=�$	��Top=�$	��100��$ Right=� . ���	#FFFFDC46���	#FF333951���	#FFD4D4D4$��20(��� -   $ tbTMS��$
��Left=�$��30��$��246,275,0,0q�$
 Wrap=�$	��Top=�$	��100��$ Right=� . ���	#FFBE00FF���	#FF333951���	#FFD4D4D4$��20(��� -   $ lbTWaste��$��T Waste:��$
��Left=�$��
22,326,0,0q�$	��Top=�$��SemiBold"����	#FFD4D4D4$��20(��� -   $ tbWTotal��$
��Left=�$��34��$��111,328,0,0q�$
 Wrap=�$��0��$	��Top=�$	��100��$ Right=� . ���	#FF4EB8CE$��SemiBold"� �� 	UndoLimit$	 101�����	#FF222431���	#FFD4D4D4$��20(�$	��2.5q��� -     ��Name$  
btCalcular��$��	CALCULATE��$
��Left=�$��240,369,0,0q�$	��Top=�$	��112��$��50�����	#FF4EB8CE���	#FF4EB8CE$��20(�! � Cursor$
! Hand����Z� �� -   $  btReiniciar��$��Reset��$
��Left=�$��246,318,0,0q�$	��Top=�$	��100��$��40�����	#FFD4D4D4���	#FFD4D4D4$��20(�$
! Hand����Z� �� -   $ 	lbBalance��$��Balance:��$
��Left=�$��
44,502,0,0q�$	��Top=�$��SemiBold"�$��20(����	#FFD4D4D4�� -   $ lbProfit��$��Profit/each:��$
��Left=�$��
14,546,0,0q�$	��Top=�$��SemiBold"�$��20(����	#FFD4D4D4�� -    $ lbBalanceValue��$��0��$
��Left=�$��130,503,0,0q�$	��Top=�$��SemiBold"�$��20(����	#FFD4D4D4�� -!   $ lbProfitValue��$��0��$
��Left=�$��130,547,0,0q�$	��Top=�$��SemiBold"�$��20(����	#FFD4D4D4�� -"   $  btHistorial��$��Record��$
��Left=�$��240,528,0,0q�$	��Top=�$	��117��$��50�����	#FF4EB8CE���	#FF222431$��22(�$��SemiBold"�$
! Hand����Z� �� -#   $ lbTitol��$��Hunt Profit��$
��Left=�$	��Top=�$��40,2,0,0q����	#FFD4D4D4$��20(�$��SemiBold"��� -$   $ lbClose��$��X��$
��Left=�$��
358,-3,0,0q�$	��Top=�$
��Bold"�$��20(����� " ��.��� 	 � ��
 �	#FF4EB8CE P�  �@J�IsMouseOver . . ��� 	 � ��
 �	#FFD4D4D4�� $
��Left=�$��2��$��0,43,0,0q�$	��Top=�$	��386�����	#FFD4D4D4�� $
��Left=�$��2��$��	0,485,0,0q�$	��Top=�$	��386�����	#FFD4D4D4�� -%   " ��Name$" imgEK��$
��Left=�$��22��$��22��$��
62,145,0,0q�$	��Top=�$��Resources\EK_g.png��# J� RenderTransformOrigin$# 0.5,0.5&�z�V� .~��� �� �� $ �� Angle$$ 43.461Q�T� �� -&   $" imgED��$
��Left=�$��20��$��20��$��
53,192,0,0q�$	��Top=�$��Resources\ED_g.png���� -'   $" imgRP��$
��Left=�$��20��$��20��$��
59,236,0,0q�$	��Top=�$��Resources\RP_g.png��$# 0.5,0.5&�z�V� .~��� �� �� $$ -39.551Q�T� �� -(   $" imgMS��$
��Left=�$��20��$��20��$��
49,281,0,0q�$	��Top=�$��Resources\ms_g.png���� -)   $" 
imgIconoTC��$
��Left=�$��25��$��25��$��11,9,0,0q�$	��Top=�$!��Resources\HuntProfit_TC.jpg���� -*   $ lbFolderSettings��$�� ��$
��Left=�$��
355,50,0,0q�$	��Top=�$��25��$��25��
�������% � ToolTip$$% Modify Hunt Record folder path������ " ��.��� 	 � ��
 �� & �� ImageSource$$& Resources/FolderSettings_b.png�� P�  �@J�IsMouseOver . . ��� 	 � ��
 �� $$& Resources/FolderSettings_g.png�� ' �� BasedOn��%\   '_�       7_��� " _�8'   .��� 	 � ��
 �	#FF222431�� 	 � ��
 �	#FFD4D4D4