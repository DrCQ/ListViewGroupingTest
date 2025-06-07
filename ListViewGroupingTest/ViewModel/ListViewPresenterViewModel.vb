Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.IO
Imports CommunityToolkit.Mvvm.ComponentModel
Imports CommunityToolkit.Mvvm.Input
Imports ListViewGroupingTest.Services
Imports Microsoft.Win32

Namespace ViewModel

    Public Class ListViewPresenterViewModel
        Inherits ObservableObject

#Region "Private variables"

        Private _isGrouping As Boolean = False

#End Region

#Region "Properties: DataService, ImageList, ImageView, IsGrouping"

        ReadOnly Property DataService As ApplicationDataService

        ReadOnly Property ImageList As New ObservableCollection(Of ImageItemViewModel)

        ReadOnly Property ImageView As ListCollectionView = Nothing

        Property IsGrouping As Boolean
            Get
                Return _isGrouping
            End Get
            Set(value As Boolean)
                If SetProperty(Of Boolean)(_isGrouping, value) Then
                    If Me.IsGrouping Then
                        Me.ImageView.GroupDescriptions.Add(New PropertyGroupDescription("GroupingValue"))
                    Else
                        Me.ImageView.GroupDescriptions.Clear()
                    End If
                End If
            End Set
        End Property

#End Region

#Region "Commands: LoadImagesCommand, ClearViewCommand"

        ReadOnly Property LoadImagesCommand As New RelayCommand(
            Sub()
                Dim dlg As New OpenFolderDialog With {.Title = "Select image folder", .RootDirectory = "D:\Christoph\Pictures\Development\Test Bilder"}
                Dim ret As Boolean? = dlg.ShowDialog
                If ret.HasValue AndAlso ret.Value = True Then
                    With dlg
                        Me.DataService.FolderName = .FolderName
                        Dim iList As String() = Directory.GetFiles(.FolderName, "*.JPG")
                        Me.ImageList.Clear()
                        For Each img As String In iList
                            Me.ImageList.Add(New ImageItemViewModel(img))
                        Next
                    End With
                End If
                LoadImagesCommand.NotifyCanExecuteChanged()
                ClearViewCommand.NotifyCanExecuteChanged()
            End Sub,
            Function() As Boolean
                Return Me.ImageList.Count = 0
            End Function
        )

        ReadOnly Property ClearViewCommand As New RelayCommand(
            Sub()
                Me.ImageList.Clear()
                LoadImagesCommand.NotifyCanExecuteChanged()
                ClearViewCommand.NotifyCanExecuteChanged()
            End Sub,
            Function() As Boolean
                Return Me.ImageList.Count > 0
            End Function
        )

#End Region

#Region "Methods:New"

        Sub New(iAppData As IAppDataService)
            Me.DataService = iAppData
            Me.ImageView = CType(CollectionViewSource.GetDefaultView(Me.ImageList), ListCollectionView)
        End Sub

#End Region

    End Class

End Namespace

