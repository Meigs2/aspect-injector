Imports System
Imports AspectInjector.Broker
Imports Xunit

Namespace General
    Public Class PropagationControlTests
        Private ReadOnly _testTarget As TestClass

        Public Sub New()
            _testTarget = New TestClass()
        End Sub

        <Fact>
        Public Sub Propagation_Options_Correctly_Processed()
            _testTarget.Method()
            AddHandler _testTarget.[Event], Sub(s, e)
                                            End Sub

            AddHandler _testTarget.Ev2ent, Sub(s, e)
                                           End Sub

            _testTarget.Property = ""
        End Sub

        <EventTrigger>
        <PropertyTrigger>
        <MethodTrigger>
        Public Class TestClass
            Public Event [Event] As EventHandler
            Public Event Ev2ent As EventHandler
            Public Property [Property] As String

            Public Sub Method()
            End Sub
        End Class

        <Injection(GetType(SuccessAspect), Propagation:=PropagateTo.Events, PropagationFilter:="Event")>
        Public Class EventTrigger
            Inherits Attribute
        End Class

        <Injection(GetType(SuccessAspect), Propagation:=PropagateTo.Properties)>
        Public Class PropertyTrigger
            Inherits Attribute
        End Class

        <Injection(GetType(SuccessAspect), Propagation:=PropagateTo.Methods)>
        Public Class MethodTrigger
            Inherits Attribute
        End Class

        <Aspect(Scope.Global)>
        Public Class SuccessAspect
            <Advice(Kind.Before, Targets:=Target.Method Or Target.EventAdd Or Target.EventRemove Or Target.Getter Or Target.Setter)>
            Public Sub Success(
            <Argument(Source.Name)> ByVal name As String,
            <Argument(Source.Triggers)> ByVal triggers As Attribute())
                Select Case name
                    Case NameOf(TestClass.[Event])
                        Assert.True(triggers.Length = 1 AndAlso TypeOf triggers(0) Is EventTrigger)
                    Case NameOf(TestClass.Property)
                        Assert.True(triggers.Length = 1 AndAlso TypeOf triggers(0) Is PropertyTrigger)
                    Case NameOf(TestClass.Method)
                        Assert.True(triggers.Length = 1 AndAlso TypeOf triggers(0) Is MethodTrigger)
                    Case Else
                        Assert.True(False)
                End Select
            End Sub
        End Class
    End Class
End Namespace
