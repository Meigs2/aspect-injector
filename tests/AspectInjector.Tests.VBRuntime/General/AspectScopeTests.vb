Imports System
Imports AspectInjector.Broker
Imports Xunit

Namespace General
    Public Class AspectScopeTests
        <Fact>
        Public Sub SCOPE_Create_Aspect_Per_Instance()
            AspectScopeTests_PerInstanceAspect._counter = 0

            For i = 0 To 9
                Dim t = New AspectScopeTests_Target()
                Assert.Equal(i + 1, AspectScopeTests_PerInstanceAspect._counter)
            Next
        End Sub

        <Fact>
        Public Sub SCOPE_Create_Global_Aspect()
            For i = 0 To 9
                Dim t = New AspectScopeTests_Target()
                Assert.Equal(1, AspectScopeTests_GlobalAspect._counter)
            Next
        End Sub
    End Class

    <Aspect(Scope.PerInstance)>
    <Injection(GetType(AspectScopeTests_PerInstanceAspect))>
    Public Class AspectScopeTests_PerInstanceAspect
        Inherits Attribute

        Public Shared _counter As Integer

        Public Sub New()
            _counter += 1
        End Sub

        <Advice(Kind.Before, Targets:=Target.Method)>
        Public Sub [Do]()
        End Sub
    End Class

    <Aspect(Scope.Global)>
    <Injection(GetType(AspectScopeTests_GlobalAspect))>
    Public Class AspectScopeTests_GlobalAspect
        Inherits Attribute

        Public Shared _counter As Integer

        Public Sub New()
            _counter += 1
        End Sub

        <Advice(Kind.Before, Targets:=Target.Method)>
        Public Sub [Do]()
        End Sub
    End Class

    <AspectScopeTests_PerInstanceAspect>
    <AspectScopeTests_GlobalAspect>
    Friend Class AspectScopeTests_Target
        Public Sub Fact()
        End Sub
    End Class
End Namespace
