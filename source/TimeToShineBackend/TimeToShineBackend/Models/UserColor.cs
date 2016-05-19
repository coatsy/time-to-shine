using System;

namespace TimeToShineBackend.Models
{
    public class UserColor
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public bool? Approved { get; set; }
        public DateTime? Submitted { get; set; }


        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public UserColor()
        {
            Submitted = DateTime.Now;
        }
    }
}