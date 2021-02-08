using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace Falcon.Radio.Utilities
{
    public sealed class ProcessMonitor
    {
        #region Fields

        //  ManagementEventWatcher accepts a query (in our case queries regarding process startup and
        //  termination) and fires an event to an event handler method.

        private ManagementEventWatcher ProcessStartMonitor;

        private ManagementEventWatcher ProcessStopMonitor;

        //  process tracking dictionary associates process IDs with process file names.

        private Dictionary<int, string> TrackedProcesses;

        //  Issued callbacks whenever a process starts or stops.

        private Callback OnProcessStartedCallback;

        private Callback OnProcessStoppedCallback;

        #endregion

        #region Constructors

        //
        //  public ProcessMonitor( Callback, Callback )
        //
        //  When provided with a non-null onProcessStartedCallback, ProcessMonitor will track the startup
        //  of Windows processes, recording the started process ID and its fully qualified filename, and
        //  present that information via the callback function.  Providing an onProcessStoppedCallback will
        //  present the same information when a tracked process stops (however, if the startup wasn't
        //  tracked, the filename will be null).
        //

        public ProcessMonitor(Callback onProcessStartedCallback, Callback onProcessStoppedCallback)
        {
            TrackedProcesses = new Dictionary<int, string>();

            if (onProcessStartedCallback != null)
            {
                ProcessStartMonitor = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStartTrace");
                ProcessStartMonitor.EventArrived += OnProcessStarted;

                OnProcessStartedCallback = onProcessStartedCallback;
            }

            if (onProcessStoppedCallback != null)
            {
                ProcessStopMonitor = new ManagementEventWatcher("SELECT * FROM Win32_ProcessStopTrace");
                ProcessStopMonitor.EventArrived += OnProcessStopped;

                OnProcessStoppedCallback = onProcessStoppedCallback;
            }
        }

        #endregion

        #region Methods

        public void Start()
        {
            if (OnProcessStartedCallback != null) ProcessStartMonitor.Start();
            if (OnProcessStoppedCallback != null) ProcessStopMonitor.Start();
        }


        public void Stop()
        {
            if (OnProcessStartedCallback != null) ProcessStartMonitor.Stop();
            if (OnProcessStoppedCallback != null) ProcessStopMonitor.Stop();
        }


        private void OnProcessStarted(object sender, EventArrivedEventArgs managementEvent)
        {
            string ProcessFilename = null;

            int ProcessID;

            try
            {
                //  Let's attempt to get both the process ID and the fully qualified filename of the new
                //  process. If the extracted process ID exists within the list of running processes, get its
                //  filename. (This could fail with an exception if the process closes in the time it takes
                //  to get the filename, or access is denied because the process was started by the system.)

                ProcessID = Convert.ToInt32(managementEvent.NewEvent.Properties["ProcessID"].Value);

                if (Process.GetProcesses().Any(x => x.Id == ProcessID))
                {
                    ProcessModule processModule = Process.GetProcessById(ProcessID).MainModule;

                    if (processModule != null)
                        ProcessFilename = processModule.FileName;
                }
            }
            catch (Exception)
            {
                return;
            }

            if (ProcessFilename != null)
            {
                //  Start tracking the process by adding it to the list, then pass the process ID and filename
                //  to relevant callback.

                if (!TrackedProcesses.ContainsKey(ProcessID)) TrackedProcesses.Add(ProcessID, ProcessFilename);

                OnProcessStartedCallback(ProcessID, ProcessFilename);
            }
        }

        private void OnProcessStopped(object sender, EventArrivedEventArgs managementEvent)
        {
            try
            {
                //  Let's attempt to get the process ID of the process that we've been informed has stopped
                //  executing and pass the relevant information to the callback function.  Finally, remove
                //  the process from the process list if it exists.               

                int processId = Convert.ToInt32(managementEvent.NewEvent.Properties["ProcessID"].Value);

                OnProcessStoppedCallback(processId,
                    TrackedProcesses.ContainsKey(processId) ? TrackedProcesses[processId] : null);

                if (TrackedProcesses.ContainsKey(processId)) TrackedProcesses.Remove(processId);
            }

            catch (Exception)
            {
                return;
            }
        }

        #endregion

        #region Delegates

        public delegate void Callback(int processID, string processFilename);

        #endregion
    }

    internal sealed class ProcessItem
    {
        #region Fields

        public int ProcessId;

        public string AssociatedProfile;

        #endregion

        #region Constructors

        public ProcessItem(int processId, string associatedProfile)
        {
            ProcessId = processId;
            AssociatedProfile = associatedProfile;
        }

        #endregion
    }
}