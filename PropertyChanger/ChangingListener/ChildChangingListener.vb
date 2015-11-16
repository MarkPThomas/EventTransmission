Option Strict On
Option Explicit On

'Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel
'Imports System.Linq
Imports System.Reflection

Public Class ChildChangingListener
    Inherits ChangingListener
#Region "*** Members ***"
    Protected Shared ReadOnly _inotifyType As Type = GetType(INotifyPropertyChanging)

    Private ReadOnly _value As INotifyPropertyChanging
    Private ReadOnly _type As Type
    Private ReadOnly _childListeners As New Dictionary(Of String, ChangingListener)()
#End Region


#Region "*** Constructors ***"
    Public Sub New(instance As INotifyPropertyChanging)
        If instance Is Nothing Then
            Throw New ArgumentNullException("instance")
        End If

        _value = instance
        _type = _value.[GetType]()

        Subscribe()
    End Sub

    Public Sub New(instance As INotifyPropertyChanging, propertyName As String)
        Me.New(instance)
        _propertyName = propertyName
    End Sub
#End Region


#Region "*** Private Methods ***"
    Private Sub Subscribe()
        AddHandler _value.PropertyChanging, AddressOf value_PropertyChanging

        Dim query = From [property] In _type.GetProperties(BindingFlags.Instance Or BindingFlags.[Public]) Where _inotifyType.IsAssignableFrom([property].PropertyType)

        For Each [property] In query
            ' Declare property as known "Child", then register it
            _childListeners.Add([property].Name, Nothing)
            ResetChildListener([property].Name)
        Next
    End Sub


    ''' <summary>
    ''' Resets known (must exist in children collection) child event handlers
    ''' </summary>
    ''' <param name="propertyName">Name of known child property</param>
    Private Sub ResetChildListener(propertyName As String)
        If _childListeners.ContainsKey(propertyName) Then
            ' Unsubscribe if existing
            If _childListeners(propertyName) IsNot Nothing Then
                RemoveHandler _childListeners(propertyName).PropertyChanging, AddressOf child_PropertyChanging

                ' Should unsubscribe all events
                _childListeners(propertyName).Dispose()
                _childListeners(propertyName) = Nothing
            End If

            Dim [property] = _type.GetProperty(propertyName)
            If [property] Is Nothing Then
                Throw New InvalidOperationException(String.Format("Was unable to get '{0}' property information from Type '{1}'", propertyName, _type.Name))
            End If

            Dim newValue As Object = [property].GetValue(_value, Nothing)

            ' Only recreate if there is a new value
            If newValue IsNot Nothing Then
                If TypeOf newValue Is INotifyPropertyChanging Then
                    _childListeners(propertyName) = New ChildChangingListener(TryCast(newValue, INotifyPropertyChanging), propertyName)
                End If

                If _childListeners(propertyName) IsNot Nothing Then
                    AddHandler _childListeners(propertyName).PropertyChanging, AddressOf child_PropertyChanging
                End If
            End If
        End If
    End Sub
#End Region


#Region "*** Event Handler ***"
    Private Sub child_PropertyChanging(sender As Object, e As PropertyChangingEventArgs)
        RaisePropertyChanging(e.PropertyName)
    End Sub

    Private Sub value_PropertyChanging(sender As Object, e As PropertyChangingEventArgs)
        ' First, reset child on change, if required...
        ResetChildListener(e.PropertyName)

        ' ...then, notify about it
        RaisePropertyChanging(e.PropertyName)
    End Sub

    Protected Overrides Sub RaisePropertyChanging(propertyName As String)
        ' Special Formatting
        MyBase.RaisePropertyChanging(String.Format("{0}{1}{2}", _propertyName, If(_propertyName IsNot Nothing, ".", Nothing), propertyName))
    End Sub
#End Region


#Region "*** Overrides ***"
    ''' <summary>
    ''' Release all child handlers and self handler
    ''' </summary>
    Protected Overrides Sub Unsubscribe()
        RemoveHandler _value.PropertyChanging, AddressOf value_PropertyChanging

        For Each binderKey As String In _childListeners.Keys
            If _childListeners(binderKey) IsNot Nothing Then
                _childListeners(binderKey).Dispose()
            End If
        Next

        _childListeners.Clear()

        System.Diagnostics.Debug.WriteLine("ChildChangeListener '{0}' unsubscribed", _propertyName)
    End Sub
#End Region
End Class
