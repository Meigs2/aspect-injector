Imports AspectInjector.Tests.Assets

Namespace Around
    Public Class Instance_Tests
        Inherits Global_Tests

        Protected Overrides ReadOnly Property EnterToken As String
            Get
                Return InstanceAspect.AroundEnter
            End Get
        End Property

        Protected Overrides ReadOnly Property ExitToken As String
            Get
                Return InstanceAspect.AroundExit
            End Get
        End Property
    End Class
End Namespace
