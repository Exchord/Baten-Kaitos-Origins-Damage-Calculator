Public Class QuestMagnus
    Inherits Form

    Public inventory(24) As PictureBox
    Dim card(181) As PictureBox
    Public magnus(181) As Bitmap
    Public move_slot As Integer = -1
    Public result(18), E_result(12) As Label
    Dim panel As CustomPanel
    Dim clear As Button
    Public hover As ToolTip

    ReadOnly offense_effects() As Integer = {9, 10, 11, 12, 13, 14, 15, 22, 36, 37, 38, 41, 42, 43, 48}
    ReadOnly defense_effects() As Integer = {22, 27, 32, 33, 34, 35, 46, 47}
    Private Sub Open() Handles MyBase.Load
        Hide()
        BackColor = Color.LightGray
        KeyPreview = True
        MaximizeBox = False
        Font = New Font("Segoe UI", 9, FontStyle.Regular)
        Text = "Quest Magnus"
        Icon = New Icon(Me.GetType(), "icon.ico")
        MinimumSize = New Size(1053, 699)
        MaximumSize = New Size(1053, 1149)
        LoadWindowData()

        For x = 0 To 180
            If x < 93 Then
                magnus(x) = New Bitmap(My.Resources.ResourceManager.GetObject("_" & x + 500), New Size(50, 80))
            ElseIf x < 153 Then
                magnus(x) = New Bitmap(My.Resources.ResourceManager.GetObject("_" & x + 517), New Size(50, 80))
            Else
                magnus(x) = New Bitmap(My.Resources.ResourceManager.GetObject("_" & x + 519), New Size(50, 80))
            End If
        Next

        For x = 0 To 23
            inventory(x) = New PictureBox()
            With inventory(x)
                .Size = New Size(50, 80)
                .Location = New Point(10 + 69 * (x Mod 8), 10 + 96 * Math.Floor(x / 8))
                .Image = magnus(Main.QM_inventory(x))
                .Cursor = Cursors.Hand
                .Name = x
                .Tag = Main.QM_inventory(x)
                AddHandler .Click, AddressOf Remove
                AddHandler .Click, AddressOf Swap
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(inventory(x))
        Next

        clear = New Button
        With clear
            .Text = "Reset"
            .Location = New Point(837, 33)
            .Size = New Size(88, 29)
            .UseVisualStyleBackColor = True
            AddHandler .Click, AddressOf ClearAll
        End With
        Controls.Add(clear)

        Dim variable() As String = {"Physical offense", "Fire offense", "Ice offense", "Lightning offense", "Light offense", "Darkness offense", "Extra hit offense", "Critical hit chance", "Level", "Physical defense", "Knockdown threshold", "Knockout threshold", "Sagi max HP", "Milly max HP", "Guillo max HP"}

        For x = 0 To 17
            result(x) = New Label()
            With result(x)
                If x < 9 Then
                    .Size = New Size(109, 24)
                    .Location = New Point(600, 34 + x * 25)
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Text = variable(x)
                Else
                    .Size = New Size(49, 24)
                    .Location = New Point(710, 34 + (x - 9) * 25)
                    .TextAlign = ContentAlignment.MiddleCenter
                End If
                If x < 6 Then
                    .BackColor = Main.element_color(x)
                Else
                    .BackColor = Main.default_color
                End If
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(result(x))
        Next

        For x = 0 To 11
            E_result(x) = New Label()
            With E_result(x)
                If x < 6 Then
                    .Size = New Size(129, 24)
                    .Location = New Point(800, 109 + x * 25)
                    .TextAlign = ContentAlignment.MiddleLeft
                    .Text = variable(x + 9)
                Else
                    .Size = New Size(49, 24)
                    .Location = New Point(930, 109 + (x - 6) * 25)
                    .TextAlign = ContentAlignment.MiddleCenter
                End If
                .BackColor = Main.default_color
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(E_result(x))
        Next

        panel = New CustomPanel
        With panel
            .AutoScroll = True
            .Location = New Point(0, 300)
            .Size = New Size(Width - 16, Height - 339)
            AddHandler .Click, AddressOf ChangeFocus
        End With
        Controls.Add(panel)

        For x = 1 To 180
            card(x) = New PictureBox()
            With card(x)
                Dim y As Integer = x - 1
                .Size = New Size(50, 80)
                .Location = New Point(10 + 50 * (y Mod 20), 90 * Math.Floor(y / 20))
                .Cursor = Cursors.Hand
                .Tag = x
                AddHandler .Click, AddressOf Add
                AddHandler .Click, AddressOf RemoveLastInstance
                AddHandler .MouseEnter, AddressOf ShowName
                AddHandler .Click, AddressOf ChangeFocus
            End With
            panel.Controls.Add(card(x))
        Next

        hover = New ToolTip()
        With hover
            .AutoPopDelay = 5000
            .InitialDelay = 500
            .ReshowDelay = 500
            .Active = My.Settings.QMTooltips
        End With

        Show()
        SwitchMode()
        AddHandler Resize, AddressOf ResizePanel
        AddHandler Resize, AddressOf SaveWindowData
        AddHandler Move, AddressOf SaveWindowData
        AddHandler Click, AddressOf ChangeFocus
        AddHandler panel.Scroll, AddressOf SaveWindowData
        AddHandler panel.MouseWheel, AddressOf SaveWindowData
        panel.VerticalScroll.Value = My.Settings.QMWindowScroll
        panel.Focus()
    End Sub

    Private Sub LoadWindowData()
        Size = My.Settings.QMWindowSize
        Dim pt As Point = My.Settings.QMWindowLocation
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
            My.Settings.QMWindowSize = Size
            My.Settings.QMWindowScroll = panel.VerticalScroll.Value
            My.Settings.QMWindowLocation = Location
        End If
    End Sub

    Public Sub SwitchMode()
        Dim effects() As Integer
        If Not Main.enemy_mode Then
            effects = offense_effects
        Else
            effects = defense_effects
        End If
        For x = 1 To 180
            If effects.Contains(Main.QM_effect(x)) Then
                card(x).Image = magnus(x)
            Else
                card(x).Image = Main.MakeTransparent(magnus(x))
            End If
        Next
        Main.CheckQuestMagnus()
    End Sub

    Private Sub Add(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Left Then
            Return
        End If
        Dim id As Integer = sender.Tag
        For x = 0 To 23
            If inventory(x).Tag = 0 Then
                inventory(x).Image = magnus(id)
                inventory(x).Tag = id
                Main.QM_inventory(x) = id
                Main.CheckQuestMagnus()
                If move_slot = x Then
                    move_slot = -1
                End If
                Exit For
            End If
        Next
    End Sub

    Private Sub RemoveLastInstance(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Right Then
            Return
        End If
        Dim id As Integer = sender.Tag
        For x = 23 To 0 Step -1
            If inventory(x).Tag = id Then
                Remove(inventory(x), New MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0))
                Exit For
            End If
        Next
    End Sub

    Private Sub Remove(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Left Then
            Return
        End If
        Dim slot As Integer = sender.Name
        inventory(slot).Image = magnus(0)
        inventory(slot).Tag = 0
        Main.QM_inventory(slot) = 0
        Main.CheckQuestMagnus()
        If move_slot = slot Then
            move_slot = -1
        End If
    End Sub

    Private Sub Swap(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Right Then
            Return
        End If
        If move_slot = -1 Then
            move_slot = sender.Name
            sender.Image = Main.MakeTransparent(magnus(sender.Tag))
            Return
        End If
        Dim magnus_1 As Integer = inventory(move_slot).Tag
        Dim magnus_2 As Integer = sender.Tag
        Dim move_slot_2 As Integer = sender.Name
        inventory(move_slot_2).Image = magnus(magnus_1)
        inventory(move_slot_2).Tag = magnus_1
        Main.QM_inventory(move_slot_2) = magnus_1
        inventory(move_slot).Image = magnus(magnus_2)
        inventory(move_slot).Tag = magnus_2
        Main.QM_inventory(move_slot) = magnus_2
        move_slot = -1
    End Sub

    Private Sub ShowName(sender As Object, e As EventArgs)
        hover.SetToolTip(sender, Main.QM_name(sender.Tag))
    End Sub

    Private Sub ClearAll()
        move_slot = -1
        For x = 0 To 23
            inventory(x).Image = magnus(0)
            inventory(x).Tag = 0
            Main.QM_inventory(x) = 0
        Next
        Main.CheckQuestMagnus()
    End Sub

    Private Sub ResizePanel()
        panel.Size = New Size(Width - 16, Height - 339)
    End Sub

    Private Sub ChangeFocus()
        panel.Focus()
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.R
                ClearAll()
            Case Keys.Escape
                Close()
            Case Else
                Main.SelectWindow(sender, e)
        End Select
    End Sub
End Class