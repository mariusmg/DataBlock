/*

      file : ListTableMetadata.cs
description:
    author : Marius Gheorghe
  
  
*/

using System;
using System.Collections.Generic;


namespace voidsoft.DataBlock
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListTableMetadata<T>: List<TableMetadata> where T : TableMetadata
    //public class ListTableMetadata<T> where T : TableMetadata
    {
         private List<T> list = null; 


        #region ctors        
        /// <summary>
        /// Creates a new instance of ListTableMetadata
        /// </summary>
        public ListTableMetadata()
        {
            list = new List<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        public ListTableMetadata(Array array)
            : this()
        {
            foreach (T ar in array)
            {
                this.Add(ar);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        public ListTableMetadata(T[] array)
            : this()
        {
            this.AddRange(array);
        } 
        #endregion


        #region search methods

        public int IndexOfTableMetadata(object primaryKeyValue)
        {
            return this.IndexOfTableMetadata(primaryKeyValue, 0);
        }

        public int IndexOfTableMetadata(object primaryKeyValue, int startIndex)
        {
            //get the database field for this.

            try
            {

                if (this.Count == 0)
                {
                    return -1;
                }

                TableMetadata meta = this[0];
                DatabaseField field = meta.GetPrimaryKeyField();

                int indexOfField = -1;

                //get the index of the DatabaseField from TableMetadata
                for (int i = 0; i < meta.TableFields.Length; i++)
                {
                    if (field.fieldName == meta.TableFields[i].fieldName)
                    {
                        indexOfField = i;
                        break;
                    }
                }

                //get the index of the TableMetadata which contains the PK value
                for (int i = startIndex; i < this.Count; i++)
                {
                    meta = this[i];

                    if (meta.TableFields[indexOfField].fieldValue == primaryKeyValue)
                    {
                        return i;
                    }
                }


                return -1;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

    }
}