Imports CommunityToolkit.Mvvm.ComponentModel
Imports CommunityToolkit.Mvvm.Input
Imports CommunityToolkit.Mvvm.Messaging
Imports ListViewGroupingTest.Helper
Imports ListViewGroupingTest.Messages
Imports ListViewGroupingTest.Services
Imports ListViewGroupingTest.View.Dialog
Imports ListViewGroupingTest.ViewModel.Dialog
Imports MaterialDesignThemes.Wpf

Namespace ViewModel

    Public Class MainWindowViewModel
        Inherits ObservableRecipient

#Region "Private variables"

        Private _isDarkMode As Boolean

#End Region

#Region "Private Properties: PaletteMngr"

        Private ReadOnly PaletteMngr As New PaletteHelper

#End Region

#Region "Properties: DataService, IsDarkMode"

        ReadOnly Property DataService As ApplicationDataService

        Property IsDarkMode As Boolean
            Get
                Return _isDarkMode
            End Get
            Set(value As Boolean)
                If SetProperty(Of Boolean)(_isDarkMode, value) Then
                    Dim thm As Theme = Me.PaletteMngr.GetTheme
                    If Me.IsDarkMode Then
                        thm.SetBaseTheme(BaseTheme.Dark)
                    Else
                        thm.SetBaseTheme(BaseTheme.Light)
                    End If
                    Me.PaletteMngr.SetTheme(thm)
                    My.Settings.IsDarkMode = Me.IsDarkMode
                    My.Settings.Save()
                End If
            End Set
        End Property

#End Region

#Region "Window Commands: Maximize, Minimize, Close"

        ReadOnly Property WindowCloseCommand As New RelayCommand(Of Window)(
            Sub(wnd As Window)
                wnd.Close()
            End Sub)

        ReadOnly Property WindowMaximizeCommand As New RelayCommand(Of Window)(
            Sub(wnd As Window)
                If wnd.WindowState = WindowState.Normal Then
                    wnd.WindowState = WindowState.Maximized
                Else
                    wnd.WindowState = WindowState.Normal
                End If
            End Sub)

        ReadOnly Property WindowMinimizeCommand As New RelayCommand(Of Window)(
            Sub(wnd As Window)
                wnd.WindowState = WindowState.Minimized
            End Sub)

#End Region

#Region "Application Commands: ShowAboutDialogCommand"

        ReadOnly Property ShowAboutDialogCommand As New RelayCommand(
            Async Sub()
                Dim view As New AboutDialogView() With {.DataContext = New AboutDialogViewModel}
                Me.DataService.IsNotModal = False
                Await DialogHost.Show(view)
                Me.DataService.IsNotModal = True
            End Sub)

        ReadOnly Property ShowImagesCommand As New RelayCommand(
            Sub()
                'Messenger.Send(Of NotificationMessage)(New NotificationMessage(New _
                '                   AppNotification(NotificationTypes.Error, NameOf(MainWindowViewModel), "Test Error Message")))
            End Sub
        )

        ReadOnly Property ClearViewCommand As New RelayCommand(
            Sub()

            End Sub
        )

#End Region


#Region "Methods: New"

        Sub New(iAppData As IAppDataService)
            Me.DataService = iAppData
            Me._isDarkMode = Not My.Settings.IsDarkMode
            Me.IsDarkMode = My.Settings.IsDarkMode
        End Sub

#End Region

    End Class

End Namespace

