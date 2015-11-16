Option Strict On
Option Explicit On

Imports System.ComponentModel

Public Class ChildLoud
    Inherits PropertyChanger

    Private _myProperty As String = "Loud Child Property"
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

    ' 'WithEvents' needed to relay changes to the defined event handlers within this class.
    Private WithEvents _myDoll As New DollLoud
    Public ReadOnly Property myDoll() As DollLoud
        Get
            Return _myDoll
        End Get
    End Property

    Sub New()

    End Sub

    ' Added property changed event handlers for the case of myDoll changing internally. This is unaffected by it being a ReadOnly property.
    Private Sub childDoll_PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Handles _myDoll.PropertyChanged
        Value_PropertyChanged(Me, e)
    End Sub
    Private Sub childDoll_PropertyChanging(sender As Object, e As PropertyChangingEventArgs) Handles _myDoll.PropertyChanging
        Value_PropertyChanging(Me, e)
    End Sub
End Class
