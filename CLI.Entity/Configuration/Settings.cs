using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Entity.Configuration
{
    /// <summary>
    /// Class that represents the appsettings.json file
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// The base api base url
        /// </summary>
        public string ApiBaseUrl { get; set; }
    }
}
