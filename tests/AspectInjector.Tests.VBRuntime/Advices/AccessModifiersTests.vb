Imports System
Imports System.Reflection
Imports AspectInjector.Broker
Imports Xunit

Namespace Advices
    Public Class AccessModifiersTests
        Private _testClass As TestTarget = New TestTarget()

        <Fact>
        Public Sub Advices_Inject_Into_Internal_Static()
            Call TestTarget.InternalStatic()
        End Sub

        <Fact>
        Public Sub Advices_Inject_Into_Internal()
            _testClass.Internal()
        End Sub

        <Fact>
        Public Sub Advices_Inject_Skips_Public()
            _testClass.Public()
        End Sub

        <AccessTestAspect>
        Friend Class TestTarget
            Public Sub [Public]()
            End Sub

            Friend Sub Internal()
                Assert.True(False)
            End Sub

            Friend Shared Sub InternalStatic()
                Assert.True(False)
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(AccessTestAspect))>
        Public Class AccessTestAspect
            Inherits Attribute

            <Advice(Kind.Around, Targets:=Target.Internal)>
            Public Function TestAccess(
            <Argument(Source.Metadata)> ByVal method As MethodBase) As Object
                Assert.True(method.IsAssembly)
                Assert.False(method.IsPublic)
                Return Nothing
            End Function
        End Class
    End Class
End Namespace
