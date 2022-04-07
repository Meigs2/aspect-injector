Imports System
Imports System.Collections.Generic
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Advices
    Public Class IteratorTests
        <Fact>
        Public Sub Advices_InjectAfterIteratorMethod()
            Passed = False
            Dim a = New TargetClass()

            For Each d In a.Get("test")
                Assert.False(Passed)
                d.Equals("a"c)
            Next

            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAfterIteratorMethod_WithArgs()
            Passed = False
            Dim a = New TargetClass()

            For Each d In a.Get1("test")
                Assert.False(Passed)
                d.Equals("a"c)
            Next

            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAfterIteratorMethod2()
            Passed = False
            Dim a = New TargetClass()

            For Each d In a.Get2()
                Assert.False(Passed)
                d.Equals("a"c)
            Next

            Assert.True(Passed)
        End Sub

        Public Class TargetClass
            <TestAspect>
            Public Iterator Function [Get](ByVal input As String) As IEnumerable(Of Char)
                For Each c In input
                    Yield c
                Next
            End Function

            <TestArgsAspect>
            Public Iterator Function Get1(ByVal input As String) As IEnumerable(Of Char)
                input = "ololo"

                For Each c In input
                    Yield c
                Next
            End Function

            <TestAspect>
            Public Iterator Function Get2() As IEnumerable(Of Char)
                Yield "a"c
                Yield "b"c
                Yield "c"c
            End Function
        End Class

        <Aspect(Scope.PerInstance)>
        <Injection(GetType(TestAspect))>
        Public Class TestAspect
            Inherits Attribute

            <Advice(Kind.After, Targets:=Target.Method)>
            Public Sub After()
                Passed = True
            End Sub
        End Class

        <Aspect(Scope.PerInstance)>
        <Injection(GetType(TestArgsAspect))>
        Public Class TestArgsAspect
            Inherits Attribute

            <Advice(Kind.After, Targets:=Target.Method)>
            Public Sub After(
            <Argument(Source.Arguments)> ByVal args As Object(),
            <Argument(Source.ReturnValue)> ByVal res As Object)
                Passed = True
            End Sub
        End Class
    End Class
End Namespace
