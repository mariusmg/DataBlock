using System;
using System.Data;
using voidsoft.DataBlock;
using System.Collections;
using Extender;
namespace BusinessObjects
{
public class BookAuthorsBusinessObject
{ 

       private BookAuthors mapped = new BookAuthors();
       private PersistentObject persistent = null; 


      public BookAuthorsBusinessObject(DatabaseServer database, string connectionString) 
      {
           persistent = new PersistentObject(database, connectionString, mapped);
      }

      
      public BookAuthorsBusinessObject(Session session)
      {
           persistent = new PersistentObject(session, mapped);
      }

      public BookAuthorsBusinessObject()
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


   public BookAuthors[] GetBookAuthors()
	 {
        	 return (BookAuthors[])persistent.GetTableMetadata();
	 }


   public BookAuthors GetBookAuthors(object primaryKeyValue)
   {
      	return(BookAuthors)persistent.GetTableMetadata(primaryKeyValue);
   }


   public  Array GetBookAuthors(string relatedTableName, Type classType, object foreignKeyValue)
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


	public int Create(BookAuthors mappedObject)
	{
		return persistent.Create(mappedObject);
	}


	public int Update(BookAuthors mappedObject)
	{
		return persistent.Update(mappedObject);
	}


	public int Update(QueryCriteria criteria)
	{
		return persistent.Update(criteria);
	}


	public int Delete(BookAuthors mappedObject)
	{
		return persistent.Delete(mappedObject);
	}


	public int Delete(QueryCriteria criteria)
	{
		return persistent.Delete(criteria);
	}


 
   }
}
