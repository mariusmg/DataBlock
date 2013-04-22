Imports System
Imports System.Data
Imports voidsoft.DataBlock

Namespace Extender
 


		Public Class Book
		             Inherits TableMetadata
			Private _fields() as DatabaseField

			Sub New() 
			
					_fields = new DatabaseField(2) {}
 _fields(0) = new DatabaseField(DbType.Int32,"BookId",True,True,Nothing)
 _fields(1) = new DatabaseField(DbType.String,"Name",False,False,Nothing)
 _fields(2) = new DatabaseField(DbType.Int32,"Pages",False,False,Nothing)
 
me.currentTableName = "Book"

Me.listRelations.Add(new ManyToManyTableRelation("Author","BookAuthors","BookId","AuthorId"))

End Sub



			Public Overrides Property TableFields() as DatabaseField() 
		   	   Get
				  Return _fields
			   End Get
			   Set (ByVal Value as DatabaseField())
			      _fields = value
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

     Public Property Name As System.String
        Get
           Return  CType((Me.GetField("Name")).fieldValue,System.String)
        End Get 
        Set(ByVal Value As System.String)
          Me.SetFieldValue("Name", Value)
        End Set
      End Property

     Public Property Pages As System.Int32
        Get
           Return  CType((Me.GetField("Pages")).fieldValue,System.Int32)
        End Get 
        Set(ByVal Value As System.Int32)
          Me.SetFieldValue("Pages", Value)
        End Set
      End Property
Public Function GetAuthor() As Array
    Dim relatedType As Type = Type.GetType("Extender.Author")
    Dim intermediaryType As Type = Type.GetType("Extender.BookAuthors")
    Return Me.GetRelatedTableData(relatedType, intermediaryType)
End Function
End Class
End Namespace
