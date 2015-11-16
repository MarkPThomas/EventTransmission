Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Collections.Specialized

Public MustInherit Class ChangedListener
    Implements INotifyPropertyChanged
    Implements IDisposable

#Region "Members"
    Protected _propertyName As String
#End Region

#Region "Abstract Members"
    Protected MustOverride Sub Unsubscribe()
#End Region

#Region "INotifyPropertyChanged Members & Invoker"

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) Implements INotifyPropertyChanged.PropertyChanged

    Protected Overridable Sub RaisePropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
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
    Public Shared Function Create(ByVal value As INotifyPropertyChanged) As ChangedListener
        Return Create(value, Nothing)
    End Function

    Public Shared Function Create(ByVal value As INotifyPropertyChanged,
                                  ByVal propertyName As String) As ChangedListener
        If TypeOf value Is INotifyCollectionChanged Then
            Return New CollectionChangedListener(TryCast(value, INotifyCollectionChanged), propertyName)
        ElseIf TypeOf value Is INotifyPropertyChanged Then
            Return New ChildChangedListener(TryCast(value, INotifyPropertyChanged), propertyName)
        Else
            Return Nothing
        End If
    End Function
#End Region

End Class
