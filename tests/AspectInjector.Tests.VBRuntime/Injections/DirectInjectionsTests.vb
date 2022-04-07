Imports System
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Injections
    Public Class DirectInjectionsTests
        <Fact>
        Public Sub CanInjectIntoGetterDirectly()
            Passed = False
            Dim t = New TestTarget()
            Dim a = t.Text
            Assert.True(Passed)
            Passed = False
        End Sub

        Private Class TestTarget
            Public Property Text As String
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(TestAspect))>
        Public Class TestAspect
            Inherits Attribute

            Private _count As Integer = 0

            <Advice(Kind.Before)>
            Public Sub Before()
                _count += 1
                Assert.Equal(1, _count)
                Passed = True
            End Sub
        End Class
    End Class
End Namespace
