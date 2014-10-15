﻿using System.Collections.Generic;
using System.Linq;
using TaskProcessor.Contract.Hub;
using TaskProcessor.Contract.Queue;
using TaskProcessor.Contracts;
using TaskProcessor.Contracts.Queue;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Hub {
    [Export(typeof(QueueHub))]
    public class QueueHub : Microsoft.AspNet.SignalR.Hub, IQueueHub {
        private readonly IQueueManager _queueManager;

        [Import]
        public QueueHub(IQueueManager queueManager) {
            _queueManager = queueManager;
        }

        public IEnumerable<ITaskQueue> GetAll() {
            return _queueManager.Queues;
        }

        public ITaskQueue GetByName(string name) {
            return _queueManager.Queues.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<ITaskQueue> FindByName(string name) {
            return _queueManager.Queues.Where(x => x.Name.Contains(name));
        }

        public ITaskQueue Create(string name) {
            return _queueManager.Create(name);
        }
    }
}
