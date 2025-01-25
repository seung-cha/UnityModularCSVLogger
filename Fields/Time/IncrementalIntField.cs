using UnityEngine;

namespace CSVLogger.Fields.TimeMeasure
{
    public class IncrementalIntField : ItemField
    {
        [SerializeField]
        int startingNumber = 0;
        public IncrementalIntField() : base()
        {
            header = "Trial (Incrementing Int)";
        }

        protected override string Data
        {
            get
            {
                return (startingNumber++).ToString();
            }
        }
    }
}