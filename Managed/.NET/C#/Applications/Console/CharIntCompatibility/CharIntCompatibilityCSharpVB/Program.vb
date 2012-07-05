Friend Class Program
    ' Methods
    Friend Shared Sub Main()
        Console.WriteLine("asterisk")
        Dim asterisk As Char = "*"c
        'writeLineSbyte(asterisk) 'no implicit conversion
        'writeLineByte(asterisk) 'no implicit conversion
        'writeLineShort(asterisk) 'no implicit conversion        
        writeLineChar(asterisk)
        'writeLineUshort(asterisk) 'no implicit conversion
        'writeLineInt(asterisk) 'no implicit conversion
        'writeLineUint(asterisk) 'no implicit conversion
        'writeLineLong(asterisk) 'no implicit conversion
        'writeLineUlong(asterisk) 'no implicit conversion
        'writeLineFloat(asterisk) 'no implicit conversion
        'writeLineDouble(asterisk) 'no implicit conversion
        'writeLineDecimal(asterisk) 'no implicit conversion
        Console.WriteLine("space")
        Dim space As Byte = 32
        writeLineSbyte(space)
        writeLineByte(space)
        writeLineShort(space)
        'writeLineChar(space) 'no implicit conversion
        writeLineUshort(space)
        writeLineInt(space)
        writeLineUint(space)
        writeLineLong(space)
        writeLineUlong(space)
        writeLineFloat(space)
        writeLineDouble(space)
        writeLineDecimal(space)
        Console.Write("Press <Enter>")
        Console.ReadLine()
    End Sub

    Private Shared Sub writeLineByte(ByVal character As Byte)
        Console.WriteLine(character)
    End Sub

    Private Shared Sub writeLineChar(ByVal character As Char)
        Console.WriteLine(character)
    End Sub

    Private Shared Sub writeLineDecimal(ByVal character As Decimal)
        Console.WriteLine(character)
    End Sub

    Private Shared Sub writeLineDouble(ByVal character As Double)
        Console.WriteLine(character)
    End Sub

    Private Shared Sub writeLineFloat(ByVal character As Single)
        Console.WriteLine(character)
    End Sub

    Private Shared Sub writeLineInt(ByVal character As Integer)
        Console.WriteLine(character)
    End Sub

    Private Shared Sub writeLineLong(ByVal character As Long)
        Console.WriteLine(character)
    End Sub

    Private Shared Sub writeLineSbyte(ByVal character As Byte)
        Console.WriteLine(character)
    End Sub

    Private Shared Sub writeLineShort(ByVal character As Short)
        Console.WriteLine(character)
    End Sub

    Private Shared Sub writeLineUint(ByVal character As UInt32)
        Console.WriteLine(character)
    End Sub

    Private Shared Sub writeLineUlong(ByVal character As UInt64)
        Console.WriteLine(character)
    End Sub

    Private Shared Sub writeLineUshort(ByVal character As UInt16)
        Console.WriteLine(character)
    End Sub

End Class
