using System;
using SQLite;

namespace MyDiet.Models
{
    public class DietRecord
    {
        public DietRecord()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int DietId
        {
            get; set;
        }

        public int UserId { get; set; }

        public String Description { get; set;  }

        public int Calories { get; set;  }

        public String Image { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public void SetTime(){

            Date=Date.AddHours(Time.Hours);
            Date=Date.AddMinutes(Time.Minutes);

        }
    }
}
