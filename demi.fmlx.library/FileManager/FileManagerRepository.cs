using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;


namespace demi.fmlx.library.FileManager
{
    public class FileManagerRepository
    {

        private XmlTextWriter creator = new XmlTextWriter("ItemRepository.xml", System.Text.Encoding.UTF8);

        /// <summary>
        /// Initialize the repository for use
        /// </summary>
        public void Initialize()
        {
            creator.WriteStartDocument(true);
            creator.Formatting = Formatting.Indented;
            creator.Indentation = 2;
            creator.WriteStartElement("table");
            creator.WriteEndElement();
            creator.WriteEndDocument();
            creator.Close();
        }

        /// <summary>
        /// Store an item to the repository.
        /// Parameter itemType is used to differentiate JSON or XML
        /// 1 = itemContent is a JSON string
        /// 2 = itemContent is an XML string
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemContent"></param>
        /// <param name="itemType"></param>
        public void Register(string itemName, string itemContent, int itemType)
        {
            bool checkItemExist = CheckingExistingElement(itemContent, itemType);

            if (!checkItemExist)
            {
                XElement xml = XElement.Load("ItemRepository.xml");
                xml.AddFirst(new XElement("ItemList",
                    new XAttribute("Name", itemName),
                    new XAttribute("Content", itemContent),
                    new XAttribute("Type", itemType)
                    ));
                xml.Save("ItemRepository.xml");
                Console.WriteLine("Add Item Completed ...!");
            }
            else
            {
                Console.WriteLine("Add Item Failed ...!");
            }
        }

        /// <summary>
        /// Retrieve an item from the repository
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public string Retrieve(string itemName)
        {
            string result = string.Empty;

            XmlReader xmlFile;
            xmlFile = XmlReader.Create("ItemRepository.xml", new XmlReaderSettings());
            DataSet dtSet = new DataSet();
            DataView dtView;
            dtSet.ReadXml(xmlFile);

            dtView = new DataView(dtSet.Tables[0]);
            dtView.Sort = "Name";
            int index = dtView.Find(itemName);

            if (index == -1)
            {
                Console.WriteLine("Item Not Found");
            }
            else
            {
                Console.WriteLine(dtView[index]["Name"].ToString() + " " + dtView[index]["Content"].ToString() + " " + dtView[index]["Type"].ToString());

                result = dtView[index]["Name"].ToString() + " " + dtView[index]["Content"].ToString() + " " +
                         dtView[index]["Type"].ToString();

            }

            return result;
        }

        /// <summary>
        /// Retrieve the type of the item (JSON or XML)
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public int GetType(string itemName)
        {
            int result = 0;

            XmlReader xmlFile;
            xmlFile = XmlReader.Create("ItemRepository.xml", new XmlReaderSettings());
            DataSet dtSet = new DataSet();
            DataView dtView;
            dtSet.ReadXml(xmlFile);

            dtView = new DataView(dtSet.Tables[0]);
            dtView.Sort = "Name";
            int index = dtView.Find(itemName);

            if (index == -1)
            {
                Console.WriteLine("Item Not Found");
            }
            else
            {
                Console.WriteLine(dtView[index]["Name"].ToString() + " " + dtView[index]["Content"].ToString() + " " + dtView[index]["Type"].ToString());

                result = Convert.ToInt32(dtView[index]["Type"]);

            }

            return result;
        }

        /// <summary>
        /// Remove an item from the repository
        /// </summary>
        /// <param name="itemName"></param>
        public void Deregister(string itemName)
        {
            //open selected file
            XDocument xml = XDocument.Load("ItemRepository.xml");


            //Find section
            XElement findElement =
                xml.Root.Elements("ItemList").Where(item => item.Attribute("Name").Value == itemName).FirstOrDefault();

            if (findElement == null)
            {
                Console.WriteLine("Delete item failed ...!");
            }
            else if (findElement.HasAttributes)
            {
                xml.Root.Elements("ItemList")
                .Where(item => item.Attribute("Name").Value == itemName)
                .FirstOrDefault()
                .Remove();

                xml.Save("ItemRepository.xml");

                Console.WriteLine("Item Deleted ...!");
            }

        }

        /// <summary>
        /// Construct Node in XML file
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemContent"></param>
        /// <param name="itemType"></param>
        /// <param name="creator"></param>
        private void ConstructNode(string itemName, string itemContent, int itemType, XmlTextWriter creator)
        {
            creator.WriteStartElement("ItemList");
            creator.WriteStartElement("ItemName");
            creator.WriteString(itemName);
            creator.WriteEndElement();
            creator.WriteStartElement("ItemContent");
            creator.WriteString(itemContent);
            creator.WriteEndElement();
            creator.WriteStartElement("ItemType");
            creator.WriteString(itemType.ToString());
            creator.WriteEndElement();
            creator.WriteEndElement();
        }

        private bool CheckingExistingElement(string itemContent, int itemType)
        {
            bool result = false;

            try
            {
                //open selected file
                XDocument xml = XDocument.Load("ItemRepository.xml");


                //Find section
                XElement findElement =
                    xml.Root.Elements("ItemList").Where(item => item.Attribute("Content").Value == itemContent && item.Attribute("Type").Value == itemType.ToString() ).FirstOrDefault();

                if (findElement == null)
                {
                    result = false;
                    Console.WriteLine("Item not found ...!");
                }
                else if (findElement.HasAttributes)
                {
                    result = true;
                    Console.WriteLine("Item found ...!");
                }
            }
            catch (Exception)
            {
                
            }

            return result;
        }
    }
}
