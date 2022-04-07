Imports System
Imports AspectInjector.Broker
Imports AspectInjector.Tests.RuntimeAssets.CrossAssemblyHelpers
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace InheritedInjections
    <Aspect(Scope.Global)>
    Public Class TestAspect
        <Advice(Kind.Around)>
        Public Function Around(
        <Argument(Source.Target)> ByVal target As Func(Of Object(), Object),
        <Argument(Source.Arguments)> ByVal args As Object(),
        <Argument(Source.Triggers)> ByVal attrs As Attribute()) As Object
            Dim res = target(args)
            Passed = True
            Return res
        End Function
    End Class

    <Injection(GetType(TestAspect), Inherited:=True)>
    Public MustInherit Class BaseLocalAttribute
        Inherits Attribute

        Public Property Value As Integer
        Public Value2 As Integer
    End Class

    Public Class RealAttribute
        Inherits BaseLocalAttribute
    End Class

    Public Class RemoteAttribute
        Inherits BaseAttribute
    End Class

    Friend Class TestClass
        <Real(Value:=1, Value2:=2)>
        <Remote>
        Public Sub [Do]()
        End Sub
    End Class

    Public Class InheritedInjectionsTests
        <Fact>
        Public Sub InheritedInjection()
            Passed = False
            Call New TestClass().Do()
            Assert.True(Passed)
        End Sub
    End Class
End Namespace
