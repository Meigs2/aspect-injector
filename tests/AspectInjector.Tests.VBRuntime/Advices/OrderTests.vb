Imports System
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Advices
    Public Class OrderTests
        Private _beforeTestClass As OrderTests_Target = New OrderTests_Target()

        <Fact>
        Public Sub Advices_InjectBeforeMethod_Ordered()
            Passed = False
            _beforeTestClass.Fact()
            Assert.True(Passed)
        End Sub

        Friend Class OrderTests_Target
            <Trigger>
            Public Sub Fact()
            End Sub
        End Class

        <Injection(GetType(OrderTests_Aspect1), Priority:=0)>
        <Injection(GetType(OrderTests_Aspect3), Priority:=2)>
        <Injection(GetType(OrderTests_Aspect2), Priority:=1)>
        Friend Class Trigger
            Inherits Attribute
        End Class

        <Aspect(Scope.Global)>
        Public Class OrderTests_Aspect1
            <Advice(Kind.Before)>
            Public Sub BeforeMethod()
                Passed = False
            End Sub
        End Class

        <Aspect(Scope.Global)>
        Public Class OrderTests_Aspect2
            <Advice(Kind.Before)>
            Public Sub BeforeMethod()
                Passed = False
            End Sub
        End Class

        <Aspect(Scope.Global)>
        Public Class OrderTests_Aspect3
            <Advice(Kind.Before)>
            Public Sub BeforeMethod()
                Passed = True
            End Sub
        End Class
    End Class
End Namespace
