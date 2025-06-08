Imports CommunityToolkit.Mvvm.ComponentModel

Namespace Services

    Public Class ApplicationDataService
        Inherits ObservableObject
        Implements IAppDataService

#Region "Private variables"

        Private _fileName As String = "File Name"
        Private _isNotModal As Boolean = True
        Private _cnt As Integer = 0

#End Region

        Property FolderName As String Implements IAppDataService.FolderName
            Get
                Return _fileName
            End Get
            Set(value As String)
                SetProperty(Of String)(_fileName, value)
            End Set
        End Property

        Property IsNotModal As Boolean Implements IAppDataService.IsNotModal
            Get
                Return _isNotModal
            End Get
            Friend Set(value As Boolean)
                SetProperty(Of Boolean)(_isNotModal, value)
            End Set
        End Property

        Property ImageCount As Integer Implements IAppDataService.ImageCount
            Get
                Return _cnt
            End Get
            Set(value As Integer)
                SetProperty(Of Integer)(_cnt, value)
            End Set
        End Property

    End Class

End Namespace

