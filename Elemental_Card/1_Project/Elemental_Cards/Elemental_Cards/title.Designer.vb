<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class title
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("メイリオ", 15.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(176, 200)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 31)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "→"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("メイリオ", 15.25!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(216, 200)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(157, 31)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "GAME START"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("メイリオ", 15.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(176, 264)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 31)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "→"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("メイリオ", 15.25!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(216, 264)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(140, 31)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "ルールブック"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("メイリオ", 15.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(176, 328)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 31)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "→"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("メイリオ", 15.25!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(216, 328)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 31)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "終了"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("メイリオ", 25.25!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(128, 24)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(312, 51)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Elemental Cards"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("MS UI Gothic", 15.0!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(168, 128)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(220, 20)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "スペースキーを押すと決定"
        '
        'title
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(584, 561)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "title"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "タイトル"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
End Class
