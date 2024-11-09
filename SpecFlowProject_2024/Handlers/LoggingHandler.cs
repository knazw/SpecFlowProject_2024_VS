using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.Handlers
{
    internal class LoggingHandler : DelegatingHandler
    {
        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public LoggingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            logger.Info(String.Format("override request.Method - {0} at {1}", request.Method.ToString(), DateTime.Now));
            logger.Info(String.Format("override request.RequestUri - {0} at {1}", request.RequestUri, DateTime.Now));
            Console.WriteLine("override Request:");
            Console.WriteLine("override "+request.ToString());
            if (request.Content != null)
            {
                logger.Info(String.Format("override request.Content - {0} at {1}", await request.Content.ReadAsStringAsync(), DateTime.Now));
                Console.WriteLine("override " + await request.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

            Console.WriteLine("override Response:");
            Console.WriteLine("override " + response.ToString());
            if (response.Content != null)
            {
                logger.Info(String.Format("override response.Content - {0} at {1}", await response.Content.ReadAsStringAsync(), DateTime.Now));
                Console.WriteLine("override " + await response.Content.ReadAsStringAsync());
            }
            Console.WriteLine();

            return response;
        }
    }
}
