using System;
using System.Data;
using voidsoft.DataBlock;
using System.Collections;
using Extender;
namespace BusinessObjects
{
public class AuthorBusinessObject
{ 

       private Author mapped = new Author();
       private PersistentObject persistent = null; 


      public AuthorBusinessObject(DatabaseServer database, string connectionString) 
      {
           persistent = new PersistentObject(database, connectionString, mapped);
      }

      
      public AuthorBusinessObject(Session session)
      {
           persistent = new PersistentObject(session, mapped);
      }

      public AuthorBusinessObject()
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


   public Author[] GetAuthor()
	 {
        	 return (Author[])persistent.GetTableMetadata();
	 }


   public Author GetAuthor(object primaryKeyValue)
   {
      	return(Author)persistent.GetTableMetadata(primaryKeyValue);
   }


   public  Array GetAuthor(string relatedTableName, Type classType, object foreignKeyValue)
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


	public int Create(Author mappedObject)
	{
		return persistent.Create(mappedObject);
	}


	public int Update(Author mappedObject)
	{
		return persistent.Update(mappedObject);
	}


	public int Update(QueryCriteria criteria)
	{
		return persistent.Update(criteria);
	}


	public int Delete(Author mappedObject)
	{
		return persistent.Delete(mappedObject);
	}


	public int Delete(QueryCriteria criteria)
	{
		return persistent.Delete(criteria);
	}


 
   }
}
