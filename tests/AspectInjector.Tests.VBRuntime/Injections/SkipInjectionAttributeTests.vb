Imports System
Imports AspectInjector.Broker
Imports Xunit

Namespace Injections
    Public Class SkipInjectionAttributeTests
        <Fact>
        Public Sub DontInjectIntoSkippedMember()
            Dim target = New TestMethodTarget()
            Assert.Equal(0, TestMethodTarget.Counter)
            Dim dummy = target.FirstProperty
            Assert.Equal(0, TestMethodTarget.Counter)
            dummy = target.SecondProperty
            Assert.Equal(1, TestMethodTarget.Counter)
            AddHandler target.FirstEvent, Sub(s, e)
                                          End Sub

            Assert.Equal(1, TestMethodTarget.Counter)
            AddHandler target.SecondEvent, Sub(s, e)
                                           End Sub

            Assert.Equal(2, TestMethodTarget.Counter)
            target.FirstMethod()
            Assert.Equal(2, TestMethodTarget.Counter)
            target.SecondMethod()
            Assert.Equal(3, TestMethodTarget.Counter)
        End Sub

        <Fact>
        Public Sub DontInjectIntoSkippedClass()
            Dim target = New TestClassTarget()
            Assert.Equal(0, TestClassTarget.Counter)
            target.Method()
            Assert.Equal(0, TestClassTarget.Counter)
        End Sub

        <TestMethodAspect>
        Private Class TestMethodTarget
            Public Shared Counter As Integer = 0

            <SkipInjection>
            Shared Sub New()
            End Sub

            <SkipInjection>
            Public Sub New()
            End Sub

            <SkipInjection>
            Public Property FirstProperty As Integer
            Public Property SecondProperty As Integer
            <SkipInjection>
            Public Event FirstEvent As EventHandler
            Public Event SecondEvent As EventHandler

            <SkipInjection>
            Public Sub FirstMethod()
            End Sub

            Public Sub SecondMethod()
            End Sub
        End Class

        <TestClassAspect>
        <SkipInjection>
        Private Class TestClassTarget
            Public Shared Counter As Integer = 0

            Public Sub New()
            End Sub

            Public Sub Method()
            End Sub
        End Class

        <Injection(GetType(TestMethodAspect))>
        <Aspect(Scope.Global)>
        Public Class TestMethodAspect
            Inherits Attribute

            <Advice(Kind.Before)>
            Public Sub Before()
                TestMethodTarget.Counter += 1
            End Sub
        End Class

        <Injection(GetType(TestClassAspect))>
        <Aspect(Scope.Global)>
        Public Class TestClassAspect
            Inherits Attribute

            <Advice(Kind.Before)>
            Public Sub Before()
                TestClassTarget.Counter += 1
            End Sub
        End Class
    End Class
End Namespace
