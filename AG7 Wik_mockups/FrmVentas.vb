

Public Class FrmVentas
    Private Sub FrmVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color.White
        Me.Font = New Font("Segoe UI", 10)
        Me.FormBorderStyle = FormBorderStyle.FixedSingle

        txtCodigo.BorderStyle = BorderStyle.FixedSingle
        txtCodigo.BackColor = Color.White
        txtCodigo.Font = New Font("Segoe UI", 10)

        lblCodigo.Font = New Font("Segoe UI", 10)

        txtNombre.BorderStyle = BorderStyle.FixedSingle
        txtNombre.BackColor = Color.White
        txtNombre.Font = New Font("Segoe UI", 10)

        lblNombre.Font = New Font("Segoe UI", 10)

        nudCantidad.BorderStyle = BorderStyle.FixedSingle
        nudCantidad.BackColor = Color.White
        nudCantidad.Font = New Font("Segoe UI", 10)

        lblCantidad.Font = New Font("Segoe UI", 10)

        txtPrecio.BorderStyle = BorderStyle.FixedSingle
        txtPrecio.BackColor = Color.White
        txtPrecio.Font = New Font("Segoe UI", 10)

        lblPrecio.Font = New Font("Segoe UI", 10)

        txtTotal.BorderStyle = BorderStyle.FixedSingle
        txtTotal.BackColor = Color.White
        txtTotal.Font = New Font("Segoe UI", 10)

        lblTotal.Font = New Font("Segoe UI", 10)

        txtCajero.BorderStyle = BorderStyle.FixedSingle
        txtCajero.BackColor = Color.White
        txtCajero.Font = New Font("Segoe UI", 10)

        lblCajero.Font = New Font("Segoe UI", 10)

        btnAgregar.BackColor = Color.FromArgb(52, 58, 64)
        btnAgregar.ForeColor = Color.White
        btnAgregar.FlatStyle = FlatStyle.Flat
        btnAgregar.FlatAppearance.BorderSize = 0
        btnAgregar.Font = New Font("Segoe UI", 10)

        btnEliminar.BackColor = Color.FromArgb(52, 58, 64)
        btnEliminar.ForeColor = Color.White
        btnEliminar.FlatStyle = FlatStyle.Flat
        btnEliminar.FlatAppearance.BorderSize = 0
        btnEliminar.Font = New Font("Segoe UI", 10)

        btnCobrar.BackColor = Color.FromArgb(52, 58, 64)
        btnCobrar.ForeColor = Color.White
        btnCobrar.FlatStyle = FlatStyle.Flat
        btnCobrar.FlatAppearance.BorderSize = 0
        btnCobrar.Font = New Font("Segoe UI", 10)

        btnCancelar.BackColor = Color.FromArgb(52, 58, 64)
        btnCancelar.ForeColor = Color.White
        btnCancelar.FlatStyle = FlatStyle.Flat
        btnCancelar.FlatAppearance.BorderSize = 0
        btnCancelar.Font = New Font("Segoe UI", 10)

        With dgvProductos
            .BackgroundColor = Color.White
            .BorderStyle = BorderStyle.None
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
            .GridColor = Color.LightGray
            .EnableHeadersVisualStyles = False
            .RowHeadersVisible = False
            .Font = New Font("Segoe UI", 10)

            ' Estilo para el encabezado
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 58, 64)
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.White
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)

            ' Ajuste de selección (opcional)
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 224, 224)
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With

        With dgvProductos
            .Columns.Add("Codigo", "Código")
            .Columns.Add("Nombre", "Nombre")
            .Columns.Add("Cantidad", "Cantidad")
            .Columns.Add("Precio", "Precio")
            .Columns.Add("Subtotal", "Subtotal")
        End With

    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click


        If txtCodigo.Text = "" Then
            MsgBox("Por favor, ingresa el código del producto.", MsgBoxStyle.Exclamation, "Código vacío")
            Exit Sub
        End If
        ' Simulación: si el nombre está vacío, asumimos que el producto no se encontró
        If txtNombre.Text = "" Then
            MsgBox("El producto no fue encontrado en la base de datos.", MsgBoxStyle.Critical, "Producto no encontrado")
            Exit Sub
        End If
        If nudCantidad.Value <= 0 Then
            MsgBox("La cantidad debe ser mayor a cero.", MsgBoxStyle.Information, "Cantidad inválida")
            Exit Sub
        End If




        ' Obtener datos y calcular subtotal
        Dim cantidad As Integer = CInt(nudCantidad.Value)
        Dim precio As Decimal = CDec(txtPrecio.Text)
        Dim subtotal As Decimal = cantidad * precio

        ' Agregar fila al DataGridView
        dgvProductos.Rows.Add(txtCodigo.Text, txtNombre.Text, cantidad, precio.ToString("0.00"), subtotal.ToString("0.00"))

        ' Limpiar campos
        txtCodigo.Clear()
        txtNombre.Clear()
        nudCantidad.Value = 0
        txtPrecio.Clear()

        ' Actualizar total
        CalcularTotal()
    End Sub

    Private Sub CalcularTotal()
        Dim total As Decimal = 0

        For Each fila As DataGridViewRow In dgvProductos.Rows
            If Not fila.IsNewRow Then
                total += CDec(fila.Cells("Subtotal").Value)
            End If
        Next

        txtTotal.Text = total.ToString("0.00")
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If dgvProductos.CurrentRow IsNot Nothing AndAlso Not dgvProductos.CurrentRow.IsNewRow Then
            dgvProductos.Rows.Remove(dgvProductos.CurrentRow)
            CalcularTotal()
        Else
            MessageBox.Show("Selecciona una fila para eliminar.")
        End If
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        dgvProductos.Rows.Clear()
        txtTotal.Clear()
    End Sub

    Private Sub btnCobrar_Click(sender As Object, e As EventArgs) Handles btnCobrar.Click
        ' Verifica que haya productos reales en el carrito
        If dgvProductos.Rows.Cast(Of DataGridViewRow)().All(Function(r) r.IsNewRow) Then
            MsgBox("No hay productos en el carrito para cobrar.", MsgBoxStyle.Exclamation, "Carrito vacío")
            Exit Sub
        End If

        ' Si hay productos, continúa el proceso
        MsgBox("Venta registrada con éxito. Total: $" & txtTotal.Text, MsgBoxStyle.Information, "Venta exitosa")

        ' (Opcional) Limpiar carrito
        dgvProductos.Rows.Clear()
        txtTotal.Clear()
    End Sub
End Class


