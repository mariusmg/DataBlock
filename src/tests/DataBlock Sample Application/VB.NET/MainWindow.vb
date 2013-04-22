
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports ExtenderTerritories
Imports voidsoft.NorthwindSample
Imports voidsoft.DataBlock
Imports ExtenderOrders
Imports ExtenderRegion




Namespace voidsoft.NorthwindSample

    Public Class MainWindow
        Inherits System.Windows.Forms.Form
        Private WithEvents buttonTerritories As System.Windows.Forms.Button
        Private label1 As System.Windows.Forms.Label
        Private WithEvents textBoxConnectionString As System.Windows.Forms.TextBox
        Private label2 As System.Windows.Forms.Label
        Private label3 As System.Windows.Forms.Label
        Private WithEvents buttonRegions As System.Windows.Forms.Button
        Private WithEvents buttonOrders As System.Windows.Forms.Button
        Private WithEvents button1 As System.Windows.Forms.Button
        Private WithEvents comboBoxServers As System.Windows.Forms.ComboBox
        Private components As System.ComponentModel.Container = Nothing

        Public Sub New()
            InitializeComponent()
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
            Me.buttonTerritories = New System.Windows.Forms.Button
            Me.label1 = New System.Windows.Forms.Label
            Me.textBoxConnectionString = New System.Windows.Forms.TextBox
            Me.label2 = New System.Windows.Forms.Label
            Me.buttonRegions = New System.Windows.Forms.Button
            Me.label3 = New System.Windows.Forms.Label
            Me.buttonOrders = New System.Windows.Forms.Button
            Me.button1 = New System.Windows.Forms.Button
            Me.comboBoxServers = New System.Windows.Forms.ComboBox
            Me.SuspendLayout()
            '
            'buttonTerritories
            '
            Me.buttonTerritories.Location = New System.Drawing.Point(16, 128)
            Me.buttonTerritories.Name = "buttonTerritories"
            Me.buttonTerritories.Size = New System.Drawing.Size(80, 48)
            Me.buttonTerritories.TabIndex = 0
            Me.buttonTerritories.Text = "Territories"
            '
            'label1
            '
            Me.label1.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.label1.Location = New System.Drawing.Point(80, 16)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(368, 24)
            Me.label1.TabIndex = 2
            Me.label1.Text = "DataBlock Demo Application"
            '
            'textBoxConnectionString
            '
            Me.textBoxConnectionString.Location = New System.Drawing.Point(112, 88)
            Me.textBoxConnectionString.Name = "textBoxConnectionString"
            Me.textBoxConnectionString.Size = New System.Drawing.Size(304, 20)
            Me.textBoxConnectionString.TabIndex = 3
            Me.textBoxConnectionString.Text = ""
            '
            'label2
            '
            Me.label2.Location = New System.Drawing.Point(8, 88)
            Me.label2.Name = "label2"
            Me.label2.Size = New System.Drawing.Size(96, 16)
            Me.label2.TabIndex = 4
            Me.label2.Text = "Connection String"
            '
            'buttonRegions
            '
            Me.buttonRegions.BackColor = System.Drawing.SystemColors.Control
            Me.buttonRegions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
            Me.buttonRegions.Location = New System.Drawing.Point(464, 128)
            Me.buttonRegions.Name = "buttonRegions"
            Me.buttonRegions.Size = New System.Drawing.Size(112, 48)
            Me.buttonRegions.TabIndex = 5
            Me.buttonRegions.Text = "&Regions"
            '
            'label3
            '
            Me.label3.Location = New System.Drawing.Point(192, 48)
            Me.label3.Name = "label3"
            Me.label3.Size = New System.Drawing.Size(112, 16)
            Me.label3.TabIndex = 6
            Me.label3.Text = "Northwind database"
            '
            'buttonOrders
            '
            Me.buttonOrders.Location = New System.Drawing.Point(112, 128)
            Me.buttonOrders.Name = "buttonOrders"
            Me.buttonOrders.Size = New System.Drawing.Size(144, 48)
            Me.buttonOrders.TabIndex = 7
            Me.buttonOrders.Text = "&Orders - normal mode"
            '
            'button1
            '
            Me.button1.Location = New System.Drawing.Point(296, 128)
            Me.button1.Name = "button1"
            Me.button1.Size = New System.Drawing.Size(152, 48)
            Me.button1.TabIndex = 8
            Me.button1.Text = "&Orders - with lazy loading"
            '
            'comboBoxServers
            '
            Me.comboBoxServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.comboBoxServers.Items.AddRange(New Object() {"SqlServer", "Access", "MySql"})
            Me.comboBoxServers.Location = New System.Drawing.Point(424, 88)
            Me.comboBoxServers.Name = "comboBoxServers"
            Me.comboBoxServers.Size = New System.Drawing.Size(136, 21)
            Me.comboBoxServers.TabIndex = 9
            '
            'Button2
            '
            '
            'MainWindow
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(586, 205)
            Me.Controls.Add(Me.comboBoxServers)
            Me.Controls.Add(Me.button1)
            Me.Controls.Add(Me.buttonOrders)
            Me.Controls.Add(Me.label3)
            Me.Controls.Add(Me.buttonRegions)
            Me.Controls.Add(Me.label2)
            Me.Controls.Add(Me.textBoxConnectionString)
            Me.Controls.Add(Me.label1)
            Me.Controls.Add(Me.buttonTerritories)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "MainWindow"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Main Window"
            Me.ResumeLayout(False)

        End Sub

        <STAThread()> _
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.Run(New MainWindow)
        End Sub

        Private Sub MainWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.comboBoxServers.SelectedIndex = 0
        End Sub

        Private Sub buttonTerritories_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonTerritories.Click
            Dim tr As TerritoriesUI = Nothing
            Try
                tr = New TerritoriesUI
                tr.ShowDialog()
            Catch ex As Exception
            Finally
                If Not (tr Is Nothing) Then
                    tr.Dispose()
                End If
            End Try
        End Sub

        Private Sub textBoxConnectionString_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles textBoxConnectionString.TextChanged
            SharedData.ConnectionString = Me.textBoxConnectionString.Text.Trim
        End Sub

        Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonRegions.Click
            Dim tr As RegionsUI = Nothing
            Try
                tr = New RegionsUI
                tr.ShowDialog()
            Catch ex As Exception
            Finally
                If Not (tr Is Nothing) Then
                    tr.Dispose()
                End If
            End Try
        End Sub

        Private Sub buttonOrders_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles buttonOrders.Click
            Dim ors As Orders = Nothing
            Try
                ors = New Orders(False)
                ors.ShowDialog()
            Catch ex As Exception
            Finally
                If Not (ors Is Nothing) Then
                    ors.Dispose()
                End If
            End Try
        End Sub

        Private Sub buttonOrdersLazy_Lick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button1.Click
            Dim ors As Orders = Nothing
            Try
                ors = New Orders(True)
                ors.ShowDialog()
            Catch ex As Exception
            Finally
                If Not (ors Is Nothing) Then
                    ors.Dispose()
                End If
            End Try
        End Sub

        Private Sub comboBoxServers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboBoxServers.SelectedIndexChanged
            If Me.comboBoxServers.SelectedIndex = 0 Then
                Me.textBoxConnectionString.Text = "server=klamath\klamathpc;user id=sa; password=1234;database = northwind"
                SharedData.DatabaseServer = EDatabase.SqlServer
            ElseIf Me.comboBoxServers.SelectedIndex = 1 Then
                Me.textBoxConnectionString.Text = "Provider=Microsoft.Jet.OLEDB.4.0 ;Data Source=d:\northwind.mdb"
                SharedData.DatabaseServer = EDatabase.Access
            Else
                Me.textBoxConnectionString.Text = "Host=localhost; UserName=root; Password=;Database=Northwind"
                SharedData.DatabaseServer = EDatabase.MySQL
            End If
        End Sub

     
    End Class
End Namespace
