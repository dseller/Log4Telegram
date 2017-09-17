using System;
using log4net;
using log4net.Config;

namespace Example
{
    class Program
    {
        public static void Main(string[] args)
        {
            XmlConfigurator.Configure();

            var logger = LogManager.GetLogger(typeof(Program));
            logger.Debug("Hello World!");

            try
            {
                throw new ArgumentException("Testing something", "parameter");
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception testing", ex);
            }

            // Wait because the log4net logging subsystem handles logging events asynchronously.
            Console.Write("Press enter to close this screen.");
            Console.ReadLine();
        }
    }
}
