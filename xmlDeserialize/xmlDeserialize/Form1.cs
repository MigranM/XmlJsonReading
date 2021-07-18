using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json;

namespace xmlDeserialize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            #region Data
            string xmlData = "<root><tag1>someData</tag1><tag2>someData1</tag2></root>";  //дату преобразуем в xml, убираем пробелы перед именами тегов если есть
            string jsonData = "{ \"millis\": \"1000\",\"stamp\": \"1273010254\",\"datetime\": \"2010/5/4 21:57:34\"}";
            #endregion

            #region ReadingXml
            byte[] tmpData = Encoding.Default.GetBytes(xmlData); //запихиваем строку в массив байтов
            MemoryStream MS = new MemoryStream(tmpData); //запихиваем строку в поток данных
            var xml = XElement.Load(MS); //загружает из потока xml
            var tag = xml.Elements().Where(elem => elem.Name.LocalName.Equals("tag2")); //поиск по тегу
            label1.Text = tag.First().Value.ToString(); //возврат значения определенного тега
            #endregion

            #region ReadingJson
            dynamic array = JsonConvert.DeserializeObject(jsonData); //десериализуем json в дайнемик объект
            foreach(var a in array)
            {
                if(a.Name == "millis") //обход json и поиск аттрибута с заданным именем
                {
                    label2.Text = a.Value; //возврат значения аттрибута
                }
            }
            #endregion
        }

    }
}
