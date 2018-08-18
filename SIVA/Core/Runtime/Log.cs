﻿using System;

namespace SIVA.Core.Runtime {
    public class Log {
        public static Log GetLogger() {
            return new Log();
        }

        public void Info(string message) {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("[INFO]: " + message);
        }

        public void Warn(string message) {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[WARN]: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Error(string message) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[ERROR]: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Fatal(string message) {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[FATAL]: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Debug(string message) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[DEBUG]: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintVersion() {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Currently running SIVA V{Version.GetFullVersion()}!");
        }
    }
}