Public Class Target
    Dim enemy(140) As PictureBox
    Public hover As ToolTip

    Private Sub Target_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Hide()
        AutoScroll = True
        BackColor = Color.LightGray
        DoubleBuffered = True
        KeyPreview = True
        MaximizeBox = False
        MinimumSize = New Size(783, 669)
        MaximumSize = New Size(783, 1299)
        Text = "Target"
        LoadWindowData()

        For x = 0 To 139
            enemy(x) = New PictureBox()
            With enemy(x)
                .Size = New Size(75, 90)
                .Location = New Point(75 * (x - (x - (x Mod 10))), 90 * (x - (x Mod 10)) / 10)
                .BackColor = Color.Transparent
                .Cursor = Cursors.Hand
                .Tag = x + 4
                .Image = New Bitmap(My.Resources.ResourceManager.GetObject("target_" & x + 4), New Size(75, 90))
            End With
            Controls.Add(enemy(x))
            AddHandler enemy(x).Click, AddressOf ClickEnemy
            AddHandler enemy(x).MouseEnter, AddressOf ShowName
        Next x

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
                StartPosition = FormStartPosition.Manual
                Location = pt
                Return
            End If
        Next
        CenterToScreen()
    End Sub

    Private Sub SaveWindowData(sender As Object, e As EventArgs)
        If WindowState = FormWindowState.Normal Then
            My.Settings.TargetWindowSize = Size
            My.Settings.TargetWindowScroll = VerticalScroll.Value
            My.Settings.TargetWindowLocation = Location
        End If
    End Sub

    Private Sub ClickEnemy(sender As Object, e As EventArgs)
        Main.ChangeTarget(sender.Tag, 0, True)
        If My.Settings.TargetAutoClose Then
            Close()
        End If
    End Sub

    Private Sub ShowName(sender As Object, e As EventArgs)
        hover.SetToolTip(sender, Main.enemy_name(sender.Tag))
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub
End Class