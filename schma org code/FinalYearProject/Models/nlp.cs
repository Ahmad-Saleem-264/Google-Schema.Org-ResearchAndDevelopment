using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using edu.stanford.nlp.ie.crf;
namespace FinalYearProject.Models
{
    public class nlp
    {
        public string getNER(string S)
        {
            CRFClassifier Classifier = CRFClassifier.getClassifierNoExceptions(@"C:\english.all.3class.distsim.crf.ser.gz");

             //S = "David go to school at Stanford University, which is located in California.";
            string S3 = S.Trim(new Char[] { ',', '.' });
            string S2 = S3.Replace(@",", "");
            //  Console.WriteLine(S2);
            String classify = Classifier.classifyToString(S2);
            string[] words = classify.Split(' ');
            string result = "";
            //List<String> iList = new List<String>();ctory 

            //List<String> iList = new List<String>();
            foreach (string s in words)
            {

                if (!s.EndsWith("/O"))
                {
                    //System.Console.WriteLine(s);
                    result = result + s + "\n";
                }

            }

            // Keep the console window open in debug mode.

            return (result);

        }
    }
}