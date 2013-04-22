using System;
using System.Data;
using voidsoft.DataBlock;
using System.Collections;
using voidsoft.Mappings;
namespace BusinessObjects
{
    public class CustomerBusinessObject
    {

        private Customer mapped = new Customer();
        private PersistentObject persistent = null;


        #region Constructors
        public CustomerBusinessObject(DatabaseServer database, string connectionString)
        {
            persistent = new PersistentObject(database, connectionString, mapped);
        }


        public CustomerBusinessObject(Session session)
        {
            persistent = new PersistentObject(session, mapped);
        }

        public CustomerBusinessObject()
        {
            persistent = new PersistentObject(mapped);
        }
        #endregion


        #region generated implementation
        public DataTable GetDataTable()
        {
            return persistent.GetDataTable();
        }


        public DataTable GetDataTable(QueryCriteria qc)
        {
            return persistent.GetDataTable(qc);
        }


        public DataTable GetDataTable(params DatabaseField[] fields)
        {
            return persistent.GetDataTable(fields);
        }

        public Customer[] GetCustomer(QueryCriteria qc)
        {
            return (Customer[]) persistent.GetTableMetadata(qc);
        }


        public Customer[] GetCustomer()
        {
            return (Customer[]) persistent.GetTableMetadata();
        }


        public Customer GetCustomer(object primaryKeyValue)
        {
            return (Customer) persistent.GetTableMetadata(primaryKeyValue);
        }


        public Array GetCustomer(string relatedTableName, Type classType, object foreignKeyValue)
        {
            return persistent.GetTableMetadata(relatedTableName, classType, foreignKeyValue);
        }


        public ArrayList GetFieldList(QueryCriteria criteria)
        {
            return persistent.GetFieldList(criteria);
        }


        public ArrayList GetFieldList(DatabaseField field)
        {
            return persistent.GetFieldList(field);
        }


        public object GetValue(QueryCriteria criteria)
        {
            return persistent.GetValue(criteria);
        }


        public bool IsUnique(DatabaseField field, object value)
        {
            return persistent.IsUnique(field, value);
        }


        public object GetMax(DatabaseField field)
        {
            return persistent.GetMax(field);
        }


        public object GetMin(DatabaseField field)
        {
            return persistent.GetMin(field);
        }


        public object GetCount()
        {
            return persistent.GetCount();
        }


        public int Create(Customer mappedObject)
        {
            return persistent.Create(mappedObject);
        }


        public int Update(Customer mappedObject)
        {
            return persistent.Update(mappedObject);
        }


        public int Update(QueryCriteria criteria)
        {
            return persistent.Update(criteria);
        }


        public int Delete(Customer mappedObject)
        {
            return persistent.Delete(mappedObject);
        }


        public int Delete(QueryCriteria criteria)
        {
            return persistent.Delete(criteria);
        }


        #endregion

    }
}
