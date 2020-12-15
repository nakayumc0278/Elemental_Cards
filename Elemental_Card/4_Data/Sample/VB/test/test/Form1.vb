Public Class Form1
    Const CELL_SIZE As Integer = 100
    Dim board(2, 2) As Integer
    Dim img(2) As Bitmap
    Dim man As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim x, y As Integer
        img(0) = My.Resources.PH02750U
        img(1) = My.Resources.PH02752U
        img(2) = My.Resources.PH02748U

        Me.ClientSize = New Size(CELL_SIZE * 3, CELL_SIZE * 3)

        'ボード初期化
        For y = 0 To 2
            For x = 0 To 2
                board(x, y) = 0
            Next
        Next
        '先手設定
        man = 1
    End Sub

    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Dim x, y As Integer
        x = e.X \ CELL_SIZE
        y = e.Y \ CELL_SIZE
        If board(x, y) <> 0 Then
            MessageBox.Show("ここに置くことはできません", "三つ並べ", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If

        'コマを置く
        board(x, y) = man
        Me.Invalidate() '再描画(Paintイベント発生)

        If man = 1 Then
            man = 2
        Else
            man = 1
        End If

    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim gr As Graphics
        Dim x, y, n As Integer
        Dim pt As Point
        gr = e.Graphics

        'コマ描画
        pt.Y = 0
        For y = 0 To 2
            pt.X = 0
            For x = 0 To 2
                n = board(x, y)
                gr.DrawImage(img(n), pt)
                pt.X += CELL_SIZE
            Next
            pt.Y += CELL_SIZE
        Next
    End Sub
End Class
