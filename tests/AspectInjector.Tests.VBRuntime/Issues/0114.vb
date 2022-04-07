Imports System
Imports AspectInjector.Broker
Imports Xunit

Namespace Issues
    Public Class Issue_0114
        <Fact>
        Public Sub Test()
            Call New TestClass().Do()
        End Sub

        Friend Class TestClass
            <TestInjection>
            Public Sub [Do]()
                Assert.True(False)
            End Sub
        End Class

        <Aspect(Scope.Global)>
        Public Class TestAspect
            <Advice(Kind.Around)>
            Public Function TestAdvice(
            <Argument(Source.Triggers)> ByVal attributes As Attribute()) As Object
                Assert.NotEmpty(attributes)
                Return Nothing
            End Function
        End Class

        <Injection(GetType(TestAspect))>
        Public Class TestInjection
            Inherits Attribute

            Public Sub New(ByVal Optional testArg As String = Nothing)
            End Sub
        End Class
    End Class
End Namespace
