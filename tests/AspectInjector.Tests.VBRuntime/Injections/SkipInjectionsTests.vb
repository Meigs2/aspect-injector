Imports System
Imports AspectInjector.Broker
Imports Xunit

Namespace Injections
    <SkipInjectionsTests.TestSelfInjection>
    Public Class SkipInjectionsTests
        <Fact>
        Public Sub DontInjectIntoInjection()
            Dim t = New TestTarget()
            t.Test()
            Dim i1 = New TestSelfInjection()
            i1.Test()
            Dim i2 = New TestOtherInjection()
            i2.Test()
            Assert.Equal(2, TestTarget.Counter)
        End Sub

        Private Class TestTarget
            Public Shared Counter As Integer = 0

            Public Sub Test()
            End Sub
        End Class

        <Injection(GetType(TestAspect))>
        Friend Class TestSelfInjection
            Inherits Attribute

            Public Sub Test()
            End Sub
        End Class

        <Injection(GetType(TestAspect))>
        Private Class TestOtherInjection
            Inherits Attribute

            Public Sub Test()
            End Sub
        End Class

        <Aspect(Scope.Global)>
        Public Class TestAspect
            <Advice(Kind.Before, Targets:=Target.Method)>
            Public Sub Before()
                TestTarget.Counter += 1
            End Sub
        End Class
    End Class
End Namespace
