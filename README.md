# ListViewGroupingTest
This small WPF application (*PhotoBook*) shows a wrong behaviour of the `ListView` Control while using the Grouping functionality.
The basic functionality is implemented in the `ListViewPresenterView.xml` and `ListViewPresenterViewModel.vb`. This small app uses the `ImageItemViewModel.vb` class to provide a few properties for a small image icon as shown in the next picture.

![](https://github.com/DrCQ/ListViewGroupingTest/blob/a198a3a079006579f66d052d1d887ce6ef8876f8/ListViewGroupingTest/Pictures/PhotoBook%20-%20Wrap%20List%20View.png)

To achive this View the `ListView` control uses the following `Style`:
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
The wrong alignement occurres when the `ListView` control should support Grouping. Adding just empty `<ListView.GroupStyle>` is overriting the `WrapPanel` template of the `ItemsPanel`.
```
<ListView ItemsSource="{Binding ImageView}" Style="{StaticResource ListViewStyle}">
   <ListView.GroupStyle>
      <GroupStyle>
      </GroupStyle>
   </ListView.GroupStyle>
</ListView>
```
The resulting view is shown in the next picture. The horizontal wrap alignment is replaces wit the vertical alignment.

![](https://github.com/DrCQ/ListViewGroupingTest/blob/570a93803c2ec17e03a81aad5ea7c5b82c463ab2/ListViewGroupingTest/Pictures/PhotoBook%20with%20Wrong%20Alignment.png)

After activating the Grouping functionality (adding the `GroupDescription`) the view show proper grouping alignment using the default style (our `GroupStyle` property was empty).
