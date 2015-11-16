Option Strict On
Option Explicit On

Imports System.ComponentModel

Public Class ParentPassive
    Inherits PropertyChanger

    Private WithEvents _myChild As New ChildLoud
    Public ReadOnly Property myChild() As ChildLoud
        Get
            Return _myChild
        End Get
    End Property

    Sub New(ByVal p_child As ChildLoud)
        ' Passing reference to a separate child object.
        _myChild = p_child
    End Sub

    Friend Sub SwapChildren(ByVal p_child As ChildLoud)
        _myChild = p_child
    End Sub

    ' Added property changed event handlers for the case of myChild changing internally. This is unaffected by it being a ReadOnly property.
    ' This only captures property changes of the child's doll if the child also has similar event handlers defined for the doll object property.
    Private Sub child_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles _myChild.PropertyChanged
        Value_PropertyChanged(Me, e)
    End Sub
    Private Sub child_PropertyChanged(sender As Object, e As PropertyChangingEventArgs) Handles _myChild.PropertyChanging
        Value_PropertyChanging(Me, e)
    End Sub

End Class
