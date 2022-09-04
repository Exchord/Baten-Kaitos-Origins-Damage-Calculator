Public Class MP
    Inherits Form

    Dim label(8), spirit_draw As Label
    Dim class_selector, deviation, knockdown As ComboBox
    Dim reset, yellow, orange As Button
    Public MP As TextBox
    Dim magnus(8), artifact(4), plus_minus(4) As PictureBox
    Dim include_selection As CheckBox
    Public burst As CheckBox
    Public hover As ToolTip
    Public factor, current_MP As Double
    Public current_class, max_MP As Integer
    Dim auto As Boolean

    ReadOnly mp_magnus() As Integer = {427, 428, 439, 440, 441, 87, 162, 187}

    Private Sub MP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Hide()
        Font = New Font("Segoe UI", 9, FontStyle.Regular)
        BackColor = Color.LightGray
        FormBorderStyle = FormBorderStyle.FixedSingle
        DoubleBuffered = True
        KeyPreview = True
        MaximizeBox = False
        Text = "Magnus Power"
        Icon = New Icon(Me.GetType(), "icon.ico")
        Size = New Size(386, 427)
        LoadWindowData()

        For x = 0 To 7
            label(x) = New Label()
            With label(x)
                .BackColor = Main.default_color
                .TextAlign = ContentAlignment.MiddleCenter
            End With
            AddHandler label(x).Click, AddressOf ChangeFocus
        Next


        ' CLASS

        With label(0)
            .Size = New Size(59, 24)
            .Location = New Point(10, 10)
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            .Text = "Class"
        End With

        class_selector = New ComboBox()
        With class_selector
            .Size = New Size(54, 24)
            .Location = New Point(70, 10)
            .MaxLength = 2
            For y = 1 To 30
                .Items.Add(y)
            Next
            AddHandler .KeyPress, AddressOf FilterInput
            AddHandler .TextChanged, AddressOf ChangeClass
            AddHandler .LostFocus, AddressOf FixClass
        End With
        Controls.Add(class_selector)


        ' FACTOR

        With label(1)
            .Size = New Size(59, 24)
            .Location = New Point(10, 35)
            .Text = "Factor"
        End With

        With label(2)
            .Size = New Size(54, 24)
            .Location = New Point(70, 35)
        End With


        ' WINGDASH

        With label(3)
            .Size = New Size(120, 24)
            .Location = New Point(175, 7)
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            .BackColor = Color.Transparent
            .Text = "Wingdash"
        End With

        yellow = New Button()
        With yellow
            .Size = New Size(61, 29)
            .Location = New Point(174, 31)
            .UseVisualStyleBackColor = True
            .Text = "Yellow"
            .Tag = 16
            AddHandler .Click, AddressOf ChangeMP
        End With
        Controls.Add(yellow)

        orange = New Button()
        With orange
            .Size = New Size(61, 29)
            .Location = New Point(235, 31)
            .UseVisualStyleBackColor = True
            .Text = "Orange"
            .Tag = 17
            AddHandler .Click, AddressOf ChangeMP
        End With
        Controls.Add(orange)


        ' MP CONTROL

        reset = New Button()
        With reset
            .Size = New Size(69, 32)
            .Location = New Point(56, 79)
            .UseVisualStyleBackColor = True
            .Font = New Font("Segoe UI", 11, FontStyle.Regular)
            .Text = "Reset"
            .Tag = 18
            AddHandler .Click, AddressOf ChangeMP
        End With
        Controls.Add(reset)

        MP = New TextBox()
        With MP
            .AutoSize = False
            .Size = New Size(90, 30)
            .Font = New Font("Segoe UI", 12, FontStyle.Bold)
            .TextAlign = HorizontalAlignment.Center
            .Location = New Point(125, 80)
            .MaxLength = 7
            AddHandler .TextChanged, AddressOf CustomMP
            AddHandler .MouseWheel, AddressOf ScrollMP
            AddHandler .KeyPress, AddressOf FilterInput
            AddHandler .LostFocus, AddressOf FixMP
        End With
        Controls.Add(MP)

        burst = New CheckBox()
        With burst
            .Size = New Size(104, 30)
            .Location = New Point(216, 80)
            .BackColor = Main.default_color
            .Padding = New Padding(10, 0, 0, 0)
            .Font = New Font("Segoe UI", 11, FontStyle.Regular)
            .Text = "MP burst"
            AddHandler .CheckedChanged, AddressOf ToggleBurst
        End With
        Controls.Add(burst)


        ' MAGNUS

        Dim magnus_y As Integer = 130

        For x = 0 To 7
            magnus(x) = New PictureBox()
            With magnus(x)
                If x < 5 Then
                    .Size = New Size(50, 80)
                    .Location = New Point(35 + 55 * x, magnus_y)
                Else
                    .Size = New Size(40, 64)
                    .Location = New Point(225 + 45 * (x - 5), magnus_y + 137)
                End If
                .Image = New Bitmap(My.Resources.ResourceManager.GetObject("_" & mp_magnus(x)), .Size)
                .Cursor = Cursors.Hand
                .Tag = x
                AddHandler .Click, AddressOf ChangeMP
                AddHandler .MouseEnter, AddressOf ShowName
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(magnus(x))
        Next

        With label(4)
            .Size = New Size(69, 24)
            .Location = New Point(10, magnus_y + 95)
            .Text = "Deviation"
        End With

        deviation = New ComboBox()
        With deviation
            .Size = New Size(51, 24)
            .Location = New Point(80, magnus_y + 95)
            .DropDownStyle = ComboBoxStyle.DropDownList
            For i = 4 To 1 Step -1
                .Items.Add("+" & i & "%")
            Next
            For i = 0 To -10 Step -1
                .Items.Add(i & "%")
            Next
            .SelectedIndex = 7
        End With
        Controls.Add(deviation)

        include_selection = New CheckBox()
        With include_selection
            .Size = New Size(200, 24)
            .Location = New Point(132, magnus_y + 95)
            .BackColor = Main.default_color
            .Padding = New Padding(5, 0, 0, 0)
            .Text = "Include MP from selecting card"
        End With
        Controls.Add(include_selection)


        ' ARTIFACTS

        For x = 0 To 3
            artifact(x) = New PictureBox()
            With artifact(x)
                .Size = New Size(40, 40)
                .Location = New Point(35 + 40 * x, magnus_y + 130)
                '.Image = New Bitmap(My.Resources.ResourceManager.GetObject("artifact" & x + 1), .Size)
                .Image = Main.ChangeOpacity(New Bitmap(My.Resources.ResourceManager.GetObject("artifact" & x + 1), .Size), 0.5)
                .Cursor = Cursors.Hand
                .Tag = x + 8
                AddHandler .Click, AddressOf ChangeMP
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(artifact(x))
        Next


        ' SPIRIT DRAWS & LIGHTNING KNOCKDOWN

        With label(5)
            .Size = New Size(135, 28)
            .Location = New Point(10, magnus_y + 190)
            .Text = "Item / spirit draw"
        End With

        With label(6)
            .Size = New Size(135, 28)
            .Location = New Point(10, magnus_y + 220)
            .Text = "Lightning knockdown"
        End With

        For x = 0 To 3
            plus_minus(x) = New PictureBox
            With plus_minus(x)
                .Size = New Size(28, 28)
                If x < 2 Then
                    .Location = New Point(147, magnus_y + 190 + 30 * x)
                    .Image = New Bitmap(My.Resources.ResourceManager.GetObject("plus"), .Size)
                Else
                    .Location = New Point(177, magnus_y + 190 + 30 * (x - 2))
                    .Image = New Bitmap(My.Resources.ResourceManager.GetObject("minus"), .Size)
                End If
                .Cursor = Cursors.Hand
                .Tag = x + 12
                AddHandler .Click, AddressOf ChangeMP
            End With
            Controls.Add(plus_minus(x))
        Next

        knockdown = New ComboBox()
        With knockdown
            .Size = New Size(64, 28)
            .Location = New Point(209, magnus_y + 222)
            .DropDownStyle = ComboBoxStyle.DropDownList
            For y = 50 To 30 Step -0.5
                If y <> Math.Floor(y) Then
                    .Items.Add(y)
                Else
                    .Items.Add(y & ".0")
                End If
            Next
            .SelectedIndex = 20
        End With
        Controls.Add(knockdown)

        For x = 0 To 6
            Controls.Add(label(x))
        Next

        hover = New ToolTip()
        With hover
            .AutoPopDelay = 5000
            .InitialDelay = 500
            .ReshowDelay = 500
            .Active = My.Settings.MPTooltips
        End With

        AddHandler Click, AddressOf ChangeFocus
        AddHandler Move, AddressOf SaveWindowData
        AddHandler Resize, AddressOf SaveWindowData

        class_selector.SelectedIndex = My.Settings.DeckClass - 1
        current_MP = 0
        Show()
        UpdateUI(True)
    End Sub

    Private Sub LoadWindowData()
        Dim pt As Point = My.Settings.MPWindowLocation
        If pt.X = -1 And pt.Y = -1 Then
            CenterToScreen()
            Return
        End If
        For Each s As Screen In Screen.AllScreens
            If s.Bounds.Contains(pt) Then
                StartPosition = FormStartPosition.Manual
                Location = pt
                Return
            End If
        Next
        CenterToScreen()
    End Sub

    Private Sub SaveWindowData(sender As Object, e As EventArgs)
        If WindowState = FormWindowState.Normal Then
            My.Settings.MPWindowLocation = Location
        End If
    End Sub

    Private Sub ChangeFocus(sender As Object, e As EventArgs)
        If sender.GetType.ToString = "Baten_Kaitos_Origins_Damage_Calculator.MP" Then
            label(0).Focus()
            Return
        End If
        sender.Focus()
    End Sub

    Private Sub UpdateUI(change_text As Boolean)
        If burst.Checked And current_class < 7 Then
            burst.Checked = False
        End If
        current_MP = Math.Max(0, Math.Min(current_MP, max_MP))
        If current_MP = 500 Then
            burst.Enabled = True
        Else
            burst.Checked = False
            burst.Enabled = False
        End If
        For x = 0 To 3
            If current_MP < (x + 1) * 100 Then
                artifact(x).Image = Main.ChangeOpacity(New Bitmap(My.Resources.ResourceManager.GetObject("artifact" & x + 1), artifact(x).Size), 0.5)
            Else
                artifact(x).Image = New Bitmap(My.Resources.ResourceManager.GetObject("artifact" & x + 1), artifact(x).Size)
            End If
        Next
        If change_text Then
            auto = True
            MP.Text = current_MP
            auto = False
        End If
        Main.current_MP = current_MP
        Main.DisplayMP()
        Main.CheckCards()
    End Sub

    Private Sub ChangeClass(sender As Object, e As EventArgs)
        Dim new_class As String = class_selector.Text

        If new_class = "" Then
            class_selector.ForeColor = Color.Red
            Return
        End If
        If Not Main.IsNonNegativeInteger(new_class) Then
            class_selector.SelectedIndex = 0
            Return
        End If
        If new_class < 1 Or new_class > 30 Then
            class_selector.ForeColor = Color.Red
            Return
        End If
        class_selector.ForeColor = Color.Black
        current_class = new_class
        factor = Math.Round(8 + (sender.Text - 1) / 29 * 12, 1)
        label(2).Text = factor

        Select Case current_class
            Case < 3
                max_MP = 200
            Case < 5
                max_MP = 300
            Case < 7
                max_MP = 400
            Case Else
                max_MP = 500
        End Select

        My.Settings.DeckClass = current_class
        UpdateUI(True)
    End Sub

    Private Sub FixClass(sender As Object, e As EventArgs)
        If class_selector.ForeColor = Color.Red Then
            class_selector.SelectedIndex = current_class - 1
            Return
        End If
        'remove leading zero
        Dim value As Integer = class_selector.Text
        class_selector.Text = value
    End Sub

    Private Sub ChangeMP(sender As Object, e As MouseEventArgs)
        If burst.Checked Or e.Button = MouseButtons.Middle Then
            Return
        End If

        Dim sign As Integer = 1
        If e.Button = MouseButtons.Right Then
            sign = -1
        End If
        Dim item_factor() As Integer = {3, 7, 6, 4, 8}
        Dim mp_drain() As Integer = {5, 10, 20}
        Dim action As Integer = sender.Tag

        Select Case action
            Case < 5
                If include_selection.Checked Then
                    current_MP += sign * factor
                End If
                current_MP += sign * factor * item_factor(action) * RandomFactor()
            Case < 8
                current_MP += Math.Truncate(sign * mp_drain(action - 5) * RandomFactor())
            Case < 12
                Dim level As Integer = action - 7                       'artifact
                If sign = -1 OrElse current_MP >= level * 100 Then
                    current_MP += sign * (factor - 100 * level)
                End If
            Case 12
                current_MP += factor                            'spirit draw (+)
            Case 14
                current_MP -= factor                            'spirit draw (-)
            Case 13
                current_MP += knockdown.Text                    'lightning knockdown (+)
            Case 15
                current_MP -= knockdown.Text                    'lightning knockdown (-)
            Case 16
                current_MP = Math.Floor(50 + 250 * (current_class - 1) / 29)        'yellow wingdash
            Case 17
                current_MP = Math.Floor(100 + 400 * (current_class - 1) / 29)       'orange wingdash
            Case 18
                current_MP = 0                                  'reset
        End Select
        UpdateUI(True)
    End Sub

    Private Sub CustomMP(sender As Object, e As EventArgs)
        If auto Then
            Return
        End If
        With MP
            If Not IsNumeric(.Text) OrElse (.Text > max_MP Or .Text < 0) Then
                .ForeColor = Color.Red
                Return
            End If
            current_MP = Round(.Text)
            .ForeColor = Color.Black
        End With
        UpdateUI(False)
    End Sub

    Private Sub ScrollMP(sender As Object, e As MouseEventArgs)
        FixMP(sender, e)
        If e.Delta > 0 Then
            MP.Text = Math.Min(MP.Text + factor, max_MP)
        Else
            MP.Text = Math.Max(0, MP.Text - factor)
        End If
    End Sub

    Private Sub FixMP(sender As Object, e As EventArgs)
        If MP.ForeColor = Color.Red Then
            sender.Text = current_MP
            Return
        End If
        'remove leading zeros
        Dim value As Double = MP.Text
        MP.Text = value
    End Sub

    Private Sub ToggleBurst(sender As Object, e As EventArgs)
        If burst.Checked Then
            Main.burst_active = True
            burst.BackColor = Color.LightYellow
        Else
            Main.burst_active = False
            burst.BackColor = Main.default_color
        End If
        Main.DisplayMP()
    End Sub

    Private Function RandomFactor() As Double
        Select Case deviation.SelectedIndex
            Case -1
                Return 1
            Case Else
                Return 1 + 0.01 * (4 - deviation.SelectedIndex)
        End Select
    End Function

    Private Function Round(input As Double) As Double
        Return Math.Round(Math.Round(input, 12), 3, MidpointRounding.AwayFromZero)
    End Function

    Private Sub ShowName(sender As Object, e As EventArgs)
        hover.SetToolTip(sender, Main.magnus_name(mp_magnus(sender.Tag)))
    End Sub

    Private Sub FilterInput(sender As Object, e As KeyPressEventArgs)
        If IsNumeric(e.KeyChar) Then
            Return
        End If
        Select Case e.KeyChar
            Case ChrW(Keys.Back), ".", ChrW(1), ChrW(3), ChrW(22), ChrW(24), ChrW(26)    'Ctrl+A/C/V/X/Z
                Return
        End Select
        e.Handled = True
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub CloseWindow(sender As Object, e As EventArgs) Handles Me.FormClosing
        Hide()
        Main.burst_active = False
        Main.DisplayMP()
    End Sub
End Class