Imports AspectInjector.Tests.Assets

Namespace After
    Public Class Instance_Tests
        Inherits Global_Tests

        Protected Overrides ReadOnly Property Token As String
            Get
                Return InstanceAspect.AfterExecuted
            End Get
        End Property
    End Class
End Namespace
