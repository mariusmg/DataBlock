using System;
using System.Data;
using voidsoft.DataBlock;
namespace oop
{
    public class CategoriesBusinessObject
    {

        private Categories mapped = new Categories();
        private PersistentObject persistent = null;


        public CategoriesBusinessObject(DatabaseServer database, string connectionString)
        {
            persistent = new PersistentObject(database, connectionString, mapped);
        }


        public CategoriesBusinessObject(Session session)
        {
            persistent = new PersistentObject(session, mapped);
        }

        public CategoriesBusinessObject()
        {
            persistent = new PersistentObject(mapped);
        }


        public DataTable GetDataTable()
        {
            return persistent.GetDataTable();
        }
        public DataTable GetDataTable()
        {
            return persistent.GetDataTable();
        }
        public DataTable GetDataTable(params DatabaseFields[] fields)
        {
            return persistent.GetDataTable(fields);
        }
        public Categories[] GetCategories()
        {
            return (Categories[])persistent.GetTableMetadata();
        }
        public Categories GetCategories(object primaryKeyValue)
        {
            return (Categories)persistent.GetTableMetadata(primaryKeyValue);
        }
        public Array GetCategories(string relatedTableName, Type classType, object foreignKeyValue)
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
        public bool IsUnique(DatabaseField field)
        {
            return persistent.GetValue(criteria);
        }
        public object Max(DatabaseField field)
        {
            return persistent.Max(field);
        }
        public object Min(DatabaseField field)
        {
            return persistent.Min(field);
        }
        public object Count(DatabaseField field)
        {
            return persistent.Count(field);
        }
        public int Create(Categories mappedObject)
        {
            return persistent.Create(mappedObject);
        }
        public int Update(Categories mappedObject)
        {
            return persistent.Update(mappedObject);
        }
        public int Update(ObjectQuery criteria)
        {
            return persistent.Update(criteria);
        }
        public int Delete(Categories mappedObject)
        {
            return persistent.Delete(mappedObject);
        }
        public int Delete(QueryCriteria criteria)
        {
            return persistent.Delete(criteria);
        }

    }
}
