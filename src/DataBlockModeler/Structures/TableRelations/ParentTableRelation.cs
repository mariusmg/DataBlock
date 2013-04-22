using System;

namespace voidsoft.DataBlockModeler
{
    /// <summary>
    /// This is a Parent -> Child relationship. Note that the type of the fields on which
    /// the 2 tables are related must be the same so the type of the foreign key is inferred
    /// from the Primary Key of the parent table.
    /// </summary>
    [Serializable]
    public class ParentTableRelation : TableRelation
    {
        private string relatedTableName;

        private string foreignKeyName;

        private RelationCardinality relationCardinality;

        private bool cascadeDeleteChildren;

        /// <summary>
        /// Constructor. Creates a new instance of ChildTableRealtion
        /// </summary>
        public ParentTableRelation(string relatedTableName, string foreignKeyName, RelationCardinality relationCardinality, bool cascadeDeleteChildren)
        {
            this.relatedTableName = relatedTableName;
            this.foreignKeyName = foreignKeyName;
            this.relationCardinality = relationCardinality;
            this.cascadeDeleteChildren = cascadeDeleteChildren;
        }

        public ParentTableRelation()
        {
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
            set
            {
                relatedTableName = value;
            }
        }


        /// <summary>
        /// The cardinality with the related table.
        /// </summary>
        public override RelationCardinality RelationCardinality
        {
            get
            {
                return relationCardinality;
            }
            set
            {
                relationCardinality = value;
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
            set
            {
                foreignKeyName = value;
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
            set
            {
                cascadeDeleteChildren = value;
            }
        }
    }
}