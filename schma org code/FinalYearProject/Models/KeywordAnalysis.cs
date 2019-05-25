using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalYearProject.Models
{
   public class KeywordAnalysis
    {
        public string save_amount { get; set;}
        public string off_amount { get; set; }
        public string end_date { get; set; }
        public string eligible_amount { get; set; }
        public string sitewide { get; set; }
        public string freeshipping { get; set; }
        public string code { get; set; }
        public int offerid { get; set; }
        public string offerdescription { get; set; }


        public KeywordAnalysis receiver(string description)
        {
            description = System.Text.RegularExpressions.Regex.Replace(description, @"\s+", " ");
            description = description.ToUpper();

            if (description.Contains("SAVE"))
            {

                var a = find_save(description);
                if (a != null)
                {
                    save_amount = a;
                }
            }


            if (description.Contains("OVER"))
            {
                var a = find_over(description);

                if (a != null)
                {
                    eligible_amount = a;
                }
            }

            if (description.Contains("OFF"))
            {
                var a = find_off(description);
                if (a != null)
                {
                    off_amount = a;
                }
            }

            if (description.Contains(" ENDS "))
            {
                var a = find_ends(description);

                if (end_date != null)
                {
                    end_date = a;
                }

            }

            if (description.Contains("SITEWIDE"))
            {

                var a = find_sitewide(description);
                if (a != null)
                {
                    sitewide = a;
                }
            }

            if (description.Contains("FREE SHIPPING"))
            {
                var a = find_freeshipping(description);
                if (a != null)
                {
                    freeshipping = a;
                }
            }

            if (description.Contains("CODE"))
            {
                var a = find_code(description);
                if (a != null)
                {
                    code = a;
                }
            }

            return this;
        }






        public static string find_save(string description)
        {

            var a = description.Split();
            var b = a.ToList();
            var index = b.IndexOf("SAVE");
            if (description.Contains("UP TO")) { index += 3; }
            else { index += 1; }
            if (index >= 0 && index < b.Count())
            {
                string Saved_amount = b[index];
                return Saved_amount;
            }

            else
            {

                return null;
            }

        }


        public static string find_off(string description)
        {

            var a = description.Split();
            var b = a.ToList();
            var index = b.IndexOf("OFF");
            if (index > 0 && index < b.Count())
            {

                var Saved_amount = b[index - 1];
                return Saved_amount;

            }

            else
            {
                return null;
            }

        }


        public static string find_over(string description)
        {

            var a = description.Split();
            var b = a.ToList();
            var index = b.IndexOf("OVER");

            if (index >= 0 && index < (b.Count() - 1))
            {

                var Eligible_amount = b[index + 1];
                return Eligible_amount;
            }

            else
            {
                return null;
            }


        }


        public static string find_code(string description)
        {

            var a = description.Split();
            var b = a.ToList();
            var index = b.IndexOf("CODE");
            if (index < 0)
            {
                index = b.IndexOf("CODE:");
            }
            if (index >= 0 && index < (b.Count() - 1))
            {
                return (b[index + 1]);
            }

            else { return null; }

        }


        public static string find_freeshipping(string description)
        {

            var a = description.Split();
            var b = a.ToList();
            var index = b.IndexOf("FREE");
            if (index >= 0 && index < (b.Count() - 1))
            {
                if (b[index + 1].Equals("SHIPPING"))
                {
                    return ("free shipping");
                }
                else { return null; }
            }

            else { return null; }
        }

        public static string find_sitewide(string description)
        {
            if (description.Contains("SITEWIDE"))
            {
                return "sitewide";
            }

            else return null;
        }


        public static string find_ends(string description)
        {

            var a = description.Split();
            var b = a.ToList();
            var index = b.IndexOf("ENDS");
            if (index >= 0 && index < (b.Count() - 1))
            {
                return b[index + 1];
            }

            else
            {
                return null;
            }
        }




    }
}
