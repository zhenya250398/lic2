using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataCollectionApp
{
    public class LicenseDto
    {
        private List<string> _allowedFeatures = new List<string>();

        public string LicenseeName { get; set; }

        public DateTime ValidUntil { get; set; }

        public List<string> AllowedFeatures
        {
            get
            {
                return _allowedFeatures;
            }
        }
    }
}
