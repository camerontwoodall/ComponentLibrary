using ComponentLibrary.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComponentLibrary.Components.Calendar.ViewModels
{
    public class CalendarViewModel<T> : ComponentBase
    {
        [Parameter] public List<CalendarItem<T>> DataSource { get; set; }
        [Parameter] public RenderFragment<CalendarItem<T>> DataTemplate { get; set; }
        [Parameter] public EventCallback<CalendarItemDropEvent<T>> OnDropEvent { get; set; }
        [Parameter] public EventCallback<Tuple<DateTime, DateTime>> OnDateChangedEvent { get; set; }
        [Parameter] public int SelectedMonth { get; set; } = DateTime.Now.Month;
        [Parameter] public int SelectedYear { get; set; } = DateTime.Now.Year;
        public List<CalendarItem<T>> Dates { get; set; } = new List<CalendarItem<T>>();
        public CalendarItem<T> DraggedItem { get; set; }

        public async Task OnDropAsync(CalendarItemDropEvent<T> updatedItem)
        {
            await OnDropEvent.InvokeAsync(updatedItem);
            this.StateHasChanged();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            var newDates = UpdateDates();
            OnDateChangedEvent.InvokeAsync(newDates);
        }

        public void UpdateMonth(UIMouseEventArgs e, int i)
        {
            int newMonth = SelectedMonth + i;
            if (newMonth > 12)
            {
                SelectedMonth = 1;
                SelectedYear += i;

            }
            else if (newMonth < 1)
            {
                SelectedMonth = 12;
                SelectedYear += i;
            }
            else
            {
                SelectedMonth += i;
            }
            var newDates = UpdateDates();
            OnDateChangedEvent.InvokeAsync(newDates);
        }

        //TODO Remove Tuple and use DateChangedEvent
        private Tuple<DateTime, DateTime> UpdateDates()
        {
            Dates = new List<CalendarItem<T>>();
            var firstDayOfMonth = new DateTime(SelectedYear, SelectedMonth, 1);
            var daysToSubtract = firstDayOfMonth.DayOfWeek;
            DateTime startOfCalendar = firstDayOfMonth.AddDays((int)daysToSubtract * -1);
            DateTime endOfCalendar = startOfCalendar.AddDays(41);
            for (int i = 0; i < 42; i++)
            {
                Dates.Add(new CalendarItem<T> { Date = startOfCalendar.AddDays(i) });
            }

            return new Tuple<DateTime, DateTime>(startOfCalendar, endOfCalendar);
        }

        //TODO
        public Dictionary<int, string> Months = new Dictionary<int, string>() {
        { 1, "January" },
        { 2, "February"},
        { 3, "March"},
        { 4, "April"},
        { 5, "May"},
        { 6, "June"},
        { 7, "July"},
        { 8, "August"},
        { 9, "September"},
        { 10, "October"},
        { 11, "November"},
        { 12, "December"}
    };
    }
}
