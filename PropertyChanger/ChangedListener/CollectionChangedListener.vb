Option Strict On
Option Explicit On

Imports System.Collections.Specialized
Imports System.ComponentModel

Public Class CollectionChangedListener
    Inherits ChangedListener
#Region "*** Members ***"
    Private ReadOnly _value As INotifyCollectionChanged
    Private ReadOnly _collectionListeners As New Dictionary(Of INotifyPropertyChanged, ChangedListener)()
#End Region


#Region "*** Constructors ***"
    Public Sub New(collection As INotifyCollectionChanged, propertyName As String)
        _value = collection
        _propertyName = propertyName

        Subscribe()
    End Sub
#End Region


#Region "*** Private Methods ***"
    Private Sub Subscribe()
        AddHandler _value.CollectionChanged, AddressOf value_CollectionChanged

        For Each item As INotifyPropertyChanged In DirectCast(_value, IEnumerable)
            ResetChildListener(item)
        Next
    End Sub

    Private Sub ResetChildListener(item As INotifyPropertyChanged)
        If item Is Nothing Then
            Throw New ArgumentNullException("item")
        End If

        RemoveItem(item)

        Dim listener As ChangedListener = Nothing

        ' Add new
        If TypeOf item Is INotifyCollectionChanged Then
            listener = New CollectionChangedListener(TryCast(item, INotifyCollectionChanged), _propertyName)
        Else
            listener = New ChildChangedListener(TryCast(item, INotifyPropertyChanged))
        End If

        AddHandler listener.PropertyChanged, AddressOf listener_PropertyChanged

        _collectionListeners.Add(item, listener)
    End Sub

    Private Sub RemoveItem(item As INotifyPropertyChanged)
        ' Remove old
        If _collectionListeners.ContainsKey(item) Then
            RemoveHandler _collectionListeners(item).PropertyChanged, AddressOf listener_PropertyChanged

            _collectionListeners(item).Dispose()
            _collectionListeners.Remove(item)
        End If
    End Sub


    Private Sub ClearCollection()
        For Each key As INotifyPropertyChanged In _collectionListeners.Keys
            _collectionListeners(key).Dispose()
        Next

        _collectionListeners.Clear()
    End Sub
#End Region


#Region "*** Event handlers ***"
    Private Sub value_CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs)
        If e.Action = NotifyCollectionChangedAction.Reset Then
            ClearCollection()
        Else
            ' Don't care about e.Action, if there are old items, Remove them...
            If e.OldItems IsNot Nothing Then
                For Each item As INotifyPropertyChanged In DirectCast(e.OldItems, IEnumerable)
                    RemoveItem(item)
                Next
            End If

            ' ...add new items as well
            If e.NewItems IsNot Nothing Then
                For Each item As INotifyPropertyChanged In DirectCast(e.NewItems, IEnumerable)
                    ResetChildListener(item)
                Next
            End If
        End If
    End Sub


    Private Sub listener_PropertyChanged(sender As Object, e As PropertyChangedEventArgs)
        ' ...then, notify about it
        RaisePropertyChanged(String.Format("{0}{1}{2}", _propertyName, If(_propertyName IsNot Nothing, "[].", Nothing), e.PropertyName))
    End Sub
#End Region


#Region "*** Overrides ***"
    ''' <summary>
    ''' Releases all collection item handlers and self handler
    ''' </summary>
    Protected Overrides Sub Unsubscribe()
        ClearCollection()

        RemoveHandler _value.CollectionChanged, AddressOf value_CollectionChanged

        System.Diagnostics.Debug.WriteLine("CollectionChangeListener unsubscribed")
    End Sub
#End Region
End Class
