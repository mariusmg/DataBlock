using System;
using System.Collections.Generic;
using System.Text;

namespace voidsoft.DataBlockModeler
{
    interface IBusinessObjectGenerator
    {
        void GenerateBusinessObjects(string objectName, string namespaceName, string fileName);
    }
}
