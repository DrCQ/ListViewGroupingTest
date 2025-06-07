Imports CommunityToolkit.Mvvm.Messaging.Messages
Imports ListViewGroupingTest.Helper

Namespace Messages

    Friend Class NotificationMessage
        Inherits ValueChangedMessage(Of AppNotification)

        Sub New(arg As AppNotification)
            MyBase.New(arg)
        End Sub

    End Class

End Namespace

