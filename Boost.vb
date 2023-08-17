Public Class Boost
    Inherits Form

    Public character(4) As PictureBox
    Dim card(10) As PictureBox
    Dim element(6), label(3) As Label
    Public boost(5, 6, 2) As TextBox
    Dim next_turn(5), reset(5), reset_all As Button
    Public hover As ToolTip
    Dim auto As Boolean

    ReadOnly offense_magnus() As Integer = {389, 390, 391, 392, 393, 398, 399, 414, 417, 418}
    ReadOnly offense_data(,) As Integer = {
              {100, 0, 0, 0, 0, 0, 0} _             'Brawn-Brewed Tea
            , {0, 100, 0, 0, 0, 0, 0} _             'Fire-Brewed Tea
            , {0, 0, 100, 0, 0, 0, 0} _             'Ice-Brewed Tea
            , {0, 0, 0, 100, 0, 0, 0} _             'Lightning-Brewed Tea
            , {100, 100, 100, 100, 100, 100, 0} _   'Elbow Grease Tea
            , {-50, -50, -50, -50, -50, -50, 0} _   'Rainbow Fruit
            , {150, 150, 150, 150, 150, 150, 0} _   'Berserker Drink
            , {30, 30, 30, 30, 30, 30, 0} _         'Hero's Crest
            , {20, 20, 20, 20, 20, 20, 1} _         'The Beast's Collar
            , {20, 20, 20, 20, 20, 20, 2}}          'The Beast's Shackles

    ReadOnly defense_magnus() As Integer = {394, 395, 396, 397, 398, 393, 399, 415, 417, 418}
    ReadOnly defense_data(,) As Integer = {
              {100, 0, 0, 0, 0, 0, 0} _             'Brawn Fruit
            , {0, 100, 0, 0, 0, 0, 0} _             'Fire Fruit
            , {0, 0, 100, 0, 0, 0, 0} _             'Ice Fruit
            , {0, 0, 0, 100, 0, 0, 0} _             'Lightning Fruit
            , {100, 100, 100, 100, 100, 100, 0} _   'Rainbow Fruit
            , {-50, -50, -50, -50, -50, -50, 0} _   'Elbow Grease Tea
            , {-90, -90, -90, -90, -90, -90, 0} _   'Berserker Drink
            , {30, 30, 30, 30, 30, 30, 0} _         'Emperor's Crest
            , {20, 20, 20, 20, 20, 20, 1} _         'The Beast's Collar
            , {20, 20, 20, 20, 20, 20, 2}}          'The Beast's Shackles

    Private Sub Open() Handles MyBase.Load
        Hide()
        Font = New Font("Segoe UI", 9, FontStyle.Regular)
        BackColor = Color.LightGray
        FormBorderStyle = FormBorderStyle.FixedSingle
        DoubleBuffered = True
        KeyPreview = True
        MaximizeBox = False
        Text = "Temporary Boost"
        Icon = New Icon(Me.GetType(), "icon.ico")
        Size = New Size(696, 679)
        LoadWindowData()


        ' PARTY / ENEMY ICONS

        For x = 0 To 3
            Dim top As Integer = My.Settings.PartyOrder.IndexOf(x) * 80
            character(x) = New PictureBox()
            With character(x)
                If x < 3 Then
                    .Size = New Size(70, 70)
                    .Location = New Point(20, 50 + top)
                    If x = Main.item_target Then
                        .Image = Main.char_icon(x)
                    Else
                        .Image = Main.MakeTransparent(Main.char_icon(x))
                    End If
                    .Cursor = Cursors.Hand
                    AddHandler .Click, AddressOf SwitchCharacter
                Else
                    .Size = New Size(70, 84)
                    .Location = New Point(20, 413)
                    .Image = New Bitmap(My.Resources.ResourceManager.GetObject("target_" & Main.combo_target), .Size)
                End If
                .Tag = x
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(character(x))
        Next


        ' OFFENSE / DEFENSE

        For x = 0 To 2
            label(x) = New Label
            With label(x)
                .Size = New Size(69, 23)
                .Font = New Font("Segoe UI", 9, FontStyle.Bold)
                .TextAlign = ContentAlignment.MiddleCenter
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(label(x))
        Next
        label(0).Location = New Point(20, 10)
        label(1).Location = New Point(20, 388)
        label(2).Location = New Point(20, 498)
        label(1).Text = "Defense"
        label(2).Text = "Offense"


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
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(element(x))
        Next


        ' TEXT BOXES

        For x = 0 To 4
            Dim top As Integer = My.Settings.PartyOrder.IndexOf(x) * 80
            For y = 0 To 5
                For z = 0 To 1
                    boost(x, y, z) = New TextBox()
                    With boost(x, y, z)
                        .Size = New Size(69, 24)
                        .TextAlign = HorizontalAlignment.Center
                        .MaxLength = 8
                        Select Case x
                            Case < 3
                                .Location = New Point(110 + 70 * y, 61 + top + 25 * z)
                            Case 3
                                .Location = New Point(110 + 70 * y, 391 + 25 * z)
                            Case 4
                                .Location = New Point(110 + 70 * y, 471 + 25 * z)
                        End Select
                        .Tag = x
                        .Name = y & z
                        AddHandler .KeyPress, AddressOf FilterInput
                        AddHandler .TextChanged, AddressOf CheckBoost
                        AddHandler .MouseWheel, AddressOf ScrollBoost
                        AddHandler .LostFocus, AddressOf FixBoost
                    End With
                    Controls.Add(boost(x, y, z))
                Next
            Next
        Next


        ' MAGNUS

        For x = 0 To 9
            card(x) = New PictureBox()
            With card(x)
                .Size = New Size(50, 80)
                If x < 8 Then
                    .Location = New Point(120 + 70 * x, 290)
                Else
                    .Location = New Point(400 + (x - 8) * 70, 540)
                End If
                .Cursor = Cursors.Hand
                .Tag = x
                AddHandler .Click, AddressOf Add
                AddHandler .MouseEnter, AddressOf ShowName
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(card(x))
        Next


        ' BUTTONS

        For x = 0 To 4
            Dim top As Integer = My.Settings.PartyOrder.IndexOf(x) * 80
            next_turn(x) = New Button()
            With next_turn(x)
                Select Case x
                    Case < 3
                        .Location = New Point(548, 60 + top)
                    Case 3
                        .Location = New Point(548, 390)
                    Case 4
                        .Location = New Point(548, 470)
                End Select
                .Size = New Size(90, 25)
                .UseVisualStyleBackColor = True
                .Text = "Next turn"
                .Tag = x
                AddHandler .Click, AddressOf NextTurn
            End With
            Controls.Add(next_turn(x))

            reset(x) = New Button()
            With reset(x)
                Select Case x
                    Case < 3
                        .Location = New Point(548, 85 + top)
                    Case 3
                        .Location = New Point(548, 415)
                    Case 4
                        .Location = New Point(548, 495)
                End Select
                .Size = New Size(90, 25)
                .UseVisualStyleBackColor = True
                .Text = "Reset"
                .Tag = x
                AddHandler .Click, AddressOf ResetBoost
            End With
            Controls.Add(reset(x))
        Next

        reset_all = New Button()
        With reset_all
            .Location = New Point(548, 574)
            .Size = New Size(90, 32)
            .UseVisualStyleBackColor = True
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            .Text = "Reset all"
            AddHandler .Click, AddressOf ResetAll
        End With
        Controls.Add(reset_all)

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
        SwitchMode()
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
                Location = pt
                Return
            End If
        Next
        CenterToScreen()
    End Sub

    Private Sub SaveWindowData()
        If WindowState = FormWindowState.Normal Then
            My.Settings.BoostWindowLocation = Location
        End If
    End Sub

    Public Sub SwitchMode()
        Dim magnus() As Integer
        If Not Main.enemy_mode Then
            magnus = offense_magnus
            label(0).Text = "Offense"
        Else
            magnus = defense_magnus
            label(0).Text = "Defense"
        End If
        For x = 0 To 9
            card(x).Image = New Bitmap(My.Resources.ResourceManager.GetObject("_" & magnus(x)), New Size(50, 80))
            card(x).Name = magnus(x)
        Next
        For x = 0 To 4
            For y = 0 To 5
                For z = 0 To 1
                    Select Case x
                        Case < 3
                            If Not Main.enemy_mode Then
                                ChangeBox(boost(x, y, z), Main.offense_boost(x, y, z))
                            Else
                                ChangeBox(boost(x, y, z), Main.defense_boost(x, y, z))
                            End If
                        Case 3
                            ChangeBox(boost(x, y, z), Main.enemy_defense_boost(y, z))
                        Case 4
                            ChangeBox(boost(x, y, z), Main.enemy_offense_boost(y, z))
                    End Select
                Next
            Next
        Next
    End Sub

    Private Sub ChangeFocus(sender As Object, e As EventArgs)
        If sender Is Me Then
            label(0).Focus()
            Return
        End If
        sender.Focus()
    End Sub

    Private Sub SwitchCharacter(sender As Object, e As EventArgs)
        Dim new_char As Integer = sender.Tag
        Main.item_target = new_char
        character(new_char).Image = Main.char_icon(new_char)
        For x = 0 To 2
            If x <> new_char Then
                character(x).Image = Main.MakeTransparent(Main.char_icon(x))
            End If
        Next
    End Sub

    Private Sub Add(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Middle Then
            Return
        End If

        Dim magnus As Integer = sender.Tag
        Dim result As Double
        Dim sign As Integer = 1
        If e.Button = MouseButtons.Right Then
            sign = -1
        End If

        If magnus < 8 Then
            Dim x As Integer = Main.item_target
            If Not Main.enemy_mode Then
                For y = 0 To 5                          'party offense
                    For z = 0 To 1
                        result = Main.offense_boost(x, y, z) + sign * offense_data(magnus, y) * 0.01
                        result = LimitBoost(result)
                        result = Main.Round(result)
                        Main.offense_boost(x, y, z) = result
                        ChangeBox(boost(x, y, z), result)
                    Next
                Next
            Else
                For y = 0 To 5                          'party defense
                    For z = 0 To 1
                        result = Main.defense_boost(x, y, z) + sign * defense_data(magnus, y) * 0.01
                        result = LimitBoost(result)
                        result = Main.Round(result)
                        Main.defense_boost(x, y, z) = result
                        ChangeBox(boost(x, y, z), result)
                    Next
                Next
            End If
        ElseIf magnus = 8 Then                          'enemy defense
            For y = 0 To 5
                For z = 0 To 1
                    result = Main.enemy_defense_boost(y, z) - sign * offense_data(magnus, y) * 0.01
                    result = LimitBoost(result)
                    result = Main.Round(result)
                    Main.enemy_defense_boost(y, z) = result
                    ChangeBox(boost(3, y, z), result)
                Next
            Next
        ElseIf magnus = 9 Then                          'enemy offense
            For y = 0 To 5
                For z = 0 To 1
                    result = Main.enemy_offense_boost(y, z) - sign * offense_data(magnus, z) * 0.01
                    result = LimitBoost(result)
                    result = Main.Round(result)
                    Main.enemy_offense_boost(y, z) = result
                    ChangeBox(boost(4, y, z), result)
                Next
            Next
        End If
        Main.Calculate()
    End Sub

    Private Sub NextTurn(sender As Object, e As EventArgs)
        Dim x As Integer = sender.Tag
        For y = 0 To 5
            Select Case x
                Case < 3
                    If Not Main.enemy_mode Then
                        Main.offense_boost(x, y, 0) = Main.offense_boost(x, y, 1)
                        Main.offense_boost(x, y, 1) = 0
                    Else
                        Main.defense_boost(x, y, 0) = Main.defense_boost(x, y, 1)
                        Main.defense_boost(x, y, 1) = 0
                    End If
                Case 3
                    Main.enemy_defense_boost(y, 0) = Main.enemy_defense_boost(y, 1)
                    Main.enemy_defense_boost(y, 1) = 0
                Case 4
                    Main.enemy_offense_boost(y, 0) = Main.enemy_offense_boost(y, 1)
                    Main.enemy_offense_boost(y, 1) = 0
            End Select

            ChangeBox(boost(x, y, 0), boost(x, y, 1).Text)
            ChangeBox(boost(x, y, 1), 0)
        Next
        Main.Calculate()
    End Sub

    Private Sub ResetBoost(sender As Object, e As EventArgs)
        Dim x As Integer = sender.Tag
        For y = 0 To 5
            Select Case x
                Case < 3
                    If Not Main.enemy_mode Then
                        Main.offense_boost(x, y, 0) = 0
                        Main.offense_boost(x, y, 1) = 0
                    Else
                        Main.defense_boost(x, y, 0) = 0
                        Main.defense_boost(x, y, 1) = 0
                    End If
                Case 3
                    Main.enemy_defense_boost(y, 0) = 0
                    Main.enemy_defense_boost(y, 1) = 0
                Case 4
                    Main.enemy_offense_boost(y, 0) = 0
                    Main.enemy_offense_boost(y, 1) = 0
            End Select

            ChangeBox(boost(x, y, 0), 0)
            ChangeBox(boost(x, y, 1), 0)
        Next
        Main.Calculate()
    End Sub

    Private Sub ResetAll()
        For x = 0 To 4
            ResetBoost(reset(x), New EventArgs)
        Next
    End Sub

    Private Sub ChangeBox(box As Control, new_value As String)
        auto = True
        box.Text = new_value
        If box.Text = "0" Then
            box.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        Else
            box.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        End If
        auto = False
    End Sub

    Private Sub CheckBoost(sender As Object, e As EventArgs)
        If auto Then
            Return
        End If

        Dim x, y, z As Integer
        x = sender.Tag
        y = sender.Name.SubString(0, 1)
        z = sender.Name.SubString(1, 1)

        With boost(x, y, z)
            Dim new_value As String = .Text
            If new_value = "" Or new_value = "-" Or new_value = "." Or new_value = "-." Then
                .ForeColor = Color.Red
                ChangeBoost(x, y, z, 0)
                Return
            End If
            If Not IsNumeric(new_value) Then
                .Text = "0"
                Return
            End If
            If new_value < -1000 Or new_value > 1000 Then
                .ForeColor = Color.Red
            Else
                .ForeColor = Color.Black
            End If
            ChangeBoost(x, y, z, new_value)
        End With
    End Sub

    Private Sub ChangeBoost(x As Integer, y As Integer, z As Integer, value As Double)
        value = Main.Round(value)
        value = Main.Clamp(value, -1000, 1000)
        If value = 0 Then
            boost(x, y, z).Font = New Font("Segoe UI", 9, FontStyle.Regular)
        Else
            boost(x, y, z).Font = New Font("Segoe UI", 9, FontStyle.Bold)
        End If
        Select Case x
            Case < 3
                If Not Main.enemy_mode Then
                    Main.offense_boost(x, y, z) = value
                Else
                    Main.defense_boost(x, y, z) = value
                End If
            Case 3
                Main.enemy_defense_boost(y, z) = value
            Case 4
                Main.enemy_offense_boost(y, z) = value
        End Select
        Main.Calculate()
    End Sub

    Private Sub ScrollBoost(sender As Object, e As MouseEventArgs)
        FixBoost(sender, e)
        Dim x, y As Integer
        x = sender.Tag
        y = sender.Name.SubString(0, 1)

        For Each box As Control In {boost(x, y, 0), boost(x, y, 1)}
            If e.Delta > 0 Then
                box.Text = Math.Min(box.Text + 1, 1000)
            Else
                box.Text = Math.Max(-1000, box.Text - 1)
            End If
        Next
    End Sub

    Private Sub FixBoost(sender As Object, e As EventArgs)          'called when text box loses focus
        Dim boost As String = sender.Text
        If sender.ForeColor = Color.Red Then
            If IsNumeric(boost) Then
                sender.Text = LimitBoost(boost)
            Else
                sender.Text = "0"
            End If
            Return
        End If
        sender.Text = Main.Round(boost)
    End Sub

    Private Function LimitBoost(input As Double) As Double
        Return Main.Clamp(input, -1000, 1000)
    End Function

    Private Sub FilterInput(sender As Object, e As KeyPressEventArgs)
        If IsNumeric(e.KeyChar) Then                                                            'allow numbers
            Return
        End If
        Dim text As String = sender.Text
        Dim selection As String = sender.SelectedText
        If e.KeyChar = "-" And sender.SelectionStart = 0 Then                                   'allow only one sign at the start
            If Not text.Contains("-") OrElse selection.Contains("-") Then
                Return
            End If
        End If
        If e.KeyChar = "." Then                                                                 'allow only one decimal separator, and only after the sign
            If Not text.Contains(".") OrElse selection.Contains(".") Then
                If sender.SelectionStart > text.IndexOf("-") OrElse selection.Contains("-") Then
                    Return
                End If
            End If
        End If
        Select Case e.KeyChar
            Case ChrW(Keys.Back), ChrW(1), ChrW(3), ChrW(22), ChrW(24), ChrW(26)                'allow backspace and Ctrl+A/C/V/X/Z
                Return
        End Select
        e.Handled = True                                                                        'block everything else
    End Sub

    Public Sub ChangePartyOrder()
        For x = 0 To 2
            Dim top As Integer = My.Settings.PartyOrder.IndexOf(x) * 80
            character(x).Top = 50 + top
            For y = 0 To 5
                boost(x, y, 0).Top = 61 + top
                boost(x, y, 1).Top = 86 + top
            Next
            next_turn(x).Top = 60 + top
            reset(x).Top = 85 + top
        Next
    End Sub

    Private Sub ShowName(sender As Object, e As EventArgs)
        hover.SetToolTip(sender, Main.magnus_name(sender.Name))
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If TypeOf ActiveControl IsNot TextBox Then
            Dim number As Integer
            Select Case e.KeyCode
                Case Keys.D1, Keys.NumPad1
                    number = 1
                Case Keys.D2, Keys.NumPad2
                    number = 2
                Case Keys.D3, Keys.NumPad3
                    number = 3
            End Select
            If number > 0 Then
                Dim character As Integer = My.Settings.PartyOrder.Substring(number - 1, 1)
                SwitchCharacter(Me.character(character), e)
                Return
            End If
            else
        End If

        If e.KeyCode = Keys.X Then
            label(0).Focus()
        End If

        Select Case e.KeyCode
            Case Keys.R
                label(0).Focus()
                ResetAll()
            Case Keys.Escape
                Close()
            Case Else
                Main.SelectWindow(sender, e)
        End Select
    End Sub
End Class