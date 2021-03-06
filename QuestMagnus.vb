Public Class QuestMagnus
    Public inventory(24) As PictureBox
    Dim card(181) As PictureBox
    Public magnus(181) As Bitmap
    Public move_slot As Integer
    Public result(18) As Label
    Dim panel As DoubleBufferPanel
    Dim clear As Button
    Public hover As ToolTip

    Private Sub QuestMagnus_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Hide()
        BackColor = Color.LightGray
        KeyPreview = True
        MaximizeBox = False
        Font = New Font("Segoe UI", 9, FontStyle.Regular)
        Text = "Quest Magnus"
        MinimumSize = New Size(1053, 699)
        MaximumSize = New Size(1053, 1149)
        LoadWindowData()

        clear = New Button
        With clear
            .Text = "Reset"
            .Location = New Point(817, 45)
            .Size = New Size(88, 29)
            .UseVisualStyleBackColor = True
        End With
        Controls.Add(clear)
        AddHandler clear.Click, AddressOf ClearAll

        panel = New DoubleBufferPanel
        With panel
            .AutoScroll = True
            .Location = New Point(0, 300)
            .Size = New Size(Width - 16, Height - 339)
        End With
        Controls.Add(panel)
        AddHandler panel.Click, AddressOf FocusPanel

        For x = 1 To 180
            card(x) = New PictureBox()
            With card(x)
                Dim y As Integer = x - 1
                .Size = New Size(50, 80)
                .Location = New Point(10 + 50 * (y - (y - (y Mod 20))), 90 * (y - (y Mod 20)) / 20)
                .BackColor = Color.Transparent
                .Cursor = Cursors.Hand
                .Tag = x
                If x < 93 Then
                    magnus(x) = New Bitmap(My.Resources.ResourceManager.GetObject("_" & x + 500), New Size(50, 80))
                ElseIf x < 153 Then
                    magnus(x) = New Bitmap(My.Resources.ResourceManager.GetObject("_" & x + 517), New Size(50, 80))
                Else
                    magnus(x) = New Bitmap(My.Resources.ResourceManager.GetObject("_" & x + 519), New Size(50, 80))
                End If
            End With
            panel.Controls.Add(card(x))
            AddHandler card(x).Click, AddressOf Add
            AddHandler card(x).MouseEnter, AddressOf ShowName
        Next
        magnus(0) = New Bitmap(My.Resources.ResourceManager.GetObject("_500"), New Size(50, 80))
        For x = 1 To 180
            card(x).Image = magnus(x)
        Next

        For x = 0 To 23
            inventory(x) = New PictureBox()
            With inventory(x)
                .Size = New Size(50, 80)
                .Location = New Point(10 + 69 * (x - (x - (x Mod 8))), 10 + 96 * (x - (x Mod 8)) / 8)
                .BackColor = Color.Transparent
                .Cursor = Cursors.Hand
                .Name = x
                .Tag = Main.QM_inventory(x)
                .Image = magnus(Main.QM_inventory(x))
            End With
            Controls.Add(inventory(x))
            AddHandler inventory(x).Click, AddressOf Remove
        Next

        move_slot = -1

        For x = 0 To 17
            result(x) = New Label()
            With result(x)
                If x < 9 Then
                    .Width = 109
                    .Location = New Point(600, 46 + x * 25)
                    .TextAlign = ContentAlignment.MiddleLeft
                Else
                    .Width = 49
                    .Location = New Point(710, 46 + (x - 9) * 25)
                    .TextAlign = ContentAlignment.MiddleCenter
                End If
                .Height = 24
                .BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
            End With
            Controls.Add(result(x))
        Next
        result(0).Text = "Physical offense"
        result(1).Text = "Fire offense"
        result(2).Text = "Ice offense"
        result(3).Text = "Lightning offense"
        result(4).Text = "Light offense"
        result(5).Text = "Dark offense"
        result(6).Text = "Extra hit offense"
        result(7).Text = "Critical hit chance"
        result(8).Text = "Level"
        For x = 0 To 8
            If x = 7 Then
                result(x + 9).Text = Main.QM_total_bonus(x) & "%"
            ElseIf Main.QM_total_bonus(x) > 0 Then
                result(x + 9).Text = "+" & Main.QM_total_bonus(x)
            Else
                result(x + 9).Text = Main.QM_total_bonus(x)
            End If
        Next

        hover = New ToolTip()
        With hover
            .AutoPopDelay = 5000
            .InitialDelay = 500
            .ReshowDelay = 500
            .Active = My.Settings.QMTooltips
        End With

        Show()
        AddHandler Resize, AddressOf ResizePanel
        AddHandler Resize, AddressOf SaveWindowData
        AddHandler Move, AddressOf SaveWindowData
        AddHandler panel.Scroll, AddressOf SaveWindowData
        AddHandler panel.MouseWheel, AddressOf SaveWindowData
        panel.VerticalScroll.Value = My.Settings.QMWindowScroll
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
                StartPosition = FormStartPosition.Manual
                Location = pt
                Return
            End If
        Next
        CenterToScreen()
    End Sub

    Private Sub SaveWindowData(sender As Object, e As EventArgs)
        If WindowState = FormWindowState.Normal Then
            My.Settings.QMWindowSize = Size
            My.Settings.QMWindowScroll = panel.VerticalScroll.Value
            My.Settings.QMWindowLocation = Location
        End If
    End Sub

    Private Sub Add(sender As Object, e As MouseEventArgs)
        For x = 0 To 23
            If inventory(x).Tag = 0 Then
                inventory(x).Image = magnus(sender.Tag)
                inventory(x).Tag = sender.Tag
                Main.QM_inventory(x) = sender.Tag
                Main.CheckQuestMagnus()
                If move_slot = x Then
                    move_slot = -1
                End If
                Exit For
            End If
        Next
    End Sub

    Private Sub Remove(sender As Object, e As MouseEventArgs)
        If e.Button = MouseButtons.Left Then
            inventory(sender.Name).Image = magnus(0)
            inventory(sender.Name).Tag = 0
            Main.QM_inventory(sender.Name) = sender.Tag
            Main.CheckQuestMagnus()
            If move_slot = sender.Name Then
                move_slot = -1
            End If
        ElseIf e.Button = MouseButtons.Right Then
            If move_slot = -1 Then
                move_slot = sender.Name
                sender.Image = Main.ChangeOpacity(magnus(sender.Tag), 0.5)
            Else
                Dim source As Integer = inventory(move_slot).Tag
                Dim target As Integer = sender.Tag
                Dim move_slot_2 As Integer = sender.Name
                inventory(move_slot_2).Image = magnus(source)
                inventory(move_slot_2).Tag = source
                Main.QM_inventory(move_slot_2) = source
                inventory(move_slot).Image = magnus(target)
                inventory(move_slot).Tag = target
                Main.QM_inventory(move_slot) = target
                move_slot = -1
            End If
        End If
    End Sub

    Private Sub ShowName(sender As Object, e As EventArgs)
        hover.SetToolTip(sender, Main.QM_name(sender.Tag))
    End Sub

    Private Sub ClearAll(sender As Object, e As EventArgs)
        For x = 0 To 23
            inventory(x).Image = magnus(0)
            inventory(x).Tag = 0
            Main.QM_inventory(x) = 0
        Next
        Main.CheckQuestMagnus()
    End Sub

    Private Sub ResizePanel(sender As Object, e As EventArgs)
        panel.Size = New Size(Width - 16, Height - 339)
    End Sub

    Private Sub FocusPanel(sender As Object, e As EventArgs)
        panel.Focus()
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub
End Class