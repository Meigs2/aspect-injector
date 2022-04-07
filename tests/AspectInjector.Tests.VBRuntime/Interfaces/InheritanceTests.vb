Imports System
Imports AspectInjector.Broker
Imports Xunit

Namespace Interfaces
    Public Class InheritanceTests
        <Fact>
        Public Sub Interfaces_InjectionSupportsInheritance()
            Dim ti = CType(New InheritanceTests_Target(), IInheritanceTests)
            Dim r1 = ti.GetAspectType()
            Dim tib = CType(New InheritanceTests_Base(), IInheritanceTests)
            Dim r2 = tib.GetAspectType()
            Assert.Equal(r1, r2)
        End Sub

        <InheritanceTests_Aspect>
        Public Class InheritanceTests_Base
        End Class

        <InheritanceTests_Aspect>
        Public Class InheritanceTests_Target
            Inherits InheritanceTests_Base
        End Class

        Public Interface IInheritanceTests
            Function GetAspectType() As String
            Function GetAspectHash() As Integer
        End Interface

        <Mixin(GetType(IInheritanceTests))>
        <Aspect(Scope.Global)>
        <Injection(GetType(InheritanceTests_Aspect))>
        Public Class InheritanceTests_Aspect
            Inherits Attribute
            Implements IInheritanceTests

            Public Function GetAspectType() As String Implements IInheritanceTests.GetAspectType
                Return [GetType]().ToString()
            End Function

            Public Function GetAspectHash() As Integer Implements IInheritanceTests.GetAspectHash
                Return GetHashCode()
            End Function
        End Class
    End Class
End Namespace
