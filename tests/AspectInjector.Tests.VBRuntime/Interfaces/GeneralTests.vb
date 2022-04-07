Imports System
Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Interfaces
    Public Class GeneralTests
        Private _testClass As IGeneralTests
        Private _testClass2 As GeneralTests_Target2

        Public Sub New()
            _testClass = CType(New GeneralTests_Target(), IGeneralTests)
            _testClass2 = New GeneralTests_Target2()
        End Sub

        <Fact>
        Public Sub Inject_Mixin_If_Injected_into_Member()
            Assert.True(TypeOf _testClass2 Is IGeneralTests)
        End Sub

        <Fact>
        Public Sub Interfaces_InjectAspectReference_SecondConstructor()
            Dim ref1 As Object = New Object()
            Dim out1 As Object
            Dim ref2 = 2
            Dim out2 As Integer
            Passed = False
            Dim result = CType(New GeneralTests_Target(1), IGeneralTests).Fact("ok", 1, ref1, out1, ref2, out2, StringSplitOptions.None)
            Assert.Equal("ok", result)
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Interfaces_InjectMethodProxy()
            Dim ref1 As Object = New Object()
            Dim out1 As Object
            Dim ref2 = 2
            Dim out2 As Integer
            Passed = False
            Dim result = _testClass.Fact("ok", 1, ref1, out1, ref2, out2, StringSplitOptions.RemoveEmptyEntries)
            Assert.Equal("ok", result)
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Interfaces_InjectSetterProxy()
            Passed = False
            _testClass.TestProperty = 4
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Interfaces_InjectGetterProxy()
            Passed = False
            Dim a = _testClass.TestProperty
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Interfaces_InjectEventAddProxy()
            Passed = False
            AddHandler _testClass.TestEvent, AddressOf OnTestClassOnTestEvent
            Assert.True(Passed)
        End Sub

        <Fact>
        Public Sub Interfaces_InjectEventRemoveProxy()
            Passed = False
            RemoveHandler _testClass.TestEvent, AddressOf OnTestClassOnTestEvent
            Assert.True(Passed)
        End Sub

        Private Sub OnTestClassOnTestEvent(ByVal s As Object, ByVal e As EventArgs)
        End Sub

        <GeneralTests_Trigger>
        Friend Class GeneralTests_Target
            Public Sub New()
            End Sub

            Public Sub New(ByVal a As Integer)
            End Sub
        End Class

        Friend Class GeneralTests_Target2
            <GeneralTests_Trigger>
            Public Sub [Do]()
            End Sub
        End Class

        Friend Interface IGeneralTests
            Function Fact(ByVal data As String, ByVal value As Integer, ByRef testRef As Object, <Out> ByRef testOut As Object, ByRef testRefValue As Integer, <Out> ByRef testOutValue As Integer, ByVal stringSplit As StringSplitOptions) As String
            Event TestEvent As EventHandler(Of EventArgs)
            Property TestProperty As Integer
        End Interface

        <Injection(GetType(GeneralTests_Aspect))>
        Private Class GeneralTests_Trigger
            Inherits Attribute
        End Class

        <Mixin(GetType(IGeneralTests))>
        <Mixin(GetType(INotifyPropertyChanged))>
        <Aspect(Scope.Global)>
        Public Class GeneralTests_Aspect
            Implements IGeneralTests, INotifyPropertyChanged
            Private ReadOnly _events As New System.ComponentModel.EventHandlerList

            Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

            Private Function Fact(ByVal data As String, ByVal value As Integer, ByRef testRef As Object, <Out> ByRef testOut As Object, ByRef testRefValue As Integer, <Out> ByRef testOutValue As Integer, ByVal stringSplit As StringSplitOptions) As String Implements IGeneralTests.Fact
                Passed = True
                testOut = New Object()
                testOutValue = 0
                Return data
            End Function

            Public Custom Event TestEvent As EventHandler(Of EventArgs) Implements IGeneralTests.TestEvent
                AddHandler(ByVal value As EventHandler(Of EventArgs))
                    Passed = True
                    _events.AddHandler("TestEventEvent", value)
                End AddHandler
                RemoveHandler(ByVal value As EventHandler(Of EventArgs))
                    Passed = True
                    _events.RemoveHandler("TestEventEvent", value)
                End RemoveHandler
                RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
                    CType(_events("TestEventEvent"), EventHandler).Invoke(sender, e)
                End RaiseEvent
            End Event

            Public Property TestProperty As Integer Implements IGeneralTests.TestProperty
                Get
                    Passed = True
                    Return 0
                End Get
                Set(ByVal value As Integer)
                    Passed = True
                End Set
            End Property
        End Class
    End Class
End Namespace
