using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManagement.models
{
    public class Appointment : Item
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        private DateTimeOffset _boundstart;

        public DateTimeOffset Boundstart
        {
            get
            {
                return _boundstart;
            }
            set
            {
                _boundstart = value;
                Start = _boundstart.DateTime;

            }
        }

        private DateTimeOffset _boundend;

        public DateTimeOffset Boundend
        {
            get
            {
                return _boundend;
            }
            set
            {
                _boundend = value;
                End = _boundend.DateTime;

            }
        }

        public List<string> Attendees { get; set; }

        public Appointment()
        {
            Boundstart = DateTime.Today;
            Boundend = DateTime.Today.AddDays(1);
            Attendees = new List<string>();
        }

        public override string ToString()
        {
            return $"{Name} {Description} From {Start} to {End} Priority: {Priority}";
        }
    }
}
