using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bend.Util;

namespace OutlookCalendarServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpServer httpServer;
            if (args.GetLength(0) > 0)
            {
                httpServer = new OutlookCalendarHttpServer(Convert.ToInt16(args[0]));
            }
            else
            {
                httpServer = new OutlookCalendarHttpServer(8777);
            }
            Thread thread = new Thread(new ThreadStart(httpServer.listen));
            thread.Start();
        }
    }
}
