Public Class Setting
    Public Structure range
        Dim min As Integer
        Dim max As Integer
    End Structure
    Public Structure pos
        Dim gdiff As Integer
        Dim tForm As Integer
        Dim lForm As Integer
        Dim position As Integer
    End Structure
    Dim storage(11, 11, 11, 11) As range
    Public Function setArray()
        Dim initial As range
        initial.min = 0
        initial.max = 0
        For i = 0 To 10
            For j = 0 To 10
                For k = 0 To 10
                    For l = 0 To 10
                        storage(i, j, k, l) = initial
                    Next
                Next
            Next
        Next
        Return True
    End Function
    Public Function setStorage(homeA As Decimal, tform As Decimal, lform As Decimal, position As Decimal, min As Integer, max As Integer) As Boolean
        storage(homeA * 20, tform * 10, lform * 10, position * 10).min += min
        storage(homeA * 20, tform * 10, lform * 10, position * 10).max += max
        Return True
    End Function
    Public Function returnBest(number As Integer) As String()
        Dim array(number - 1) As pos
        Dim returnArray(number - 1) As String
        Dim used As Integer = 0
        Dim setUp As pos
        Dim l As Integer
        Dim placed As Boolean
        setUp.gdiff = 0
        setUp.tForm = 0
        setUp.lForm = 0
        For i = 0 To number - 1
            array(i) = setUp
        Next
        For h = 0 To 10
            For i = 0 To 10
                For j = 0 To 10
                    For k = 0 To 10
                        l = 0
                        placed = False
                        While l < used And placed = False
                            If (storage(h, i, j, k).min > storage(array(l).gdiff, array(l).tForm, array(l).lForm, array(l).position).min) Or ((storage(h, i, j, k).min = storage(array(l).gdiff, array(l).tForm, array(l).lForm, array(l).position).min) And (storage(h, i, j, k).max > storage(array(l).gdiff, array(l).tForm, array(l).lForm, array(l).position).max)) Then
                                If ((used - l - 2) >= 0) Then
                                    For m As Integer = used - 1 To l + 1 Step -1
                                        array(m) = array(m - 1)
                                    Next
                                End If

                                array(l).gdiff = h
                                array(l).tForm = i
                                array(l).lForm = j
                                array(l).position = k
                                placed = True
                                If used < number Then
                                    used += 1
                                End If
                            End If
                            l += 1
                        End While
                        If l = used And used < number Then
                            array(l).gdiff = h
                            array(l).tForm = i
                            array(l).lForm = j
                            array(l).position = k
                            used += 1
                        End If
                    Next
                Next
            Next
        Next
        Dim gdiff, tform, lform, position As Decimal
        For i As Integer = 0 To number - 1
            gdiff = (array(i).gdiff) / 20
            tform = (array(i).tForm / 10)
            lform = (array(i).lForm / 10)
            position = (array(i).position / 10)
            returnArray(i) = gdiff.ToString + "," + tform.ToString + "," + lform.ToString + "," + position.ToString + "," + storage(array(i).gdiff, array(i).tForm, array(i).lForm, array(i).position).min.ToString + "-" + storage(array(i).gdiff, array(i).tForm, array(i).lForm, array(i).position).max.ToString
        Next
        Return returnArray
    End Function
End Class
