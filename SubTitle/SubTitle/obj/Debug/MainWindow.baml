   M S B A M L   `   `   `  ����C  ?SubTitle, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:SubTitleSubTitle  5   	       SubTitle.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:SubTitle     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � SizeToContent$ WidthAndHeight=�65   $	��350��6T   $	��525��6d    J� CommandBindings 5	      )  #System.Windows.Input.CommandBinding  5
   
   -   5           Command$ MediaCommands.Play��5
      6S   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   o� -   5        $
��menu��5      $	��Top=�6   .@�5      m� $
��Open��6   .@�5      m� -   5        $��	openVideo��5      $��Video��6,   6;   m� 5      -   5        $��openSubtitle��5      $��Subtitle��60   6B   5      5      �� 5   
   -   5        $
��play��5      $��MediaCommands.Play��6   $��15��6:   $��50��6F   $��Center=�6Q   $��Center=�6l   $��8(�6�   .��6�   Play5      r� 5   
   -   5        $��Player��5   $   $��1��6    r� LoadedBehavior$ Manual=�62    r� Stretch$
 None=�6J   �� 5   
   -   5        $��	subtitles��5   H   $��2��6   $��60��6!   $��Bottom=�6-   $��12(�6Y    �� TextAlignment$ Center=�6g   
�������
�������$
��Wrap=�6�   5      5      