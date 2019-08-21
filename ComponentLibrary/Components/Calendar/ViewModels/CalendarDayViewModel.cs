using ComponentLibrary.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ComponentLibrary.Components.Calendar.ViewModels
{
    public class CalendarDayViewModel<T> : ComponentBase
    {
        [CascadingParameter] public Calendar<T> Calendar { get; set; }
        [Parameter] public CalendarItem<T> CurrentDate { get; set; }
        [CascadingParameter] public RenderFragment<CalendarItem<T>> DataTemplate { get; set; }

        public string DropClass = "";

        public void HandleDragStart(CalendarItem<T> item)
        {
            Calendar.DraggedItem = item;
        }

        public void HandleDragEnter()
        {
            DropClass = "can-drop";
        }

        public void HandleDragLeave()
        {
            DropClass = "";
        }

        public async Task HandleDrop()
        {
            DropClass = "";
            var updatedItem = new CalendarItemDropEvent<T>(Calendar.DraggedItem, CurrentDate.Date);
            await Calendar.OnDropAsync(updatedItem);
        }
    }
}
