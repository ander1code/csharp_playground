using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class WorkingWithDateTimeDateInCSharp
    {
        public static void Principal(String[] args)
        {
            {
                Console.WriteLine(DateTime.Now);      // 9/10/2026 1:23:21 AM
                Console.WriteLine(DateTime.Today);    // 9/10/2026 12:00:00 AM
                Console.WriteLine(DateTime.MaxValue); // 12/31/9999 11:59:59 PM
                Console.WriteLine(DateTime.MinValue); // 1/1/0001 12:00:00 AM
            }

            {
                DateTime date = new DateTime();
                Console.WriteLine(date.Date);        // 1/1/0001 12:00:00 AM
            }

            {
                DateTime date = new DateTime(DateTime.Now.Ticks);
                Console.WriteLine(date);
            }

            {
                DateTime date = new DateTime(2005, 5, 1, 10, 30, 30);
                Console.WriteLine(date);
            }

            {
                TimeSpan timespan = new TimeSpan();
                Console.WriteLine(timespan); // equivale a zero: 00:00:00
            }

            {
                // TimeSpan:
                //     Represents a time interval.
                TimeSpan timespan = new TimeSpan(2,12,0);
                Console.WriteLine(String.Format("{0}: H:{1}, M:{2}, S:{3}", timespan, timespan.Hours, timespan.Minutes, timespan.Seconds));
            }

            // Format Date:
            {
                DateTime date = new DateTime(2005, 5, 1, 10, 30, 30);
                Console.WriteLine(String.Format("{0}: {1}", date.ToShortDateString(), date.ToString("dd/MM/yyyy"))); // 5/1/2005: Sunday, 01/05/2005
                Console.WriteLine(String.Format("{0}: {1}", date.ToShortDateString(), date.ToString("dddd, d MMMM yyyy"))); // 5/1/2005: Sunday, 1 May 2005
                Console.WriteLine(String.Format("{0}: {1}", date.ToShortDateString(), date.ToString("dd/MM/yyyy - HH:mm:tt"))); // 5/1/2005: 01/05/2005 - 10:30:AM
            }

            // UTC
            {
                Console.WriteLine(DateTime.UtcNow);   // 9/10/2026 4:23:21 AM

                // ToUniversalTime():
                //     Converts the value of the current System.DateTime object to Coordinated Universal
                //     Time (UTC).
                Console.WriteLine(DateTime.Now.ToUniversalTime().ToString("dd/MM/yyyy - HH:mm:ss tt")); // 10/09/2026 - 04:50:02 AM
                
                // caminho inverso do anterior: UTC para Local:
                Console.WriteLine(DateTime.UtcNow.ToLocalTime()); // 9/10/2026 1:51:04 AM
            }


            // ZoneInfo (TimeZoneInfo)
            {
                // Unspecified (Custom)
                {
                    DateTime date = new DateTime(2005, 5, 1, 10, 30, 30, DateTimeKind.Unspecified);

                    TimeZoneInfo tzi = null;

                    try
                    {
                        tzi = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"); // Ta certo, mas da erro!
                    }
                    catch (Exception)
                    {
                        tzi = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                    }

                    Console.WriteLine(String.Format("{0}, {1}", date, TimeZoneInfo.ConvertTime(date, tzi)));
                }


                // UTC
                {
                    DateTime date = DateTime.UtcNow;

                    TimeZoneInfo tzi = null;

                    try
                    {
                        tzi = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"); // Ta certo, mas da erro!
                    }
                    catch (Exception)
                    {
                        tzi = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                    }

                    Console.WriteLine(String.Format("{0}, {1}", date, TimeZoneInfo.ConvertTime(date, tzi)));
                }
            }

            // Equivalente ao timedelta do python: TimeSpan
            {
                TimeSpan timedelta = TimeSpan.FromDays(200); // mais 200 dias
                DateTime new_date = DateTime.Now + timedelta;
                Console.WriteLine(String.Format("{0}, {1}", DateTime.Now, new_date));
            }

            // CultureInfo
            {
                // CultureInfo:
                //     Provides information about a specific culture (called a locale for unmanaged
                //     code development). The information includes the names for the culture, the
                //     writing system, the calendar used, the sort order of strings, and formatting
                //     for dates and numbers.
                CultureInfo culture = new CultureInfo("pt-BR");
                Console.WriteLine(DateTime.Now.ToString(culture)); // 10/09/2026 02:17:13
                Console.WriteLine(DateTime.Now.ToString("D", culture)); // quinta-feira, 10 de setembro de 2026

                // plus: uso do CultureInfo com currency:
                float salary = 1512.54f;
                Console.WriteLine(String.Format("Default: {0:C}, BR: {1:C}", salary, salary.ToString("C", culture))); // Default: $1,512.54, BR: R$ 1.512,54
            }
            
            Console.ReadKey();
        }
    }
}
