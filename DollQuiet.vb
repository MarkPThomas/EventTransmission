Option Strict On
Option Explicit On

Public Class DollQuiet
    Inherits PropertyChanger

    Private _myDollProperty As String = "Quiet Doll Property"
    Public Property myDollProperty() As String
        Get
            Return _myDollProperty
        End Get
        Set(ByVal value As String)
            If Not value = _myDollProperty Then
                RaisePropertyChanging(Function() myDollProperty)
                _myDollProperty = value
                RaisePropertyChanged(Function() myDollProperty)
            End If
        End Set
    End Property
End Class
