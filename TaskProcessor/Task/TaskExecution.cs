﻿using System;
using System.Collections.Generic;
using TaskProcessor.Contract;
using TaskProcessor.Contract.Task;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Task {
    [Export(typeof(ITaskExecution))]
    public class TaskExecution : ITaskExecution {
        private readonly ITask _task;
        private TaskStatus _status;
        private readonly IList<ILog> _logs;

        public TaskExecution(ITask task) : this(task, DateTime.Now) { }

        public TaskExecution(ITask task, ITaskConfiguration configuration = null) : this(task, DateTime.Now, configuration) { }

        public TaskExecution(ITask task, DateTime startTime, ITaskConfiguration configuration = null) {
            _task = task;
            _logs = new List<ILog>();

            TaskExecutionId = Guid.NewGuid();
            StartTime = startTime;
            Configuration = configuration;
            Status = TaskStatus.Initial;
        }

        #region ITaskExecution implementation

        public Guid TaskExecutionId { get; private set; }

        public ITask Task {
            get { return _task; }
        }

        public ITaskConfiguration Configuration { get; private set; }

        public DateTime StartTime { get; set; }

        public TaskStatus Status {
            get { return _status; }
            set {
                _status = value;
                Log(string.Format("Status Changed: {0}", value));
            }
        }

        public string Output { get; set; }

        public IEnumerable<ILog> Logs { get { return _logs; } }

        public void Log(ILog log) {
            _logs.Add(log);
        }

        public void Log(string message) {
            Log(new Log(message));
        }

        public void Log(Exception exception) {
            Log(new Log(exception));
        }

        #endregion
    }
}

