Option Strict On
Option Explicit On

Imports System.ComponentModel
Imports System.Linq.Expressions

Public MustInherit Class PropertyChanger
    Implements INotifyPropertyChanged
    Implements INotifyPropertyChanging

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event PropertyChanging As PropertyChangingEventHandler Implements INotifyPropertyChanging.PropertyChanging

    Protected Sub Value_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        RaiseEvent PropertyChanged(Me, e)
    End Sub
    Protected Sub RaisePropertyChanged(Of T)(p_propertyName As Expression(Of Func(Of T)))
        Dim currentPropertyName As String = p_propertyName.ToString.Split("."c).Last()

        If Not String.IsNullOrEmpty(currentPropertyName) Then
            Value_PropertyChanged(Me, New PropertyChangedEventArgs(currentPropertyName))
        End If
    End Sub

    Protected Sub Value_PropertyChanging(sender As Object, e As PropertyChangingEventArgs)
        RaiseEvent PropertyChanging(Me, e)
    End Sub
    Protected Sub RaisePropertyChanging(Of T)(p_propertyName As Expression(Of Func(Of T)))
        Dim currentPropertyName As String = p_propertyName.ToString.Split("."c).Last()

        If Not String.IsNullOrEmpty(currentPropertyName) Then
            Value_PropertyChanging(Me, New PropertyChangingEventArgs(currentPropertyName))
        End If
    End Sub

    ''' <summary>
    ''' Returns the name of the property supplied.
    ''' Call this by: NameOf(Function() Me.{property})
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="p_propertyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function NameOfProp(Of T)(p_propertyName As Expression(Of Func(Of T))) As String
        Dim exp As MemberExpression = TryCast(p_propertyName.Body, MemberExpression)

        If exp IsNot Nothing Then
            Return exp.Member.Name
        Else
            Return Nothing
        End If
    End Function
End Class
