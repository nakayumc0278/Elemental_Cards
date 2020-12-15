Imports System.ComponentModel

Public Class game

    Dim r As New Random
    Dim Card_Number As Integer
    Dim i, j As Integer
    Dim img(20) As Bitmap
    Dim back_img As Bitmap
    Dim key_img As Bitmap
    Dim enemy_cards(5) As Integer
    Dim player_cards(5) As Integer
    Dim enemy_points_int As Integer = 0
    Dim player_points_int As Integer = 0
    Dim syokika_flg As Boolean = True
    Dim start_flg As Boolean = True
    Dim game_flg As Boolean = False
    Dim enemy_flg As Boolean = True
    Dim player_flg As Boolean
    Dim lock_flg As Boolean = False
    Dim mouse_flg As Boolean
    Dim kagoubutu_name_enemy As String
    Dim kagoubutu_name_player As String
    Dim kekka As String = ""
    Dim elements_changebox As String
    Dim enemy_points_str As String
    Dim player_points_str As String
    Dim enemy_elements(20) As String
    Dim player_elements(20) As String
    Dim Lock_enemy() As Boolean = {False, False, False, False, False}
    Dim Lock_player() As Boolean = {False, False, False, False, False}

    Dim H_count_enemy As Integer = 0, He_count_enemy As Integer = 0, Li_count_enemy As Integer = 0, Be_count_enemy As Integer = 0, B_count_enemy As Integer = 0
    Dim C_count_enemy As Integer = 0, N_count_enemy As Integer = 0, O_count_enemy As Integer = 0, F_count_enemy As Integer = 0, Ne_count_enemy As Integer = 0
    Dim Na_count_enemy As Integer = 0, Mg_count_enemy As Integer = 0, Al_count_enemy As Integer = 0, Si_count_enemy As Integer = 0, P_count_enemy As Integer = 0
    Dim S_count_enemy As Integer = 0, Cl_count_enemy As Integer = 0, Ar_count_enemy As Integer = 0, K_count_enemy As Integer = 0, Ca_count_enemy As Integer = 0

    Dim H_count_player As Integer = 0, He_count_player As Integer = 0, Li_count_player As Integer = 0, Be_count_player As Integer = 0, B_count_player As Integer = 0
    Dim C_count_player As Integer = 0, N_count_player As Integer = 0, O_count_player As Integer = 0, F_count_player As Integer = 0, Ne_count_player As Integer = 0
    Dim Na_count_player As Integer = 0, Mg_count_player As Integer = 0, Al_count_player As Integer = 0, Si_count_player As Integer = 0, P_count_player As Integer = 0
    Dim S_count_player As Integer = 0, Cl_count_player As Integer = 0, Ar_count_player As Integer = 0, K_count_player As Integer = 0, Ca_count_player As Integer = 0

    'キー押下されたときの処理
    Private Sub click1(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

        'タイトル画面に戻る処理
        If e.KeyData = Keys.Escape Then     'もし[Escキー] が押下されたら
            Dim frm_title As New title
            frm_title.Owner = Me
            title.Show()                    'タイトルに戻る
            Close()                         'ゲーム画面を閉じる

            'ゲームを開始する処理
        ElseIf e.KeyData = Keys.Space Then  'もし[Spaceキー] が押下されたら
            If start_flg = True Then
                start_flg = False
                game_flg = True
                Game_Start()
            End If
        ElseIf e.KeyData = Keys.L And game_flg = True Then　'もし[Lキー] が押下されたら
            lock_flg = True
            Invalidate()    'フォームコントロール無効化して再描画

        ElseIf e.KeyData = Keys.R And game_flg = False And lock_flg = True Then  'もし[Rキー] が押下されたら

            enemy_points_int = 0
            player_points_int = 0
            game_flg = True
            enemy_flg = True
            lock_flg = False
            kekka = ""
            For i = 0 To 4
                Lock_enemy(i) = False
                Lock_player(i) = False
            Next

            Game_Start()
        End If
    End Sub

    '画像読込処理
    Private Sub game_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '画像(鍵)をkeyに代入
        key_img = My.Resources.KEY

        '画像(うら)をbacking()に代入
        back_img = My.Resources.裏縦

        '画像(原子記号)をimg()に代入
        img(0) = My.Resources.H_水素
        img(1) = My.Resources.He_ヘリウム
        img(2) = My.Resources.Li_リチウム
        img(3) = My.Resources.Be_ベリリウム
        img(4) = My.Resources.B_ホウ素
        img(5) = My.Resources.C_炭素
        img(6) = My.Resources.N_窒素
        img(7) = My.Resources.O_酸素
        img(8) = My.Resources.F_フッ素
        img(9) = My.Resources.Ne_ネオン
        img(10) = My.Resources.Na_ナトリウム
        img(11) = My.Resources.Mg_マグネシウム
        img(12) = My.Resources.Al_アルミニウム
        img(13) = My.Resources.Si_ケイ素
        img(14) = My.Resources.P_リン
        img(15) = My.Resources.S_硫黄
        img(16) = My.Resources.Cl_塩素
        img(17) = My.Resources.Ar_アルゴン
        img(18) = My.Resources.K_カリウム
        img(19) = My.Resources.Ca_カルシウム

        ClientSize = New Size(1440, 960)    'フォームのサイズ指定

        Invalidate()                        'フォームコントロール無効化して再描画
    End Sub

    '描画処理
    Private Sub game_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim gr As Graphics = e.Graphics
        Dim pt, pt_elements As Point
        Dim fnt_score As New Font("MS UI Gothic", 25)
        Dim fnt_kagoubutuname As New Font("MS UI Gothic", 35)
        Dim fnt_kekka As New Font("MS UI Gothic", 60)
        Dim fnt_change As New Font("MS UI Gothic", 50)
        Dim fnt_elements As New Font("MS UI Gothic", 20)

        If syokika_flg = True Then           'shokikaflgがTrueのとき
            syokika_flg = False              'Falseにする

            elements_show() 'elements_show関数の呼び出し

            pt_elements.Y = 30

            gr.DrawString("Spaceキーを押してゲームを開始", fnt_change, Brushes.Cyan, 275.0, 425.0) 'x=275,y=425
            gr.DrawString(enemy_points_str, fnt_score, Brushes.White, 0.0, 0.0) 'x=0,Y=0
            gr.DrawString(player_points_str, fnt_score, Brushes.White, 1150.0, 0.0) 'x=1150,y=0

            For j = 0 To 19
                gr.DrawString(enemy_elements(j), fnt_elements, Brushes.White, 0.0, pt_elements.Y + 40) 'x=0,y= pt_elements.Y + 40
                gr.DrawString(player_elements(j), fnt_elements, Brushes.White, 1150.0, pt_elements.Y + 40) 'x=1150,y= pt_elements.Y + 40
                pt_elements.Y += 30
            Next

            pt.X = 0                    'pt.X座標を初期化

            For i = 0 To 4
                gr.DrawImage(back_img, 312 + pt.X, 12)            'backimg(1)を座標(312 + pt.X, 12)の位置に描画
                pt.X += 160
            Next

            pt.X = 0                    'pt.X座標を初期化

            For i = 0 To 4
                gr.DrawImage(back_img, 312 + pt.X, 653)           'backimg(1)を座標(312 + pt.X, 653)の位置に描画
                pt.X += 160
            Next

        Else
            pt.X = 0                    'pt.X座標を初期化

            For i = 0 To 4
                gr.DrawImage(img(enemy_cards(i)), 312 + pt.X, 12)   'backimg(1)を座標(312 + pt.X, 12)の位置に描画
                pt.X += 160
            Next

            pt.X = 0                    'pt.X座標を初期化

            For i = 0 To 4
                gr.DrawImage(img(player_cards(i)), 312 + pt.X, 653) 'backimg(1)を座標(312 + pt.X, 653)の位置に描画
                pt.X += 160
            Next
        End If

        If game_flg = True Then                 'game_flgが Trueのとき

            elements_show() 'elements_show関数の呼び出し

            pt_elements.Y = 30 'pt_elementsのY座標を設定

            gr.DrawString(enemy_points_str, fnt_score, Brushes.White, 0.0, 0.0) 'x=0,Y=0
            gr.DrawString(player_points_str, fnt_score, Brushes.White, 1150.0, 0.0) 'x=1150,y=0

            For j = 0 To 19
                gr.DrawString(enemy_elements(j), fnt_elements, Brushes.White, 0.0, pt_elements.Y + 40) 'x=0,y= pt_elements.Y + 40
                gr.DrawString(player_elements(j), fnt_elements, Brushes.White, 1150.0, pt_elements.Y + 40) 'x=1150,y= pt_elements.Y + 40
                pt_elements.Y += 30
            Next

            gr.DrawString("どのカードを残すか選択してください", fnt_change, Brushes.Cyan, 220.0, 425.0) 'x=220,y=425

            If lock_flg = True Then

                cards_changer()

                pt.X = 0                    'pt.X座標を初期化

                For i = 0 To 4
                    gr.DrawImage(img(enemy_cards(i)), 312 + pt.X, 12)   'backimg(1)を座標(312 + pt.X, 12)の位置に描画
                    pt.X += 160
                Next

                pt.X = 0                    'pt.X座標を初期化

                For i = 0 To 4
                    gr.DrawImage(img(player_cards(i)), 312 + pt.X, 653) 'backimg(1)を座標(312 + pt.X, 653)の位置に描画
                    pt.X += 160
                Next

                For i = 0 To 1
                    If enemy_flg = True Then
                        enemy_points_int = hantei() 'hantei関数の呼び出し
                    ElseIf player_flg = True Then
                        player_points_int = hantei() 'hantei関数の呼び出し
                    End If
                Next

                If game_flg = True Then
                    game_flg = False
                    kekka = win_lose(enemy_points_int, player_points_int) 'win_lose関数の呼び出し

                End If
            End If
        End If

        If lock_flg = False Then
            For i = 0 To 4
                If Lock_player(i) = True Then
                    Select Case i
                        Case 0
                            gr.DrawImage(key_img, 352, 717)
                        Case 1
                            gr.DrawImage(key_img, 512, 717)
                        Case 2
                            gr.DrawImage(key_img, 672, 717)
                        Case 3
                            gr.DrawImage(key_img, 832, 717)
                        Case 4
                            gr.DrawImage(key_img, 992, 717)
                    End Select
                End If
            Next
        Else

            game_flg = False

            elements_show() 'elements_show関数の呼び出し

            pt_elements.Y = 30

            gr.DrawString(enemy_points_str, fnt_score, Brushes.White, 0.0, 0.0) 'x=0,Y=0
            gr.DrawString(player_points_str, fnt_score, Brushes.White, 1150.0, 0.0) 'x=1150,y=0
            gr.DrawString(kagoubutu_name_enemy, fnt_kagoubutuname, Brushes.White, 570.0, 300.0) 'x=570,y=300
            gr.DrawString(kagoubutu_name_player, fnt_kagoubutuname, Brushes.White, 570.0, 570.0) 'x=570,y=570
            gr.DrawString(kekka, fnt_kekka, Brushes.Cyan, 570.0, 415.0) 'x=570,y=415

            For j = 0 To 19
                gr.DrawString(enemy_elements(j), fnt_elements, Brushes.White, 0.0, pt_elements.Y + 40) 'x=0,y= pt_elements.Y + 40
                gr.DrawString(player_elements(j), fnt_elements, Brushes.White, 1150.0, pt_elements.Y + 40) 'x=1150,y= pt_elements.Y + 40
                pt_elements.Y += 30
            Next
        End If
    End Sub

    'マウスの左ボタンが押された時の処理
    Private Sub game_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        Dim x, y As Integer

        x = e.X     'xにe.Xのパラメータ取得
        y = e.Y     'yにe.Yのパラメータ取得

        'カードをロックする
        If start_flg <> True Then
            If x > 312 And x < 472 And y > 653 And y < 909 Then
                If Lock_player(0) = False Then
                    Lock_player(0) = True
                Else
                    Lock_player(0) = False
                End If

            ElseIf x > 472 And x < 632 And y > 653 And y < 909 Then
                If Lock_player(1) = False Then
                    Lock_player(1) = True
                Else
                    Lock_player(1) = False
                End If

            ElseIf x > 632 And x < 792 And y > 653 And y < 909 Then
                If Lock_player(2) = False Then
                    Lock_player(2) = True
                Else
                    Lock_player(2) = False
                End If

            ElseIf x > 792 And x < 952 And y > 653 And y < 909 Then
                If Lock_player(3) = False Then
                    Lock_player(3) = True
                Else
                    Lock_player(3) = False
                End If

            ElseIf x > 952 And x < 1112 And y > 653 And y < 909 Then
                If Lock_player(4) = False Then
                    Lock_player(4) = True
                Else
                    Lock_player(4) = False
                End If

            End If

            Invalidate()
        End If

    End Sub

    'ゲーム開始
    Private Sub Game_Start()

        '乱数を使用してカードを生成
        For i = 0 To 1
            For j = 0 To 4
                Card_Number = r.Next(0, 79)
                If Card_Number >= 76 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 19
                        Ca_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 19
                        Ca_count_player += 1
                    End If

                ElseIf Card_Number >= 72 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 18
                        K_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 18
                        K_count_player += 1
                    End If

                ElseIf Card_Number >= 68 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 17
                        Ar_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 17
                        Ar_count_player += 1
                    End If

                ElseIf Card_Number >= 64 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 16
                        Cl_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 16
                        Cl_count_player += 1
                    End If

                ElseIf Card_Number >= 60 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 15
                        S_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 15
                        S_count_player += 1
                    End If

                ElseIf Card_Number >= 56 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 14
                        P_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 14
                        P_count_player += 1
                    End If

                ElseIf Card_Number >= 52 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 13
                        Si_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 13
                        Si_count_player += 1
                    End If

                ElseIf Card_Number >= 48 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 12
                        Al_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 12
                        Al_count_player += 1
                    End If

                ElseIf Card_Number >= 44 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 11
                        Mg_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 11
                        Mg_count_player += 1
                    End If

                ElseIf Card_Number >= 40 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 10
                        Na_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 10
                        Na_count_player += 1
                    End If

                ElseIf Card_Number >= 36 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 9
                        Ne_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 9
                        Ne_count_player += 1
                    End If

                ElseIf Card_Number >= 32 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 8
                        F_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 8
                        F_count_player += 1
                    End If

                ElseIf Card_Number >= 28 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 7
                        O_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 7
                        O_count_player += 1
                    End If

                ElseIf Card_Number >= 24 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 6
                        N_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 6
                        N_count_player += 1
                    End If

                ElseIf Card_Number >= 20 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 5
                        C_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 5
                        C_count_player += 1
                    End If

                ElseIf Card_Number >= 16 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 4
                        B_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 4
                        B_count_player += 1
                    End If

                ElseIf Card_Number >= 12 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 3
                        Be_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 3
                        Be_count_player += 1
                    End If

                ElseIf Card_Number >= 8 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 2
                        Li_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 2
                        Li_count_player += 1
                    End If

                ElseIf Card_Number >= 4 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 1
                        He_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 1
                        He_count_player += 1
                    End If

                ElseIf Card_Number >= 0 Then
                    If enemy_flg = True Then
                        enemy_cards(j) = 0
                        H_count_enemy += 1
                    ElseIf player_flg = True Then
                        player_cards(j) = 0
                        H_count_player += 1
                    End If

                End If
            Next
            enemy_flg = False
            player_flg = True
        Next
        enemy_flg = True
        player_flg = False

        Invalidate()
    End Sub

    '×ボタンが押されたら
    Private Sub game_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        title.Show()
    End Sub

    Private Function hantei()
        Dim kagoubutu_name As String = "無し"
        hantei = 0
        '
        If enemy_flg = True Then
            enemy_flg = False
            player_flg = True


            If H_count_enemy >= 8 Then

                If C_count_enemy >= 1 And N_count_enemy >= 2 And O_count_enemy >= 3 And hantei < 14 Then '(NH4)2CO3
                    hantei = 14
                    H_count_enemy -= 8
                    C_count_enemy -= 1
                    N_count_enemy -= 2
                    O_count_enemy -= 3
                    kagoubutu_name = "炭酸アンモニウム (NH4)2CO3"

                ElseIf C_count_enemy >= 3 And hantei < 11 Then 'C3H8
                    hantei = 11
                    H_count_enemy -= 8
                    C_count_enemy -= 3
                    kagoubutu_name = "プロパン C3H8"
                End If
            End If

            If He_count_enemy >= 8 Then

                If hantei < 8 Then
                    hantei = 8
                    He_count_enemy -= 8
                    kagoubutu_name = "ヘリウム He×8"
                End If
            End If

            If Li_count_enemy >= 2 Then

                If S_count_enemy >= 1 And O_count_enemy >= 4 And hantei < 7 Then 'Li2SO4
                    hantei = 7
                    Li_count_enemy -= 2
                    S_count_enemy -= 1
                    O_count_enemy -= 4
                    kagoubutu_name = "硫酸リチウム Li2SO4"
                ElseIf O_count_enemy >= 1 And hantei < 3 Then 'Li2O
                    hantei = 3
                    Li_count_enemy -= 2
                    O_count_enemy -= 1
                    kagoubutu_name = "酸化リチウム Li2O"
                End If
            End If

            If Be_count_enemy >= 2 Then

                If O_count_enemy >= 4 And S_count_enemy >= 1 And hantei < 6 Then 'BeSO4
                    hantei = 6
                    Be_count_enemy -= 1
                    O_count_enemy -= 4
                    S_count_enemy -= 1
                    kagoubutu_name = "硫酸ベリリウム BeSO4"
                ElseIf O_count_enemy >= 2 And H_count_enemy >= 2 And hantei < 5 Then 'Be(OH)2
                    hantei = 5
                    Be_count_enemy -= 1
                    O_count_enemy -= 2
                    H_count_enemy -= 2
                    kagoubutu_name = "水酸化ベリリウム Be(OH)2"
                ElseIf Cl_count_enemy >= 2 And hantei < 3 Then 'BeCl2
                    hantei = 3
                    Be_count_enemy -= 1
                    Cl_count_enemy -= 2
                    kagoubutu_name = "塩化ベリリウム BeCl2"
                ElseIf F_count_enemy >= 2 And hantei < 3 Then 'BeF2
                    hantei = 3
                    Be_count_enemy -= 1
                    F_count_enemy -= 2
                    kagoubutu_name = "フッ化ベリリウム BeF2"
                ElseIf O_count_enemy >= 1 And hantei < 2 Then 'BeO
                    hantei = 2
                    Be_count_enemy -= 1
                    O_count_enemy -= 1
                    kagoubutu_name = "酸化ベリリウム BeO"
                End If
            End If

            If B_count_enemy >= 2 Then

                If O_count_enemy >= 3 And hantei < 5 Then 'B2O3
                    hantei = 5
                    B_count_enemy -= 2
                    O_count_enemy -= 3
                    kagoubutu_name = "酸化ホウ素 B2O3"
                End If
            End If

            If C_count_enemy >= 1 Then

                If O_count_enemy >= 1 And hantei < 2 Then 'CO
                    hantei = 2
                    C_count_enemy -= 1
                    O_count_enemy -= 1
                    kagoubutu_name = "一酸化炭素 CO"
                End If
            End If

            If N_count_enemy >= 2 Then

                If Ca_count_enemy >= 1 And O_count_enemy >= 6 And hantei < 9 Then 'Ca(NO3)2
                    hantei = 9
                    N_count_enemy -= 2
                    Ca_count_enemy -= 1
                    O_count_enemy -= 6
                    kagoubutu_name = "硝酸カルシウム Ca(NO3)2"
                ElseIf hantei < 2 Then 'N2
                    hantei = 2
                    N_count_enemy -= 2
                    kagoubutu_name = "窒素 N2"
                End If
            End If

            If O_count_enemy >= 4 Then

                If Na_count_enemy >= 3 And P_count_enemy >= 1 And hantei < 8 Then 'Na3PO4
                    hantei = 8
                    O_count_enemy -= 4
                    Na_count_enemy -= 3
                    P_count_enemy -= 1
                    kagoubutu_name = "リン酸ナトリウム Na3PO4"
                ElseIf K_count_enemy >= 2 And S_count_enemy >= 1 And hantei < 7 Then 'K2SO4
                    hantei = 7
                    O_count_enemy -= 4
                    K_count_enemy -= 2
                    S_count_enemy -= 1
                    kagoubutu_name = "硫酸カリウム K2SO4"
                ElseIf Ca_count_enemy >= 1 And S_count_enemy >= 1 And hantei < 6 Then 'CaSO4
                    hantei = 6
                    O_count_enemy -= 4
                    Ca_count_enemy -= 1
                    S_count_enemy -= 1
                    kagoubutu_name = "硫酸カルシウム CaSO4"
                ElseIf Al_count_enemy >= 1 And P_count_enemy >= 1 And hantei < 6 Then 'AlPO4
                    hantei = 6
                    O_count_enemy -= 4
                    Al_count_enemy -= 1
                    P_count_enemy -= 1
                    kagoubutu_name = "リン酸アルミニウム AlPO4"
                End If
            End If

            If F_count_enemy >= 3 Then

                If Al_count_enemy >= 1 And hantei < 4 Then 'AlF3
                    hantei = 4
                    F_count_enemy -= 3
                    Al_count_enemy -= 1
                    kagoubutu_name = "フッ化アルミニウム AlF3"
                End If
            End If

            If Ne_count_enemy >= 8 Then

                If hantei < 8 Then
                    hantei = 8
                    Ne_count_enemy -= 8
                    kagoubutu_name = "ネオン Ne×8"
                End If
            End If

            If Na_count_enemy >= 2 Then

                If S_count_enemy >= 1 And hantei < 3 Then 'Na2S
                    hantei = 3
                    Na_count_enemy -= 2
                    S_count_enemy -= 1
                    kagoubutu_name = "硫化ナトリウム Na2S"
                End If
            End If

            If Mg_count_enemy >= 1 Then

                If Cl_count_enemy >= 2 And hantei < 3 Then 'MgCl2
                    hantei = 3
                    Mg_count_enemy -= 1
                    Cl_count_enemy -= 2
                    kagoubutu_name = "塩化マグネシウム MgCl2"
                End If
            End If

            If Al_count_enemy >= 1 Then

                If Cl_count_enemy >= 3 And hantei < 4 Then 'AlCl3
                    hantei = 4
                    Al_count_enemy -= 1
                    Cl_count_enemy -= 3
                    kagoubutu_name = "塩化アルミニウム AlCl3"
                End If
            End If

            If Si_count_enemy >= 3 Then

                If N_count_enemy >= 4 And hantei < 7 Then 'Si3N4
                    hantei = 7
                    Si_count_enemy -= 3
                    N_count_enemy -= 4
                    kagoubutu_name = "窒化ケイ素 Si3N4"
                End If
            End If

            If P_count_enemy >= 2 Then

                If Ca_count_enemy >= 3 And hantei < 5 Then 'Ca3P2
                    hantei = 5
                    P_count_enemy -= 2
                    Ca_count_enemy -= 3
                    kagoubutu_name = "二リン化三カルシウム Ca3P2"
                End If
            End If

            If S_count_enemy >= 1 Then

                If Ca_count_enemy >= 1 And hantei < 2 Then 'CaS
                    hantei = 2
                    S_count_enemy -= 1
                    Ca_count_enemy -= 1
                    kagoubutu_name = "硫化カルシウム CaS"
                End If
            End If

            If Cl_count_enemy >= 2 Then

                If Ca_count_enemy >= 1 And hantei < 3 Then 'CaCl2
                    hantei = 3
                    Cl_count_enemy -= 2
                    Ca_count_enemy -= 1
                    kagoubutu_name = "塩化カルシウム CaCl2"
                ElseIf hantei < 2 Then 'Cl2
                    hantei = 2
                    Cl_count_enemy -= 2
                    kagoubutu_name = "塩素 Cl2"
                End If
            End If

            If Ar_count_enemy >= 8 Then

                If hantei < 8 Then
                    hantei = 8
                    Ar_count_enemy -= 8
                    kagoubutu_name = "アルゴン Ar×8"
                End If
            End If

            If K_count_enemy >= 1 Then

                If F_count_enemy >= 1 And hantei < 2 Then 'KF
                    hantei = 2
                    K_count_enemy -= 1
                    F_count_enemy -= 1
                    kagoubutu_name = "フッ化カリウム KF"
                End If
            End If

            If Ca_count_enemy >= 1 Then

                If O_count_enemy >= 6 And N_count_enemy >= 2 And hantei < 9 Then 'Ca(NO3)2
                    hantei = 9
                    Ca_count_enemy -= 1
                    O_count_enemy -= 6
                    N_count_enemy -= 2
                    kagoubutu_name = "硝酸カルシウム Ca(NO3)2"

                ElseIf O_count_enemy >= 4 And S_count_enemy >= 1 And hantei < 6 Then 'CaSO4
                    hantei = 6
                    Ca_count_enemy -= 1
                    O_count_enemy -= 4
                    S_count_enemy -= 1
                    kagoubutu_name = "硫酸カルシウム CaSO4"
                End If
            End If


            Select Case H_count_enemy

                Case 6, 7
                    If C_count_enemy >= 6 And O_count_enemy >= 1 And hantei < 13 Then 'C6H5OH
                        hantei = 13
                        H_count_enemy -= 6
                        C_count_enemy -= 6
                        O_count_enemy -= 1
                        kagoubutu_name = "フェノール C6H5OH"
                    ElseIf C_count_enemy >= 6 And hantei < 12 Then 'C6H6
                        hantei = 12
                        H_count_enemy -= 6
                        C_count_enemy -= 6
                        kagoubutu_name = "ベンゼン C6H6"
                    ElseIf C_count_enemy >= 3 And O_count_enemy >= 1 And hantei < 10 Then 'CH3COCH3
                        hantei = 10
                        H_count_enemy -= 6
                        C_count_enemy -= 3
                        O_count_enemy -= 1
                        kagoubutu_name = "アセトン CH3COCH3"
                    ElseIf C_count_enemy >= 2 And O_count_enemy >= 1 And hantei < 9 Then 'C2H5OH
                        hantei = 9
                        H_count_enemy -= 6
                        C_count_enemy -= 2
                        O_count_enemy -= 1
                        kagoubutu_name = "エタノール C2H5OH"
                    ElseIf C_count_enemy >= 3 And hantei < 9 Then 'C3H6
                        hantei = 9
                        H_count_enemy -= 6
                        C_count_enemy -= 3
                        kagoubutu_name = "プロピレン C3H6"
                    ElseIf C_count_enemy >= 2 And hantei < 8 Then 'C2H6
                        hantei = 8
                        H_count_enemy -= 6
                        C_count_enemy -= 2
                        kagoubutu_name = "エタン C2H6"
                    End If

                Case 4, 5
                    If C_count_enemy >= 2 And O_count_enemy >= 2 And hantei < 8 Then 'CH3COOH
                        hantei = 8
                        H_count_enemy -= 4
                        C_count_enemy -= 2
                        O_count_enemy -= 2
                        kagoubutu_name = "酢酸 CH3COOH"
                    ElseIf C_count_enemy >= 2 And O_count_enemy >= 1 And hantei < 7 Then 'CH3CHO
                        hantei = 7
                        H_count_enemy -= 4
                        C_count_enemy -= 2
                        O_count_enemy -= 1
                        kagoubutu_name = "アセトアルデヒド CH3CHO"
                    ElseIf C_count_enemy >= 2 And hantei < 6 Then 'C2H4
                        hantei = 6
                        H_count_enemy -= 4
                        C_count_enemy -= 2
                        kagoubutu_name = "エチレン C2H4"
                    ElseIf N_count_enemy >= 1 And Cl_count_enemy >= 1 And hantei < 6 Then 'NH4Cl
                        hantei = 6
                        H_count_enemy -= 4
                        N_count_enemy -= 1
                        Cl_count_enemy -= 1
                        kagoubutu_name = "塩化アンモニウム NH4Cl"
                    ElseIf C_count_enemy >= 1 And O_count_enemy >= 1 And hantei < 6 Then 'CH3OH
                        hantei = 6
                        H_count_enemy -= 4
                        C_count_enemy -= 1
                        O_count_enemy -= 1
                        kagoubutu_name = "メタノール CH3OH"
                    ElseIf C_count_enemy >= 1 And hantei < 5 Then 'CH4
                        hantei = 5
                        H_count_enemy -= 4
                        C_count_enemy -= 1
                        kagoubutu_name = "メタン CH4"
                    End If

                Case 3
                    If O_count_enemy >= 4 And P_count_enemy >= 1 And hantei < 8 Then 'H3PO4
                        hantei = 8
                        H_count_enemy -= 3
                        O_count_enemy -= 4
                        P_count_enemy -= 1
                        kagoubutu_name = "リン酸 H3PO4"
                    ElseIf C_count_enemy >= 2 And O_count_enemy >= 2 And Na_count_enemy >= 1 And hantei < 8 Then 'CH3COONa
                        hantei = 8
                        H_count_enemy -= 3
                        C_count_enemy -= 2
                        O_count_enemy -= 2
                        Na_count_enemy -= 1
                        kagoubutu_name = "酢酸ナトリウム CH3COONa"
                    ElseIf N_count_enemy >= 1 And hantei < 4 Then  'NH3
                        hantei = 4
                        H_count_enemy -= 3
                        N_count_enemy -= 1
                        kagoubutu_name = "アンモニア NH3"
                    End If

                Case 2
                    If C_count_enemy >= 2 And O_count_enemy >= 4 And hantei < 8 Then 'H2C2O4
                        hantei = 8
                        H_count_enemy -= 2
                        C_count_enemy -= 2
                        O_count_enemy -= 4
                        kagoubutu_name = "シュウ酸 H2C2O4"
                    ElseIf S_count_enemy >= 1 And O_count_enemy >= 4 And hantei < 7 Then 'H2SO4
                        hantei = 7
                        H_count_enemy -= 2
                        S_count_enemy -= 1
                        O_count_enemy -= 4
                        kagoubutu_name = "硫酸 H2SO4"
                    ElseIf S_count_enemy >= 1 And O_count_enemy >= 3 And hantei < 6 Then 'H2SO3
                        hantei = 6
                        H_count_enemy -= 2
                        S_count_enemy -= 1
                        O_count_enemy -= 3
                        kagoubutu_name = "亜硫酸 H2SO3"
                    ElseIf C_count_enemy >= 1 And O_count_enemy >= 3 And hantei < 6 Then 'H2CO3
                        hantei = 6
                        H_count_enemy -= 2
                        C_count_enemy -= 1
                        O_count_enemy -= 3
                        kagoubutu_name = "炭酸 H2CO3"
                    ElseIf O_count_enemy >= 2 And hantei < 4 Then 'H2O2
                        hantei = 4
                        H_count_enemy -= 2
                        O_count_enemy -= 2
                        kagoubutu_name = "過酸化水素 H2O2"
                    ElseIf C_count_enemy >= 2 And hantei < 4 Then 'C2H2
                        hantei = 4
                        H_count_enemy -= 2
                        C_count_enemy -= 2
                        kagoubutu_name = "アセチレン C2H2"
                    ElseIf C_count_enemy >= 1 And O_count_enemy >= 1 And hantei < 4 Then 'HCHO
                        hantei = 4
                        H_count_enemy -= 2
                        C_count_enemy -= 1
                        O_count_enemy -= 1
                        kagoubutu_name = "ホルムアルデヒド HCHO"
                    ElseIf O_count_enemy >= 1 And hantei < 3 Then 'H2O
                        hantei = 3
                        H_count_enemy -= 2
                        O_count_enemy -= 1
                        kagoubutu_name = "水 H2O"
                    ElseIf S_count_enemy >= 1 And hantei < 3 Then 'H2S
                        hantei = 3
                        H_count_enemy -= 2
                        S_count_enemy -= 1
                        kagoubutu_name = "硫化水素 H2S"
                    ElseIf hantei < 2 Then 'H2
                        hantei = 2
                        H_count_enemy -= 2
                        kagoubutu_name = "水素 H2"
                    End If

                Case 1
                    If Na_count_enemy >= 1 And S_count_enemy >= 1 And O_count_enemy >= 4 And hantei < 7 Then 'NaHSO4
                        hantei = 7
                        H_count_enemy -= 1
                        Na_count_enemy -= 1
                        S_count_enemy -= 1
                        O_count_enemy -= 1
                        kagoubutu_name = "硫酸水素ナトリウム NaHSO4"
                    ElseIf Al_count_enemy >= 1 And O_count_enemy >= 3 And hantei < 7 Then 'Al(OH)3
                        hantei = 7
                        H_count_enemy -= 1
                        Al_count_enemy -= 1
                        O_count_enemy -= 3
                        kagoubutu_name = "水酸化アルミニウム Al(OH)3"
                    ElseIf Na_count_enemy >= 1 And C_count_enemy >= 1 And O_count_enemy >= 3 And hantei < 6 Then 'NaHCO3
                        hantei = 6
                        H_count_enemy -= 1
                        Na_count_enemy -= 1
                        C_count_enemy -= 1
                        O_count_enemy -= 3
                        kagoubutu_name = "炭酸水素ナトリウム NaHCO3"
                    ElseIf Cl_count_enemy >= 1 And O_count_enemy >= 4 And hantei < 6 Then 'HClO4
                        hantei = 6
                        H_count_enemy -= 1
                        Cl_count_enemy -= 1
                        O_count_enemy -= 4
                        kagoubutu_name = "過塩素酸 HClO4"
                    ElseIf Cl_count_enemy >= 1 And O_count_enemy >= 3 And hantei < 5 Then 'HClO3
                        hantei = 5
                        H_count_enemy -= 1
                        Cl_count_enemy -= 1
                        O_count_enemy -= 3
                        kagoubutu_name = "塩素酸 HClO3"
                    ElseIf Ca_count_enemy >= 1 And O_count_enemy >= 2 And hantei < 5 Then 'Ca(OH)2
                        hantei = 5
                        H_count_enemy -= 1
                        Ca_count_enemy -= 1
                        O_count_enemy -= 2
                        kagoubutu_name = "水酸化カルシウム Ca(OH)2"
                    ElseIf N_count_enemy >= 1 And O_count_enemy >= 3 And hantei < 5 Then 'HNO3
                        hantei = 5
                        H_count_enemy -= 1
                        N_count_enemy -= 1
                        O_count_enemy -= 3
                        kagoubutu_name = "硝酸 HNO3"
                    ElseIf N_count_enemy >= 1 And O_count_enemy >= 2 And hantei < 4 Then 'HNO2
                        hantei = 4
                        H_count_enemy -= 1
                        N_count_enemy -= 1
                        O_count_enemy -= 2
                        kagoubutu_name = "亜硝酸 HNO2"
                    ElseIf Cl_count_enemy >= 1 And O_count_enemy >= 2 And hantei < 4 Then 'HClO2
                        hantei = 4
                        H_count_enemy -= 1
                        Cl_count_enemy -= 1
                        O_count_enemy -= 2
                        kagoubutu_name = "亜塩素酸 HClO2"
                    ElseIf Cl_count_enemy >= 1 And O_count_enemy >= 1 And hantei < 3 Then 'HClO
                        hantei = 3
                        H_count_enemy -= 1
                        Cl_count_enemy -= 1
                        O_count_enemy -= 1
                        kagoubutu_name = "次亜塩素酸 HClO"
                    ElseIf Na_count_enemy >= 1 And O_count_enemy >= 1 And hantei < 3 Then 'NaOH
                        hantei = 3
                        H_count_enemy -= 1
                        Na_count_enemy -= 1
                        O_count_enemy -= 1
                        kagoubutu_name = "水酸化ナトリウム NaOH"
                    ElseIf K_count_enemy >= 1 And O_count_enemy >= 1 And hantei < 3 Then 'KOH
                        hantei = 3
                        H_count_enemy -= 1
                        K_count_enemy -= 1
                        O_count_enemy -= 1
                        kagoubutu_name = "水酸化カリウム KOH"
                    ElseIf F_count_enemy >= 1 And hantei < 2 Then 'HF
                        hantei = 2
                        H_count_enemy -= 1
                        F_count_enemy -= 1
                        kagoubutu_name = "フッ化水素 HF"
                    ElseIf Cl_count_enemy >= 1 And hantei < 2 Then 'HCl
                        hantei = 2
                        H_count_enemy -= 1
                        Cl_count_enemy -= 1
                        kagoubutu_name = "塩化水素 HCl"
                    End If
            End Select

            Select Case He_count_enemy 'He

                Case 7
                    If hantei < 7 Then
                        hantei = 7
                        He_count_enemy -= 7
                        kagoubutu_name = "ヘリウム He×7"
                    End If

                Case 6
                    If hantei < 6 Then
                        hantei = 6
                        He_count_enemy -= 6
                        kagoubutu_name = "ヘリウム He×6"
                    End If

                Case 5
                    If hantei < 5 Then
                        hantei = 5
                        He_count_enemy -= 5
                        kagoubutu_name = "ヘリウム He×5"
                    End If

                Case 4
                    If hantei < 4 Then
                        hantei = 4
                        He_count_enemy -= 4
                        kagoubutu_name = "ヘリウム He×4"
                    End If

                Case 3
                    If hantei < 3 Then
                        hantei = 3
                        He_count_enemy -= 3
                        kagoubutu_name = "ヘリウム He×3"
                    End If

                Case 2
                    If hantei < 2 Then
                        hantei = 2
                        He_count_enemy -= 2
                        kagoubutu_name = "ヘリウム He×2"
                    End If

                Case 1
                    If hantei < 1 Then
                        hantei = 1
                        He_count_enemy -= 1
                        kagoubutu_name = "ヘリウム He"
                    End If

            End Select

            Select Case Li_count_enemy

                Case 1
                    If O_count_enemy >= 1 And H_count_enemy >= 1 And hantei < 3 Then 'LiOH
                        hantei = 3
                        Li_count_enemy -= 1
                        O_count_enemy -= 1
                        H_count_enemy -= 1
                        kagoubutu_name = "水酸化リチウム LiOH"
                    ElseIf Cl_count_enemy >= 1 And hantei < 2 Then 'LiCl
                        hantei = 2
                        Li_count_enemy -= 1
                        Cl_count_enemy -= 1
                        kagoubutu_name = "塩化リチウム LiCl"
                    ElseIf F_count_enemy >= 1 And hantei < 2 Then 'LiF
                        hantei = 2
                        Li_count_enemy -= 1
                        F_count_enemy -= 1
                        kagoubutu_name = "フッ化リチウム LiF"
                    End If

            End Select

            Select Case Be_count_enemy

            End Select

            Select Case B_count_enemy

                Case 1
                    If O_count_enemy >= 3 And H_count_enemy >= 3 And hantei < 7 Then 'B(OH)3
                        hantei = 7
                        B_count_enemy -= 1
                        O_count_enemy -= 3
                        H_count_enemy -= 3
                        kagoubutu_name = "ホウ酸 B(OH)3"
                    ElseIf P_count_enemy >= 1 And O_count_enemy >= 4 And hantei < 6 Then 'BPO4
                        hantei = 6
                        B_count_enemy -= 1
                        P_count_enemy -= 1
                        O_count_enemy -= 4
                        kagoubutu_name = "リン酸ホウ素 BPO4"
                    ElseIf F_count_enemy >= 3 And hantei < 4 Then 'BF3
                        hantei = 4
                        B_count_enemy -= 1
                        F_count_enemy -= 3
                        kagoubutu_name = "三フッ化ホウ素 BF3"
                    ElseIf N_count_enemy >= 1 And hantei < 4 Then 'BN
                        hantei = 2
                        B_count_enemy -= 1
                        N_count_enemy -= 1
                        kagoubutu_name = "窒化ホウ素 BN"
                    End If

            End Select

            Select Case C_count_enemy

            End Select

            Select Case N_count_enemy

                Case 1
                    If Na_count_enemy >= 1 And O_count_enemy >= 3 And hantei < 5 Then 'NaNO3
                        hantei = 5
                        N_count_enemy -= 1
                        Na_count_enemy -= 1
                        O_count_enemy -= 3
                        kagoubutu_name = "硝酸ナトリウム NaNO3"
                    ElseIf K_count_enemy >= 1 And O_count_enemy >= 3 And hantei < 5 Then 'KNO3
                        hantei = 5
                        N_count_enemy -= 1
                        K_count_enemy -= 1
                        O_count_enemy -= 3
                        kagoubutu_name = "硝酸カリウム KN03"
                    ElseIf O_count_enemy >= 2 And hantei < 3 Then 'NO2
                        hantei = 3
                        N_count_enemy -= 1
                        O_count_enemy -= 2
                        kagoubutu_name = "二酸化窒素 NO2"
                    ElseIf O_count_enemy >= 1 And hantei < 2 Then 'NO
                        hantei = 2
                        N_count_enemy -= 1
                        O_count_enemy -= 1
                        kagoubutu_name = "一酸化窒素 NO"
                    End If

            End Select

            Select Case O_count_enemy

                Case 3
                    If Na_count_enemy >= 1 And H_count_enemy >= 1 And C_count_enemy >= 1 And hantei < 6 Then 'NaHCO3
                        hantei = 6
                        O_count_enemy -= 3
                        C_count_enemy -= 1
                        Na_count_enemy -= 1
                        H_count_enemy -= 1
                        kagoubutu_name = "炭酸ナトリウム NaHCO3"
                    ElseIf Mg_count_enemy >= 1 And C_count_enemy >= 1 And hantei < 5 Then 'MgCO3
                        hantei = 5
                        O_count_enemy -= 3
                        C_count_enemy -= 1
                        Mg_count_enemy -= 1
                        kagoubutu_name = "炭酸マグネシウム MgCO3"
                    ElseIf Ca_count_enemy >= 1 And C_count_enemy >= 1 And hantei < 5 Then 'CaCO3
                        hantei = 5
                        O_count_enemy -= 3
                        Ca_count_enemy -= 1
                        C_count_enemy -= 1
                        kagoubutu_name = "炭酸カルシウム CaCO3"
                    ElseIf Al_count_enemy >= 2 And hantei < 5 Then 'Al2O3
                        hantei = 5
                        O_count_enemy -= 3
                        Al_count_enemy -= 2
                        kagoubutu_name = "酸化アルミニウム Al2O3"
                    ElseIf S_count_enemy >= 1 And hantei < 4 Then 'SO3
                        hantei = 4
                        O_count_enemy -= 3
                        S_count_enemy -= 1
                        kagoubutu_name = "三酸化硫黄 SO3"
                    ElseIf hantei < 3 Then 'O3
                        hantei = 3
                        O_count_enemy -= 3
                        kagoubutu_name = "オゾン O3"
                    End If

                Case 2
                    If C_count_enemy >= 1 And hantei < 3 Then 'CO2
                        hantei = 3
                        O_count_enemy -= 2
                        C_count_enemy -= 1
                        kagoubutu_name = "二酸化炭素 CO2"
                    ElseIf S_count_enemy >= 1 And hantei < 3 Then 'SO2
                        hantei = 3
                        O_count_enemy -= 2
                        S_count_enemy -= 1
                        kagoubutu_name = "二酸化硫黄 SO2"
                    ElseIf Si_count_enemy >= 1 And hantei < 3 Then 'SiO2
                        hantei = 3
                        O_count_enemy -= 2
                        Si_count_enemy -= 1
                        kagoubutu_name = "二酸化ケイ素 SiO2"
                    ElseIf hantei < 3 Then 'O2
                        hantei = 2
                        O_count_enemy -= 2
                        kagoubutu_name = "酸素 O2"
                    End If

                Case 1
                    If Na_count_enemy >= 2 And hantei < 3 Then 'Na2O
                        hantei = 3
                        O_count_enemy -= 1
                        Na_count_enemy -= 2
                        kagoubutu_name = "酸化ナトリウム Na2O"
                    ElseIf Mg_count_enemy >= 1 And hantei < 2 Then 'MgO
                        hantei = 2
                        O_count_enemy -= 1
                        Mg_count_enemy -= 1
                        kagoubutu_name = "酸化マグネシウム MgO"
                    ElseIf Ca_count_enemy >= 1 And hantei < 2 Then 'CaO
                        hantei = 2
                        O_count_enemy -= 1
                        Ca_count_enemy -= 1
                        kagoubutu_name = "酸化カルシウム CaO"
                    End If

            End Select

            Select Case F_count_enemy

                Case 2
                    If Ca_count_enemy >= 1 And hantei < 3 Then 'CaF2
                        hantei = 3
                        F_count_enemy -= 2
                        Ca_count_enemy -= 1
                        kagoubutu_name = "フッ化カルシウム CaF2"
                    ElseIf hantei < 2 Then 'F2
                        hantei = 2
                        F_count_enemy -= 2
                        kagoubutu_name = "フッ素 F2"
                    End If

                Case 1
                    If Na_count_enemy >= 1 And hantei < 2 Then 'NaF
                        hantei = 2
                        F_count_enemy -= 1
                        Na_count_enemy -= 1
                        kagoubutu_name = "フッ化ナトリウム NaF"
                    End If

            End Select

            Select Case Ne_count_enemy 'Ne

                Case 7
                    If hantei < 7 Then
                        hantei = 7
                        Ne_count_enemy -= 7
                        kagoubutu_name = "ネオン Ne×7"
                    End If

                Case 6
                    If hantei < 6 Then
                        hantei = 6
                        Ne_count_enemy -= 6
                        kagoubutu_name = "ネオン Ne×6"
                    End If

                Case 5
                    If hantei < 5 Then
                        hantei = 5
                        Ne_count_enemy -= 5
                        kagoubutu_name = "ネオン Ne×5"
                    End If

                Case 4
                    If hantei < 4 Then
                        hantei = 4
                        Ne_count_enemy -= 4
                        kagoubutu_name = "ネオン Ne×4"
                    End If

                Case 3
                    If hantei < 3 Then
                        hantei = 3
                        Ne_count_enemy -= 3
                        kagoubutu_name = "ネオン Ne×3"
                    End If

                Case 2
                    If hantei < 2 Then
                        hantei = 2
                        Ne_count_enemy -= 2
                        kagoubutu_name = "ネオン Ne×2"
                    End If

                Case 1
                    If hantei < 1 Then
                        hantei = 1
                        Ne_count_enemy -= 1
                        kagoubutu_name = "ネオン Ne"
                    End If

            End Select

            Select Case Na_count_enemy

                Case 1
                    If Cl_count_enemy >= 1 And hantei < 2 Then 'NaCl
                        hantei = 2
                        Na_count_enemy -= 1
                        Cl_count_enemy -= 1
                        kagoubutu_name = "塩化ナトリウム NaCl"
                    End If

            End Select

            Select Case Mg_count_enemy

            End Select

            Select Case Al_count_enemy

            End Select

            Select Case Si_count_enemy

                Case 1, 2
                    If Cl_count_enemy >= 3 And hantei < 4 Then 'SiCl3
                        hantei = 4
                        Si_count_enemy -= 1
                        Cl_count_enemy -= 3
                        kagoubutu_name = "塩化ケイ素 SiCl3"
                    ElseIf O_count_enemy >= 1 And hantei < 2 Then 'SiO
                        hantei = 2
                        Si_count_enemy -= 1
                        O_count_enemy -= 1
                        kagoubutu_name = "一酸化ケイ素 SiO"
                    ElseIf S_count_enemy >= 1 And hantei < 2 Then 'SSi
                        hantei = 2
                        Si_count_enemy -= 1
                        S_count_enemy -= 1
                        kagoubutu_name = "硫化ケイ素 SSi"
                    End If

            End Select

            Select Case P_count_enemy

                Case 1
                    If F_count_enemy >= 3 And hantei < 4 Then 'PF3
                        hantei = 4
                        P_count_enemy -= 1
                        F_count_enemy -= 3
                        kagoubutu_name = "三フッ化リン PF3"
                    ElseIf Cl_count_enemy >= 3 And hantei < 4 Then 'PCl3
                        hantei = 4
                        P_count_enemy -= 1
                        Cl_count_enemy -= 3
                        kagoubutu_name = "三塩化リン PCl3"
                    End If


            End Select

            Select Case S_count_enemy

            End Select

            Select Case Cl_count_enemy

                Case 1
                    If K_count_enemy >= 1 And hantei < 2 Then 'KCl
                        hantei = 2
                        Cl_count_enemy -= 1
                        K_count_enemy -= 1
                        kagoubutu_name = "塩化カリウム KCl"
                    End If

            End Select

            Select Case Ar_count_enemy 'Ar

                Case 7
                    If hantei < 7 Then
                        hantei = 7
                        Ar_count_enemy -= 7
                        kagoubutu_name = "アルゴン Ar×7"
                    End If

                Case 6
                    If hantei < 6 Then
                        hantei = 6
                        Ar_count_enemy -= 6
                        kagoubutu_name = "アルゴン Ar×6"
                    End If

                Case 5
                    If hantei < 5 Then
                        hantei = 5
                        Ar_count_enemy -= 5
                        kagoubutu_name = "アルゴン Ar×5"
                    End If

                Case 4
                    If hantei < 4 Then
                        hantei = 4
                        Ar_count_enemy -= 4
                        kagoubutu_name = "アルゴン Ar×4"
                    End If

                Case 3
                    If hantei < 3 Then
                        hantei = 3
                        Ar_count_enemy -= 3
                        kagoubutu_name = "アルゴン Ar×3"
                    End If

                Case 2
                    If hantei < 2 Then
                        hantei = 2
                        Ar_count_enemy -= 2
                        kagoubutu_name = "アルゴン Ar×2"
                    End If

                Case 1
                    If hantei < 1 Then
                        hantei = 1
                        Ar_count_enemy -= 1
                        kagoubutu_name = "アルゴン Ar"
                    End If

            End Select

            Select Case K_count_enemy

            End Select

            Select Case Ca_count_enemy

            End Select

            kagoubutu_name_enemy = kagoubutu_name


        Else


            enemy_flg = True
            player_flg = False

            If H_count_player >= 8 Then

                If C_count_player >= 1 And N_count_player >= 2 And O_count_player >= 3 And hantei < 14 Then '(NH4)2CO3
                    hantei = 14
                    H_count_player -= 8
                    C_count_player -= 1
                    N_count_player -= 2
                    O_count_player -= 3
                    kagoubutu_name = "炭酸アンモニウム (NH4)2CO3"

                ElseIf C_count_player >= 3 And hantei < 11 Then 'C3H8
                    hantei = 11
                    H_count_player -= 8
                    C_count_player -= 3
                    kagoubutu_name = "プロパン C3H8"
                End If
            End If

            If He_count_player >= 8 Then

                If hantei < 8 Then
                    hantei = 8
                    He_count_player -= 8
                    kagoubutu_name = "ヘリウム He×8"
                End If
            End If

            If Li_count_player >= 2 Then

                If S_count_player >= 1 And O_count_player >= 4 And hantei < 7 Then 'Li2SO4
                    hantei = 7
                    Li_count_player -= 2
                    S_count_player -= 1
                    O_count_player -= 4
                    kagoubutu_name = "硫酸リチウム Li2SO4"
                ElseIf O_count_player >= 1 And hantei < 3 Then 'Li2O
                    hantei = 3
                    Li_count_player -= 2
                    O_count_player -= 1
                    kagoubutu_name = "酸化リチウム Li2O"
                End If
            End If

            If Be_count_player >= 2 Then

                If O_count_player >= 4 And S_count_player >= 1 And hantei < 6 Then 'BeSO4
                    hantei = 6
                    Be_count_player -= 1
                    O_count_player -= 4
                    S_count_player -= 1
                    kagoubutu_name = "硫酸ベリリウム BeSO4"
                ElseIf O_count_player >= 2 And H_count_player >= 2 And hantei < 5 Then 'Be(OH)2
                    hantei = 5
                    Be_count_player -= 1
                    O_count_player -= 2
                    H_count_player -= 2
                    kagoubutu_name = "水酸化ベリリウム Be(OH)2"
                ElseIf Cl_count_player >= 2 And hantei < 3 Then 'BeCl2
                    hantei = 3
                    Be_count_player -= 1
                    Cl_count_player -= 2
                    kagoubutu_name = "塩化ベリリウム BeCl2"
                ElseIf F_count_player >= 2 And hantei < 3 Then 'BeF2
                    hantei = 3
                    Be_count_player -= 1
                    F_count_player -= 2
                    kagoubutu_name = "フッ化ベリリウム BeF2"
                ElseIf O_count_player >= 1 And hantei < 2 Then 'BeO
                    hantei = 2
                    Be_count_player -= 1
                    O_count_player -= 1
                    kagoubutu_name = "酸化ベリリウム BeO"
                End If
            End If

            If B_count_player >= 2 Then

                If O_count_player >= 3 And hantei < 5 Then 'B2O3
                    hantei = 5
                    B_count_player -= 2
                    O_count_player -= 3
                    kagoubutu_name = "酸化ホウ素 B2O3"
                End If
            End If

            If C_count_player >= 1 Then

                If O_count_player >= 1 And hantei < 2 Then 'CO
                    hantei = 2
                    C_count_player -= 1
                    O_count_player -= 1
                    kagoubutu_name = "一酸化炭素 CO"
                End If
            End If

            If N_count_player >= 2 Then

                If Ca_count_player >= 1 And O_count_player >= 6 And hantei < 9 Then 'Ca(NO3)2
                    hantei = 9
                    N_count_player -= 2
                    Ca_count_player -= 1
                    O_count_player -= 6
                    kagoubutu_name = "硝酸カルシウム Ca(NO3)2"
                ElseIf hantei < 2 Then 'N2
                    hantei = 2
                    N_count_player -= 2
                    kagoubutu_name = "窒素 N2"
                End If
            End If

            If O_count_player >= 4 Then

                If Na_count_player >= 3 And P_count_player >= 1 And hantei < 8 Then 'Na3PO4
                    hantei = 8
                    O_count_player -= 4
                    Na_count_player -= 3
                    P_count_player -= 1
                    kagoubutu_name = "リン酸ナトリウム Na3PO4"
                ElseIf K_count_player >= 2 And S_count_player >= 1 And hantei < 7 Then 'K2SO4
                    hantei = 7
                    O_count_player -= 4
                    K_count_player -= 2
                    S_count_player -= 1
                    kagoubutu_name = "硫酸カリウム K2SO4"
                ElseIf Ca_count_player >= 1 And S_count_player >= 1 And hantei < 6 Then 'CaSO4
                    hantei = 6
                    O_count_player -= 4
                    Ca_count_player -= 1
                    S_count_player -= 1
                    kagoubutu_name = "硫酸カルシウム CaSO4"
                ElseIf Al_count_player >= 1 And P_count_player >= 1 And hantei < 6 Then 'AlPO4
                    hantei = 6
                    O_count_player -= 4
                    Al_count_player -= 1
                    P_count_player -= 1
                    kagoubutu_name = "リン酸アルミニウム AlPO4"
                End If
            End If

            If F_count_player >= 3 Then

                If Al_count_player >= 1 And hantei < 4 Then 'AlF3
                    hantei = 4
                    F_count_player -= 3
                    Al_count_player -= 1
                    kagoubutu_name = "フッ化アルミニウム AlF3"
                End If
            End If

            If Ne_count_player >= 8 Then

                If hantei < 8 Then
                    hantei = 8
                    Ne_count_player -= 8
                    kagoubutu_name = "ネオン Ne×8"
                End If
            End If

            If Na_count_player >= 2 Then

                If S_count_player >= 1 And hantei < 3 Then 'Na2S
                    hantei = 3
                    Na_count_player -= 2
                    S_count_player -= 1
                    kagoubutu_name = "硫化ナトリウム Na2S"
                End If
            End If

            If Mg_count_player >= 1 Then

                If Cl_count_player >= 2 And hantei < 3 Then 'MgCl2
                    hantei = 3
                    Mg_count_player -= 1
                    Cl_count_player -= 2
                    kagoubutu_name = "塩化マグネシウム MgCl2"
                End If
            End If

            If Al_count_player >= 1 Then

                If Cl_count_player >= 3 And hantei < 4 Then 'AlCl3
                    hantei = 4
                    Al_count_player -= 1
                    Cl_count_player -= 3
                    kagoubutu_name = "塩化アルミニウム AlCl3"
                End If
            End If

            If Si_count_player >= 3 Then

                If N_count_player >= 4 And hantei < 7 Then 'Si3N4
                    hantei = 7
                    Si_count_player -= 3
                    N_count_player -= 4
                    kagoubutu_name = "窒化ケイ素 Si3N4"
                End If
            End If

            If P_count_player >= 2 Then

                If Ca_count_player >= 3 And hantei < 5 Then 'Ca3P2
                    hantei = 5
                    P_count_player -= 2
                    Ca_count_player -= 3
                    kagoubutu_name = "二リン化三カルシウム Ca3P2"
                End If
            End If

            If S_count_player >= 1 Then

                If Ca_count_player >= 1 And hantei < 2 Then 'CaS
                    hantei = 2
                    S_count_player -= 1
                    Ca_count_player -= 1
                    kagoubutu_name = "硫化カルシウム CaS"
                End If
            End If

            If Cl_count_player >= 2 Then

                If Ca_count_player >= 1 And hantei < 3 Then 'CaCl2
                    hantei = 3
                    Cl_count_player -= 2
                    Ca_count_player -= 1
                    kagoubutu_name = "塩化カルシウム CaCl2"
                ElseIf hantei < 2 Then 'Cl2
                    hantei = 2
                    Cl_count_player -= 2
                    kagoubutu_name = "塩素 Cl2"
                End If
            End If

            If Ar_count_player >= 8 Then

                If hantei < 8 Then
                    hantei = 8
                    Ar_count_player -= 8
                    kagoubutu_name = "アルゴン Ar×8"
                End If
            End If

            If K_count_player >= 1 Then

                If F_count_player >= 1 And hantei < 2 Then 'KF
                    hantei = 2
                    K_count_player -= 1
                    F_count_player -= 1
                    kagoubutu_name = "フッ化カリウム KF"
                End If
            End If

            If Ca_count_player >= 1 Then

                If O_count_player >= 6 And N_count_player >= 2 And hantei < 9 Then 'Ca(NO3)2
                    hantei = 9
                    Ca_count_player -= 1
                    O_count_player -= 6
                    N_count_player -= 2
                    kagoubutu_name = "硝酸カルシウム Ca(NO3)2"

                ElseIf O_count_player >= 4 And S_count_player >= 1 And hantei < 6 Then 'CaSO4
                    hantei = 6
                    Ca_count_player -= 1
                    O_count_player -= 4
                    S_count_player -= 1
                    kagoubutu_name = "硫酸カルシウム CaSO4"
                End If
            End If


            Select Case H_count_player

                Case 6, 7
                    If C_count_player >= 6 And O_count_player >= 1 And hantei < 13 Then 'C6H5OH
                        hantei = 13
                        H_count_player -= 6
                        C_count_player -= 6
                        O_count_player -= 1
                        kagoubutu_name = "フェノール C6H5OH"
                    ElseIf C_count_player >= 6 And hantei < 12 Then 'C6H6
                        hantei = 12
                        H_count_player -= 6
                        C_count_player -= 6
                        kagoubutu_name = "ベンゼン C6H6"
                    ElseIf C_count_player >= 3 And O_count_player >= 1 And hantei < 10 Then 'CH3COCH3
                        hantei = 10
                        H_count_player -= 6
                        C_count_player -= 3
                        O_count_player -= 1
                        kagoubutu_name = "アセトン CH3COCH3"
                    ElseIf C_count_player >= 2 And O_count_player >= 1 And hantei < 9 Then 'C2H5OH
                        hantei = 9
                        H_count_player -= 6
                        C_count_player -= 2
                        O_count_player -= 1
                        kagoubutu_name = "エタノール C2H5OH"
                    ElseIf C_count_player >= 3 And hantei < 9 Then 'C3H6
                        hantei = 9
                        H_count_player -= 6
                        C_count_player -= 3
                        kagoubutu_name = "プロピレン C3H6"
                    ElseIf C_count_player >= 2 And hantei < 8 Then 'C2H6
                        hantei = 8
                        H_count_player -= 6
                        C_count_player -= 2
                        kagoubutu_name = "エタン C2H6"
                    End If

                Case 4, 5
                    If C_count_player >= 2 And O_count_player >= 2 And hantei < 8 Then 'CH3COOH
                        hantei = 8
                        H_count_player -= 4
                        C_count_player -= 2
                        O_count_player -= 2
                        kagoubutu_name = "酢酸 CH3COOH"
                    ElseIf C_count_player >= 2 And O_count_player >= 1 And hantei < 7 Then 'CH3CHO
                        hantei = 7
                        H_count_player -= 4
                        C_count_player -= 2
                        O_count_player -= 1
                        kagoubutu_name = "アセトアルデヒド CH3CHO"
                    ElseIf C_count_player >= 2 And hantei < 6 Then 'C2H4
                        hantei = 6
                        H_count_player -= 4
                        C_count_player -= 2
                        kagoubutu_name = "エチレン C2H4"
                    ElseIf N_count_player >= 1 And Cl_count_player >= 1 And hantei < 6 Then 'NH4Cl
                        hantei = 6
                        H_count_player -= 4
                        N_count_player -= 1
                        Cl_count_player -= 1
                        kagoubutu_name = "塩化アンモニウム NH4Cl"
                    ElseIf C_count_player >= 1 And O_count_player >= 1 And hantei < 6 Then 'CH3OH
                        hantei = 6
                        H_count_player -= 4
                        C_count_player -= 1
                        O_count_player -= 1
                        kagoubutu_name = "メタノール CH3OH"
                    ElseIf C_count_player >= 1 And hantei < 5 Then 'CH4
                        hantei = 5
                        H_count_player -= 4
                        C_count_player -= 1
                        kagoubutu_name = "メタン CH4"
                    End If

                Case 3
                    If O_count_player >= 4 And P_count_player >= 1 And hantei < 8 Then 'H3PO4
                        hantei = 8
                        H_count_player -= 3
                        O_count_player -= 4
                        P_count_player -= 1
                        kagoubutu_name = "リン酸 H3PO4"
                    ElseIf C_count_player >= 2 And O_count_player >= 2 And Na_count_player >= 1 And hantei < 8 Then 'CH3COONa
                        hantei = 8
                        H_count_player -= 3
                        C_count_player -= 2
                        O_count_player -= 2
                        Na_count_player -= 1
                        kagoubutu_name = "酢酸ナトリウム CH3COONa"
                    ElseIf N_count_player >= 1 And hantei < 4 Then  'NH3
                        hantei = 4
                        H_count_player -= 3
                        N_count_player -= 1
                        kagoubutu_name = "アンモニア NH3"
                    End If

                Case 2
                    If C_count_player >= 2 And O_count_player >= 4 And hantei < 8 Then 'H2C2O4
                        hantei = 8
                        H_count_player -= 2
                        C_count_player -= 2
                        O_count_player -= 4
                        kagoubutu_name = "シュウ酸 H2C2O4"
                    ElseIf S_count_player >= 1 And O_count_player >= 4 And hantei < 7 Then 'H2SO4
                        hantei = 7
                        H_count_player -= 2
                        S_count_player -= 1
                        O_count_player -= 4
                        kagoubutu_name = "硫酸 H2SO4"
                    ElseIf S_count_player >= 1 And O_count_player >= 3 And hantei < 6 Then 'H2SO3
                        hantei = 6
                        H_count_player -= 2
                        S_count_player -= 1
                        O_count_player -= 3
                        kagoubutu_name = "亜硫酸 H2SO3"
                    ElseIf C_count_player >= 1 And O_count_player >= 3 And hantei < 6 Then 'H2CO3
                        hantei = 6
                        H_count_player -= 2
                        C_count_player -= 1
                        O_count_player -= 3
                        kagoubutu_name = "炭酸 H2CO3"
                    ElseIf O_count_player >= 2 And hantei < 4 Then 'H2O2
                        hantei = 4
                        H_count_player -= 2
                        O_count_player -= 2
                        kagoubutu_name = "過酸化水素 H2O2"
                    ElseIf C_count_player >= 2 And hantei < 4 Then 'C2H2
                        hantei = 4
                        H_count_player -= 2
                        C_count_player -= 2
                        kagoubutu_name = "アセチレン C2H2"
                    ElseIf C_count_player >= 1 And O_count_player >= 1 And hantei < 4 Then 'HCHO
                        hantei = 4
                        H_count_player -= 2
                        C_count_player -= 1
                        O_count_player -= 1
                        kagoubutu_name = "ホルムアルデヒド HCHO"
                    ElseIf O_count_player >= 1 And hantei < 3 Then 'H2O
                        hantei = 3
                        H_count_player -= 2
                        O_count_player -= 1
                        kagoubutu_name = "水 H2O"
                    ElseIf S_count_player >= 1 And hantei < 3 Then 'H2S
                        hantei = 3
                        H_count_player -= 2
                        S_count_player -= 1
                        kagoubutu_name = "硫化水素 H2S"
                    ElseIf hantei < 2 Then 'H2
                        hantei = 2
                        H_count_player -= 2
                        kagoubutu_name = "水素 H2"
                    End If

                Case 1
                    If Na_count_player >= 1 And S_count_player >= 1 And O_count_player >= 4 And hantei < 7 Then 'NaHSO4
                        hantei = 7
                        H_count_player -= 1
                        Na_count_player -= 1
                        S_count_player -= 1
                        O_count_player -= 1
                        kagoubutu_name = "硫酸水素ナトリウム NaHSO4"
                    ElseIf Al_count_player >= 1 And O_count_player >= 3 And hantei < 7 Then 'Al(OH)3
                        hantei = 7
                        H_count_player -= 1
                        Al_count_player -= 1
                        O_count_player -= 3
                        kagoubutu_name = "水酸化アルミニウム Al(OH)3"
                    ElseIf Na_count_player >= 1 And C_count_player >= 1 And O_count_player >= 3 And hantei < 6 Then 'NaHCO3
                        hantei = 6
                        H_count_player -= 1
                        Na_count_player -= 1
                        C_count_player -= 1
                        O_count_player -= 3
                        kagoubutu_name = "炭酸水素ナトリウム NaHCO3"
                    ElseIf Cl_count_player >= 1 And O_count_player >= 4 And hantei < 6 Then 'HClO4
                        hantei = 6
                        H_count_player -= 1
                        Cl_count_player -= 1
                        O_count_player -= 4
                        kagoubutu_name = "過塩素酸 HClO4"
                    ElseIf Cl_count_player >= 1 And O_count_player >= 3 And hantei < 5 Then 'HClO3
                        hantei = 5
                        H_count_player -= 1
                        Cl_count_player -= 1
                        O_count_player -= 3
                        kagoubutu_name = "塩素酸 HClO3"
                    ElseIf Ca_count_player >= 1 And O_count_player >= 2 And hantei < 5 Then 'Ca(OH)2
                        hantei = 5
                        H_count_player -= 1
                        Ca_count_player -= 1
                        O_count_player -= 2
                        kagoubutu_name = "水酸化カルシウム Ca(OH)2"
                    ElseIf N_count_player >= 1 And O_count_player >= 3 And hantei < 5 Then 'HNO3
                        hantei = 5
                        H_count_player -= 1
                        N_count_player -= 1
                        O_count_player -= 3
                        kagoubutu_name = "硝酸 HNO3"
                    ElseIf N_count_player >= 1 And O_count_player >= 2 And hantei < 4 Then 'HNO2
                        hantei = 4
                        H_count_player -= 1
                        N_count_player -= 1
                        O_count_player -= 2
                        kagoubutu_name = "亜硝酸 HNO2"
                    ElseIf Cl_count_player >= 1 And O_count_player >= 2 And hantei < 4 Then 'HClO2
                        hantei = 4
                        H_count_player -= 1
                        Cl_count_player -= 1
                        O_count_player -= 2
                        kagoubutu_name = "亜塩素酸 HClO2"
                    ElseIf Cl_count_player >= 1 And O_count_player >= 1 And hantei < 3 Then 'HClO
                        hantei = 3
                        H_count_player -= 1
                        Cl_count_player -= 1
                        O_count_player -= 1
                        kagoubutu_name = "次亜塩素酸 HClO"
                    ElseIf Na_count_player >= 1 And O_count_player >= 1 And hantei < 3 Then 'NaOH
                        hantei = 3
                        H_count_player -= 1
                        Na_count_player -= 1
                        O_count_player -= 1
                        kagoubutu_name = "水酸化ナトリウム NaOH"
                    ElseIf K_count_player >= 1 And O_count_player >= 1 And hantei < 3 Then 'KOH
                        hantei = 3
                        H_count_player -= 1
                        K_count_player -= 1
                        O_count_player -= 1
                        kagoubutu_name = "水酸化カリウム KOH"
                    ElseIf F_count_player >= 1 And hantei < 2 Then 'HF
                        hantei = 2
                        H_count_player -= 1
                        F_count_player -= 1
                        kagoubutu_name = "フッ化水素 HF"
                    ElseIf Cl_count_player >= 1 And hantei < 2 Then 'HCl
                        hantei = 2
                        H_count_player -= 1
                        Cl_count_player -= 1
                        kagoubutu_name = "塩化水素 HCl"
                    End If
            End Select

            Select Case He_count_player 'He

                Case 7
                    If hantei < 7 Then
                        hantei = 7
                        He_count_player -= 7
                        kagoubutu_name = "ヘリウム He×7"
                    End If

                Case 6
                    If hantei < 6 Then
                        hantei = 6
                        He_count_player -= 6
                        kagoubutu_name = "ヘリウム He×6"
                    End If

                Case 5
                    If hantei < 5 Then
                        hantei = 5
                        He_count_player -= 5
                        kagoubutu_name = "ヘリウム He×5"
                    End If

                Case 4
                    If hantei < 4 Then
                        hantei = 4
                        He_count_player -= 4
                        kagoubutu_name = "ヘリウム He×4"
                    End If

                Case 3
                    If hantei < 3 Then
                        hantei = 3
                        He_count_player -= 3
                        kagoubutu_name = "ヘリウム He×3"
                    End If

                Case 2
                    If hantei < 2 Then
                        hantei = 2
                        He_count_player -= 2
                        kagoubutu_name = "ヘリウム He×2"
                    End If

                Case 1
                    If hantei < 1 Then
                        hantei = 1
                        He_count_player -= 1
                        kagoubutu_name = "ヘリウム He"
                    End If

            End Select

            Select Case Li_count_player

                Case 1
                    If O_count_player >= 1 And H_count_player >= 1 And hantei < 3 Then 'LiOH
                        hantei = 3
                        Li_count_player -= 1
                        O_count_player -= 1
                        H_count_player -= 1
                        kagoubutu_name = "水酸化リチウム LiOH"
                    ElseIf Cl_count_player >= 1 And hantei < 2 Then 'LiCl
                        hantei = 2
                        Li_count_player -= 1
                        Cl_count_player -= 1
                        kagoubutu_name = "塩化リチウム LiCl"
                    ElseIf F_count_player >= 1 And hantei < 2 Then 'LiF
                        hantei = 2
                        Li_count_player -= 1
                        F_count_player -= 1
                        kagoubutu_name = "フッ化リチウム LiF"
                    End If

            End Select

            Select Case Be_count_player

            End Select

            Select Case B_count_player

                Case 1
                    If O_count_player >= 3 And H_count_player >= 3 And hantei < 7 Then 'B(OH)3
                        hantei = 7
                        B_count_player -= 1
                        O_count_player -= 3
                        H_count_player -= 3
                        kagoubutu_name = "ホウ酸 B(OH)3"
                    ElseIf P_count_player >= 1 And O_count_player >= 4 And hantei < 6 Then 'BPO4
                        hantei = 6
                        B_count_player -= 1
                        P_count_player -= 1
                        O_count_player -= 4
                        kagoubutu_name = "リン酸ホウ素 BPO4"
                    ElseIf F_count_player >= 3 And hantei < 4 Then 'BF3
                        hantei = 4
                        B_count_player -= 1
                        F_count_player -= 3
                        kagoubutu_name = "三フッ化ホウ素 BF3"
                    ElseIf N_count_player >= 1 And hantei < 4 Then 'BN
                        hantei = 2
                        B_count_player -= 1
                        N_count_player -= 1
                        kagoubutu_name = "窒化ホウ素 BN"
                    End If

            End Select

            Select Case C_count_player

            End Select

            Select Case N_count_player

                Case 1
                    If Na_count_player >= 1 And O_count_player >= 3 And hantei < 5 Then 'NaNO3
                        hantei = 5
                        N_count_player -= 1
                        Na_count_player -= 1
                        O_count_player -= 3
                        kagoubutu_name = "硝酸ナトリウム NaNO3"
                    ElseIf K_count_player >= 1 And O_count_player >= 3 And hantei < 5 Then 'KNO3
                        hantei = 5
                        N_count_player -= 1
                        K_count_player -= 1
                        O_count_player -= 3
                        kagoubutu_name = "硝酸カリウム KN03"
                    ElseIf O_count_player >= 2 And hantei < 3 Then 'NO2
                        hantei = 3
                        N_count_player -= 1
                        O_count_player -= 2
                        kagoubutu_name = "二酸化窒素 NO2"
                    ElseIf O_count_player >= 1 And hantei < 2 Then 'NO
                        hantei = 2
                        N_count_player -= 1
                        O_count_player -= 1
                        kagoubutu_name = "一酸化窒素 NO"
                    End If

            End Select

            Select Case O_count_player

                Case 3
                    If Na_count_player >= 1 And H_count_player >= 1 And C_count_player >= 1 And hantei < 6 Then 'NaHCO3
                        hantei = 6
                        O_count_player -= 3
                        C_count_player -= 1
                        Na_count_player -= 1
                        H_count_player -= 1
                        kagoubutu_name = "炭酸ナトリウム NaHCO3"
                    ElseIf Mg_count_player >= 1 And C_count_player >= 1 And hantei < 5 Then 'MgCO3
                        hantei = 5
                        O_count_player -= 3
                        C_count_player -= 1
                        Mg_count_player -= 1
                        kagoubutu_name = "炭酸マグネシウム MgCO3"
                    ElseIf Ca_count_player >= 1 And C_count_player >= 1 And hantei < 5 Then 'CaCO3
                        hantei = 5
                        O_count_player -= 3
                        Ca_count_player -= 1
                        C_count_player -= 1
                        kagoubutu_name = "炭酸カルシウム CaCO3"
                    ElseIf Al_count_player >= 2 And hantei < 5 Then 'Al2O3
                        hantei = 5
                        O_count_player -= 3
                        Al_count_player -= 2
                        kagoubutu_name = "酸化アルミニウム Al2O3"
                    ElseIf S_count_player >= 1 And hantei < 4 Then 'SO3
                        hantei = 4
                        O_count_player -= 3
                        S_count_player -= 1
                        kagoubutu_name = "三酸化硫黄 SO3"
                    ElseIf hantei < 3 Then 'O3
                        hantei = 3
                        O_count_player -= 3
                        kagoubutu_name = "オゾン O3"
                    End If

                Case 2
                    If C_count_player >= 1 And hantei < 3 Then 'CO2
                        hantei = 3
                        O_count_player -= 2
                        C_count_player -= 1
                        kagoubutu_name = "二酸化炭素 CO2"
                    ElseIf S_count_player >= 1 And hantei < 3 Then 'SO2
                        hantei = 3
                        O_count_player -= 2
                        S_count_player -= 1
                        kagoubutu_name = "二酸化硫黄 SO2"
                    ElseIf Si_count_player >= 1 And hantei < 3 Then 'SiO2
                        hantei = 3
                        O_count_player -= 2
                        Si_count_player -= 1
                        kagoubutu_name = "二酸化ケイ素 SiO2"
                    ElseIf hantei < 3 Then 'O2
                        hantei = 2
                        O_count_player -= 2
                        kagoubutu_name = "酸素 O2"
                    End If

                Case 1
                    If Na_count_player >= 2 And hantei < 3 Then 'Na2O
                        hantei = 3
                        O_count_player -= 1
                        Na_count_player -= 2
                        kagoubutu_name = "酸化ナトリウム Na2O"
                    ElseIf Mg_count_player >= 1 And hantei < 2 Then 'MgO
                        hantei = 2
                        O_count_player -= 1
                        Mg_count_player -= 1
                        kagoubutu_name = "酸化マグネシウム MgO"
                    ElseIf Ca_count_player >= 1 And hantei < 2 Then 'CaO
                        hantei = 2
                        O_count_player -= 1
                        Ca_count_player -= 1
                        kagoubutu_name = "酸化カルシウム CaO"
                    End If

            End Select

            Select Case F_count_player

                Case 2
                    If Ca_count_player >= 1 And hantei < 3 Then 'CaF2
                        hantei = 3
                        F_count_player -= 2
                        Ca_count_player -= 1
                        kagoubutu_name = "フッ化カルシウム CaF2"
                    ElseIf hantei < 2 Then 'F2
                        hantei = 2
                        F_count_player -= 2
                        kagoubutu_name = "フッ素 F2"
                    End If

                Case 1
                    If Na_count_player >= 1 And hantei < 2 Then 'NaF
                        hantei = 2
                        F_count_player -= 1
                        Na_count_player -= 1
                        kagoubutu_name = "フッ化ナトリウム NaF"
                    End If

            End Select

            Select Case Ne_count_player 'Ne

                Case 7
                    If hantei < 7 Then
                        hantei = 7
                        Ne_count_player -= 7
                        kagoubutu_name = "ネオン Ne×7"
                    End If

                Case 6
                    If hantei < 6 Then
                        hantei = 6
                        Ne_count_player -= 6
                        kagoubutu_name = "ネオン Ne×6"
                    End If

                Case 5
                    If hantei < 5 Then
                        hantei = 5
                        Ne_count_player -= 5
                        kagoubutu_name = "ネオン Ne×5"
                    End If

                Case 4
                    If hantei < 4 Then
                        hantei = 4
                        Ne_count_player -= 4
                        kagoubutu_name = "ネオン Ne×4"
                    End If

                Case 3
                    If hantei < 3 Then
                        hantei = 3
                        Ne_count_player -= 3
                        kagoubutu_name = "ネオン Ne×3"
                    End If

                Case 2
                    If hantei < 2 Then
                        hantei = 2
                        Ne_count_player -= 2
                        kagoubutu_name = "ネオン Ne×2"
                    End If

                Case 1
                    If hantei < 1 Then
                        hantei = 1
                        Ne_count_player -= 1
                        kagoubutu_name = "ネオン Ne"
                    End If

            End Select

            Select Case Na_count_player

                Case 1
                    If Cl_count_player >= 1 And hantei < 2 Then 'NaCl
                        hantei = 2
                        Na_count_player -= 1
                        Cl_count_player -= 1
                        kagoubutu_name = "塩化ナトリウム NaCl"
                    End If

            End Select

            Select Case Mg_count_player

            End Select

            Select Case Al_count_player

            End Select

            Select Case Si_count_player

                Case 1, 2
                    If Cl_count_player >= 3 And hantei < 4 Then 'SiCl3
                        hantei = 4
                        Si_count_player -= 1
                        Cl_count_player -= 3
                        kagoubutu_name = "塩化ケイ素 SiCl3"
                    ElseIf O_count_player >= 1 And hantei < 2 Then 'SiO
                        hantei = 2
                        Si_count_player -= 1
                        O_count_player -= 1
                        kagoubutu_name = "一酸化ケイ素 SiO"
                    ElseIf S_count_player >= 1 And hantei < 2 Then 'SSi
                        hantei = 2
                        Si_count_player -= 1
                        S_count_player -= 1
                        kagoubutu_name = "硫化ケイ素 SSi"
                    End If

            End Select

            Select Case P_count_player

                Case 1
                    If F_count_player >= 3 And hantei < 4 Then 'PF3
                        hantei = 4
                        P_count_player -= 1
                        F_count_player -= 3
                        kagoubutu_name = "三フッ化リン PF3"
                    ElseIf Cl_count_player >= 3 And hantei < 4 Then 'PCl3
                        hantei = 4
                        P_count_player -= 1
                        Cl_count_player -= 3
                        kagoubutu_name = "三塩化リン PCl3"
                    End If


            End Select

            Select Case S_count_player

            End Select

            Select Case Cl_count_player

                Case 1
                    If K_count_player >= 1 And hantei < 2 Then 'KCl
                        hantei = 2
                        Cl_count_player -= 1
                        K_count_player -= 1
                        kagoubutu_name = "塩化カリウム KCl"
                    End If

            End Select

            Select Case Ar_count_player 'Ar

                Case 7
                    If hantei < 7 Then
                        hantei = 7
                        Ar_count_player -= 7
                        kagoubutu_name = "アルゴン Ar×7"
                    End If

                Case 6
                    If hantei < 6 Then
                        hantei = 6
                        Ar_count_player -= 6
                        kagoubutu_name = "アルゴン Ar×6"
                    End If

                Case 5
                    If hantei < 5 Then
                        hantei = 5
                        Ar_count_player -= 5
                        kagoubutu_name = "アルゴン Ar×5"
                    End If

                Case 4
                    If hantei < 4 Then
                        hantei = 4
                        Ar_count_player -= 4
                        kagoubutu_name = "アルゴン Ar×4"
                    End If

                Case 3
                    If hantei < 3 Then
                        hantei = 3
                        Ar_count_player -= 3
                        kagoubutu_name = "アルゴン Ar×3"
                    End If

                Case 2
                    If hantei < 2 Then
                        hantei = 2
                        Ar_count_player -= 2
                        kagoubutu_name = "アルゴン Ar×2"
                    End If

                Case 1
                    If hantei < 1 Then
                        hantei = 1
                        Ar_count_player -= 1
                        kagoubutu_name = "アルゴン Ar"
                    End If

            End Select

            Select Case K_count_player

            End Select

            Select Case Ca_count_player

            End Select

            kagoubutu_name_player = kagoubutu_name

        End If
    End Function

    '勝敗の判定
    Private Function win_lose(e_points, p_points)

        If e_points > p_points Then
            win_lose = "YOU LOSE"
        ElseIf e_points < p_points Then
            win_lose = "YOU WIN"
        Else
            win_lose = "DRAW"
        End If

    End Function

    'カードの交換
    Private Sub cards_changer()

        '敵側のカードロック処理
        For i = 0 To 4
            Select Case enemy_cards(i)

                Case 0
                    Lock_enemy(i) = True

                Case 5
                    Lock_enemy(i) = True

                Case 7
                    Lock_enemy(i) = True

            End Select
        Next

        '乱数を使用してカードを生成
        For i = 0 To 1
            For j = 0 To 4
                Card_Number = r.Next(0, 79)
                If Card_Number >= 76 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 19
                        Ca_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 19
                        Ca_count_player += 1
                    End If

                ElseIf Card_Number >= 72 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 18
                        K_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 18
                        K_count_player += 1
                    End If

                ElseIf Card_Number >= 68 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 17
                        Ar_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 17
                        Ar_count_player += 1
                    End If

                ElseIf Card_Number >= 64 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 16
                        Cl_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 16
                        Cl_count_player += 1
                    End If

                ElseIf Card_Number >= 60 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 15
                        S_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 15
                        S_count_player += 1
                    End If

                ElseIf Card_Number >= 56 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 14
                        P_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 14
                        P_count_player += 1
                    End If

                ElseIf Card_Number >= 52 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 13
                        Si_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 13
                        Si_count_player += 1
                    End If

                ElseIf Card_Number >= 48 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 12
                        Al_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 12
                        Al_count_player += 1
                    End If

                ElseIf Card_Number >= 44 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 11
                        Mg_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 11
                        Mg_count_player += 1
                    End If

                ElseIf Card_Number >= 40 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 10
                        Na_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 10
                        Na_count_player += 1
                    End If

                ElseIf Card_Number >= 36 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 9
                        Ne_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 9
                        Ne_count_player += 1
                    End If

                ElseIf Card_Number >= 32 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 8
                        F_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 8
                        F_count_player += 1
                    End If

                ElseIf Card_Number >= 28 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 7
                        O_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 7
                        O_count_player += 1
                    End If

                ElseIf Card_Number >= 24 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 6
                        N_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 6
                        N_count_player += 1
                    End If

                ElseIf Card_Number >= 20 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 5
                        C_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 5
                        C_count_player += 1
                    End If

                ElseIf Card_Number >= 16 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 4
                        B_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 4
                        B_count_player += 1
                    End If

                ElseIf Card_Number >= 12 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 3
                        Be_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 3
                        Be_count_player += 1
                    End If

                ElseIf Card_Number >= 8 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 2
                        Li_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 2
                        Li_count_player += 1
                    End If

                ElseIf Card_Number >= 4 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 1
                        He_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 1
                        He_count_player += 1
                    End If

                ElseIf Card_Number >= 0 Then
                    If enemy_flg = True And Lock_enemy(j) = False Then
                        element_changer(enemy_cards(j))
                        enemy_cards(j) = 0
                        H_count_enemy += 1
                    ElseIf player_flg = True And Lock_player(j) = False Then
                        element_changer(player_cards(j))
                        player_cards(j) = 0
                        H_count_player += 1
                    End If

                End If
            Next
            enemy_flg = False
            player_flg = True
        Next
        enemy_flg = True
        player_flg = False

        Invalidate()    'フォームコントロール無効化して再描画
    End Sub

    'カード交換に応じた元素数の増減
    Private Sub element_changer(elements)
        Select Case elements

            Case 19
                If enemy_flg = True Then
                    Ca_count_enemy -= 1
                    Ca_count_player += 1
                ElseIf player_flg = True Then
                    Ca_count_enemy += 1
                    Ca_count_player -= 1
                End If

            Case 18
                If enemy_flg = True Then
                    K_count_enemy -= 1
                    K_count_player += 1
                ElseIf player_flg = True Then
                    K_count_enemy += 1
                    K_count_player -= 1
                End If

            Case 17
                If enemy_flg = True Then
                    Ar_count_enemy -= 1
                    Ar_count_player += 1
                ElseIf player_flg = True Then
                    Ar_count_enemy += 1
                    Ar_count_player -= 1
                End If

            Case 16
                If enemy_flg = True Then
                    Cl_count_enemy -= 1
                    Cl_count_player += 1
                ElseIf player_flg = True Then
                    Cl_count_enemy += 1
                    Cl_count_player -= 1
                End If

            Case 15
                If enemy_flg = True Then
                    S_count_enemy -= 1
                    S_count_player += 1
                ElseIf player_flg = True Then
                    S_count_enemy += 1
                    S_count_player -= 1
                End If

            Case 14
                If enemy_flg = True Then
                    P_count_enemy -= 1
                    P_count_player += 1
                ElseIf player_flg = True Then
                    P_count_enemy += 1
                    P_count_player -= 1
                End If

            Case 13
                If enemy_flg = True Then
                    Si_count_enemy -= 1
                    Si_count_player += 1
                ElseIf player_flg = True Then
                    Si_count_enemy += 1
                    Si_count_player -= 1
                End If

            Case 12
                If enemy_flg = True Then
                    Al_count_enemy -= 1
                    Al_count_player += 1
                ElseIf player_flg = True Then
                    Al_count_enemy += 1
                    Al_count_player -= 1
                End If

            Case 11
                If enemy_flg = True Then
                    Mg_count_enemy -= 1
                    Mg_count_player += 1
                ElseIf player_flg = True Then
                    Mg_count_enemy += 1
                    Mg_count_player -= 1
                End If

            Case 10
                If enemy_flg = True Then
                    Na_count_enemy -= 1
                    Na_count_player += 1
                ElseIf player_flg = True Then
                    Na_count_enemy += 1
                    Na_count_player -= 1
                End If

            Case 9
                If enemy_flg = True Then
                    Ne_count_enemy -= 1
                    Ne_count_player += 1
                ElseIf player_flg = True Then
                    Ne_count_enemy += 1
                    Ne_count_player -= 1
                End If

            Case 8
                If enemy_flg = True Then
                    F_count_enemy -= 1
                    F_count_player += 1
                ElseIf player_flg = True Then
                    F_count_enemy += 1
                    F_count_player -= 1
                End If

            Case 7
                If enemy_flg = True Then
                    O_count_enemy -= 1
                    O_count_player += 1
                ElseIf player_flg = True Then
                    O_count_enemy += 1
                    O_count_player -= 1
                End If

            Case 6
                If enemy_flg = True Then
                    N_count_enemy -= 1
                    N_count_player += 1
                ElseIf player_flg = True Then
                    N_count_enemy += 1
                    N_count_player -= 1
                End If

            Case 5
                If enemy_flg = True Then
                    C_count_enemy -= 1
                    C_count_player += 1
                ElseIf player_flg = True Then
                    C_count_enemy += 1
                    C_count_player -= 1
                End If

            Case 4
                If enemy_flg = True Then
                    B_count_enemy -= 1
                    B_count_player += 1
                ElseIf player_flg = True Then
                    B_count_enemy += 1
                    B_count_player -= 1
                End If

            Case 3
                If enemy_flg = True Then
                    Be_count_enemy -= 1
                    Be_count_player += 1
                ElseIf player_flg = True Then
                    Be_count_enemy += 1
                    Be_count_player -= 1
                End If

            Case 2
                If enemy_flg = True Then
                    Li_count_enemy -= 1
                    Li_count_player += 1
                ElseIf player_flg = True Then
                    Li_count_enemy += 1
                    Li_count_player -= 1
                End If

            Case 1
                If enemy_flg = True Then
                    He_count_enemy -= 1
                    He_count_player += 1
                ElseIf player_flg = True Then
                    He_count_enemy += 1
                    He_count_player -= 1
                End If

            Case 0
                If enemy_flg = True Then
                    H_count_enemy -= 1
                    H_count_player += 1
                ElseIf player_flg = True Then
                    H_count_enemy += 1
                    H_count_player -= 1
                End If

        End Select
    End Sub

    'カード表示代入処理
    Private Sub elements_show()

        '文字列初期化
        For i = 0 To 19
            enemy_elements(i) = ""
            player_elements(i) = ""
        Next

        '得点を文字列に変換処理
        enemy_points_str = Str(enemy_points_int)
        player_points_str = Str(player_points_int)
        enemy_points_str = "Enemy_Points=" + enemy_points_str
        player_points_str = "Player_Points=" + player_points_str

        '元素数を配列の代入
        '敵側
        elements_changebox = Str(H_count_enemy)
        enemy_elements(0) += "H=" + elements_changebox
        elements_changebox = Str(He_count_enemy)
        enemy_elements(1) += "He=" + elements_changebox
        elements_changebox = Str(Li_count_enemy)
        enemy_elements(2) += "Li=" + elements_changebox
        elements_changebox = Str(Be_count_enemy)
        enemy_elements(3) += "Be=" + elements_changebox
        elements_changebox = Str(B_count_enemy)
        enemy_elements(4) += "B=" + elements_changebox
        elements_changebox = Str(C_count_enemy)
        enemy_elements(5) += "C=" + elements_changebox
        elements_changebox = Str(N_count_enemy)
        enemy_elements(6) += "N=" + elements_changebox
        elements_changebox = Str(O_count_enemy)
        enemy_elements(7) += "O=" + elements_changebox
        elements_changebox = Str(F_count_enemy)
        enemy_elements(8) += "F=" + elements_changebox
        elements_changebox = Str(Ne_count_enemy)
        enemy_elements(9) += "Ne=" + elements_changebox
        elements_changebox = Str(Na_count_enemy)
        enemy_elements(10) += "Na=" + elements_changebox
        elements_changebox = Str(Mg_count_enemy)
        enemy_elements(11) += "Mg=" + elements_changebox
        elements_changebox = Str(Al_count_enemy)
        enemy_elements(12) += "Al=" + elements_changebox
        elements_changebox = Str(Si_count_enemy)
        enemy_elements(13) += "Si=" + elements_changebox
        elements_changebox = Str(P_count_enemy)
        enemy_elements(14) += "P=" + elements_changebox
        elements_changebox = Str(S_count_enemy)
        enemy_elements(15) += "S=" + elements_changebox
        elements_changebox = Str(Cl_count_enemy)
        enemy_elements(16) += "Cl=" + elements_changebox
        elements_changebox = Str(Ar_count_enemy)
        enemy_elements(17) += "Ar=" + elements_changebox
        elements_changebox = Str(K_count_enemy)
        enemy_elements(18) += "K=" + elements_changebox
        elements_changebox = Str(Ca_count_enemy)
        enemy_elements(19) += "Ca=" + elements_changebox

        'プレイヤー側
        elements_changebox = Str(H_count_player)
        player_elements(0) += "H=" + elements_changebox
        elements_changebox = Str(He_count_player)
        player_elements(1) += "He=" + elements_changebox
        elements_changebox = Str(Li_count_player)
        player_elements(2) += "Li=" + elements_changebox
        elements_changebox = Str(Be_count_player)
        player_elements(3) += "Be=" + elements_changebox
        elements_changebox = Str(B_count_player)
        player_elements(4) += "B=" + elements_changebox
        elements_changebox = Str(C_count_player)
        player_elements(5) += "C=" + elements_changebox
        elements_changebox = Str(N_count_player)
        player_elements(6) += "N=" + elements_changebox
        elements_changebox = Str(O_count_player)
        player_elements(7) += "O=" + elements_changebox
        elements_changebox = Str(F_count_player)
        player_elements(8) += "F=" + elements_changebox
        elements_changebox = Str(Ne_count_player)
        player_elements(9) += "Ne=" + elements_changebox
        elements_changebox = Str(Na_count_player)
        player_elements(10) += "Na=" + elements_changebox
        elements_changebox = Str(Mg_count_player)
        player_elements(11) += "Mg=" + elements_changebox
        elements_changebox = Str(Al_count_player)
        player_elements(12) += "Al=" + elements_changebox
        elements_changebox = Str(Si_count_player)
        player_elements(13) += "Si=" + elements_changebox
        elements_changebox = Str(P_count_player)
        player_elements(14) += "P=" + elements_changebox
        elements_changebox = Str(S_count_player)
        player_elements(15) += "S=" + elements_changebox
        elements_changebox = Str(Cl_count_player)
        player_elements(16) += "Cl=" + elements_changebox
        elements_changebox = Str(Ar_count_player)
        player_elements(17) += "Ar=" + elements_changebox
        elements_changebox = Str(K_count_player)
        player_elements(18) += "K=" + elements_changebox
        elements_changebox = Str(Ca_count_player)
        player_elements(19) += "Ca=" + elements_changebox

    End Sub
End Class