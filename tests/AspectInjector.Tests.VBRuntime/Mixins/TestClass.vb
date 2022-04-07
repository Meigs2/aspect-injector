Imports System
Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks
Imports AspectInjector.Tests.Assets

Namespace Mixins
    Partial Friend NotInheritable Class TestClassWrapper(Of T1 As Structure)
        Partial Public Class TestClass(Of T2 As Class)
            Public Sub New()
            End Sub

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Sub New(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As T1, ByVal a4 As T2, ByRef ar1 As Integer, ByRef ar2 As Asset1, ByRef ar3 As T1, ByRef ar4 As T2, <Out> ByRef ao1 As Integer, <Out> ByRef ao2 As Asset1, <Out> ByRef ao3 As T1, <Out> ByRef ao4 As T2)
                TestLog.Write(Events.TestConstructorEnter)
                ao1 = ar1
                ao2 = ar2
                ao3 = ar3
                ao4 = ar4
                TestLog.Write(Events.TestConstructorExit)
            End Sub

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Shared Sub New()
                TestLog.Write(Events.TestStaticConstructorEnter)
                TestLog.Write(Events.TestStaticConstructorExit)
            End Sub

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Function Fact(Of T3 As Class)(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As T1, ByVal a4 As T2, ByVal a5 As T3, ByRef ar1 As Integer, ByRef ar2 As Asset1, ByRef ar3 As T1, ByRef ar4 As T2, ByRef ar5 As T3, <Out> ByRef ao1 As Integer, <Out> ByRef ao2 As Asset1, <Out> ByRef ao3 As T1, <Out> ByRef ao4 As T2, <Out> ByRef ao5 As T3) As Tuple(Of Integer, Asset1, T1, T2, T3)
                TestLog.Write(Events.FactEnter)
                ao1 = ar1
                ao2 = ar2
                ao3 = ar3
                ao4 = ar4
                ao5 = ar5
                TestLog.Write(Events.FactExit)
                Return New Tuple(Of Integer, Asset1, T1, T2, T3)(a1, a2, a3, a4, a5)
            End Function

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Shared Function TestStaticMethod(Of T3 As Class)(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As T1, ByVal a4 As T2, ByVal a5 As T3, ByRef ar1 As Integer, ByRef ar2 As Asset1, ByRef ar3 As T1, ByRef ar4 As T2, ByRef ar5 As T3, <Out> ByRef ao1 As Integer, <Out> ByRef ao2 As Asset1, <Out> ByRef ao3 As T1, <Out> ByRef ao4 As T2, <Out> ByRef ao5 As T3) As Tuple(Of Integer, Asset1, T1, T2, T3)
                TestLog.Write(Events.TestStaticMethodEnter)
                ao1 = ar1
                ao2 = ar2
                ao3 = ar3
                ao4 = ar4
                ao5 = ar5
                TestLog.Write(Events.TestStaticMethodExit)
                Return New Tuple(Of Integer, Asset1, T1, T2, T3)(a1, a2, a3, a4, a5)
            End Function

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Iterator Function TestIteratorMethod(Of T3 As Class)(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As T1, ByVal a4 As T2, ByVal a5 As T3) As IEnumerable(Of Tuple(Of Integer, Asset1, T1, T2, T3))
                TestLog.Write(Events.TestIteratorMethodEnter)

                For i = 0 To a1 - 1
                    Yield New Tuple(Of Integer, Asset1, T1, T2, T3)(a1, a2, a3, a4, a5)
                Next

                TestLog.Write(Events.TestIteratorMethodExit)
            End Function

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Shared Iterator Function TestStaticIteratorMethod(Of T3 As Class)(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As T1, ByVal a4 As T2, ByVal a5 As T3) As IEnumerable(Of Tuple(Of Integer, Asset1, T1, T2, T3))
                TestLog.Write(Events.TestStaticIteratorMethodEnter)

                For i = 0 To a1 - 1
                    Yield New Tuple(Of Integer, Asset1, T1, T2, T3)(a1, a2, a3, a4, a5)
                Next

                TestLog.Write(Events.TestStaticIteratorMethodExit)
            End Function

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Async Function TestAsyncMethod1(Of T3 As Class)(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As T1, ByVal a4 As T2, ByVal a5 As T3) As Task(Of Tuple(Of Integer, Asset1, T1, T2, T3))
                TestLog.Write(Events.TestAsyncMethodEnter)
                Await Task.Delay(200)
                TestLog.Write(Events.TestAsyncMethodExit)
                Return New Tuple(Of Integer, Asset1, T1, T2, T3)(a1, a2, a3, a4, a5)
            End Function

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Shared Async Function TestStaticAsyncMethod1(Of T3 As Class)(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As T1, ByVal a4 As T2, ByVal a5 As T3) As Task(Of Tuple(Of Integer, Asset1, T1, T2, T3))
                TestLog.Write(Events.TestStaticAsyncMethodEnter)
                Await Task.Delay(200)
                TestLog.Write(Events.TestStaticAsyncMethodExit)
                Return New Tuple(Of Integer, Asset1, T1, T2, T3)(a1, a2, a3, a4, a5)
            End Function

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Async Function TestAsyncMethod2(Of T3 As Class)(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As T1, ByVal a4 As T2, ByVal a5 As T3) As Task
                TestLog.Write(Events.TestAsyncMethodEnter)
                Await Task.Delay(200)
                TestLog.Write(Events.TestAsyncMethodExit)
            End Function

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Shared Async Function TestStaticAsyncMethod2(Of T3 As Class)(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As T1, ByVal a4 As T2, ByVal a5 As T3) As Task
                TestLog.Write(Events.TestStaticAsyncMethodEnter)
                Await Task.Delay(200)
                TestLog.Write(Events.TestStaticAsyncMethodExit)
            End Function

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Async Function TestAsyncMethod3(Of T3 As Class)(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As T1, ByVal a4 As T2, ByVal a5 As T3) As Task
                TestLog.Write(Events.TestAsyncMethodEnter)
                Await Task.Delay(200)
                TestLog.Write(Events.TestAsyncMethodExit)
            End Function

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Shared Async Function TestStaticAsyncMethod3(Of T3 As Class)(ByVal a1 As Integer, ByVal a2 As Asset1, ByVal a3 As T1, ByVal a4 As T2, ByVal a5 As T3) As Task
                TestLog.Write(Events.TestStaticAsyncMethodEnter)
                Await Task.Delay(200)
                TestLog.Write(Events.TestStaticAsyncMethodExit)
            End Function

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Property TestProperty As Tuple(Of T1, T2)
                Get
                    TestLog.Write(Events.TestPropertyGetterEnter)
                    TestLog.Write(Events.TestPropertyGetterExit)
                    Return Nothing
                End Get
                Set(ByVal value As Tuple(Of T1, T2))
                    TestLog.Write(Events.TestPropertySetterEnter)
                    TestLog.Write(Events.TestPropertySetterExit)
                End Set
            End Property

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Shared Property TestStaticProperty As Tuple(Of T1, T2)
                Get
                    TestLog.Write(Events.TestStaticPropertyGetterEnter)
                    TestLog.Write(Events.TestStaticPropertyGetterExit)
                    Return Nothing
                End Get
                Set(ByVal value As Tuple(Of T1, T2))
                    TestLog.Write(Events.TestStaticPropertySetterEnter)
                    TestLog.Write(Events.TestStaticPropertySetterExit)
                End Set
            End Property

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Custom Event TestEvent As EventHandler(Of Tuple(Of T1, T2))
                AddHandler(ByVal value As EventHandler(Of Tuple(Of T1, T2)))
                    TestLog.Write(Events.TestEventAddEnter)
                    TestLog.Write(Events.TestEventAddExit)
                End AddHandler
                RemoveHandler(ByVal value As EventHandler(Of Tuple(Of T1, T2)))
                    TestLog.Write(Events.TestEventRemoveEnter)
                    TestLog.Write(Events.TestEventRemoveExit)
                End RemoveHandler
                <InjectInstanceAspect>
                <InjectGlobalAspect>
                RaiseEvent(ByVal sender As Object, ByVal e As Tuple(Of T1, T2))
                End RaiseEvent
            End Event

            <InjectInstanceAspect>
            <InjectGlobalAspect>
            Public Shared Custom Event TestStaticEvent As EventHandler(Of Tuple(Of T1, T2))
                AddHandler(ByVal value As EventHandler(Of Tuple(Of T1, T2)))
                    TestLog.Write(Events.TestStaticEventAddEnter)
                    TestLog.Write(Events.TestStaticEventAddExit)
                End AddHandler
                RemoveHandler(ByVal value As EventHandler(Of Tuple(Of T1, T2)))
                    TestLog.Write(Events.TestStaticEventRemoveEnter)
                    TestLog.Write(Events.TestStaticEventRemoveExit)
                End RemoveHandler
                <InjectInstanceAspect>
                <InjectGlobalAspect>
                RaiseEvent(ByVal sender As Object, ByVal e As Tuple(Of T1, T2))
                End RaiseEvent
            End Event
        End Class
    End Class
End Namespace
