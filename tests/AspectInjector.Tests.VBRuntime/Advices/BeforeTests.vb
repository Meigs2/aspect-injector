Imports System
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Advices
    Public Class BeforeTests
        Private _beforeTestClass As BeforeTests_Target = New BeforeTests_Target()

        <Fact>
        Public Sub Advices_InjectBeforeMethod()
            Passed = False
            _beforeTestClass.Fact("")
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectBeforeSetter()
            Passed = False
            _beforeTestClass.TestProperty = 1
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectBeforeGetter()
            Passed = False
            Dim a = _beforeTestClass.TestProperty
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectBeforeAddEvent()
            Passed = False
            AddHandler _beforeTestClass.TestEvent, AddressOf OnBeforeTestClassOnTestEvent
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectBeforeRemoveEvent()
            Passed = False
            RemoveHandler _beforeTestClass.TestEvent, AddressOf OnBeforeTestClassOnTestEvent
            Assert.True(Passed)
        End Sub

        Private Sub OnBeforeTestClassOnTestEvent(ByVal s As Object, ByVal e As EventArgs)
        End Sub

        <Fact>
        Public Sub Advices_InjectBeforeConstructor()
            Passed = False
            Dim a = New BeforeTests_BeforeConstructorTarget()
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Advices_InjectBeforeConstructor_WithInterface()
            Passed = False
            Dim a = New BeforeTests_BeforeConstructorWithInterfaceTarget()
            Assert.True(Passed)
        End Sub
    End Class

    'test classes

    <BeforeTests_BeforeConstructorWithInterfaceAspect>
    Friend Class BeforeTests_BeforeConstructorWithInterfaceTarget
    End Class

    <BeforeTests_BeforeConstructorAspect>
    Friend Class BeforeTests_BeforeConstructorTarget
        Private testField As BeforeTests_Target = New BeforeTests_Target()
    End Class

    <BeforeTests_Aspect>
    Friend Class BeforeTests_Target
        Public Sub Fact(ByVal data As String)
        End Sub

        Public Event TestEvent As EventHandler(Of EventArgs)
        Public Property TestProperty As Integer
    End Class

    'aspects
    <Aspect(Scope.Global)>
    <Injection(GetType(BeforeTests_Aspect))>
    Public Class BeforeTests_Aspect
        Inherits Attribute
        'Property
        <Advice(Kind.Before, Targets:=Target.Setter)>
        Public Sub BeforeSetter()
            Passed = True
        End Sub

        <Advice(Kind.Before, Targets:=Target.Getter)>
        Public Sub BeforeGetter()
            Passed = True
        End Sub

        'Event
        <Advice(Kind.Before, Targets:=Target.EventAdd)>
        Public Sub BeforeEventAdd()
            Passed = True
        End Sub

        <Advice(Kind.Before, Targets:=Target.EventRemove)>
        Public Sub BeforeEventRemove()
            Passed = True
        End Sub

        'Method
        <Advice(Kind.Before, Targets:=Target.Method)>
        Public Sub BeforeMethod()
            Passed = True
        End Sub
    End Class

    <Aspect(Scope.Global)>
    <Injection(GetType(BeforeTests_BeforeConstructorAspect))>
    Public Class BeforeTests_BeforeConstructorAspect
        Inherits Attribute

        <Advice(Kind.Before, Targets:=Target.Constructor)>
        Public Sub BeforeConstructor(
        <Argument(Source.Instance)> ByVal instance As Object)
            If instance IsNot Nothing Then Passed = True
        End Sub
    End Class

    <Mixin(GetType(IDisposable))>
    <Aspect(Scope.Global)>
    <Injection(GetType(BeforeTests_BeforeConstructorWithInterfaceAspect))>
    Public Class BeforeTests_BeforeConstructorWithInterfaceAspect
        Inherits Attribute
        Implements IDisposable

        <Advice(Kind.Before, Targets:=Target.Constructor)>
        Public Sub BeforeConstructor()
            Passed = True
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Throw New NotImplementedException()
        End Sub
    End Class
End Namespace
