Public Class title
    Dim count As Integer = 0

    '開始矢印の初期化
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'ゲームスタートに矢印が出るように設定
        Label1.Visible = True
        Label3.Visible = False
        Label5.Visible = False

    End Sub

    'キーボードから指定したキーが押下された時の処理
    Private Sub click1(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Dim sentaku As Integer

        '下矢印キーが押された時の処理
        If e.KeyData = Keys.Down Then       '下矢印キーが押されたら
            count = count + 1
            sentaku = Math.Abs(count)
            sentaku = count Mod 3

            Select Case sentaku

                Case 0
                    'GAMESTARTの矢印を表示
                    Label1.Visible = True
                    Label3.Visible = False
                    Label5.Visible = False

                Case 1
                    'ルールブックの矢印を表示
                    Label1.Visible = False
                    Label3.Visible = True
                    Label5.Visible = False

                Case 2
                    '終了の矢印を表示
                    Label1.Visible = False
                    Label3.Visible = False
                    Label5.Visible = True

            End Select

            '上矢印が押された時の処理
        ElseIf e.KeyData = Keys.Up Then     '上矢印が押されたら

            If count <= 0 Then
                count = 2
            Else
                count = count - 1
            End If

            sentaku = Math.Abs(count)
            sentaku = count Mod 3

            Select Case sentaku

                Case 0
                    'GAMESTARTの矢印を表示
                    Label1.Visible = True
                    Label3.Visible = False
                    Label5.Visible = False
                Case 1
                    'ルールブックの矢印を表示
                    Label1.Visible = False
                    Label3.Visible = True
                    Label5.Visible = False

                Case 2
                    '終了の矢印を表示
                    Label1.Visible = False
                    Label3.Visible = False
                    Label5.Visible = True

            End Select

            '[Spaceキー]が押された時の処理
        ElseIf e.KeyData = Keys.Space Then  '[Spaceキー]が押されたら
            sentaku = Math.Abs(count)
            sentaku = count Mod 3

            Select Case sentaku

                Case 0
                    'ゲーム画面に飛ぶ処理
                    Dim frm_game As New game
                    frm_game.Owner = Me
                    game.Show()              'ゲーム画面を表示
                    Hide()                  'タイトル画面を非表示

                Case 1
                    'ルールブックの表示処理
                    Dim frm_rulebook As New rulebook
                    frm_rulebook.Owner = Me
                    rulebook.Show()
                    Hide()

                Case 2
                    'ゲームの終了処理
                    Close()

            End Select
        End If
    End Sub
End Class
