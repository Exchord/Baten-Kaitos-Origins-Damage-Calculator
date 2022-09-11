Public Class Settings
    Inherits Form

    Public row(31), setting(8) As CheckBox
    Dim tooltips(7), heavenlapse(9), aphelion_dustwake(13) As CheckBox
    Dim random_hits(20), tooltips_label, party_label(2), empty, version As Label
    Dim show_all, hide_all, documentation As Button
    Dim panel, party_panel(2) As Panel
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
        MinimumSize = New Size(700, 598)
        MaximumSize = New Size(700, 856)
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
        description(7) = "Using this item raises the durability of equipped magnus to their max durability plus 5." & vbCrLf & "If this setting is checked, the dropdown menus for equipped magnus will allow you to add up to 5 extra durability points."

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

        Dim random_hits_ypos As Integer = 244
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
                .BackColor = Main.default_color
                AddHandler random_hits(x).Click, AddressOf ChangeFocus
            End With
            Controls.Add(random_hits(x))
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
                .BackColor = Main.default_color
                .Padding = New Padding(5, 0, 0, 0)
                .Tag = x
                If My.Settings.Heavenlapse.ElementAt(x) = "1" Then
                    .Checked = True
                    .BackColor = Color.LightGreen
                Else
                    .BackColor = Main.default_color
                End If
                AddHandler .CheckedChanged, AddressOf ChangeHeavenlapse
            End With
            Controls.Add(heavenlapse(x))
        Next

        For x = 0 To 12
            aphelion_dustwake(x) = New CheckBox()
            With aphelion_dustwake(x)
                .Size = New Size(24, 24)
                .Location = New Point(135 + x * 25, random_hits_ypos + 50)
                .BackColor = Main.default_color
                .Padding = New Padding(5, 0, 0, 0)
                .Tag = x
                If My.Settings.AphelionDustwake.ElementAt(x) = "1" Then
                    .Checked = True
                    .BackColor = Color.LightGreen
                Else
                    .BackColor = Main.default_color
                End If
                AddHandler .CheckedChanged, AddressOf ChangeAphelionDustwake
            End With
            Controls.Add(aphelion_dustwake(x))
        Next


        ' TOOLTIPS

        Dim tooltips_ypos As Integer = 353

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
            .Location = New Point(385, 10)
            .Text = "Show all"
            .UseVisualStyleBackColor = True
            AddHandler .Click, AddressOf ShowAll
        End With
        Controls.Add(show_all)

        hide_all = New Button()
        With hide_all
            .Size = New Size(90, 30)
            .Location = New Point(385, 45)
            .Text = "Hide all"
            .UseVisualStyleBackColor = True
            AddHandler .Click, AddressOf HideAll
        End With
        Controls.Add(hide_all)

        panel = New Panel
        With panel
            .AutoScroll = True
            .Location = New Point(487, 10)
            .Size = New Size(197, Height - 49)
            AddHandler .Click, AddressOf ChangeFocus
        End With
        Controls.Add(panel)

        For x = 0 To 31
            row(x) = New CheckBox()
            With row(x)
                .Size = New Size(175, 24)
                .Location = New Point(0, x * 25)
                .Padding = New Padding(5, 0, 0, 0)
                .Text = Main.variable(x)
                If My.Settings.ResultsRow.ElementAt(x) = "1" Then
                    .Checked = True
                    .BackColor = Color.LightGreen
                Else
                    .BackColor = Main.default_color
                End If
                .Tag = x
                AddHandler .CheckedChanged, AddressOf ToggleRow
            End With
            panel.Controls.Add(row(x))
        Next
        If My.Settings.EffectiveHPRemaining Then
            row(31).Text = "Effective HP remaining"
        End If

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

    Private Sub SaveWindowData()
        If WindowState = FormWindowState.Normal Then
            My.Settings.SettingsWindowSize = Size
            My.Settings.SettingsWindowScroll = panel.VerticalScroll.Value
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
            row(i).BackColor = Color.LightGreen
        Else
            temp = temp.Insert(i, "0")
            row(i).BackColor = Main.default_color
        End If
        My.Settings.ResultsRow = temp
        Main.UpdateRows()
    End Sub

    Private Sub ShowAll()
        For x = 0 To 31
            RemoveHandler row(x).CheckedChanged, AddressOf ToggleRow
            row(x).Checked = True
            row(x).BackColor = Color.LightGreen
            AddHandler row(x).CheckedChanged, AddressOf ToggleRow
        Next
        My.Settings.ResultsRow = "11111111111111111111111111111111"
        Main.UpdateRows()
    End Sub

    Private Sub HideAll()
        For x = 0 To 31
            RemoveHandler row(x).CheckedChanged, AddressOf ToggleRow
            row(x).Checked = False
            row(x).BackColor = Main.default_color
            AddHandler row(x).CheckedChanged, AddressOf ToggleRow
        Next
        My.Settings.ResultsRow = "00000000000000000000000000000000"
        Main.UpdateRows()
    End Sub

    Private Sub ChangeHeavenlapse(sender As Object, e As EventArgs)
        Dim i As Integer = sender.Tag
        Dim temp As String = My.Settings.Heavenlapse
        temp = temp.Remove(i, 1)
        If heavenlapse(i).Checked Then
            temp = temp.Insert(i, "1")
            heavenlapse(i).BackColor = Color.LightGreen
        Else
            temp = temp.Insert(i, "0")
            heavenlapse(i).BackColor = Main.default_color
        End If
        My.Settings.Heavenlapse = temp
        Main.Calculate()
    End Sub

    Private Sub ChangeAphelionDustwake(sender As Object, e As EventArgs)
        Dim i As Integer = sender.Tag
        Dim temp As String = My.Settings.AphelionDustwake
        temp = temp.Remove(i, 1)
        If aphelion_dustwake(i).Checked Then
            temp = temp.Insert(i, "1")
            aphelion_dustwake(i).BackColor = Color.LightGreen
        Else
            temp = temp.Insert(i, "0")
            aphelion_dustwake(i).BackColor = Main.default_color
        End If
        My.Settings.AphelionDustwake = temp
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
        Process.Start("https://github.com/Exchord/Baten-Kaitos-Origins-Damage-Calculator#readme")
    End Sub

    Private Sub ShowDescription(sender As Object, e As EventArgs)
        hover.SetToolTip(sender, description(sender.Tag))
    End Sub

    Private Sub ResizePanel()
        panel.Height = Height - 49
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