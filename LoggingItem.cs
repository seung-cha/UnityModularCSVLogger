using System.Collections.Generic;
using UnityEngine;

namespace CSVLogger
{
    [System.Serializable]
    public class LoggingItem
    {
        public void Initialise(Logger logger)
        {
            foreach(var field in fields)
            {
                field.Initialise(this, logger);
            }
        }

        public void WriteHeader(Logger logger, bool lastItem)
        {
            if (fields.Count == 0) return;

            for (int i = 0; i < fields.Count - 1; i++)
            {
                fields[i].WriteHeader(logger, false);
            }

            fields[fields.Count - 1].WriteHeader(logger, lastItem);
        }

        public void WriteData(Logger logger, bool lastItem)
        {
            if (fields.Count == 0) return;

            for (int i = 0; i < fields.Count - 1; i++)
            {
                fields[i].WriteData(logger, false);
            }

            fields[fields.Count - 1].WriteData(logger, lastItem);
        }

        public string Prefix { get => prefix; }
        public GameObject Reference { get => reference; }
        public List<Fields.ItemField> Fields { get => fields; }

        [SerializeField]
        private string prefix;

        [SerializeField]
        private GameObject reference;

        [SerializeReference]
        private List<Fields.ItemField> fields;

    }
}