﻿using System;
namespace TaskProcessor.Contracts
{
    /// <summary>
    /// Interface for log entries.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Time when the entry was created.
        /// </summary>
        DateTime Time { get; }

        /// <summary>
        /// Logs message.
        /// </summary>
        string Message { get; }
    }
}
