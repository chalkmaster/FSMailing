using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace FSMailing.Core.Infrastructure
{
    [Serializable]
    public abstract class XmlSerializable<T>
    {

        #region IXmlSerializable members
        /// <summary>
        /// Serializa uma instância em formato XML
        /// </summary>
        /// <returns>string</returns>
        public virtual string ToXml(string path)
        {
            var xmlSerializer = new XmlSerializer(GetType());
            var stringWriter = new StringWriter();

            xmlSerializer.Serialize(stringWriter, this);
            var xml = stringWriter.ToString();

            var file = new StreamWriter(path);
            file.Write(xml);
            file.Close();

            return xml;
        }

        public virtual T Load(string path)
        {
            var xmlSerializer = new XmlSerializer(GetType());
            var xmlReader = new StreamReader(path);
            return (T)xmlSerializer.Deserialize(xmlReader);
        }
        #endregion

    }
}
