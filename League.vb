Public Class League
    Private name As String
    Private noOfTeams As Integer
    Private teamList As List(Of String)
    Private pointForResult As Integer
    Private pointForScore As Integer
    Public Function getName()
        Return name
    End Function
    Public Function getNoOfTeams()
        Return noOfTeams
    End Function
    Public Function getTeamList()
        Return teamList
    End Function
    Public Function getPointForResult()
        Return pointForResult
    End Function
    Public Function getPointForScore()
        Return pointForScore
    End Function
    Public Function setName(name As String)
        Me.name = name
        Return True
    End Function
    Public Function setNoOfTeams(teamnumber As Integer)
        Me.noOfTeams = teamnumber
        Return True
    End Function
    Public Function setTeamList(teamsInput As List(Of String))
        Me.teamList = teamsInput
        Return True
    End Function
    Public Function setPointForResult(point As Integer)
        Me.pointForResult = point
        Return True
    End Function
    Public Function setPointForScore(point As Integer)
        Me.pointForScore = point
        Return True
    End Function
End Class
