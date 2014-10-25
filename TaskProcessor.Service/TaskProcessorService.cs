﻿using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace TaskProcessor.Service {
    public partial class TaskProcessorService : ServiceBase {
        /// <summary>
        /// Service entry point with integrated cli installer.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            if (args.Any() && args.First() != "") {
                if (args.First() == "install" && ServiceController.GetServices().All(x => x.ServiceName != TaskProcessorServiceName)) {
                    ManagedInstallerClass.InstallHelper(new[] { "/i", Assembly.GetExecutingAssembly().Location });
                }

                if (args.First() == "uninstall" &&  ServiceController.GetServices().Any(x => x.ServiceName == TaskProcessorServiceName)) {
                    ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });
                }
            } else {
                Run(new TaskProcessorService());
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="T:System.ServiceProcess.ServiceBase"/> class.
        /// </summary>
        public TaskProcessorService() {
            ServiceName = TaskProcessorServiceName;

            if (!EventLog.SourceExists(ServiceName)) {
                EventLog.CreateEventSource(ServiceName, "Application");
            }

            EventLog.Source = ServiceName;
        }

        public static string TaskProcessorServiceName = "TaskProcessor";

        protected override void OnStart(string[] args) {
            var thread = new Thread(RunApplication);
            thread.IsBackground = false;
            thread.Start();
        }

        private void RunApplication() {
            EventLog.WriteEntry("RunApplication");
            DI.Container.RegisterAssembly(Assembly.GetAssembly(typeof(IApplication)));
            DI.Container.GetExport<IApplication>();
            EventLog.WriteEntry("RunApplication End");
        }
    }
}
