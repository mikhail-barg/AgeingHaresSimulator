using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using NLog;

namespace AgeingHaresSimulator.Common
{
    public static class ProgramHelper
    {
        [HandleProcessCorruptedStateExceptions()]
        public static void MainWrapper(Action mainAction)
        {
            if (!Debugger.IsAttached)
            {
                try
                {
                    AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                        if (e.IsTerminating)
                        {
                            Logger logger = LogManager.GetLogger("Program");
                            logger.Error("Application is terminating due to an unhandled exception in a secondary thread.");
                        }
                        LogFatalException(e.ExceptionObject as Exception);
                    };

                    TaskScheduler.UnobservedTaskException += (sender, e) => {
                        Logger logger = LogManager.GetLogger("Program");
                        logger.Error("Got UnobservedTaskException with observed={0}", e.Observed);
                        LogFatalException(e.Exception as Exception);
                    };

                    // Set the unhandled exception mode to force all Windows Forms errors to go through our handler.
                    System.Windows.Forms.Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                    System.Windows.Forms.Application.ThreadException += (sender, e) => {
                        Logger logger = LogManager.GetLogger("Program");
                        logger.Error(e.Exception, "Got Application.ThreadException");
                        LogFatalException(e.Exception);
                    };

                    mainAction();
                }
                catch (Exception e)
                {
                    LogFatalException(e);
                    throw;
                }
            }
            else
            {
                mainAction();
            }
        }


        private static void LogFatalException(Exception e)
        {
            Logger logger = LogManager.GetLogger("Program");
            logger.Fatal(e, "Fatal exception");
            LogManager.Flush();

            Environment.Exit(1);
        }
    }
}
