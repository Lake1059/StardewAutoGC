Imports System.Runtime.InteropServices
Imports StardewModdingAPI
Imports StardewModdingAPI.Events
Imports StardewValley

Public Class ModEntry
    Inherits [Mod]

    Dim 用户配置 As ModConfig

    Public Overrides Sub Entry(helper As IModHelper)

        用户配置 = helper.ReadConfig(Of ModConfig)()

        AddHandler helper.Events.GameLoop.DayEnding, AddressOf Me.每天结束事件
        AddHandler helper.Events.Input.ButtonPressed, AddressOf Me.按钮按下事件
    End Sub

    Public Sub 每天结束事件(sender As Object, e As DayEndingEventArgs)
        Dim a As Long = Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024
        GC.Collect()
        GC.WaitForPendingFinalizers()
        Dim b As Long = Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024

        Dim T1 As String = Helper.Translation.Get("Auto.Part1")
        Dim T2 As String = Helper.Translation.Get("Auto.Part2")
        Dim T3 As String = Helper.Translation.Get("Auto.Part3")
        Dim c As String
        If a > 1024 Then
            c = T1 & T2 & Format(a / 1024, "0.00") & " GB " & T3 & Format(b / 1024, "0.00") & " GB "
        Else
            c = T1 & T2 & a & " MB " & T3 & b & " MB"
        End If

        Me.Monitor.Log(c, LogLevel.Debug)

        Game1.addHUDMessage(New HUDMessage(c, 1))
    End Sub

    Public Sub 按钮按下事件(sender As Object, e As ButtonPressedEventArgs)
        If e.Button = 用户配置.ManualButton Then
            Dim a As Long = Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024
            ClearMemory()
            Dim b As Long = Process.GetCurrentProcess().PrivateMemorySize64 / 1024 / 1024

            Dim T1 As String = Helper.Translation.Get("Manual.Part1")
            Dim T2 As String = Helper.Translation.Get("Manual.Part2")
            Dim T3 As String = Helper.Translation.Get("Manual.Part3")
            Dim c As String
            If a > 1024 Then
                c = T1 & T2 & Format(a / 1024, "0.00") & " GB " & T3 & Format(b / 1024, "0.00") & " GB "
            Else
                c = T1 & T2 & a & " MB " & T3 & b & " MB"
            End If
            Me.Monitor.Log(c, LogLevel.Info)
            Game1.addHUDMessage(New HUDMessage(c, 1))
        End If
    End Sub

    ''' <summary>
    '''设置线程工作的空间
    ''' </summary>
    ''' <param name="process">线程</param>
    ''' <param name="minSize">最小空间</param>
    ''' <param name="maxSize">最大空间</param>
    ''' <returns></returns>
    <DllImport("kernel32.dll", EntryPoint:="SetProcessWorkingSetSize")>
    Public Shared Function SetProcessWorkingSetSize(process As IntPtr, minSize As Integer, maxSize As Integer) As Integer
    End Function

    Public Shared Sub ClearMemory()
        GC.Collect()
        GC.WaitForPendingFinalizers()
        If Environment.OSVersion.Platform = PlatformID.Win32NT Then
            Dim unused = SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1)
        End If
    End Sub


End Class
