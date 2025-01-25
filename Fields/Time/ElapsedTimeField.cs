using UnityEngine;

namespace CSVLogger.Fields.TimeMeasure
{
    public class ElapsedTimeField : ItemField
    {
        private float initialTime;
        [SerializeField]
        private TimeMeasureMethodOption option;
       
        public ElapsedTimeField() : base()
        {
            header = "Elapsed Time (s)";
        }

        public override void Initialise(LoggingItem parent, Logger logger)
        {
            base.Initialise(parent, logger);


            if(option == TimeMeasureMethodOption.TimeTime)
            {
                initialTime = Time.time;
            }
            else
            {
                initialTime = Time.unscaledTime;
            }

        }

        protected override string Data
        {
            get
            {
                float t = option == TimeMeasureMethodOption.TimeTime ? Time.time : Time.unscaledTime;
                return (t - initialTime).ToString();
            }
        }
    }
}
