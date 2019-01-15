
Module Globals
    Public leagueList As New List(Of League)
    Public chosenLeague
    Public chosenweek As Integer
    Public weeklyScores As New List(Of List(Of Match))
    Public tform As Decimal = 0.22
    Public lform As Decimal = 0.3
    Public position As Decimal = 0.48
    Public goalDiff As Decimal = 0
    Public homeAdv As Decimal = 0.5

    Public holder, holder2 As Decimal
    Public checker As String
    Public mTform, mLform, mPos, mRTform, mRLform, mXG, xhomeS, xawayS, xhomeC, xawayC, hhform, aaform, htform, atform As Decimal
    Public Structure Normal
        Public average As Decimal
        Public sd As Decimal
    End Structure
    Public Structure TableEntry
        Public name As String
        Public homeP, awayP, points, gd, homeScored, homeConceded, awayScored, awayConceded As Integer
    End Structure
    Public Structure Table
        Public league As List(Of TableEntry)
        Public homeWNum, drawNum, homeLNum, homeWG, drawG, homeLG As Integer
    End Structure
    Public leaguetable As New Table
End Module
