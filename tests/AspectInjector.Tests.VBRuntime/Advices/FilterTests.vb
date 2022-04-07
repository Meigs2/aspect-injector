Imports System
Imports System.Runtime.InteropServices
Imports AspectInjector.Broker
Imports AspectInjector.Tests.VBRuntime.Utils
Imports Xunit

Namespace Advices
    <SkipUnmanagedAndAbstractTests_Aspect>
    Public MustInherit Class SkipAbstractTests
        Public MustOverride Function MessageBox() As Integer
    End Class

    <SkipUnmanagedAndAbstractTests_Aspect>
    Public Class SkipUnmanagedTests
        <DllImport("user32.dll", CharSet:=CharSet.Auto)>
        Public Shared Function MessageBox(ByVal hWnd As IntPtr, ByVal text As String, ByVal caption As String, ByVal options As Integer) As Integer
        End Function
    End Class

    <Aspect(Scope.Global)>
    <Injection(GetType(SkipUnmanagedAndAbstractTests_Aspect))>
    Public Class SkipUnmanagedAndAbstractTests_Aspect
        Inherits Attribute

        <Advice(Kind.Around)>
        Public Function Trace() As Object
            Return 0
        End Function
    End Class

    Public Class FilterTests
        <Fact>
        Public Sub Advices_InjectAfterMethod_NameFilter()
            Passed = False
            Dim a = New FilterTests_Target()
            a.Do123()
            Assert.True(Passed)
        End Sub

        <FilterTests_Aspect>
        Public Class FilterTests_Target
            <FilterTests_Aspect>
            Public Sub Do123()
            End Sub
        End Class

        <Aspect(Scope.Global)>
        <Injection(GetType(FilterTests_Aspect))>
        Public Class FilterTests_Aspect
            Inherits Attribute

            Public Counter As Integer = 0

            <Advice(Kind.After, Targets:=Target.Method)>
            Public Sub AfterMethod()
                Counter += 1
                Passed = Counter = 1
            End Sub
        End Class
    End Class
End Namespace
