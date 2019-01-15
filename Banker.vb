Public Class Banker
    Inherits League
    Private numOfBankers As Integer
    Private correctScore As Integer
    Private correctResult As Integer
    Private incorrectScore As Integer
    Public Function getNumOfBankers()
        Return numOfBankers
    End Function
    Public Function getCorrectScore()
        Return correctScore
    End Function
    Public Function getCorrectResult()
        Return correctResult
    End Function
    Public Function getIncorrectScore()
        Return incorrectScore
    End Function
    Public Function setNumOfBankers(number As Integer)
        Me.numOfBankers = number
        Return True
    End Function
    Public Function setCorrectScore(number As Integer)
        Me.correctScore = number
        Return True
    End Function
    Public Function setCorrectResult(number As Integer)
        Me.correctResult = number
        Return True
    End Function
    Public Function setIncorrectScore(number As Integer)
        Me.incorrectScore = number
        Return True
    End Function

End Class
