Imports System
Imports System.Data
Imports voidsoft.DataBlock

Namespace Extender
 


		Public Class BookAuthors
		             Inherits TableMetadata
			Private _fields() as DatabaseField

			Sub New() 
			
					_fields = new DatabaseField(2) {}
 _fields(0) = new DatabaseField(DbType.Int32,"Id",True,True,Nothing)
 _fields(1) = new DatabaseField(DbType.Int32,"AuthorId",False,False,Nothing)
 _fields(2) = new DatabaseField(DbType.Int32,"BookId",False,False,Nothing)
 
me.currentTableName = "BookAuthors"


End Sub



			Public Overrides Property TableFields() as DatabaseField() 
		   	   Get
				  Return _fields
			   End Get
			   Set (ByVal Value as DatabaseField())
			      _fields = value
			   End Set
			End Property

     Public Property Id As System.Int32
        Get
           Return  CType((Me.GetField("Id")).fieldValue,System.Int32)
        End Get 
        Set(ByVal Value As System.Int32)
          Me.SetFieldValue("Id", Value)
        End Set
      End Property

     Public Property AuthorId As System.Int32
        Get
           Return  CType((Me.GetField("AuthorId")).fieldValue,System.Int32)
        End Get 
        Set(ByVal Value As System.Int32)
          Me.SetFieldValue("AuthorId", Value)
        End Set
      End Property

     Public Property BookId As System.Int32
        Get
           Return  CType((Me.GetField("BookId")).fieldValue,System.Int32)
        End Get 
        Set(ByVal Value As System.Int32)
          Me.SetFieldValue("BookId", Value)
        End Set
      End Property
End Class
End Namespace
