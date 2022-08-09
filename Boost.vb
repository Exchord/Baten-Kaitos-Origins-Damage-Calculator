Public Class Boost
    Inherits Form

    Public chars(4) As PictureBox
    Dim card(10) As PictureBox
    Dim element(6), label(3) As Label
    Public boost(5, 6, 2) As TextBox
    Dim next_turn(5), reset(5) As Button
    Public hover As ToolTip
    ReadOnly magnus() As Integer = {389, 390, 391, 392, 393, 398, 399, 414, 417, 418}

    Private Sub Boost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Hide()
        Font = New Font("Segoe UI", 9, FontStyle.Regular)
        BackColor = Color.LightGray
        FormBorderStyle = FormBorderStyle.FixedSingle
        KeyPreview = True
        MaximizeBox = False
        Text = "Temporary Boost"
        Icon = New Icon(Me.GetType(), "icon.ico")
        Size = New Size(696, 599)
        LoadWindowData()


        ' PARTY / ENEMY ICONS

        For x = 0 To 3
            chars(x) = New PictureBox()
            With chars(x)
                If x < 3 Then
                    .Size = New Size(70, 70)
                    .Location = New Point(20, 50 + x * 80)
                Else
                    .Size = New Size(70, 84)
                    .Location = New Point(20, 416)
                End If
                .BackColor = Color.Transparent
                .Tag = x
                If x < 3 Then
                    If x = Main.item_target Then
                        .Image = Main.char_icon(x)
                    Else
                        .Image = Main.ChangeOpacity(Main.char_icon(x), 0.5)
                    End If
                Else
                    .Image = New Bitmap(My.Resources.ResourceManager.GetObject("target_" & Main.combo_target), New Size(70, 84))
                End If
            End With
            Controls.Add(chars(x))
            If x < 3 Then
                chars(x).Cursor = Cursors.Hand
                AddHandler chars(x).Click, AddressOf SwitchCharacter
            End If
            AddHandler chars(x).Click, AddressOf ChangeFocus
        Next


        'MAGNUS

        For x = 0 To 9
            card(x) = New PictureBox()
            With card(x)
                .Size = New Size(50, 80)
                .Location = New Point(120 + 70 * x, 290)
                If x = 8 Then
                    .Location = New Point(470, 460)
                ElseIf x = 9 Then
                    .Location = New Point(190, 460)
                End If
                .BackColor = Color.Transparent
                .Cursor = Cursors.Hand
                .Tag = x
            End With
            Controls.Add(card(x))
            AddHandler card(x).Click, AddressOf Add
            AddHandler card(x).MouseEnter, AddressOf ShowName
            AddHandler card(x).Click, AddressOf ChangeFocus
        Next x
        For x = 0 To 9
            card(x).Image = New Bitmap(My.Resources.ResourceManager.GetObject("_" & magnus(x)), New Size(50, 80))
        Next


        ' ELEMENTS

        For x = 0 To 5
            element(x) = New Label()
            With element(x)
                .Size = New Size(69, 24)
                .BackColor = Main.element_color(x)
                .Text = Main.element_name(x)
                .Location = New Point(110 + 70 * x, 10)
                .BorderStyle = BorderStyle.FixedSingle
                .TextAlign = ContentAlignment.MiddleCenter
            End With
            AddHandler element(x).Click, AddressOf ChangeFocus
            Controls.Add(element(x))
        Next


        ' OFFENSE / DEFENSE

        For x = 0 To 2
            label(x) = New Label
            With label(x)
                .Size = New Size(69, 23)
                .Font = New Font("Segoe UI", 9, FontStyle.Bold)
                .TextAlign = ContentAlignment.MiddleCenter
            End With
            AddHandler label(x).Click, AddressOf ChangeFocus
            Controls.Add(label(x))
        Next
        label(0).Location = New Point(20, 10)
        label(1).Location = New Point(20, 391)
        label(2).Location = New Point(20, 501)
        label(0).Text = "Offense"
        label(1).Text = "Defense"
        label(2).Text = "Offense"


        'INPUT BOXES

        For x = 0 To 3
            For y = 0 To 5
                For z = 0 To 1
                    boost(x, y, z) = New TextBox()
                    With boost(x, y, z)
                        If x < 3 Then
                            .Location = New Point(110 + 70 * y, 61 + 80 * x + 25 * z)
                        Else
                            .Location = New Point(110 + 70 * y, 391 + 25 * z)
                        End If
                        .Size = New Size(69, 24)
                        .TextAlign = HorizontalAlignment.Center
                        .MaxLength = 8
                        .Tag = x '* 12 + y * 2 + z
                        .Name = y & z
                    End With
                    Controls.Add(boost(x, y, z))
                    If x < 3 Then
                        boost(x, y, z).Text = Main.offense_boost(x, y, z)
                    Else
                        boost(x, y, z).Text = Main.defense_boost(y, z)
                    End If
                    If boost(x, y, z).Text = "0" Then
                        boost(x, y, z).Font = New Font("Segoe UI", 9, FontStyle.Regular)
                    Else
                        boost(x, y, z).Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    End If
                    AddHandler boost(x, y, z).KeyPress, AddressOf FilterInput
                    AddHandler boost(x, y, z).TextChanged, AddressOf ChangeBoost
                    AddHandler boost(x, y, z).MouseWheel, AddressOf ScrollBoost
                    AddHandler boost(x, y, z).LostFocus, AddressOf FixBoost
                Next
            Next
        Next
        For x = 0 To 1
            boost(4, 0, x) = New TextBox()
            With boost(4, 0, x)
                .Location = New Point(110, 476 + 25 * x)
                .Size = New Size(69, 24)
                .TextAlign = HorizontalAlignment.Center
                .MaxLength = 8
                .Tag = 4
                .Name = 0 & x
            End With
            Controls.Add(boost(4, 0, x))
            boost(4, 0, x).Text = Main.enemy_offense_boost(x)
            If boost(4, 0, x).Text = "0" Then
                boost(4, 0, x).Font = New Font("Segoe UI", 9, FontStyle.Regular)
            Else
                boost(4, 0, x).Font = New Font("Segoe UI", 9, FontStyle.Bold)
            End If
            AddHandler boost(4, 0, x).KeyPress, AddressOf FilterInput
            AddHandler boost(4, 0, x).TextChanged, AddressOf ChangeBoost
            AddHandler boost(4, 0, x).MouseWheel, AddressOf ScrollBoost
            AddHandler boost(4, 0, x).LostFocus, AddressOf FixBoost
        Next


        'BUTTONS

        For x = 0 To 4
            next_turn(x) = New Button()
            reset(x) = New Button()
            With next_turn(x)
                If x < 3 Then
                    .Location = New Point(548, 60 + x * 80)
                ElseIf x = 3 Then
                    .Location = New Point(548, 390)
                ElseIf x = 4 Then
                    .Location = New Point(260, 476)
                End If
                .Size = New Size(90, 25)
                .UseVisualStyleBackColor = True
                .Text = "Next turn"
                .Tag = x
            End With
            With reset(x)
                If x < 3 Then
                    .Location = New Point(548, 85 + x * 80)
                ElseIf x = 3 Then
                    .Location = New Point(548, 415)
                ElseIf x = 4 Then
                    .Location = New Point(260, 501)
                End If
                .Size = New Size(90, 25)
                .UseVisualStyleBackColor = True
                .Text = "Reset"
                .Tag = x
            End With
            Controls.Add(next_turn(x))
            Controls.Add(reset(x))
            AddHandler next_turn(x).Click, AddressOf NextTurn
            AddHandler reset(x).Click, AddressOf ResetBoost
        Next

        hover = New ToolTip()
        With hover
            .AutoPopDelay = 5000
            .InitialDelay = 500
            .ReshowDelay = 500
            .Active = My.Settings.ItemTooltips
        End With

        AddHandler Move, AddressOf SaveWindowData
        AddHandler Click, AddressOf ChangeFocus
        Show()
        label(0).Focus()
    End Sub

    Private Sub LoadWindowData()
        Dim pt As Point = My.Settings.BoostWindowLocation
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
            My.Settings.BoostWindowLocation = Location
        End If
    End Sub

    Private Sub ChangeFocus(sender As Object, e As EventArgs)
        If sender.GetType.ToString = "Baten_Kaitos_Origins_Damage_Calculator.Boost" Then
            label(0).Focus()
            Return
        End If
        sender.Focus()
    End Sub

    Private Sub Add(sender As Object, e As MouseEventArgs)
        Dim result As Single
        If sender.Tag < 8 Then
            For x = 0 To 5
                For y = 0 To 1
                    result = Round(Main.offense_boost(Main.item_target, x, y) + Main.boost_data(sender.Tag, x) * 0.01)
                    If result > 1000 Or result < -1000 Then
                        Continue For
                    End If
                    Main.offense_boost(Main.item_target, x, y) = result
                    RemoveHandler boost(Main.item_target, x, y).TextChanged, AddressOf ChangeBoost
                    boost(Main.item_target, x, y).Text = Main.offense_boost(Main.item_target, x, y)
                    If boost(Main.item_target, x, y).Text = "0" Then
                        boost(Main.item_target, x, y).Font = New Font("Segoe UI", 9, FontStyle.Regular)
                    Else
                        boost(Main.item_target, x, y).Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    End If
                    AddHandler boost(Main.item_target, x, y).TextChanged, AddressOf ChangeBoost
                Next
            Next
        ElseIf sender.Tag = 8 Then
            For x = 0 To 5
                For y = 0 To 1
                    result = Round(Main.defense_boost(x, y) - Main.boost_data(sender.Tag, x) * 0.01)
                    If result > 1000 Or result < -1000 Then
                        Continue For
                    End If
                    Main.defense_boost(x, y) = result
                    RemoveHandler boost(3, x, y).TextChanged, AddressOf ChangeBoost
                    boost(3, x, y).Text = Main.defense_boost(x, y)
                    If boost(3, x, y).Text = "0" Then
                        boost(3, x, y).Font = New Font("Segoe UI", 9, FontStyle.Regular)
                    Else
                        boost(3, x, y).Font = New Font("Segoe UI", 9, FontStyle.Bold)
                    End If
                    AddHandler boost(3, x, y).TextChanged, AddressOf ChangeBoost
                Next
            Next
        ElseIf sender.Tag = 9 Then
            For x = 0 To 1
                result = Round(Main.enemy_offense_boost(x) - Main.boost_data(sender.Tag, x) * 0.01)
                If result > 1000 Or result < -1000 Then
                    Continue For
                End If
                Main.enemy_offense_boost(x) = result
                RemoveHandler boost(4, 0, x).TextChanged, AddressOf ChangeBoost
                boost(4, 0, x).Text = Main.enemy_offense_boost(x)
                If boost(4, 0, x).Text = "0" Then
                    boost(4, 0, x).Font = New Font("Segoe UI", 9, FontStyle.Regular)
                Else
                    boost(4, 0, x).Font = New Font("Segoe UI", 9, FontStyle.Bold)
                End If
                AddHandler boost(4, 0, x).TextChanged, AddressOf ChangeBoost
            Next
        End If
        Main.Calculate()
    End Sub

    Private Sub SwitchCharacter(sender As Object, e As MouseEventArgs)
        Main.item_target = sender.Tag
        chars(sender.Tag).Image = Main.char_icon(sender.Tag)
        For x = 0 To 2
            If x <> sender.Tag Then
                chars(x).Image = Main.ChangeOpacity(Main.char_icon(x), 0.5)
            End If
        Next
    End Sub

    Private Sub NextTurn(sender As Object, e As EventArgs)
        If sender.Tag = 4 Then
            Main.enemy_offense_boost(0) = Main.enemy_offense_boost(1)
            Main.enemy_offense_boost(1) = 0
            RemoveHandler boost(4, 0, 0).TextChanged, AddressOf ChangeBoost
            RemoveHandler boost(4, 0, 1).TextChanged, AddressOf ChangeBoost
            boost(4, 0, 0).Text = boost(4, 0, 1).Text
            boost(4, 0, 1).Text = "0"
            If boost(4, 0, 0).Text = "0" Then
                boost(4, 0, 0).Font = New Font("Segoe UI", 9, FontStyle.Regular)
            Else
                boost(4, 0, 0).Font = New Font("Segoe UI", 9, FontStyle.Bold)
            End If
            boost(4, 0, 1).Font = New Font("Segoe UI", 9, FontStyle.Regular)
            AddHandler boost(4, 0, 0).TextChanged, AddressOf ChangeBoost
            AddHandler boost(4, 0, 1).TextChanged, AddressOf ChangeBoost
        Else
            For x = 0 To 5
                If sender.Tag = 3 Then
                    Main.defense_boost(x, 0) = Main.defense_boost(x, 1)
                    Main.defense_boost(x, 1) = 0
                Else
                    Main.offense_boost(sender.Tag, x, 0) = Main.offense_boost(sender.Tag, x, 1)
                    Main.offense_boost(sender.Tag, x, 1) = 0
                End If
                RemoveHandler boost(sender.Tag, x, 0).TextChanged, AddressOf ChangeBoost
                RemoveHandler boost(sender.Tag, x, 1).TextChanged, AddressOf ChangeBoost
                boost(sender.Tag, x, 0).Text = boost(sender.Tag, x, 1).Text
                boost(sender.Tag, x, 1).Text = "0"
                If boost(sender.Tag, x, 0).Text = "0" Then
                    boost(sender.Tag, x, 0).Font = New Font("Segoe UI", 9, FontStyle.Regular)
                Else
                    boost(sender.Tag, x, 0).Font = New Font("Segoe UI", 9, FontStyle.Bold)
                End If
                boost(sender.Tag, x, 1).Font = New Font("Segoe UI", 9, FontStyle.Regular)
                AddHandler boost(sender.Tag, x, 0).TextChanged, AddressOf ChangeBoost
                AddHandler boost(sender.Tag, x, 1).TextChanged, AddressOf ChangeBoost
            Next
        End If
        Main.Calculate()
    End Sub

    Private Sub ResetBoost(sender As Object, e As EventArgs)
        If sender.Tag = 4 Then
            Main.enemy_offense_boost(0) = 0
            Main.enemy_offense_boost(1) = 0
            RemoveHandler boost(4, 0, 0).TextChanged, AddressOf ChangeBoost
            RemoveHandler boost(4, 0, 1).TextChanged, AddressOf ChangeBoost
            boost(4, 0, 0).Text = "0"
            boost(4, 0, 1).Text = "0"
            boost(4, 0, 0).Font = New Font("Segoe UI", 9, FontStyle.Regular)
            boost(4, 0, 1).Font = New Font("Segoe UI", 9, FontStyle.Regular)
            AddHandler boost(4, 0, 0).TextChanged, AddressOf ChangeBoost
            AddHandler boost(4, 0, 1).TextChanged, AddressOf ChangeBoost
        Else
            For x = 0 To 5
                If sender.Tag = 3 Then
                    Main.defense_boost(x, 0) = 0
                    Main.defense_boost(x, 1) = 0
                Else
                    Main.offense_boost(sender.Tag, x, 0) = 0
                    Main.offense_boost(sender.Tag, x, 1) = 0
                End If
                RemoveHandler boost(sender.Tag, x, 0).TextChanged, AddressOf ChangeBoost
                RemoveHandler boost(sender.Tag, x, 1).TextChanged, AddressOf ChangeBoost
                boost(sender.Tag, x, 0).Text = "0"
                boost(sender.Tag, x, 1).Text = "0"
                boost(sender.Tag, x, 0).Font = New Font("Segoe UI", 9, FontStyle.Regular)
                boost(sender.Tag, x, 1).Font = New Font("Segoe UI", 9, FontStyle.Regular)
                AddHandler boost(sender.Tag, x, 0).TextChanged, AddressOf ChangeBoost
                AddHandler boost(sender.Tag, x, 1).TextChanged, AddressOf ChangeBoost
            Next
        End If
        Main.Calculate()
    End Sub

    Private Sub ShowName(sender As Object, e As EventArgs)
        hover.SetToolTip(sender, Main.boost_magnus(sender.Tag))
    End Sub

    Private Sub FilterInput(sender As Object, e As KeyPressEventArgs)
        If IsNumeric(e.KeyChar) Then
            Return
        End If
        Select Case e.KeyChar
            Case ChrW(Keys.Back), "-", ".", ChrW(1), ChrW(3), ChrW(22), ChrW(24), ChrW(26)    'Ctrl+A/C/V/X/Z
                Return
        End Select
        e.Handled = True
    End Sub

    Private Sub ChangeBoost(sender As Object, e As EventArgs)
        Dim x, y, z As Integer
        x = sender.Tag
        y = sender.Name.ToString.ElementAt(0).ToString
        z = sender.Name.ToString.ElementAt(1).ToString
        Dim new_value As Single
        If Not IsNumeric(boost(x, y, z).Text) Then
            boost(x, y, z).Font = New Font("Segoe UI", 9, FontStyle.Regular)
            boost(x, y, z).ForeColor = Color.Red
            Return
        End If
        If boost(x, y, z).Text > 1000 Or boost(x, y, z).Text < -1000 Then
            boost(x, y, z).Font = New Font("Segoe UI", 9, FontStyle.Regular)
            boost(x, y, z).ForeColor = Color.Red
            Return
        End If
        new_value = Round(boost(x, y, z).Text)
        If new_value <> 0 Then
            boost(x, y, z).Font = New Font("Segoe UI", 9, FontStyle.Bold)
        Else
            boost(x, y, z).Font = New Font("Segoe UI", 9, FontStyle.Regular)
        End If
        If x < 3 Then
            Main.offense_boost(x, y, z) = new_value
        ElseIf x = 3 Then
            Main.defense_boost(y, z) = new_value
        Else
            Main.enemy_offense_boost(z) = new_value
        End If
        boost(x, y, z).ForeColor = Color.Black
        Main.Calculate()
    End Sub

    Private Sub ScrollBoost(sender As Object, e As MouseEventArgs)
        FixBoost(sender, e)
        Dim x, y As Integer
        x = sender.Tag
        y = sender.Name.ToString.ElementAt(0).ToString
        If e.Delta > 0 Then
            If boost(x, y, 0).Text <= 999 Then
                boost(x, y, 0).Text += 1
            Else
                boost(x, y, 0).Text = 1000
            End If
            If boost(x, y, 1).Text <= 999 Then
                boost(x, y, 1).Text += 1
            Else
                boost(x, y, 1).Text = 1000
            End If
        Else
            If boost(x, y, 0).Text >= -999 Then
                boost(x, y, 0).Text -= 1
            Else
                boost(x, y, 0).Text = -1000
            End If
            If boost(x, y, 1).Text >= -999 Then
                boost(x, y, 1).Text -= 1
            Else
                boost(x, y, 1).Text = -1000
            End If
        End If
    End Sub

    Private Sub FixBoost(sender As Object, e As EventArgs)
        If sender.ForeColor = Color.Red Then
            sender.Text = "0"
            Return
        End If
        Dim value As Single = Round(sender.Text)
        sender.Text = value
    End Sub

    Private Function Round(input As Single) As Single
        Return Math.Round(input, 3, MidpointRounding.AwayFromZero)
    End Function

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub
End Class