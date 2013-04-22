using System;
using System.Collections.Generic;

namespace voidsoft.DataBlockModeler
{
    [Serializable]
    public class DatabaseTable
    {
        /// <summary>
        /// Name of the table
        /// </summary>
        private string tableName;

        /// <summary>
        /// Array of columns
        /// </summary>
        private DatabaseColumn[] columns;

        /// <summary>
        /// Table relations
        /// </summary>
        private List<TableRelation> relations;


        /// <summary>
        /// Holds the name of the mapping entity.
        /// </summary>
        private string entityName;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="relations">The relations.</param>
        /// <param name="entityName">Name of the entity.</param>
        public DatabaseTable(string name,
                             DatabaseColumn[] columns,
                             List<TableRelation> relations,
                             string entityName)
        {
            tableName = name;
            Columns = columns;
            this.relations = relations;
            this.entityName = entityName;
        }


        public DatabaseTable(string tableName, DatabaseColumn[] columns, string entityName)
        {
            this.tableName = tableName;
            this.entityName = entityName;
            this.relations = new List<TableRelation>();
            Columns = columns;
        }

        /// <summary>
        /// Name of the table
        /// </summary>
        public string TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                tableName = value;
            }
        }

        /// <summary>
        /// Holds the name of the mapping entity.
        /// </summary>
        public string EntityName
        {
            get
            {
                return entityName;
            }
            set
            {
                entityName = value;
            }
        }

        /// <summary>
        /// Table relations
        /// </summary>
        public List<TableRelation> Relations
        {
            get
            {
                return relations;
            }
            set
            {
                relations = value;
            }
        }

        /// <summary>
        /// Array of columns
        /// </summary>
        public DatabaseColumn[] Columns
        {
            get { return columns; }
            set { columns = value; }
        }
    }
}