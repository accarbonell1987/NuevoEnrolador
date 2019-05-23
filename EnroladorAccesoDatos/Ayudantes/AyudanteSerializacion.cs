using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EnroladorAccesoDatos.Ayudantes {
    public static class AyudanteSerializacion<T> {

        public static T Clone(T t) {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            xs.Serialize(ms, t);
            ms.Position = 0;
            
            return (T)xs.Deserialize(ms);
        }
    }
}
