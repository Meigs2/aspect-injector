Imports System
Imports AspectInjector.Broker

Namespace Issues
    Public Class Issue_0140
        <Aspect(Scope.PerInstance)>
        <Injection(GetType(TestAspect))>
        Public Class TestAspect
            Inherits Attribute

            <Advice(Kind.Before)>
            Public Sub Before()
            End Sub
        End Class

        <Aspect(Scope.PerInstance)>
        <Injection(GetType(TestAspect))>
        Public Class TestAspect2
            Inherits Attribute

            <Advice(Kind.Before)>
            Public Sub Before()
            End Sub
        End Class

        <TestAspect>
        <TestAspect2>
        Friend Class ArgumentsTests_GenericClassConstructorChainTargetImpl
            Inherits ArgumentsTests_GenericClassConstructorChainTarget(Of String)
        End Class

        <TestAspect>
        Friend MustInherit Class ArgumentsTests_GenericClassConstructorChainTarget(Of T As Class)
            Public Sub New()
            End Sub

            Public Sub New(ByVal a As Integer)
                Me.New()
            End Sub

            Public Sub New(ByVal a As Integer, ByVal b As Integer)
                Me.New(a)
            End Sub

            Public Sub Fact()
            End Sub
        End Class
    End Class
End Namespace
