using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FSMailing.Core.Infrastructure
{
    [XmlRoot("host")]
    [Serializable]
    public class Host: XmlSerializable<Host>
    {
        [XmlElement(ElementName = "HostName",DataType = "string",IsNullable = false,Order = 0   )]
        public string HostName { get; set; }


        [XmlElement(ElementName = "User",DataType = "string",IsNullable = false,Order = 1)]
        public string User { get; set; }


        [XmlElement(ElementName = "Pass",DataType = "string",IsNullable = false,Order = 2)]
        public string Pass { get; set; }
        
        
        [XmlElement(ElementName = "Mail",DataType = "string",IsNullable = false,Order = 3)]
        public string Mail { get; set; }

    }

    [XmlRoot("hosts")]
    [Serializable]
    public class Hosts : XmlSerializable<Hosts>
    {

        [XmlArray("HostList", IsNullable = true, Order = 0)]
        [XmlArrayItem("Host")]
        public List<Host> HostList { get; set; }
    }

}
