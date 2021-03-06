Public Class Settings
    Public row(31), setting(7) As CheckBox
    Dim tooltips(5), heavenlapse(9), aphelion_dustwake(13) As CheckBox
    Dim random_hits(20), tooltips_label As Label
    Dim show_all, hide_all, documentation As Button
    Dim panel As Panel

    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Hide()
        Font = New Font("Segoe UI", 9, FontStyle.Regular)
        BackColor = Color.LightGray
        KeyPreview = True
        MaximizeBox = False
        Text = "Settings"
        MinimumSize = New Size(700, 523)
        MaximumSize = New Size(700, 856)
        LoadWindowData()
        AddHandler Click, AddressOf ChangeFocus


        ' SETTINGS

        For x = 0 To 6
            setting(x) = New CheckBox()
            With setting(x)
                .Size = New Size(350, 24)
                .Location = New Point(10, 10 + x * 25)
                .BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
                .Padding = New Padding(5, 0, 0, 0)
                .Tag = x
            End With
            Controls.Add(setting(x))
        Next
        setting(0).Text = "Auto-close target window"
        setting(1).Text = "Highlight hits when hovering over an attack card"
        setting(2).Text = "Dolphin button: read battle data (JP only)"
        setting(3).Text = "Show effective HP remaining"
        setting(4).Text = "Guillo's retroactive EX combo bonus"
        setting(5).Text = "Secret Queen after enemy gets up"
        setting(6).Text = "Saber Dragon Horn (+5 max durability on all equipment)"
        setting(0).Checked = My.Settings.TargetAutoClose
        setting(1).Checked = My.Settings.HighlightHits
        setting(2).Checked = My.Settings.BattleData
        setting(3).Checked = My.Settings.EffectiveHPRemaining
        setting(4).Checked = My.Settings.GuilloExtraBonus
        setting(5).Checked = My.Settings.SecretQueenGetUp
        setting(6).Checked = My.Settings.SaberDragonHorn
        For x = 0 To 6
            AddHandler setting(x).CheckedChanged, AddressOf ChangeSetting
        Next


        ' RANDOM HITS

        Dim random_hits_ypos As Integer = 219
        For x = 0 To 19
            random_hits(x) = New Label()
            With random_hits(x)
                If x < 13 Then
                    .Size = New Size(24, 24)
                    .Location = New Point(135 + x * 25, random_hits_ypos)
                    .Text = x + 1
                    .TextAlign = ContentAlignment.MiddleCenter
                ElseIf x < 17 Then
                    .Size = New Size(24, 24)
                    .Location = New Point(35 + x * 25, random_hits_ypos + 25)
                Else
                    .Size = New Size(124, 24)
                    .Location = New Point(10, random_hits_ypos + 25 * (x - 17))
                    .TextAlign = ContentAlignment.MiddleLeft
                End If
                .BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
            End With
            Controls.Add(random_hits(x))
            AddHandler random_hits(x).Click, AddressOf ChangeFocus
        Next
        random_hits(17).BackColor = Color.Transparent
        random_hits(17).Font = New Font("Segoe UI", 9, FontStyle.Bold)
        random_hits(17).Text = "Random hits"
        random_hits(18).Text = "Heavenlapse"
        random_hits(19).Text = "Aphelion Dustwake"

        For x = 0 To 8
            heavenlapse(x) = New CheckBox()
            With heavenlapse(x)
                .Size = New Size(24, 24)
                .Location = New Point(135 + x * 25, random_hits_ypos + 25)
                .BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
                .Padding = New Padding(5, 0, 0, 0)
                .Tag = x
                If My.Settings.Heavenlapse.ElementAt(x) = "1" Then
                    .Checked = True
                    .BackColor = Color.LightGreen
                Else
                    .BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
                End If
            End With
            Controls.Add(heavenlapse(x))
            AddHandler heavenlapse(x).CheckedChanged, AddressOf ChangeHeavenlapse
        Next
        For x = 0 To 12
            aphelion_dustwake(x) = New CheckBox()
            With aphelion_dustwake(x)
                .Size = New Size(24, 24)
                .Location = New Point(135 + x * 25, random_hits_ypos + 50)
                .BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
                .Padding = New Padding(5, 0, 0, 0)
                .Tag = x
                If My.Settings.AphelionDustwake.ElementAt(x) = "1" Then
                    .Checked = True
                    .BackColor = Color.LightGreen
                Else
                    .BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
                End If
            End With
            Controls.Add(aphelion_dustwake(x))
            AddHandler aphelion_dustwake(x).CheckedChanged, AddressOf ChangeAphelionDustwake
        Next


        ' TOOLTIPS

        Dim tooltips_ypos As Integer = 328
        tooltips_label = New Label()
        With tooltips_label
            .Size = New Size(100, 24)
            .Location = New Point(10, tooltips_ypos)
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            .Text = "Show tooltips"
            .TabIndex = 0
        End With
        AddHandler tooltips_label.Click, AddressOf ChangeFocus
        Controls.Add(tooltips_label)
        For x = 0 To 4
            tooltips(x) = New CheckBox()
            With tooltips(x)
                .Size = New Size(120, 24)
                .Location = New Point(10, tooltips_ypos + (x + 1) * 25)
                .BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
                .Padding = New Padding(5, 0, 0, 0)
                .Tag = x + 7
            End With
            Controls.Add(tooltips(x))
        Next
        tooltips(0).Text = "Variables"
        tooltips(1).Text = "Enemies"
        tooltips(2).Text = "Battle magnus"
        tooltips(3).Text = "Quest magnus"
        tooltips(4).Text = "Item magnus"
        tooltips(0).Checked = My.Settings.TableTooltips
        tooltips(1).Checked = My.Settings.TargetTooltips
        tooltips(2).Checked = My.Settings.DeckTooltips
        tooltips(3).Checked = My.Settings.QMTooltips
        tooltips(4).Checked = My.Settings.ItemTooltips
        For x = 0 To 4
            AddHandler tooltips(x).CheckedChanged, AddressOf ChangeSetting
        Next

        documentation = New Button()
        With documentation
            .Size = New Size(150, 30)
            .Location = New Point(233, tooltips_ypos + 24)
            .Text = "View documentation"
            .UseVisualStyleBackColor = True
        End With
        Controls.Add(documentation)
        AddHandler documentation.Click, AddressOf ViewDocumentation


        ' SHOW ALL / HIDE ALL

        show_all = New Button()
        With show_all
            .Size = New Size(90, 30)
            .Location = New Point(385, 10)
            .Text = "Show all"
            .UseVisualStyleBackColor = True
        End With
        Controls.Add(show_all)
        AddHandler show_all.Click, AddressOf ShowAll
        hide_all = New Button()
        With hide_all
            .Size = New Size(90, 30)
            .Location = New Point(385, 45)
            .Text = "Hide all"
            .UseVisualStyleBackColor = True
        End With
        Controls.Add(hide_all)
        AddHandler hide_all.Click, AddressOf HideAll


        ' RESULTS ROWS

        panel = New Panel
        With panel
            .AutoScroll = True
            .Location = New Point(487, 10)
            .Size = New Size(197, Height - 49)
        End With
        Controls.Add(panel)
        AddHandler panel.Click, AddressOf ChangeFocus

        For x = 0 To 31
            row(x) = New CheckBox()
            With row(x)
                .Size = New Size(175, 24)
                .Location = New Point(0, x * 25)
                .BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
                .Padding = New Padding(5, 0, 0, 0)
                .Tag = x
                .Text = Main.variable(x)
            End With
            panel.Controls.Add(row(x))
            If My.Settings.ResultsRow.ElementAt(x) = "1" Then
                row(x).Checked = True
                row(x).BackColor = Color.LightGreen
            End If
            AddHandler row(x).CheckedChanged, AddressOf ToggleRow
        Next
        If My.Settings.EffectiveHPRemaining Then
            row(31).Text = "Effective HP remaining"
        End If

        AddHandler Resize, AddressOf ResizePanel
        AddHandler Move, AddressOf SaveWindowData
        AddHandler Resize, AddressOf SaveWindowData
        AddHandler panel.Scroll, AddressOf SaveWindowData
        AddHandler panel.MouseWheel, AddressOf SaveWindowData
        Show()
        panel.VerticalScroll.Value = My.Settings.SettingsWindowScroll
    End Sub

    Private Sub LoadWindowData()
        Size = My.Settings.SettingsWindowSize
        Dim pt As Point = My.Settings.SettingsWindowLocation
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
            My.Settings.SettingsWindowSize = Size
            My.Settings.SettingsWindowScroll = panel.VerticalScroll.Value
            My.Settings.SettingsWindowLocation = Location
        End If
    End Sub

    Private Sub ChangeFocus()
        tooltips_label.Focus()
    End Sub

    Public Sub ToggleRow(sender As Object, e As EventArgs)
        Dim temp As String = My.Settings.ResultsRow
        temp = temp.Remove(sender.Tag, 1)
        If row(sender.Tag).Checked Then
            temp = temp.Insert(sender.Tag, "1")
            row(sender.Tag).BackColor = Color.LightGreen
        Else
            temp = temp.Insert(sender.Tag, "0")
            row(sender.Tag).BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
        End If
        My.Settings.ResultsRow = temp
        Main.UpdateRows()
    End Sub

    Public Sub ChangeSetting(sender As Object, e As EventArgs)
        Select Case sender.Tag
            Case 0
                My.Settings.TargetAutoClose = Not My.Settings.TargetAutoClose
            Case 1
                My.Settings.HighlightHits = Not My.Settings.HighlightHits
            Case 2
                My.Settings.BattleData = Not My.Settings.BattleData
            Case 3
                Main.ToggleEffectiveHP()
            Case 4
                My.Settings.GuilloExtraBonus = Not My.Settings.GuilloExtraBonus
                Main.Calculate()
            Case 5
                My.Settings.SecretQueenGetUp = Not My.Settings.SecretQueenGetUp
                Main.Calculate()
            Case 6
                My.Settings.SaberDragonHorn = Not My.Settings.SaberDragonHorn
                ToggleSaberDragonHorn()
            Case 7
                My.Settings.TableTooltips = Not My.Settings.TableTooltips
                Main.hover.Active = My.Settings.TableTooltips
            Case 8
                My.Settings.TargetTooltips = Not My.Settings.TargetTooltips
                If Target.Visible Then
                    Target.hover.Active = My.Settings.TargetTooltips
                End If
            Case 9
                My.Settings.DeckTooltips = Not My.Settings.DeckTooltips
                If Deck.Visible Then
                    Deck.hover.Active = My.Settings.DeckTooltips
                End If
            Case 10
                My.Settings.QMTooltips = Not My.Settings.QMTooltips
                If QuestMagnus.Visible Then
                    QuestMagnus.hover.Active = My.Settings.QMTooltips
                End If
            Case 11
                My.Settings.ItemTooltips = Not My.Settings.ItemTooltips
                If Boost.Visible Then
                    Boost.hover.Active = My.Settings.ItemTooltips
                End If
        End Select
    End Sub

    Private Sub ToggleSaberDragonHorn()
        Dim first, extra, before, after As Integer
        If My.Settings.SaberDragonHorn Then
            extra = 5
        End If
        For x = 0 To 2
            If Not Main.equipment(x).Visible Then
                Continue For
            End If
            before = Main.eq_durability(x).Items.Count
            after = Main.durability(Main.equipment(x).Tag) + extra
            first = 0
            If Main.durability(Main.equipment(x).Tag) > 0 Then
                first = 1
            ElseIf Not My.Settings.SaberDragonHorn Then
                first = 1
            End If
            If after < before Then
                For y = after To before - 1
                    Main.eq_durability(x).Items.RemoveAt(Main.eq_durability(x).Items.Count - 1)
                Next
            Else
                For y = before + first To after
                    Main.eq_durability(x).Items.Add(y)
                Next
            End If
            If Main.eq_durability(x).Items.Count > 0 Then
                If Main.eq_durability(x).Text = "" Then
                    Main.eq_durability(x).SelectedIndex = Main.durability(Main.equipment(x).Tag) - first
                End If
                Main.eq_durability(x).Show()
            Else
                Main.eq_durability(x).Hide()
                Main.Calculate()
            End If
        Next
    End Sub

    Private Sub ShowAll(sender As Object, e As EventArgs)
        For x = 0 To 31
            RemoveHandler row(x).CheckedChanged, AddressOf ToggleRow
            row(x).Checked = True
            row(x).BackColor = Color.LightGreen
            AddHandler row(x).CheckedChanged, AddressOf ToggleRow
        Next
        My.Settings.ResultsRow = "11111111111111111111111111111111"
        Main.UpdateRows()
    End Sub

    Private Sub HideAll(sender As Object, e As EventArgs)
        For x = 0 To 31
            RemoveHandler row(x).CheckedChanged, AddressOf ToggleRow
            row(x).Checked = False
            row(x).BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
            AddHandler row(x).CheckedChanged, AddressOf ToggleRow
        Next
        My.Settings.ResultsRow = "00000000000000000000000000000000"
        Main.UpdateRows()
    End Sub

    Private Sub ChangeHeavenlapse(sender As Object, e As EventArgs)
        Dim temp As String = My.Settings.Heavenlapse
        temp = temp.Remove(sender.Tag, 1)
        If heavenlapse(sender.Tag).Checked Then
            temp = temp.Insert(sender.Tag, "1")
            heavenlapse(sender.Tag).BackColor = Color.LightGreen
        Else
            temp = temp.Insert(sender.Tag, "0")
            heavenlapse(sender.Tag).BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
        End If
        My.Settings.Heavenlapse = temp
        Main.Calculate()
    End Sub

    Private Sub ChangeAphelionDustwake(sender As Object, e As EventArgs)
        Dim temp As String = My.Settings.AphelionDustwake
        temp = temp.Remove(sender.Tag, 1)
        If aphelion_dustwake(sender.Tag).Checked Then
            temp = temp.Insert(sender.Tag, "1")
            aphelion_dustwake(sender.Tag).BackColor = Color.LightGreen
        Else
            temp = temp.Insert(sender.Tag, "0")
            aphelion_dustwake(sender.Tag).BackColor = Color.FromArgb(&H90, &HFF, &HFF, &HFF)
        End If
        My.Settings.AphelionDustwake = temp
        Main.Calculate()
    End Sub

    Private Sub ViewDocumentation(sender As Object, e As EventArgs)
        Process.Start("https://docs.google.com/document/d/16S6PzD7il28LaEgBUjuSPb0oKtzHl5pvsfDkeBqQ8bk/view")
    End Sub

    Private Sub ResizePanel(sender As Object, e As EventArgs)
        panel.Height = Height - 49
    End Sub

    Private Sub Keyboard(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape Then
            Close()
        End If
    End Sub
End Class