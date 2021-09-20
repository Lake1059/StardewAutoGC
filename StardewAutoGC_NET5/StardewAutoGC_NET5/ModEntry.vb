Imports StardewModdingAPI
Imports StardewModdingAPI.Events
Imports StardewValley
Imports StardewValley.Menus

Public Class ModEntry
    Inherits [Mod]
    Public Overrides Sub Entry(helper As IModHelper)

        AddHandler helper.Events.GameLoop.DayEnding, AddressOf Me.ÿһ�����ʱ�¼�
        AddHandler helper.Events.Input.ButtonPressed, AddressOf Me.��ť����ʱ�¼�
    End Sub

    Public Sub ÿһ�����ʱ�¼�(sender As Object, e As DayEndingEventArgs)
        Dim a As Long = Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024
        GC.Collect()
        Dim b As Long = Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024

        Dim T1 As String = Helper.Translation.Get("Auto.Part1")
        Dim T2 As String = Helper.Translation.Get("Auto.Part2")
        Dim T3 As String = Helper.Translation.Get("Auto.Part3")
        Dim c As String = T1 & T2 & a & " MB " & T3 & b & " MB"
        Me.Monitor.Log(c, LogLevel.Debug)
        Game1.addHUDMessage(New HUDMessage(c, ""))
    End Sub

    Public Sub ��ť����ʱ�¼�(sender As Object, e As ButtonPressedEventArgs)
        If e.Button = SButton.RightAlt Then
            Dim a As Long = Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024
            GC.Collect()
            Dim b As Long = Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024

            Dim T1 As String = Helper.Translation.Get("Manual.Part1")
            Dim T2 As String = Helper.Translation.Get("Manual.Part2")
            Dim T3 As String = Helper.Translation.Get("Manual.Part3")
            Dim c As String = T1 & T2 & a & " MB " & T3 & b & " MB"
            Me.Monitor.Log(c, LogLevel.Info)
            Game1.addHUDMessage(New HUDMessage(c, ""))
            'Game1.activeClickableMenu = New DialogueBox("���ԶԻ���")
        End If
    End Sub

End Class
