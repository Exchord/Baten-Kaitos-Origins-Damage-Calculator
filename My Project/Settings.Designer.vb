﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    <Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.6.0.0"),  _
     Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
    Partial Friend NotInheritable Class MySettings
        Inherits Global.System.Configuration.ApplicationSettingsBase
        
        Private Shared defaultInstance As MySettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New MySettings()),MySettings)
        
#Region "My.Settings Auto-Save Functionality"
#If _MyType = "WindowsForms" Then
    Private Shared addedHandler As Boolean

    Private Shared addedHandlerLockObject As New Object

    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(), Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)> _
    Private Shared Sub AutoSaveSettings(sender As Global.System.Object, e As Global.System.EventArgs)
        If My.Application.SaveMySettingsOnExit Then
            My.Settings.Save()
        End If
    End Sub
#End If
#End Region
        
        Public Shared ReadOnly Property [Default]() As MySettings
            Get
                
#If _MyType = "WindowsForms" Then
               If Not addedHandler Then
                    SyncLock addedHandlerLockObject
                        If Not addedHandler Then
                            AddHandler My.Application.Shutdown, AddressOf AutoSaveSettings
                            addedHandler = True
                        End If
                    End SyncLock
                End If
#End If
                Return defaultInstance
            End Get
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
        Public Property MagnusActive() As Global.System.Collections.Specialized.StringCollection
            Get
                Return CType(Me("MagnusActive"),Global.System.Collections.Specialized.StringCollection)
            End Get
            Set
                Me("MagnusActive") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("11111111111111111111111111111111")>  _
        Public Property ResultsRow() As String
            Get
                Return CType(Me("ResultsRow"),String)
            End Get
            Set
                Me("ResultsRow") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property HighlightHits() As Boolean
            Get
                Return CType(Me("HighlightHits"),Boolean)
            End Get
            Set
                Me("HighlightHits") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property TargetTooltips() As Boolean
            Get
                Return CType(Me("TargetTooltips"),Boolean)
            End Get
            Set
                Me("TargetTooltips") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property TargetAutoClose() As Boolean
            Get
                Return CType(Me("TargetAutoClose"),Boolean)
            End Get
            Set
                Me("TargetAutoClose") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property ReadCombo() As Boolean
            Get
                Return CType(Me("ReadCombo"),Boolean)
            End Get
            Set
                Me("ReadCombo") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SaberDragonHorn() As Boolean
            Get
                Return CType(Me("SaberDragonHorn"),Boolean)
            End Get
            Set
                Me("SaberDragonHorn") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property TableTooltips() As Boolean
            Get
                Return CType(Me("TableTooltips"),Boolean)
            End Get
            Set
                Me("TableTooltips") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("100100101")>  _
        Public Property Heavenlapse() As String
            Get
                Return CType(Me("Heavenlapse"),String)
            End Get
            Set
                Me("Heavenlapse") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1001010101010")>  _
        Public Property AphelionDustwake() As String
            Get
                Return CType(Me("AphelionDustwake"),String)
            End Get
            Set
                Me("AphelionDustwake") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property GuilloExtraBonus() As Boolean
            Get
                Return CType(Me("GuilloExtraBonus"),Boolean)
            End Get
            Set
                Me("GuilloExtraBonus") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1, -1")>  _
        Public Property WindowLocation() As Global.System.Drawing.Point
            Get
                Return CType(Me("WindowLocation"),Global.System.Drawing.Point)
            End Get
            Set
                Me("WindowLocation") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("857, 1294")>  _
        Public Property WindowSize() As Global.System.Drawing.Size
            Get
                Return CType(Me("WindowSize"),Global.System.Drawing.Size)
            End Get
            Set
                Me("WindowSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1, -1")>  _
        Public Property TargetWindowLocation() As Global.System.Drawing.Point
            Get
                Return CType(Me("TargetWindowLocation"),Global.System.Drawing.Point)
            End Get
            Set
                Me("TargetWindowLocation") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1, -1")>  _
        Public Property DeckWindowLocation() As Global.System.Drawing.Point
            Get
                Return CType(Me("DeckWindowLocation"),Global.System.Drawing.Point)
            End Get
            Set
                Me("DeckWindowLocation") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1, -1")>  _
        Public Property DeckWindowSize() As Global.System.Drawing.Size
            Get
                Return CType(Me("DeckWindowSize"),Global.System.Drawing.Size)
            End Get
            Set
                Me("DeckWindowSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1, -1")>  _
        Public Property QMWindowLocation() As Global.System.Drawing.Point
            Get
                Return CType(Me("QMWindowLocation"),Global.System.Drawing.Point)
            End Get
            Set
                Me("QMWindowLocation") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1, -1")>  _
        Public Property QMWindowSize() As Global.System.Drawing.Size
            Get
                Return CType(Me("QMWindowSize"),Global.System.Drawing.Size)
            End Get
            Set
                Me("QMWindowSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1, -1")>  _
        Public Property BoostWindowLocation() As Global.System.Drawing.Point
            Get
                Return CType(Me("BoostWindowLocation"),Global.System.Drawing.Point)
            End Get
            Set
                Me("BoostWindowLocation") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1, -1")>  _
        Public Property SettingsWindowLocation() As Global.System.Drawing.Point
            Get
                Return CType(Me("SettingsWindowLocation"),Global.System.Drawing.Point)
            End Get
            Set
                Me("SettingsWindowLocation") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1, -1")>  _
        Public Property SettingsWindowSize() As Global.System.Drawing.Size
            Get
                Return CType(Me("SettingsWindowSize"),Global.System.Drawing.Size)
            End Get
            Set
                Me("SettingsWindowSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1, -1")>  _
        Public Property TargetWindowSize() As Global.System.Drawing.Size
            Get
                Return CType(Me("TargetWindowSize"),Global.System.Drawing.Size)
            End Get
            Set
                Me("TargetWindowSize") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property TargetWindowScroll() As Integer
            Get
                Return CType(Me("TargetWindowScroll"),Integer)
            End Get
            Set
                Me("TargetWindowScroll") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0, 0")>  _
        Public Property DeckWindowScroll() As Global.System.Drawing.Point
            Get
                Return CType(Me("DeckWindowScroll"),Global.System.Drawing.Point)
            End Get
            Set
                Me("DeckWindowScroll") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property SettingsWindowScroll() As Integer
            Get
                Return CType(Me("SettingsWindowScroll"),Integer)
            End Get
            Set
                Me("SettingsWindowScroll") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property QMWindowScroll() As Integer
            Get
                Return CType(Me("QMWindowScroll"),Integer)
            End Get
            Set
                Me("QMWindowScroll") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property DeckTooltips() As Boolean
            Get
                Return CType(Me("DeckTooltips"),Boolean)
            End Get
            Set
                Me("DeckTooltips") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property QMTooltips() As Boolean
            Get
                Return CType(Me("QMTooltips"),Boolean)
            End Get
            Set
                Me("QMTooltips") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property ItemTooltips() As Boolean
            Get
                Return CType(Me("ItemTooltips"),Boolean)
            End Get
            Set
                Me("ItemTooltips") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property SecretQueenGetUp() As Boolean
            Get
                Return CType(Me("SecretQueenGetUp"),Boolean)
            End Get
            Set
                Me("SecretQueenGetUp") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property EffectiveHPRemaining() As Boolean
            Get
                Return CType(Me("EffectiveHPRemaining"),Boolean)
            End Get
            Set
                Me("EffectiveHPRemaining") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("4")>  _
        Public Property Target() As Integer
            Get
                Return CType(Me("Target"),Integer)
            End Get
            Set
                Me("Target") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property FinalPhase() As Boolean
            Get
                Return CType(Me("FinalPhase"),Boolean)
            End Get
            Set
                Me("FinalPhase") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property SagiLevel() As Integer
            Get
                Return CType(Me("SagiLevel"),Integer)
            End Get
            Set
                Me("SagiLevel") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property MillyLevel() As Integer
            Get
                Return CType(Me("MillyLevel"),Integer)
            End Get
            Set
                Me("MillyLevel") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property GuilloLevel() As Integer
            Get
                Return CType(Me("GuilloLevel"),Integer)
            End Get
            Set
                Me("GuilloLevel") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("000000")>  _
        Public Property Auras() As String
            Get
                Return CType(Me("Auras"),String)
            End Get
            Set
                Me("Auras") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("000000000000000000000000000000000000000000000000")>  _
        Public Property QuestMagnus() As String
            Get
                Return CType(Me("QuestMagnus"),String)
            End Get
            Set
                Me("QuestMagnus") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0")>  _
        Public Property Character() As Integer
            Get
                Return CType(Me("Character"),Integer)
            End Get
            Set
                Me("Character") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("012")>  _
        Public Property PartyOrder() As String
            Get
                Return CType(Me("PartyOrder"),String)
            End Get
            Set
                Me("PartyOrder") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("False")>  _
        Public Property EnglishVersion() As Boolean
            Get
                Return CType(Me("EnglishVersion"),Boolean)
            End Get
            Set
                Me("EnglishVersion") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("-1, -1")>  _
        Public Property MPWindowLocation() As Global.System.Drawing.Point
            Get
                Return CType(Me("MPWindowLocation"),Global.System.Drawing.Point)
            End Get
            Set
                Me("MPWindowLocation") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1")>  _
        Public Property DeckClass() As Integer
            Get
                Return CType(Me("DeckClass"),Integer)
            End Get
            Set
                Me("DeckClass") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property MPTooltips() As Boolean
            Get
                Return CType(Me("MPTooltips"),Boolean)
            End Get
            Set
                Me("MPTooltips") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("True")>  _
        Public Property SettingsTooltips() As Boolean
            Get
                Return CType(Me("SettingsTooltips"),Boolean)
            End Get
            Set
                Me("SettingsTooltips") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1010010100110100")>  _
        Public Property Fusillade() As String
            Get
                Return CType(Me("Fusillade"),String)
            End Get
            Set
                Me("Fusillade") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0110001001")>  _
        Public Property CrimsonCatharsis() As String
            Get
                Return CType(Me("CrimsonCatharsis"),String)
            End Get
            Set
                Me("CrimsonCatharsis") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("01001")>  _
        Public Property BerserkTech() As String
            Get
                Return CType(Me("BerserkTech"),String)
            End Get
            Set
                Me("BerserkTech") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0010001110010110")>  _
        Public Property HyperslaughtMode4() As String
            Get
                Return CType(Me("HyperslaughtMode4"),String)
            End Get
            Set
                Me("HyperslaughtMode4") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("0100101100")>  _
        Public Property HyperslaughtMode3() As String
            Get
                Return CType(Me("HyperslaughtMode3"),String)
            End Get
            Set
                Me("HyperslaughtMode3") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("1100100100")>  _
        Public Property MagnusOfLife() As String
            Get
                Return CType(Me("MagnusOfLife"),String)
            End Get
            Set
                Me("MagnusOfLife") = value
            End Set
        End Property
        
        <Global.System.Configuration.UserScopedSettingAttribute(),  _
         Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
         Global.System.Configuration.DefaultSettingValueAttribute("111111111111111111111111")>  _
        Public Property EnemyResultsRow() As String
            Get
                Return CType(Me("EnemyResultsRow"),String)
            End Get
            Set
                Me("EnemyResultsRow") = value
            End Set
        End Property
    End Class
End Namespace

Namespace My
    
    <Global.Microsoft.VisualBasic.HideModuleNameAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute()>  _
    Friend Module MySettingsProperty
        
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")>  _
        Friend ReadOnly Property Settings() As Global.Baten_Kaitos_Origins_Damage_Calculator.My.MySettings
            Get
                Return Global.Baten_Kaitos_Origins_Damage_Calculator.My.MySettings.Default
            End Get
        End Property
    End Module
End Namespace
