using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DragAndDrop
{
   public class Behavior : Behavior<ContentPage>
    {
        SfListView ListView; Label headerLabel; StackLayout Stack; Label deleteLabel;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");
            headerLabel = bindable.FindByName<Label>("headerLabel");
            Stack = bindable.FindByName<StackLayout>("stackLayout");
            deleteLabel = bindable.FindByName<Label>("deleteLabel");
            ListView.ItemDragging += ListView_ItemDragging;
            base.OnAttachedTo(bindable);
        }

        private async void ListView_ItemDragging(object sender, ItemDraggingEventArgs e)
        {
            /* Delete the drag item when drop into the particular view. */
            var viewModel = this.ListView.BindingContext as ViewModel;

            if (e.Action == DragAction.Start)
            {
                this.headerLabel.IsVisible = false;
                viewModel.IsVisible = true;
            }

            if (e.Action == DragAction.Dragging)
            {
                var position = new Point(e.Position.X - this.ListView.Bounds.X, e.Position.Y - this.ListView.Bounds.Y);
                if (this.Stack.Bounds.Contains(position))
                    this.deleteLabel.TextColor = Color.Red;
                else
                    this.deleteLabel.TextColor = Color.White;
            }

            if (e.Action == DragAction.Drop)
            {
                var position = new Point(e.Position.X - this.ListView.Bounds.X, e.Position.Y - this.ListView.Bounds.Y);
                if (this.Stack.Bounds.Contains(position))
                {
                    await Task.Delay(100);
                    viewModel.ToDoList.Remove(e.ItemData as ToDoItem);
                }
                viewModel.IsVisible = false;
                this.deleteLabel.TextColor = Color.White;
                this.headerLabel.IsVisible = true;
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            ListView.ItemDragging -= ListView_ItemDragging;
            ListView = null;
            headerLabel = null;
            deleteLabel = null;
            Stack = null;
            base.OnDetachingFrom(bindable);
        }

    }
}
