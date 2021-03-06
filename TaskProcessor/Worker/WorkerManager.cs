﻿using System.Collections.Generic;
using TaskProcessor.Contract.Worker;
using TaskProcessor.DI;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.Worker {
    [Export(typeof(IWorkerManager))]
    public class WorkerManager : IWorkerManager {
        public IEnumerable<IWorker> Spawn(int numerOfWorkers) {
            var workers = new List<IWorker>();

            for (var i = 0; i <= numerOfWorkers; i++) {
                workers.Add(Container.GetExport<IWorker>());
            }

            return workers;
        }
    }
}
