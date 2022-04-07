Imports System
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace General
    Public Class AspectFactoryTests
        <Fact>
        Public Sub General_AspectFactory_CreateAspect()
            Passed = False
            Dim test = New AspectFactoryTests_Target()
            Assert.True(Passed)
        End Sub
    End Class

    <AspectFactoryTests_Aspect>
    Public Class AspectFactoryTests_Target
    End Class

    <Aspect(Scope.PerInstance, Factory:=GetType(AspectFactory))>
    <Injection(GetType(AspectFactoryTests_Aspect))>
    Public Class AspectFactoryTests_Aspect
        Inherits Attribute

        Private Shared aaa As Object

        <Advice(Kind.After, Targets:=Target.Constructor)>
        Public Sub Fact()
        End Sub

        Private Shared Sub ololo()
            aaa = AspectFactory.GetInstance(GetType(AspectFactoryTests_Aspect))
        End Sub
    End Class

    Public Class AspectFactory
        Public Shared Function GetInstance(ByVal aspectType As Type) As Object
            Passed = True
            Return Activator.CreateInstance(aspectType)
        End Function
    End Class
End Namespace
