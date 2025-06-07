Imports System.Collections.ObjectModel
Imports CommunityToolkit.Mvvm.ComponentModel
Imports CommunityToolkit.Mvvm.Input
Imports CommunityToolkit.Mvvm.Messaging
Imports ListViewGroupingTest.Helper
Imports ListViewGroupingTest.Messages
Imports ListViewGroupingTest.Services
Imports MaterialDesignThemes.Wpf

Namespace ViewModel

    Public Class StatusBarViewModel
        Inherits ObservableRecipient

#Region "Private variables"

        Private _errCnt As Integer = 0
        Private _warCnt As Integer = 0
        Private _infCnt As Integer = 0
        Private _showErr As Boolean = True
        Private _showWrn As Boolean = True
        Private _showInf As Boolean = True
        Private _showAlert As Boolean = True
        Private _autoWidth As Double = Double.NaN

#End Region

#Region "Properties: DataService, NotificationsView, ErrorCount, WarningCount, InfoCount, ShowErrors, ShowWarnings, ShowInfos, HasNotifications, ShowAlert, AutoWidth"

        ReadOnly Property DataService As ApplicationDataService

        ReadOnly Property NotificationsView As CollectionView

        Property ErrorCount As Integer
            Get
                Return _errCnt
            End Get
            Private Set(value As Integer)
                SetProperty(Of Integer)(_errCnt, value)
            End Set
        End Property

        Property WarningCount As Integer
            Get
                Return _warCnt
            End Get
            Private Set(value As Integer)
                SetProperty(Of Integer)(_warCnt, value)
            End Set
        End Property

        Property InfoCount As Integer
            Get
                Return _infCnt
            End Get
            Private Set(value As Integer)
                SetProperty(Of Integer)(_infCnt, value)
            End Set
        End Property

        Property ShowErrors As Boolean
            Get
                Return _showErr
            End Get
            Set(value As Boolean)
                If SetProperty(Of Boolean)(_showErr, value) Then
                    Me.AutoWidth = 0
                    Me.NotificationsView.Refresh()
                    Me.AutoWidth = Double.NaN
                    Me.ClearListCommand.NotifyCanExecuteChanged()
                End If
            End Set
        End Property

        Property ShowWarnings As Boolean
            Get
                Return _showWrn
            End Get
            Set(value As Boolean)
                If SetProperty(Of Boolean)(_showWrn, value) Then
                    Me.AutoWidth = 0
                    Me.NotificationsView.Refresh()
                    Me.AutoWidth = Double.NaN
                    Me.ClearListCommand.NotifyCanExecuteChanged()
                End If
            End Set
        End Property

        Property ShowInfos As Boolean
            Get
                Return _showInf
            End Get
            Set(value As Boolean)
                If SetProperty(Of Boolean)(_showInf, value) Then
                    Me.AutoWidth = 0
                    Me.NotificationsView.Refresh()
                    Me.AutoWidth = Double.NaN
                    Me.ClearListCommand.NotifyCanExecuteChanged()
                End If
            End Set
        End Property

        ReadOnly Property HasNotifications As Boolean
            Get
                Return Me.NotificationsList.Count > 0
            End Get
        End Property

        Property ShowAlert As Boolean
            Get
                Return _showAlert
            End Get
            Private Set(value As Boolean)
                SetProperty(Of Boolean)(_showAlert, value)
            End Set
        End Property

        Property AutoWidth As Double
            Get
                Return _autoWidth
            End Get
            Private Set(value As Double)
                SetProperty(Of Double)(_autoWidth, value)
            End Set
        End Property

#End Region

#Region "Private Properties: NotificationsList"

        Private ReadOnly Property NotificationsList As New ObservableCollection(Of AppNotification)

#End Region

#Region "Commands: ClearNotificationsCommand, ClearListCommand, CloseListCommand"

        ReadOnly Property ClearNotificationsCommand As New RelayCommand(
            Sub()
                Me.NotificationsList.Clear()
                Me.UpdateCounters()
                Me.ShowAlert = False
            End Sub,
            Function() As Boolean
                Return Me.NotificationsList.Count > 0
            End Function)

        ReadOnly Property ClearListCommand As New RelayCommand(
            Sub()
                If Me.ShowErrors AndAlso Me.ShowWarnings AndAlso Me.ShowInfos Then
                    Me.NotificationsList.Clear()
                Else
                    Dim removeList As New List(Of AppNotification)
                    For Each item As AppNotification In Me.NotificationsList
                        If item.Type = NotificationTypes.Error AndAlso Me.ShowErrors Then
                            removeList.Add(item)
                        ElseIf item.Type = NotificationTypes.Warning AndAlso Me.ShowWarnings Then
                            removeList.Add(item)
                        ElseIf item.Type = NotificationTypes.Info AndAlso Me.ShowInfos Then
                            removeList.Add(item)
                        End If
                    Next
                    For Each item As AppNotification In removeList
                        Me.NotificationsList.Remove(item)
                    Next
                End If
                Me.AutoWidth = 0
                Me.UpdateCounters()
                Me.AutoWidth = Double.NaN
                Me.ShowAlert = False
            End Sub,
            Function() As Boolean
                Return Me.NotificationsView.Count > 0
            End Function)

        ReadOnly Property CloseListCommand As New RelayCommand(
            Sub()
                Me.ShowAlert = False
                PopupBox.ClosePopupCommand.Execute(Nothing, Nothing)
            End Sub)

#End Region

#Region "Methods: New"

        'For the XAML Designer only!
        Sub New()

        End Sub

        Sub New(iAppData As IAppDataService)
            Me.DataService = iAppData

            Me.NotificationsView = CType(CollectionViewSource.GetDefaultView(Me.NotificationsList), CollectionView)
            Me.NotificationsView.Filter = New Predicate(Of Object)(
                Function(item As AppNotification) As Boolean
                    If item.Type = NotificationTypes.Error Then
                        Return Me.ShowErrors
                    ElseIf item.Type = NotificationTypes.Warning Then
                        Return Me.ShowWarnings
                    Else
                        Return Me.ShowInfos
                    End If
                End Function)

            Messenger.Register(Of NotificationMessage)(Me,
                Sub(recipient As Object, msg As NotificationMessage)
                    Me.NotificationsList.Add(msg.Value)
                    Me.AutoWidth = 0
                    Me.UpdateCounters()
                    Me.AutoWidth = Double.NaN
                    Me.ShowAlert = True
                End Sub)
        End Sub

#End Region

#Region "Private methods: UpdateCounters"

        Private Sub UpdateCounters()
            Me.ErrorCount = Me.NotificationsList.Where(Function(x) x.Type = NotificationTypes.Error).Count
            Me.WarningCount = Me.NotificationsList.Where(Function(x) x.Type = NotificationTypes.Warning).Count
            Me.InfoCount = Me.NotificationsList.Where(Function(x) x.Type = NotificationTypes.Info).Count
            Me.ClearNotificationsCommand.NotifyCanExecuteChanged()
            Me.ClearListCommand.NotifyCanExecuteChanged()
            OnPropertyChanged(NameOf(Me.HasNotifications))
        End Sub

#End Region

    End Class

End Namespace

