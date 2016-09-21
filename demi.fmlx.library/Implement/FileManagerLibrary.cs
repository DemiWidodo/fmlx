using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using demi.fmlx.library.FileManager;
using demi.fmlx.library.Interface;

namespace demi.fmlx.library.Implement
{
    public class FileManagerLibrary : IFileManagerLibrary
    {
        private FileManagerRepository _fileManagerRepository = new FileManagerRepository();

        public void Initialize()
        {
            _fileManagerRepository.Initialize();
        }

        public void Register(string itemName, string itemContent, int itemType)
        {
            _fileManagerRepository.Register(itemName ,itemContent ,itemType);
        }

        public string Retrieve(string itemName)
        {
            return _fileManagerRepository.Retrieve(itemName);
        }

        public int GetType(string itemName)
        {
            return _fileManagerRepository.GetType(itemName);
        }

        public void Deregister(string itemName)
        {
            _fileManagerRepository.Deregister(itemName);
        }
    }
}
