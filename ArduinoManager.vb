Imports System.IO.Ports
Imports System.Threading

Public Class ArduinoManager

    Private Shared WithEvents arduinoPort As New SerialPort()
    Private Shared cancellationTokenSource As CancellationTokenSource

    Public Shared Event ConnectionStatusChanged(sender As Object, status As String)

    ' Connect to Arduino
    Public Shared Sub Connect()
        Try
            cancellationTokenSource = New CancellationTokenSource()
            Dim availablePorts As String() = SerialPort.GetPortNames()

            If availablePorts.Length > 0 Then
                For Each port As String In availablePorts
                    If cancellationTokenSource.IsCancellationRequested Then Exit For

                    arduinoPort.PortName = port
                    arduinoPort.BaudRate = 9600
                    arduinoPort.ReadTimeout = 1000
                    arduinoPort.WriteTimeout = 1000

                    Try
                        arduinoPort.Open()
                        Thread.Sleep(1000)
                        arduinoPort.WriteLine("Hello")
                        Dim response As String = arduinoPort.ReadLine()

                        If response.Contains("Hello") Then
                            RaiseEvent ConnectionStatusChanged(Nothing, "Connected to " & arduinoPort.PortName)
                            Exit For
                        Else
                            arduinoPort.Close()
                        End If
                    Catch ex As Exception
                        arduinoPort.Close()
                    End Try
                Next

                If Not arduinoPort.IsOpen Then
                    RaiseEvent ConnectionStatusChanged(Nothing, "Unable to connect to any COM ports.")
                End If
            Else
                RaiseEvent ConnectionStatusChanged(Nothing, "No COM ports available!")
            End If
        Catch ex As Exception
            RaiseEvent ConnectionStatusChanged(Nothing, "Error: " & ex.Message)
        Finally
            cancellationTokenSource.Dispose()
        End Try
    End Sub

    ' Disconnect
    Public Shared Sub Disconnect()
        If arduinoPort.IsOpen Then
            arduinoPort.Close()
            RaiseEvent ConnectionStatusChanged(Nothing, "Disconnected.")
        End If
    End Sub

    ' Send string to Arduino
    Public Shared Sub SendToArduino(message As String)
        Try
            If arduinoPort.IsOpen Then
                arduinoPort.WriteLine(message)
            Else
                RaiseEvent ConnectionStatusChanged(Nothing, "Port is not open.")
            End If
        Catch ex As Exception
            RaiseEvent ConnectionStatusChanged(Nothing, "Send error: " & ex.Message)
        End Try
    End Sub

    ' Read string from Arduino
    Public Shared Function ReadFromArduino() As String
        Try
            If arduinoPort.IsOpen Then
                Return arduinoPort.ReadLine()
            Else
                Return "Port is not open."
            End If
        Catch ex As Exception
            Return "Read error: " & ex.Message
        End Try
    End Function
End Class


