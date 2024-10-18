<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        dgvFIFO = New DataGridView()
        txtItemID = New TextBox()
        txtQuantity = New TextBox()
        txtItemName = New TextBox()
        btnAddFIFO = New Button()
        btnRemoveFIFO = New Button()
        dtpDateReceived = New DateTimePicker()
        PRF = New Label()
        Label1 = New Label()
        Label2 = New Label()
        Label3 = New Label()
        txtBarcodeScan = New TextBox()
        Label4 = New Label()
        PictureBox1 = New PictureBox()
        btnPrintPDF = New Button()
        CType(dgvFIFO, ComponentModel.ISupportInitialize).BeginInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dgvFIFO
        ' 
        dgvFIFO.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFIFO.Location = New Point(6, 80)
        dgvFIFO.Name = "dgvFIFO"
        dgvFIFO.RowHeadersWidth = 51
        dgvFIFO.Size = New Size(646, 303)
        dgvFIFO.TabIndex = 0
        ' 
        ' txtItemID
        ' 
        txtItemID.Location = New Point(779, 123)
        txtItemID.Name = "txtItemID"
        txtItemID.Size = New Size(125, 27)
        txtItemID.TabIndex = 1
        ' 
        ' txtQuantity
        ' 
        txtQuantity.Location = New Point(779, 189)
        txtQuantity.Name = "txtQuantity"
        txtQuantity.Size = New Size(125, 27)
        txtQuantity.TabIndex = 3
        ' 
        ' txtItemName
        ' 
        txtItemName.Location = New Point(779, 156)
        txtItemName.Name = "txtItemName"
        txtItemName.Size = New Size(125, 27)
        txtItemName.TabIndex = 4
        ' 
        ' btnAddFIFO
        ' 
        btnAddFIFO.Location = New Point(160, 474)
        btnAddFIFO.Name = "btnAddFIFO"
        btnAddFIFO.Size = New Size(94, 29)
        btnAddFIFO.TabIndex = 5
        btnAddFIFO.Text = "Ajout PRF"
        btnAddFIFO.UseVisualStyleBackColor = True
        ' 
        ' btnRemoveFIFO
        ' 
        btnRemoveFIFO.Location = New Point(835, 474)
        btnRemoveFIFO.Name = "btnRemoveFIFO"
        btnRemoveFIFO.Size = New Size(94, 29)
        btnRemoveFIFO.TabIndex = 6
        btnRemoveFIFO.Text = "Sotrie PRF"
        btnRemoveFIFO.UseVisualStyleBackColor = True
        ' 
        ' dtpDateReceived
        ' 
        dtpDateReceived.Location = New Point(826, 236)
        dtpDateReceived.Name = "dtpDateReceived"
        dtpDateReceived.Size = New Size(234, 27)
        dtpDateReceived.TabIndex = 7
        ' 
        ' PRF
        ' 
        PRF.AutoSize = True
        PRF.Location = New Point(661, 123)
        PRF.Name = "PRF"
        PRF.Size = New Size(33, 20)
        PRF.TabIndex = 8
        PRF.Text = "PRF"
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(661, 156)
        Label1.Name = "Label1"
        Label1.Size = New Size(56, 20)
        Label1.TabIndex = 9
        Label1.Text = "Famille"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(661, 189)
        Label2.Name = "Label2"
        Label2.Size = New Size(66, 20)
        Label2.TabIndex = 10
        Label2.Text = "Quantité"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(658, 241)
        Label3.Name = "Label3"
        Label3.Size = New Size(162, 20)
        Label3.TabIndex = 11
        Label3.Text = "Choisir la date d'entrée"
        ' 
        ' txtBarcodeScan
        ' 
        txtBarcodeScan.Location = New Point(160, 389)
        txtBarcodeScan.Name = "txtBarcodeScan"
        txtBarcodeScan.Size = New Size(602, 27)
        txtBarcodeScan.TabIndex = 12
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI", 28.2F, FontStyle.Bold Or FontStyle.Underline, GraphicsUnit.Point, CByte(0))
        Label4.ForeColor = SystemColors.Highlight
        Label4.ImageAlign = ContentAlignment.TopCenter
        Label4.Location = New Point(255, 6)
        Label4.Name = "Label4"
        Label4.Size = New Size(603, 62)
        Label4.TabIndex = 13
        Label4.Text = "Gestion FIFO magasin EOT"
        ' 
        ' PictureBox1
        ' 
        PictureBox1.BorderStyle = BorderStyle.Fixed3D
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(12, 6)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(237, 65)
        PictureBox1.TabIndex = 14
        PictureBox1.TabStop = False
        ' 
        ' btnPrintPDF
        ' 
        btnPrintPDF.Location = New Point(12, 389)
        btnPrintPDF.Name = "btnPrintPDF"
        btnPrintPDF.Size = New Size(94, 29)
        btnPrintPDF.TabIndex = 15
        btnPrintPDF.Text = "Print PDF"
        btnPrintPDF.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1102, 515)
        Controls.Add(btnPrintPDF)
        Controls.Add(PictureBox1)
        Controls.Add(Label4)
        Controls.Add(txtBarcodeScan)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Label1)
        Controls.Add(PRF)
        Controls.Add(dtpDateReceived)
        Controls.Add(btnRemoveFIFO)
        Controls.Add(btnAddFIFO)
        Controls.Add(txtItemName)
        Controls.Add(txtQuantity)
        Controls.Add(txtItemID)
        Controls.Add(dgvFIFO)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "Form1"
        Text = "Form1"
        CType(dgvFIFO, ComponentModel.ISupportInitialize).EndInit()
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents dgvFIFO As DataGridView
    Friend WithEvents txtItemID As TextBox
    Friend WithEvents txtQuantity As TextBox
    Friend WithEvents txtItemName As TextBox
    Friend WithEvents btnAddFIFO As Button
    Friend WithEvents btnRemoveFIFO As Button
    Friend WithEvents dtpDateReceived As DateTimePicker
    Friend WithEvents PRF As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtBarcodeScan As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents btnPrintPDF As Button

End Class
