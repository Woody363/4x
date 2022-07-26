namespace reactprogress2
{
    public class AppSettings
    {

        public ConnectionStringsCS ConnectionStrings { get; set; }
        public LoggingCS Logging { get; set; }

        public class ConnectionStringsCS
        {
            public string WoodyServer { get; set; }

        }
        public class LoggingCS
        {

            public LogLevelCS LogLevel { get; set; }
            public class LogLevelCS
            {
                public string Default { get; set; }
                public string Microsoft { get; set; }
                public string MicrosoftHostingLifetime { get; set; }

            }

        }

    }





}


