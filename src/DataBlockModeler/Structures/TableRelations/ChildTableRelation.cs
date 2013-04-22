/*

 file : ChildTableRelation.cs 
  
  
 */
using System;

namespace voidsoft.DataBlockModeler
{
    /// <summary>
    /// Represents a Child - Parent relation between 2 entities
    /// </summary>
    [Serializable]
    public class ChildTableRelation : TableRelation
    {
        private string relatedTableName;

        private RelationCardinality tableCardinality;

        private string relatedTableKeyName;

        private string foreignKeyName;


        /// <summary>
        /// Constructor.Creates a new instance of ChildTableRelation.
        /// </summary>
        /// <param name="relatedTableName">Name of the related table.</param>
        /// <param name="tableCardinality">The table cardinality.</param>
        /// <param name="relatedTableKeyName">Name of the related table key.</param>
        /// <param name="foreignKeyName">Name of the foreign key.</param>
        public ChildTableRelation(string relatedTableName, RelationCardinality tableCardinality, string relatedTableKeyName, string foreignKeyName)
        {
            this.relatedTableName = relatedTableName;
            this.tableCardinality = tableCardinality;
            this.relatedTableKeyName = relatedTableKeyName;
            this.foreignKeyName = foreignKeyName;
        }


        public ChildTableRelation()
        {
        }

        /// <summary>
        /// Get the name of the related table
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
        /// Gets the relation cardinality 
        /// </summary>
        public override RelationCardinality RelationCardinality
        {
            get
            {
                return tableCardinality;
            }
            set
            {
                tableCardinality = value;
            }
        }

        //this is the primary key from the main table.

        /// <summary>
        /// Gets the name of the primary key from the parent table
        /// </summary>
        public string RelatedTableKeyName
        {
            get
            {
                return relatedTableKeyName;
            }
            set
            {
                relatedTableKeyName = value;
            }
        }


        /// <summary>
        /// Gets the name of the foreign key from the child table
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
    }
}