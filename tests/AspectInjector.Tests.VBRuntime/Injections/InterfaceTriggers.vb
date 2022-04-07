Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Injections
    Public Class InterfaceTriggers
        <Fact>
        Public Sub Interface_Triggers_Work()
            Passed = False
            Call New TestTarget().Do()
            Assert.True(Passed)
        End Sub

        <Aspect(Scope.Global)>
        Public Class TestAspect
            <Advice(Kind.Before, Targets:=Target.Method)>
            Public Sub Before()
                Passed = True
            End Sub
        End Class

        <Injection(GetType(TestAspect))>
        Public Interface INeedInjection
        End Interface

        Public Class TestTarget
            Implements INeedInjection

            Public Sub [Do]()
            End Sub
        End Class
    End Class
End Namespace
