Option Strict On
Option Explicit On

Imports System.ComponentModel

Public Class ChildQuiet
    Inherits PropertyChanger

    Private _myProperty As String = "Quiet Child Property"
    Public Property myProperty() As String
        Get
            Return _myProperty
        End Get
        Set(ByVal value As String)
            If Not value = _myProperty Then
                RaisePropertyChanging(Function() myProperty)
                _myProperty = value
                RaisePropertyChanged(Function() myProperty)
            End If
        End Set
    End Property

    ' No 'WithEvents' needed.
    Private _myDoll As New DollQuiet
    Public ReadOnly Property myDoll() As DollQuiet
        Get
            Return _myDoll
        End Get
    End Property

    Sub New()

    End Sub

    ' No event handlers needed for object property.
End Class
