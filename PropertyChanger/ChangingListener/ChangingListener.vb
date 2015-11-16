Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.Specialized

Public MustInherit Class ChangingListener
    Implements INotifyPropertyChanging
    Implements IDisposable

#Region "Members"
    Protected _propertyName As String
#End Region

#Region "Abstract Members"
    Protected MustOverride Sub Unsubscribe()
#End Region

#Region "INotifyPropertyChanged Members & Invoker"

    Public Event PropertyChanging(sender As Object, e As PropertyChangingEventArgs) Implements INotifyPropertyChanging.PropertyChanging

    Protected Overridable Sub RaisePropertyChanging(ByVal propertyName As String)
        RaiseEvent PropertyChanging(Me, New PropertyChangingEventArgs(propertyName))
    End Sub

#End Region

#Region "IDisposable Support"

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub


    Protected Overridable Sub Dispose(disposing As Boolean)
        If disposing Then Unsubscribe()
    End Sub

    Protected Overrides Sub Finalize()
        Try
            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub
#End Region

#Region "Factory"
    Public Shared Function Create(ByVal value As INotifyPropertyChanging) As ChangingListener
        Return Create(value, Nothing)
    End Function

    Public Shared Function Create(ByVal value As INotifyPropertyChanging,
                                  ByVal propertyName As String) As ChangingListener
        If TypeOf value Is INotifyPropertyChanging Then
            Return New ChildChangingListener(TryCast(value, INotifyPropertyChanging), propertyName)
        Else
            Return Nothing
        End If
    End Function
#End Region

End Class
