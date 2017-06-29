using MusiUploaderWeb.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;

namespace MusiUploaderWeb.Controllers
{
    public class XmlGeneratorController : Controller
    {
        public ActionResult Index()
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("ern", "http://ddex.net/xml/ern/382");
            XmlSerializer writer = new XmlSerializer(typeof(NewReleaseMessage));

            var overview = new NewReleaseMessage();
            var header = new NewReleaseMessageMessageHeader();
            overview.ReleaseProfileVersionId = "CommonReleaseTypesTypes/13/AudioAlbumMusicOnly";
            overview.MessageSchemaVersionId = "ern/382";
            header.MessageThreadId = "3243534";
            header.MessageId = "444";
            overview.MessageHeader = header;
            var encoding = Encoding.GetEncoding("UTF-8");

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";

            using (StreamWriter sw = new StreamWriter(path, false, encoding))
            {
                writer.Serialize(sw, overview, ns);
            }
            
            return View();
        }
    }
}