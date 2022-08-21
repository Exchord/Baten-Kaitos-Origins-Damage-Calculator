﻿Public Class Boost
    Inherits Form

    Public character(4) As PictureBox
    Dim card(10) As PictureBox
    Dim element(6), label(3) As Label
    Public boost(5, 6, 2) As TextBox
    Dim next_turn(5), reset(5) As Button
    Public hover As ToolTip
    Dim auto As Boolean

    ReadOnly magnus() As Integer = {389, 390, 391, 392, 393, 398, 399, 414, 417, 418}

    Private Sub Boost_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Hide()
        Font = New Font("Segoe UI", 9, FontStyle.Regular)
        BackColor = Color.LightGray
        FormBorderStyle = FormBorderStyle.FixedSingle
        DoubleBuffered = True
        KeyPreview = True
        MaximizeBox = False
        Text = "Temporary Boost"
        Icon = New Icon(Me.GetType(), "icon.ico")
        Size = New Size(696, 599)
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
                        .Image = Main.ChangeOpacity(Main.char_icon(x), 0.5)
                    End If
                    .Cursor = Cursors.Hand
                    AddHandler .Click, AddressOf SwitchCharacter
                Else
                    .Size = New Size(70, 84)
                    .Location = New Point(20, 416)
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
        label(1).Location = New Point(20, 391)
        label(2).Location = New Point(20, 501)
        label(0).Text = "Offense"
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

        For x = 0 To 3
            Dim top As Integer = My.Settings.PartyOrder.IndexOf(x) * 80
            For y = 0 To 5
                For z = 0 To 1
                    boost(x, y, z) = New TextBox()
                    With boost(x, y, z)
                        .Size = New Size(69, 24)
                        .TextAlign = HorizontalAlignment.Center
                        .MaxLength = 8
                        If x < 3 Then
                            .Location = New Point(110 + 70 * y, 61 + top + 25 * z)
                            ChangeBox(boost(x, y, z), Main.offense_boost(x, y, z))
                        Else
                            .Location = New Point(110 + 70 * y, 391 + 25 * z)
                            ChangeBox(boost(x, y, z), Main.defense_boost(y, z))
                        End If
                        .Tag = x
                        .Name = y & z
                        AddHandler .KeyPress, AddressOf FilterInput
                        AddHandler .TextChanged, AddressOf CustomBoost
                        AddHandler .MouseWheel, AddressOf ScrollBoost
                        AddHandler .LostFocus, AddressOf FixBoost
                    End With
                    Controls.Add(boost(x, y, z))
                Next
            Next
        Next
        For z = 0 To 1
            boost(4, 0, z) = New TextBox()
            With boost(4, 0, z)
                .Location = New Point(110, 476 + 25 * z)
                .Size = New Size(69, 24)
                .TextAlign = HorizontalAlignment.Center
                .MaxLength = 8
                ChangeBox(boost(4, 0, z), Main.enemy_offense_boost(z))
                .Tag = 4
                .Name = 0 & z
                AddHandler .KeyPress, AddressOf FilterInput
                AddHandler .TextChanged, AddressOf CustomBoost
                AddHandler .MouseWheel, AddressOf ScrollBoost
                AddHandler .LostFocus, AddressOf FixBoost
            End With
            Controls.Add(boost(4, 0, z))
        Next


        ' MAGNUS

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
                .Image = New Bitmap(My.Resources.ResourceManager.GetObject("_" & magnus(x)), .Size)
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
                If x < 3 Then
                    .Location = New Point(548, 60 + top)
                ElseIf x = 3 Then
                    .Location = New Point(548, 390)
                ElseIf x = 4 Then
                    .Location = New Point(260, 476)
                End If
                .Size = New Size(90, 25)
                .UseVisualStyleBackColor = True
                .Text = "Next turn"
                .Tag = x
                AddHandler .Click, AddressOf NextTurn
            End With
            Controls.Add(next_turn(x))

            reset(x) = New Button()
            With reset(x)
                If x < 3 Then
                    .Location = New Point(548, 85 + top)
                ElseIf x = 3 Then
                    .Location = New Point(548, 415)
                ElseIf x = 4 Then
                    .Location = New Point(260, 501)
                End If
                .Size = New Size(90, 25)
                .UseVisualStyleBackColor = True
                .Text = "Reset"
                .Tag = x
                AddHandler .Click, AddressOf ResetBoost
            End With
            Controls.Add(reset(x))
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

    Private Sub SwitchCharacter(sender As Object, e As MouseEventArgs)
        Main.item_target = sender.Tag
        character(sender.Tag).Image = Main.char_icon(sender.Tag)
        For x = 0 To 2
            If x <> sender.Tag Then
                character(x).Image = Main.ChangeOpacity(Main.char_icon(x), 0.5)
            End If
        Next
    End Sub

    Private Sub Add(sender As Object, e As MouseEventArgs)
        Dim result As Double
        If sender.Tag < 8 Then
            Dim x As Integer = Main.item_target
            For y = 0 To 5
                For z = 0 To 1
                    result = LimitBoost(Main.offense_boost(x, y, z) + Main.boost_data(sender.Tag, y) * 0.01)
                    Main.offense_boost(x, y, z) = result
                    ChangeBox(boost(x, y, z), result)
                Next
            Next
        ElseIf sender.Tag = 8 Then
            For y = 0 To 5
                For z = 0 To 1
                    result = LimitBoost(Main.defense_boost(y, z) - Main.boost_data(sender.Tag, y) * 0.01)
                    Main.defense_boost(y, z) = result
                    ChangeBox(boost(3, y, z), result)
                Next
            Next
        ElseIf sender.Tag = 9 Then
            For z = 0 To 1
                result = LimitBoost(Main.enemy_offense_boost(z) - Main.boost_data(sender.Tag, z) * 0.01)
                Main.enemy_offense_boost(z) = result
                ChangeBox(boost(4, 0, z), result)
            Next
        End If
        Main.Calculate()
    End Sub

    Private Sub NextTurn(sender As Object, e As EventArgs)
        If sender.Tag = 4 Then
            Main.enemy_offense_boost(0) = Main.enemy_offense_boost(1)
            Main.enemy_offense_boost(1) = 0

            ChangeBox(boost(4, 0, 0), boost(4, 0, 1).Text)
            ChangeBox(boost(4, 0, 1), 0)
        Else
            For x = 0 To 5
                If sender.Tag = 3 Then
                    Main.defense_boost(x, 0) = Main.defense_boost(x, 1)
                    Main.defense_boost(x, 1) = 0
                Else
                    Main.offense_boost(sender.Tag, x, 0) = Main.offense_boost(sender.Tag, x, 1)
                    Main.offense_boost(sender.Tag, x, 1) = 0
                End If

                ChangeBox(boost(sender.Tag, x, 0), boost(sender.Tag, x, 1).Text)
                ChangeBox(boost(sender.Tag, x, 1), 0)
            Next
        End If
        Main.Calculate()
    End Sub

    Private Sub ResetBoost(sender As Object, e As EventArgs)
        If sender.Tag = 4 Then
            Main.enemy_offense_boost(0) = 0
            Main.enemy_offense_boost(1) = 0

            ChangeBox(boost(4, 0, 0), 0)
            ChangeBox(boost(4, 0, 1), 0)
        Else
            For x = 0 To 5
                If sender.Tag = 3 Then
                    Main.defense_boost(x, 0) = 0
                    Main.defense_boost(x, 1) = 0
                Else
                    Main.offense_boost(sender.Tag, x, 0) = 0
                    Main.offense_boost(sender.Tag, x, 1) = 0
                End If

                ChangeBox(boost(sender.Tag, x, 0), 0)
                ChangeBox(boost(sender.Tag, x, 1), 0)
            Next
        End If
        Main.Calculate()
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

    Private Sub CustomBoost(sender As Object, e As EventArgs)
        If auto Then
            Return
        End If

        Dim x, y, z As Integer
        x = sender.Tag
        y = sender.Name.ToString.ElementAt(0).ToString
        z = sender.Name.ToString.ElementAt(1).ToString

        With boost(x, y, z)
            If Not IsNumeric(.Text) OrElse (.Text > 1000 Or .Text < -1000) Then
                .Font = New Font("Segoe UI", 9, FontStyle.Regular)
                .ForeColor = Color.Red
                Return
            End If

            Dim new_value As Double = Round(.Text)
            If new_value = 0 Then
                .Font = New Font("Segoe UI", 9, FontStyle.Regular)
            Else
                .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            End If
            .ForeColor = Color.Black

            If x < 3 Then
                Main.offense_boost(x, y, z) = new_value
            ElseIf x = 3 Then
                Main.defense_boost(y, z) = new_value
            Else
                Main.enemy_offense_boost(z) = new_value
            End If
        End With

        Main.Calculate()
    End Sub

    Private Sub ScrollBoost(sender As Object, e As MouseEventArgs)
        FixBoost(sender, e)
        Dim x, y As Integer
        x = sender.Tag
        y = sender.Name.ToString.ElementAt(0).ToString

        For Each box As Control In {boost(x, y, 0), boost(x, y, 1)}
            If e.Delta > 0 Then
                box.Text = LimitBoost(box.Text + 1)
            Else
                box.Text = LimitBoost(box.Text - 1)
            End If
        Next
    End Sub

    Private Sub FixBoost(sender As Object, e As EventArgs)
        If sender.ForeColor = Color.Red Then
            If IsNumeric(sender.Text) Then
                sender.Text = LimitBoost(sender.Text)
            Else
                sender.Text = "0"
            End If
            Return
        End If
        sender.Text = Round(sender.Text)
    End Sub

    Private Function Round(input As Double) As Double
        Return Math.Round(Math.Round(input, 12), 3, MidpointRounding.AwayFromZero)
    End Function

    Public Function LimitBoost(input As Double) As Double
        Return Math.Max(-1000, Math.Min(Round(input), 1000))
    End Function

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
        hover.SetToolTip(sender, Main.boost_magnus(sender.Tag))
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub
End Class