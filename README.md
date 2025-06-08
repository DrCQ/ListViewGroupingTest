# ListViewGroupingTest
This small WPF application (*PhotoBook*) shows a wrong behaviour of the `ListView` Control while using the **Grouping** functionality. This app is based on a template using the MaterialDesign Library, NET 9 and CommunityToolkit but 
the basic functionality is implemented in the `ListViewPresenterView.xml` and `ListViewPresenterViewModel.vb` only. The `ImageItemViewModel.vb` class provides few properties for a small image icon as shown in the picture below.

Just run the small app, use File -> ShowImages to select a folder with JPG images inside. The dectected content will be displayed.

![](https://github.com/DrCQ/ListViewGroupingTest/blob/a198a3a079006579f66d052d1d887ce6ef8876f8/ListViewGroupingTest/Pictures/PhotoBook%20-%20Wrap%20List%20View.png)

To achive this *view* the `ListView` control uses the following `Style`, where the `ItemsPanel` template contains the `WrapPanel` control (see below).
```
<Style x:Key="ListViewStyle" TargetType="{x:Type ListView}" BasedOn="{StaticResource {x:Type ListView}}">
   <Setter Property="matD:ListViewAssist.ListViewItemPadding" Value="5"/>
   <Setter Property="ScrollViewer.HorizontalScrollBarVisibility" Value="Disabled"/>
   <Setter Property="ItemTemplate" Value="{StaticResource ImageItemTemplate}"/>
   <Setter Property="ItemsPanel">
      <Setter.Value>
        <ItemsPanelTemplate>
          <WrapPanel Orientation="Horizontal" IsItemsHost="True"/>
        </ItemsPanelTemplate>
      </Setter.Value>
   </Setter>
</Style>
```
The wrong alignement occurres when the `ListView` control should support **Grouping**. Adding just an empty `<ListView.GroupStyle>` is overwriting the `WrapPanel` template of the `ItemsPanel`.
```
<ListView ItemsSource="{Binding ImageView}" Style="{StaticResource ListViewStyle}">
   <ListView.GroupStyle>
      <GroupStyle>
      </GroupStyle>
   </ListView.GroupStyle>
</ListView>
```
The resulting *view* is shown in the next picture. The horizontal wrap alignment is now replaced with the vertical alignment. This is defintly WRONG!

![](https://github.com/DrCQ/ListViewGroupingTest/blob/570a93803c2ec17e03a81aad5ea7c5b82c463ab2/ListViewGroupingTest/Pictures/PhotoBook%20with%20Wrong%20Alignment.png)

After activating the **Grouping** functionality (adding the `GroupDescription`) the *view* shows proper **Grouping** alignment using the default style (our `GroupStyle` property was empty).

![](https://github.com/DrCQ/ListViewGroupingTest/blob/74e7b21715904ff3f48058b70155a99952461a48/ListViewGroupingTest/Pictures/PhotoBook%20-%20Grouping%20View.png)

Switching off the **Grouping** (Clear the `GroupDescriptions`) is resulting in the propert (horizonal wrap) allignement as shown in the first picture.

# Summary
1. The `ListView` control with `<GroupStyle>` is overwriting the `ItemPanel` settings with some internal setting of the default grouping template.
2. Switching the grouping functionality on and off is fixing the alignment problem and the proper `<ItemsPanel>` template is used.
3. I don't find a way to fix this alignment problem in  XAMP or code.
