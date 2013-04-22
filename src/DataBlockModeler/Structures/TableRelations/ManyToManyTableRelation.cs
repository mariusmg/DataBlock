/*

       file : ManyToManyTableRelation.cs
 description: This represents a many to many relationship between 2 tables.
     author : Marius Gheorghe
    
*/

using System;

namespace voidsoft.DataBlockModeler
{
    /// <summary>
    /// Represents a ManyToMany relation between two entities 
    /// </summary>
    [Serializable]
    public class ManyToManyTableRelation : TableRelation
    {
        private string relatedTableName;

        private string intermediaryTableName;

        private string intermediaryKeyFieldFromParentTable; //This is usually the name of out PK in the intermediary table

        private string intermediaryKeyFieldFromChildTable; //This is usually the name of the PK field of the related table in the intermediary table.

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="relatedTableName">Name of the related table</param>
        /// <param name="intermediaryTableName">Name of the intermediary table</param>
        /// <param name="intermediaryKeyFieldFromParentTable">Name of the intermediary field from the parent table</param>
        /// <param name="intermediaryKeyFieldFromChildTable">Name of the intermediary field from the child table</param>
        public ManyToManyTableRelation(string relatedTableName, string intermediaryTableName, string intermediaryKeyFieldFromParentTable, string intermediaryKeyFieldFromChildTable)

        {
            this.relatedTableName = relatedTableName;
            this.intermediaryTableName = intermediaryTableName;
            this.intermediaryKeyFieldFromParentTable = intermediaryKeyFieldFromParentTable;
            this.intermediaryKeyFieldFromChildTable = intermediaryKeyFieldFromChildTable;
        }

        public ManyToManyTableRelation()
        {
        }

        #region properties

        /// <summary>
        /// Name of the related table
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
        /// Type of relation cardinality. In this case it's always ManyToMany
        /// </summary>
        public override RelationCardinality RelationCardinality
        {
            get
            {
                return RelationCardinality.ManyToMany;
            }
            set
            {
            }
        }

        /// <summary>
        /// Name of the intermediary table
        /// </summary>
        public string IntermediaryTableName
        {
            get
            {
                return intermediaryTableName;
            }
            set
            {
                intermediaryTableName = value;
            }
        }

        /// <summary>
        /// Name of the foreign key field from the parent table
        /// </summary>
        public string IntermediaryKeyFieldFromParentTable
        {
            get
            {
                return intermediaryKeyFieldFromParentTable;
            }
            set
            {
                intermediaryKeyFieldFromParentTable = value;
            }
        }

        /// <summary>
        /// Name of the foreign key field from the child table
        /// </summary>
        public string IntermediaryKeyFieldFromChildTable
        {
            get
            {
                return intermediaryKeyFieldFromChildTable;
            }
            set
            {
                intermediaryKeyFieldFromChildTable = value;
            }
        }

        #endregion
    }
}