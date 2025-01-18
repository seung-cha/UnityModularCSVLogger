using UnityEngine;

namespace CSVLogger.Fields
{
    public enum RotationOption
    {
        Euler,
        Quaternion
    };

    public enum EulerRepresentationOption
    {
        Radian,
        Degree,
    };

    public enum VectorRepresentationOption
    {
        Global,
        Local,
    }

    public enum AxisOption
    {
        Up,
        Down,
        Left,
        Right,
        Forward,
        Back,
    };

    public enum TransformReferenceOption
    {
        Difference,
        DifferenceNormalised,
        Distance,
        DistanceSquare,
    }

    [System.Serializable]
    public abstract class ItemField
    {
        public ItemField() { }

        /// <summary>
        /// Override this for multi-field data types (e.g Vector3).
        /// </summary>
        protected virtual string Header {  get => $"\"{header}\""; }

        /// <summary>
        /// Capture data
        /// </summary>
        protected abstract string Data { get; }
      
        /// <summary>
        /// Called in Start() to initialise references and variables.
        /// </summary>
        /// <param name="parent"></param>
        public virtual void Initialise(LoggingItem parent, Logger logger)
        {
            this.parent = parent;

            if(parent.Prefix.Length != 0)
            {
                header = $"{parent.Prefix}{logger.PrefixDelimiter}{header}";
            }
        }

        public void WriteHeader(Logger logger, bool lastField)
        {
  
            if(lastField)
            {
                logger.WriteLine(Header);
            }
            else
            {
                logger.WriteDelimited(Header);
            }
            
        }

        public void WriteData(Logger logger, bool lastField)
        {
            if(lastField)
            {
                logger.WriteLine(Data);
            }
            else
            {
                logger.WriteDelimited(Data);
            }
        }


        [SerializeField]
        protected string header;
        protected LoggingItem parent;
    }

}