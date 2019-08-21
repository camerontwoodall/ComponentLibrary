using System;
using System.Collections.Generic;
using System.Text;

namespace ComponentLibrary.Models
{
    //TODO move classes into separate .cs files
    public class CalendarItem<T>
    {
        public DateTime Date { get; set; }
        public T Item { get; set; }
    }

    public class CalendarItemDropEvent<T>
    {
        public CalendarItem<T> Item { get; set; }

        public DateTime NewDate { get; set; }

        public CalendarItemDropEvent(CalendarItem<T> item)
        {
            Item = item;
        }
        public CalendarItemDropEvent(CalendarItem<T> item, DateTime newDate)
        {
            Item = item;
            NewDate = newDate;
        }
    }

    public class DateChangedEvent
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
