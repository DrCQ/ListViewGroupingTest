Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Reflection
Imports CommunityToolkit.Mvvm.ComponentModel

Namespace ViewModel.Dialog

    Public Class AboutDialogViewModel
        Inherits ObservableObject

#Region "Private variables"

        Private _appName As String
        Private _appVersion As String
        Private _rights As String
        Private _description As String

#End Region

        Public Class PackageInfo

            ReadOnly Property Name As String

            ReadOnly Property Version As String

            Friend Sub New(sName As String, sVersion As String)
                Me.Name = sName
                Me.Version = sVersion
            End Sub

        End Class

#Region "Properties: ApplicationName, ApplicationVersion, CopyrightText, ApplicationDescription, PackageListView"

        Property ApplicationName As String
            Get
                Return _appName
            End Get
            Private Set(value As String)
                SetProperty(Of String)(_appName, value)
            End Set
        End Property

        Property ApplicationVersion As String
            Get
                Return _appVersion
            End Get
            Set(value As String)
                SetProperty(Of String)(_appVersion, value)
            End Set
        End Property

        Property CopyrightText As String
            Get
                Return _rights
            End Get
            Private Set(value As String)
                SetProperty(Of String)(_rights, value)
            End Set
        End Property

        Property ApplicationDescription As String
            Get
                Return _description
            End Get
            Private Set(value As String)
                SetProperty(Of String)(_description, value)
            End Set
        End Property

        ReadOnly Property PackageListView As ICollectionView

#End Region

        Private ReadOnly Property PackageList As New ObservableCollection(Of PackageInfo)

#Region "Metods: New"

        Sub New()
            Me.PackageListView = CollectionViewSource.GetDefaultView(Me.PackageList)

            Dim ver As Version, company As String = String.Empty, copyRight As String = String.Empty
            Dim attr As Attribute() = Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyTitleAttribute))
            Me.ApplicationName = CType(attr(0), AssemblyTitleAttribute).Title
            ver = Assembly.GetExecutingAssembly.GetName.Version
            Me.ApplicationVersion = String.Format("Version {0}.{1} Build: {2} Rev: {3} Env: {4} bit", ver.Major, ver.Minor, ver.Build, ver.Revision, IIf(Environment.Is64BitProcess, "64", "32"))
            attr = Assembly.GetExecutingAssembly.GetCustomAttributes(GetType(AssemblyCompanyAttribute))
            If attr.Length > 0 Then company = DirectCast(attr(0), AssemblyCompanyAttribute).Company
            attr = Assembly.GetExecutingAssembly.GetCustomAttributes(GetType(AssemblyCopyrightAttribute))
            If attr.Length > 0 Then copyRight = CType(attr(0), AssemblyCopyrightAttribute).Copyright
            Me.CopyrightText = String.Format("{0} by {1}", copyRight, company)
            attr = Assembly.GetEntryAssembly.GetCustomAttributes(GetType(AssemblyDescriptionAttribute))
            If attr.Length > 0 Then Me.ApplicationDescription = CType(attr(0), AssemblyDescriptionAttribute).Description

            For Each ass As Assembly In AppDomain.CurrentDomain.GetAssemblies
                With ass.FullName
                    If .StartsWith("MaterialDesign") OrElse .StartsWith("CommunityToolkit") OrElse .StartsWith("PresentationCore,") OrElse
                        .StartsWith("System.Runtime,") Then
                        Dim parts() As String = .Split(",")
                        Dim name As String = parts(0)
                        If name.Equals("PresentationCore") Then
                            name = "WPF Presentation Core"
                        ElseIf name.Equals("System.Runtime") Then
                            name = "NET"
                        End If
                        Me.PackageList.Add(New PackageInfo(name, parts(1).Replace("Version=", "")))
                    End If
                End With
            Next

        End Sub

#End Region


    End Class

End Namespace

