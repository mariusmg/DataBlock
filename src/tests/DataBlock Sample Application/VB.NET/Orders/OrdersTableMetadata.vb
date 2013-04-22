Imports System
Imports System.Data
Imports voidsoft.DataBlock
Namespace ExtenderOrders

    Public Class OrderDetailsTableMetadata
        Inherits TableMetadata
        Private _fields As DatabaseField()

        Public Sub New()
            _fields = New DatabaseField(4) {}
            _fields(0) = New DatabaseField(DbType.Int32, "OrderID", False, False, Nothing)
            _fields(1) = New DatabaseField(DbType.Int32, "ProductID", False, False, Nothing)
            _fields(2) = New DatabaseField(DbType.Decimal, "UnitPrice", False, False, Nothing)
            _fields(3) = New DatabaseField(DbType.Int16, "Quantity", False, False, Nothing)
            _fields(4) = New DatabaseField(DbType.Single, "Discount", False, False, Nothing)
            Me.currentTableName = "Order Details"
        End Sub

        Public Overloads Overrides Property TableFields() As DatabaseField()
            Get
                Return _fields
            End Get
            Set(ByVal Value As DatabaseField())
                _fields = value
            End Set
        End Property

        Public Property OrderID() As System.Int32
            Get
                Return CType((Me.GetField("OrderID")).fieldValue, System.Int32)
            End Get
            Set(ByVal Value As System.Int32)
                Me.SetFieldValue("OrderID", value)
            End Set
        End Property

        Public Property ProductID() As System.Int32
            Get
                Return CType((Me.GetField("ProductID")).fieldValue, System.Int32)
            End Get
            Set(ByVal Value As System.Int32)
                Me.SetFieldValue("ProductID", value)
            End Set
        End Property

        Public Property UnitPrice() As System.Decimal
            Get
                Return CType((Me.GetField("UnitPrice")).fieldValue, System.Decimal)
            End Get
            Set(ByVal Value As System.Decimal)
                Me.SetFieldValue("UnitPrice", value)
            End Set
        End Property

        Public Property Quantity() As System.Int16
            Get
                Return CType((Me.GetField("Quantity")).fieldValue, System.Int16)
            End Get
            Set(ByVal Value As System.Int16)
                Me.SetFieldValue("Quantity", value)
            End Set
        End Property

        Public Property Discount() As System.Single
            Get
                Return CType((Me.GetField("Discount")).fieldValue, System.Single)
            End Get
            Set(ByVal Value As System.Single)
                Me.SetFieldValue("Discount", value)
            End Set
        End Property
    End Class

    Public Class OrdersTableMetadata
        Inherits TableMetadata
        Private _fields As DatabaseField()

        Public Sub New()
            _fields = New DatabaseField(13) {}
            _fields(0) = New DatabaseField(DbType.Int32, "OrderID", True, True, Nothing)
            _fields(1) = New DatabaseField(DbType.String, "CustomerID", False, False, Nothing)
            _fields(2) = New DatabaseField(DbType.Int32, "EmployeeID", False, False, Nothing)
            _fields(3) = New DatabaseField(DbType.DateTime, "OrderDate", False, False, Nothing)
            _fields(4) = New DatabaseField(DbType.DateTime, "RequiredDate", False, False, Nothing)
            _fields(5) = New DatabaseField(DbType.DateTime, "ShippedDate", False, False, Nothing)
            _fields(6) = New DatabaseField(DbType.Int32, "ShipVia", False, False, Nothing)
            _fields(7) = New DatabaseField(DbType.Decimal, "Freight", False, False, Nothing)
            _fields(8) = New DatabaseField(DbType.String, "ShipName", False, False, Nothing)
            _fields(9) = New DatabaseField(DbType.String, "ShipAddress", False, False, Nothing)
            _fields(10) = New DatabaseField(DbType.String, "ShipCity", False, False, Nothing)
            _fields(11) = New DatabaseField(DbType.String, "ShipRegion", False, False, Nothing)
            _fields(12) = New DatabaseField(DbType.String, "ShipPostalCode", False, False, Nothing)
            _fields(13) = New DatabaseField(DbType.String, "ShipCountry", False, False, Nothing)
            Me.tableName = "Orders"
            Me.alRelations.Add(New TableRelation("FK_OrderDetails", "Order Details", "OrderID"))
        End Sub

        Public Overloads Overrides Property TableFields() As DatabaseField()
            Get
                Return _fields
            End Get
            Set(ByVal Value As DatabaseField())
                _fields = value
            End Set
        End Property

        Public Property OrderID() As System.Int32
            Get
                Return CType((Me.GetField("OrderID")).fieldValue, System.Int32)
            End Get
            Set(ByVal Value As System.Int32)
                Me.SetFieldValue("OrderID", value)
            End Set
        End Property

        Public Property CustomerID() As System.String
            Get
                Return CType((Me.GetField("CustomerID")).fieldValue, System.String)
            End Get
            Set(ByVal Value As System.String)
                Me.SetFieldValue("CustomerID", value)
            End Set
        End Property

        Public Property EmployeeID() As System.Int32
            Get
                Return CType((Me.GetField("EmployeeID")).fieldValue, System.Int32)
            End Get
            Set(ByVal Value As System.Int32)
                Me.SetFieldValue("EmployeeID", value)
            End Set
        End Property

        Public Property OrderDate() As Date
            Get
                Return CType((Me.GetField("OrderDate")).fieldValue, Date)
            End Get
            Set(ByVal Value As Date)
                Me.SetFieldValue("OrderDate", value)
            End Set
        End Property

        Public Property RequiredDate() As Date
            Get
                Return CType((Me.GetField("RequiredDate")).fieldValue, Date)
            End Get
            Set(ByVal Value As Date)
                Me.SetFieldValue("RequiredDate", value)
            End Set
        End Property

        Public Property ShippedDate() As Date
            Get
                Return CType((Me.GetField("ShippedDate")).fieldValue, Date)
            End Get
            Set(ByVal Value As Date)
                Me.SetFieldValue("ShippedDate", value)
            End Set
        End Property

        Public Property ShipVia() As System.Int32
            Get
                Return CType((Me.GetField("ShipVia")).fieldValue, System.Int32)
            End Get
            Set(ByVal Value As System.Int32)
                Me.SetFieldValue("ShipVia", value)
            End Set
        End Property

        Public Property Freight() As System.Decimal
            Get
                Return CType((Me.GetField("Freight")).fieldValue, System.Decimal)
            End Get
            Set(ByVal Value As System.Decimal)
                Me.SetFieldValue("Freight", value)
            End Set
        End Property

        Public Property ShipName() As System.String
            Get
                Return CType((Me.GetField("ShipName")).fieldValue, System.String)
            End Get
            Set(ByVal Value As System.String)
                Me.SetFieldValue("ShipName", value)
            End Set
        End Property

        Public Property ShipAddress() As System.String
            Get
                Return CType((Me.GetField("ShipAddress")).fieldValue, System.String)
            End Get
            Set(ByVal Value As System.String)
                Me.SetFieldValue("ShipAddress", value)
            End Set
        End Property

        Public Property ShipCity() As System.String
            Get
                Return CType((Me.GetField("ShipCity")).fieldValue, System.String)
            End Get
            Set(ByVal Value As System.String)
                Me.SetFieldValue("ShipCity", value)
            End Set
        End Property

        Public Property ShipRegion() As System.String
            Get
                Return CType((Me.GetField("ShipRegion")).fieldValue, System.String)
            End Get
            Set(ByVal Value As System.String)
                Me.SetFieldValue("ShipRegion", value)
            End Set
        End Property

        Public Property ShipPostalCode() As System.String
            Get
                Return CType((Me.GetField("ShipPostalCode")).fieldValue, System.String)
            End Get
            Set(ByVal Value As System.String)
                Me.SetFieldValue("ShipPostalCode", value)
            End Set
        End Property

        Public Property ShipCountry() As System.String
            Get
                Return CType((Me.GetField("ShipCountry")).fieldValue, System.String)
            End Get
            Set(ByVal Value As System.String)
                Me.SetFieldValue("ShipCountry", value)
            End Set
        End Property
    End Class
End Namespace

