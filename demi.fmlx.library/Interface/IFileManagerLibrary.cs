using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace demi.fmlx.library.Interface
{
    public interface IFileManagerLibrary
    {

        /// <summary>
        /// Initialize the repository for use
        /// </summary>
        void Initialize();

        /// <summary>
        /// Store an item to the repository.
        /// Parameter itemType is used to differentiate JSON or XML
        /// 1 = itemContent is a JSON string
        /// 2 = itemContent is an XML string
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemContent"></param>
        /// <param name="itemType"></param>
        void Register(string itemName, string itemContent, int itemType);

        /// <summary>
        /// Retrieve an item from the repository
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        string Retrieve(string itemName);

        /// <summary>
        /// Retrieve the type of the item (JSON or XML)
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        int GetType(string itemName);

        /// <summary>
        /// Remove an item from the repository
        /// </summary>
        /// <param name="itemName"></param>
        void Deregister(string itemName);
    }
}
