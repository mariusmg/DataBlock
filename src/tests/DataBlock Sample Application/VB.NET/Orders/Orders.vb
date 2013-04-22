Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Collections.Specialized
Imports voidsoft.NorthwindSample
Imports voidsoft.DataBlock
Imports ExtenderOrders

Namespace ExtenderOrders

    Public Class Orders
        Inherits System.Windows.Forms.Form
        Private comboBoxOrdersId As System.Windows.Forms.ComboBox
        Private labelOrderId As System.Windows.Forms.Label
        Private components As System.ComponentModel.Container = Nothing
        Private order As OrdersTableMetadata = Nothing
        Private orderPersist As OrderPersistentObject = Nothing
        Private textBoxCustomerId As System.Windows.Forms.TextBox
        Private label1 As System.Windows.Forms.Label
        Private labelEmployeeId As System.Windows.Forms.Label
        Private textBoxEmployeeId As System.Windows.Forms.TextBox
        Private dateTimePickerOrderDate As System.Windows.Forms.DateTimePicker
        Private label2 As System.Windows.Forms.Label
        Private label3 As System.Windows.Forms.Label
        Private dateTimePickerRequiredDate As System.Windows.Forms.DateTimePicker
        Private label4 As System.Windows.Forms.Label
        Private dateTimePickerShippedDate As System.Windows.Forms.DateTimePicker
        Private label5 As System.Windows.Forms.Label
        Private textBoxShippedVia As System.Windows.Forms.TextBox
        Private labelFreight As System.Windows.Forms.Label
        Private textBoxFreight As System.Windows.Forms.TextBox
        Private label6 As System.Windows.Forms.Label
        Private textBoxShipAddress As System.Windows.Forms.TextBox
        Private label7 As System.Windows.Forms.Label
        Private textBoxShipName As System.Windows.Forms.TextBox
        Private label8 As System.Windows.Forms.Label
        Private textBoxShipCity As System.Windows.Forms.TextBox
        Private label9 As System.Windows.Forms.Label
        Private textBoxShipRegion As System.Windows.Forms.TextBox
        Private label10 As System.Windows.Forms.Label
        Private textBoxShipPostalCode As System.Windows.Forms.TextBox
        Private label11 As System.Windows.Forms.Label
        Private textBoxShipCountry As System.Windows.Forms.TextBox
        Private groupBox1 As System.Windows.Forms.GroupBox
        Private groupBox2 As System.Windows.Forms.GroupBox
        Private dataGrid As System.Windows.Forms.DataGrid
        Private orders As OrdersTableMetadata() = Nothing
        Private orderDetails As OrderDetailsTableMetadata() = Nothing
        Private buttonDelete As System.Windows.Forms.Button
        Private useLazyLoading As Boolean = False

        Public Sub New(ByVal useLazyLoading As Boolean)
            InitializeComponent()
            order = New OrdersTableMetadata
            orderPersist = New OrderPersistentObject(SharedData.DatabaseServer, SharedData.ConnectionString, order)
            Me.useLazyLoading = useLazyLoading
        End Sub

        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        Private Sub InitializeComponent()
            Me.comboBoxOrdersId = New System.Windows.Forms.ComboBox
            Me.labelOrderId = New System.Windows.Forms.Label
            Me.textBoxCustomerId = New System.Windows.Forms.TextBox
            Me.label1 = New System.Windows.Forms.Label
            Me.labelEmployeeId = New System.Windows.Forms.Label
            Me.textBoxEmployeeId = New System.Windows.Forms.TextBox
            Me.dateTimePickerOrderDate = New System.Windows.Forms.DateTimePicker
            Me.label2 = New System.Windows.Forms.Label
            Me.label3 = New System.Windows.Forms.Label
            Me.dateTimePickerRequiredDate = New System.Windows.Forms.DateTimePicker
            Me.label4 = New System.Windows.Forms.Label
            Me.dateTimePickerShippedDate = New System.Windows.Forms.DateTimePicker
            Me.label5 = New System.Windows.Forms.Label
            Me.textBoxShippedVia = New System.Windows.Forms.TextBox
            Me.labelFreight = New System.Windows.Forms.Label
            Me.textBoxFreight = New System.Windows.Forms.TextBox
            Me.label6 = New System.Windows.Forms.Label
            Me.textBoxShipAddress = New System.Windows.Forms.TextBox
            Me.label7 = New System.Windows.Forms.Label
            Me.textBoxShipName = New System.Windows.Forms.TextBox
            Me.label8 = New System.Windows.Forms.Label
            Me.textBoxShipCity = New System.Windows.Forms.TextBox
            Me.label9 = New System.Windows.Forms.Label
            Me.textBoxShipRegion = New System.Windows.Forms.TextBox
            Me.label10 = New System.Windows.Forms.Label
            Me.textBoxShipPostalCode = New System.Windows.Forms.TextBox
            Me.label11 = New System.Windows.Forms.Label
            Me.textBoxShipCountry = New System.Windows.Forms.TextBox
            Me.groupBox1 = New System.Windows.Forms.GroupBox
            Me.dataGrid = New System.Windows.Forms.DataGrid
            Me.groupBox2 = New System.Windows.Forms.GroupBox
            Me.buttonDelete = New System.Windows.Forms.Button
            Me.groupBox1.SuspendLayout()
            CType((Me.dataGrid), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.groupBox2.SuspendLayout()
            Me.SuspendLayout()
            Me.comboBoxOrdersId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.comboBoxOrdersId.Location = New System.Drawing.Point(16, 24)
            Me.comboBoxOrdersId.Name = "comboBoxOrdersId"
            Me.comboBoxOrdersId.Size = New System.Drawing.Size(160, 21)
            Me.comboBoxOrdersId.TabIndex = 0
            AddHandler Me.comboBoxOrdersId.SelectedIndexChanged, AddressOf Me.comboBoxOrdersId_SelectedIndexChanged
            Me.labelOrderId.Location = New System.Drawing.Point(16, 8)
            Me.labelOrderId.Name = "labelOrderId"
            Me.labelOrderId.Size = New System.Drawing.Size(88, 16)
            Me.labelOrderId.TabIndex = 1
            Me.labelOrderId.Text = "OrderID"
            Me.textBoxCustomerId.Location = New System.Drawing.Point(24, 104)
            Me.textBoxCustomerId.Name = "textBoxCustomerId"
            Me.textBoxCustomerId.Size = New System.Drawing.Size(152, 20)
            Me.textBoxCustomerId.TabIndex = 2
            Me.textBoxCustomerId.Text = ""
            Me.label1.Location = New System.Drawing.Point(24, 88)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(112, 16)
            Me.label1.TabIndex = 3
            Me.label1.Text = "CustomerId"
            Me.labelEmployeeId.Location = New System.Drawing.Point(24, 136)
            Me.labelEmployeeId.Name = "labelEmployeeId"
            Me.labelEmployeeId.Size = New System.Drawing.Size(104, 16)
            Me.labelEmployeeId.TabIndex = 4
            Me.labelEmployeeId.Text = "EmployeeId"
            Me.textBoxEmployeeId.Location = New System.Drawing.Point(24, 152)
            Me.textBoxEmployeeId.Name = "textBoxEmployeeId"
            Me.textBoxEmployeeId.Size = New System.Drawing.Size(152, 20)
            Me.textBoxEmployeeId.TabIndex = 5
            Me.textBoxEmployeeId.Text = ""
            Me.dateTimePickerOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dateTimePickerOrderDate.Location = New System.Drawing.Point(24, 200)
            Me.dateTimePickerOrderDate.Name = "dateTimePickerOrderDate"
            Me.dateTimePickerOrderDate.Size = New System.Drawing.Size(152, 20)
            Me.dateTimePickerOrderDate.TabIndex = 6
            Me.label2.Location = New System.Drawing.Point(24, 184)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(104, 16)
            Me.label2.TabIndex = 7
            Me.label2.Text = "Order Date"
            Me.label3.Location = New System.Drawing.Point(24, 240)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(104, 16)
            Me.label3.TabIndex = 9
            Me.label3.Text = "Required Date"
            Me.dateTimePickerRequiredDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dateTimePickerRequiredDate.Location = New System.Drawing.Point(24, 256)
            Me.dateTimePickerRequiredDate.Name = "dateTimePickerRequiredDate"
            Me.dateTimePickerRequiredDate.Size = New System.Drawing.Size(152, 20)
            Me.dateTimePickerRequiredDate.TabIndex = 8
            Me.label4.Location = New System.Drawing.Point(24, 296)
            Me.label4.Name = "label4"
            Me.label4.Size = New System.Drawing.Size(104, 16)
            Me.label4.TabIndex = 11
            Me.label4.Text = "Shipped Date"
            Me.dateTimePickerShippedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short
            Me.dateTimePickerShippedDate.Location = New System.Drawing.Point(24, 312)
            Me.dateTimePickerShippedDate.Name = "dateTimePickerShippedDate"
            Me.dateTimePickerShippedDate.Size = New System.Drawing.Size(152, 20)
            Me.dateTimePickerShippedDate.TabIndex = 10
            Me.label5.Location = New System.Drawing.Point(376, 184)
            Me.label5.Name = "label5"
            Me.label5.Size = New System.Drawing.Size(112, 16)
            Me.label5.TabIndex = 13
            Me.label5.Text = "Shipped Via"
            Me.textBoxShippedVia.Location = New System.Drawing.Point(376, 200)
            Me.textBoxShippedVia.Name = "textBoxShippedVia"
            Me.textBoxShippedVia.Size = New System.Drawing.Size(152, 20)
            Me.textBoxShippedVia.TabIndex = 12
            Me.textBoxShippedVia.Text = ""
            Me.labelFreight.Location = New System.Drawing.Point(208, 88)
            Me.labelFreight.Name = "labelFreight"
            Me.labelFreight.Size = New System.Drawing.Size(112, 16)
            Me.labelFreight.TabIndex = 15
            Me.labelFreight.Text = "Freight"
            Me.textBoxFreight.Location = New System.Drawing.Point(208, 104)
            Me.textBoxFreight.Name = "textBoxFreight"
            Me.textBoxFreight.Size = New System.Drawing.Size(152, 20)
            Me.textBoxFreight.TabIndex = 14
            Me.textBoxFreight.Text = ""
            Me.label6.Location = New System.Drawing.Point(376, 72)
            Me.label6.Name = "label6"
            Me.label6.Size = New System.Drawing.Size(112, 16)
            Me.label6.TabIndex = 17
            Me.label6.Text = "ShipAddress"
            Me.textBoxShipAddress.Location = New System.Drawing.Point(376, 88)
            Me.textBoxShipAddress.Name = "textBoxShipAddress"
            Me.textBoxShipAddress.Size = New System.Drawing.Size(152, 20)
            Me.textBoxShipAddress.TabIndex = 16
            Me.textBoxShipAddress.Text = ""
            Me.label7.Location = New System.Drawing.Point(208, 136)
            Me.label7.Name = "label7"
            Me.label7.Size = New System.Drawing.Size(112, 16)
            Me.label7.TabIndex = 19
            Me.label7.Text = "ShipName"
            Me.textBoxShipName.Location = New System.Drawing.Point(208, 152)
            Me.textBoxShipName.Name = "textBoxShipName"
            Me.textBoxShipName.Size = New System.Drawing.Size(152, 20)
            Me.textBoxShipName.TabIndex = 18
            Me.textBoxShipName.Text = ""
            Me.label8.Location = New System.Drawing.Point(200, 128)
            Me.label8.Name = "label8"
            Me.label8.Size = New System.Drawing.Size(112, 16)
            Me.label8.TabIndex = 21
            Me.label8.Text = "ShipCity"
            Me.textBoxShipCity.Location = New System.Drawing.Point(200, 144)
            Me.textBoxShipCity.Name = "textBoxShipCity"
            Me.textBoxShipCity.Size = New System.Drawing.Size(152, 20)
            Me.textBoxShipCity.TabIndex = 20
            Me.textBoxShipCity.Text = ""
            Me.label9.Location = New System.Drawing.Point(208, 248)
            Me.label9.Name = "label9"
            Me.label9.Size = New System.Drawing.Size(112, 16)
            Me.label9.TabIndex = 23
            Me.label9.Text = "ShipRegion"
            Me.textBoxShipRegion.Location = New System.Drawing.Point(208, 264)
            Me.textBoxShipRegion.Name = "textBoxShipRegion"
            Me.textBoxShipRegion.Size = New System.Drawing.Size(152, 20)
            Me.textBoxShipRegion.TabIndex = 22
            Me.textBoxShipRegion.Text = ""
            Me.label10.Location = New System.Drawing.Point(376, 128)
            Me.label10.Name = "label10"
            Me.label10.Size = New System.Drawing.Size(112, 16)
            Me.label10.TabIndex = 25
            Me.label10.Text = "ShipPostalCode"
            Me.textBoxShipPostalCode.Location = New System.Drawing.Point(376, 144)
            Me.textBoxShipPostalCode.Name = "textBoxShipPostalCode"
            Me.textBoxShipPostalCode.Size = New System.Drawing.Size(152, 20)
            Me.textBoxShipPostalCode.TabIndex = 24
            Me.textBoxShipPostalCode.Text = ""
            Me.label11.Location = New System.Drawing.Point(384, 88)
            Me.label11.Name = "label11"
            Me.label11.Size = New System.Drawing.Size(112, 16)
            Me.label11.TabIndex = 27
            Me.label11.Text = "ShipCountry"
            Me.textBoxShipCountry.Location = New System.Drawing.Point(384, 104)
            Me.textBoxShipCountry.Name = "textBoxShipCountry"
            Me.textBoxShipCountry.Size = New System.Drawing.Size(152, 20)
            Me.textBoxShipCountry.TabIndex = 26
            Me.textBoxShipCountry.Text = ""
            Me.groupBox1.Controls.Add(Me.label6)
            Me.groupBox1.Controls.Add(Me.textBoxShipAddress)
            Me.groupBox1.Controls.Add(Me.label8)
            Me.groupBox1.Controls.Add(Me.textBoxShipCity)
            Me.groupBox1.Controls.Add(Me.textBoxShipPostalCode)
            Me.groupBox1.Controls.Add(Me.label10)
            Me.groupBox1.Controls.Add(Me.label5)
            Me.groupBox1.Controls.Add(Me.textBoxShippedVia)
            Me.groupBox1.Location = New System.Drawing.Point(8, 64)
            Me.groupBox1.Name = "groupBox1"
            Me.groupBox1.Size = New System.Drawing.Size(536, 280)
            Me.groupBox1.TabIndex = 28
            Me.groupBox1.TabStop = False
            Me.groupBox1.Text = "Order"
            Me.dataGrid.DataMember = ""
            Me.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
            Me.dataGrid.Location = New System.Drawing.Point(8, 16)
            Me.dataGrid.Name = "dataGrid"
            Me.dataGrid.Size = New System.Drawing.Size(520, 144)
            Me.dataGrid.TabIndex = 29
            Me.groupBox2.Controls.Add(Me.dataGrid)
            Me.groupBox2.Location = New System.Drawing.Point(8, 352)
            Me.groupBox2.Name = "groupBox2"
            Me.groupBox2.Size = New System.Drawing.Size(536, 168)
            Me.groupBox2.TabIndex = 30
            Me.groupBox2.TabStop = False
            Me.groupBox2.Text = "Order Details"
            Me.buttonDelete.Location = New System.Drawing.Point(328, 24)
            Me.buttonDelete.Name = "buttonDelete"
            Me.buttonDelete.Size = New System.Drawing.Size(96, 24)
            Me.buttonDelete.TabIndex = 31
            Me.buttonDelete.Text = "&Delete"
            AddHandler Me.buttonDelete.Click, AddressOf Me.buttonDelete_Click
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(546, 520)
            Me.Controls.Add(Me.buttonDelete)
            Me.Controls.Add(Me.groupBox2)
            Me.Controls.Add(Me.label11)
            Me.Controls.Add(Me.textBoxShipCountry)
            Me.Controls.Add(Me.label9)
            Me.Controls.Add(Me.textBoxShipRegion)
            Me.Controls.Add(Me.label7)
            Me.Controls.Add(Me.textBoxShipName)
            Me.Controls.Add(Me.labelFreight)
            Me.Controls.Add(Me.textBoxFreight)
            Me.Controls.Add(Me.label4)
            Me.Controls.Add(Me.dateTimePickerShippedDate)
            Me.Controls.Add(Me.label3)
            Me.Controls.Add(Me.dateTimePickerRequiredDate)
            Me.Controls.Add(Me.label2)
            Me.Controls.Add(Me.dateTimePickerOrderDate)
            Me.Controls.Add(Me.textBoxEmployeeId)
            Me.Controls.Add(Me.labelEmployeeId)
            Me.Controls.Add(Me.label1)
            Me.Controls.Add(Me.textBoxCustomerId)
            Me.Controls.Add(Me.labelOrderId)
            Me.Controls.Add(Me.comboBoxOrdersId)
            Me.Controls.Add(Me.groupBox1)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            Me.Name = "Orders"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.Text = "Orders"
            AddHandler Me.Load, AddressOf Me.Orders_Load
            Me.groupBox1.ResumeLayout(False)
            CType((Me.dataGrid), System.ComponentModel.ISupportInitialize).EndInit()
            Me.groupBox2.ResumeLayout(False)
            Me.ResumeLayout(False)
        End Sub

        Private Sub Orders_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            Me.LoadData()
        End Sub

        Private Sub LoadData()
            Try
                If useLazyLoading = False Then
                    Me.orders = CType(Me.orderPersist.GetTableMetadata, OrdersTableMetadata())
                    Me.comboBoxOrdersId.DataSource = Me.orders
                    Me.comboBoxOrdersId.DisplayMember = "OrderID"
                Else
                    Dim scData As StringCollection = Me.orderPersist.GetFieldList(Me.order.GetPrimaryKeyField)
                    Me.comboBoxOrdersId.DataSource = scData
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub comboBoxOrdersId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim selectedIndex As Integer = Me.comboBoxOrdersId.SelectedIndex
                If Me.useLazyLoading = False Then
                    Me.order = Me.orders(selectedIndex)
                Else
                    Dim value As Object = Me.comboBoxOrdersId.SelectedItem
                    Me.order = CType(Me.orderPersist.GetTableMetadata(value), OrdersTableMetadata)
                End If
                Me.textBoxCustomerId.Text = Me.order.CustomerID
                Me.textBoxEmployeeId.Text = Me.order.EmployeeID.ToString
                Me.dateTimePickerOrderDate.Value = Me.order.OrderDate
                Me.dateTimePickerRequiredDate.Value = Me.order.RequiredDate
                Me.dateTimePickerShippedDate.Value = Me.order.ShippedDate
                Me.textBoxFreight.Text = Me.order.Freight.ToString
                Me.textBoxShipAddress.Text = Me.order.ShipAddress
                Me.textBoxShipCity.Text = Me.order.ShipCity
                Me.textBoxShipCountry.Text = Me.order.ShipCountry
                Me.textBoxShipName.Text = Me.order.ShipName
                If Me.order.IsNull("ShipVia") Then
                    Me.textBoxShippedVia.Text = ""
                Else
                    Me.textBoxShippedVia.Text = Me.order.ShipVia.ToString
                End If
                If Me.order.IsNull("ShipPostalCode") Then
                    Me.textBoxShipPostalCode.Text = ""
                Else
                    Me.textBoxShipPostalCode.Text = Me.order.ShipPostalCode
                End If
                If Me.order.IsNull("ShipRegion") Then
                    Me.textBoxShipRegion.Text = ""
                Else
                    Me.textBoxShipRegion.Text = Me.order.ShipRegion
                End If
                Me.LoadDetails()
            Catch ex As Exception
            Finally
            End Try
        End Sub

        Private Sub LoadDetails()
            Try
                Me.dataGrid.DataSource = Nothing
                Dim value As Object = Nothing
                If Me.useLazyLoading Then
                    value = Me.comboBoxOrdersId.SelectedItem
                Else
                    value = CType(Me.comboBoxOrdersId.SelectedItem, OrdersTableMetadata).OrderID
                End If
                Me.orderDetails = CType(Me.orderPersist.GetTableMetadata("FK_OrderDetails", "ExtenderOrders.OrderDetailsTableMetadata", value), OrderDetailsTableMetadata())
                Me.dataGrid.DataSource = Me.orderDetails
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Sub

        Private Sub buttonDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                If MessageBox.Show("Are you sure you want to delete the current order ?", "DataBlock Demo Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    Me.orderPersist.Delete(Me.order, Me.orderDetails)
                End If
            Catch ex As Exception
                MessageBox.Show("Failed to delete the current order")
            End Try
        End Sub
    End Class
End Namespace
