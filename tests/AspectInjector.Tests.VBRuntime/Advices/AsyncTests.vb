Imports System
Imports System.Threading.Tasks
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Advices
    Public Class AsyncTests
        Public Shared Data As Boolean = False

        <Fact>
        Public Sub Advices_InjectAfterAsyncMethod()
            Data = False
            Passed = False
            Dim a = New AsyncTests_Target()
            a.Do().Wait()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAfterAsyncMethod_WithResult()
            Data = False
            Passed = False
            Dim a = New AsyncTests_Target()
            Dim result = a.Do2().Result
            Assert.Equal("test", result)
        End Sub

        <Fact>
        Public Sub Advices_InjectAfterAsyncMethod_Void()
            Data = False
            Passed = False
            Dim a = New AsyncTests_Target()
            a.Do3()
            Call Task.Delay(200).Wait()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAfterAsyncMethod_WithArguments_And_Result()
            Data = False
            Passed = False
            Dim a = New AsyncTests_Target()
            a.Do4("args_test").ConfigureAwait(False).GetAwaiter().GetResult()
            Assert.True(Passed)
        End Sub
    End Class

    Public Class AsyncTests_Target
        <AsyncTests_SimpleAspect>
        Public Async Function [Do]() As Task
            Await Task.Delay(1)
            AsyncTests.Data = True
        End Function

        <AsyncTests_SimpleAspectGlobal>
        Public Async Function Do2() As Task(Of String)
            Await Task.Delay(1)
            AsyncTests.Data = True
            Return "test"
        End Function

        <AsyncTests_SimpleAspect>
        Public Async Sub Do3()
            Await Task.Delay(1)
            AsyncTests.Data = True
        End Sub

        <AsyncTests_ArgumentsAspect>
        Public Async Function Do4(ByVal testData As String) As Task(Of String)
            Await Task.Delay(1)
            AsyncTests.Data = True
            Return testData
        End Function

        <Aspect(Scope.Global)>
        <Injection(GetType(AsyncTests_ArgumentsAspect))>
        Public Class AsyncTests_ArgumentsAspect
            Inherits Attribute

            <Advice(Kind.After, Targets:=Target.Method)>
            Public Sub AfterMethod(
            <Argument(Source.ReturnValue)> ByVal value As Object,
            <Argument(Source.Arguments)> ByVal args As Object())
                Passed = Equals(args(0).ToString(), "args_test") AndAlso value IsNot Nothing
            End Sub
        End Class

        <Aspect(Scope.PerInstance)>
        <Injection(GetType(AsyncTests_SimpleAspect))>
        Public Class AsyncTests_SimpleAspect
            Inherits Attribute

            <Advice(Kind.After, Targets:=Target.Method)>
            Public Sub AfterMethod(
            <Argument(Source.Arguments)> ByVal args As Object(),
            <Argument(Source.Instance)> ByVal th As Object)
                Passed = AsyncTests.Data
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(AsyncTests_SimpleAspectGlobal))>
        Public Class AsyncTests_SimpleAspectGlobal
            Inherits Attribute

            <Advice(Kind.After, Targets:=Target.Method)>
            Public Sub AfterMethod(
            <Argument(Source.Arguments)> ByVal args As Object(),
            <Argument(Source.Instance)> ByVal th As Object)
                Console.WriteLine(th.ToString())
                Passed = AsyncTests.Data
            End Sub
        End Class
    End Class
End Namespace
