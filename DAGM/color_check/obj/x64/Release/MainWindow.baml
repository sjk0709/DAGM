   M S B A M L   `   `   `  ���� ?  ;dagm, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:DAGM.cameraDAGM.camera  /clr-namespace:DAGM.solver_uiDAGM.solver_ui      DAGM.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ,cameraclr-namespace:DAGM.camera     2	solver_uiclr-namespace:DAGM.solver_ui       � Title$
  DAGM��$	��800��$	��800��   DarkBackground#��[   � 	Resources  �� DataType   DAGM.camera.CameraView � DataContext#   DAGM.solver_ui.ResultMainView"   DAGM.solver_ui.CnnResultView��%�   (��       *, +)(�� 2     *, +)(�� h     *, +)�� CameraViewTemplate��"  .R�   �� �� ResultMainViewTemplate��"  .R�   �� �� CnnViewTemplate��"  .R�   �� .���  � RowDefinitions �� $��20 ��� $	��100 ��� �� �� $��50 ��� �� $��5 � � ColumnDefinitions �� $��5 ��� �� �� �� �� $��5 �.+�]� $��0��$��0��$��6��.+�o� $	��Top=�
�������.@�m� $��_File��.@�m� $��_Open�� m� Icon �� $��16��$��16��m� $��_Save�� �� $��16��$��16��m� $��
Save As…���� m� -   $��E_xit��m� $��_Edit��.@�m� $
��_Cut��m� $��C_opy��m� $��_Paste��m� $��_Help��.@�m� $��_About��m� $��	_Contents��m� $��_Help��]� $��1��$��1��$��4��.+��� $
��DAGM��$��90��$��Center=�$
��Left=�$��2q�$��14(�
�������$
��Bold"�.����  �� Orientation$ 
Horizontal=�.+��� -    ��Name$
 Load��$
��Open��$	��120��$
��Left=�$��2q�$��16(��� -   $
��Exit��$	��120��$
��Left=�$��2 2 2 2q�$��16(��� $	��CNN��$��90��$��Center=�$
��Left=�$��2q�$��14(�
�������$
��Bold"�.���� $ 
Horizontal=�.+��� -   $��
CnnSetting��$��Setting��$	��120��$��2 2 2 2q�$��16(��� -   $ CnnRun��$	��Run��$	��120��$��2 2 2 2q�$��16(�]� $��2��$��1��$��2��$��2��$��2q�.+��� 
�������$��3q�._��� -   	 ��Name$	 	ImageView��$��10q����� �� $��2��$��3��$��2��$��2��$��2q�
�������$��3q�._��� -   $	 
ResultView��$��10q����� ]� $��4��$��1��$��1��$��1��
���   �$��2q�.+��� 
�������$��3q�._��� $��25(�$��Center=�$��Center=�
�������$
��Bold"�
 J� Effect
 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ 	#FFFF0202��   	Direction$ 0Q�   ShadowDepth$ 0Q�   
BlurRadius$ 10Q�]� $��5��$��1��$��1��$��1��
�������$��2q�.+��� $��72(�$��Center=�$��Center=�
   $ 2Q�$	 Red��$ 0Q�$ 0Q�]� $��4��$��2��$��1��$��1��
���   �$��2q�.+��� 
�������$��3q�._��� $��	Class No.��$��25(�$��Center=�$��Center=�
�������$
��Bold"�
   $ 	#FFFF0202��$ 0Q�$ 0Q�$ 10Q�]� $��5��$��2��$��1��$��1��
�������$��2q�.+��� -   $��ClassNo��$��72(�$��Center=�$��Center=�
   $ 2Q�$	 Red��$ 0Q�$ 0Q�]� $��4��$��3��$��1��$��1��
���   �$��2q�.+��� 
�������$��3q�._��� $��# of Defect��$��25(�$��Center=�$��Center=�
�������$
��Bold"�
   $ 	#FFFF0202��$ 0Q�$ 0Q�$ 10Q�]� $��5��$��3��$��1��$��1��
�������$��2q�.+��� -	   $��nDefect��$��72(�$��Center=�$��Center=�
   $ 2Q�$	 Red��$ 0Q�$ 0Q�]� $��4��$��4��$��1��$��1��
���   �$��2q�.+��� 
�������$��3q�._��� $��Result��$��25(�$��Center=�$��Center=�
�������$
��Bold"�
   $ 	#FFFF0202��$ 0Q�$ 0Q�$ 10Q�]� $��5��$��4��$��1��$��1��
�������$��2q�.+��� -
   $��FinalResult��$��72(�$��Center=�$��Center=�
   $ 2Q�$	 Red��$ 0Q�$ 0Q�