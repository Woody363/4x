using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace reactprogress2.FileHandler
{
    public static class FileHandler
    {
        public static void WriteExceptionFile(Exception exception)
        {
            //were not going to email ourselves errors yet so i thought this may do 

            string logfile = String.Empty;
            try
            {

            
                /*
                 may want to replace with this
                Log4Net

                Smtp Logger:
                 
                 */

                String filePath = String.Format("{0}ErrorLogs{1}{2}.txt",
                 AppDomain.CurrentDomain.RelativeSearchPath,
                (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) ?  "/" : "\\",
                 DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"));
  
              
                using (var writer = new StreamWriter(filePath, true))
                    {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date : " + DateTime.Now.ToString());
                    writer.WriteLine();

                    while (exception != null)
                    {
                        writer.WriteLine(exception.GetType().FullName);
                        writer.WriteLine("Message : " + exception.Message);
                        writer.WriteLine("StackTrace : " + exception.StackTrace);

                        exception = exception.InnerException;
                    }
                }
               // }
            }
            catch (Exception e)
            {
                //Big Error
                throw e;
               

            }
        }
    }
}