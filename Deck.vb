Public Class Deck
    Inherits Form

    Dim card(455), all_on(25), all_off(25) As PictureBox
    Dim magnus(455) As Bitmap
    Public hover As ToolTip
    Dim active(455) As Boolean
    ReadOnly index() As Integer = {1, 7, 11, 21, 29, 45, 75, 125, 142, 163, 176, 189, 203, 236, 246, 258, 269, 293, 305, 319, 333, 351, 360, 371, 423, 439, 443, 453}

    Private Sub Deck_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Hide()
        AutoScroll = True
        BackColor = Color.LightGray
        DoubleBuffered = True
        KeyPreview = True
        MinimumSize = New Size(923, 486)
        Text = "Deck"
        Icon = New Icon(Me.GetType(), "icon.ico")
        LoadWindowData()

        Dim range As Integer
        For x = 1 To 454
            card(x) = New PictureBox()
            For y = 0 To 27
                If x >= index(y) Then
                    range = y
                Else
                    Exit For
                End If
            Next y
            'skip items and stratagems
            If range = 23 Or range = 25 Or range = 27 Then
                card(x).Name = "0"
                Continue For
            End If

            With card(x)
                .Image = Nothing
                .Size = New Size(50, 80)
                .Left = 90 + 50 * (x - index(range))
                If range < 23 Then
                    .Top = 10 + 85 * range
                ElseIf range = 24 Then
                    .Top = 10 + 85 * 23
                ElseIf range = 26 Then
                    .Top = 10 + 85 * 24
                End If
                .BackColor = Color.Transparent
                .Cursor = Cursors.Hand
                .Tag = x
                magnus(x) = New Bitmap(My.Resources.ResourceManager.GetObject("_" & x), New Size(50, 80))
            End With
            Controls.Add(card(x))
            AddHandler card(x).Click, AddressOf ToggleCard
            AddHandler card(x).MouseEnter, AddressOf ShowName
        Next x
        For x = 1 To 454
            If card(x).Name = "0" Then
                Continue For
            End If
            If Main.deck_magnus(x) = "1" Then
                card(x).Image = magnus(x)
                active(x) = True
            Else
                card(x).Image = Main.ChangeOpacity(magnus(x), 0.5)
                active(x) = False
            End If
        Next

        For x = 0 To 24
            all_on(x) = New PictureBox()
            all_off(x) = New PictureBox()
            With all_on(x)
                .Size = New Size(28, 28)
                .Location = New Point(10, 36 + 85 * x)
                .Image = New Bitmap(My.Resources.ResourceManager.GetObject("plus"), New Size(28, 28))
                .Tag = x
                .Cursor = Cursors.Hand
            End With
            With all_off(x)
                .Size = New Size(28, 28)
                .Location = New Point(50, 36 + 85 * x)
                .Image = New Bitmap(My.Resources.ResourceManager.GetObject("minus"), New Size(28, 28))
                .Tag = x
                .Cursor = Cursors.Hand
            End With
            Controls.Add(all_on(x))
            Controls.Add(all_off(x))
            AddHandler all_on(x).Click, AddressOf EnableGroup
            AddHandler all_off(x).Click, AddressOf DisableGroup
        Next
        all_on(23).Tag = 24
        all_on(24).Tag = 26
        all_off(23).Tag = 24
        all_off(24).Tag = 26

        hover = New ToolTip()
        With hover
            .AutoPopDelay = 5000
            .InitialDelay = 500
            .ReshowDelay = 500
            .Active = My.Settings.DeckTooltips
        End With

        AddHandler Resize, AddressOf SaveWindowData
        AddHandler Move, AddressOf SaveWindowData
        AddHandler Scroll, AddressOf SaveWindowData
        AddHandler MouseWheel, AddressOf SaveWindowData
        Show()
        HorizontalScroll.Value = My.Settings.DeckWindowScroll.X
        VerticalScroll.Value = My.Settings.DeckWindowScroll.Y
    End Sub

    Private Sub LoadWindowData()
        Size = My.Settings.DeckWindowSize
        Dim pt As Point = My.Settings.DeckWindowLocation
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
            My.Settings.DeckWindowSize = Size
            My.Settings.DeckWindowScroll = New Point(HorizontalScroll.Value, VerticalScroll.Value)
            My.Settings.DeckWindowLocation = Location
        End If
    End Sub

    Public Sub ToggleCard(sender As Object, e As EventArgs)
        If active(sender.Tag) Then
            card(sender.Tag).Image = Main.ChangeOpacity(magnus(sender.Tag), 0.5)
            active(sender.Tag) = False
            Main.deck_magnus(sender.Tag) = "0"
        Else
            card(sender.Tag).Image = magnus(sender.Tag)
            active(sender.Tag) = True
            Main.deck_magnus(sender.Tag) = "1"
        End If
        Main.ShowDeck()
    End Sub

    Private Sub EnableGroup(sender As Object, e As EventArgs)
        For x = index(sender.Tag) To index(sender.Tag + 1) - 1
            If Not active(x) Then
                ToggleCard(card(x), e)
            End If
        Next
        Main.ShowDeck()
    End Sub

    Private Sub DisableGroup(sender As Object, e As EventArgs)
        For x = index(sender.Tag) To index(sender.Tag + 1) - 1
            If active(x) Then
                ToggleCard(card(x), e)
            End If
        Next
        Main.ShowDeck()
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub

    Private Sub ShowName(sender As Object, e As EventArgs)
        hover.SetToolTip(sender, Main.magnus_name(sender.Tag))
    End Sub
End Class