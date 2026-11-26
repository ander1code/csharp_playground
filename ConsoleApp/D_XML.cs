using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp
{
    public class XML
    {
        public static void Principal()
        {
            // Carragando o arquivo na memoria:
            XmlDocument document = new XmlDocument();
            document.Load("people.xml");

            // Gerando o elemento root do XML:
            XmlElement root = document.DocumentElement;
            
            // Nome do root
            Console.WriteLine("Root: {0}", root.Name);

            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    // node.InnerText: Bruno1993-04-175200Inactive
                    Console.WriteLine(node.InnerText);
                }
            }

            Console.WriteLine("\n");

            // listando ...
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    Console.WriteLine("Name: {0}", node.SelectSingleNode("name").InnerText);
                }
            }

            Console.WriteLine("\n");

            // consultando por parametro
            {
                XmlNode person = document.SelectSingleNode("//person[@id='10']");
                Console.WriteLine(person.InnerText);
            }

            Console.WriteLine("\n");

            // consultando por atributo
            {
                XmlNode person = document.SelectSingleNode(String.Format("//person[name='{0}']", "Camila"));
                Console.WriteLine(person.InnerText);
            }

            // removendo campo ...
            {
                foreach (XmlNode node in root.ChildNodes)
                {
                    XmlNode birthday = node.SelectSingleNode("birthday");
                    if(birthday != null)
                    {
                        node.RemoveChild(birthday);
                    }
                }
            }

            document.Save("person_without_birthday.xml");
            Console.ReadKey();
        }
    }
}
