Option Strict On
Option Explicit On

'Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel
'Imports System.Linq
Imports System.Reflection

Public Class ChildChangedListener
    Inherits ChangedListener
#Region "*** Members ***"
    Protected Shared ReadOnly _inotifyType As Type = GetType(INotifyPropertyChanged)

    Private ReadOnly _value As INotifyPropertyChanged
    Private ReadOnly _type As Type
    Private ReadOnly _childListeners As New Dictionary(Of String, ChangedListener)()
#End Region


#Region "*** Constructors ***"
    Public Sub New(instance As INotifyPropertyChanged)
        If instance Is Nothing Then
            Throw New ArgumentNullException("instance")
        End If

        _value = instance
        _type = _value.[GetType]()

        Subscribe()
    End Sub

    Public Sub New(instance As INotifyPropertyChanged, propertyName As String)
        Me.New(instance)
        _propertyName = propertyName
    End Sub
#End Region


#Region "*** Private Methods ***"
    Private Sub Subscribe()
        AddHandler _value.PropertyChanged, AddressOf value_PropertyChanged

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
                RemoveHandler _childListeners(propertyName).PropertyChanged, AddressOf child_PropertyChanged

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
                If TypeOf newValue Is INotifyCollectionChanged Then
                    _childListeners(propertyName) = New CollectionChangedListener(TryCast(newValue, INotifyCollectionChanged), propertyName)
                ElseIf TypeOf newValue Is INotifyPropertyChanged Then
                    _childListeners(propertyName) = New ChildChangedListener(TryCast(newValue, INotifyPropertyChanged), propertyName)
                End If

                If _childListeners(propertyName) IsNot Nothing Then
                    AddHandler _childListeners(propertyName).PropertyChanged, AddressOf child_PropertyChanged
                End If
            End If
        End If
    End Sub
#End Region


#Region "*** Event Handler ***"
    Private Sub child_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        RaisePropertyChanged(e.PropertyName)
    End Sub

    Private Sub value_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        ' First, reset child on change, if required...
        ResetChildListener(e.PropertyName)

        ' ...then, notify about it
        RaisePropertyChanged(e.PropertyName)
    End Sub

    Protected Overrides Sub RaisePropertyChanged(propertyName As String)
        ' Special Formatting
        MyBase.RaisePropertyChanged(String.Format("{0}{1}{2}", _propertyName, If(_propertyName IsNot Nothing, ".", Nothing), propertyName))
    End Sub
#End Region


#Region "*** Overrides ***"
    ''' <summary>
    ''' Release all child handlers and self handler
    ''' </summary>
    Protected Overrides Sub Unsubscribe()
        RemoveHandler _value.PropertyChanged, AddressOf value_PropertyChanged

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
