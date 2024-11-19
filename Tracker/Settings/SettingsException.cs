﻿namespace Tracker.Settings
{
    public class SettingsException : Exception
    {
        public SettingsException()
        {
        }

        public SettingsException(string? message) : base(message)
        {
        }

        public SettingsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
