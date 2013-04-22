namespace voidsoft.DataBlockModeler
{
    /// <summary>
    /// Describes a database column.
    /// </summary>
    public class ColumnInfo
    {
        /// <summary>
        /// Column's name.
        /// </summary>
        public string columnName;

        /// <summary>
        /// Column's DataType.
        /// </summary>
        public string columnDataType;

        /// <summary>
        /// Flag to know if this is PK.
        /// </summary>
        public bool isPrimaryKey;

        /// <summary>
        /// Flag to know if the promary key's value in autoincremented.
        ///This fied is used only if the column in PK. 
        /// </summary>
        public bool isAutoIncremented;

        public bool allowsNull;

        public int fieldLength;


        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnInfo"/> class.
        /// </summary>
        public ColumnInfo()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnInfo"/> class.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="columnDataType">Type of the column data.</param>
        /// <param name="isPrimaryKey">if set to <c>true</c> [is primary key].</param>
        /// <param name="isAutoIncremented">if set to <c>true</c> [is auto incremented].</param>
        /// <param name="allowsNull">if set to <c>true</c> [allows null].</param>
        /// <param name="fieldLength">Length of the field.</param>
        public ColumnInfo(string columnName,
                          string columnDataType,
                          bool isPrimaryKey,
                          bool isAutoIncremented,
                          bool allowsNull,
                          int fieldLength)
        {
            this.columnName = columnName;
            this.columnDataType = columnDataType;
            this.isPrimaryKey = isPrimaryKey;
            this.isAutoIncremented = isAutoIncremented;
            this.allowsNull = allowsNull;
            this.fieldLength = fieldLength;
        }


    }
}