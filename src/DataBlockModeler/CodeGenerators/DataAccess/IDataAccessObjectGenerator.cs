namespace voidsoft.DataBlockModeler
{
    internal interface IDataAccessObjectGenerator
    {
        void GenerateDataAccessObjects(string entityName, string namespaceName, string filePath);
    }
}