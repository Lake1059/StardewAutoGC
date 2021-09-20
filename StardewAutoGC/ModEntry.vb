Imports Microsoft.Xna.Framework
Imports StardewModdingAPI
Imports StardewModdingAPI.Events
Imports StardewModdingAPI.Utilities
Imports StardewValley
Imports StardewValley.Menus

Public Class ModEntry
    Inherits [Mod]
    Public Overrides Sub Entry(helper As IModHelper)

        AddHandler helper.Events.GameLoop.DayEnding, AddressOf Me.每一天结束时事件
        AddHandler helper.Events.Input.ButtonPressed, AddressOf Me.按钮按下时事件
        AddHandler helper.Events.GameLoop.GameLaunched, AddressOf Me.启动完毕事件
    End Sub

    Public Shared 是否启用内存极限警告 As Boolean = False

    Public Sub 启动完毕事件(sender As Object, e As GameLaunchedEventArgs)
        If IntPtr.Size = 4 Then
            Me.Monitor.Log(Helper.Translation.Get("ActiveRAMwarning"), LogLevel.Alert)
            是否启用内存极限警告 = True
        End If
    End Sub


    Public Sub 每一天结束时事件(sender As Object, e As DayEndingEventArgs)
        Dim a As Long = Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024
        GC.Collect()
        Dim b As Long = Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024

        Dim T1 As String = Helper.Translation.Get("Auto.Part1")
        Dim T2 As String = Helper.Translation.Get("Auto.Part2")
        Dim T3 As String = Helper.Translation.Get("Auto.Part3")

        Dim c As String = T1 & T2 & a & " MB " & T3 & b & " MB"
        Me.Monitor.Log(c, LogLevel.Debug)
        Game1.addHUDMessage(New HUDMessage(c, ""))

        If 是否启用内存极限警告 = True Then
            If b >= 3500 Then
                Dim x1 As String = Helper.Translation.Get("RAMwarning.Part1")
                Dim x2 As String = Helper.Translation.Get("RAMwarning.Part2")
                Me.Monitor.Log(x1, LogLevel.Alert)
                Me.Monitor.Log(x2, LogLevel.Alert)
                Game1.activeClickableMenu = New DialogueBox(x1 & "^" & x2)
            End If
        End If
    End Sub

    Public Sub 按钮按下时事件(sender As Object, e As ButtonPressedEventArgs)
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

            If 是否启用内存极限警告 = True Then
                If b >= 3500 Then
                    Dim x1 As String = Helper.Translation.Get("RAMwarning.Part1")
                    Dim x2 As String = Helper.Translation.Get("RAMwarning.Part2")
                    Me.Monitor.Log(x1, LogLevel.Alert)
                    Me.Monitor.Log(x2, LogLevel.Alert)
                    Game1.activeClickableMenu = New DialogueBox(x1 & "^" & x2)
                End If
            End If

        End If
    End Sub

End Class

