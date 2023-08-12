Public Class Target
    Inherits Form

    Dim enemy(140) As PictureBox
    Public hover As ToolTip

    Private Sub Open() Handles MyBase.Load
        Hide()
        AutoScroll = True
        BackColor = Color.LightGray
        DoubleBuffered = True
        KeyPreview = True
        MaximizeBox = False
        MinimumSize = New Size(783, 669)
        MaximumSize = New Size(783, 1299)
        If Not Main.enemy_mode Then
            Text = "Target"
        Else
            Text = "Enemy"
        End If
        Icon = New Icon(Me.GetType(), "icon.ico")
        LoadWindowData()

        For x = 0 To 139
            enemy(x) = New PictureBox()
            With enemy(x)
                .Size = New Size(75, 90)
                .Location = New Point(75 * (x Mod 10), 90 * Math.Floor(x / 10))
                .Cursor = Cursors.Hand
                .Tag = x + 4
                .Image = New Bitmap(My.Resources.ResourceManager.GetObject("target_" & x + 4), .Size)
                AddHandler .Click, AddressOf ChangeTarget
                AddHandler .MouseEnter, AddressOf ShowName
            End With
            Controls.Add(enemy(x))
        Next

        hover = New ToolTip()
        With hover
            .AutomaticDelay = 250
            .AutoPopDelay = 5000
            .InitialDelay = 250
            .ReshowDelay = 50
            .Active = My.Settings.TargetTooltips
        End With

        AddHandler Resize, AddressOf SaveWindowData
        AddHandler Move, AddressOf SaveWindowData
        AddHandler Scroll, AddressOf SaveWindowData
        AddHandler MouseWheel, AddressOf SaveWindowData
        Show()
        VerticalScroll.Value = My.Settings.TargetWindowScroll
    End Sub

    Private Sub LoadWindowData()
        Size = My.Settings.TargetWindowSize
        Dim pt As Point = My.Settings.TargetWindowLocation
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
            My.Settings.TargetWindowSize = Size
            My.Settings.TargetWindowScroll = VerticalScroll.Value
            My.Settings.TargetWindowLocation = Location
        End If
    End Sub

    Private Sub ChangeTarget(sender As Object, e As EventArgs)
        Main.ChangeTarget(sender.Tag, -1, True)
        If My.Settings.TargetAutoClose Then
            Close()
        End If
    End Sub

    Private Sub ShowName(sender As Object, e As EventArgs)
        hover.SetToolTip(sender, Main.enemy_name(sender.Tag))
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Close()
            Case Else
                Main.SelectWindow(sender, e)
        End Select
    End Sub
End Class