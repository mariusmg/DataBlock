/*

       file : 
description :
     author :
  
*/


using System.Collections;

namespace voidsoft.DataBlockModeler
{
    /// <summary>
    /// Interface which defines operations for TableMetadata generator.
    /// </summary>
    internal interface ITableMetadataGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableNames">String array with table names</param>
        /// <param name="alColumns">ArrayList with columns for each table name</param>
        /// <param name="fileName">The file path</param>
        /// <param name="namespaceName">Namespace name</param>
        void GenerateTableMetadatata(string[] entityNames, string[] tableNames, ArrayList alColumns, ArrayList alTableRelations, string fileName, string namespaceName);
    }
}