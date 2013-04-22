using System;
using System.Data;
using voidsoft.DataBlock;
using System.Collections;
using Extender;
namespace BusinessObjects
{
public class CustomerBusinessObject
{

    private CustomerTableMetadata mapped = new CustomerTableMetadata();
       private PersistentObject persistent = null; 


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


   public CustomerTableMetadata[] GetCustomer()
	 {
         return (CustomerTableMetadata[]) persistent.GetTableMetadata();
	 }


    public CustomerTableMetadata GetCustomer(object primaryKeyValue)
   {
       return (CustomerTableMetadata) persistent.GetTableMetadata(primaryKeyValue);
   }


   public  Array GetCustomer(string relatedTableName, Type classType, object foreignKeyValue)
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


   public object GetValue (QueryCriteria criteria)
	 {
         return persistent.GetValue(criteria);
	 }


   public bool IsUnique (DatabaseField field, object value)
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


    public int Create(CustomerTableMetadata mappedObject)
	{
		return persistent.Create(mappedObject);
	}


    public int Update(CustomerTableMetadata mappedObject)
	{
		return persistent.Update(mappedObject);
	}


	public int Update(QueryCriteria criteria)
	{
		return persistent.Update(criteria);
	}


    public int Delete(CustomerTableMetadata mappedObject)
	{
		return persistent.Delete(mappedObject);
	}


	public int Delete(QueryCriteria criteria)
	{
		return persistent.Delete(criteria);
	}


 
   }
}
