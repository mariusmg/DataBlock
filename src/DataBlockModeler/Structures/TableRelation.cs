using System;
using System.Collections.Generic;
using System.Text;

namespace voidsoft.DataBlockModeler
{
    public class TableRelation
    {

        /// <summary>
        /// Related table name
        /// </summary>
        public string relatedTable;

        /// <summary>
        /// Relation cardinality
        /// </summary>
        public RelationCardinality Cardinality;
        public RelationType TypeOfRelation;


        /// <summary>
        /// Name of the foreign key from the child table
        /// </summary>
        public string foreignKeyName;
        public bool cascadeDelete; //in parent->child relations


        //these are for a child 
        public string primaryKeyNameInRelatedTable;
        private string relatedTableKeyType;



    }
}
