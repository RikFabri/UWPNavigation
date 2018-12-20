using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPNavigation
{
    class XmlEntry
    {
        public static List<Sprite> sprites = new List<Sprite>();
        int x, y, width, height;
        int SpriteIndex;
        string Name;
        public XmlEntry(int x, int y, Canvas canvas, WriteableBitmap bimg, string name)
        {
            this.Name = name;
            this.x = x;
            this.y = y;
            Sprite sprite = new Sprite(canvas, x, y);
            sprite.AddToCanvas();
            sprite.setSource(bimg);
            this.width = (int)sprite.width;
            this.height = (int)sprite.height;
            sprites.Add(sprite);
            SpriteIndex = sprites.Count - 1;
        }
        public void WriteDataToXML(XmlDocument xmlDoc, XmlElement rootElement)
        {
            XmlElement root = rootElement;
            XmlElement tex = xmlDoc.CreateElement("SubTexture");
            tex.SetAttribute("name", Name);
            tex.SetAttribute("x", x.ToString());
            tex.SetAttribute("y", y.ToString());
            tex.SetAttribute("width", width.ToString());
            tex.SetAttribute("height", height.ToString());
            root.AppendChild(tex);
        }
        public static double getHeight()
        {
            double BiggestHeight = 0;
            foreach(Sprite i in sprites)
            {
                double height = i.y + i.height;
                if (height > BiggestHeight)
                {
                    BiggestHeight = height;
                }
            }
            return BiggestHeight;
        }
        public static double getWidth()
        {
            double BiggestWidth = 0;
            foreach (Sprite i in sprites)
            {
                double width = i.x + i.width;
                if (width > BiggestWidth)
                {
                    BiggestWidth = width;
                }
            }
            return BiggestWidth;
        }
    }
}
