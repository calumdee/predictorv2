Imports System.IO

Public Class MainPage
    Private Sub MainPage_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = Color.RoyalBlue
        Me.Size = New Size(714, 436)
        Me.MinimumSize = New Size(714, 436)
        leaguetable.league = New List(Of TableEntry)
        getLeagues()
        addTabs()
        setupDataEntryTab(0)
        setupDataEntryTab(1)
        setupSettingTab(2)
        setupDataEntryTab(3)
        setupTableTab(4)
    End Sub
    Private Sub addTabs()
        Dim TabControl1 As New TabControl
        Dim names() As String = {"Input Predictions", "tabInPrediction", "Enter Results", "tabEnResults", "Settings", "tabSet", "Calculate", "tabCalcSett", "Table", "tabTable"}
        TabControl1.Name = "TabControl1"
        For i As Integer = 0 To names.Length / 2 - 1
            Dim tabPage As New TabPage()
            tabPage.Text = names(2 * i)
            tabPage.Name = names(2 * i + 1)
            tabPage.BackColor = Color.RoyalBlue
            TabControl1.TabPages.Add(tabPage)
            tabPage.Show()
        Next
        Me.Controls.Add(TabControl1)
        SizeTabControl(Me.Size)
    End Sub
    Private Sub SizeTabControl(FormSize As Size)
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        TabControl1.Location = New Point(0, 0)
        TabControl1.Size = New Size(FormSize.Width, FormSize.Height)
    End Sub
    Private Sub setupTableTab(tab As Integer)
        Dim TC1 As TabControl = Me.Controls("TabControl1")
        createChooseLeague(tab)
        Dim btn As Button = TC1.TabPages(tab).Controls("btn(3,2)")
        RemoveHandler btn.Click, AddressOf Me.LeagueEnter_Click
        AddHandler btn.Click, AddressOf Me.TableDisplay_Click
    End Sub
    Private Sub TableDisplay_Click(sender As System.Object, e As System.EventArgs)
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        Dim tab As Integer = sender.tag
        Dim comboLeague As ComboBox = TabControl1.TabPages(tab).Controls("combo(2,1)")
        Dim comboWeek As ComboBox = TabControl1.TabPages(tab).Controls("combo(2,2)")
        Dim bottomBox = TabControl1.TabPages(tab).Controls("lbl(1,2)")
        Dim point As New Point(bottomBox.Location.X, bottomBox.Location.Y + bottomBox.Height + 10)
        Dim leagueNumber As Integer
        RemoveBonusControls(tab)
        If comboLeague.Text <> "" And comboWeek.Text <> "" Then
            chosenweek = comboWeek.Text
            For i As Integer = 0 To leagueList.Count - 1
                If comboLeague.Text = leagueList(i).getName Then
                    leagueNumber = i
                    chosenLeague = leagueList(i)
                End If
            Next
            If weeklyScores.Count = 0 Then
                getGames(leagueList(leagueNumber).getName)
            End If
            createTable()
            createTableDisplay(tab, leagueNumber, point)
        End If
    End Sub
    Private Sub createTableDisplay(TAB As Integer, leagueNumber As Integer, location As Point)
        Dim TC1 As TabControl = Me.Controls("TabControl1")
        Dim cleague As League = leagueList(leagueNumber)
        Dim startLocation As New Point(10, location.Y)
        Dim tlocation As New Point
        Dim teamNumber As Integer = cleague.getNoOfTeams
        Dim teamList As List(Of String) = cleague.getTeamList
        Dim teamArray(teamNumber - 1) As String
        For i As Integer = 0 To teamNumber - 1
            teamArray(i) = teamList(i)
        Next
        Dim csize = New Size(160, 30)
        Dim tsize = New Size(30, 30)
        Dim lsize = New Size(100, 30)
        Dim c As String
        For i As Integer = 0 To teamNumber - 1
            c = (i + 3).ToString
            tlocation = startLocation
            createLabel("(1," + c + ")", (i + 1).ToString, lsize, TAB, tlocation)
            tlocation.X = (tlocation.X + lsize.Width)
            createLabel("(2," + c + ")", leaguetable.league(i).name, lsize, TAB, tlocation)
            tlocation.X = (tlocation.X + lsize.Width)
            createLabel("(3," + c + ")", (leaguetable.league(i).homeP + leaguetable.league(i).awayP).ToString, lsize, TAB, tlocation)
            tlocation.X = tlocation.X + lsize.Width
            createLabel("(4," + c + ")", leaguetable.league(i).gd, lsize, TAB, tlocation)
            tlocation.X = (tlocation.X + lsize.Width)
            createLabel("(5," + c + ")", leaguetable.league(i).points, lsize, TAB, tlocation)
            tlocation.X = tlocation.X + lsize.Width
            startLocation.Y += lsize.Height
        Next
        Me.Size = New Size(Me.Size.Width, startLocation.Y + 70 + csize.Height)
        Me.Controls("TabControl1").Size = New Size(Me.Size.Width, startLocation.Y + 70 + csize.Height)
    End Sub
    Private Sub setupSettingTab(tab As Integer)
        Dim TabControl As TabControl = Me.Controls("TabControl1")
        Dim point As New Point(10, 10)
        Dim point2 As New Point(10, 10)
        Dim size As New Size(100, 30)
        For i As Integer = 1 To 2
            createTextBox("(1," + i.ToString + ")", "", size, tab, point)
            Dim textBox = TabControl.TabPages(tab).Controls("txt(1," + i.ToString + ")")
            AddHandler textBox.TextChanged, AddressOf Me.textBox_TextChanged
            point2.X = point.X + size.Width + 10
            point2.Y = point.Y
            createTextBox("(2," + i.ToString + ")", "", size, tab, point2)
            point.Y = point.Y + size.Height + 10
            Dim textBox1 = TabControl.TabPages(tab).Controls("txt(2," + i.ToString + ")")
            AddHandler textBox1.TextChanged, AddressOf Me.textBox_TextChanged
        Next
    End Sub
    Private Sub textBox_TextChanged(sender As System.Object, e As System.EventArgs)
        Dim TC1 As TabControl = Me.Controls("TabControl1")
        Dim txt As TextBox = sender
        If txt.Name = "txt(1,1)" Then
            tform = txt.Text
        ElseIf txt.Name = "txt(2,1)" Then
            lform = txt.Text
        ElseIf txt.Name = "txt(1,2)" Then
            position = txt.Text
        ElseIf txt.Name = "txt(2,2)" Then
            homeAdv = txt.Text
        End If
    End Sub
    Private Sub setupDataEntryTab(tab As Integer)
        createChooseLeague(tab)
    End Sub
    Private Sub createChooseLeague(tab As Integer)
        Dim TabControl As TabControl = Me.Controls("TabControl1")
        Dim point As New Point(10, 10)
        Dim point2 As New Point(10, 10)
        Dim size As New Size(100, 30)
        Dim label() As String = {"Choose League", "Choose Gameweek"}
        Dim array1() As String
        For i As Integer = 0 To leagueList.Count - 1
            ReDim Preserve array1(i)
            array1(i) = leagueList(i).getName
        Next
        Dim array2() As String = {}
        Dim combo() = {array1, array2}
        For i As Integer = 1 To 2
            createLabel("(1," + i.ToString + ")", label(i - 1), size, tab, point)
            point2.X = point.X + size.Width + 10
            point2.Y = point.Y
            createCombo("(2," + i.ToString + ")", combo(i - 1), size, tab, point2, False)
            point.Y = point.Y + size.Height + 10
        Next

        Dim comboBox = TabControl.TabPages(tab).Controls("combo(2,1)")
        AddHandler comboBox.TextChanged, AddressOf Me.ComboBoxLeague_TextChanged
        comboBox = TabControl.TabPages(tab).Controls("combo(2,2)")
        AddHandler comboBox.TextChanged, AddressOf Me.ComboBoxWeek_TextChanged
        createButton("(3,2)", "Enter", size, tab, New Point(point2.X + size.Width + 10, point2.Y))
        Dim button = TabControl.TabPages(tab).Controls("btn(3,2)")
        AddHandler button.Click, AddressOf Me.LeagueEnter_Click
    End Sub
    Private Sub ComboBoxLeague_TextChanged(sender As System.Object, e As System.EventArgs)
        Dim combo As ComboBox = sender
        Dim c As Integer = 0
        If combo.Text = "" Then

        Else
            Do Until leagueList(c).getName = combo.Text
                c += 1
            Loop
            Dim x As Integer
            x = (leagueList(c).getNoOfTeams - 1) * 2
            Dim TabControl1 As TabControl = Me.Controls("TabControl1")
            Dim comboGW As ComboBox = TabControl1.TabPages(combo.Tag).Controls("combo(2,2)")
            For i As Integer = 0 To x + 1
                comboGW.Items.Add(i)
            Next
        End If
    End Sub
    Private Sub ComboBoxWeek_TextChanged(sender As System.Object, e As System.EventArgs)
        Dim combo As ComboBox = sender
        chosenweek = combo.Text
    End Sub
    Private Sub RemoveBonusControls(tab As Integer)
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        Dim con As Control
        For i As Integer = TabControl1.TabPages(tab).Controls.Count - 1 To 5 Step -1
            con = TabControl1.TabPages(tab).Controls(i)
            TabControl1.TabPages(tab).Controls.Remove(con)
        Next
    End Sub
    Private Sub LeagueEnter_Click(sender As System.Object, e As System.EventArgs)
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        Dim tab As Integer = sender.tag
        Dim comboLeague As ComboBox = TabControl1.TabPages(tab).Controls("combo(2,1)")
        Dim comboWeek As ComboBox = TabControl1.TabPages(tab).Controls("combo(2,2)")
        Dim bottomBox = TabControl1.TabPages(tab).Controls("lbl(1,2)")
        Dim point As New Point(bottomBox.Location.X, bottomBox.Location.Y + bottomBox.Height + 10)
        Dim leagueNumber As Integer
        Dim cleague As League
        Dim z As Integer
        RemoveBonusControls(tab)
        If comboLeague.Text = "" Or comboWeek.Text = "" Then

        Else
            chosenweek = comboWeek.Text
            For i As Integer = 0 To leagueList.Count - 1
                If comboLeague.Text = leagueList(i).getName Then
                    leagueNumber = i
                    chosenLeague = leagueList(i)
                    cleague = leagueList(i)
                End If
            Next

            If weeklyScores.Count = 0 Then
                getGames(leagueList(leagueNumber).getName)
            End If
            If weeklyScores.Count >= chosenweek Then
                z = weeklyScores(chosenweek - 1).Count
                If z > 0 Then
                    createInputBoxes(tab, leagueNumber, point, z)
                    Dim comboTeam As ComboBox
                    Dim textScore As TextBox
                    Dim temp As Match
                    Dim c As String
                    For i As Integer = 1 To z
                        temp = weeklyScores(chosenweek - 1)(i - 1)
                        c = (i + 2).ToString
                        comboTeam = New ComboBox
                        comboTeam = TabControl1.TabPages(tab).Controls("combo(1," + c + ")")
                        comboTeam.Text = temp.gethTeam
                        textScore = New TextBox
                        textScore = TabControl1.TabPages(tab).Controls("txt(2," + c + ")")
                        textScore.Text = temp.gethScore
                        textScore = New TextBox
                        textScore = TabControl1.TabPages(tab).Controls("txt(4," + c + ")")
                        textScore.Text = temp.getaScore
                        comboTeam = New ComboBox
                        comboTeam = TabControl1.TabPages(tab).Controls("combo(5," + c + ")")
                        comboTeam.Text = temp.getaTeam
                    Next
                Else
                    createInputBoxes(tab, leagueNumber, point, cleague.getNoOfTeams / 2)
                End If

            Else
                createInputBoxes(tab, leagueNumber, point, cleague.getNoOfTeams / 2)
            End If

        End If

    End Sub
    Private Sub createInputBoxes(tab As Integer, leagueNumber As Integer, location As Point, inputNumber As Integer)
        Dim TC1 As TabControl = Me.Controls("TabControl1")
        Dim cleague As League = leagueList(leagueNumber)
        Dim startLocation As New Point(10, location.Y)
        Dim tlocation As New Point
        Dim teamNumber As Integer = cleague.getNoOfTeams
        Dim teamList As List(Of String) = cleague.getTeamList
        Dim teamArray(teamNumber - 1) As String
        For i As Integer = 0 To teamNumber - 1
            teamArray(i) = teamList(i)
        Next
        Dim csize = New Size(160, 30)
        Dim tsize = New Size(30, 30)
        Dim lsize = New Size(100, 30)
        Dim c As String
        For i As Integer = 1 To inputNumber
            c = (i + 2).ToString
            tlocation = startLocation
            createCombo("(1," + c + ")", teamArray, csize, tab, tlocation, False)
            tlocation.X = (tlocation.X + csize.Width + 10)
            createTextBox("(2," + c + ")", "", tsize, tab, tlocation)
            tlocation.X = (tlocation.X + tsize.Width + 10)
            createLabel("(3," + c + ")", "", lsize, tab, tlocation)
            tlocation.X = tlocation.X + lsize.Width + 10
            createTextBox("(4," + c + ")", "", tsize, tab, tlocation)
            tlocation.X = (tlocation.X + tsize.Width + 10)
            createCombo("(5," + c + ")", teamArray, csize, tab, tlocation, False)
            tlocation.X = (tlocation.X + csize.Width + 10)
            If chosenLeague.GetType = GetType(Banker) Then
                createCheckBox("(6," + c + ")", "", tsize, tab, tlocation)
            End If
            startLocation.Y += csize.Height + 10
        Next
        createButton("(1," + (c + 1).ToString + ")", "", csize, tab, startLocation)
        tlocation = startLocation
        tlocation.X = (tlocation.X + csize.Width + 10)
        If tab = 0 Then
            Dim button As Button = TC1.TabPages(tab).Controls("btn(1," + (c + 1).ToString + ")")
            button.Text = "Predict"
            AddHandler button.Click, AddressOf Me.Predict_Click
        ElseIf tab = 1 Then
            Dim button As Button = TC1.TabPages(tab).Controls("btn(1," + (c + 1).ToString + ")")
            AddHandler button.Click, AddressOf Me.ScoreEnter_Click
        ElseIf tab = 3 Then
            Dim button As Button = TC1.TabPages(tab).Controls("btn(1," + (c + 1).ToString + ")")
            AddHandler button.Click, AddressOf Me.method2
        End If
        Me.Size = New Size(Me.Size.Width, startLocation.Y + 70 + csize.Height)
        Me.Controls("TabControl1").Size = New Size(Me.Size.Width, startLocation.Y + 70 + csize.Height)
    End Sub
    Private Sub ScoreEnter_Click(sender As System.Object, e As System.EventArgs)
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        Dim tab As Integer = sender.tag
        Dim cleague As League = chosenLeague
        Dim hcombo As ComboBox
        Dim acombo As ComboBox
        Dim hscore As TextBox
        Dim ascore As TextBox
        Dim blanklist As New List(Of Match)
        Dim c As Integer = weeklyScores.Count
        If chosenweek > weeklyScores.Count Then
            For j As Integer = c To chosenweek - 1
                blanklist = New List(Of Match)
                weeklyScores.Add(blanklist)
                MessageBox.Show(weeklyScores.Count)
            Next
        End If
        For i As Integer = 3 To cleague.getNoOfTeams / 2 + 2
            hcombo = TabControl1.TabPages(tab).Controls("combo(1," + i.ToString + ")")
            hscore = TabControl1.TabPages(tab).Controls("txt(2," + i.ToString + ")")
            ascore = TabControl1.TabPages(tab).Controls("txt(4," + i.ToString + ")")
            acombo = TabControl1.TabPages(tab).Controls("combo(5," + i.ToString + ")")
            If Not (hcombo.Text = "" Or acombo.Text = "") Then
                Dim blankmatch As New Match
                blankmatch.sethTeam(hcombo.Text)
                blankmatch.setaTeam(acombo.Text)
                If hscore.Text = "" Then
                    blankmatch.sethScore(0)
                Else
                    blankmatch.sethScore(hscore.Text)
                End If
                If ascore.Text = "" Then
                    blankmatch.setaScore(0)
                Else
                    blankmatch.setaScore(ascore.Text)
                End If
                blankmatch.setaprediction(0)
                blankmatch.sethxG(0)
                blankmatch.setaxG(0)
                weeklyScores(chosenweek - 1).Add(blankmatch)
            End If
        Next
        setGames()
    End Sub
    Private Sub Predict_Click(sender As System.Object, e As System.EventArgs)
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        Dim tab As Integer = sender.tag
        Dim text As String = sender.text
        Dim hcombo As ComboBox
        Dim acombo As ComboBox
        Dim hscore As TextBox
        Dim ascore As TextBox
        Dim tmatch As New Match
        Dim counter, setting As Integer
        Dim w1(8), w2(8), hpred, apred As Decimal
        Dim banker As Boolean
        banker = chosenLeague.GetType = GetType(Banker)
        If Banker Then
            counter = 6
        Else
            counter = 5
        End If

        Dim x As Integer = chosenweek
        ml(setting, w1, w2)
        'MessageBox.Show(Math.Round(w1(0), 2) & "," & Math.Round(w1(1), 2) & "," & Math.Round(w1(2), 2) & "," & Math.Round(w1(3), 2) & "," & Math.Round(w1(4), 2) & "," & Math.Round(w1(5), 2) & "," & Math.Round(w1(6), 2) & "," & Math.Round(w1(7), 2) & "," & Math.Round(w1(8), 2))
        chosenweek = x
        createTable()
        chosenweek = x
        'MessageBox.Show(TabControl1.TabPages(tab).Controls.Count)
        counter = (TabControl1.TabPages(tab).Controls.Count - 6) / counter
        Dim result(4) As Decimal
        result(0) = -10
        result(2) = -10

        For l As Integer = 3 To counter + 2
            hcombo = TabControl1.TabPages(tab).Controls("combo(1," + l.ToString + ")")
            hscore = TabControl1.TabPages(tab).Controls("txt(2," + l.ToString + ")")
            ascore = TabControl1.TabPages(tab).Controls("txt(4," + l.ToString + ")")
            acombo = TabControl1.TabPages(tab).Controls("combo(5," + l.ToString + ")")
            If Not (hcombo.Text = "" Or acombo.Text = "") Then
                hpred = predict(hcombo.Text, acombo.Text)
                'MessageBox.Show(w1(0) & "," & mTform & "," & mLform & "," & mPos & "," & mRTform & "," & mRLform & "," & mXG & "," & xhomeS & "," & xawayC)
                hpred = w1(0) + w1(1) * htform + w1(2) * atform + w1(3) * mPos + w1(4) * hhform + w1(5) * aaform + w1(6) * mXG + w1(7) * xhomeS + w1(8) * xawayC

                apred = w2(0) + w2(1) * htform + w2(2) * atform + w2(3) * mPos + w2(4) * hhform + w2(5) * aaform + w2(6) * mXG + w2(7) * xawayS + w2(8) * xhomeC
                'MessageBox.Show(hpred & "," & apred)
                TabControl1.TabPages(tab).Controls("lbl(3," + l.ToString + ")").Text = exactscore2(hpred, apred).ToString + " , " + Math.Round(holder, 2).ToString
                If hscore.Text <> "" And ascore.Text <> "" Then
                    If exactscore2(hpred, apred) = hscore.Text + ascore.Text / 10 Then
                        TabControl1.TabPages(tab).Controls("lbl(3," + l.ToString + ")").BackColor = Color.Green
                    ElseIf (convScoreDiff(exactscore2(hpred, apred)) > 0 And hscore.Text - ascore.Text > 0) Or (convScoreDiff(exactscore2(hpred, apred)) < 0 And hscore.Text - ascore.Text < 0) Or (convScoreDiff(exactscore2(hpred, apred)) = 0 And hscore.Text - ascore.Text = 0) Then
                        TabControl1.TabPages(tab).Controls("lbl(3," + l.ToString + ")").BackColor = Color.Blue
                    End If
                End If
                'MessageBox.Show(hcombo.Text & " , " & checker)
                If holder2 > result(0) Then
                        result(3) = result(1)
                        result(2) = result(0)
                        result(1) = l
                        result(0) = holder2
                    ElseIf holder2 > result(2) Then
                        result(3) = l
                        result(2) = holder2
                    End If
                End If
        Next
        Dim check As CheckBox = TabControl1.TabPages(tab).Controls("chk(6," + result(1).ToString + ")")
        check.Checked = True
        check = TabControl1.TabPages(tab).Controls("chk(6," + result(3).ToString + ")")
        check.Checked = True
    End Sub
    Private Function MaxWeek() As Integer
        If chosenweek > weeklyScores.Count Then
            Return weeklyScores.Count - 1
        Else
            Return chosenweek - 1
        End If
    End Function
    Private Function exactScore(hName As String, aName As String, prediction As Decimal) As Decimal
        Dim potential, previous, prev(3) As Decimal
        Dim c1, c2, best(2), rounded, best1(3)() As Integer
        Dim cleague As League = chosenLeague
        Dim hw, d, aw As Decimal
        holder = prediction
        rounded = Math.Round(prediction, 0, MidpointRounding.AwayFromZero)
        For k = 0 To cleague.getNoOfTeams - 1
            If leaguetable.league(k).name = hName Then
                c1 = k
            ElseIf leaguetable.league(k).name = aName Then
                c2 = k
            End If
        Next
        If rounded > 0 Then
            best(0) = 1
            best(1) = 0
            best1(0)(0) = 1
            best1(0)(1) = 0
        ElseIf rounded < 0 Then
            best(0) = 0
            best(1) = 1
            best1(1)(0) = 0
            best1(1)(1) = 1
        Else
            best(0) = 1
            best(1) = 1
            best1(2)(0) = 1
            best1(2)(1) = 1
        End If
        previous = 0
        For z = 0 To 2
            prev(z) = 0
        Next
        hw = 0
        d = 0
        aw = 0
        For i As Integer = 0 To 5
            For j As Integer = 0 To 5
                'If (rounded > 0 And i > j) Or (rounded < 0 And i < j) Or (rounded = 0 And i = j) Then
                potential = 1
                potential = poisson(Math.Abs(i - j), Math.Abs(prediction))
                If leaguetable.league(c1).homeP <> 0 Or leaguetable.league(c2).awayP <> 0 Then
                    If leaguetable.league(c1).homeP <> 0 Then
                        potential = potential * (poisson(i, xhomeS) + poisson(j, xhomeC))
                        If leaguetable.league(c2).awayP <> 0 Then
                            potential = potential * (poisson(i, xawayC) + poisson(j, xawayS))
                        End If
                    Else
                        potential = potential * (poisson(i, xawayC) + poisson(j, xawayS))
                    End If
                End If
                If potential > previous Then
                    best(0) = i
                    best(1) = j
                    previous = potential
                    'holder2 = potential
                End If
                'End If
                ' If i > j Then
                'hw += potential
                'If potential > prev(0) Then
                'best1(0)(0) = i
                'best1(0)(1) = j
                'prev(0) = potential
                'holder2 = potential
                'End If
                ' ElseIf i < j Then
                'aw += potential
                'If potential > prev(1) Then
                'best1(1)(0) = i
                'best1(1)(1) = j
                'prev(1) = potential
                'holder2 = potential
                'End If
                'Else
                'd += potential
                'If potential > prev(2) Then
                'best1(2)(0) = i
                'best1(2)(1) = j
                'prev(2) = potential
                '      'holder2 = potential
                'End If
                'End If
            Next
        Next
        ' If hw >= aw And hw >= d Then
        '    Return best1(0)(0) + best1(0)(1) / 10
        'ElseIf aw >= d Then
        '    Return best1(1)(0) + best1(1)(1) / 10
        'Else
        '    Return best1(2)(0) + best1(2)(1) / 10
        'End If


        Return (best(0)) + (best(1)) / 10
    End Function
    Private Function poisson(k As Integer, mean As Decimal)
        Return ((Math.Pow(mean, k)) * (Math.Exp(-mean))) / factorial(k)
    End Function
    Private Function factorial(x As Integer)
        Dim fact As Double
        fact = 1
        If x <> 0 Then
            For i As Integer = 1 To x
                fact = fact * i
            Next
        End If
        Return fact
    End Function
    Private Function predict(hName As String, aName As String) As Decimal
        Dim xd, xdiff, backPredict, a, b, relTform, relLform As Decimal
        hhform = 0
        aaform = 0
        htform = 0
        atform = 0
        relTform = 0
        relLform = 0
        xd = 0
        xhomeS = 0
        xawayS = 0
        xhomeC = 0
        xawayC = 0
        Dim c1, c2, h1, a1, i, j, gd, hpoint, apoint, pointgap As Integer
        c1 = 5
        c2 = 5
        h1 = 5
        a1 = 5
        Dim tmatch As Match
        Dim cleague As League = chosenLeague
        j = MaxWeek()

        If chosenweek <> 0 Then
            While weeklyScores(j).Count = 0
                j -= 1
            End While
            i = 0
            While (c1 > 0 Or c2 > 0 Or h1 > 0 Or a1 > 0) And j > -1
                tmatch = New Match
                tmatch = weeklyScores(j)(i)
                backPredict = tmatch.getaprediction
                gd = tmatch.gethScore - tmatch.getaScore
                pointCalc(gd, hpoint, apoint)
                If Math.Abs(gd) > 0 And Math.Abs(backPredict) >= 0.5 Then
                    a = 0
                Else
                    a = (gd - backPredict)
                End If
                'MessageBox.Show(tmatch.gethxG)
                If tmatch.gethxG = 0 And tmatch.getaxG = 0 Then
                    xdiff = 0
                Else
                    xdiff = gd - (tmatch.gethxG - tmatch.getaxG)
                End If

                If (tmatch.gethTeam = hName Or tmatch.getaTeam = hName) And (c1 > 0 Or h1 > 0) Then
                    If tmatch.gethTeam = hName Then
                        If c1 > 0 Then
                            updatePoint(htform, hpoint, c1)
                            relTform += a
                            xd += xdiff
                        End If
                        If h1 > 0 Then
                            updatePoint(hhform, hpoint, h1)

                            relLform += a

                            If tmatch.gethxG = 0 And tmatch.getaxG = 0 Then
                                xhomeS += tmatch.gethScore
                                xhomeC += tmatch.getaScore
                            Else
                                xhomeS += tmatch.gethxG
                                xhomeC += tmatch.getaxG
                            End If


                        End If
                    Else
                        If c1 > 0 Then
                            updatePoint(htform, apoint, c1)
                            relTform -= a
                            xd -= xdiff
                        End If
                    End If
                End If
                If (tmatch.getaTeam = aName Or tmatch.gethTeam = aName) And (c2 > 0 Or a1 > 0) Then
                    If tmatch.gethTeam = aName Then
                        If c2 > 0 Then
                            updatePoint(atform, hpoint, c2)
                            relTform += a
                            xd += xdiff
                        End If
                    Else
                        If c2 > 0 Then
                            updatePoint(atform, apoint, c2)
                            relTform -= a
                            xd -= xdiff
                        End If
                        If a1 > 0 Then
                            updatePoint(aaform, apoint, a1)
                            
                            relLform -= a


                            If tmatch.gethxG = 0 And tmatch.getaxG = 0 Then
                                xawayS += tmatch.getaScore
                                xawayC += tmatch.gethScore
                            Else
                                xawayS += tmatch.getaxG
                                xawayC += tmatch.gethxG
                            End If

                        End If
                    End If

                End If
                i += 1
                If i = weeklyScores(j).Count Then
                    j -= 1
                    i = 0
                End If
            End While
        End If

        fixWeightForm(htform, c1, 5)
        fixWeightForm(atform, c2, 5)
        fixWeightForm(hhform, h1, 5)
        fixWeightForm(aaform, a1, 5)
        If h1 <> 5 Then

            xhomeS = xhomeS / (5 - h1)
            xhomeC = xhomeC / (5 - h1)
        Else

        End If
        If a1 <> 5 Then

            xawayS = xawayS / (5 - a1)
            xawayC = xawayC / (5 - a1)
        Else

        End If
        c1 = 0
        c2 = 0
        pointgap = 1
        For k = 0 To cleague.getNoOfTeams - 1
            If leaguetable.league(k).name = hName Then
                c1 = k
            ElseIf leagueTable.league(k).name = aName Then
                c2 = k
            End If
        Next
        holder2 = leaguetable.league(c1).points - leaguetable.league(c2).points
        checker = htform.ToString & " , " & atform.ToString & " , " & hhform.ToString & "," & aaform.ToString
        'holder2 = c2 - c1
        'holder2 = htform - atform
        'MessageBox.Show((htform - atform).ToString & "," & (hhform - aaform).ToString & "," & (((c2 - c1) / 23) * position).ToString)
        mTform = htform - atform
        mLform = hhform - aaform
        mPos = c2 - c1
        mRTform = relTform
        mRLform = relLform
        mXG = xd
        Return (((htform - atform) * tform) + ((hhform - aaform) * lform) + (((c2 - c1) / 19) * position)) * 3 + homeAdv
        If leaguetable.league(c1).homeP + leaguetable.league(c1).awayP > 0 And leaguetable.league(c2).homeP + leaguetable.league(c2).awayP > 0 Then
            'Return ((htform - atform) * tform) + ((hhform - aaform) * lform) + (((table(c1).points - table(c2).points) / pointgap) * position) + ((table(c1).gd / table(c1).played) - (table(c2).gd / table(c2).played)) * goalDiff + homeAdv
            'Return (((table(c1).gd / table(c1).played) - (table(c2).gd / table(c2).played)) * goalDiff) ^ 3 + ((htform - atform) * tform) + ((hhform - aaform) * lform) + (((c2 - c1) / 23) * position)
        Else
            'Return (((htform - atform) * tform) + ((hhform - aaform) * lform) + (((c2 - c1) / 23) * position)) * 3 + homeAdv
            'Return 0
        End If



    End Function
    Private Sub updatePoint(ByRef form As Decimal, ByRef point As Decimal, ByRef counter As Integer)
        form += point
        counter += -1
    End Sub
    Private Sub fixWeightForm(ByRef form As Decimal, ByRef counter As Integer, ByRef i As Integer)
        If counter <> i Then
            form = form / (3 * (i - counter))
        End If
    End Sub
    Private Sub createTable()
        Dim cl As League = chosenLeague
        Dim tempEntry As TableEntry
        Dim teamlist As List(Of String) = cl.getTeamList
        Dim gd, hpoints, apoints As Integer
        Dim tmatch As Match
        leaguetable.league.Clear()
        leaguetable.homeLG = 0
        leaguetable.homeLNum = 0
        leaguetable.homeWG = 0
        leaguetable.homeWNum = 0
        leaguetable.drawG = 0
        leaguetable.drawNum = 0
        For i As Integer = 0 To cl.getNoOfTeams - 1
            tempEntry = New TableEntry
            tempEntry.name = teamlist(i)
            tempEntry.homeP = 0
            tempEntry.awayP = 0
            tempEntry.points = 0
            tempEntry.gd = 0
            tempEntry.homeScored = 0
            tempEntry.homeConceded = 0
            tempEntry.awayConceded = 0
            tempEntry.awayScored = 0
            leaguetable.league.Add(tempEntry)
        Next
        tempEntry = New TableEntry
        For i As Integer = 0 To MaxWeek()
            If weeklyScores(i).Count > 0 Then
                For j As Integer = weeklyScores(i).Count - 1 To 0 Step -1
                    tmatch = weeklyScores(i)(j)
                    gd = tmatch.gethScore - tmatch.getaScore
                    pointCalc(gd, hpoints, apoints)
                    If gd > 0 Then
                        leaguetable.homeWNum += 1
                        leaguetable.homeWG += (tmatch.gethScore - tmatch.getaScore)
                    ElseIf gd < 0 Then
                        leaguetable.homeLNum += 1
                        leaguetable.homeLG += (tmatch.getaScore - tmatch.gethScore)
                    Else
                        leaguetable.drawNum += 1
                        leaguetable.drawG += (tmatch.gethScore + tmatch.getaScore)
                    End If
                    For k As Integer = 0 To leaguetable.league.Count - 1
                        If leaguetable.league(k).name = tmatch.gethTeam Then
                            tempEntry = leaguetable.league(k)
                            tempEntry.homeP += 1
                            tempEntry.points += hpoints
                            tempEntry.gd += gd
                            tempEntry.homeScored += tmatch.gethScore
                            tempEntry.homeConceded += tmatch.getaScore
                            leaguetable.league(k) = tempEntry
                            tempEntry = New TableEntry
                        ElseIf leagueTable.league(k).name = tmatch.getaTeam Then
                            tempEntry = leaguetable.league(k)
                            tempEntry.awayP += 1
                            tempEntry.points += apoints
                            tempEntry.gd -= gd
                            tempEntry.awayScored += tmatch.getaScore
                            tempEntry.awayConceded += tmatch.gethScore
                            leaguetable.league(k) = tempEntry
                            tempEntry = New TableEntry
                        End If
                    Next
                Next
            End If
        Next
        For i = 0 To leaguetable.league.Count - 2
            For j = 0 To leaguetable.league.Count - 2 - i
                If leaguetable.league(j).points < leaguetable.league(j + 1).points Or ((leaguetable.league(j).points = leaguetable.league(j + 1).points) And (leaguetable.league(j).gd < leaguetable.league(j + 1).gd)) Then
                    tempEntry = New TableEntry
                    tempEntry = leaguetable.league(j)
                    leaguetable.league(j) = leaguetable.league(j + 1)
                    leaguetable.league(j + 1) = tempEntry
                End If
            Next
        Next
    End Sub
    Private Sub pointCalc(gd As Integer, ByRef hpoints As Integer, ByRef apoints As Integer)
        If gd > 0 Then
            hpoints = 3
            apoints = 0
        ElseIf gd = 0 Then
            hpoints = 1
            apoints = 1
        Else
            hpoints = 0
            apoints = 3
        End If
    End Sub
    Private Sub createLabel(name As String, text As String, size As Size, tab As Integer, location As Point)
        Dim lbl As New Label
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        lbl.Name = "lbl" + name
        lbl.Text = text
        lbl.TextAlign = ContentAlignment.MiddleCenter
        lbl.BackColor = Color.White
        lbl.BorderStyle = BorderStyle.FixedSingle
        TabControl1.TabPages(tab).Controls.Add(lbl)
        sizeLabel(TabControl1.TabPages(tab).Controls(lbl.Name), size, location)
    End Sub
    Private Sub sizeLabel(label As Label, size As Size, location As Point)
        label.Width = size.Width
        label.Height = size.Height
        label.Location = location
    End Sub
    Private Sub createCombo(name As String, options() As String, size As Size, tab As Integer, location As Point, editable As Boolean)
        Dim combo As New ComboBox
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        combo.Name = "combo" + name
        combo.BackColor = Color.White
        combo.Tag = tab
        For i As Integer = 0 To options.Length - 1
            combo.Items.Add(options(i))
        Next
        If editable = False Then
            AddHandler combo.KeyPress, AddressOf combobox_keypress
        End If
        TabControl1.TabPages(tab).Controls.Add(combo)
        sizeCombo(TabControl1.TabPages(tab).Controls(combo.Name), size, location)
    End Sub
    Private Sub sizeCombo(combo As ComboBox, size As Size, location As Point)
        combo.Width = size.Width
        combo.Height = size.Height
        combo.Location = location
    End Sub
    Private Sub combobox_keypress(sender As System.Object, e As System.Windows.Forms.KeyPressEventArgs)
        e.Handled = True
    End Sub
    Private Sub createButton(name As String, text As String, size As Size, tab As Integer, location As Point)
        Dim btn As New Button
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        btn.Name = "btn" + name
        btn.Text = text
        btn.Tag = tab
        btn.TextAlign = ContentAlignment.MiddleCenter
        btn.BackColor = Color.White
        TabControl1.TabPages(tab).Controls.Add(btn)
        sizeButton(TabControl1.TabPages(tab).Controls(btn.Name), size, location)
    End Sub
    Private Sub sizeButton(btn As Button, size As Size, location As Point)
        btn.Width = size.Width
        btn.Height = size.Height
        btn.Location = location
    End Sub
    Private Sub createTextBox(name As String, text As String, size As Size, tab As Integer, location As Point)
        Dim txt As New TextBox
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        txt.Name = "txt" + name
        txt.Text = text
        txt.TextAlign = HorizontalAlignment.Center
        txt.BackColor = Color.White
        TabControl1.TabPages(tab).Controls.Add(txt)
        sizeTextBox(TabControl1.TabPages(tab).Controls(txt.Name), size, location)
    End Sub
    Private Sub sizeTextBox(txt As TextBox, size As Size, location As Point)
        txt.Width = size.Width
        txt.Height = size.Height
        txt.Location = location
    End Sub
    Private Sub createCheckBox(name As String, text As String, size As Size, tab As Integer, location As Point)
        Dim chk As New CheckBox
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        chk.Name = "chk" + name
        chk.Text = text
        chk.TextAlign = HorizontalAlignment.Center
        TabControl1.TabPages(tab).Controls.Add(chk)
        sizeCheckBox(TabControl1.TabPages(tab).Controls(chk.Name), size, location)
    End Sub
    Private Sub sizeCheckBox(chk As CheckBox, size As Size, location As Point)
        chk.Width = size.Width
        chk.Height = size.Height
        chk.Location = location
    End Sub
    Private Sub getLeagues()
        weeklyScores = New List(Of List(Of Match))
        Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader("C:\Users\calum\OneDrive\Documents\Predictor\Leagues.txt")
        Dim blankLeague As New League
        Dim blankBanker As New Banker
        Dim teamList As New List(Of String)
        Dim c As Integer
        Dim x As Integer
        Do Until reader.EndOfStream
            If reader.ReadLine = "" Then
                If reader.ReadLine = "Banker" Then
                    blankBanker = New Banker
                    blankBanker.setNumOfBankers(reader.ReadLine)
                    blankBanker.setCorrectResult(reader.ReadLine)
                    blankBanker.setCorrectScore(reader.ReadLine)
                    blankBanker.setIncorrectScore(reader.ReadLine)
                    leagueList.Add(blankBanker)
                Else
                    blankLeague = New League
                    leagueList.Add(blankLeague)
                End If
                c = leagueList.Count - 1
                leagueList(c).setName(reader.ReadLine)
                x = reader.ReadLine
                leagueList(c).setNoOfTeams(x)
                leagueList(c).setPointForResult(reader.ReadLine)
                leagueList(c).setPointForScore(reader.ReadLine)
                teamList = New List(Of String)
                For i As Integer = 1 To x
                    teamList.Add(reader.ReadLine)
                Next
                teamList.Add("++")
                teamList.Add("==")
                teamList.Add("--")
                leagueList(c).setTeamList(teamList)
            End If
        Loop
        reader.Close()
    End Sub
    Private Sub getGames(league As String)
        Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader("C:\Users\calum\OneDrive\Documents\Predictor\" & league & ".txt")
        Dim tstring As String
        Dim blankmatch As New Match
        Dim blanklist As New List(Of Match)
        Dim x As Integer
        Do Until reader.EndOfStream
            tstring = reader.ReadLine
            If tstring = "n" Then
                weeklyScores.Add(blanklist)
                blanklist = New List(Of Match)
            ElseIf Not (tstring = "") Then
                blankmatch = New Match
                blanklist.Add(blankmatch)
                x = blanklist.Count - 1
                Dim sarray As String() = tstring.Split(New Char() {","c})
                blanklist(x).sethTeam(sarray(0))
                blanklist(x).sethScore(sarray(1))
                blanklist(x).setaScore(sarray(2))
                blanklist(x).setaTeam(sarray(3))
                blanklist(x).setaprediction(sarray(4))
                If sarray.Length > 5 Then
                    blanklist(x).sethxG(sarray(5))
                    blanklist(x).setaxG(sarray(6))
                End If
                If sarray.Length > 7 Then
                    blanklist(x).sethOdd(sarray(7))
                    blanklist(x).setdOdd(sarray(8))
                    blanklist(x).setaOdd(sarray(9))
                End If

            End If

        Loop
        weeklyScores.Add(blanklist)
        reader.Close()
    End Sub
    Private Sub setGames()
        Dim cleague As League = chosenLeague
        Dim league As String = cleague.getName
        Dim writer As StreamWriter = My.Computer.FileSystem.OpenTextFileWriter("C:\Users\calum\OneDrive\Documents\Predictor\" & league & ".txt", False)
        Dim c As Integer
        Dim temp As Match
        For i As Integer = 0 To weeklyScores.Count - 1
            c = weeklyScores(i).Count
            For j As Integer = 0 To c - 1
                temp = weeklyScores(i)(j)
                writer.WriteLine(temp.gethTeam + "," + (temp.gethScore).ToString + "," + (temp.getaScore).ToString + "," + temp.getaTeam + "," + Math.Round(temp.getaprediction, 3).ToString + "," + Math.Round(temp.gethxG, 2).ToString + "," + Math.Round(temp.getaxG, 2).ToString + "," + temp.gethOdd.ToString + "," + temp.getdOdd.ToString + "," + temp.getaOdd.ToString)
            Next
            writer.WriteLine("n")
        Next

        writer.Close()
        MessageBox.Show("Done")
    End Sub
    Private Function convScoreDiff(x As Decimal)
        Return Int(x) - ((x - Int(x)) * 10)
    End Function
    Private Sub dataSpread(sender As System.Object, e As System.EventArgs)
        Dim tmatch As Match
        Dim hName, aName As String
        Dim prediction As Decimal
        Dim result As Integer
        Dim test As New Setting
        Dim cLeague As League
        Dim cBanker As Banker
        Dim noOfTeam As Integer
        Dim banker As Boolean
        Dim blank As List(Of Decimal)
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        Dim tab As Integer = sender.Tag
        Dim collect(13) As List(Of Decimal)
        banker = chosenLeague.GetType = GetType(Banker)
        If banker Then
            cBanker = chosenLeague
            noOfTeam = cBanker.getNoOfTeams
        Else
            cLeague = chosenLeague
            noOfTeam = cLeague.getNoOfTeams
        End If
        For i As Integer = 0 To collect.Count - 1
            blank = New List(Of Decimal)
            collect(i) = blank
        Next
        For i As Decimal = 0 To weeklyScores.Count - 1
            chosenweek = i
            createTable()
            For j As Integer = 0 To weeklyScores(i).Count - 1
                tmatch = New Match
                tmatch = weeklyScores(i)(j)
                hName = tmatch.gethTeam
                aName = tmatch.getaTeam
                result = tmatch.gethScore - tmatch.getaScore
                prediction = predict(hName, aName)
                collect(result + 6).Add(prediction)
            Next
        Next
        Dim med, IQR, q1, q3 As Decimal
        For i As Integer = 0 To 11
            collect(i).Sort()
            stats(collect(i), med, IQR, q3, q1)
            TabControl1.TabPages(tab).Controls("lbl(3," + (i + 3).ToString + ")").Text = (i - 6).ToString + " = " + Math.Round(q1, 2).ToString + " to " + Math.Round(med, 2).ToString + " to " + Math.Round(q3, 2).ToString
        Next
    End Sub
    Private Sub stats(ByVal list As List(Of Decimal), ByRef med As Decimal, ByRef IQR As Decimal, ByRef q3 As Decimal, ByRef q1 As Decimal)
        Dim tlist1, tlist2 As List(Of Decimal)
        Dim x As Decimal
        If list.Count = 0 Then
            med = 0
            IQR = 0
            q1 = 0
            q3 = 0
        ElseIf list.Count = 1 Then
            med = list(0)
            q1 = med
            q3 = med
            IQR = 0
        Else
            x = (list.Count + 1) \ 2
            med = median(list)
            If list.Count Mod 2 = 0 Then
                tlist1 = list.GetRange(0, x)
            Else
                tlist1 = list.GetRange(0, x - 1)
            End If
            tlist2 = list
            tlist2.RemoveRange(0, x)
            q1 = median(tlist1)
            q3 = median(tlist2)

            IQR = q3 - q1
        End If
    End Sub
    Private Function median(ByVal list As List(Of Decimal)) As Decimal
        Dim x As Decimal
        Dim med As Decimal
        x = (list.Count + 1) \ 2
        med = list(x - 1)
        If list.Count Mod 2 = 0 Then
            med += list(x)
            med = med / 2
        End If
        Return med
    End Function
    Private Sub normDist(ByVal list As List(Of Decimal), ByRef avg As Decimal, ByRef sd As Decimal)
        avg = 0
        sd = 0
        For i As Integer = 0 To list.Count - 1
            avg += list(i)
        Next
        avg = avg / list.Count
        For i As Integer = 0 To list.Count - 1
            sd += Math.Pow((list(i) - avg), 2)
        Next
        sd = Math.Sqrt(sd / list.Count)
        'MessageBox.Show(avg.ToString + "" + sd.ToString)
    End Sub
    Private Function normal3(ByVal value As Decimal, ByVal negative As Normal, ByVal zero As Normal, ByVal positive As Normal)
        Dim negZ, zeroZ, posZ As Decimal
        negZ = normalZ(value, negative.average, negative.sd)
        zeroZ = normalZ(value, zero.average, zero.sd)
        posZ = normalZ(value, positive.average, positive.sd)
        If negZ < zeroZ And negZ < posZ Then
            Return -1
        ElseIf zeroZ < posZ Then
            Return 0
        Else
            Return 1
        End If
    End Function
    Private Function normalZ(ByVal k As Decimal, ByVal x As Decimal, ByVal z As Decimal)
        Return Math.Abs((k - x) / z)
    End Function
    Private Sub method2(sender As System.Object, e As System.EventArgs)
        Dim btn As Button = sender
        Dim tab As Integer = sender.tag
        Dim TabControl1 As TabControl = Me.Controls("TabControl1")
        Dim setting As Integer
        Dim w1(8), w2(8) As Decimal
        ml(setting, w1, w2)
        TabControl1.TabPages(TAB).Controls("lbl(3," + 3.ToString + ")").Text = Setting
    End Sub
    Private Sub ml(ByRef setting As Integer, ByRef w1() As Decimal, ByRef w2() As Decimal)
        Dim x, h, score, z As Integer
        Dim hName, aName As String
        Dim tmatch As Match
        Dim result, prediction, hprediction, aprediction, w3(11), resultpred As Decimal
        x = chosenweek - 1
        Dim a1, a2, b1, b2 As Decimal
        Dim X1_test, X2_test, X3_test As New List(Of List(Of Decimal))
        Dim y1, y2, y3, y_, new_x As New List(Of Decimal)
        Dim g As New List(Of List(Of String))
        Dim new_g As New List(Of String)
        Dim y3_ As New List(Of String)
        Dim betting As Decimal
        h = 0
        betting = 0
        For i As Integer = 0 To x
            a1 = -25
            a2 = -1
            b1 = -25
            b2 = -1
            While X1_test.Count > 110
                X1_test.RemoveRange(0, 1)
                y1.RemoveRange(0, 1)
            End While
            While X2_test.Count > 110
                X2_test.RemoveRange(0, 1)
                y2.RemoveRange(0, 1)
            End While
            'While X3_test.Count > 110
            'X3_test.RemoveRange(0, 1)
            'y3.RemoveRange(0, 1)
            'End While
            If i < 1 Then
                w1(0) = 1.1
                w2(0) = 0
                w3(0) = 1
                For a As Integer = 1 To 10
                    If a <= 8 Then
                        w1(a) = 0
                        w2(a) = 0
                    End If

                    w3(a) = 0
                Next
            Else
                'MessageBox.Show(X_test.Count)
                makeW(w1, X1_test, y1)
                makeW(w2, X2_test, y2)
                makeW(w3, X3_test, y3)
            End If
            'MessageBox.Show(Math.Round(w1(0), 2).ToString + "," + Math.Round(w(1), 2).ToString + "," + Math.Round(w(2), 2).ToString + "," + Math.Round(w(3), 2).ToString + "," + Math.Round(w(4), 2).ToString + "," + Math.Round(w(5), 2).ToString + "," + Math.Round(w(6), 2).ToString)
            If i = 15 Then
                'MessageBox.Show(Math.Round(w1(0), 2) & "," & Math.Round(w1(1), 2) & "," & Math.Round(w1(2), 2) & "," & Math.Round(w1(3), 2) & "," & Math.Round(w1(4), 2) & "," & Math.Round(w1(5), 2) & "," & Math.Round(w1(6), 2) & "," & Math.Round(w1(7), 2) & "," & Math.Round(w1(8), 2))
            End If
            chosenweek = i
            createTable()
            chosenweek = i

            For j As Integer = 0 To weeklyScores(i).Count - 1
                tmatch = New Match
                tmatch = weeklyScores(i)(j)
                hName = tmatch.gethTeam
                aName = tmatch.getaTeam
                result = tmatch.gethScore + tmatch.getaScore / 10
                hprediction = predict(hName, aName)

                new_g = New List(Of String)
                new_g.Add(hName)
                new_g.Add(tmatch.gethScore)
                new_g.Add(tmatch.gethxG)
                g.Add(new_g)
                new_g = New List(Of String)
                new_g.Add(aName)
                new_g.Add(tmatch.getaScore)
                new_g.Add(tmatch.getaxG)
                g.Add(new_g)

                For a = 1 To 3
                    new_x = New List(Of Decimal)
                    new_x.Add(1)
                    new_x.Add(htform)
                    new_x.Add(atform)
                    new_x.Add(mPos)
                    new_x.Add(hhform)
                    new_x.Add(aaform)
                    new_x.Add(0)



                    If a = 1 Then
                        new_x.Add(xhomeS)
                        new_x.Add(xawayC)
                        X1_test.Add(new_x)
                    ElseIf a = 2 Then
                        new_x.Add(xawayS)
                        new_x.Add(xhomeC)
                        X2_test.Add(new_x)
                    Else
                        new_x.Add(xhomeS)
                        new_x.Add(xawayC)
                        new_x.Add(xawayS)
                        new_x.Add(xhomeC)
                        X3_test.Add(new_x)
                    End If


                Next

                'y.Add(Math.Sign(tmatch.gethScore - tmatch.getaScore) * 2)
                y1.Add(tmatch.gethScore)
                y2.Add(tmatch.getaScore)
                y_.Add(tmatch.gethScore + tmatch.getaScore / 10)
                score = tmatch.gethScore - tmatch.getaScore
                If score > 0 Then
                    y3_.Add("W")
                    y3.Add(1)
                ElseIf score < 0 Then
                    y3_.Add("L")
                    y3.Add(-1)
                Else
                    y3_.Add("D")
                    y3.Add(0)
                End If
                hprediction = 0
                aprediction = 0
                resultpred = 0
                For a As Integer = 0 To 10
                    If a <= 8 Then
                        hprediction += X1_test(X1_test.Count - 1)(a) * w1(a)
                        aprediction += X2_test(X2_test.Count - 1)(a) * w2(a)
                    End If
                    resultpred += X3_test(X3_test.Count - 1)(a) * w3(a)
                Next

                prediction = exactscore2(hprediction, aprediction)
                weeklyScores(i)(j).setprediction(prediction)
                weeklyScores(i)(j).setaprediction(hprediction - aprediction)
                If i = 15 Then

                    'MessageBox.Show(setting)
                    'MessageBox.Show(hprediction & " , " & aprediction)
                    'MessageBox.Show(result & " , " & prediction)

                End If
                z = convScoreDiff(result)
                If prediction = result Then
                    ' potentialLookBack += 3
                    setting += 3
                    If weeklyScores(i)(j).gethOdd <> 0 Then
                        If z > 0 Then
                            betting += weeklyScores(i)(j).gethOdd
                        ElseIf z < 0 Then
                            betting += weeklyScores(i)(j).getaOdd
                        Else
                            betting += weeklyScores(i)(j).getdOdd
                        End If
                    End If
                ElseIf (((convScoreDiff(prediction) > 0) And (convScoreDiff(result) > 0)) Or ((convScoreDiff(prediction) < 0) And (convScoreDiff(result) < 0)) Or ((convScoreDiff(prediction) = 0) And (convScoreDiff(result) = 0))) Then
                    'potentialLookBack += 1
                    setting += 1

                    If weeklyScores(i)(j).gethOdd <> 0 Then
                        If z > 0 Then
                            betting += weeklyScores(i)(j).gethOdd
                        ElseIf z < 0 Then
                            betting += weeklyScores(i)(j).getaOdd

                        Else
                            betting += weeklyScores(i)(j).getdOdd

                        End If
                    End If
                Else
                    If weeklyScores(i)(j).gethOdd <> 0 Then
                        betting -= 1
                    End If

                End If
                'MessageBox.Show(betting)
                If (z > 0 And resultpred > 0.5) Or (z = 0 And resultpred <= 0.5 And resultpred >= -0.5) Or (z < 0 And resultpred < -0.5) Then
                    h += 0
                End If

                If holder2 > a1 Then
                    b1 = a1
                    b2 = a2
                    a1 = holder2
                    a2 = j
                ElseIf holder2 > b1 Then
                    b1 = holder2
                    b2 = j
                End If

            Next
            While X1_test.Count > 110
                X1_test.RemoveRange(0, 1)
                y1.RemoveRange(0, 1)
            End While
            While X2_test.Count > 110
                X2_test.RemoveRange(0, 1)
                y2.RemoveRange(0, 1)
            End While
            For k As Integer = 1 To 2
                tmatch = New Match
                If k = 1 Then
                    tmatch = weeklyScores(i)(a2)
                Else
                    tmatch = weeklyScores(i)(b2)
                End If
                result = tmatch.gethScore + tmatch.getaScore / 10
                prediction = tmatch.getprediction
                If prediction = result Then
                    'potentialLookBack += 3
                    'pot += 3
                    setting += 3
                ElseIf (((convScoreDiff(prediction) > 0) And (convScoreDiff(result) > 0)) Or ((convScoreDiff(prediction) < 0) And (convScoreDiff(result) < 0)) Or ((convScoreDiff(prediction) = 0) And (convScoreDiff(result) = 0))) Then
                    'potentialLookBack += 1
                    'pot += 1
                    setting += 1
                Else
                    'potentialLookBack += -1
                    'pot += -1
                    setting -= 1
                End If
            Next


            'TabControl1.TabPages(tab).Controls("lbl(3," + (h + 2).ToString + ")").Text = potentialLookBack.ToString + " - " + pot.ToString
        Next
        'MessageBox.Show(setting)
        chosenweek = x + 1
        If chosenweek < 1 Then
            w1(0) = 1.1
            w2(0) = 0
            w3(0) = 1
            For a As Integer = 1 To 10
                If a <= 8 Then
                    w1(a) = 0
                    w2(a) = 0
                End If

                w3(a) = 0
            Next
        Else
            'MessageBox.Show(X_test.Count)
            makeW(w1, X1_test, y1)
            makeW(w2, X2_test, y2)
            makeW(w3, X3_test, y3)

        End If
        'setting = setting - h
        'MessageBox.Show(h)
        MessageBox.Show(betting)
        MessageBox.Show(X3_test.Count)
        Dim writer1 As StreamWriter = My.Computer.FileSystem.OpenTextFileWriter("C:\Users\calum\OneDrive\Documents\Predictor\X.txt", False)
        Dim writer2 As StreamWriter = My.Computer.FileSystem.OpenTextFileWriter("C:\Users\calum\OneDrive\Documents\Predictor\y.txt", False)
        writer1.WriteLine()
        writer2.WriteLine()
        For i As Integer = 0 To X3_test.Count - 1
            For j As Integer = 1 To X3_test(i).Count - 1
                writer1.Write(X3_test(i)(j).ToString)
                If j < X3_test(i).Count - 1 Then
                    writer1.Write(",")
                End If
            Next
            writer1.WriteLine()
            writer2.Write(y_(i).ToString)
            writer2.WriteLine()
        Next
        writer1.Close()
        writer2.Close()
        Dim writerg As StreamWriter = My.Computer.FileSystem.OpenTextFileWriter("C:\Users\calum\OneDrive\Documents\Predictor\g.txt", False)
        For i As Integer = 0 To g.Count - 1
            For j As Integer = 1 To g(i).Count - 1
                writerg.Write(g(i)(j).ToString)
                If j < g(i).Count - 1 Then
                    writerg.Write(",")
                End If
            Next
            writerg.WriteLine()
        Next
        writerg.Close()
    End Sub
    Private Function inverse(x As Array)
        Dim xs(x.Length - 1, x.Length * 2 - 1) As Decimal
        Dim m, n As Integer
        Dim temp As Decimal
        m = Math.Sqrt(x.Length)
        n = m * 2

        For i As Integer = 0 To m - 1
            For j As Integer = 0 To n - 1
                If j < m Then
                    xs(i, j) = x(i, j)
                ElseIf i + m = j Then
                    xs(i, j) = 1
                Else
                    xs(i, j) = 0
                End If
                'MessageBox.Show(xs(i, j))
            Next
        Next

        For k As Integer = 0 To m - 1
            temp = xs(k, k)
            For j = 0 To n - 1
                xs(k, j) = xs(k, j) / temp
            Next
            For i = k + 1 To m - 1
                temp = xs(i, k)
                For j = 0 To n - 1
                    If j <= k Then
                        xs(i, j) = 0
                    Else
                        xs(i, j) = xs(i, j) - xs(k, j) * temp

                    End If
                Next
            Next

        Next

        For i As Integer = 0 To m - 2
            For j As Integer = i + 1 To m - 1
                temp = xs(i, j)
                For k As Integer = j To n - 1
                    xs(i, k) = xs(i, k) - temp * xs(j, k)
                Next
            Next
        Next

        For i As Integer = 0 To m - 1
            For j As Integer = m To n - 1
                x(i, j - m) = xs(i, j)
            Next
        Next

        Return x
    End Function
    Private Sub makeW(ByRef w() As Decimal, X_test As List(Of List(Of Decimal)), y As List(Of Decimal))
        Dim t(8) As Decimal
        Dim x_inv(8, 8) As Decimal
        Dim allzero As Boolean = True

        For a As Integer = 0 To 8
            allzero = True
            For b As Integer = 0 To 8
                x_inv(a, b) = 0
                For c As Integer = 0 To X_test.Count - 1
                    'MessageBox.Show(X_test(c)(b))
                    x_inv(a, b) += X_test(c)(a) * X_test(c)(b)
                Next
                allzero = allzero And (x_inv(a, b) = 0)
            Next
            x_inv(a, a) += 0.001
            If allzero Then
                x_inv(a, a) = 1
            End If

        Next

        x_inv = inverse(x_inv)
        For a As Integer = 0 To 8
            t(a) = 0
            For b As Integer = 0 To y.Count - 1
                t(a) += X_test(b)(a) * y(b)
            Next
        Next
        For a As Integer = 0 To 8
            w(a) = 0
            For b As Integer = 0 To 8
                w(a) += x_inv(a, b) * t(b)
            Next
        Next
    End Sub
    Private Function exactscore2(hpred As Decimal, apred As Decimal)
        Dim percentage, bestpercent As Decimal
        Dim best(1) As Integer
        bestpercent = 0
        holder = hpred - apred
        If apred < 0 Then
            hpred -= apred
            apred = 0
        End If
        If hpred < 0 Then
            apred -= hpred
            hpred = 0
        End If
        For i As Integer = 0 To 6
            For j As Integer = 0 To 6
                percentage = poisson(i, hpred) * poisson(j, apred)
                'MessageBox.Show(i & "," & j & "," & percentage)
                If percentage > bestpercent Then
                    bestpercent = percentage
                    best(0) = i
                    best(1) = j
                End If
            Next
        Next
        'holder2 = bestpercent
        Return best(0) + best(1) / 10
    End Function
End Class
