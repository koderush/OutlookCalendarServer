using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Outlook;
using Bend.Util;

namespace OutlookCalendarServer
{
    public class OutlookCalendarHttpServer : HttpServer
    {
        private static DateTime EPOCH = new DateTime(1970, 1, 1);

        public OutlookCalendarHttpServer(int port)
            : base(port)
        {
        }
        public override void handleGETRequest(HttpProcessor p)
        {
            if (p.http_url.Equals("/outlookcalendar"))
            {
                StringBuilder sb = new StringBuilder();

                List<AppointmentItem> OutlookEntries = OutlookCalendar.Instance.getCalendarEntriesInRange();
                foreach (AppointmentItem ai in OutlookEntries)
                {
                    sb.Append(ai.Start + "|" + getTimestamp(ai.Start.ToUniversalTime()) + "|" + ai.End + "|" + getTimestamp(ai.End.ToUniversalTime()) + "|" + ai.Subject + "|" + ai.Location + "|\n");
                }

                p.outputStream.BaseStream.Write(Encoding.ASCII.GetBytes(sb.ToString()),0,sb.Length);
                p.outputStream.BaseStream.Flush();
            }
        }

        private long getTimestamp(DateTime dateTime)
        {
            TimeSpan ts = dateTime - EPOCH;

            return (long) ts.TotalSeconds;
        }

        public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
        {
        }
    }
}
