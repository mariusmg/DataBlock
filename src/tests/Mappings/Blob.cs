using System;
using System.Data;
using voidsoft.DataBlock;

namespace Extender
{

      [Serializable()]
		public class Blob : TableMetadata
		{

                   public enum BlobFields
                   {
                      id,
                      SmallBlob,
                      BigBlob
                  }


			    private DatabaseField[] _fields;

		    	public Blob()
			    {
					    _fields = new DatabaseField[3];
                    _fields[0] = new DatabaseField(DbType.Int32,"id",true,true,null);
                    _fields[1] = new DatabaseField(DbType.Binary,"SmallBlob",false,false,null);
                    _fields[2] = new DatabaseField(DbType.Binary,"BigBlob",false,false,null);
 
                        this.currentTableName = "Blobs";


                  }


			public override DatabaseField[] TableFields 
			{
				get{ return _fields;}
				set{_fields = value;}
			}
          public Blob Clone()
          {
                 return this.Clone<Blob>();
          }

public System.Int32? id
{
    get
    {
          return (System.Int32? ) (this.GetField("id")).fieldValue;
    }

    set
    {
          this.SetFieldValue("id", value);
    }
}


public System.Byte[] SmallBlob
{
    get
    {
         object result = (this.GetField("SmallBlob")).fieldValue;
         if(result == null)
         {
              return new System.Byte[0];
         }

          return (System.Byte[]) (this.GetField("SmallBlob")).fieldValue;
    }

    set
    {
          this.SetFieldValue("SmallBlob", value);
    }
}


public System.Byte[] BigBlob
{
    get
    {
         object result = (this.GetField("BigBlob")).fieldValue;
         if(result == null)
         {
              return new System.Byte[0];
         }

          return (System.Byte[]) (this.GetField("BigBlob")).fieldValue;
    }

    set
    {
          this.SetFieldValue("BigBlob", value);
    }
}

}
}
