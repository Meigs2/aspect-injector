Imports System
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace AspectInjector.Tests.Advices
    Public Class AfterTests
        Private _afterTestClass As AfterTests_AfterMethodTarget = New AfterTests_AfterMethodTarget()


        'after

        <Fact>
        Public Sub Advices_InjectAfterMethod()
            Passed = False
            _afterTestClass.TestMethod("")
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAfterSetter()
            Passed = False
            _afterTestClass.TestProperty = 1
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAfterCustomSetter()
            Passed = False
            _afterTestClass.TestCustomSetterProperty = 2
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAfterCustomSetter2()
            Passed = False
            _afterTestClass.TestCustomSetterProperty = 1
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAfterGetter()
            Passed = False
            Dim a = _afterTestClass.TestProperty
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAfterAddEvent()
            Passed = False
            AddHandler _afterTestClass.TestEvent, AddressOf OnAfterTestClassOnTestEvent
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAfterRemoveEvent()
            Passed = False
            RemoveHandler _afterTestClass.TestEvent, AddressOf OnAfterTestClassOnTestEvent
            Assert.True(Passed)
        End Sub

        Private Sub OnAfterTestClassOnTestEvent(ByVal s As Object, ByVal e As EventArgs)
        End Sub

        'constructors

        <Fact>
        Public Sub Advices_InjectAfterConstructor()
            Passed = False
            Dim a = New AfterTests_AfterConstructorTarget()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectAfterSecondConstructor()
            Passed = False
            Dim a = New AfterTests_AfterConstructorTarget("")
            Assert.True(Passed)
        End Sub
    End Class

    'test classes

    <AfterTests_AfterConstructorAspect>
    Friend Class AfterTests_AfterConstructorTarget
        Public Sub New()
        End Sub

        Public Sub New(ByVal a As String)
        End Sub
    End Class

    <AfterTests_AfterMethodAspect>
    Friend Class AfterTests_AfterMethodTarget
        Public Sub TestMethod(ByVal data As String)
        End Sub

        Public Event TestEvent As EventHandler(Of EventArgs)
        Public Property TestProperty As Integer

        Public Property TestCustomSetterProperty As Integer
            Get
                Return 1
            End Get
            Set(ByVal value As Integer)

                If True Then
                    If value = 2 Then Return
                End If
            End Set
        End Property
    End Class

    'aspects
    <Aspect(Scope.Global)>
    <Injection(GetType(AfterTests_AfterMethodAspect))>
    Public Class AfterTests_AfterMethodAspect
        Inherits Attribute
        'Property
        <Advice(Kind.After, Targets:=Target.Setter)>
        Public Sub AfterSetter()
            Passed = True
        End Sub

        <Advice(Kind.After, Targets:=Target.Getter)>
        Public Sub AfterGetter()
            Passed = True
        End Sub

        'Event
        <Advice(Kind.After, Targets:=Target.EventAdd)>
        Public Sub AfterEventAdd()
            Passed = True
        End Sub

        <Advice(Kind.After, Targets:=Target.EventRemove)>
        Public Sub AfterEventRemove()
            Passed = True
        End Sub

        'Method
        <Advice(Kind.After, Targets:=Target.Method)>
        Public Sub AfterMethod()
            Passed = True
        End Sub
    End Class

    <Aspect(Scope.Global)>
    <Injection(GetType(AfterTests_AfterConstructorAspect))>
    Public Class AfterTests_AfterConstructorAspect
        Inherits Attribute

        <Advice(Kind.After, Targets:=Target.Constructor)>
        Public Sub AfterConstructor()
            Passed = True
        End Sub
    End Class
End Namespace
