using UnityEngine;
using System;

namespace CSVLogger.Fields.TimeMeasure
{

    public class DateTimeField : ItemField
    {
        [SerializeField]
        private DateTimeOption timeOption;

        [SerializeField]
        private string toStringFormat = "HH:MM:ss.FF";

        public DateTimeField() : base()
        {
            header = "Date Time";
        }
        protected override string Data
        {
            get
            {
                DateTime time;

                if(timeOption == DateTimeOption.LocalTime)
                {
                    time = DateTime.Now;
                }
                else
                {
                    time = DateTime.UtcNow;
                }

                return $"\"{time.ToString(toStringFormat)}\"";
            }
        }
    }

}