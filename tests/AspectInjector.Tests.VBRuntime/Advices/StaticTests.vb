Imports System
Imports System.Text
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Advices
    Public Class StaticTests
        <Fact>
        Public Sub Advices_InjectBeforeStaticMethod()
            Passed = False
            Call StaticTests_BeforeTarget.TestStaticMethod()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAroundStaticMethod()
            Passed = False
            Dim v = 2
            Dim vv As Object = v
            Call StaticTests_AroundTarget.Do123(vv, New StringBuilder(), New Object(), False, False)
            Assert.True(Passed)
        End Sub

        Public Class StaticTests_AroundTarget
            <StaticTests_AroundAspect1>
            <StaticTests_AroundAspect2> 'fire first
            Public Shared Function Do123(ByVal data As Integer, ByVal sb As StringBuilder, ByVal [to] As Object, ByVal passed As Boolean, ByVal passed2 As Boolean) As Integer
                Checker.Passed = passed AndAlso passed2
                Dim a = 1
                Dim b = a + data
                Return b
            End Function
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(StaticTests_AroundAspect1))>
        Public Class StaticTests_AroundAspect1
            Inherits Attribute

            <Advice(Kind.Around, Targets:=Target.Method)>
            Public Function AroundMethod(
            <Argument(Source.Target)> ByVal target As Func(Of Object(), Object),
            <Argument(Source.Arguments)> ByVal arguments As Object()) As Object
                Return target(New Object() {arguments(0), arguments(1), arguments(2), True, arguments(4)})
            End Function
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(StaticTests_AroundAspect2))>
        Public Class StaticTests_AroundAspect2
            Inherits Attribute

            <Advice(Kind.Around, Targets:=Target.Method)>
            Public Function AroundMethod(
            <Argument(Source.Target)> ByVal target As Func(Of Object(), Object),
            <Argument(Source.Arguments)> ByVal arguments As Object()) As Object
                Return target(New Object() {arguments(0), arguments(1), arguments(2), arguments(3), True})
            End Function
        End Class

        <StaticTests_BeforeAspect>
        Friend Class StaticTests_BeforeTarget
            Public Shared Sub TestStaticMethod()
            End Sub

            Public Sub TestInstanceMethod()
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(StaticTests_BeforeAspect))>
        Public Class StaticTests_BeforeAspect
            Inherits Attribute
            'Property
            <Advice(Kind.Before, Targets:=Target.Method)>
            Public Sub BeforeMethod()
                Passed = True
            End Sub
        End Class
    End Class
End Namespace
