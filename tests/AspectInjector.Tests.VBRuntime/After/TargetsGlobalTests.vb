Imports System.Collections.Generic
Imports AspectInjector.Tests.Assets
Imports Xunit

Namespace After
    Public Class Global_Tests
        Inherits TestRunner

        Protected Overridable ReadOnly Property Token As String
            Get
                Return GlobalAspect.AfterExecuted
            End Get
        End Property

        <Fact>
        Public Sub AdviceAfter_Constructor()
            MyBase.ExecConstructor()
            MyBase.CheckSequence(New List(Of String) From {
                Events.TestConstructorExit,
                Token
            })
        End Sub

        <Fact>
        Public Sub AdviceAfter_Static_Constructor()
            MyBase.ExecStaticConstructor()
            MyBase.CheckSequence(New List(Of String) From {
                Events.TestStaticConstructorExit,
                Token
            })
        End Sub

        <Fact>
        Public Sub AdviceAfter_Setter()
            MyBase.ExecSetter()
            MyBase.CheckSequence(New List(Of String) From {
                Events.TestPropertySetterExit,
                Token
            })
        End Sub

        <Fact>
        Public Sub AdviceAfter_Static_Setter()
            MyBase.ExecStaticSetter()
            MyBase.CheckSequence(New List(Of String) From {
                Events.TestStaticPropertySetterExit,
                Token
            })
        End Sub

        <Fact>
        Public Sub AdviceAfter_Getter()
            MyBase.ExecGetter()
            MyBase.CheckSequence(New List(Of String) From {
                Events.TestPropertyGetterExit,
                Token
            })
        End Sub

        <Fact>
        Public Sub AdviceAfter_Static_Getter()
            MyBase.ExecStaticGetter()
            MyBase.CheckSequence(New List(Of String) From {
                Events.TestStaticPropertyGetterExit,
                Token
            })
        End Sub

        <Fact>
        Public Sub AdviceAfter_Add()
            MyBase.ExecAdd()
            MyBase.CheckSequence(New List(Of String) From {
                Events.TestEventAddExit,
                Token
            })
        End Sub

        <Fact>
        Public Sub AdviceAfter_Static_Add()
            MyBase.ExecStaticAdd()
            MyBase.CheckSequence(New List(Of String) From {
                Events.TestStaticEventAddExit,
                Token
            })
        End Sub

        <Fact>
        Public Sub AdviceAfter_Remove()
            MyBase.ExecRemove()
            MyBase.CheckSequence(New List(Of String) From {
                Events.TestEventRemoveExit,
                Token
            })
        End Sub

        <Fact>
        Public Sub AdviceAfter_Static_Remove()
            MyBase.ExecStaticRemove()
            MyBase.CheckSequence(New List(Of String) From {
                Events.TestStaticEventRemoveExit,
                Token
            })
        End Sub

        <Fact>
        Public Async Sub AdviceAfter_Methods()
            MyBase.ExecMethod()
            MyBase.ExecIteratorMethod()
            Await MyBase.ExecAsyncVoidMethod()
            Await MyBase.ExecAsyncTaskMethod()
            Await MyBase.ExecAsyncTypedTaskMethod()
            MyBase.CheckSequence(New List(Of String) From {
                Events.FactExit,
                Token,
                Events.TestIteratorMethodExit,
                Token,
                Events.TestAsyncMethodExit,
                Token,
                Events.TestAsyncMethodExit,
                Token,
                Events.TestAsyncMethodExit,
                Token
            })
        End Sub

        <Fact>
        Public Async Sub AdviceAfter_Static_Methods()
            MyBase.ExecStaticMethod()
            MyBase.ExecStaticIteratorMethod()
            Await MyBase.ExecStaticAsyncVoidMethod()
            Await MyBase.ExecStaticAsyncTaskMethod()
            Await MyBase.ExecStaticAsyncTypedTaskMethod()
            MyBase.CheckSequence(New List(Of String) From {
                Events.TestStaticMethodExit,
                Token,
                Events.TestStaticIteratorMethodExit,
                Token,
                Events.TestStaticAsyncMethodExit,
                Token,
                Events.TestStaticAsyncMethodExit,
                Token,
                Events.TestStaticAsyncMethodExit,
                Token
            })
        End Sub
    End Class
End Namespace
