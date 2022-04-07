Imports System
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Advices
    Public Class NestedClassesTests
        <Fact>
        Public Sub Advices_InjectBeforeMethod_NestedClass()
            Passed = False
            Dim testClass = New NestedClassesTests_Target()
            testClass.Do()
            Assert.True(Passed)
        End Sub

        <NestedClassesTests_Aspect>
        Private Class NestedClassesTests_Target
            Public Sub [Do]()
            End Sub
        End Class
    End Class

    <Aspect(Scope.Global)>
    <Injection(GetType(NestedClassesTests_Aspect))>
    Public Class NestedClassesTests_Aspect
        Inherits Attribute

        <Advice(Kind.Before, Targets:=Target.Method)>
        Public Sub Fact()
            Passed = True
        End Sub
    End Class
End Namespace
