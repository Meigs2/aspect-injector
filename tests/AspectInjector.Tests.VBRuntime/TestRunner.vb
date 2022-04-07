Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Runtime.CompilerServices
Imports System.Threading.Tasks
Imports AspectInjector.Tests.Assets
Imports Xunit


Public Class TestRunner
    Private Shared ReadOnly _staticCtorEvents As List(Of String)

    Public Sub New()
        Dim a = GetType(TestRunner)
        Call TestLog.Reset()
    End Sub

    Shared Sub New()
        Call TestLog.Reset()
        Dim type = GetType(TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1)))
        RuntimeHelpers.RunClassConstructor(type.TypeHandle)
        _staticCtorEvents = TestLog.Log.ToList()
        Call TestLog.Reset()
    End Sub

    Public Function GetConstructorArgsSequence(ByVal prefix As String) As List(Of String)
        Return New List(Of String) From {
            $"{prefix}:{GetArgEvent(TestAssets.asset1)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset2)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset3)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset4)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset1)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset2)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset3)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset4)}",
            $"{prefix}:{GetArgEvent(Nothing)}",
            $"{prefix}:{GetArgEvent(Nothing)}",
            $"{prefix}:{GetArgEvent(Nothing)}",
            $"{prefix}:{GetArgEvent(Nothing)}"
            }
    End Function

    Public Function GetMethodArgsSequence(ByVal prefix As String) As List(Of String)
        Return New List(Of String) From {
            $"{prefix}:{GetArgEvent(TestAssets.asset1)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset2)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset3)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset4)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset5)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset1)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset2)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset3)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset4)}",
            $"{prefix}:{GetArgEvent(TestAssets.asset5)}",
            $"{prefix}:{GetArgEvent(Nothing)}",
            $"{prefix}:{GetArgEvent(Nothing)}",
            $"{prefix}:{GetArgEvent(Nothing)}",
            $"{prefix}:{GetArgEvent(Nothing)}",
            $"{prefix}:{GetArgEvent(Nothing)}"
            }
    End Function

    Private Function GetArgEvent(ByVal o As Object) As String
        If o Is Nothing Then Return $"Arguments:null"
        Return $"Arguments:{o.GetType().Name}:{o.ToString()}"
    End Function

    Public Sub ExecConstructor()
        Dim ao1 As Integer
        Dim ao2 As Asset1
        Dim ao3 As Short
        Dim ao4 As IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1)
        Dim test = New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))(TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, ao1, ao2, ao3, ao4)
        Assert.Equal(TestAssets.asset1, ao1)
        Assert.Equal(TestAssets.asset2, ao2)
        Assert.Equal(TestAssets.asset3, ao3)
        Assert.Equal(TestAssets.asset4, ao4)
    End Sub

    Public Sub ExecStaticConstructor()
        Call _staticCtorEvents.ForEach(New Action(Of String)(AddressOf TestLog.Write))
    End Sub

    Public Sub ExecMethod()
        Dim ao1 As Integer
        Dim ao2 As Asset1
        Dim ao3 As Short
        Dim ao4 As IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1)
        Dim ao5 As Asset2
        Dim result = New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))().Fact(TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset5, TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset5, ao1, ao2, ao3, ao4, ao5)
        Assert.Equal(TestAssets.asset1, ao1)
        Assert.Equal(TestAssets.asset2, ao2)
        Assert.Equal(TestAssets.asset3, ao3)
        Assert.Equal(TestAssets.asset4, ao4)
        Assert.Equal(TestAssets.asset5, ao5)
        Assert.Equal(TestAssets.asset1, result.Item1)
        Assert.Equal(TestAssets.asset2, result.Item2)
        Assert.Equal(TestAssets.asset3, result.Item3)
        Assert.Equal(TestAssets.asset4, result.Item4)
        Assert.Equal(TestAssets.asset5, result.Item5)
    End Sub

    Public Sub ExecStaticMethod()
        Dim ao1 As Integer
        Dim ao2 As Asset1
        Dim ao3 As Short
        Dim ao4 As IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1)
        Dim ao5 As Asset2
        Dim result = TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1)).TestStaticMethod(TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset5, TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset5, ao1, ao2, ao3, ao4, ao5)
        Assert.Equal(TestAssets.asset1, ao1)
        Assert.Equal(TestAssets.asset2, ao2)
        Assert.Equal(TestAssets.asset3, ao3)
        Assert.Equal(TestAssets.asset4, ao4)
        Assert.Equal(TestAssets.asset5, ao5)
        Assert.Equal(TestAssets.asset1, result.Item1)
        Assert.Equal(TestAssets.asset2, result.Item2)
        Assert.Equal(TestAssets.asset3, result.Item3)
        Assert.Equal(TestAssets.asset4, result.Item4)
        Assert.Equal(TestAssets.asset5, result.Item5)
    End Sub

    Public Sub ExecSetter()
        Dim temp = New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))()
        temp.TestProperty =
            New _
                Tuple _
                    (Of Short,
                        IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))(
                            TestAssets.asset3, TestAssets.asset4)

    End Sub

    Public Sub ExecStaticSetter()
        TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1)).TestStaticProperty = New Tuple(Of Short, IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))(TestAssets.asset3, TestAssets.asset4)
    End Sub

    Public Sub ExecGetter()
        Dim result = New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))().TestProperty
    End Sub

    Public Sub ExecStaticGetter()
        Dim result = TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1)).TestStaticProperty
    End Sub

    Public Sub ExecAdd()
        AddHandler New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))().TestEvent, Sub(s, e)
        End Sub
    End Sub

    Public Sub ExecStaticAdd()
        AddHandler TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1)).TestStaticEvent, Sub(s, e)
        End Sub
    End Sub

    Public Sub ExecRemove()
        RemoveHandler New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))().TestEvent, Sub(s, e)
        End Sub
    End Sub

    Public Sub ExecStaticRemove()
        RemoveHandler TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1)).TestStaticEvent, Sub(s, e)
        End Sub
    End Sub

    Public Sub ExecIteratorMethod()
        Dim result = New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))().TestIteratorMethod(TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset5).Last()
        Assert.Equal(TestAssets.asset1, result.Item1)
        Assert.Equal(TestAssets.asset2, result.Item2)
        Assert.Equal(TestAssets.asset3, result.Item3)
        Assert.Equal(TestAssets.asset4, result.Item4)
        Assert.Equal(TestAssets.asset5, result.Item5)
    End Sub

    Public Sub ExecStaticIteratorMethod()
        Dim result = TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1)).TestStaticIteratorMethod(TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset5).Last()
        Assert.Equal(TestAssets.asset1, result.Item1)
        Assert.Equal(TestAssets.asset2, result.Item2)
        Assert.Equal(TestAssets.asset3, result.Item3)
        Assert.Equal(TestAssets.asset4, result.Item4)
        Assert.Equal(TestAssets.asset5, result.Item5)
    End Sub

    Public Async Function ExecAsyncTypedTaskMethod() As Task
        Dim a = New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))
        Dim result = await a.TestAsyncMethod1(TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset5)
        Assert.Equal(TestAssets.asset1, result.Item1)
        Assert.Equal(TestAssets.asset2, result.Item2)
        Assert.Equal(TestAssets.asset3, result.Item3)
        Assert.Equal(TestAssets.asset4, result.Item4)
        Assert.Equal(TestAssets.asset5, result.Item5)
    End Function

    Public Async Function ExecStaticAsyncTypedTaskMethod() As Task
        Dim a = New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))
        Dim result = await a.TestStaticAsyncMethod1(TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset5)
        Assert.Equal(TestAssets.asset1, result.Item1)
        Assert.Equal(TestAssets.asset2, result.Item2)
        Assert.Equal(TestAssets.asset3, result.Item3)
        Assert.Equal(TestAssets.asset4, result.Item4)
        Assert.Equal(TestAssets.asset5, result.Item5)
    End Function

    Public Async Function ExecAsyncTaskMethod() As Task
        Dim a = New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))
        await a.TestAsyncMethod2(TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset5)
    End Function

    Public Async Function ExecStaticAsyncTaskMethod() As Task
        Dim a = New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))
        await a.TestStaticAsyncMethod2(TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset5)
    End Function

    Public Async Function ExecAsyncVoidMethod() As Task
        Dim a = New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))
        await a.TestAsyncMethod3(TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset5)
        await Task.Delay(300)
    End Function

    Public Async Function ExecStaticAsyncVoidMethod() As Task
        Dim a = New TestClassWrapper(Of Short).TestClass(Of IAssetIface1Wrapper(Of Global.AspectInjector.Tests.Assets.Asset1).IAssetIface1(Of Asset1))
        await a.TestStaticAsyncMethod3(TestAssets.asset1, TestAssets.asset2, TestAssets.asset3, TestAssets.asset4, TestAssets.asset5)
        await Task.Delay(300)
    End Function

    Public Sub CheckSequence(ByVal orderedEvents As IReadOnlyList(Of String))
        Dim logEvents = TestLog.Log.Where(Function(e) orderedEvents.Contains(e))
        If Not logEvents.SequenceEqual(orderedEvents) Then Assert.True(False, String.Join(Environment.NewLine, logEvents))
    End Sub
End Class