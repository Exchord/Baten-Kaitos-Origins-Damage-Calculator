Public Class Settings
    Inherits Form

    Public row(31), enemy_row(24), setting(8) As CheckBox
    Dim random_hit(9, 17), tooltips(7) As CheckBox
    Dim random_hit_label(9, 16), tooltips_label, party_label(2), empty, version As Label
    Dim show_all, hide_all, documentation As Button
    Dim row_panel, enemy_row_panel, party_panel(2) As Panel
    Dim member(5) As RadioButton
    Dim party(3) As Integer
    Dim description(8) As String
    Dim hover As ToolTip
    Dim auto As Boolean

    ReadOnly member_name() As String = {"Sagi", "Milly", "Guillo"}

    Private Sub Open() Handles MyBase.Load
        Hide()
        Font = New Font("Segoe UI", 9, FontStyle.Regular)
        BackColor = Color.LightGray
        KeyPreview = True
        MaximizeBox = False
        Text = "Settings"
        Icon = New Icon(Me.GetType(), "icon.ico")
        MinimumSize = New Size(775, 748)
        MaximumSize = New Size(775, 856)
        LoadWindowData()
        AddHandler Click, AddressOf ChangeFocus


        ' MAIN SETTINGS

        For x = 0 To 7
            setting(x) = New CheckBox()
            With setting(x)
                .Size = New Size(350, 24)
                .Location = New Point(10, 10 + x * 25)
                .BackColor = Main.default_color
                .Padding = New Padding(5, 0, 0, 0)
                .Tag = x
                AddHandler .MouseEnter, AddressOf ShowDescription
            End With
            Controls.Add(setting(x))
        Next

        setting(0).Text = "Auto-close target window"
        setting(1).Text = "Highlight hits when hovering over an attack card"
        setting(2).Text = "Read combo from Dolphin"
        setting(3).Text = "Show effective HP remaining"
        setting(4).Text = "Guillo's English EX combos"
        setting(5).Text = "Guillo's retroactive EX combo bonus"
        setting(6).Text = "Secret Queen after enemy gets up"
        setting(7).Text = "Saber Dragon Horn (+5 max durability on all equipment)"

        description(0) = "When you choose a target, the target window closes automatically."
        description(1) = "When you hover over an attack card in your combo, all of its hits in the output table are highlighted in blue."
        description(2) = "If this is checked, you can import combos from the game by clicking the Dolphin button."
        description(3) = "Switch between true HP and effective HP for the last row in the output table. You can also click the row title in the main window to toggle this setting."
        description(4) = "The following EX combos are stronger in the English version of the game: Black Yang, White Yin, Fiery Ice Queen, Blazing Glacial Queen, Frigid Queen's Parade, Frigid Queen's Festival"
        description(5) = "If any of Guillo's standard attacks (except Medium Attack A) directly precedes an EX combo, the projectile will likely hit the target after the EX combo bonus becomes active." & vbCrLf & "This depends on the attack and the distance between Guillo and the enemy." & vbCrLf & "For example, if you use Weak Attack, Strong Attack, Icefan, and Sigil Cry in a row, the strong attack may get the 1.3 factor from Ice Queen."
        description(6) = "Secret Queen and Secret Queen II are EX combos that can only be triggered if the enemy is down. If the enemy gets up as Milly runs toward them, the first hit will not be a critical hit."
        description(7) = "Using this item raises the durability of equipped magnus to their initial durability plus 5." & vbCrLf & "If this setting is checked, the dropdown menus for equipped magnus will allow you to add up to 5 extra durability points."

        setting(0).Checked = My.Settings.TargetAutoClose
        setting(1).Checked = My.Settings.HighlightHits
        setting(2).Checked = My.Settings.ReadCombo
        setting(3).Checked = My.Settings.EffectiveHPRemaining
        setting(4).Checked = My.Settings.EnglishVersion
        setting(5).Checked = My.Settings.GuilloExtraBonus
        setting(6).Checked = My.Settings.SecretQueenGetUp
        setting(7).Checked = My.Settings.SaberDragonHorn

        For x = 0 To 7
            AddHandler setting(x).CheckedChanged, AddressOf ChangeSetting
        Next


        ' RANDOM HITS

        Dim attack_name() As String = {"Random hits", "Heavenlapse", "Aphelion Dustwake", "Fusillade", "Crimson Catharsis", "Berserk Tech", "Hyperslaught Mode 4", "Hyperslaught Mode 3", "Magnus of Life"}
        Dim attack_config() As String = {"", My.Settings.Heavenlapse, My.Settings.AphelionDustwake, My.Settings.Fusillade, My.Settings.CrimsonCatharsis, My.Settings.BerserkTech, My.Settings.HyperslaughtMode4, My.Settings.HyperslaughtMode3, My.Settings.MagnusOfLife}
        Dim hits() As Integer = {0, 9, 13, 16, 10, 5, 16, 10, 10}
        Dim random_hits_ypos As Integer = 244

        For y = 0 To 8
            random_hit_label(y, 0) = New Label()
            With random_hit_label(y, 0)
                .Size = New Size(124, 24)
                .Location = New Point(10, random_hits_ypos + 25 * y)
                .Text = attack_name(y)
                .TextAlign = ContentAlignment.MiddleLeft
                .BackColor = Main.default_color
                AddHandler .Click, AddressOf ChangeFocus
            End With
            Controls.Add(random_hit_label(y, 0))

            Dim config As String = attack_config(y)
            For x = 1 To hits(y)
                random_hit(y, x) = New CheckBox()
                With random_hit(y, x)
                    .Size = New Size(24, 24)
                    .Location = New Point(110 + x * 25, random_hits_ypos + y * 25)
                    .Padding = New Padding(5, 0, 0, 0)
                    .Tag = x
                    .Name = y
                    If config.ElementAt(x - 1) = "1" Then
                        .Checked = True
                        .BackColor = Color.LightBlue
                    Else
                        .BackColor = Main.default_color
                    End If
                    AddHandler .CheckedChanged, AddressOf ChangeRandomHit
                End With
                Controls.Add(random_hit(y, x))
            Next

            For x = hits(y) + 1 To 16
                random_hit_label(y, x) = New Label()
                With random_hit_label(y, x)
                    .Size = New Size(24, 24)
                    .Location = New Point(110 + x * 25, random_hits_ypos + 25 * y)
                    If y = 0 Then
                        .Text = x
                        .TextAlign = ContentAlignment.MiddleCenter
                    End If
                    .BackColor = Main.default_color
                    AddHandler .Click, AddressOf ChangeFocus
                End With
                Controls.Add(random_hit_label(y, x))
            Next
        Next
        random_hit_label(0, 0).BackColor = Color.Transparent
        random_hit_label(0, 0).Font = New Font("Segoe UI", 9, FontStyle.Bold)


        ' TOOLTIPS

        Dim tooltips_ypos As Integer = 353 + 150

        tooltips_label = New Label()
        With tooltips_label
            .Size = New Size(100, 24)
            .Location = New Point(10, tooltips_ypos)
            .TextAlign = ContentAlignment.MiddleLeft
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            .Text = "Show tooltips"
            .TabIndex = 0
            AddHandler .Click, AddressOf ChangeFocus
        End With
        Controls.Add(tooltips_label)

        For x = 0 To 6
            tooltips(x) = New CheckBox()
            With tooltips(x)
                .Size = New Size(124, 24)
                .Location = New Point(10, tooltips_ypos + (x + 1) * 25)
                .BackColor = Main.default_color
                .Padding = New Padding(5, 0, 0, 0)
                .Tag = x + 8
            End With
            Controls.Add(tooltips(x))
        Next
        tooltips(0).Text = "Variables"
        tooltips(1).Text = "Target"
        tooltips(2).Text = "Deck"
        tooltips(3).Text = "Quest Magnus"
        tooltips(4).Text = "Temporary Boost"
        tooltips(5).Text = "Magnus Power"
        tooltips(6).Text = "Settings"
        tooltips(0).Checked = My.Settings.TableTooltips
        tooltips(1).Checked = My.Settings.TargetTooltips
        tooltips(2).Checked = My.Settings.DeckTooltips
        tooltips(3).Checked = My.Settings.QMTooltips
        tooltips(4).Checked = My.Settings.ItemTooltips
        tooltips(5).Checked = My.Settings.MPTooltips
        tooltips(6).Checked = My.Settings.SettingsTooltips
        For x = 0 To 6
            AddHandler tooltips(x).CheckedChanged, AddressOf ChangeSetting
        Next


        ' PARTY ORDER

        For x = 0 To 1
            party_panel(x) = New Panel()
            With party_panel(x)
                .Size = New Size(275, 24)
                .Location = New Point(171, tooltips_ypos + (x + 1) * 25)
            End With
            Controls.Add(party_panel(x))

            party_label(x) = New Label()
            With party_label(x)
                .Size = New Size(79, 24)
                .BackColor = Main.default_color
                .TextAlign = ContentAlignment.MiddleCenter
                AddHandler .Click, AddressOf ChangeFocus
            End With
            party_panel(x).Controls.Add(party_label(x))
        Next
        party_label(0).Text = "1st member"
        party_label(1).Text = "2nd member"

        empty = New Label()
        With empty
            .Size = New Size(64, 24)
            .BackColor = Main.default_color
            AddHandler .Click, AddressOf ChangeFocus
        End With
        party_panel(1).Controls.Add(empty)

        For x = 0 To 4
            member(x) = New RadioButton()
            With member(x)
                .Size = New Size(64, 24)
                .Location = New Point(80 + x * 65, 0)
                .BackColor = Main.default_color
                .Padding = New Padding(5, 0, 0, 0)
                .Tag = x
                AddHandler .CheckedChanged, AddressOf ChangePartyOrder
                If x < 3 Then
                    party(x) = My.Settings.PartyOrder.Substring(x, 1)
                    .Text = member_name(x)
                    party_panel(0).Controls.Add(member(x))
                Else
                    party_panel(1).Controls.Add(member(x))
                End If
            End With
        Next
        For x = 0 To 2
            If x = party(0) Then
                member(x).Checked = True
                Exit For
            End If
        Next


        ' APP INFO

        Dim i, count As Integer
        For i = 0 To Main.version.Length - 1
            If Main.version.ElementAt(i) = "." Then
                count += 1
            End If
            If count = 2 Then
                Exit For
            End If
        Next
        Dim number As String = Main.version.Substring(0, i)

        version = New Label()
        With version
            .Size = New Size(90, 24)
            .Location = New Point(263, tooltips_ypos + 105)
            .TextAlign = ContentAlignment.MiddleCenter
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
            .Text = "Version " & number
            AddHandler .Click, AddressOf ChangeFocus
        End With
        Controls.Add(version)

        documentation = New Button()
        With documentation
            .Size = New Size(150, 32)
            .Location = New Point(233, tooltips_ypos + 135)
            .Text = "View documentation"
            .UseVisualStyleBackColor = True
            AddHandler .Click, AddressOf ViewDocumentation
        End With
        Controls.Add(documentation)


        ' RESULT ROWS

        show_all = New Button()
        With show_all
            .Size = New Size(90, 30)
            .Location = New Point(460, 10)
            .Text = "Show all"
            .UseVisualStyleBackColor = True
            AddHandler .Click, AddressOf ShowAll
        End With
        Controls.Add(show_all)

        hide_all = New Button()
        With hide_all
            .Size = New Size(90, 30)
            .Location = New Point(460, 45)
            .Text = "Hide all"
            .UseVisualStyleBackColor = True
            AddHandler .Click, AddressOf HideAll
        End With
        Controls.Add(hide_all)

        row_panel = New Panel
        With row_panel
            .AutoScroll = True
            .Location = New Point(562, 10)
            .Size = New Size(197, Height - 49)
            AddHandler .Click, AddressOf ChangeFocus
        End With
        Controls.Add(row_panel)

        enemy_row_panel = New Panel
        With enemy_row_panel
            .Hide()
            .AutoScroll = True
            .Location = New Point(562, 10)
            .Size = New Size(197, Height - 49)
            AddHandler .Click, AddressOf ChangeFocus
        End With
        Controls.Add(enemy_row_panel)

        For x = 0 To 31
            row(x) = New CheckBox()
            With row(x)
                .Size = New Size(175, 24)
                .Location = New Point(0, x * 25)
                .Padding = New Padding(5, 0, 0, 0)
                .Text = Main.variable(x)
                If My.Settings.ResultsRow.ElementAt(x) = "1" Then
                    .Checked = True
                    .BackColor = Color.LightBlue
                Else
                    .BackColor = Main.default_color
                End If
                .Tag = x
                AddHandler .CheckedChanged, AddressOf ToggleRow
            End With
            row_panel.Controls.Add(row(x))
        Next
        If My.Settings.EffectiveHPRemaining Then
            row(31).Text = "Effective HP remaining"
        End If

        For x = 0 To 23
            enemy_row(x) = New CheckBox()
            With enemy_row(x)
                .Size = New Size(175, 24)
                .Location = New Point(0, x * 25)
                .Padding = New Padding(5, 0, 0, 0)
                .Text = Main.E_variable(x)
                If My.Settings.EnemyResultsRow.ElementAt(x) = "1" Then
                    .Checked = True
                    .BackColor = Color.LightBlue
                Else
                    .BackColor = Main.default_color
                End If
                .Tag = x
                AddHandler .CheckedChanged, AddressOf E_ToggleRow
            End With
            enemy_row_panel.Controls.Add(enemy_row(x))
        Next

        hover = New ToolTip()
        With hover
            .AutomaticDelay = 250
            .AutoPopDelay = 30000
            .InitialDelay = 250
            .ReshowDelay = 50
            .Active = My.Settings.SettingsTooltips
        End With

        AddHandler Resize, AddressOf ResizePanel
        AddHandler Move, AddressOf SaveWindowData
        AddHandler Resize, AddressOf SaveWindowData
        AddHandler row_panel.Scroll, AddressOf SaveWindowData
        AddHandler row_panel.MouseWheel, AddressOf SaveWindowData
        SwitchMode()
        Show()
        row_panel.VerticalScroll.Value = My.Settings.SettingsWindowScroll
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
                Location = pt
                Return
            End If
        Next
        CenterToScreen()
    End Sub

    Private Sub SaveWindowData()
        If WindowState = FormWindowState.Normal Then
            My.Settings.SettingsWindowSize = Size
            My.Settings.SettingsWindowScroll = row_panel.VerticalScroll.Value
            My.Settings.SettingsWindowLocation = Location
        End If
    End Sub

    Private Sub ChangeFocus()
        tooltips_label.Focus()
    End Sub

    Public Sub ChangeSetting(sender As Object, e As EventArgs)
        With My.Settings
            Select Case sender.Tag
                Case 0
                    .TargetAutoClose = Not .TargetAutoClose
                Case 1
                    .HighlightHits = Not .HighlightHits
                Case 2
                    .ReadCombo = Not .ReadCombo
                Case 3
                    Main.ToggleEffectiveHP()
                Case 4
                    .EnglishVersion = Not .EnglishVersion
                    Main.Calculate()
                Case 5
                    .GuilloExtraBonus = Not .GuilloExtraBonus
                    Main.Calculate()
                Case 6
                    .SecretQueenGetUp = Not .SecretQueenGetUp
                    Main.Calculate()
                Case 7
                    Main.ToggleSaberDragonHorn()
                Case 8
                    .TableTooltips = Not .TableTooltips
                    Main.hover.Active = .TableTooltips
                Case 9
                    .TargetTooltips = Not .TargetTooltips
                    If Target.Visible Then
                        Target.hover.Active = .TargetTooltips
                    End If
                Case 10
                    .DeckTooltips = Not .DeckTooltips
                    If Deck.Visible Then
                        Deck.hover.Active = .DeckTooltips
                    End If
                Case 11
                    .QMTooltips = Not .QMTooltips
                    If QuestMagnus.Visible Then
                        QuestMagnus.hover.Active = .QMTooltips
                    End If
                Case 12
                    .ItemTooltips = Not .ItemTooltips
                    If Boost.Visible Then
                        Boost.hover.Active = .ItemTooltips
                    End If
                Case 13
                    .MPTooltips = Not .MPTooltips
                    If MP.Visible Then
                        MP.hover.Active = .MPTooltips
                    End If
                Case 14
                    .SettingsTooltips = Not .SettingsTooltips
                    hover.Active = .SettingsTooltips
            End Select
        End With
    End Sub

    Public Sub ToggleRow(sender As Object, e As EventArgs)
        Dim i As Integer = sender.Tag
        Dim temp As String = My.Settings.ResultsRow
        temp = temp.Remove(i, 1)
        If row(i).Checked Then
            temp = temp.Insert(i, "1")
            row(i).BackColor = Color.LightBlue
        Else
            temp = temp.Insert(i, "0")
            row(i).BackColor = Main.default_color
        End If
        My.Settings.ResultsRow = temp
        Main.UpdateRows()
    End Sub

    Public Sub E_ToggleRow(sender As Object, e As EventArgs)
        Dim i As Integer = sender.Tag
        Dim temp As String = My.Settings.EnemyResultsRow
        temp = temp.Remove(i, 1)
        If enemy_row(i).Checked Then
            temp = temp.Insert(i, "1")
            enemy_row(i).BackColor = Color.LightBlue
        Else
            temp = temp.Insert(i, "0")
            enemy_row(i).BackColor = Main.default_color
        End If
        My.Settings.EnemyResultsRow = temp
        Main.E_UpdateRows()
    End Sub

    Public Sub SwitchMode()
        If Not Main.enemy_mode Then
            enemy_row_panel.Hide()
            row_panel.Show()
        Else
            row_panel.Hide()
            enemy_row_panel.Show()
        End If
    End Sub

    Private Sub ShowAll()
        If Main.enemy_mode Then
            E_ShowAll()
            Return
        End If
        For x = 0 To 31
            RemoveHandler row(x).CheckedChanged, AddressOf ToggleRow
            row(x).Checked = True
            row(x).BackColor = Color.LightBlue
            AddHandler row(x).CheckedChanged, AddressOf ToggleRow
        Next
        My.Settings.ResultsRow = "11111111111111111111111111111111"
        Main.UpdateRows()
    End Sub

    Private Sub HideAll()
        If Main.enemy_mode Then
            E_HideAll()
            Return
        End If
        For x = 0 To 31
            RemoveHandler row(x).CheckedChanged, AddressOf ToggleRow
            row(x).Checked = False
            row(x).BackColor = Main.default_color
            AddHandler row(x).CheckedChanged, AddressOf ToggleRow
        Next
        My.Settings.ResultsRow = "00000000000000000000000000000000"
        Main.UpdateRows()
    End Sub

    Private Sub E_ShowAll()
        For x = 0 To 23
            RemoveHandler enemy_row(x).CheckedChanged, AddressOf E_ToggleRow
            enemy_row(x).Checked = True
            enemy_row(x).BackColor = Color.LightBlue
            AddHandler enemy_row(x).CheckedChanged, AddressOf E_ToggleRow
        Next
        My.Settings.EnemyResultsRow = "111111111111111111111111"
        Main.E_UpdateRows()
    End Sub

    Private Sub E_HideAll()
        For x = 0 To 23
            RemoveHandler enemy_row(x).CheckedChanged, AddressOf E_ToggleRow
            enemy_row(x).Checked = False
            enemy_row(x).BackColor = Main.default_color
            AddHandler enemy_row(x).CheckedChanged, AddressOf E_ToggleRow
        Next
        My.Settings.EnemyResultsRow = "000000000000000000000000"
        Main.E_UpdateRows()
    End Sub

    Private Sub ChangeRandomHit(sender As Object, e As EventArgs)
        Dim attack As Integer = sender.Name
        Dim hit As Integer = sender.Tag
        Dim attack_config() As String = {"", My.Settings.Heavenlapse, My.Settings.AphelionDustwake, My.Settings.Fusillade, My.Settings.CrimsonCatharsis, My.Settings.BerserkTech, My.Settings.HyperslaughtMode4, My.Settings.HyperslaughtMode3, My.Settings.MagnusOfLife}
        Dim config As String = attack_config(attack)
        config = config.Remove(hit - 1, 1)
        If random_hit(attack, hit).Checked Then
            config = config.Insert(hit - 1, "1")
            random_hit(attack, hit).BackColor = Color.LightBlue
        Else
            config = config.Insert(hit - 1, "0")
            random_hit(attack, hit).BackColor = Main.default_color
        End If
        Select Case attack
            Case 1
                My.Settings.Heavenlapse = config
            Case 2
                My.Settings.AphelionDustwake = config
            Case 3
                My.Settings.Fusillade = config
            Case 4
                My.Settings.CrimsonCatharsis = config
            Case 5
                My.Settings.BerserkTech = config
            Case 6
                My.Settings.HyperslaughtMode4 = config
            Case 7
                My.Settings.HyperslaughtMode3 = config
            Case 8
                My.Settings.MagnusOfLife = config
        End Select
        Main.Calculate()
    End Sub

    Private Sub ChangePartyOrder(sender As Object, e As EventArgs)
        If auto OrElse sender.Checked = False Then
            Return
        End If

        If sender.Tag < 3 Then
            auto = True
            If party(1) < party(2) Then
                member(3).Checked = True
            Else
                member(4).Checked = True
            End If
            auto = False
        End If

        With My.Settings
            Dim first As Integer = .PartyOrder.Substring(0, 1)
            Select Case sender.Tag
                Case 0
                    If member(3).Checked Then
                        .PartyOrder = "012"
                    Else
                        .PartyOrder = "021"
                    End If
                Case 1
                    If member(3).Checked Then
                        .PartyOrder = "102"
                    Else
                        .PartyOrder = "120"
                    End If
                Case 2
                    If member(3).Checked Then
                        .PartyOrder = "201"
                    Else
                        .PartyOrder = "210"
                    End If
                Case 3
                    Select Case first
                        Case 0
                            .PartyOrder = "012"
                        Case 1
                            .PartyOrder = "102"
                        Case 2
                            .PartyOrder = "201"
                    End Select
                Case 4
                    Select Case first
                        Case 0
                            .PartyOrder = "021"
                        Case 1
                            .PartyOrder = "120"
                        Case 2
                            .PartyOrder = "210"
                    End Select
            End Select
        End With

        Dim y As Integer = 3
        For x = 0 To 2
            party(x) = My.Settings.PartyOrder.Substring(x, 1)
            If x = party(0) Then
                empty.Left = 80 + x * 65
                Continue For
            End If
            member(y).Text = member_name(x)
            member(y).Left = 80 + x * 65
            y += 1
        Next

        Main.ChangePartyOrder()
        If Boost.Visible Then
            Boost.ChangePartyOrder()
        End If
    End Sub

    Private Sub ViewDocumentation()
        Process.Start("https://github.com/Exchord/Baten-Kaitos-Origins-Damage-Calculator#contents")
    End Sub

    Private Sub ShowDescription(sender As Object, e As EventArgs)
        hover.SetToolTip(sender, description(sender.Tag))
    End Sub

    Private Sub ResizePanel()
        row_panel.Height = Height - 49
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