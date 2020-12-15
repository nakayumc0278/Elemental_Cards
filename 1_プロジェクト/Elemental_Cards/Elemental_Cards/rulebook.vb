Imports System.ComponentModel

Public Class rulebook
    Dim game_img(4) As Bitmap
    Dim page_count As Integer = 0
    Dim page_sentaku As Integer
    Private Sub rulebook_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ClientSize = New Size(1440, 960)    'フォームのサイズ指定
        game_img(0) = My.Resources.game1
        game_img(1) = My.Resources.game2
        game_img(2) = My.Resources.game3
        game_img(3) = My.Resources.game4
    End Sub

    '×ボタンが押されたら
    Private Sub rulebook_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        title.Show()
    End Sub

    Private Sub rulebook_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'タイトル画面に戻る処理
        If e.KeyData = Keys.Escape Then     'もし[Escキー] が押下されたら
            Dim frm_title As New title
            frm_title.Owner = Me
            title.Show()                    'タイトルに戻る
            Close()                         'ルールブックを閉じる
        End If

        '右矢印キーが押された時の処理
        If e.KeyData = Keys.Right Then       '下矢印キーが押されたら
            page_count = page_count + 1
            page_sentaku = Math.Abs(page_count)
            page_sentaku = page_count Mod 4

            Invalidate()

            '左矢印が押された時の処理
        ElseIf e.KeyData = Keys.Left Then     '上矢印が押されたら

            If page_count <= 0 Then
                page_count = 3
            Else
                page_count = page_count - 1
            End If

            page_sentaku = Math.Abs(page_count)
            page_sentaku = page_count Mod 4

            Invalidate()
        End If
    End Sub

    Private Sub rulebook_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim gr As Graphics = e.Graphics

        Select Case page_sentaku

            Case 0
                gr.DrawImage(game_img(0), 0, 0)            'game_img(0)を座標(0,0)の位置に描画

            Case 1
                gr.DrawImage(game_img(1), 0, 0)            'game_img(1)を座標(0,0)の位置に描画

            Case 2
                gr.DrawImage(game_img(2), 0, 0)            'game_img(2)を座標(0,0)の位置に描画

            Case 3
                gr.DrawImage(game_img(3), 0, 0)            'game_img(3)を座標(0,0)の位置に描画

        End Select
    End Sub
End Class