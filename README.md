# Delete listviewitem when dropping into particular view

To delete the dragged item when dropping into a particular view, handle the [ItemDragging](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemDragging_EV.html) event based on the conditions of [Action](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.ItemDraggingEventArgs~Action.html) and `Bounds` event arguments.

To delete the dragged item from the underlying collection when dropping into delete icon, follow the code example. It will enable or disable whenever drag started, and dropped by IsVisible property in ViewModel.

```
<ContentPage xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms">
<Grid>
  <Grid.RowDefinitions>
    <RowDefinition Height="Auto" />
    <RowDefinition Height="*" />
  </Grid.RowDefinitions>

  <Grid BackgroundColor="#2196F3">
    <Label Text="To Do Items" x:Name="headerLabel"  TextColor="White" FontAttributes="Bold" VerticalOptions="Center" HorizontalOptions="Center" />
    <StackLayout x:Name="stackLayout" IsVisible="{Binding IsVisible}" Orientation="Horizontal" VerticalOptions="Center" HorizontalOptions="Center">
      <Image Source="Delete.png" HeightRequest="30" WidthRequest="30" />
      <Label x:Name="deleteLabel" Text="Delete Item" FontAttributes="Bold" TextColor="White" />
    </StackLayout>
  </Grid>
  <syncfusion:SfListView x:Name="listView" Grid.Row="1"
                   ItemsSource="{Binding ToDoList}"
                   DragStartMode="OnHold"
                   BackgroundColor="#FFE8E8EC"
                   ItemSize="60" />
</Grid>            
</ContentPage>
```

```
private async void ListView_ItemDragging(object sender, ItemDraggingEventArgs e)
{
  var viewModel = this.listView.BindingContext as ViewModel;

  if (e.Action == DragAction.Start)
  {
    viewModel.IsVisible = true;
    this.stackLayout.Opacity = 0.25;
  }

  if(e.Action == DragAction.Dragging)
  {
    var position = new Point(e.Position.X - this.listView.Bounds.X, e.Position.Y - this.listView.Bounds.Y);
    if (this.stackLayout.Bounds.Contains(position))
      this.deleteLabel.TextColor = Color.Red;
    else
      this.deleteLabel.TextColor = Color.White;
  }

  if(e.Action == DragAction.Drop)
  {
    var position = new Point(e.Position.X - this.listView.Bounds.X, e.Position.Y - this.listView.Bounds.Y);
    if (this.stackLayout.Bounds.Contains(position))
    {
      await Task.Delay(100);
      viewModel.ToDoList.Remove(e.ItemData as ToDoItem);
    }
    viewModel.IsVisible = false;
    this.deleteLabel.TextColor = Color.White;
    this.headerLabel.IsVisible = true;
  }
}
```
To know more about DragAndDrop in ListView, please refer our documentation [here](https://help.syncfusion.com/xamarin/sflistview/item-drag-and-drop)

