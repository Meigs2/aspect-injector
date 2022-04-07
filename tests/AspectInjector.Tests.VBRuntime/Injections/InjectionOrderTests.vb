Imports System
Imports AspectInjector.Broker
Imports Xunit

Namespace Injections
    Public Class InjectionOrderTests
        <Fact>
        Public Sub AspectInstance_InHierarchy_MustBeOne()
            Dim target = New TestInjectionTarget()
            Assert.Equal(1, target.Test1())
            Assert.Equal(2, target.Test2())
        End Sub

        <Aspect(Scope.PerInstance)>
        <Injection(GetType(TestAspect))>
        Public Class TestAspect
            Inherits Attribute

            Private _Count As Integer

            Public Property Count As Integer
                Get
                    Return _Count
                End Get
                Private Set(ByVal value As Integer)
                    _Count = value
                End Set
            End Property

            <Advice(Kind.Around, Targets:=Target.Method)>
            Public Function Test(
            <Argument(Source.Arguments)> ByVal arguments As Object(),
            <Argument(Source.Target)> ByVal target As Func(Of Object(), Object)) As Object
                Count += 1
                target(arguments)
                Return Count
            End Function
        End Class

        <TestAspect>
        Private Class TestInjectionTarget
            Inherits TestInjectionTargetBase

            Public Function Test2() As Integer
                Return 0
            End Function
        End Class

        <TestAspect>
        Private Class TestInjectionTargetBase
            Public Function Test1() As Integer
                Return 0
            End Function
        End Class
    End Class
End Namespace
