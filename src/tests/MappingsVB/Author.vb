
Option Strict On
Option Explicit On

Imports System
Imports System.Data
Imports voidsoft.DataBlock

Namespace Extender
 


		Public Class Author
		             Inherits TableMetadata
			Private _fields() as DatabaseField

			Sub New() 
			
					_fields = new DatabaseField(3) {}
 _fields(0) = new DatabaseField(DbType.Int32,"AuthorId",True,True,Nothing)
 _fields(1) = new DatabaseField(DbType.String,"Name",False,False,Nothing)
 _fields(2) = new DatabaseField(DbType.Int32,"Age",False,False,Nothing)
 _fields(3) = new DatabaseField(DbType.String,"Location",False,False,Nothing)
 
me.currentTableName = "Author"

Me.listRelations.Add(new ManyToManyTableRelation("Book","BookAuthors","AuthorId","BookId"))

End Sub



			Public Overrides Property TableFields() as DatabaseField() 
		   	   Get
				  Return _fields
			   End Get
			   Set (ByVal Value as DatabaseField())
			      _fields = value
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

     Public Property Name As System.String
        Get
           Return  CType((Me.GetField("Name")).fieldValue,System.String)
        End Get 
        Set(ByVal Value As System.String)
          Me.SetFieldValue("Name", Value)
        End Set
      End Property

     Public Property Age As System.Int32
        Get
           Return  CType((Me.GetField("Age")).fieldValue,System.Int32)
        End Get 
        Set(ByVal Value As System.Int32)
          Me.SetFieldValue("Age", Value)
        End Set
      End Property

     Public Property Location As System.String
        Get
           Return  CType((Me.GetField("Location")).fieldValue,System.String)
        End Get 
        Set(ByVal Value As System.String)
          Me.SetFieldValue("Location", Value)
        End Set
      End Property
Public Function GetBook() As Array
    Dim relatedType As Type = Type.GetType("Extender.Book")
    Dim intermediaryType As Type = Type.GetType("Extender.BookAuthors")
    Return Me.GetRelatedTableData(relatedType, intermediaryType)
End Function
End Class
End Namespace
