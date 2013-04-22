/*
  
	   file: TableRelation.cs
description: Represents a data relation between two tables.
    author: Marius Gheorghe

  
     notes:  - if the relation mode is Parent then RelatedTableKey name is the
              Foreign Key.
             - if the relation mode is Child then the RelatedTableKey name is the
              Primary Key of the related Table.  
 
*/

using System;
using System.Data;

namespace voidsoft.DataBlock
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
        public virtual TableRelationCardinality RelationCardinality
        {
            get
            {
                return TableRelationCardinality.OneToOne;
            }
        }

        /// <summary>
        /// The name of the related table
        /// </summary>
        public virtual string RelatedTableName
        {
            get
            {
                return string.Empty;
            }
        }
    }


    
    

    /// <summary>
    /// This is a Parent -> Child relationship. Note that the type of the fields on which
    /// the 2 tables are related must be the same so the type of the foreign key is infered
    /// from the Primary Key of the parent table.
    /// </summary>
    [Serializable]
    public class ParentTableRelation : TableRelation
    {
        private string relatedTableName;
        private string foreignKeyName;
        private string foreignKeyType;
        private TableRelationCardinality relationCardinality;
        private bool cascadeDeleteChildren;

        /// <summary>
        /// Constructor. Creates a new instance of ChildTableRealtion
        /// </summary>
        public ParentTableRelation(string relatedTableName,
                                   string foreignKeyName,
                                   TableRelationCardinality relationCardinality,
                                   bool cascadeDeleteChildren)
        {
            this.relatedTableName = relatedTableName;
            this.foreignKeyName = foreignKeyName;
            this.relationCardinality = relationCardinality;
            this.cascadeDeleteChildren = cascadeDeleteChildren;
        }

        /// <summary>
        /// The name of the related table.
        /// </summary>
        public override string RelatedTableName
        {
            get
            {
                return relatedTableName;
            }
        }


        /// <summary>
        /// The cardinality with the related table.
        /// </summary>
        public override TableRelationCardinality RelationCardinality
        {
            get
            {
                return relationCardinality;
            }
        }

        /// <summary>
        /// The name of the foreign key.
        /// </summary>
        public string ForeignKeyName
        {
            get
            {
                return foreignKeyName;
            }
        }


        /// <summary>
        /// Flag to know if the data from the related tables is deleted when 
        /// we delete data from the main table.
        /// </summary>
        public bool CascadeDeleteChildren
        {
            get
            {
                return cascadeDeleteChildren;
            }
        }
    }





    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ChildTableRelation : TableRelation
    {

        private string relatedTableName;
        private TableRelationCardinality tableCardinality;
        private string relatedTableKeyName;
        private DbType relatedTableKeyType;
        private string foreignKeyName;
        

        /// <summary>
        /// Constructor.Creates a new instance of ChildTableRelation.
        /// </summary>
        /// <param name="relatedTableName"></param>
        /// <param name="cardinality"></param>
        /// <param name="relatedTableKeyName"></param>
        /// <param name="relatedTableKeyType"></param>
        /// <param name="foreignKeyName"></param>
        public ChildTableRelation(string relatedTableName,
                                  TableRelationCardinality tableCardinality,
                                  string relatedTableKeyName,
                                  DbType relatedTableKeyType,
                                  string foreignKeyName)
        {
            this.relatedTableName = relatedTableName;
            this.tableCardinality = tableCardinality;
            this.relatedTableKeyName = relatedTableKeyName;
            this.foreignKeyName = foreignKeyName;
        }

        public override string RelatedTableName
        {
            get
            {
                return relatedTableName;
            }
        }

        public override TableRelationCardinality RelationCardinality
        {
            get
            {
                return tableCardinality;
            }
        }

        //this is the primary key from the main table.
        public string RelatedTableKeyName
        {
            get
            {
                return relatedTableName;
            }
        }

        public DbType RelatedTableKeyType
        {
            get
            {
                return relatedTableKeyType; 
            }
        }

        public string ForeignKeyName
        {
            get
            {
                return foreignKeyName; 
            }
        }
    }








    ///// <summary>
    ///// Represents a relation between two tables.
    ///// </summary>
    //[Serializable]
    //public struct TableRelation
    //{

    //    #region fields
    //    //the name of the relation
    //    private string relationName;
        
    //    //relation cardinality
    //    private TableRelationCardinality relationCardinality;
        
    //    //relation mode. Our table can be either parent or child.
    //    private TableRelationMode relationMode;

    //    //the name of the related table
    //    private string relatedTableName;
        
    //    //type of the related field type.
    //    private System.Data.DbType relatedTableKeyType;

    //    //this is empty if the relation mode is Parent
    //    private string relatedTableKeyName; 
    //    #endregion


    //    #region properties

    //    /// <summary>
    //    /// The relation name
    //    /// </summary>
    //    public string RelationName
    //    {
    //        get
    //        {
    //            return relationName;
    //        }
    //        set
    //        {
    //            relationName = value;
    //        }
    //    }

    //    /// <summary>
    //    /// The type of the field in the related table. 
    //    /// </summary>
    //    public System.Data.DbType RelatedTableKeyType
    //    {
    //        get
    //        {
    //            return relatedTableKeyType;
    //        }

    //        set
    //        {
    //            relatedTableKeyType = value;
    //        }
    //    }

    //    /// <summary>
    //    /// The name of the field from the related table.
    //    /// </summary>
    //    public string RelatedTableKeyName
    //    {
    //        get
    //        {
    //            return relatedTableKeyName;
    //        }
    //        set
    //        {
    //            relatedTableKeyName = value;
    //        }
    //    }

    //    /// <summary>
    //    /// The cardinality of the relation. 
    //    /// </summary>
    //    public TableRelationCardinality RelationType
    //    {
    //        get
    //        {
    //            return relationCardinality;
    //        }
    //        set
    //        {
    //            relationCardinality = value;
    //        }
    //    }

    //    /// <summary>
    //    /// The realtion mode
    //    /// </summary>
    //    public TableRelationMode RelationMode
    //    {
    //        get
    //        {
    //            return relationMode;
    //        }
    //        set
    //        {
    //            relationMode = value;
    //        }
    //    }

    //    /// <summary>
    //    /// The name of the related table.
    //    /// </summary>
    //    public string RelatedTableName
    //    {
    //        get
    //        {
    //            return relatedTableName;
    //        }
    //        set
    //        {
    //            relatedTableName = value;
    //        }
    //    }
    //    #endregion
    //}

}
