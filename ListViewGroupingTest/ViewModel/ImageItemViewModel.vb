Imports System.IO
Imports CommunityToolkit.Mvvm.ComponentModel

Namespace ViewModel

    Public Class ImageItemViewModel
        Inherits ObservableObject

#Region "Private variables"

        Private _imgName As String
        Private _imgPath As String
        Private _dtCreated As DateTime
        Private _imgSource As ImageSource = Nothing

#End Region

#Region "Properties: ImageName, ImagePath, DateCreated, ImageIcon, GroupingValue"

        Property ImageName As String
            Get
                Return _imgName
            End Get
            Set(value As String)
                SetProperty(Of String)(_imgName, value)
            End Set
        End Property

        Property ImagePath As String
            Get
                Return _imgPath
            End Get
            Set(value As String)
                SetProperty(Of String)(_imgPath, value)
            End Set
        End Property

        Property DateCreated As DateTime
            Get
                Return _dtCreated
            End Get
            Set(value As DateTime)
                SetProperty(Of DateTime)(_dtCreated, value)
            End Set
        End Property

        Property ImageIcon As ImageSource
            Get
                Return _imgSource
            End Get
            Set(value As ImageSource)
                SetProperty(Of ImageSource)(_imgSource, value)
            End Set
        End Property

        ReadOnly Property GroupingValue As String = String.Empty

#End Region

#Region "Methods: New"

        Sub New(sPath As String)
            Me.ImagePath = sPath
            Me.ImageName = Path.GetFileName(Me.ImagePath)
            Me.ImageIcon = New BitmapImage(New Uri(Me.ImagePath))
            Dim fi As New FileInfo(Me.ImagePath)
            Me.DateCreated = fi.CreationTime
            Me.GroupingValue = String.Format("{0:yyyy-MM}", Me.DateCreated)
        End Sub

#End Region

    End Class

End Namespace

