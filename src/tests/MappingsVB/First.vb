Imports System
Imports System.Data
Imports voidsoft.DataBlock

Namespace Extender
 


		Public Class First
		             Inherits TableMetadata
			Private _fields() as DatabaseField

			Sub New() 
			
					_fields = new DatabaseField(2) {}
 _fields(0) = new DatabaseField(DbType.Int32,"Id",True,True,Nothing)
 _fields(1) = new DatabaseField(DbType.String,"Name",False,False,Nothing)
 
me.currentTableName = "First"

Me.listRelations.Add(new ParentTableRelation("Second", "FirstId",TableRelationCardinality.OneToMany,True))

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
          Me.SetFieldValue(Id, Value)
        End Set
      End Property

     Public Property Name As System.String
        Get
           Return  CType((Me.GetField("Name")).fieldValue,System.String)
        End Get 
        Set(ByVal Value As System.String)
          Me.SetFieldValue(Name, Value)
        End Set
      End Property
Public Function GetSecond() As Array
    Dim relatedType As Type = Type.GetType("Extender.Second")
    Return Me.GetRelatedTableData(relatedType)
End Function
End Class
End Namespace
