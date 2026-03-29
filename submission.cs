using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;

// Andrey Luzhnov 1232821085

namespace ConsoleApp1
{
    public class Submission
    {
        public static string xmlURL = "https://Andrey-Luzhnov.github.io/cse445_AndreyLuzhnov/NationalParks.xml";
        public static string xmlErrorURL = "https://Andrey-Luzhnov.github.io/cse445_AndreyLuzhnov/NationalParksErrors.xml";
        public static string xsdURL = "https://Andrey-Luzhnov.github.io/cse445_AndreyLuzhnov/NationalParks.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);

            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);

            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1 - XML Validation
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            string errors = "";
            try
            {
                XmlSchemaSet schemas = new XmlSchemaSet();
                schemas.Add(null, xsdUrl);
                schemas.Compile();

                XmlDocument doc = new XmlDocument();
                doc.Schemas = schemas;

                doc.Load(xmlUrl);

                doc.Validate((sender, e) =>
                {
                    errors += e.Message + "\n";
                });

                if (string.IsNullOrEmpty(errors))
                    return "No errors are found";
                return errors.Trim();
            }
            catch (XmlException ex)
            {
                return "XML Error: " + ex.Message;
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        // Q2.2 - XML to JSON
        public static string Xml2Json(string xmlUrl)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            return jsonText;
        }
		
		private static string DownloadContent(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}