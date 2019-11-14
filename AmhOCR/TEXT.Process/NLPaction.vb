﻿
'Copyright ©  Kumneger Hussien, kumneger.h@gmail.com, 2019 GPLv3
Imports System.Text

Public Class NLPaction

    Public Overloads Shared Function GetWordList(ByVal hocrpages As List(Of HocrPage)) As String

        Dim ListOfWord As New List(Of String)
        For Each page In hocrpages
            Dim areas = page.AllocrCarea
            For ar As Integer = 0 To areas.Count - 1

                Dim Pars = areas(ar).AllocrParas
                For pr As Integer = 0 To Pars.Count - 1

                    Dim lins = Pars(pr).AllocrLines
                    For ln As Integer = 0 To lins.Count - 1

                        Dim words = lins(ln).AllocrWords
                        For wd As Integer = 0 To words.Count - 1
                            Dim subwords = words(wd).Text.Split({" "}, StringSplitOptions.RemoveEmptyEntries)

                            If subwords.Count > 0 Then
                                Dim Varifiedwords = subwords.AsEnumerable.Where(Function(X) X.Length >= OCRsettings.MinimumWordLength)

                                If Varifiedwords.Count > 0 Then
                                    ListOfWord.AddRange(Varifiedwords.ToArray)
                                End If
                            End If

                        Next

                    Next

                Next

            Next
        Next




        Return String.Join(Environment.NewLine, ListOfWord.AsEnumerable)

    End Function

    Public Overloads Shared Function GetWordFrequency(ByVal hocrpages As List(Of HocrPage)) As String
        Dim ListOfWordFrequency As New List(Of String)

        Dim ListOfWord As New List(Of String)

        For Each page In hocrpages
            Dim areas = page.AllocrCarea
            For ar As Integer = 0 To areas.Count - 1

                Dim Pars = areas(ar).AllocrParas
                For pr As Integer = 0 To Pars.Count - 1

                    Dim lins = Pars(pr).AllocrLines
                    For ln As Integer = 0 To lins.Count - 1

                        Dim words = lins(ln).AllocrWords
                        For wd As Integer = 0 To words.Count - 1

                            Dim subwords = words(wd).Text.Split({" "}, StringSplitOptions.RemoveEmptyEntries)

                            If subwords.Count > 0 Then
                                For Each wrd In subwords

                                    Dim Varifiedwords = wrd.TrimStart(" ").TrimEnd(" ")

                                    If Varifiedwords.Length >= OCRsettings.MinimumWordLength Then
                                        ListOfWord.Add(Varifiedwords)
                                    End If
                                Next

                            End If


                        Next

                    Next

                Next

            Next
        Next


        Dim wordFrequency = From Word In ListOfWord
                            Select Word
                            Group By wrd = Word Into freq = Group
                            Order By freq.Count Descending



        For Each grp In wordFrequency

            ListOfWordFrequency.Add(grp.wrd + " " + grp.freq.Count.ToString)

        Next



        Return String.Join(Environment.NewLine, ListOfWordFrequency.AsEnumerable)
    End Function




    Public Overloads Shared Function GetWordList(ByVal Spellchk As SpellCheker, ByVal hocrpages As List(Of HocrPage)) As String

        Dim ListOfWord As New List(Of String)

        For Each page In hocrpages
            Dim areas = page.AllocrCarea
            For ar As Integer = 0 To areas.Count - 1

                Dim Pars = areas(ar).AllocrParas
                For pr As Integer = 0 To Pars.Count - 1

                    Dim lins = Pars(pr).AllocrLines
                    For ln As Integer = 0 To lins.Count - 1

                        Dim words = lins(ln).AllocrWords
                        For wd As Integer = 0 To words.Count - 1

                            Dim TextValue As String = words(wd).Text

                            Dim subwords() As String
                            subwords = TextValue.Split({" "}, StringSplitOptions.RemoveEmptyEntries)


                            If subwords.Count > 0 Then
                                TextValue = String.Join(" ", subwords)

                                If Spellchk.Loaded = True Then

                                    If Spellchk.puncs.Count > 0 Then
                                        subwords = TextValue.Split(Spellchk.puncs, StringSplitOptions.RemoveEmptyEntries)
                                        TextValue = String.Join(" ", subwords)
                                    End If

                                    If Spellchk.nums.Count > 0 Then
                                        subwords = TextValue.Split(Spellchk.nums, StringSplitOptions.RemoveEmptyEntries)
                                    End If
                                End If

                                If subwords.Count > 0 Then
                                    For Each wrd In subwords

                                        Dim Varifiedwords = wrd.TrimStart(" ").TrimEnd(" ")

                                        If Varifiedwords.Length >= OCRsettings.MinimumWordLength Then
                                            ListOfWord.Add(Varifiedwords)
                                        End If
                                    Next

                                End If


                            End If

                        Next

                    Next

                Next

            Next
        Next




        Return String.Join(Environment.NewLine, ListOfWord.AsEnumerable)

    End Function

    Public Overloads Shared Function GetWordFrequency(ByVal Spellchk As SpellCheker, ByVal hocrpages As List(Of HocrPage)) As String
        Dim ListOfWordFrequency As New List(Of String)

        Dim ListOfWord As New List(Of String)

        For Each page In hocrpages
            Dim areas = page.AllocrCarea
            For ar As Integer = 0 To areas.Count - 1

                Dim Pars = areas(ar).AllocrParas
                For pr As Integer = 0 To Pars.Count - 1

                    Dim lins = Pars(pr).AllocrLines
                    For ln As Integer = 0 To lins.Count - 1

                        Dim words = lins(ln).AllocrWords
                        For wd As Integer = 0 To words.Count - 1


                            Dim TextValue As String = words(wd).Text

                            Dim subwords() As String
                            subwords = TextValue.Split({" "}, StringSplitOptions.RemoveEmptyEntries)

                            If subwords.Count > 0 Then
                                TextValue = String.Join(" ", subwords)

                                If Spellchk.Loaded = True Then

                                    If Spellchk.puncs.Count > 0 Then
                                        subwords = TextValue.Split(Spellchk.puncs, StringSplitOptions.RemoveEmptyEntries)
                                        TextValue = String.Join(" ", subwords)
                                    End If

                                    If Spellchk.nums.Count > 0 Then
                                        subwords = TextValue.Split(Spellchk.nums, StringSplitOptions.RemoveEmptyEntries)
                                    End If
                                End If


                                If subwords.Count > 0 Then
                                    For Each wrd In subwords

                                        Dim Varifiedwords = wrd.TrimStart(" ").TrimEnd(" ")

                                        If Varifiedwords.Length >= OCRsettings.MinimumWordLength Then
                                            ListOfWord.Add(Varifiedwords)
                                        End If
                                    Next

                                End If
                            End If


                        Next

                    Next

                Next

            Next
        Next


        Dim wordFrequency = From Word In ListOfWord
                            Select Word
                            Group By wrd = Word Into freq = Group
                            Order By freq.Count Descending



        For Each grp In wordFrequency

            ListOfWordFrequency.Add(grp.wrd + " " + grp.freq.Count.ToString)

        Next



        Return String.Join(Environment.NewLine, ListOfWordFrequency.AsEnumerable)
    End Function

    Public Overloads Shared Function GetSentencesList(ByVal hocrpages As List(Of HocrPage)) As String
        Dim ListOfSentencesFrequency As New List(Of String)

        Dim ListOfWordInParagraph As New List(Of String)


        Dim SequenceOfWord As String = ""

        For Each page In hocrpages
            Dim areas = page.AllocrCarea
            For ar As Integer = 0 To areas.Count - 1

                Dim Pars = areas(ar).AllocrParas
                For pr As Integer = 0 To Pars.Count - 1
                    Dim ListOfWord As New List(Of String)
                    Dim lins = Pars(pr).AllocrLines
                    For ln As Integer = 0 To lins.Count - 1

                        Dim words = lins(ln).AllocrWords
                        For wd As Integer = 0 To words.Count - 1

                            Dim subwords() As String
                            subwords = words(wd).Text.Split({" "}, StringSplitOptions.RemoveEmptyEntries)

                            If subwords.Count > 0 Then
                                ListOfWord.AddRange(subwords.ToArray)
                            End If


                        Next

                    Next

                    If ListOfWord.Count > 0 Then
                        SequenceOfWord = String.Join(" ", ListOfWord.AsEnumerable)
                        Dim subwords() = SequenceOfWord.Split({"።", "::", "፡፡", "?"}, StringSplitOptions.RemoveEmptyEntries)
                        If subwords.Count > 0 Then
                            ListOfWordInParagraph.AddRange(subwords.ToArray)
                        End If

                    End If
                Next

            Next
        Next




        Return String.Join(Environment.NewLine, ListOfWordInParagraph.AsEnumerable)


    End Function

    Public Overloads Shared Function GetSentencesList(ByVal Spellchk As SpellCheker, ByVal hocrpages As List(Of HocrPage)) As String
        Dim ListOfSentencesFrequency As New List(Of String)

        Dim ListOfWordInParagraph As New List(Of String)


        Dim SequenceOfWord As String = ""

        For Each page In hocrpages
            Dim areas = page.AllocrCarea
            For ar As Integer = 0 To areas.Count - 1

                Dim Pars = areas(ar).AllocrParas
                For pr As Integer = 0 To Pars.Count - 1
                    Dim ListOfWord As New List(Of String)
                    Dim lins = Pars(pr).AllocrLines
                    For ln As Integer = 0 To lins.Count - 1

                        Dim words = lins(ln).AllocrWords
                        For wd As Integer = 0 To words.Count - 1

                            Dim subwords() As String
                            subwords = words(wd).Text.Split({" "}, StringSplitOptions.RemoveEmptyEntries)

                            If subwords.Count > 0 Then
                                ListOfWord.AddRange(subwords.ToArray)
                            End If


                        Next

                    Next

                    If ListOfWord.Count > 0 Then
                        SequenceOfWord = String.Join(" ", ListOfWord.AsEnumerable)
                        Dim subwords() = SequenceOfWord.Split({"።", "::", "፡፡", "?"}, StringSplitOptions.RemoveEmptyEntries)
                        If subwords.Count > 0 Then
                            For Each wrd In subwords
                                SequenceOfWord = wrd.TrimStart(Spellchk.puncs).TrimStart(" ")
                                SequenceOfWord = SequenceOfWord.TrimEnd(Spellchk.puncs).TrimEnd(" ")
                                If SequenceOfWord.Length >= OCRsettings.MinimumWordLength Then
                                    ListOfWordInParagraph.Add(SequenceOfWord)
                                End If
                            Next

                        End If

                    End If
                Next

            Next
        Next




        Return String.Join(Environment.NewLine, ListOfWordInParagraph.AsEnumerable)


    End Function
End Class