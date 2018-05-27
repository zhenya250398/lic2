using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DataCollectionApp
{
    public class LicenseManager
    {
        private static LicenseManager _instance;

        public static LicenseManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LicenseManager();

                return _instance;
            }
        }

        public bool HasLicense { get; set; }

        public string Licensee { get; set; }

        public DateTime ValidUntil { get; set; }

        private List<string> _enabledFeatures = new List<string>();

        public List<string> EnabledFeatures
        {
            get
            {
                return _enabledFeatures;
            }
        }

        //Код проверяет валидность подписи:
        public bool TryLoadLicense(string fileName)
        {
            if (!File.Exists("public.xml"))
                return false;

            if (!File.Exists(fileName))
                return false;

            string publicKey = File.ReadAllText("public.xml");

            RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider();
            rsaKey.FromXmlString(publicKey);

            // Create a new XML document.
            XmlDocument xmlDoc = new XmlDocument();

            // Load an XML file into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(fileName);

            // Verify the signature of the signed XML.
            bool result = VerifyXml(xmlDoc, rsaKey);
            if (!result)
                return false;

            HasLicense = true;
            LicenseDto dto;
            using (var fileStream = File.OpenRead(fileName))
            {
                dto = (LicenseDto)new XmlSerializer(typeof(LicenseDto)).Deserialize(fileStream);
            }

            Licensee = dto.LicenseeName;
            ValidUntil = dto.ValidUntil;

            if (DateTime.Now > ValidUntil)
                return false;

            if (dto.AllowedFeatures != null)
                foreach (var f in dto.AllowedFeatures)
                {
                    _enabledFeatures.Add(f);
                }

            return true;
        }

        // Verify the signature of an XML file against an asymmetric 
        // algorithm and return the result.
        private Boolean VerifyXml(XmlDocument Doc, RSA Key)
        {
            // Check arguments.
            if (Doc == null)
                throw new ArgumentException("Doc");
            if (Key == null)
                throw new ArgumentException("Key");

            // Create a new SignedXml object and pass it
            // the XML document class.
            SignedXml signedXml = new SignedXml(Doc);

            // Find the "Signature" node and create a new
            // XmlNodeList object.
            XmlNodeList nodeList = Doc.GetElementsByTagName("Signature");

            // Throw an exception if no signature was found.
            if (nodeList.Count <= 0)
            {
                throw new CryptographicException("Verification failed: No Signature was found in the document.");
            }

            // This example only supports one signature for
            // the entire XML document.  Throw an exception 
            // if more than one signature was found.
            if (nodeList.Count >= 2)
            {
                throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }

            // Load the first <signature> node.  
            signedXml.LoadXml((XmlElement)nodeList[0]);

            // Check the signature and return the result.
            return signedXml.CheckSignature(Key);
        }
    }
}
