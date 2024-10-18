Imports System.Data.OleDb
Imports System.Text.RegularExpressions
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Windows.Forms


Public Class Form1
    Private FIFO As New List(Of FIFOItem)
    Private connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=d:\s.gharbi\Desktop\CMFB.accdb;"

    Private Class FIFOItem
        Public Property ItemID As String
        Public Property ItemName As String
        Public Property Quantity As Integer
        Public Property DateReceived As Date
        Public Property RackAddress As String

    End Class

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvFIFO.ColumnCount = 5
        dgvFIFO.Columns(0).Name = "Item ID"
        dgvFIFO.Columns(1).Name = "Item Name"
        dgvFIFO.Columns(2).Name = "Quantity"
        dgvFIFO.Columns(3).Name = "Date Received"
        dgvFIFO.Columns(4).Name = "RackAdress"
        dgvFIFO.AllowUserToAddRows = False
    End Sub

    Private Sub btnAddFIFO_Click(sender As Object, e As EventArgs) Handles btnAddFIFO.Click
        If ValidateInputFields() Then
            AddFIFOItem(txtItemID.Text, txtItemName.Text, Integer.Parse(txtQuantity.Text), dtpDateReceived.Value)
        End If
    End Sub

    Private Sub btnRemoveFIFO_Click(sender As Object, e As EventArgs) Handles btnRemoveFIFO.Click
        If ValidateRemoveFields() Then
            Try
                RemoveFIFO(txtItemID.Text, Integer.Parse(txtQuantity.Text))
                UpdateDataGridView()
                ClearInputFields()
                MessageBox.Show("Item removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error removing item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Function ValidateInputFields() As Boolean
        If String.IsNullOrWhiteSpace(txtItemID.Text) OrElse String.IsNullOrWhiteSpace(txtItemName.Text) OrElse String.IsNullOrWhiteSpace(txtQuantity.Text) Then
            MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If Not Integer.TryParse(txtQuantity.Text, Nothing) OrElse Integer.Parse(txtQuantity.Text) <= 0 Then
            MessageBox.Show("Quantity must be a positive integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    Private Function ValidateRemoveFields() As Boolean
        If String.IsNullOrWhiteSpace(txtItemID.Text) OrElse String.IsNullOrWhiteSpace(txtQuantity.Text) Then
            MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        If Not Integer.TryParse(txtQuantity.Text, Nothing) OrElse Integer.Parse(txtQuantity.Text) <= 0 Then
            MessageBox.Show("Quantity must be a positive integer.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If
        Return True
    End Function

    Private Sub RemoveFIFO(itemID As String, quantityToRemove As Integer)
        For Each item In FIFO.Where(Function(i) i.ItemID = itemID).OrderBy(Function(i) i.DateReceived).ToList()
            If quantityToRemove <= 0 Then Exit For

            If item.Quantity > quantityToRemove Then
                item.Quantity -= quantityToRemove
                quantityToRemove = 0
            Else
                quantityToRemove -= item.Quantity
                FIFO.Remove(item)
                MarkRackAddressAsAvailable(itemID)
            End If
        Next
    End Sub

    Private Sub MarkRackAddressAsAvailable(itemID As String)
        Using connection As New OleDbConnection(connectionString)
            Dim query As String = "UPDATE RackAddresses SET IsFull = False WHERE RackAddress = (SELECT RackAddress FROM x1 WHERE ItemID = @ItemID)"
            Using command As New OleDbCommand(query, connection)
                command.Parameters.AddWithValue("@ItemID", itemID)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub


    Private Sub UpdateDataGridView()
        dgvFIFO.Rows.Clear()
        For Each item In FIFO
            dgvFIFO.Rows.Add(item.ItemID, item.ItemName, item.Quantity, item.DateReceived, item.RackAddress)
        Next
    End Sub


    Private Sub ClearInputFields()
        txtItemID.Clear()
        txtItemName.Clear()
        txtQuantity.Clear()
        txtBarcodeScan.Clear()
        dtpDateReceived.Value = DateTime.Now
    End Sub

    Private Sub txtBarcodeScan_TextChanged(sender As Object, e As EventArgs) Handles txtBarcodeScan.TextChanged
        Dim barcodeText As String = txtBarcodeScan.Text
        Dim patternItemID As String = "PRF\d{8}"
        Dim matchItemID As Match = Regex.Match(barcodeText, patternItemID)
        Dim patternQuantity As String = "Q(\d{2})"
        Dim matchQuantity As Match = Regex.Match(barcodeText, patternQuantity)

        If matchItemID.Success AndAlso matchQuantity.Success Then
            Dim itemID As String = matchItemID.Value
            Dim quantity As Integer = Integer.Parse(matchQuantity.Groups(1).Value)
            Dim itemName As String = GetItemNameFromDatabase(itemID)

            If Not String.IsNullOrEmpty(itemName) Then
                AddFIFOItem(itemID, itemName, quantity, DateTime.Now)
                ClearInputFields()
            End If
        End If
    End Sub

    Private Function GetItemNameFromDatabase(itemID As String) As String
        Dim itemName As String = String.Empty
        Using connection As New OleDbConnection(connectionString)
            Dim query As String = "SELECT ItemName FROM x1 WHERE ItemID = @ItemID"
            Using command As New OleDbCommand(query, connection)
                command.Parameters.AddWithValue("@ItemID", itemID)
                connection.Open()
                Dim result As Object = command.ExecuteScalar()
                If result IsNot Nothing Then
                    itemName = result.ToString()
                End If
            End Using
        End Using
        Return itemName
    End Function

    Private Sub AddFIFOItem(itemID As String, itemName As String, quantity As Integer, dateReceived As Date)
        If String.IsNullOrWhiteSpace(itemID) OrElse String.IsNullOrWhiteSpace(itemName) OrElse quantity <= 0 Then
            MessageBox.Show("Invalid item details.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim rackAddress As String = GetRandomAvailableRackAddress()

        If String.IsNullOrEmpty(rackAddress) Then
            MessageBox.Show("No available rack address found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim existingItem = FIFO.FirstOrDefault(Function(i) i.ItemID = itemID)
        If existingItem IsNot Nothing Then
            existingItem.Quantity += quantity
        Else
            Try
                Dim item As New FIFOItem With {
                .ItemID = itemID,
                .ItemName = itemName,
                .Quantity = quantity,
                .DateReceived = dateReceived,
                .RackAddress = rackAddress ' Assign the Rack Address
            }

                FIFO.Add(item)

                ' Mark the rack address as full in the database
                MarkRackAddressAsFull(rackAddress)
            Catch ex As Exception
                MessageBox.Show("Error adding item: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try
        End If
        UpdateDataGridView()
    End Sub

    Private Sub MarkRackAddressAsFull(rackAddress As String)
        Using connection As New OleDbConnection(connectionString)
            Dim query As String = "UPDATE RackAddresses SET IsFull = True WHERE RackAddress = @RackAddress"
            Using command As New OleDbCommand(query, connection)
                command.Parameters.AddWithValue("@RackAddress", rackAddress)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub




    Private Sub btnPrintPDF_Click(sender As Object, e As EventArgs) Handles btnPrintPDF.Click
        PrintPDF()
    End Sub
    Private Sub PrintPDF()
        ' Create a SaveFileDialog to prompt the user for a save location
        Dim saveFileDialog As New SaveFileDialog()
        saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"
        saveFileDialog.Title = "Save PDF File"
        saveFileDialog.FileName = "output.pdf"

        If saveFileDialog.ShowDialog() = DialogResult.OK Then
            ' Define the PDF document
            Dim doc As New Document()
            Try
                ' Create a file stream to save the PDF file
                Dim output As New FileStream(saveFileDialog.FileName, FileMode.Create)
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, output)

                ' Open the document for writing
                doc.Open()

                ' Add a title to the document
                Dim baseFont As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
                Dim titleFont As New Font(baseFont, 18, Font.Bold)
                Dim title As New Paragraph("Inventaire magasin", titleFont)
                title.Alignment = Element.ALIGN_CENTER
                doc.Add(title)

                ' Add a blank line
                doc.Add(New Paragraph(Environment.NewLine))

                ' Create a table with the same number of columns as the DataGridView
                Dim table As New PdfPTable(dgvFIFO.ColumnCount)

                ' Add the headers from the DataGridView to the table
                For Each column As DataGridViewColumn In dgvFIFO.Columns
                    Dim cell As New PdfPCell(New Phrase(column.HeaderText))
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY
                    table.AddCell(cell)
                Next

                ' Add the data from the DataGridView to the table
                For Each row As DataGridViewRow In dgvFIFO.Rows
                    For Each cell As DataGridViewCell In row.Cells
                        table.AddCell(cell.Value?.ToString())
                    Next
                Next

                ' Add the table to the document
                doc.Add(table)

                ' Close the document
                doc.Close()
                MessageBox.Show("PDF created successfully! Saved at: " & saveFileDialog.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error creating PDF: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                If doc.IsOpen Then doc.Close()
            End Try
        End If
    End Sub
    Private Function GetRandomAvailableRackAddress() As String
        Dim rackAddress As String = String.Empty
        Try
            Using connection As New OleDbConnection(connectionString)
                Dim query As String = "SELECT RackAddress FROM RackAddresses WHERE IsFull = False"
                Using command As New OleDbCommand(query, connection)
                    connection.Open()
                    Dim reader As OleDbDataReader = command.ExecuteReader()
                    Dim availableAddresses As New List(Of String)
                    While reader.Read()
                        availableAddresses.Add(reader("RackAddress").ToString())
                    End While

                    If availableAddresses.Count > 0 Then
                        Dim random As New Random()
                        rackAddress = availableAddresses(random.Next(availableAddresses.Count))
                    Else
                        MessageBox.Show("No available rack addresses found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End Using
            End Using
        Catch ex As OleDbException
            MessageBox.Show("Database error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return rackAddress
    End Function




End Class
