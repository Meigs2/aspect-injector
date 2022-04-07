Imports System
Imports AspectInjector.Broker
Imports Xunit

Namespace Advices
    Public Class DebugTests
        <Fact()>
        Public Sub DebugWorks()
            Call New TestClass().Do()
        End Sub

        <Aspect(Scope.Global)>
        <Injection(GetType(TestAspect))>
        Public Class TestAspect
            Inherits Attribute

            <Advice(Kind.Around)>
            Public Function Around(
            <Argument(Source.Arguments)> ByVal args As Object(),
            <Argument(Source.Target)> ByVal target As Func(Of Object(), Object)) As Object
                Return target(args)
            End Function
        End Class

        Public Class TestClass
            <TestAspect>
            Public Sub [Do]()
                Trace.WriteLine("Test")
            End Sub
        End Class
    End Class
End Namespace
