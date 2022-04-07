Imports System
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Advices
    Public Class GenericTests
        Private _target As GenericTests_Target(Of String) = New GenericTests_Target(Of String)()
        Private _openGenericTarget As GenericTests_OpenGenericTarget(Of String, Integer) = New GenericTests_OpenGenericTarget(Of String, Integer)()

        <Fact>
        Public Sub Advices_InjectBeforeMethod_GenericClass()
            Passed = False
            _target.Fact()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectBeforeMethod_GenericMethod()
            Passed = False
            _target.TestGenericMethod(Of Object)()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectBeforeMethod_OpenGenericMethod()
            Passed = False
            _openGenericTarget.Fact()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAroundMethod_GenericMethod()
            Passed = False
            Dim target = New GenericAroundTests_Target()
            Dim rr = String.Empty
            target.Fact(rr)
            Assert.True(Passed)
        End Sub
    End Class

    <GenericTests_Aspect>
    Friend Class GenericTests_Target(Of T)
        Public Sub Fact()
        End Sub

        Public Sub TestGenericMethod(Of U)()
        End Sub
    End Class

    <Aspect(Scope.Global)>
    <Injection(GetType(GenericTests_Aspect))>
    Public Class GenericTests_Aspect
        Inherits Attribute

        <Advice(Kind.Before, Targets:=Target.Method)>
        Public Sub BeforeMethod()
            Passed = True
        End Sub
    End Class

    Friend Class GenericTests_OpenGenericTargetBase(Of T, U)
    End Class

    <GenericTests_OpenGenericAspect>
    Friend Class GenericTests_OpenGenericTarget(Of U, V)
        Inherits GenericTests_OpenGenericTargetBase(Of String, U)

        Public Sub Fact()
        End Sub
    End Class

    <Aspect(Scope.Global)>
    <Injection(GetType(GenericTests_OpenGenericAspect))>
    Public Class GenericTests_OpenGenericAspect
        Inherits Attribute

        <Advice(Kind.Before, Targets:=Target.Method)>
        Public Sub BeforeMethod()
            Passed = True
        End Sub
    End Class

    <GenericAroundTests_Aspect>
    Friend Class GenericAroundTests_Target
        Public Function Fact(Of T)(ByRef value As T) As T
            Dim a = New Object() {value}
            Return value
        End Function
    End Class

    <Aspect(Scope.Global)>
    <Injection(GetType(GenericAroundTests_Aspect))>
    Public Class GenericAroundTests_Aspect
        Inherits Attribute

        <Advice(Kind.Around, Targets:=Target.Method)>
        Public Function AroundAdvice(
        <Argument(Source.Target)> ByVal target As Func(Of Object(), Object),
        <Argument(Source.Arguments)> ByVal arguments As Object()) As Object
            Passed = True
            Return target(arguments)
        End Function
    End Class
End Namespace
