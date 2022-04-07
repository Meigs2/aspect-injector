Imports System
Imports System.Linq
Imports System.Threading.Tasks
Imports AspectInjector.Broker
Imports Xunit

Namespace Advices
    Public Class CompilerGeneratedTargetsTests
        <TestAspect>
        Public Class TestTarget
            Public Sub Method()
                Dim a = {0, 1}
                a.Single(Function(x) x = 1)
            End Sub

            Public Async Function AsyncMethod() As Task
                Await Task.Delay(1)
            End Function
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(TestAspect))>
        Public Class TestAspect
            Inherits Attribute

            Public Shared beforeCalls As Integer = 0
            Public Shared afterCalls As Integer = 0
            Public Shared aroundCalls As Integer = 0

            <Advice(Kind.Before)>
            Public Sub Before()
                beforeCalls += 1
            End Sub

            <Advice(Kind.After)>
            Public Sub After()
                afterCalls += 1
            End Sub

            <Advice(Kind.Around)>
            Public Function Around(
            <Argument(Source.Target)> ByVal target As Func(Of Object(), Object),
            <Argument(Source.Arguments)> ByVal args As Object()) As Object
                aroundCalls += 1
                Return target(args)
            End Function

            Public Shared Sub Reset()
                beforeCalls = 0
                afterCalls = 0
                aroundCalls = 0
            End Sub
        End Class

        <Fact>
        Public Sub Does_Not_Inject_Into_Anonymous_Methods()
            Dim target = New TestTarget()
            Call TestAspect.Reset()
            target.Method()
            Assert.Equal(3, TestAspect.beforeCalls + TestAspect.afterCalls + TestAspect.aroundCalls)
        End Sub

        <Fact>
        Public Sub Does_Not_Inject_Into_Anonymous_AsyncStateMashines()
            Dim target = New TestTarget()
            Call TestAspect.Reset()
            target.AsyncMethod().GetAwaiter().GetResult()
            Assert.Equal(3, TestAspect.beforeCalls + TestAspect.afterCalls + TestAspect.aroundCalls)
        End Sub
    End Class
End Namespace
