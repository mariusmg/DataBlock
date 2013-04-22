Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports ExtenderRegion
Imports voidsoft.NorthwindSample
Imports voidsoft.DataBlock
Namespace ExtenderRegion

    Public Class RegionsUI

        Inherits System.Windows.Forms.Form
        Private components As System.ComponentModel.Container = Nothing
        Private region As RegionTableMetadata = Nothing
        Private dataGrid As System.Windows.Forms.DataGrid
        Private buttonCreate As System.Windows.Forms.Button
        Private buttonDelete As System.Windows.Forms.Button
        Private buttonUpdate As System.Windows.Forms.Button
        Private regionPersistent As RegionPersistentObject = Nothing
        Private textRegionDescription As System.Windows.Forms.TextBox
        Private ds As DataSet = Nothing
        Private label1 As System.Windows.Forms.Label
        Private regions As RegionTableMetadata() = Nothing

        Public Sub New()
            InitializeComponent()
            region = New RegionTableMetadata
            regionPersistent = New RegionPersistentObject(SharedData.DatabaseServer, SharedData.ConnectionString, region)
            ds = New DataSet
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
            Me.buttonDelete = New System.Windows.Forms.Button
            Me.buttonUpdate = New System.Windows.Forms.Button
            Me.textRegionDescription = New System.Windows.Forms.TextBox
            Me.label1 = New System.Windows.Forms.Label
            CType((Me.dataGrid), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            Me.dataGrid.DataMember = ""
            Me.dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText
            Me.dataGrid.Location = New System.Drawing.Point(8, 8)
            Me.dataGrid.Name = "dataGrid"
            Me.dataGrid.Size = New System.Drawing.Size(344, 304)
            Me.dataGrid.TabIndex = 0
            AddHandler Me.dataGrid.CurrentCellChanged, AddressOf Me.dataGrid_CurrentCellChanged
            Me.buttonCreate.Location = New System.Drawing.Point(368, 8)
            Me.buttonCreate.Name = "buttonCreate"
            Me.buttonCreate.Size = New System.Drawing.Size(80, 32)
            Me.buttonCreate.TabIndex = 1
            Me.buttonCreate.Text = "&Create"
            AddHandler Me.buttonCreate.Click, AddressOf Me.buttonCreate_Click
            Me.buttonDelete.Location = New System.Drawing.Point(368, 48)
            Me.buttonDelete.Name = "buttonDelete"
            Me.buttonDelete.Size = New System.Drawing.Size(80, 32)
            Me.buttonDelete.TabIndex = 2
            Me.buttonDelete.Text = "&Delete"
            AddHandler Me.buttonDelete.Click, AddressOf Me.buttonDelete_Click
            Me.buttonUpdate.Location = New System.Drawing.Point(368, 96)
            Me.buttonUpdate.Name = "buttonUpdate"
            Me.buttonUpdate.Size = New System.Drawing.Size(80, 32)
            Me.buttonUpdate.TabIndex = 3
            Me.buttonUpdate.Text = "&Update"
            AddHandler Me.buttonUpdate.Click, AddressOf Me.buttonUpdate_Click
            Me.textRegionDescription.Location = New System.Drawing.Point(8, 336)
            Me.textRegionDescription.Name = "textRegionDescription"
            Me.textRegionDescription.Size = New System.Drawing.Size(344, 20)
            Me.textRegionDescription.TabIndex = 4
            Me.textRegionDescription.Text = ""
            Me.label1.Location = New System.Drawing.Point(8, 320)
            Me.label1.Name = "label1"
            Me.label1.Size = New System.Drawing.Size(208, 16)
            Me.label1.TabIndex = 5
            Me.label1.Text = "Region Description"
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(464, 371)
            Me.Controls.Add(Me.label1)
            Me.Controls.Add(Me.textRegionDescription)
            Me.Controls.Add(Me.buttonUpdate)
            Me.Controls.Add(Me.buttonDelete)
            Me.Controls.Add(Me.buttonCreate)
            Me.Controls.Add(Me.dataGrid)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.Name = "RegionsUI"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "RegionsUI"
            AddHandler Me.Load, AddressOf Me.RegionsUI_Load
            CType((Me.dataGrid), System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
        End Sub

        Private Sub RegionsUI_Load(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Me.ds = Me.regionPersistent.GetDataSet
                Me.dataGrid.DataSource = ds.Tables(0).DefaultView
            Catch ex As Exception
            End Try
        End Sub

        Private Sub buttonCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim rtm As RegionTableMetadata = New RegionTableMetadata
                Dim rnd As Random = New Random
                rtm.RegionID = rnd.Next(300000)
                rtm.RegionDescription = "New Region"
                Me.regionPersistent.Create(rtm)
            Catch ex As Exception
                MessageBox.Show("Failed to create region")
            End Try
        End Sub

        Private Sub buttonDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim index As Integer = Me.dataGrid.CurrentRowIndex
                If index = -1 Then
                    Return
                End If
                DataConvertors.ConvertDataRowToTableMetadata(index, Me.ds.Tables(0), Me.region)
                Me.regionPersistent.Delete(Me.region, False)
            Catch ex As Exception
                MessageBox.Show("Failed to delete item " + ex.Message)
            Finally
            End Try
        End Sub

        Private Sub buttonUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim index As Integer = Me.dataGrid.CurrentRowIndex
                If index = -1 Then
                    Return
                End If
                DataConvertors.ConvertDataRowToTableMetadata(index, Me.ds.Tables(0), Me.region)
                Me.region.RegionDescription = "A new description"
                Me.regionPersistent.Update(Me.region)
            Catch ex As Exception
                MessageBox.Show("Failed to update item " + ex.Message)
            Finally
            End Try
        End Sub

        Private Sub dataGrid_CurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim index As Integer = Me.dataGrid.CurrentRowIndex
                If index > -1 Then
                    DataConvertors.ConvertDataRowToTableMetadata(index, Me.ds.Tables(0), Me.region)
                End If
                Me.textRegionDescription.Text = Me.region.RegionDescription
            Catch ex As Exception
            End Try
        End Sub
    End Class
End Namespace
