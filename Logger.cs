using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



namespace CSVLogger
{
    // Make sure the logger runs after everything is updated
    [DefaultExecutionOrder(int.MaxValue)]
    public class Logger : MonoBehaviour
    {
        public char Delimiter { get => delimiter; }
        public char PrefixDelimiter { get => prefixDelimiter; }
        public string FileName { get => fileName; set => fileName = value; }
        public List<LoggingItem> Items { get => items; }

        /// <summary>
        /// This does not affect the frequency if logging has already started.
        /// </summary>
        public int LogPerSecond { get => logPerSecond; set => logPerSecond = value; }
        public bool LogContinuously { get => logContinuously; set => logContinuously = value; }


        [Header("Settings")]

        [SerializeField]
        private string fileName;

        [Tooltip("CSV files will be generated in this directory")]
        [SerializeField]
        private string editorOutputDir;

        [Tooltip("CSV files will be generated in this directory in the built version")]
        [SerializeField]
        private string buildOutputDir;

        [Tooltip("Delimiter for each logging item.")]
        [SerializeField]
        private char delimiter = ',';

        [Tooltip("Delimiter to append between item prefix and field header")]
        [SerializeField]
        private char prefixDelimiter = '_';

        [Header("Capture Settings")]
        [Tooltip("If logContinuously is false, it will log once in OnStart()")]
        [SerializeField]
        private bool startLoggingOnStart = false;
        [SerializeField]
        private bool logContinuously = false;
        [Tooltip("This does not affect the frequency if logging has already started. Has no effect if logContinuously is false.")]
        [SerializeField]
        private int logPerSecond = 1;


        [Space()]
        [Header("Logging Items")]
        [SerializeField]
        private List<LoggingItem> items = new();

        private StreamWriter writer;
        private bool initialised = false;
        private Coroutine logHandle = null;


        /// <summary>
        /// Begin logging data and log once. If logContinuously is set to true, it will start logging continuously.
        /// Close the writer by calling FinishLogging() or destroying this object.
        /// </summary>
        public void StartLogging()
        {
            if(initialised)
            {
                Debug.LogError("StartLogging() is called when logging is already in place. Make sure to call FinishLogging() to terminate first.", this);
                return;
            }

            if(logContinuously)
            {
                if(logPerSecond == 0)
                {
                    Debug.LogError("Logging did not start because logPerSecond is set to 0. Make sure this number is larger than 1.", this);
                    return;
                }


                InitialiseLogger();
                logHandle = StartCoroutine(ContinuousLogging(1f / logPerSecond));
            }
            else
            {
                InitialiseLogger();
                Log();
            }
        }

        /// <summary>
        /// Log the data
        /// </summary>
        public void Log()
        {
            if(!initialised)
            {
                Debug.LogError("Log() is called when the logger is not initialised. Make sure to call StartLogging() first.", this);
                return;
            }

            if (items.Count == 0) return;

            for (int i = 0; i < items.Count - 1; i++)
            {
                items[i].WriteData(this, false);
            }

            items[items.Count - 1].WriteData(this, true);
        }

        /// <summary>
        /// Write to the file and append a delimiter.
        /// </summary>
        /// <param name="str"></param>
        public void WriteDelimited(string str)
        {
            if (!initialised)
            {
                Debug.LogError("WriteDelimited() is called when the logger is not initialised. Make sure to call StartLogging() first.", this);
                return;
            }

            writer.Write($"{str}{delimiter}");
        }

        /// <summary>
        /// Write to the file and append a new line
        /// </summary>
        /// <param name="str"></param>
        public void WriteLine(string str)
        {
            if (!initialised)
            {
                Debug.LogError("WriteLine() is called when the logger is not initialised. Make sure to call StartLogging() first.", this);
                return;
            }

            writer.WriteLine(str);
        }

        /// <summary>
        /// Write to the file, without a delimiter and a new line.
        /// </summary>
        /// <param name="str"></param>
        public void Write(string str)
        {
            if (!initialised)
            {
                Debug.LogError("Write() is called when the logger is not initialised. Make sure to call StartLogging() first.", this);
                return;
            }

            writer.Write(str);
        }

        /// <summary>
        /// Finish logging and close the StreamWriter
        /// </summary>
        public void FinishLogging()
        {
            if(!initialised)
            {
                Debug.LogWarning("FinishLogging() is called when logger is not initialised.", this);
                return;
            }

            if(logHandle != null)
                StopCoroutine(logHandle);

            logHandle = null;
            initialised = false;
            writer.Close();
        }

        private void WriteHeader()
        {
            if (items.Count == 0) return;

            for (int i = 0; i < items.Count - 1; i++)
            {
                items[i].WriteHeader(this, false);
            }

            items[items.Count - 1].WriteHeader(this, true);
        }

        private void InitialiseLogger()
        {
            initialised = true;
            string outputDir;

#if UNITY_EDITOR
            outputDir = editorOutputDir;
#else
            outputDir = buildOutputDir;
#endif


            writer = new StreamWriter($"{outputDir}/{FileName}.csv");

            foreach (var item in items)
            {
                item.Initialise(this);
            }

            WriteHeader();
        }

        // Start is called before the first frame update
        void Start()
        {
            if(startLoggingOnStart)
            {
                StartLogging();
            }
        }

        IEnumerator ContinuousLogging(float interval)
        {
            while(true)
            {
                Log();
                yield return new WaitForSecondsRealtime(interval);
            }
        }

        private void OnDestroy()
        {
            FinishLogging();
        }

    }
}