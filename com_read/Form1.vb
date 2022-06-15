Imports System.IO.Ports

Public Class Form1
    Dim baca As Boolean = True
    Private Sub btn_read_Click(sender As Object, e As EventArgs) Handles btn_read.Click
        If ComboBox1.Text = "" Or cmd_txt.Text = "" Then
            MsgBox("COM Port belum diisi !")
            Exit Sub
        End If
        Dim port As String = ComboBox1.Text
        Dim cmd As String = cmd_txt.Text

        Dim returnStr As String = ""

        'Dim com1 As IO.Ports.SerialPort = Nothing

        'com1 = My.Computer.Ports.OpenSerialPort(port)
        Dim com1 As SerialPort
        com1 = New SerialPort(port, 2400, Parity.None, 7, StopBits.One)
        If com1.IsOpen Then
            com1.Close()
        End If
        Try
            com1.Open()
            com1.WriteTimeout = 10000
            com1.WriteLine(cmd)

        Catch ex As Exception
            returnStr = ex.ToString()
            ListBox1.Items.Add(returnStr)
            com1.Close()
            Exit Sub
        End Try
        Try
            'com1 = My.Computer.Ports.OpenSerialPort(port)

            com1.ReadTimeout = 10000


            Dim Incoming As String = com1.ReadLine()

            'returnStr &= Incoming & vbCrLf
            ListBox1.Items.Add(Incoming & vbCrLf)

        Catch ex As Exception
            returnStr = ex.ToString()
            ListBox1.Items.Add(returnStr)
        Finally
            If com1 IsNot Nothing Then com1.Close()
        End Try

    End Sub
    Function ReceiveSerialData(ByVal comport As String) As String
        ' Receive strings from a serial port.
        Dim returnStr As String = ""

        Dim com1 As IO.Ports.SerialPort = Nothing
        Try
            com1 = My.Computer.Ports.OpenSerialPort(comport)
            com1.ReadTimeout = 10000
            Do
                Dim Incoming As String = com1.ReadLine()
                If baca = False Then
                    com1.Close()
                    Exit Do
                End If
                If Incoming Is Nothing Then
                    Exit Do
                Else
                    returnStr &= Incoming & vbCrLf
                End If
            Loop
        Catch ex As Exception
            returnStr = ex.ToString()
        Finally
            If com1 IsNot Nothing Then com1.Close()
        End Try

        Return returnStr
    End Function

    Private Sub btn_stop_Click(sender As Object, e As EventArgs) Handles btn_stop.Click

        Close()
    End Sub
End Class
