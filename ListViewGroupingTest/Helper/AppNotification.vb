Namespace Helper

    Public Class AppNotification

        ReadOnly Property Type As NotificationTypes

        ReadOnly Property Source As String

        ReadOnly Property Message As String

        Friend Sub New(nType As NotificationTypes, sSoure As String, sFormat As String, ParamArray args As Object())
            Me.Type = nType
            Me.Source = sSoure
            Me.Message = String.Format(sFormat, args)
        End Sub

    End Class

End Namespace
