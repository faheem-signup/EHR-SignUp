using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.AzureFileDto
{
   public class BlobDto
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string ContentType { get; set; }
    }
}
