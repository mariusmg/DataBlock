


Option Strict On


Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports ExtenderTerritories
Imports voidsoft.DataBlock
Imports voidsoft.NorthwindSample

Namespace ExtenderTerritories

    Public Class TerritoriesUI
        Inherits System.Windows.Forms.Form

        Private dataGrid As System.Windows.Forms.DataGrid
        Private buttonCreate As System.Windows.Forms.Button
        Private buttonDelete As System.Windows.Forms.Button
        Private buttonUpdate As System.Windows.Forms.Button
        Private territoriesList As TerritoriesTableMetadata() = Nothing
        Private tpo As TerritoriesPersistentObject = Nothing
        Private tm As TerritoriesTableMetadata = Nothing
        Private buttonExit As System.Windows.Forms.Button
        Private components As System.ComponentModel.Container = Nothing

        Public Sub New()
            InitializeComponent()
            tm = New TerritoriesTableMetadata
            tpo = New TerritoriesPersistentObject(SharedData.DatabaseServer, SharedData.ConnectionString, tm)
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
            Me.dataGrid = New System.Windows.Forms.DataGrid
            Me.buttonCreate = New System.Windows.Forms.Button
            Me.buttonExit = New System.Windows.Forms.Button
            Me.buttonDelete = New System.Windows.Forms.Button
            Me.buttonUpdate = New System.Windows.Forms.Button
            CType((Me.dataGrid), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            Me.dataGrid.DataMember = ""
            Me.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
            Me.dataGrid.Location = New System.Drawing.Point(8, 8)
            Me.dataGrid.Name = "dataGrid"
            Me.dataGrid.Size = New System.Drawing.Size(392, 336)
            Me.dataGrid.TabIndex = 0
            Me.buttonCreate.Location = New System.Drawing.Point(416, 24)
            Me.buttonCreate.Name = "buttonCreate"
            Me.buttonCreate.Size = New System.Drawing.Size(96, 32)
            Me.buttonCreate.TabIndex = 1
            Me.buttonCreate.Text = "&Create"
            AddHandler Me.buttonCreate.Click, AddressOf Me.buttonCreate_Click
            Me.buttonExit.Location = New System.Drawing.Point(424, 312)
            Me.buttonExit.Name = "buttonExit"
            Me.buttonExit.Size = New System.Drawing.Size(88, 32)
            Me.buttonExit.TabIndex = 2
            Me.buttonExit.Text = "&Exit"
            AddHandler Me.buttonExit.Click, AddressOf Me.buttonExit_Click
            Me.buttonDelete.Location = New System.Drawing.Point(416, 72)
            Me.buttonDelete.Name = "buttonDelete"
            Me.buttonDelete.Size = New System.Drawing.Size(96, 32)
            Me.buttonDelete.TabIndex = 3
            Me.buttonDelete.Text = "&Delete"
            AddHandler Me.buttonDelete.Click, AddressOf Me.buttonDelete_Click
            Me.buttonUpdate.Location = New System.Drawing.Point(416, 120)
            Me.buttonUpdate.Name = "buttonUpdate"
            Me.buttonUpdate.Size = New System.Drawing.Size(96, 32)
            Me.buttonUpdate.TabIndex = 4
            Me.buttonUpdate.Text = "&Update"
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(528, 355)
            Me.Controls.Add(Me.buttonUpdate)
            Me.Controls.Add(Me.buttonDelete)
            Me.Controls.Add(Me.buttonExit)
            Me.Controls.Add(Me.buttonCreate)
            Me.Controls.Add(Me.dataGrid)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "TerritoriesUI"
            Me.Text = "TerritoriesUI"
            AddHandler Me.Load, AddressOf Me.TerritoriesUI_Load
            CType((Me.dataGrid), System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
        End Sub

        Private Sub TerritoriesUI_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Me.territoriesList = CType(tpo.GetTableMetadata, TerritoriesTableMetadata())
                Me.dataGrid.DataSource = Me.territoriesList
                Me.dataGrid.DataMember = "TerritoryDescription"
            Catch ex As Exception
                MessageBox.Show("Error occurred." + ex.Message)
            End Try
        End Sub

        Private Sub buttonExit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Me.Close()
        End Sub

        Private Sub buttonDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim selectedIndex As Integer = Me.dataGrid.CurrentRowIndex
                If selectedIndex > -1 Then
                    Dim tp As TerritoriesTableMetadata = Me.territoriesList(selectedIndex)
                    Me.tpo.Delete(tp, False)
                Else
                    MessageBox.Show("Please select a item from the grid")
                    Return
                End If
            Catch ex As Exception
            Finally
            End Try
        End Sub

        Private Sub buttonCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        End Sub
    End Class
End Namespace
