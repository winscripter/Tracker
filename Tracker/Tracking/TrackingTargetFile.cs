using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Tracker.Tracking.Format;

namespace Tracker.Tracking
{
    public sealed class TrackingTargetFile
    {
        public static readonly TrackingTargetFile Empty = new(
            new TrackingModel()
            {
                Tables = []
            }
        );

        public TrackingModel Model { get; set; }

        private TrackingTargetFile(TrackingModel model)
        {
            Model = model;
        }

        public static string WriteXml(TrackingTargetFile trackingTargetFile)
        {
            var stringBuilder = new StringBuilder();
            var xw = XmlWriter.Create(stringBuilder);
            xw.WriteStartElement("trackingTarget");

            WriteTables(xw, trackingTargetFile.Model.Tables);

            xw.WriteEndElement();
            xw.Flush();

            return stringBuilder.ToString();
        }

        private static void WriteTables(XmlWriter xw, IEnumerable<TrackingTableModel> tables)
        {
            xw.WriteStartElement("tables");

            foreach (var table in tables)
            {
                WriteTable(xw, table);
            }

            xw.WriteEndElement();
        }

        private static void WriteTable(XmlWriter xw, TrackingTableModel table)
        {
            xw.WriteStartElement("table");

            xw.WriteAttributeString("name", table.Name);
            xw.WriteAttributeString("color", table.PrimaryHexColor);

            foreach (var member in table.Members)
            {
                WriteMember(xw, member);
            }

            xw.WriteEndElement();
        }

        private static void WriteMember(XmlWriter xw, TrackingMemberModel model)
        {
            xw.WriteStartElement("member");

            xw.WriteAttributeString("name", model.Name);
            xw.WriteAttributeString("done", model.Done.ToString());

            WriteValue(xw, model.Value!);

            xw.WriteEndElement();
        }

        private static void WriteValue(XmlWriter xw, TrackingValueModel model)
        {
            xw.WriteStartElement("value");

            xw.WriteAttributeString("percentage", model.Percentage.ToString());
            xw.WriteAttributeString("color", model.HexColor);

            xw.WriteEndElement();
        }

        public static TrackingTargetFile Parse(string xml)
        {
            var doc = XDocument.Parse(xml);
            var rootElement = doc.Root ?? throw new TrackingException("Root element is missing");

            if (rootElement.Name != "trackingTarget")
                throw new TrackingException("Root element name must be trackingTarget");

            XElement? element = rootElement.Element("tables") ?? throw new TrackingException($"Root element must have one descendant tag named 'tables'");

            return ParseCore(element);
        }

        private static TrackingTargetFile ParseCore(XElement element)
        {
            IEnumerable<XElement> elements = element.Elements();
            return new TrackingTargetFile(new TrackingModel()
            {
                Tables = elements.Where(e => e.Name == "table").Select(ReadTableModel).ToList()
            });
        }

        private static TrackingTableModel ReadTableModel(XElement element)
        {
            string name = element.Attribute("name")?.Value ?? throw new TrackingException("Missing attribute 'name'");
            string color = element.Attribute("color")?.Value ?? throw new TrackingException("Missing attribute 'color'");
            List<TrackingMemberModel> members = element.Elements().Where(m => m.Name == "member").Select(ReadMember).ToList();

            return new()
            {
                Name = name,
                PrimaryHexColor = color,
                Members = members
            };
        }

        private static TrackingMemberModel ReadMember(XElement element)
        {
            if (element.Name != "member")
            {
                throw new TrackingException("Element name must be 'member'");
            }

            string name = element.Attribute("name")?.Value ?? throw new TrackingException("Missing attribute 'name'");
            bool done = bool.Parse(element.Attribute("done")?.Value ?? throw new TrackingException("Missing attribute 'done'"));
            TrackingValueModel model = ReadValue(element.Element("value") ?? throw new TrackingException("Missing 'value' descendant element"));

            return new()
            {
                Done = done,
                Name = name,
                Value = model
            };
        }

        private static TrackingValueModel ReadValue(XElement element)
        {
            if (element.Name != "value")
            {
                throw new TrackingException("Element name must be 'value'");
            }

            int percentage = int.Parse(element.Attribute("percentage")?.Value ?? throw new TrackingException("Missing attribute 'percentage'"));
            string color = element.Attribute("color")?.Value ?? throw new TrackingException("Missing attribute 'color'");

            return new() { Percentage = percentage, HexColor = color };
        }
    }
}
