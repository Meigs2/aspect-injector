Imports System.Collections.Generic
Imports AspectInjector.Tests.Assets
Imports Xunit

Namespace Around
    Public Class Global_Tests
        Inherits TestRunner

        Protected Overridable ReadOnly Property EnterToken As String
            Get
                Return GlobalAspect.AroundEnter
            End Get
        End Property

        Protected Overridable ReadOnly Property ExitToken As String
            Get
                Return GlobalAspect.AroundExit
            End Get
        End Property

        <Fact>
        Public Sub AdviceAround_Static_Constructor()
            MyBase.ExecStaticConstructor()
            MyBase.CheckSequence(New List(Of String) From {
                EnterToken,
                Events.TestStaticConstructorEnter,
                Events.TestStaticConstructorExit,
                ExitToken
            })
        End Sub

        <Fact>
        Public Sub AdviceAround_Constructor()
            MyBase.ExecConstructor()
            MyBase.CheckSequence(New List(Of String) From {
                EnterToken,
                Events.TestConstructorEnter,
                Events.TestConstructorExit,
                ExitToken
            })
        End Sub

        <Fact>
        Public Sub AdviceAround_Setter()
            MyBase.ExecSetter()
            MyBase.CheckSequence(New List(Of String) From {
                EnterToken,
                Events.TestPropertySetterEnter,
                Events.TestPropertySetterExit,
                ExitToken
            })
        End Sub

        <Fact>
        Public Sub AdviceAround_Static_Setter()
            MyBase.ExecStaticSetter()
            MyBase.CheckSequence(New List(Of String) From {
                EnterToken,
                Events.TestStaticPropertySetterEnter,
                Events.TestStaticPropertySetterExit,
                ExitToken
            })
        End Sub

        <Fact>
        Public Sub AdviceAround_Getter()
            MyBase.ExecGetter()
            MyBase.CheckSequence(New List(Of String) From {
                EnterToken,
                Events.TestPropertyGetterEnter,
                Events.TestPropertyGetterExit,
                ExitToken
            })
        End Sub

        <Fact>
        Public Sub AdviceAround_Static_Getter()
            MyBase.ExecStaticGetter()
            MyBase.CheckSequence(New List(Of String) From {
                EnterToken,
                Events.TestStaticPropertyGetterEnter,
                Events.TestStaticPropertyGetterExit,
                ExitToken
            })
        End Sub

        <Fact>
        Public Sub AdviceAround_Add()
            MyBase.ExecAdd()
            MyBase.CheckSequence(New List(Of String) From {
                EnterToken,
                Events.TestEventAddEnter,
                Events.TestEventAddExit,
                ExitToken
            })
        End Sub

        <Fact>
        Public Sub AdviceAround_Static_Add()
            MyBase.ExecStaticAdd()
            MyBase.CheckSequence(New List(Of String) From {
                EnterToken,
                Events.TestStaticEventAddEnter,
                Events.TestStaticEventAddExit,
                ExitToken
            })
        End Sub

        <Fact>
        Public Sub AdviceAround_Remove()
            MyBase.ExecRemove()
            MyBase.CheckSequence(New List(Of String) From {
                EnterToken,
                Events.TestEventRemoveEnter,
                Events.TestEventRemoveExit,
                ExitToken
            })
        End Sub

        <Fact>
        Public Sub AdviceAround_Static_Remove()
            MyBase.ExecStaticRemove()
            MyBase.CheckSequence(New List(Of String) From {
                EnterToken,
                Events.TestStaticEventRemoveEnter,
                Events.TestStaticEventRemoveExit,
                ExitToken
            })
        End Sub

        <Fact>
        Public Async Sub AdviceAround_Methods()
            MyBase.ExecMethod()
            MyBase.ExecIteratorMethod()
            Await MyBase.ExecAsyncVoidMethod()
            Await MyBase.ExecAsyncTaskMethod()
            Await MyBase.ExecAsyncTypedTaskMethod()
            MyBase.CheckSequence(New List(Of String) From {
                EnterToken,
                Events.FactEnter,
                Events.FactExit,
                ExitToken,
                EnterToken,
                ExitToken,
                Events.TestIteratorMethodExit,
                EnterToken,
                ExitToken,
                Events.TestAsyncMethodExit,
                EnterToken,
                ExitToken,
                Events.TestAsyncMethodExit,
                EnterToken,
                ExitToken,
                Events.TestAsyncMethodExit
            })
        End Sub

        <Fact>
        Public Async Sub AdviceAround_Static_Methods()
            MyBase.ExecStaticMethod()
            MyBase.ExecStaticIteratorMethod()
            Await MyBase.ExecStaticAsyncVoidMethod()
            Await MyBase.ExecStaticAsyncTaskMethod()
            Await MyBase.ExecStaticAsyncTypedTaskMethod()
            MyBase.CheckSequence(New List(Of String) From {
                EnterToken,
                Events.TestStaticMethodEnter,
                Events.TestStaticMethodExit,
                ExitToken,
                EnterToken,
                ExitToken,
                Events.TestStaticIteratorMethodExit,
                EnterToken,
                ExitToken,
                Events.TestStaticAsyncMethodExit,
                EnterToken,
                ExitToken,
                Events.TestStaticAsyncMethodExit,
                EnterToken,
                ExitToken,
                Events.TestStaticAsyncMethodExit
            })
        End Sub
    End Class
End Namespace
