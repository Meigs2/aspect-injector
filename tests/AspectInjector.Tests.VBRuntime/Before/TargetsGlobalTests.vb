Imports System.Collections.Generic
Imports AspectInjector.Tests.Assets
Imports Xunit

Namespace Before
    Public Class Global_Tests
        Inherits TestRunner

        Protected Overridable ReadOnly Property Token As String
            Get
                Return GlobalAspect.BeforeExecuted
            End Get
        End Property

        <Fact>
        Public Sub AdviceBefore_Consrtuctor()
            MyBase.ExecConstructor()
            MyBase.CheckSequence(New List(Of String) From {
                Token,
                Events.TestConstructorEnter
            })
        End Sub

        <Fact>
        Public Sub AdviceBefore_Static_Consrtuctor()
            MyBase.ExecStaticConstructor()
            MyBase.CheckSequence(New List(Of String) From {
                Token,
                Events.TestStaticConstructorEnter
            })
        End Sub

        <Fact>
        Public Sub AdviceBefore_Setter()
            MyBase.ExecSetter()
            MyBase.CheckSequence(New List(Of String) From {
                Token,
                Events.TestPropertySetterEnter
            })
        End Sub

        <Fact>
        Public Sub AdviceBefore_Static_Setter()
            MyBase.ExecStaticSetter()
            MyBase.CheckSequence(New List(Of String) From {
                Token,
                Events.TestStaticPropertySetterEnter
            })
        End Sub

        <Fact>
        Public Sub AdviceBefore_Getter()
            MyBase.ExecGetter()
            MyBase.CheckSequence(New List(Of String) From {
                Token,
                Events.TestPropertyGetterEnter
            })
        End Sub

        <Fact>
        Public Sub AdviceBefore_Static_Getter()
            MyBase.ExecStaticGetter()
            MyBase.CheckSequence(New List(Of String) From {
                Token,
                Events.TestStaticPropertyGetterEnter
            })
        End Sub

        <Fact>
        Public Sub AdviceBefore_Add()
            MyBase.ExecAdd()
            MyBase.CheckSequence(New List(Of String) From {
                Token,
                Events.TestEventAddEnter
            })
        End Sub

        <Fact>
        Public Sub AdviceBefore_Static_Add()
            MyBase.ExecStaticAdd()
            MyBase.CheckSequence(New List(Of String) From {
                Token,
                Events.TestStaticEventAddEnter
            })
        End Sub

        <Fact>
        Public Sub AdviceBefore_Remove()
            MyBase.ExecRemove()
            MyBase.CheckSequence(New List(Of String) From {
                Token,
                Events.TestEventRemoveEnter
            })
        End Sub

        <Fact>
        Public Sub AdviceBefore_Static_Remove()
            MyBase.ExecStaticRemove()
            MyBase.CheckSequence(New List(Of String) From {
                Token,
                Events.TestStaticEventRemoveEnter
            })
        End Sub

        <Fact>
        Public Async Sub AdviceBefore_Methods()
            MyBase.ExecMethod()
            MyBase.ExecIteratorMethod()
            Await MyBase.ExecAsyncVoidMethod()
            Await MyBase.ExecAsyncTaskMethod()
            Await MyBase.ExecAsyncTypedTaskMethod()
            MyBase.CheckSequence(New List(Of String) From {
                Token,
                Events.FactEnter,
                Token,
                Events.TestIteratorMethodEnter,
                Token,
                Events.TestAsyncMethodEnter,
                Token,
                Events.TestAsyncMethodEnter,
                Token,
                Events.TestAsyncMethodEnter
            })
        End Sub

        <Fact>
        Public Async Sub AdviceBefore_Static_Methods()
            MyBase.ExecStaticMethod()
            MyBase.ExecStaticIteratorMethod()
            Await MyBase.ExecStaticAsyncVoidMethod()
            Await MyBase.ExecStaticAsyncTaskMethod()
            Await MyBase.ExecStaticAsyncTypedTaskMethod()
            MyBase.CheckSequence(New List(Of String) From {
                Token,
                Events.TestStaticMethodEnter,
                Token,
                Events.TestStaticIteratorMethodEnter,
                Token,
                Events.TestStaticAsyncMethodEnter,
                Token,
                Events.TestStaticAsyncMethodEnter,
                Token,
                Events.TestStaticAsyncMethodEnter
            })
        End Sub
    End Class
End Namespace
