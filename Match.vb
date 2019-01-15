Public Class Match
    Private hTeam As String
    Private aTeam As String
    Private hScore As Integer
    Private aScore As Integer
    Private prediction As Decimal
    Private aprediction As Decimal
    Private hxG As Decimal
    Private axG As Decimal
    Private hOdd As Decimal
    Private dOdd As Decimal
    Private aOdd As Decimal
    Public Function gethTeam()
        Return hTeam
    End Function
    Public Function getaTeam()
        Return aTeam
    End Function
    Public Function gethScore()
        Return hScore
    End Function
    Public Function getaScore()
        Return aScore
    End Function
    Public Function getprediction()
        Return prediction
    End Function
    Public Function getaprediction()
        Return aprediction
    End Function
    Public Function gethxG()
        Return hxG
    End Function
    Public Function getaxG()
        Return axG
    End Function
    Public Function gethOdd()
        Return hOdd
    End Function
    Public Function getdOdd()
        Return dOdd
    End Function
    Public Function getaOdd()
        Return aOdd
    End Function
    Public Function sethTeam(name As String)
        Me.hTeam = name
        Return True
    End Function
    Public Function setaTeam(name As String)
        Me.aTeam = name
        Return True
    End Function
    Public Function sethScore(num As Integer)
        Me.hScore = num
        Return True
    End Function
    Public Function setaScore(num As String)
        Me.aScore = num
        Return True
    End Function
    Public Function setprediction(num As Decimal)
        Me.prediction = num
        Return True
    End Function
    Public Function setaprediction(num As Decimal)
        Me.aprediction = num
        Return True
    End Function
    Public Function sethxG(num As Decimal)
        Me.hxG = num
        Return True
    End Function
    Public Function setaxG(num As Decimal)
        Me.axG = num
        Return True
    End Function
    Public Function sethOdd(num As Decimal)
        Me.hOdd = num
        Return True
    End Function
    Public Function setdOdd(num As Decimal)
        Me.dOdd = num
        Return True
    End Function
    Public Function setaOdd(num As Decimal)
        Me.aOdd = num
        Return True
    End Function
End Class
