Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports EventTransmission.PropertyChanger

Public Class Form1

#Region "Properties"
    Private isPassiveParent As Boolean = True
    Private childPropertyName As String
    Private dollPropertyName As String

    ' Self-Reporting Classes
    Private loudChildClass As ChildLoud
    Private otherLoudChildClass As ChildLoud
    Private isOtherLoudChildClassUsed As Boolean = False

    Private WithEvents passiveParentClass As ParentPassive

    Private passiveParentClassMyChildMyProperty As String
    Private passiveParentClassMyChildMyDollMyDollProperty As String

    ' Non-Self-Reporting Classes
    Private quietChildClass As ChildQuiet
    Private otherQuietChildClass As ChildQuiet
    Private isOtherQuietChildClassUsed As Boolean = False

    Private WithEvents listeningParentClass As ParentListening

#End Region

#Region "Initialization"
    Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        ' Initialize Classes
        InitializePassiveParentSetup()
        InitializeListeningParentSetup()

        ' Initialize Form 
        InitializeForm(isPassiveParent)
    End Sub

    Private Sub InitializePassiveParentSetup()
        loudChildClass = New ChildLoud
        passiveParentClass = New ParentPassive(loudChildClass)

        otherLoudChildClass = New ChildLoud
        otherLoudChildClass.myProperty = "Other Loud Child"
        otherLoudChildClass.myDoll.myDollProperty = "Other Loud Doll"
    End Sub

    Private Sub InitializeListeningParentSetup()
        quietChildClass = New ChildQuiet
        listeningParentClass = New ParentListening(quietChildClass)

        otherQuietChildClass = New ChildQuiet
        otherQuietChildClass.myProperty = "Other Quiet Child"
        otherQuietChildClass.myDoll.myDollProperty = "Other Quiet Doll"
    End Sub

    Private Sub InitializeForm(ByVal isPassiveParent As Boolean)
        Me.isPassiveParent = isPassiveParent

        UpdateForm()
    End Sub
#End Region

#Region "Methods: Private"
    Private Sub UpdateForm()
        If isPassiveParent Then
            lblParent.Text = "Passive Parent"
            If isOtherLoudChildClassUsed Then
                lblCurrentProperty.Text = otherLoudChildClass.myProperty
                lblCurrentPropertyDoll.Text = otherLoudChildClass.myDoll.myDollProperty
            Else
                lblCurrentProperty.Text = loudChildClass.myProperty
                lblCurrentPropertyDoll.Text = loudChildClass.myDoll.myDollProperty
            End If

            childPropertyName = NameOfProp(Function() passiveParentClass.myChild.myProperty)
            dollPropertyName = NameOfProp(Function() passiveParentClass.myChild.myDoll.myDollProperty)
        Else
            lblParent.Text = "Listening Parent"
            If isOtherQuietChildClassUsed Then
                lblCurrentProperty.Text = otherQuietChildClass.myProperty
                lblCurrentPropertyDoll.Text = otherQuietChildClass.myDoll.myDollProperty
            Else
                lblCurrentProperty.Text = quietChildClass.myProperty
                lblCurrentPropertyDoll.Text = quietChildClass.myDoll.myDollProperty
            End If

            childPropertyName = NameOfProp(Function() listeningParentClass.myChild.myProperty)

            ' Note that the property name returned from the attached handler includes any parent properties.
            dollPropertyName = NameOfProp(Function() listeningParentClass.myChild.myDoll) & "." & NameOfProp(Function() listeningParentClass.myChild.myDoll.myDollProperty)
        End If

        txtBxNewProperty.Text = ""
        txtBxNewPropertyDoll.Text = ""
    End Sub

    Private Sub UpdateChildClasses()
        If isPassiveParent Then
            If isOtherLoudChildClassUsed Then
                otherLoudChildClass.myProperty = txtBxNewProperty.Text
            Else
                loudChildClass.myProperty = txtBxNewProperty.Text
            End If
        Else
            If isOtherQuietChildClassUsed Then
                otherQuietChildClass.myProperty = txtBxNewProperty.Text
            Else
                quietChildClass.myProperty = txtBxNewProperty.Text
            End If
        End If
    End Sub

    Private Sub UpdateDollClasses()
        If isPassiveParent Then
            If isOtherLoudChildClassUsed Then
                otherLoudChildClass.myDoll.myDollProperty = txtBxNewPropertyDoll.Text
            Else
                loudChildClass.myDoll.myDollProperty = txtBxNewPropertyDoll.Text
            End If
        Else
            If isOtherQuietChildClassUsed Then
                otherQuietChildClass.myDoll.myDollProperty = txtBxNewPropertyDoll.Text
            Else
                quietChildClass.myDoll.myDollProperty = txtBxNewPropertyDoll.Text
            End If
        End If
    End Sub

    Private Sub SwapChildClasses()
        If isPassiveParent Then
            If isOtherLoudChildClassUsed Then
                passiveParentClass.SwapChildren(loudChildClass)
                isOtherLoudChildClassUsed = False
            Else
                passiveParentClass.SwapChildren(otherLoudChildClass)
                isOtherLoudChildClassUsed = True
            End If
        Else
            If isOtherQuietChildClassUsed Then
                listeningParentClass.SwapChildren(quietChildClass)
                isOtherQuietChildClassUsed = False
            Else
                listeningParentClass.SwapChildren(otherQuietChildClass)
                isOtherQuietChildClassUsed = True
            End If
        End If

        UpdateForm()
    End Sub

    Private Sub SwapParentClasses()
        If isPassiveParent Then
            isPassiveParent = False
        Else
            isPassiveParent = True
        End If

        UpdateForm()
    End Sub

    Private Function ChildChangeNotification() As String
        Dim childMessage As String = ""

        If isPassiveParent Then
            If isOtherLoudChildClassUsed Then
                childMessage = "Child(" & otherLoudChildClass.ToString &
                                ").myProperty: " & otherLoudChildClass.myProperty & vbCrLf
            Else
                childMessage = "Child(" & loudChildClass.ToString &
                                ").myProperty: " & loudChildClass.myProperty & vbCrLf
            End If

            childMessage &= "Parent(" & passiveParentClass.ToString &
                            ").myChild(" & passiveParentClass.myChild.ToString &
                            ").myProperty: " & passiveParentClass.myChild.myProperty
        Else
            If isOtherQuietChildClassUsed Then
                childMessage = "Child(" & otherQuietChildClass.ToString &
                                ").myProperty: " & otherQuietChildClass.myProperty & vbCrLf
            Else
                childMessage = "Child(" & quietChildClass.ToString &
                                ").myProperty: " & quietChildClass.myProperty & vbCrLf
            End If

            childMessage &= "Parent(" & listeningParentClass.ToString &
                            ").myChild(" & listeningParentClass.myChild.ToString &
                            ").myProperty: " & listeningParentClass.myChild.myProperty
        End If

        Return childMessage
    End Function

    Private Function DollChangeNotification() As String
        Dim childMessage As String = ""

        If isPassiveParent Then
            If isOtherLoudChildClassUsed Then
                childMessage = "Child(" & otherLoudChildClass.ToString &
                                ").myDoll(" & otherLoudChildClass.myDoll.ToString &
                                ").myDollProperty: " & otherLoudChildClass.myDoll.myDollProperty & vbCrLf
            Else
                childMessage = "Child(" & loudChildClass.ToString &
                                ").myDoll(" & loudChildClass.myDoll.ToString &
                                ").myDollProperty: " & loudChildClass.myDoll.myDollProperty & vbCrLf
            End If

            childMessage &= "Parent(" & passiveParentClass.ToString &
                            ").myChild(" & passiveParentClass.myChild.ToString &
                            ").myDoll(" & passiveParentClass.myChild.myDoll.ToString &
                            ").myDollProperty: " & passiveParentClass.myChild.myDoll.myDollProperty
        Else
            If isOtherQuietChildClassUsed Then
                childMessage = "Child(" & otherQuietChildClass.ToString &
                                ").myDoll(" & otherQuietChildClass.myDoll.ToString &
                                ").myDollProperty: " & otherQuietChildClass.myDoll.myDollProperty & vbCrLf
            Else
                childMessage = "Child(" & quietChildClass.ToString &
                                ").myDoll(" & quietChildClass.myDoll.ToString &
                                ").myDollProperty: " & quietChildClass.myDoll.myDollProperty & vbCrLf
            End If

            childMessage &= "Parent(" & listeningParentClass.ToString &
                            ").myChild(" & listeningParentClass.myChild.ToString &
                            ").myDoll(" & listeningParentClass.myChild.myDoll.ToString &
                            ").myDollProperty: " & listeningParentClass.myChild.myDoll.myDollProperty
        End If

        Return childMessage
    End Function
#End Region

#Region "Event Handlers"
    Private Sub NotifyChildChanging(sender As Object, e As PropertyChangingEventArgs) Handles passiveParentClass.PropertyChanging, listeningParentClass.PropertyChanging
        Dim message As String = "Properties are changing!"

        If e.PropertyName = childPropertyName Then
            Console.WriteLine(message & vbCrLf & ChildChangeNotification())
        ElseIf e.PropertyName = dollPropertyName Then
            Console.WriteLine(message & vbCrLf & DollChangeNotification())
        End If
    End Sub

    Private Sub NotifyChildChanged(sender As Object, e As PropertyChangedEventArgs) Handles passiveParentClass.PropertyChanged, listeningParentClass.PropertyChanged
        Dim message As String = "Properties have changed!"

        If e.PropertyName = childPropertyName Then
            Console.WriteLine(message & vbCrLf & ChildChangeNotification())
            UpdateForm()
        ElseIf e.PropertyName = dollPropertyName Then
            Console.WriteLine(message & vbCrLf & DollChangeNotification())
            UpdateForm()
        End If
    End Sub
#End Region

#Region "Form Controls"
    Private Sub btnChangeProperty_Click(sender As Object, e As EventArgs) Handles btnChangeProperty.Click
        UpdateChildClasses()
    End Sub

    Private Sub btnChangePropertyDoll_Click(sender As Object, e As EventArgs) Handles btnChangePropertyDoll.Click
        UpdateDollClasses()
    End Sub

    Private Sub btnbtnSwapChildReferences_Click(sender As Object, e As EventArgs) Handles btnSwapChildReferences.Click
        SwapChildClasses()
    End Sub

    Private Sub btnSwapParents_Click(sender As Object, e As EventArgs) Handles btnSwapParents.Click
        SwapParentClasses()
    End Sub
#End Region

End Class
