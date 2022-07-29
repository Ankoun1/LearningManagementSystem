using System;

namespace Experiment
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startDate = new DateTime(2022, 7, 9).AddHours(13);
            TimeSpan a = new TimeSpan();
            a = TimeSpan.FromMinutes(32);
            DateTime endDate = DateTime.Now;
            var result = endDate - startDate;
            Console.WriteLine(result);
            Console.WriteLine(endDate);
            Console.WriteLine(startDate);
            if(result >= a)
            {
                Console.WriteLine(false);
            }

            string uriName = @"c/documents/test1.txt";
            Uri uriResult;
            bool resultUrl = Uri.TryCreate(uriName, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            Console.WriteLine("This Url is valid {0}", resultUrl);
            int[] b = { 4, 8 };
            for (int i = 0; i < b.Length; i++)
            {
                Console.WriteLine(b[++i]);
                break;
            }
            //Console.WriteLine(b[1++]);
            Console.ReadKey();
        }
    }
}
