Imports System
Imports System.Runtime.InteropServices
Imports AspectInjector.Broker

Namespace General
    'The compilation is just failed if injector tries to process external methods
    <UnmanagedTests_Aspect>
    Public Class UnmanagedTests
        <DllImport("user32.dll", CharSet:=CharSet.Auto)>
        Public Shared Function MessageBox(ByVal hWnd As IntPtr, ByVal text As String, ByVal caption As String, ByVal options As Integer) As Integer
        End Function
    End Class

    <Aspect(Scope.Global)>
    <Injection(GetType(UnmanagedTests_Aspect))>
    Public Class UnmanagedTests_Aspect
        Inherits Attribute

        <Advice(Kind.After, Targets:=Target.Method)>
        Public Sub Trace()
        End Sub
    End Class
End Namespace
