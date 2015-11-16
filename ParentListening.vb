Option Strict On
Option Explicit On

Imports System.ComponentModel

Public Class ParentListening
    Inherits PropertyChanger

    Private _myChild As New ChildQuiet
    Public ReadOnly Property myChild() As ChildQuiet
        Get
            Return _myChild
        End Get
    End Property

    Sub New(ByVal p_child As ChildQuiet)
        ' Passing reference to a separate child object.
        _myChild = p_child
        AddListenersAndHandlers()
    End Sub

    Friend Sub SwapChildren(ByVal p_child As ChildQuiet)
        _myChild = p_child
        AddListenersAndHandlers()
    End Sub

    ' Manually add listeners to whatever object property is desired, whether it is one level or more deeper.
    ' Note that the added handlers WILL NOT remain with the assigned object if a reference is changed. Don't forget to account for this when cloning as well!
    ' With listeners, the object properties DO NOT need a 'WithEvents' clause at any level.
    ' Listeners will only work if the object triggers its own property changing/changed events.
    Private Sub AddListenersAndHandlers()
        Dim childChangingListener As ChangingListener = ChangingListener.Create(_myChild)
        AddHandler childChangingListener.PropertyChanging, AddressOf Value_PropertyChanging

        Dim childChangedListener As ChangedListener = ChangedListener.Create(_myChild)
        AddHandler childChangedListener.PropertyChanged, AddressOf Value_PropertyChanged

        ' Capture the changes to the child doll object properties directly.
        ' No need to add these handlers in the child class.
        Dim childDollChangingListener As ChangingListener = ChangingListener.Create(_myChild.myDoll)
        AddHandler childDollChangingListener.PropertyChanging, AddressOf Value_PropertyChanging

        Dim childDollChangedListener As ChangedListener = ChangedListener.Create(_myChild.myDoll)
        AddHandler childDollChangedListener.PropertyChanged, AddressOf Value_PropertyChanged
    End Sub
End Class
