/*
  
	   file: TableRelation.cs
description: Represents a data relation between two DatabaseTables.
    author: Marius Gheorghe

  
     notes:  - if the relation mode is Parent then RelatedTableKey name is the
              Foreign Key.
             - if the relation mode is Child then the RelatedTableKey name is the
              Primary Key of the related Table.  
 
*/

using System;

namespace voidsoft.DataBlockModeler
{
    /// <summary>
    /// Base class which represents a table relation
    /// </summary>
    [Serializable]
    public abstract class TableRelation
    {
        /// <summary>
        /// The cardinality with the related tabel
        /// </summary>
        public abstract RelationCardinality RelationCardinality
        {
            get;
            set;
        }


        /// <summary>
        /// The name of the related table
        /// </summary>
        public abstract string RelatedTableName
        {
            get;
            set;
        }
    }
}