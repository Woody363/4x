using Microsoft.EntityFrameworkCore;

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

                // String errorFolderPath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ErrorLog"]).ToString();
                //AppDomain.CurrentDomain.BaseDirectory + fileName
                //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory))
                // {

                /*
                 may want to replace with this
                Log4Net

                Smtp Logger:
                 
                 */
                String filePath = String.Format("{0}ErrorLogs\\{1}.txt", AppDomain.CurrentDomain.RelativeSearchPath,DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss"));

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